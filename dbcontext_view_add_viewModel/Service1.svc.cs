using dbcontext_view_add_viewModel.DatabaseContext;
using dbcontext_view_add_viewModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace dbcontext_view_add_viewModel
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public readonly EndocDataContext endocDataContext;
        public string ConnectionString = ConfigurationManager.ConnectionStrings["endoc"].ConnectionString;

        public Service1()
        {
            endocDataContext = new EndocDataContext();
        }
        public IList<ProviderLocations> GetProviderLocations()
        {

            var location = (from a in endocDataContext.GetProviderLocations.Where(x => x.Deleted !=true)
                            select
                            new
                            {
                                a.ProviderID,
                                a.ProviderLocationID,
                                a.CreatedBy,
                                a.CreatedDate,
                                a.Deleted,
                                a.ModifiedDate,
                                a.FacilityID,
                                //a.EffectiveDate,
                                //a.TerminationDate
                            }).AsEnumerable().Select(cc => new ProviderLocations

                            {
                                ProviderID =cc.ProviderID,
                                ProviderLocationID = cc.ProviderLocationID,
                                CreatedBy =cc.CreatedBy,
                                CreatedDate =cc.CreatedDate,
                                Deleted =cc.Deleted,
                                name =cc.CreatedBy,
                                namefile =cc.CreatedBy,
                                ModifiedDate =cc.ModifiedDate,
                                FacilityID =cc.FacilityID,
                                EffectiveDate = DateTime.Now,//cc.EffectiveDate,
                                TerminationDate =  DateTime.Now,//cc.TerminationDate
                            }).ToList();
            this.addlocation(location[3]);
            return location;

            //EndocDataContext obj = new EndocDataContext(ConnectionString);
            //var location = (from s in obj.GetProviderLocations
            //                select s).ToList();
            //return location;
        }
        public void addlocation(ProviderLocations locatos)
        {
            var mylocation = (from a in endocDataContext.GetProviderLocations.Where(x => x.ProviderID == locatos.ProviderID & x.Deleted !=true) select a).OrderByDescending(x => x.ProviderID).FirstOrDefault();
            mylocation.Deleted =true;
            mylocation.ModifiedDate = DateTime.Now;
            mylocation.ModifiedBy ="userss";

            endocDataContext.Entry(mylocation).State =System.Data.Entity.EntityState.Modified;
            endocDataContext.SaveChanges();
        }
    }
}

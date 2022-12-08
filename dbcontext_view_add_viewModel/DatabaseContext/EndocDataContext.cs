using dbcontext_view_add_viewModel.eitityModel;
using dbcontext_view_add_viewModel.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace dbcontext_view_add_viewModel.DatabaseContext
{
    public class EndocDataContext : DbContext
    {

        public EndocDataContext() : base("name=EndocDataContext")
        {

        }

        public EndocDataContext(string ConnectionString)
            : base(ConnectionString)
        {

        }
        public virtual DbSet<ProviderLocation> GetProviderLocations { get; set; }
    }

}
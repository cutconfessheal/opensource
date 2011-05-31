using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entites = Research.Services.Northwind.NorthwindDataSetEntities;
using Research.Services.Northwind.NorthwindDataSetExtensions;

namespace Research.WebApplication
{
    public partial class Default : Impl.ApplicationPage
    {
        public string GetFrom = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            var data = NWCacheProvider.Get<IEnumerable<Entites.Product>>("NWProducts");
            if (data == null)
            {
                GetFrom = "Database";
                data = NWCacheProvider.Add<IEnumerable<Entites.Product>>("NWProducts", GetAll, 60, false);
            }
            else
            {
                GetFrom = "Cache";
            }
            NWDataSet.Product.Merge(data.AsDataTable());
        }

        IEnumerable<Entites.Product> GetAll()
        {
            return NWServiceManager.ProductRepository.GetAll().AsEntities();
        }

        public IEnumerable<Entites.Product> NWProducts
        {
            get
            {
                return NWDataSet.Product.AsEntities();
            }
        }
        
    }
}
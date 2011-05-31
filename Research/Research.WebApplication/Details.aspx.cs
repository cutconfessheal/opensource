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
    public partial class Details : Impl.ApplicationPage
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

        public Entites.Product CurrentProduct
        {
            get
            {
                int id = -1;
                if (!String.IsNullOrWhiteSpace(Request.QueryString["id"]))
                    if (!int.TryParse(Request.QueryString["id"].Trim(), out id))
                        id = -1;
                if (id < 1)
                    return null;
                return NWDataSet.Product.SingleOrDefault(model => model.ProductID == id).AsEntity();
            }
        }
    }
}
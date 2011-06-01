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
        public Entites.Product GetProduct()
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
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
        protected void Page_Load(object sender, EventArgs e)
        {
            DataSet.Product.Merge(CacheProvider.Get<IEnumerable<Entites.Product>>("Products_GetAll").AsDataTable());
        }

        public IEnumerable<Entites.Product> GetAll()
        {
            return DataSet.Product.AsEntities();
        }

        public void ANONYM_Button_ResetCache_OnClick(object sender, EventArgs e)
        {
            CacheProvider.Clear();
            HttpContext.Current.Response.Redirect("~/Default.aspx", true);
        }
    }
}
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
        }

        public IEnumerable<Entites.Product> GetAll()
        {
            return NWDataSet.Product.AsEntities();
        }
    }
}
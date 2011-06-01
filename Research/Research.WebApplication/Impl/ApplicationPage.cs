using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entites = Research.Services.Northwind.NorthwindDataSetEntities;
using Research.Services.Northwind.NorthwindDataSetExtensions;

namespace Research.WebApplication.Impl
{
    public class ApplicationPage : System.Web.UI.Page
    {
        private Research.DataAccess.Northwind.NorthwindDataSet m_DataSet = null;
        private Research.DataAccess.Northwind.NorthwindDataSetTableAdapters.TableAdapterManager m_TableAdapterManager = null;
        private Research.Services.Northwind.NorthwindDataSetServices.IServiceManager m_ServiceManager = null;
        private Research.Core.Cache.CacheProvider m_CacheProvider = null;
        Dictionary<string, bool> m_IsCached = null;

        public ApplicationPage()
        {
            m_DataSet = new DataAccess.Northwind.NorthwindDataSet();
            m_TableAdapterManager = new DataAccess.Northwind.NorthwindDataSetTableAdapters.TableAdapterManager();
            m_ServiceManager = new Research.Services.Northwind.NorthwindDataSetServices.ServiceManager(m_TableAdapterManager);
            m_IsCached = new Dictionary<string, bool>();
            PreInit += new EventHandler(ApplicationPage_PreInit);
        }

        void ApplicationPage_PreInit(object sender, EventArgs e)
        {
            m_CacheProvider = new Core.Cache.CacheProvider(Cache);
            LoadVars();
        }

        private void LoadVars()
        {
            var products = NWCacheProvider.Get<IEnumerable<Entites.Product>>("NWProducts");
            m_IsCached.Add("NWProducts", !(products == null));
            if (products == null)
                products = NWCacheProvider.Add<IEnumerable<Entites.Product>>("NWProducts", Products_GetAll, 60, false);
            NWDataSet.Product.Merge(products.AsDataTable());

            var categories = NWCacheProvider.Get<IEnumerable<Entites.Category>>("NWCategories");
            m_IsCached.Add("NWCategories", !(products == null));
            if (categories == null)
                categories = NWCacheProvider.Add<IEnumerable<Entites.Category>>("NWCategories", Categories_GetAll, 60, false);
            NWDataSet.Category.Merge(categories.AsDataTable());

            var suppliers = NWCacheProvider.Get<IEnumerable<Entites.Supplier>>("NWSuppliers");
            m_IsCached.Add("NWSuppliers", !(products == null));
            if (suppliers == null)
                suppliers = NWCacheProvider.Add<IEnumerable<Entites.Supplier>>("NWSuppliers", Suppliers_GetAll, 60, false);
            NWDataSet.Supplier.Merge(suppliers.AsDataTable());
        }

        IEnumerable<Entites.Product> Products_GetAll()
        {
            return NWServiceManager.ProductRepository.GetAll().AsEntities();
        }

        IEnumerable<Entites.Category> Categories_GetAll()
        {
            return NWServiceManager.CategoryRepository.GetAll().AsEntities();
        }

        IEnumerable<Entites.Supplier> Suppliers_GetAll()
        {
            return NWServiceManager.SupplierRepository.GetAll().AsEntities();
        }

        public override void Dispose()
        {
            if (m_TableAdapterManager != null)
                m_TableAdapterManager.Dispose();
            if (m_DataSet != null)
                m_DataSet.Dispose();
            m_ServiceManager = null;
            m_TableAdapterManager = null;
            m_DataSet = null;
            base.Dispose();
        }

        protected Research.DataAccess.Northwind.NorthwindDataSet NWDataSet { get { return m_DataSet; } }
        protected Research.Services.Northwind.NorthwindDataSetServices.IServiceManager NWServiceManager { get { return m_ServiceManager; } }
        protected Research.Core.Cache.CacheProvider NWCacheProvider { get { return m_CacheProvider; } }
        protected Dictionary<string, bool> NWIsCached { get { return m_IsCached; } }
    }
}
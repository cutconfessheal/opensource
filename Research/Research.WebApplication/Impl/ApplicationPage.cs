using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entites = Research.Services.Northwind.NorthwindDataSetEntities;
using Research.Services.Northwind.NorthwindDataSetExtensions;
using System.Web.Caching;

namespace Research.WebApplication.Impl
{
    public class ApplicationPage : System.Web.UI.Page
    {
        private Research.DataAccess.Northwind.NorthwindDataSet m_DataSet = null;
        private Research.DataAccess.Northwind.NorthwindDataSetTableAdapters.TableAdapterManager m_TableAdapterManager = null;
        private Research.Services.Northwind.NorthwindDataSetServices.IServiceManager m_ServiceManager = null;
        private Research.Core.Web.Cache.CacheProvider m_CacheProvider = null;
        Dictionary<string, bool> m_IsCached = null;

        public ApplicationPage()
        {
            m_DataSet = new DataAccess.Northwind.NorthwindDataSet();
            m_TableAdapterManager = new DataAccess.Northwind.NorthwindDataSetTableAdapters.TableAdapterManager();
            m_ServiceManager = new Research.Services.Northwind.NorthwindDataSetServices.ServiceManager(m_TableAdapterManager);
            m_IsCached = new Dictionary<string, bool>();
            PreLoad += new EventHandler(ApplicationPage_PreLoad);
        }

        void ApplicationPage_PreLoad(object sender, EventArgs e)
        {
            m_CacheProvider = new Research.Core.Web.Cache.CacheProvider(Cache);
            LoadVars();
        }

        private void LoadVars()
        {
            if (CacheProvider.IsNull("Products_GetAll"))
                CacheProvider.Add<IEnumerable<Entites.Product>>("Products_GetAll", Products_GetAll, 3600, CacheItemPriority.High);

            if (CacheProvider.IsNull("Categories_GetAll"))
                CacheProvider.Add<IEnumerable<Entites.Category>>("Categories_GetAll", Categories_GetAll, 3600, CacheItemPriority.High);

            if (CacheProvider.IsNull("Suppliers_GetAll"))
                CacheProvider.Add<IEnumerable<Entites.Supplier>>("Suppliers_GetAll", Suppliers_GetAll, 3600, CacheItemPriority.High);
        }

        IEnumerable<Entites.Product> Products_GetAll()
        {
            return ServiceManager.ProductRepository.GetAll().AsEntities();
        }

        IEnumerable<Entites.Category> Categories_GetAll()
        {
            return ServiceManager.CategoryRepository.GetAll().AsEntities();
        }

        IEnumerable<Entites.Supplier> Suppliers_GetAll()
        {
            return ServiceManager.SupplierRepository.GetAll().AsEntities();
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

        protected Research.DataAccess.Northwind.NorthwindDataSet DataSet { get { return m_DataSet; } }
        protected Research.Services.Northwind.NorthwindDataSetServices.IServiceManager ServiceManager { get { return m_ServiceManager; } }
        protected Research.Core.Web.Cache.CacheProvider CacheProvider { get { return m_CacheProvider; } }
        protected Dictionary<string, bool> IsCached { get { return m_IsCached; } }
    }
}
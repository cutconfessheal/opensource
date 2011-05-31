using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Research.WebApplication.Impl
{
    public class ApplicationPage : System.Web.UI.Page
    {
        private Research.DataAccess.Northwind.NorthwindDataSet m_DataSet = null;
        private Research.DataAccess.Northwind.NorthwindDataSetTableAdapters.TableAdapterManager m_TableAdapterManager = null;
        private Research.Services.Northwind.NorthwindDataSetServices.IServiceManager m_ServiceManager = null;
        private Research.Core.Cache.CacheProvider m_CacheProvider = null;

        public ApplicationPage()
        {
            m_DataSet = new DataAccess.Northwind.NorthwindDataSet();
            m_TableAdapterManager = new DataAccess.Northwind.NorthwindDataSetTableAdapters.TableAdapterManager();
            m_ServiceManager = new Research.Services.Northwind.NorthwindDataSetServices.ServiceManager(m_TableAdapterManager);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            m_CacheProvider = new Core.Cache.CacheProvider(Cache);
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
    }
}
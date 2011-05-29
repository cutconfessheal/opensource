using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Research.Tests
{
    using Sets = Research.DataAccess.Northwind;
    using Adapters = Research.DataAccess.Northwind.NorthwindDataSetTableAdapters;
    using Entities = Research.Services.Northwind.NorthwindDataSetEntities;
    using Services = Research.Services.Northwind.NorthwindDataSetServices;
    using Research.Services.Northwind.NorthwindDataSetExtensions;

    /// <summary>
    /// Summary description for ResearchServicesUnitTest
    /// </summary>
    [TestClass]
    public class NorthwindServicesUnitTest
    {
        public NorthwindServicesUnitTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void Entities()
        {
            Sets.NorthwindDataSet set = null;
            Sets.NorthwindDataSet.ProductDataTable table = null;
            Adapters.TableAdapterManager tableAdapterManager = null;
            Services.ServiceManager serviceManager = null;

            try
            {
                set = new Sets.NorthwindDataSet();
                tableAdapterManager = new Adapters.TableAdapterManager();
                serviceManager = new Services.ServiceManager(tableAdapterManager);

                serviceManager.ProductRepository.Fill(set.Product);
                
                var entity = set.Product[0].AsEntity();
                var entities = set.Product.AsEntities();
                
                table = entities.AsDataTable();
                
                Assert.IsTrue(set.Product.Count() > 0);

                Assert.IsNotNull(entity);
                Assert.IsTrue(entity.ProductID == 1);

                Assert.IsNotNull(entities);
                Assert.IsTrue(entities.Count() > 0);
                Assert.IsTrue(entities.Count() == 77);

                Assert.IsTrue(table.Count() == 77);
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                Assert.IsNull(err);
            }

            if(tableAdapterManager != null)
                tableAdapterManager.Dispose();

            if (table != null)
                table.Dispose();

            if(set != null)
                set.Dispose();

            serviceManager = null;
            tableAdapterManager = null;
            table = null;
            set = null;
        }
    }
}

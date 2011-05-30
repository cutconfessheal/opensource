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
        public void ServiceManager_Entities()
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

        [TestMethod]
        public void ServiceManager_Insert()
        {
            Sets.NorthwindDataSet set = null;
            Adapters.TableAdapterManager tableAdapterManager = null;
            Services.ServiceManager serviceManager = null;
            
            try
            {
                // init defaults
                set = new Sets.NorthwindDataSet();
                tableAdapterManager = new Adapters.TableAdapterManager();
                serviceManager = new Services.ServiceManager(tableAdapterManager);
                
                // data modifier
                Random rand = new Random();
                
                // if -1 then something bad will happen!
                int affected = -1;
                
                // create new entity
                var category =
                    new Entities.Category(set.Category.NewCategoryRow())
                    {
                        CategoryName = "TEST-" + rand.Next(0, 100),
                        Description = "DESC-" + rand.Next(100, 200),
                        Picture = null
                    };
                
                // add to table as datarow (magic happens here!)
                set.Category.AddCategoryRow(category.AsDataRow());
                
                // call update method (magic happens here, because it will call an insert method then update entity id and return affected rows number)
                affected = serviceManager.CategoryRepository.Update(category);

                // assert
                Assert.IsTrue(affected == 1);
                Assert.IsTrue(category.CategoryID > 0);
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                Assert.IsNull(err);
            }

            if (tableAdapterManager != null) tableAdapterManager.Dispose();
            if (set != null) set.Dispose();
            serviceManager = null;
            tableAdapterManager = null;
            set = null;
        }

        [TestMethod]
        public void ServiceManager_Update()
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
                Random rand = new Random();
                serviceManager.ProductRepository.Fill(set.Product);

                var product = set.Product.FindByProductID(1).AsEntity();
                product.UnitsInStock = (short)rand.Next(1, 100);
                var x = serviceManager.ProductRepository.Update(product);

                var all = set.Product.AsEntities();
                all.ToList().ForEach(model => model.UnitsInStock = (short)rand.Next(0, 100));
                var y = serviceManager.ProductRepository.Update(all.ToArray());

                Console.WriteLine("{0}, {1}", x, y);

                Assert.IsTrue(x == 1);
                Assert.IsTrue(y == 77);
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                Assert.IsNull(err);
            }

            if (tableAdapterManager != null) tableAdapterManager.Dispose();
            if (table != null) table.Dispose();
            if (set != null) set.Dispose();
            serviceManager = null;
            tableAdapterManager = null;
            table = null;
            set = null;
        }

        [TestMethod]
        public void ServiceManager_Delete()
        {
            Sets.NorthwindDataSet set = null;
            Adapters.TableAdapterManager tableAdapterManager = null;
            Services.ServiceManager serviceManager = null;

            try
            {
                // init defaults
                set = new Sets.NorthwindDataSet();
                tableAdapterManager = new Adapters.TableAdapterManager();
                serviceManager = new Services.ServiceManager(tableAdapterManager);

                // data modifier
                Random rand = new Random();

                // if -1 then something bad will happen!
                int affected = -1;

                // get all and select the last
                serviceManager.CategoryRepository.Fill(set.Category);
                var category = set.Category.AsEntities().First(x => x.CategoryID == set.Category.AsEntities().Max(y => y.CategoryID));

                // call update method (magic happens here, because it will call an insert method then update entity id and return affected rows number)
                affected = serviceManager.CategoryRepository.Delete(category);

                // accept changes
                set.Category.RemoveCategoryRow(category.AsDataRow());
                set.Category.AcceptChanges();

                // assert
                Assert.IsTrue(affected == 1);
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                Assert.IsNull(err);
            }

            if (tableAdapterManager != null) tableAdapterManager.Dispose();
            if (set != null) set.Dispose();
            serviceManager = null;
            tableAdapterManager = null;
            set = null;
        }
    }
}

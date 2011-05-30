using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

namespace Research.ConsoleApplication
{
    using Sets = Research.DataAccess.Northwind;
    using Adapters = Research.DataAccess.Northwind.NorthwindDataSetTableAdapters;
    using Entities = Research.Services.Northwind.NorthwindDataSetEntities;
    using Services = Research.Services.Northwind.NorthwindDataSetServices;
    using Research.Services.Northwind.NorthwindDataSetExtensions;

    class Program
    {
        static void Main(string[] args)
        {
            Sets.NorthwindDataSet set = null;
            Adapters.TableAdapterManager tableAdapterManager = null;
            Services.IServiceManager serviceManager = null;

            try
            {
                set = new Sets.NorthwindDataSet();
                tableAdapterManager = new Adapters.TableAdapterManager();
                serviceManager = new Services.ServiceManager(tableAdapterManager);
                serviceManager.CategoryRepository.Fill(set.Category);
                serviceManager.ProductRepository.Fill(set.Product);
                foreach (var product in set.Product.AsEntities())
                    Console.WriteLine("ID: {0}, Name: {1}, Category: {2}", product.ProductID, product.ProductName, product.Category.CategoryName);
            }
            catch(SqlException err)
            {
                Console.WriteLine(err);
            }
            catch(Exception err)
            {
                Console.WriteLine(err.Message);
            }

            if(tableAdapterManager != null)
                tableAdapterManager.Dispose();

            if(set != null)
                set.Dispose();

            serviceManager = null;
            tableAdapterManager = null;
            set = null;

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}

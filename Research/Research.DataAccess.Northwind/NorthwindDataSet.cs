namespace Research.DataAccess.Northwind {
    public partial class NorthwindDataSet {
        partial class CustomerDataTable
        {
            private string _sourceTableName;

            public string SourceTableName
            {
                get
                {
                    if (string.IsNullOrWhiteSpace(_sourceTableName))
                        _sourceTableName = "Customers";
                    return _sourceTableName;
                }
            }
        }
    
        partial class ShipperDataTable
        {
            private string _sourceTableName;

            public string SourceTableName
            {
                get
                {
                    if (string.IsNullOrWhiteSpace(_sourceTableName))
                        _sourceTableName = "Shippers";
                    return _sourceTableName;
                }
            }
        }
    
        partial class OrderDataTable
        {
            private string _sourceTableName;

            public string SourceTableName
            {
                get
                {
                    if (string.IsNullOrWhiteSpace(_sourceTableName))
                        _sourceTableName = "Orders";
                    return _sourceTableName;
                }
            }
        }
    
        partial class EmployeeDataTable
        {
            private string _sourceTableName;

            public string SourceTableName
            {
                get
                {
                    if (string.IsNullOrWhiteSpace(_sourceTableName))
                        _sourceTableName = "Employees";
                    return _sourceTableName;
                }
            }
        }
    
        partial class OrderDetailDataTable
        {
            private string _sourceTableName;

            public string SourceTableName
            {
                get
                {
                    if (string.IsNullOrWhiteSpace(_sourceTableName))
                        _sourceTableName = "Order Details";
                    return _sourceTableName;
                }
            }
        }
    
        partial class SupplierDataTable
        {
            private string _sourceTableName;

            public string SourceTableName
            {
                get
                {
                    if (string.IsNullOrWhiteSpace(_sourceTableName))
                        _sourceTableName = "Suppliers";
                    return _sourceTableName;
                }
            }
        }
    
        partial class ProductDataTable
        {
            private string _sourceTableName;

            public string SourceTableName
            {
                get
                {
                    if (string.IsNullOrWhiteSpace(_sourceTableName))
                        _sourceTableName = "Products";
                    return _sourceTableName;
                }
            }
        }
    
        partial class CategoryDataTable
        {
            private string _sourceTableName;

            public string SourceTableName
            {
                get
                {
                    if (string.IsNullOrWhiteSpace(_sourceTableName))
                        _sourceTableName = "Categories";
                    return _sourceTableName;
                }
            }
        }
    }
}

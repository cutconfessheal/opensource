﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace Research.Services.Northwind.NorthwindDataSetServices
{
	public partial interface IServiceManager
	{
		System.Data.IDbConnection Connection { get; }
		Research.Services.Northwind.NorthwindDataSetRepositories.ICategoryRepository CategoryRepository { get; }
		Research.Services.Northwind.NorthwindDataSetRepositories.ICustomerRepository CustomerRepository { get; }
		Research.Services.Northwind.NorthwindDataSetRepositories.IEmployeeRepository EmployeeRepository { get; }
		Research.Services.Northwind.NorthwindDataSetRepositories.IOrderDetailRepository OrderDetailRepository { get; }
		Research.Services.Northwind.NorthwindDataSetRepositories.IOrderRepository OrderRepository { get; }
		Research.Services.Northwind.NorthwindDataSetRepositories.IProductRepository ProductRepository { get; }
		Research.Services.Northwind.NorthwindDataSetRepositories.IShipperRepository ShipperRepository { get; }
		Research.Services.Northwind.NorthwindDataSetRepositories.ISupplierRepository SupplierRepository { get; }
	}

	
	public partial class ServiceManager : Research.Services.Northwind.NorthwindDataSetServices.IServiceManager
	{
		private Research.DataAccess.Northwind.NorthwindDataSetTableAdapters.TableAdapterManager m_TableAdapterManager = null;
		private Research.Services.Northwind.NorthwindDataSetRepositories.ICategoryRepository m_CategoryRepository = null;
		private Research.Services.Northwind.NorthwindDataSetRepositories.ICustomerRepository m_CustomerRepository = null;
		private Research.Services.Northwind.NorthwindDataSetRepositories.IEmployeeRepository m_EmployeeRepository = null;
		private Research.Services.Northwind.NorthwindDataSetRepositories.IOrderDetailRepository m_OrderDetailRepository = null;
		private Research.Services.Northwind.NorthwindDataSetRepositories.IOrderRepository m_OrderRepository = null;
		private Research.Services.Northwind.NorthwindDataSetRepositories.IProductRepository m_ProductRepository = null;
		private Research.Services.Northwind.NorthwindDataSetRepositories.IShipperRepository m_ShipperRepository = null;
		private Research.Services.Northwind.NorthwindDataSetRepositories.ISupplierRepository m_SupplierRepository = null;

		public ServiceManager()
		{
		}

		public ServiceManager(Research.DataAccess.Northwind.NorthwindDataSetTableAdapters.TableAdapterManager manager)
		{
			m_TableAdapterManager = manager;
		}

		#region TableAdapters

			public Research.Services.Northwind.NorthwindDataSetRepositories.ICategoryRepository CategoryRepository
			{
				get
				{
					if(m_TableAdapterManager.CategoryTableAdapter == null)
					{
						m_TableAdapterManager.CategoryTableAdapter = new Research.DataAccess.Northwind.NorthwindDataSetTableAdapters.CategoryTableAdapter();
						m_CategoryRepository = new Research.Services.Northwind.NorthwindDataSetRepositories.CategoryRepository(m_TableAdapterManager.CategoryTableAdapter);
					}
					return m_CategoryRepository;
				}
			}
			public Research.Services.Northwind.NorthwindDataSetRepositories.ICustomerRepository CustomerRepository
			{
				get
				{
					if(m_TableAdapterManager.CustomerTableAdapter == null)
					{
						m_TableAdapterManager.CustomerTableAdapter = new Research.DataAccess.Northwind.NorthwindDataSetTableAdapters.CustomerTableAdapter();
						m_CustomerRepository = new Research.Services.Northwind.NorthwindDataSetRepositories.CustomerRepository(m_TableAdapterManager.CustomerTableAdapter);
					}
					return m_CustomerRepository;
				}
			}
			public Research.Services.Northwind.NorthwindDataSetRepositories.IEmployeeRepository EmployeeRepository
			{
				get
				{
					if(m_TableAdapterManager.EmployeeTableAdapter == null)
					{
						m_TableAdapterManager.EmployeeTableAdapter = new Research.DataAccess.Northwind.NorthwindDataSetTableAdapters.EmployeeTableAdapter();
						m_EmployeeRepository = new Research.Services.Northwind.NorthwindDataSetRepositories.EmployeeRepository(m_TableAdapterManager.EmployeeTableAdapter);
					}
					return m_EmployeeRepository;
				}
			}
			public Research.Services.Northwind.NorthwindDataSetRepositories.IOrderDetailRepository OrderDetailRepository
			{
				get
				{
					if(m_TableAdapterManager.OrderDetailTableAdapter == null)
					{
						m_TableAdapterManager.OrderDetailTableAdapter = new Research.DataAccess.Northwind.NorthwindDataSetTableAdapters.OrderDetailTableAdapter();
						m_OrderDetailRepository = new Research.Services.Northwind.NorthwindDataSetRepositories.OrderDetailRepository(m_TableAdapterManager.OrderDetailTableAdapter);
					}
					return m_OrderDetailRepository;
				}
			}
			public Research.Services.Northwind.NorthwindDataSetRepositories.IOrderRepository OrderRepository
			{
				get
				{
					if(m_TableAdapterManager.OrderTableAdapter == null)
					{
						m_TableAdapterManager.OrderTableAdapter = new Research.DataAccess.Northwind.NorthwindDataSetTableAdapters.OrderTableAdapter();
						m_OrderRepository = new Research.Services.Northwind.NorthwindDataSetRepositories.OrderRepository(m_TableAdapterManager.OrderTableAdapter);
					}
					return m_OrderRepository;
				}
			}
			public Research.Services.Northwind.NorthwindDataSetRepositories.IProductRepository ProductRepository
			{
				get
				{
					if(m_TableAdapterManager.ProductTableAdapter == null)
					{
						m_TableAdapterManager.ProductTableAdapter = new Research.DataAccess.Northwind.NorthwindDataSetTableAdapters.ProductTableAdapter();
						m_ProductRepository = new Research.Services.Northwind.NorthwindDataSetRepositories.ProductRepository(m_TableAdapterManager.ProductTableAdapter);
					}
					return m_ProductRepository;
				}
			}
			public Research.Services.Northwind.NorthwindDataSetRepositories.IShipperRepository ShipperRepository
			{
				get
				{
					if(m_TableAdapterManager.ShipperTableAdapter == null)
					{
						m_TableAdapterManager.ShipperTableAdapter = new Research.DataAccess.Northwind.NorthwindDataSetTableAdapters.ShipperTableAdapter();
						m_ShipperRepository = new Research.Services.Northwind.NorthwindDataSetRepositories.ShipperRepository(m_TableAdapterManager.ShipperTableAdapter);
					}
					return m_ShipperRepository;
				}
			}
			public Research.Services.Northwind.NorthwindDataSetRepositories.ISupplierRepository SupplierRepository
			{
				get
				{
					if(m_TableAdapterManager.SupplierTableAdapter == null)
					{
						m_TableAdapterManager.SupplierTableAdapter = new Research.DataAccess.Northwind.NorthwindDataSetTableAdapters.SupplierTableAdapter();
						m_SupplierRepository = new Research.Services.Northwind.NorthwindDataSetRepositories.SupplierRepository(m_TableAdapterManager.SupplierTableAdapter);
					}
					return m_SupplierRepository;
				}
			}

		#endregion TableAdapters

		public System.Data.IDbConnection Connection { get { return m_TableAdapterManager.Connection; } }
	}
}

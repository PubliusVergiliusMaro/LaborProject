using LaborProjectOOP.Constants.Enums;
using LaborProjectOOP.Database.Models;
using LaborProjectOOP.Services.AuthorServices;
using LaborProjectOOP.Services.BookServices;
using LaborProjectOOP.Services.CartListServices;
using LaborProjectOOP.Services.CatalogServices;
using LaborProjectOOP.Services.CustomerServices;
using LaborProjectOOP.Services.LibrarianServices;
using LaborProjectOOP.Services.OrderHistoryServices;
using LaborProjectOOP.Services.WishListServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LaborProjectOOP.Dekstop.Views.Pages
{
	/// <summary>
	/// Логика взаимодействия для EditCustomerView.xaml
	/// </summary>
	public partial class EditCustomerView : UserControl
	{
		// Лишні прибрати
		private readonly IBookService _bookService;
		private readonly ICatalogService _catalogService;
		private readonly ICustomerService _customerService;
		private readonly ILibrarianService _librarianService;
		private readonly IAuthorService _authorService;
		private readonly IWishListService _wishListService;
		private readonly ICartListService _cartListService;
		private readonly IOrderService _orderService;
		private readonly LibrarianEntity _adminEntity;
		private static List<BookGenreTypes> selectedBooksGenres;
		public EditCustomerView(IBookService bookService, ICatalogService catalogService, ICustomerService customerService, ILibrarianService librarianService, IOrderService orderService, IAuthorService authorService, IWishListService wishListService, ICartListService cartListService)
		{
			// Лишні прибрати
			_bookService = bookService;
			_catalogService = catalogService;
			_customerService = customerService;
			_librarianService = librarianService;
			_authorService = authorService;
			_wishListService = wishListService;
			_cartListService = cartListService;
			_orderService = orderService;
			selectedBooksGenres = new List<BookGenreTypes>();
			InitializeComponent();
			//customerListDataGrid.Items.Clear();
			//foreach (CustomerEntity customer in _customerService.GetAll())
			//	customerListDataGrid.Items.Add(customer);
			RefreshData(customerListDataGrid, _customerService.GetAll());
			sortingCustomersComboBox.ItemsSource = Enum.GetValues(typeof(CustomersSorting)).Cast<CustomersSorting>();
			sortingCustomersComboBox.SelectedIndex = 0;
		}
		public void RefreshData<T>(DataGrid dataGrid, List<T> items)
		{
			dataGrid.Items.Clear();
			foreach (T entity in items)
				dataGrid.Items.Add(entity);
		}
		public void RefreshData<T>(ComboBox comboBox, List<T> items)
		{
			comboBox.ItemsSource = items;
		}
		private void DeleteCustomer_Click(object sender, RoutedEventArgs e)
		{

			CustomerEntity selectedCustomer = customerListDataGrid.SelectedItem as CustomerEntity;
			if (selectedCustomer == null)
			{
				MessageBox.Show("Select Customer");
			}
			else
				_customerService.Delete(selectedCustomer.Id);
			MessageBox.Show("Succesfully deleted");

		}
		private void AddToBlackList_Click(object sender, RoutedEventArgs e)
		{
			CustomerEntity selectedCustomer = customerListDataGrid.SelectedItem as CustomerEntity;
			if (selectedCustomer == null)
			{
				MessageBox.Show("Select Customer");
			}
			else
			{
				if (selectedCustomer.IsBanned)
				{
					selectedCustomer.IsBanned = false;
				}
				else
					selectedCustomer.IsBanned = true;
				_customerService.Update(selectedCustomer);
				MessageBox.Show("Succesfully added");
			}
		}
		private void RefreshCustomerDataGrid_Click(object sender, RoutedEventArgs e)
		{
			RefreshData(customerListDataGrid, _customerService.GetAll());

		}
		private void sortingCustomersComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			CustomersSorting selectedSort = (CustomersSorting)sortingCustomersComboBox.SelectedItem;
			List<CustomerEntity> customers = _customerService.GetAll();
			switch (selectedSort)
			{
				case CustomersSorting.None:
					customerListDataGrid.Items.Clear();
					foreach (CustomerEntity customer in customers)
						customerListDataGrid.Items.Add(customer);
					break;
				case CustomersSorting.BannedCustomers:
					customerListDataGrid.Items.Clear();
					foreach (CustomerEntity customer in customers.Where(customer => customer.IsBanned == true))
						customerListDataGrid.Items.Add(customer);
					break;
				default:
					break;
			}
		}
		private void CustomerListDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
		{
			DeleteCustomerBtn.IsEnabled = true;
			AddToBlackListCustomerBtn.IsEnabled = true;
		}



	}
}

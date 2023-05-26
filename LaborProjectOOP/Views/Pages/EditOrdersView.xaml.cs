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
	/// Логика взаимодействия для EditOrdersView.xaml
	/// </summary>
	public partial class EditOrdersView : UserControl
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
		public EditOrdersView(IBookService bookService, ICatalogService catalogService, ICustomerService customerService, ILibrarianService librarianService, IOrderService orderService, IAuthorService authorService, IWishListService wishListService, ICartListService cartListService)
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
			//foreach (OrderEntity order in _orderService.GetAll())
			//ordersListDataGrid.Items.Add(order);
			RefreshData(ordersListDataGrid, _orderService.GetAll());

			sortingOrdersComboBox.ItemsSource = Enum.GetValues(typeof(OrdersSorting)).Cast<OrdersSorting>();
			sortingOrdersComboBox.SelectedIndex = 0;

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
		private void sortingOrdersComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			OrdersSorting selectedSort = (OrdersSorting)sortingOrdersComboBox.SelectedItem;
			List<OrderEntity> orders = _orderService.GetAll();
			switch (selectedSort)
			{
				case OrdersSorting.None:
					ordersListDataGrid.Items.Clear();
					foreach (OrderEntity order in orders)
						ordersListDataGrid.Items.Add(order);
					break;
				case OrdersSorting.ActiveOnly:
					ordersListDataGrid.Items.Clear();
					foreach (OrderEntity order in orders.Where(order => order.IsActual == true))
						ordersListDataGrid.Items.Add(order);
					break;
				default: break;
			}
		}
		private void MakeNotActiveSelectedOrderBtn_Click(object sender, RoutedEventArgs e)
		{
			OrderEntity selectedOrder = ordersListDataGrid.SelectedItem as OrderEntity;
			if (selectedOrder != null)
			{
				selectedOrder.IsActual = false;
				_orderService.Update(selectedOrder);
				MessageBox.Show("Succesfully make not active");
			}
			else
			{
				MessageBox.Show("Select some order");
			}
		}
		private void RefreshOrdersDataGrid_Click(object sender, RoutedEventArgs e)
		{
			RefreshData(ordersListDataGrid, _orderService.GetAll());
		}
		private void DeleteSelectedOrderBtn_Click(object sender, RoutedEventArgs e)
		{
			OrderEntity selectedOrder = ordersListDataGrid.SelectedItem as OrderEntity;
			if (selectedOrder != null)
			{
				_orderService.Delete(selectedOrder.Id);
				MessageBox.Show("Succesfully delete");
			}
			else
			{
				MessageBox.Show("Select some order");
			}
		}
		

	}
}

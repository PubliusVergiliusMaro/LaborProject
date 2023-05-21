using LaborProjectOOP.Database.Models;
using LaborProjectOOP.Services.AuthorServices;
using LaborProjectOOP.Services.BookServices;
using LaborProjectOOP.Services.CatalogServices;
using LaborProjectOOP.Services.CustomerServices;
using LaborProjectOOP.Services.LibrarianServices;
using LaborProjectOOP.Services.OrderServices;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace LaborProjectOOP.Dekstop.Views.Pages
{
	/// <summary>
	/// Логика взаимодействия для OrderPage.xaml
	/// </summary>
	public partial class OrderPage : Page
	{
		// Лишні прибрати
		private readonly IBookService _bookService;
		private readonly ICatalogService _catalogService;
		private readonly ICustomerService _customerService;
		private readonly ILibrarianService _librarianService;
		private readonly IOrderService _orderService;
		private readonly IAuthorService _authorService;
		private readonly CustomerEntity _currentCustomer;
		private readonly List<BookEntity> _customerCart;
		public OrderPage(IBookService bookService, ICatalogService catalogService, ICustomerService customerService, ILibrarianService librarianService, IOrderService orderService, IAuthorService authorService, CustomerEntity currentCustomer, List<BookEntity> customerCart)
		{
			_currentCustomer = currentCustomer;
			_customerCart = customerCart;
			// Лишні прибрати
			_bookService = bookService;
			_catalogService = catalogService;
			_customerService = customerService;
			_librarianService = librarianService;
			_orderService = orderService;
			_authorService = authorService;
			InitializeComponent();

			loginLabel.Content = _currentCustomer.Login;
			emailLabel.Content = _currentCustomer.Email;
			phoneLabel.Content = _currentCustomer.Phone;

			DateTime startDate = DateTime.Now;
			DateTime endDate = DateTime.Now.AddDays(10);

			startDataTextBox.Text = startDate.ToString();
			endDataTextBox.Text = endDate.ToString();
			foreach (BookEntity book in _customerCart)
			{
				booksListDataGrid.Items.Add(book);
			}
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			newPageGrid.Visibility = Visibility.Visible;
			orderPageGrid.Visibility = Visibility.Hidden;
			pagesFrame.Navigate(new CustomerMainPage(_bookService, _catalogService, _customerService, _librarianService, _orderService, _authorService, _currentCustomer, false, _customerCart));
		}

		private void MakeOrderBtn_Click(object sender, RoutedEventArgs e)
		{
			//OrderEntity order = new OrderEntity()
			//{
			//	Books = new List<BookEntity> { _book },
			//	CreatedOn = DateTime.Now,
			//	Customer = _currentAdmin,
			//	IsActual = true
			//};

			//List<OrderEntity> orders = _orderService.GetAll();
			//if (orders == null)
			//{
			//	orders = new List<OrderEntity>();
			//}
			//orders.Add(order);
			//_orderService.SaveOrders(orders);//Переробити на бд

			_bookService.PurchaseBooks(1, _customerCart);
			MessageBox.Show("Succesfuly created");


			newPageGrid.Visibility = Visibility.Visible;
			orderPageGrid.Visibility = Visibility.Hidden;
			pagesFrame.Navigate(new CustomerMainPage(_bookService, _catalogService, _customerService, _librarianService, _orderService, _authorService, _currentCustomer,false, _customerCart));
		}

		private void editCartBtn_Click(object sender, RoutedEventArgs e)
		{
			newPageGrid.Visibility = Visibility.Visible;
			orderPageGrid.Visibility = Visibility.Hidden;
			pagesFrame.Navigate(new CustomerCartPage(_bookService, _catalogService, _customerService, _librarianService, _orderService, _authorService, _currentCustomer,false, _customerCart));
		}
	}
}

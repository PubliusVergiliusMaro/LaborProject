using LaborProjectOOP.Database.Models;
using LaborProjectOOP.Services.AuthorServices;
using LaborProjectOOP.Services.BookServices;
using LaborProjectOOP.Services.CatalogServices;
using LaborProjectOOP.Services.CustomerServices;
using LaborProjectOOP.Services.LibrarianServices;
using LaborProjectOOP.Services.OrderServices;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LaborProjectOOP.Dekstop.Views.Pages
{
	/// <summary>
	/// Логика взаимодействия для CustomerOrderPage.xaml
	/// </summary>
	public partial class CustomerOrderPage : Page
	{
		private readonly CustomerEntity _currentCustomer;
		// Лишні прибрати
		private readonly IBookService _bookService;
		private readonly ICatalogService _catalogService;
		private readonly ICustomerService _customerService;
		private readonly ILibrarianService _librarianService;
		private readonly IOrderService _orderService;
		private readonly IAuthorService _authorService;
		private readonly List<BookEntity> _customerBuyList;
		public CustomerOrderPage(IBookService bookService, ICatalogService catalogService, ICustomerService customerService, ILibrarianService librarianService, IOrderService orderService, IAuthorService authorService, CustomerEntity customer, List<BookEntity> customerBuyList)
		{
			// Лишні прибрати
			_bookService = bookService;
			_catalogService = catalogService;
			_customerService = customerService;
			_librarianService = librarianService;
			_orderService = orderService;
			_currentCustomer = customer;
			_authorService = authorService;
			_customerBuyList = customerBuyList;
			InitializeComponent();

			OrderEntity order = _orderService.GetAll().Where(ord => ord.Customer.Login == _currentCustomer.Login).FirstOrDefault();

			foreach (BookEntity book in order.Books)
				booksListDataGrid.Items.Add(book);
		}

		private void CancelBtn_Click(object sender, RoutedEventArgs e)
		{
			newPageGrid.Visibility = Visibility.Visible;
			customerOrdersPageGrid.Visibility = Visibility.Hidden;
			pagesFrame.Navigate(new CustomerMainPage(_bookService, _catalogService, _customerService, _librarianService, _orderService, _authorService, _currentCustomer, _customerBuyList));
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{

		}
	}
}

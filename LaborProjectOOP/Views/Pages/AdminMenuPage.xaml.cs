using LaborProjectOOP.Database.Models;
using LaborProjectOOP.Services.AuthorServices;
using LaborProjectOOP.Services.BookServices;
using LaborProjectOOP.Services.CatalogServices;
using LaborProjectOOP.Services.CustomerServices;
using LaborProjectOOP.Services.LibrarianServices;
using LaborProjectOOP.Services.OrderServices;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace LaborProjectOOP.Dekstop.Views.Pages
{
	/// <summary>
	/// Логика взаимодействия для AdminMenuPage.xaml
	/// </summary>
	public partial class AdminMenuPage : Page
	{
		private readonly IBookService _bookService;
		private readonly ICatalogService _catalogService;
		private readonly ICustomerService _customerService;
		private readonly ILibrarianService _librarianService;
		private readonly IOrderService _orderService;
		private readonly IAuthorService _authorService;

		private readonly LibrarianEntity _currentAdmin;
		public AdminMenuPage(
			IBookService bookService, ICatalogService catalogService, ICustomerService customerService, ILibrarianService librarianService, IOrderService orderService, IAuthorService authorService,
			LibrarianEntity currentAdmin)
		{
			_bookService = bookService;
			_catalogService = catalogService;
			_customerService = customerService;
			_librarianService = librarianService;
			_orderService = orderService;
			_authorService = authorService;
			_currentAdmin = currentAdmin;
			InitializeComponent();
		}

		private void enterAsAdminBtn_Click(object sender, RoutedEventArgs e)
		{
			menuPageGrid.Visibility = Visibility.Hidden;
			newPageGrid.Visibility = Visibility.Visible;
			pagesFrame.Navigate(new AdminPage(_bookService, _catalogService, _customerService, _librarianService, _orderService, _authorService, _currentAdmin));

		}

		private void enterAsCustomerBtn_Click(object sender, RoutedEventArgs e)
		{
			menuPageGrid.Visibility = Visibility.Hidden;
			newPageGrid.Visibility = Visibility.Visible;
			CustomerEntity customer = new CustomerEntity
			{
				Login = _currentAdmin.Login,
				Password = _currentAdmin.Password,
			};
			pagesFrame.Navigate(new CustomerMainPage(_bookService, _catalogService, _customerService, _librarianService, _orderService, _authorService, customer, new List<BookEntity>()));
		}
	}
}

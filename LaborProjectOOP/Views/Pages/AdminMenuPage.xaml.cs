using LaborProjectOOP.Database.Models;
using LaborProjectOOP.Services.AuthorServices;
using LaborProjectOOP.Services.BookServices;
using LaborProjectOOP.Services.CartListServices;
using LaborProjectOOP.Services.CatalogServices;
using LaborProjectOOP.Services.CustomerServices;
using LaborProjectOOP.Services.LibrarianServices;
using LaborProjectOOP.Services.OrderHistoryServices;
using LaborProjectOOP.Services.WishListServices;
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
		private readonly IAuthorService _authorService;
		private readonly IWishListService _wishListService;
		private readonly ICartListService _cartListService;
		private readonly IOrderService _orderService;
		private readonly LibrarianEntity _currentAdmin;
		public AdminMenuPage(
			IBookService bookService, ICatalogService catalogService, ICustomerService customerService, ILibrarianService librarianService,  IOrderService orderService, IAuthorService authorService, IWishListService wishListService, ICartListService cartListService,
			LibrarianEntity currentAdmin)
		{
			_bookService = bookService;
			_catalogService = catalogService;
			_customerService = customerService;
			_librarianService = librarianService;
			_authorService = authorService;
			_wishListService = wishListService;
			_cartListService = cartListService;
			_currentAdmin = currentAdmin;
			_orderService = orderService;
			InitializeComponent();
		}

		private void enterAsAdminBtn_Click(object sender, RoutedEventArgs e)
		{
			menuPageGrid.Visibility = Visibility.Hidden;
			newPageGrid.Visibility = Visibility.Visible;
			pagesFrame.Navigate(new AdminPage(_bookService, _catalogService, _customerService, _librarianService, _orderService, _authorService,_wishListService,_cartListService, _currentAdmin));

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
			pagesFrame.Navigate(new CustomerMainPage(_bookService, _catalogService, _customerService, _librarianService, _orderService, _authorService, _wishListService,_cartListService,customer,true));
		}

		private void BackToLoginPage_Click(object sender, RoutedEventArgs e)
		{
			menuPageGrid.Visibility = Visibility.Hidden;
			newPageGrid.Visibility = Visibility.Visible;
			pagesFrame.Navigate(new LoginPage(_bookService, _catalogService, _customerService, _librarianService, _orderService, _authorService,_wishListService,_cartListService));
		}
    }
}

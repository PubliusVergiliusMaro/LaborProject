using LaborProjectOOP.Database.Models;
using LaborProjectOOP.Services.AuthorServices;
using LaborProjectOOP.Services.BookServices;
using LaborProjectOOP.Services.CartListServices;
using LaborProjectOOP.Services.CatalogServices;
using LaborProjectOOP.Services.CustomerServices;
using LaborProjectOOP.Services.LibrarianServices;
using LaborProjectOOP.Services.OrderServices;
using LaborProjectOOP.Services.WishListServices;
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
		// Лишні прибрати
		private readonly IBookService _bookService;
		private readonly ICatalogService _catalogService;
		private readonly ICustomerService _customerService;
		private readonly ILibrarianService _librarianService;
		private readonly IOrderService _orderService;
		private readonly IAuthorService _authorService;
		private readonly IWishListService _wishListService;
		private readonly ICartListService _cartListService;
		private readonly CustomerEntity _currentCustomer;
		public CustomerOrderPage(IBookService bookService, ICatalogService catalogService, ICustomerService customerService, ILibrarianService librarianService, IOrderService orderService, IAuthorService authorService, IWishListService wishListService, ICartListService cartListService, CustomerEntity customer)
		{
			// Лишні прибрати
			_bookService = bookService;
			_catalogService = catalogService;
			_customerService = customerService;
			_librarianService = librarianService;
			_orderService = orderService;
			_currentCustomer = customer;
			_authorService = authorService;
			_wishListService= wishListService;
			_cartListService= cartListService;
			InitializeComponent();

			OrderEntity order = _orderService.GetAll().Where(ord => ord.Customer.Login == _currentCustomer.Login).FirstOrDefault();

			foreach (BookEntity book in order.Books)
				booksListDataGrid.Items.Add(book);
		}

		private void CancelBtn_Click(object sender, RoutedEventArgs e)
		{
			newPageGrid.Visibility = Visibility.Visible;
			customerOrdersPageGrid.Visibility = Visibility.Hidden;
			pagesFrame.Navigate(new CustomerMainPage(_bookService, _catalogService, _customerService, _librarianService, _orderService, _authorService,_wishListService, _cartListService, _currentCustomer,false));
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{

		}
	}
}

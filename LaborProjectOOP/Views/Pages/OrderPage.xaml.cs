using LaborProjectOOP.Constants.Enums;
using LaborProjectOOP.Database.Models;
using LaborProjectOOP.Services.AuthorServices;
using LaborProjectOOP.Services.BookServices;
using LaborProjectOOP.Services.CartListServices;
using LaborProjectOOP.Services.CatalogServices;
using LaborProjectOOP.Services.CustomerServices;
using LaborProjectOOP.Services.LibrarianServices;
using LaborProjectOOP.Services.OrderHistoryServices;
using LaborProjectOOP.Services.OrderServices;
using LaborProjectOOP.Services.WishListServices;
using System;
using System.Collections.Generic;
using System.Linq;
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
		private readonly IOrderListService _orderListService;
		private readonly IAuthorService _authorService;
		private readonly IWishListService _wishListService;
		private readonly ICartListService _cartListService;
		private readonly IOrderService _orderService;
		private readonly CustomerEntity _currentCustomer;
		private readonly List<BookEntity> _booksInCart;
		public OrderPage(IBookService bookService, ICatalogService catalogService, ICustomerService customerService, ILibrarianService librarianService, IOrderListService orderListService, IOrderService orderService, IAuthorService authorService, IWishListService wishListService, ICartListService cartListService, CustomerEntity currentCustomer)
		{
			_currentCustomer = currentCustomer;
			
			// Лишні прибрати
			_bookService = bookService;
			_catalogService = catalogService;
			_customerService = customerService;
			_librarianService = librarianService;
			_orderListService = orderListService;
			_authorService = authorService;
			_wishListService = wishListService;
			_cartListService = cartListService;
			_orderService = orderService;
			InitializeComponent();

			loginLabel.Content = _currentCustomer.Login;
			emailLabel.Content = _currentCustomer.Email;
			phoneLabel.Content = _currentCustomer.Phone;

			DateTime startDate = DateTime.Now;
			DateTime endDate = DateTime.Now.AddDays(10);

			//startDataTextBox.Text = startDate.ToString();
			//endDataTextBox.Text = endDate.ToString();
			List<BookEntity> books = new List<BookEntity>();
			List<CartListEntity> customerCartLists = _cartListService.GetCartListByCustomerId(_currentCustomer.Id);
			foreach (CartListEntity cartBook in customerCartLists)
				books.Add(cartBook.Book);
			_booksInCart = books;
			foreach (BookEntity book in _booksInCart)
			{
				booksListDataGrid.Items.Add(book);
			}
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			newPageGrid.Visibility = Visibility.Visible;
			orderPageGrid.Visibility = Visibility.Hidden;
			pagesFrame.Navigate(new CustomerMainPage(_bookService, _catalogService, _customerService, _librarianService, _orderListService, _orderService, _authorService,_wishListService, _cartListService, _currentCustomer, false));
		}

		private void MakeOrderBtn_Click(object sender, RoutedEventArgs e)
		{

			_bookService.PurchaseBooks(_currentCustomer.Id, _booksInCart);
			MessageBox.Show("Succesfuly created");
		    foreach(CartListEntity customerCart in _currentCustomer.CartList.ToList())
			{
				_cartListService.Delete(customerCart.Id);
			}
			_customerService.Update(_currentCustomer);
			newPageGrid.Visibility = Visibility.Visible;
			orderPageGrid.Visibility = Visibility.Hidden;
			pagesFrame.Navigate(new CustomerMainPage(_bookService, _catalogService, _customerService, _librarianService, _orderListService, _orderService, _authorService,_wishListService, _cartListService, _currentCustomer,false));
		
		}

		private void editCartBtn_Click(object sender, RoutedEventArgs e)
		{
			newPageGrid.Visibility = Visibility.Visible;
			orderPageGrid.Visibility = Visibility.Hidden;
			pagesFrame.Navigate(new CustomerActivitiesPage(_bookService, _catalogService, _customerService, _librarianService, _orderListService, _orderService, _authorService,_wishListService, _cartListService,  _currentCustomer,false,CustomerActivitiesInfoType.CartList));
		}
	}
}

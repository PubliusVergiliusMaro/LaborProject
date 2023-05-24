using LaborProjectOOP.Database.Models;
using LaborProjectOOP.Services.AuthorServices;
using LaborProjectOOP.Services.BookServices;
using LaborProjectOOP.Services.CartListServices;
using LaborProjectOOP.Services.CatalogServices;
using LaborProjectOOP.Services.CustomerServices;
using LaborProjectOOP.Services.Helpers;
using LaborProjectOOP.Services.LibrarianServices;
using LaborProjectOOP.Services.OrderHistoryServices;
using LaborProjectOOP.Services.WishListServices;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace LaborProjectOOP.Dekstop.Views.Pages
{
	/// <summary>
	/// Логика взаимодействия для EditProfilePage.xaml
	/// </summary>
	public partial class EditProfilePage : Page
	{
		private readonly CustomerEntity _currentCustomer;
		// Лишні прибрати
		private readonly IBookService _bookService;
		private readonly ICatalogService _catalogService;
		private readonly ICustomerService _customerService;
		private readonly ILibrarianService _librarianService;
		private readonly IAuthorService _authorService;
		private readonly IWishListService _wishListService;
		private readonly ICartListService _cartListService;
		private readonly IOrderService _orderService;
		public EditProfilePage(IBookService bookService, ICatalogService catalogService, ICustomerService customerService, ILibrarianService librarianService,  IOrderService orderService, IAuthorService authorService, IWishListService wishListService,ICartListService cartListService, CustomerEntity currentCustomer)
		{

			_currentCustomer = currentCustomer;
			
			// Лишні прибрати
			_bookService = bookService;
			_catalogService = catalogService;
			_customerService = customerService;
			_librarianService = librarianService;
			_authorService = authorService;
			_wishListService = wishListService;
			_cartListService = cartListService;
			_orderService = orderService;
			InitializeComponent();

			loginTextBox.Text = _currentCustomer.Login;
			phoneTextBox.Text = _currentCustomer.Phone;
			emailTextBox.Text = _currentCustomer.Email;
		}

		private void CancelBtn_Click(object sender, RoutedEventArgs e)
		{
			newPageGrid.Visibility = Visibility.Visible;
			editProfilePage.Visibility = Visibility.Hidden;
			pagesFrame.Navigate(new CustomerMainPage(_bookService, _catalogService, _customerService, _librarianService, _orderService, _authorService,_wishListService, _cartListService, _currentCustomer,false));
		}

		private void SaveBtn_Click(object sender, RoutedEventArgs e)
		{
			_currentCustomer.Login = loginTextBox.Text;
			_currentCustomer.Phone = phoneTextBox.Text;
			_currentCustomer.Email = emailTextBox.Text;
			if (_currentCustomer.Password == HashService.GetMD5Hash(oldPasswordTextBox.Text))
				_currentCustomer.Password = HashService.GetMD5Hash(newPasswordTextBox.Text);
			_customerService.Update(_currentCustomer);
			MessageBox.Show("Succesfully updated");
		}
	}
}

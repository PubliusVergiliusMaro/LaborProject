using LaborProjectOOP.Database.Models;
using LaborProjectOOP.Services.AuthorServices;
using LaborProjectOOP.Services.BookServices;
using LaborProjectOOP.Services.CatalogServices;
using LaborProjectOOP.Services.CustomerServices;
using LaborProjectOOP.Services.Helpers;
using LaborProjectOOP.Services.LibrarianServices;
using LaborProjectOOP.Services.OrderServices;
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
		private readonly IOrderService _orderService;
		private readonly IAuthorService _authorService;
		private readonly List<BookEntity> _customerBuyList;
		public EditProfilePage(IBookService bookService, ICatalogService catalogService, ICustomerService customerService, ILibrarianService librarianService, IOrderService orderService, IAuthorService authorService, CustomerEntity currentCustomer, List<BookEntity> customerBuyList)
		{

			_currentCustomer = currentCustomer;
			_customerBuyList = customerBuyList;
			// Лишні прибрати
			_bookService = bookService;
			_catalogService = catalogService;
			_customerService = customerService;
			_librarianService = librarianService;
			_orderService = orderService;
			_authorService = authorService;
			InitializeComponent();

			loginTextBox.Text = _currentCustomer.Login;
			phoneTextBox.Text = _currentCustomer.Phone;
			emailTextBox.Text = _currentCustomer.Email;
		}

		private void CancelBtn_Click(object sender, RoutedEventArgs e)
		{
			newPageGrid.Visibility = Visibility.Visible;
			editProfilePage.Visibility = Visibility.Hidden;
			pagesFrame.Navigate(new CustomerMainPage(_bookService, _catalogService, _customerService, _librarianService, _orderService, _authorService, _currentCustomer,false, _customerBuyList));
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

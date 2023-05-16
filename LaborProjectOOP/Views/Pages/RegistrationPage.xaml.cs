using LaborProjectOOP.Database.Models;
using LaborProjectOOP.Services.AuthorServices;
using LaborProjectOOP.Services.BookServices;
using LaborProjectOOP.Services.CatalogServices;
using LaborProjectOOP.Services.CustomerServices;
using LaborProjectOOP.Services.LibrarianServices;
using LaborProjectOOP.Services.OrderServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LaborProjectOOP.Dekstop.Views.Pages
{
	/// <summary>
	/// Логика взаимодействия для RegistrationPage.xaml
	/// </summary>
	public partial class RegistrationPage : Page
	{
		// Лишні прибрати
		private readonly IBookService _bookService;
		private readonly ICatalogService _catalogService;
		private readonly ICustomerService _customerService;
		private readonly ILibrarianService _librarianService;
		private readonly IOrderService _orderService;
		private readonly IAuthorService _authorService;

		public RegistrationPage(IBookService bookService, ICatalogService catalogService, ICustomerService customerService, ILibrarianService librarianService, IOrderService orderService, IAuthorService authorService)
		{
			// Лишні прибрати
			_bookService = bookService;
			_catalogService = catalogService;
			_customerService = customerService;
			_librarianService = librarianService;
			_orderService = orderService;
			_authorService = authorService;
			InitializeComponent();
		}

		private void SaveBtn_Click(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(loginTextBox.Text) || string.IsNullOrEmpty(passwordTextBox.Text) || string.IsNullOrEmpty(emailTextBox.Text) || string.IsNullOrEmpty(phoneTextBox.Text))
			{
				MessageBox.Show("Fill all lines");
			}
			else
			{
				_customerService.Create(new CustomerEntity
				{
					Login = loginTextBox.Text,
					Password = passwordTextBox.Text,
					Email = emailTextBox.Text,
					Phone = phoneTextBox.Text,
				});

				//List<CustomerEntity> customers = _customerService.GetAll();
				//customers.Add(customer);
				//_customerService.SaveCustomers(customers);//Переробити на бд
				MessageBox.Show("Succesfuly created");
				registrationPageGrid.Visibility = Visibility.Hidden;
				newPageGrid.Visibility = Visibility.Visible;
				pagesFrame.Navigate(new LoginPage(_bookService, _catalogService, _customerService, _librarianService, _orderService, _authorService));
			}

		}

		private void CancelBtn_Click(object sender, RoutedEventArgs e)
		{
			registrationPageGrid.Visibility = Visibility.Hidden;
			newPageGrid.Visibility = Visibility.Visible;
			pagesFrame.Navigate(new LoginPage(_bookService, _catalogService, _customerService, _librarianService, _orderService, _authorService));
		}
	}
}

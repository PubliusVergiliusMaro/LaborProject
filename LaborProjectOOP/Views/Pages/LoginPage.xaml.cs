using LaborProjectOOP.Database.Models;
using LaborProjectOOP.Services.AuthorServices;
using LaborProjectOOP.Services.BookServices;
using LaborProjectOOP.Services.CartListServices;
using LaborProjectOOP.Services.CatalogServices;
using LaborProjectOOP.Services.CustomerServices;
using LaborProjectOOP.Services.Helpers;
using LaborProjectOOP.Services.LibrarianServices;
using LaborProjectOOP.Services.OrderHistoryServices;
using LaborProjectOOP.Services.OrderServices;
using LaborProjectOOP.Services.WishListServices;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace LaborProjectOOP.Dekstop.Views.Pages
{
	/// <summary>
	/// Логика взаимодействия для LoginPage.xaml
	/// </summary>
	//delegate void SomeDelegate();// Lab3
	public partial class LoginPage : Page
	{
		private readonly IBookService _bookService;
		private readonly ICatalogService _catalogService;
		private readonly ICustomerService _customerService;
		private readonly ILibrarianService _librarianService;
		private readonly IOrderListService _orderListService;
		private readonly IAuthorService _authorService;
		private readonly IWishListService _wishListService;
		private readonly ICartListService _cartListService;
		private readonly IOrderService _orderService;
		private static CustomerEntity _currentCustomer;
		private static LibrarianEntity _currentAdmin;
		public LoginPage(IBookService bookService, ICatalogService catalogService, ICustomerService customerService, ILibrarianService librarianService, IOrderListService orderListService, IOrderService orderService, IAuthorService authorService, IWishListService wishListService, ICartListService cartListService)
		{
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

			// Create Book
			//	_bookService.Create(
			//		new BookEntity()
			//		{
			//			Title = "Aeneid",
			//	Description = " Is a Latin epic poem, written by Virgil between 29 and 19 BC, that tells the legendary story of Aeneas, a Trojan who fled the fall of Troy and travelled to Italy, where he became the ancestor of the Romans.",
			//	Price = 300,
			//	IsTaken =false,
			//});

			// Create Catalog
			//_catalogService.Create(
			//	new CatalogEntity()
			//	{
			//		Name = "Golden Collection",
			//	}	
			//	);

			// Create Customer
			//_customerService.Create(
			//	new CustomerEntity()
			//	{
			//		Login = "Vergil",
			//		Email = "olegredko45@gmail.com",
			//		Password = "1",
			//		Phone = "313214214",
			//		IsBanned = false
			//	});

			// Create Librarian
			//_librarianService.Create(
			//	new LibrarianEntity()
			//	{
			//		Login = "admin",
			//		Password = "1",
			//		Salary = 2000,
			//		IsAdmin = true,
			//		WorkExperience = 2
			//	}
			//	);

			// Create Order
			//_orderListService.Create(
			//	new OrderListEntity()
			//	{
			//		CreatedOn= DateTime.UtcNow,
			//		IsActual = true,

			//	});

			//Create Author
			//_authorService.Create(
			//	new AuthorEntity()
			//	{
			//		Name = "Markus ",
			//		Surname = "Avrelius",
			//	});
		}

		private void signUpBtn_Click(object sender, RoutedEventArgs e)
		{
			loginGrid.Visibility = Visibility.Hidden;
			newPageGrid.Visibility = Visibility.Visible;
			pagesFrame.Navigate(new RegistrationPage(_bookService, _catalogService, _customerService, _librarianService, _orderListService, _orderService, _authorService, _wishListService, _cartListService));

		}

		private void logInBtn_Click(object sender, RoutedEventArgs e)
		{

			bool isBaned = false;
			bool canLogin = false;
			bool isAdmin = false;
			//CustomerEntity customer = new CustomerEntity()
			//{
			//	Login = "Oleg",
			//	Email = "olegredko45@gmail.com",
			//	Password = "1",
			//	Phone = "3809005185"
			//};
			//LibrarianEntity librarian = new LibrarianEntity()
			//{
			//	Login = "Vergil",
			//	Password = "1",
			//	Salary = 300,
			//	WorkExperience = 4,
			//	IsAdmin = true,
			//};
			//List<CustomerEntity> customers = new List<CustomerEntity> { customer };
			//List<LibrarianEntity> librarians = new List<LibrarianEntity> { librarian };
			//_customerService.SaveCustomers(customers);
			//LibrarianService.SaveLibrarians(librarians);

			//List<CustomerEntity> customers = _customerService.GetCustomers();
			//List<LibrarianEntity> librarians = LibrarianService.GetLibrarians();

			List<CustomerEntity> customers = _customerService.GetAll();
			List<LibrarianEntity> librarians = _librarianService.GetAll();


			foreach (LibrarianEntity libr in librarians)
			{
				if (libr.Login == loginTextBox.Text && libr.Password == HashService.GetMD5Hash(passwordTextBox.Password))
				{
					if (libr.IsAdmin == true)
					{
						isAdmin = true;
						_currentAdmin = libr;
						break;
					}
					else
					{
						canLogin = true;
						_currentCustomer = new CustomerEntity
						{
							Login = libr.Login,
							Password = libr.Password,
						};
						break;
					}
				}
			}
			foreach (CustomerEntity cust in customers)
			{
				if (cust.IsBanned)
				{
					isBaned = true;
				}
				if (cust.Login == loginTextBox.Text && cust.Password == HashService.GetMD5Hash(passwordTextBox.Password))
				{
					canLogin = true;
					_currentCustomer = cust;
				}
			}

			if (isAdmin)
			{
				loginGrid.Visibility = Visibility.Hidden;
				newPageGrid.Visibility = Visibility.Visible;
				pagesFrame.Navigate(new AdminMenuPage(_bookService, _catalogService, _customerService, _librarianService, _orderListService, _orderService, _authorService,_wishListService, _cartListService, _currentAdmin));
			}
			else if (canLogin)
			{
				loginGrid.Visibility = Visibility.Hidden;
				newPageGrid.Visibility = Visibility.Visible;
				pagesFrame.Navigate(new CustomerMainPage(_bookService, _catalogService, _customerService, _librarianService, _orderListService, _orderService, _authorService,_wishListService, _cartListService, _currentCustomer,false));
			}
			else
			{
				MessageBox.Show("User do not recognized");
			}
		}
		// Lab3
		//private void Method()
		//{
		//	SomeDelegate someDelegate = () =>
		//	{
		//		int a=0, b=0,c;
		//		c = a + b;
		//	};
		//}
	}
}

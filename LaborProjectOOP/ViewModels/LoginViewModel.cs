using LaborProjectOOP.Database.Models;
using LaborProjectOOP.Dekstop.Commands;
using LaborProjectOOP.Dekstop.NavigationServices.Stores;
using LaborProjectOOP.Services.AuthorServices;
using LaborProjectOOP.Services.BookServices;
using LaborProjectOOP.Services.CartListServices;
using LaborProjectOOP.Services.CatalogServices;
using LaborProjectOOP.Services.CustomerServices;
using LaborProjectOOP.Services.Helpers;
using LaborProjectOOP.Services.LibrarianServices;
using LaborProjectOOP.Services.OrderHistoryServices;
using LaborProjectOOP.Services.WishListServices;
using System;
using System.Collections.Generic;
using System.Security;
using System.Windows;
using System.Windows.Input;

namespace LaborProjectOOP.Dekstop.ViewModels
{
	public class LoginViewModel : ViewModelBase
	{
		private readonly IBookService _bookService;
		private readonly ICatalogService _catalogService;
		private readonly ICustomerService _customerService;
		private readonly ILibrarianService _librarianService;
		private readonly IAuthorService _authorService;
		private readonly IWishListService _wishListService;
		private readonly ICartListService _cartListService;
		private readonly IOrderService _orderService;
		private readonly NavigationStore _navigationStore;
		public LoginViewModel(NavigationStore navigationStore,ICustomerService customerService, 
			ILibrarianService librarianService,IWishListService wishListService, ICartListService cartListService,
			ICatalogService catalogService,IBookService bookService,IAuthorService authorService, IOrderService orderService) 
		{
			_customerService = customerService;
			_librarianService = librarianService;
			_navigationStore = navigationStore;
			_cartListService = cartListService;
			_wishListService = wishListService;
			_catalogService = catalogService;
			_bookService = bookService;
			_authorService = authorService;
			_orderService = orderService;
			LoginCommand = new DelegateCommand(Login, CanLogin);
			SignUpCommand = new DelegateCommand(SignUp);
		}

		private void SignUp()
		{
			_navigationStore.CurrentViewModel = new SignUpViewModel(_navigationStore, _customerService, _librarianService, _wishListService, _cartListService, _catalogService, _bookService, _authorService, _orderService);
		}

		private async void Login()
		{
			bool isBaned = false;
			bool canLogin = false;
			bool isAdmin = false;

			List<CustomerEntity> customers =  _customerService.GetAll();
			List<LibrarianEntity> librarians =  _librarianService.GetAll();
			CustomerEntity _currentCustomer = new CustomerEntity();
			LibrarianEntity _currentAdmin;

			foreach (LibrarianEntity libr in librarians)
			{
				if (libr.Login == UserLogin && libr.Password == HashService.GetMD5Hash(Password))
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
				if (cust.Login == UserLogin && cust.Password == HashService.GetMD5Hash(Password))
				{
					canLogin = true;
					_currentCustomer = cust;
				}
			}

			if (isAdmin)
			{
				_navigationStore.CurrentViewModel = new AdminMenuViewModel(_navigationStore,_currentCustomer,_catalogService,_bookService,_customerService,_librarianService,_cartListService,_wishListService,_authorService,_orderService);
				//loginGrid.Visibility = Visibility.Hidden;
				//newPageGrid.Visibility = Visibility.Visible;
				//pagesFrame.Navigate(new AdminMenuPage(_bookService, _orderService, _orderService, _orderService, _orderService, _authorService, _wishListService, _cartListService, _currentAdmin));
			}
			else if (canLogin)
			{
				
				_navigationStore.CurrentViewModel = new CustomerMainViewModel(_navigationStore, _currentCustomer,false, _catalogService, _bookService, _customerService,_librarianService, _cartListService, _wishListService, _authorService, _orderService);
				//loginGrid.Visibility = Visibility.Hidden;
				//newPageGrid.Visibility = Visibility.Visible;
				//pagesFrame.Navigate(new CustomerMainPage(_bookService, _orderService, _orderService, _orderService, _orderService, _authorService, _wishListService, _cartListService, _currentCustomer, false));
			}
			else
			{
				MessageBox.Show("User do not recognized");
			}
		}
		private bool CanLogin()
		{
			return !string.IsNullOrEmpty(UserLogin)
				&& !string.IsNullOrEmpty(Password);
		}

		public ICommand LoginCommand { get; set; }
		public ICommand SignUpCommand { get; set; }
		private string _userLogin;
		public string UserLogin 
		{ 
			get => _userLogin; 
			set 
			{
				_userLogin = value;
				OnPropertyChanged(nameof(Login));
			} 
		}
		public string _password { get; set; }

		public string Password
		{
			get => _password;
			set
			{
				_password = value;
				OnPropertyChanged(nameof(Password));
			}
		}


	}
}

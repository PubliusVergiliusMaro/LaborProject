using LaborProjectOOP.Database.Models;
using LaborProjectOOP.Dekstop.Commands;
using LaborProjectOOP.Services.CustomerServices;
using LaborProjectOOP.Services.Helpers;
using LaborProjectOOP.Services.LibrarianServices;
using System;
using System.Collections.Generic;
using System.Security;
using System.Windows;
using System.Windows.Input;

namespace LaborProjectOOP.Dekstop.ViewModels
{
	public class LoginViewModel : ViewModelBase
	{
		private readonly ICustomerService _customerService;
		private readonly ILibrarianService _librarianService;
		public LoginViewModel(ICustomerService customerService, ILibrarianService librarianService) 
		{
			_customerService = customerService;
			_librarianService = librarianService;
			LoginCommand = new DelegateCommand(Login, CanLogin);
		}
		private void Login()
		{
			bool isBaned = false;
			bool canLogin = false;
			bool isAdmin = false;

			List<CustomerEntity> customers = _customerService.GetAll();
			List<LibrarianEntity> librarians = _librarianService.GetAll();
			CustomerEntity _currentCustomer;
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
				MessageBox.Show("It is admin");
				//loginGrid.Visibility = Visibility.Hidden;
				//newPageGrid.Visibility = Visibility.Visible;
				//pagesFrame.Navigate(new AdminMenuPage(_bookService, _orderService, _orderService, _orderService, _orderService, _authorService, _wishListService, _cartListService, _currentAdmin));
			}
			else if (canLogin)
			{
				MessageBox.Show("It is customer");
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

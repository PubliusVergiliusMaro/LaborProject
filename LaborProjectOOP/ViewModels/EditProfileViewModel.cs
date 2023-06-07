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
using System.Windows;
using System.Windows.Input;

namespace LaborProjectOOP.Dekstop.ViewModels
{
	public class EditProfileViewModel : ViewModelBase
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
		public EditProfileViewModel(NavigationStore navigationStore, CustomerEntity currentCustomer, ICatalogService catalogService, IBookService bookService, ICustomerService customerService, ILibrarianService librarianService, ICartListService cartListService, IWishListService wishListService, IAuthorService authorService, IOrderService orderService) 
		{
			_currentCustomer = currentCustomer;
			_customerService = customerService;
			_librarianService = librarianService;
			_navigationStore = navigationStore;
			_cartListService = cartListService;
			_wishListService = wishListService;
			_catalogService = catalogService;
			_bookService = bookService;
			_authorService = authorService;
			_orderService = orderService;
			CancelCommand = new DelegateCommand(Cancel);
			SaveCommand = new DelegateCommand(Save, CanSave);
			
		}

		private bool CanSave()
		{
			return !string.IsNullOrEmpty(_currentCustomer.Login) &&
				!string.IsNullOrEmpty(_currentCustomer.Email) &&
				!string.IsNullOrEmpty(_currentCustomer.Phone);

		}

		private void Save()
		{
			// add data validation
			if (string.IsNullOrEmpty(_oldPassword))
			{
				_customerService.Update(_currentCustomer);
				MessageBox.Show("Succesfully updated");
			}
			else
			{
				if (HashService.GetMD5Hash(_oldPassword) == _currentCustomer.Password)
				{
					if (!string.IsNullOrEmpty(_newPassword))
					{
						_currentCustomer.Password = HashService.GetMD5Hash(_newPassword);
					_customerService.Update(_currentCustomer);
					MessageBox.Show("Succesfully updated");
					}
				}
				else
				{
					MessageBox.Show("Password is not right");
				}
			}
		}

		private void Cancel() => _navigationStore.CurrentViewModel = new CustomerMainViewModel(_navigationStore, _currentCustomer,false,_catalogService, _bookService, _customerService, _librarianService, _cartListService, _wishListService, _authorService, _orderService);
		//{
		//	MessageBox.Show("Cancel");
		//}

		public CustomerEntity _currentCustomer;
		public CustomerEntity CurrentCustomer
		{
			get => _currentCustomer;
			set
			{
				_currentCustomer = value;
				OnPropertyChanged(nameof(CurrentCustomer));
			}
		}
		private string _oldPassword;
		public string OldPassword
		{
			get => _oldPassword;
			set
			{
				_oldPassword = value;
				OnPropertyChanged(nameof(OldPassword));
			}
		}
		private string _newPassword;
		public string NewPassword
		{
			get => _newPassword;
			set
			{
				_newPassword = value;
				OnPropertyChanged(nameof(NewPassword));
			}
		}
		
		public ICommand CancelCommand { get; }
		public ICommand SaveCommand { get; }
	}
}

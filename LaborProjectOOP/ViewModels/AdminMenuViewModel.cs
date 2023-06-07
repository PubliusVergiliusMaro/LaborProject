using LaborProjectOOP.Database.Models;
using LaborProjectOOP.Dekstop.Commands;
using LaborProjectOOP.Dekstop.NavigationServices.Stores;
using LaborProjectOOP.Services.AuthorServices;
using LaborProjectOOP.Services.BookServices;
using LaborProjectOOP.Services.CartListServices;
using LaborProjectOOP.Services.CatalogServices;
using LaborProjectOOP.Services.CustomerServices;
using LaborProjectOOP.Services.LibrarianServices;
using LaborProjectOOP.Services.OrderHistoryServices;
using LaborProjectOOP.Services.WishListServices;
using System;
using System.Windows;
using System.Windows.Input;

namespace LaborProjectOOP.Dekstop.ViewModels
{
	public class AdminMenuViewModel : ViewModelBase
	{
		private readonly CustomerEntity _currentCustomer;
		private readonly IBookService _bookService;
		private readonly ICatalogService _catalogService;
		private readonly ICustomerService _customerService;
		private readonly ILibrarianService _librarianService;
		private readonly IAuthorService _authorService;
		private readonly IWishListService _wishListService;
		private readonly ICartListService _cartListService;
		private readonly IOrderService _orderService;
		private readonly NavigationStore _navigationStore;
		public AdminMenuViewModel(NavigationStore navigationStore, CustomerEntity currentCustomer, ICatalogService catalogService, IBookService bookService, ICustomerService customerService, ILibrarianService librarianService, ICartListService cartListService, IWishListService wishListService, IAuthorService authorService, IOrderService orderService) 
		{
			_currentCustomer = currentCustomer;
			_navigationStore = navigationStore;
			_customerService = customerService;
			_librarianService = librarianService;
			_navigationStore = navigationStore;
			_cartListService = cartListService;
			_wishListService = wishListService;
			_catalogService = catalogService;
			_bookService = bookService;
			_authorService = authorService;
			_orderService = orderService;
			EnterAsAdminCommand = new DelegateCommand(EnterAsAdmin);
			EnterAsCustomerCommand = new DelegateCommand(EnterAsCustomer);
			BackCommand = new DelegateCommand(Back);
		}

		private void Back() => _navigationStore.CurrentViewModel = new LoginViewModel(_navigationStore, _customerService, _librarianService, _wishListService,_cartListService,_catalogService,_bookService, _authorService, _orderService);
		//{
		//	MessageBox.Show("Go Back");
		//}

		private void EnterAsCustomer() => _navigationStore.CurrentViewModel = new CustomerMainViewModel(_navigationStore, _currentCustomer, true,_catalogService, _bookService, _customerService, _librarianService, _cartListService, _wishListService, _authorService, _orderService);

		//{
		//	MessageBox.Show("Go As a Customer");
		//}

		private void EnterAsAdmin() => _navigationStore.CurrentViewModel = new AdminViewModel(_navigationStore, _currentCustomer, _catalogService, _bookService, _customerService, _librarianService, _cartListService, _wishListService, _authorService, _orderService);

		//{
		//MessageBox.Show("Go As a Admin");
		//}

		public ICommand EnterAsAdminCommand { get; }
		public ICommand EnterAsCustomerCommand { get; }
		public ICommand BackCommand { get; }
	}
}

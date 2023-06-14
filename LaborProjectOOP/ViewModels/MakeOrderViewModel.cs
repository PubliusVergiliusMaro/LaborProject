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
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.RightsManagement;
using System.Windows;
using System.Windows.Input;

namespace LaborProjectOOP.Dekstop.ViewModels
{
	public class MakeOrderViewModel : ViewModelBase
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
		public MakeOrderViewModel(NavigationStore navigationStore, CustomerEntity currentCustomer, ICatalogService catalogService, IBookService bookService, ICustomerService customerService, ILibrarianService librarianService, ICartListService cartListService, IWishListService wishListService, IAuthorService authorService, IOrderService orderService)//,ICartListService cartService,IBookService bookService)
		{
			_currentCustomer = currentCustomer;
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
			_books = new ObservableCollection<BookEntity>();
			foreach(var cart in _currentCustomer.CartList)
			{
				_books.Add(cart.Book);
			}
			CancelCommand = new DelegateCommand(Cancel);
			EditCartCommand = new DelegateCommand(EditCart);
			MakeOrderCommand = new DelegateCommand(MakeOrder);
		}

		private async void MakeOrder()
		{
            await _bookService.PurchaseBooks(_currentCustomer.Id, _books);
			MessageBox.Show("Succesfuly created");
			foreach (CartListEntity customerCart in _currentCustomer.CartList.ToList())
			{
                await _cartListService.Delete(customerCart.Id);
			}
            await _customerService.Update(_currentCustomer);
			_navigationStore.CurrentViewModel = new CustomerMainViewModel(_navigationStore, _currentCustomer, false, _catalogService, _bookService, _customerService, _librarianService, _cartListService, _wishListService, _authorService, _orderService);

		}

		private void EditCart() => _navigationStore.CurrentViewModel = new CustomerActivitiesViewModel(_navigationStore, _currentCustomer,false, Constants.Enums.CustomerActivitiesInfoType.CartList, _catalogService, _bookService, _customerService, _librarianService, _cartListService, _wishListService, _authorService, _orderService);
		//{
		//	MessageBox.Show("Go To EditCart");
		//}

		private void Cancel() => _navigationStore.CurrentViewModel = new CustomerMainViewModel(_navigationStore, _currentCustomer,false, _catalogService, _bookService, _customerService, _librarianService, _cartListService, _wishListService, _authorService, _orderService);
		//{
		//	MessageBox.Show("Cancel");
		//}

		private CustomerEntity _currentCustomer;
		public CustomerEntity CurrentCustomer
		{
			get => _currentCustomer;
			set
			{
				_currentCustomer = value;
				OnPropertyChanged(nameof(CurrentCustomer));
			}
		}
		private ObservableCollection<CartListEntity> _cartList;
		public IEnumerable<CartListEntity> CartList => _cartList;
		private ObservableCollection<BookEntity> _books;
		public ICollection<BookEntity> Books => _books;
		public ICommand CancelCommand { get; }
		public ICommand EditCartCommand { get; }
		public ICommand MakeOrderCommand { get; }
	}
}

using LaborProjectOOP.Constants.Enums;
using LaborProjectOOP.Database.Models;
using LaborProjectOOP.Dekstop.Commands;
using LaborProjectOOP.Services.CartListServices;
using LaborProjectOOP.Services.WishListServices;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using LaborProjectOOP.Services.OrderHistoryServices;
using LaborProjectOOP.Dekstop.NavigationServices.Stores;
using LaborProjectOOP.Services.AuthorServices;
using LaborProjectOOP.Services.BookServices;
using LaborProjectOOP.Services.CatalogServices;
using LaborProjectOOP.Services.CustomerServices;
using LaborProjectOOP.Services.LibrarianServices;

namespace LaborProjectOOP.Dekstop.ViewModels
{
	public class CustomerOrderHistoryViewModel : ViewModelBase
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
		private readonly bool _IsItAdmin;
		public CustomerOrderHistoryViewModel(NavigationStore navigationStore, CustomerEntity currentCustomer,bool IsItAdmin, ICatalogService catalogService, IBookService bookService, ICustomerService customerService, ILibrarianService librarianService, ICartListService cartListService, IWishListService wishListService, IAuthorService authorService, IOrderService orderService)//ICustomerService customerService)
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
			_IsItAdmin = IsItAdmin;
			_books = new ObservableCollection<BookEntity>();
			ICollection<OrderEntity> orders= _currentCustomer.Orders;
			if (orders != null)
			{
				foreach (OrderEntity order in orders)
					_books.Add(order.Book);
				if (_books.Count <= 0)
				{
					IsOrdersListEmpty = Visibility.Visible;
				}
				else IsOrdersListEmpty = Visibility.Hidden;
			}
			else
			{
				MessageBox.Show("Error");
			}
			BackCommand = new DelegateCommand(Back);
		}

		private void Back() => _navigationStore.CurrentViewModel = new CustomerMainViewModel(_navigationStore, _currentCustomer,_IsItAdmin, _catalogService, _bookService, _customerService, _librarianService, _cartListService, _wishListService, _authorService, _orderService); 
		//{
		//	MessageBox.Show("Go Back");
		//}

		private ObservableCollection<BookEntity> _books;
		public ICollection<BookEntity> Books => _books;
		private BookEntity _selectedBook;
		public BookEntity SelectedBook
		{
			get => _selectedBook;
			set
			{
				_selectedBook = value;
				OnPropertyChanged(nameof(SelectedBook));
			}
		}
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
		private Visibility _isOrdersLsitEmpty;
		public Visibility IsOrdersListEmpty
		{
			get => _isOrdersLsitEmpty;
			set
			{
				_isOrdersLsitEmpty = value;
				OnPropertyChanged(nameof(IsOrdersListEmpty));
			}
		}
		public ICommand BackCommand { get; }
		public ICommand RemoveBookCommand { get; }
	}
}


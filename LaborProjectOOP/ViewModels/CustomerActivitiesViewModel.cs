using LaborProjectOOP.Constants.Enums;
using LaborProjectOOP.Database.Models;
using LaborProjectOOP.Dekstop.Commands;
using LaborProjectOOP.Dekstop.NavigationServices.Stores;
using LaborProjectOOP.Dekstop.ViewModels;
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
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LaborProjectOOP.Dekstop.ViewModels
{
	public class CustomerActivitiesViewModel : ViewModelBase
	{
		
		//private static ICollection<>
		public CustomerActivitiesViewModel(NavigationStore navigationStore, CustomerEntity currentCustomer,bool IsItAdmin,CustomerActivitiesInfoType customerActivitiesInfoType, ICatalogService catalogService, IBookService bookService, ICustomerService customerService, ILibrarianService librarianService, ICartListService cartListService, IWishListService wishListService, IAuthorService authorService, IOrderService orderService)
		{
			_IsItAdmin = IsItAdmin;
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
			_customerActivitiesInfoType = customerActivitiesInfoType;
			InitializeAsync();
            _bookBlocks = new ObservableCollection<BookBlockInfoViewModel>();
            EmptyWishList = Visibility.Hidden;
			EmptyCartList = Visibility.Hidden;
			BooksListVisible = Visibility.Visible;
            BackCommand = new DelegateCommand(Back);
			RefreshBooksCommand = new DelegateCommand(RefreshBooks);
		}

        private void RefreshBooks()
        {
			_books.Clear();
			_bookBlocks.Clear();
			InitializeAsync();
            MessageBox.Show("Refreshed");
        }

        private async Task InitializeAsync()
		{
		    if (_customerActivitiesInfoType == CustomerActivitiesInfoType.WishList)
            {
                customerWishLists  = await _wishListService.GetWishListByCustomerId(_currentCustomer.Id);
                foreach (WishListEntity wishBook in customerWishLists)
                    _books.Add(wishBook.Book);
            }
            else
            {
                customerCartLists  = await _cartListService.GetCartListByCustomerId(_currentCustomer.Id);
                foreach (CartListEntity cartBook in customerCartLists)
                    _books.Add(cartBook.Book);
            }
			if (_books.Count > 0)
			{
				foreach (BookEntity book in _books)
				{
					_bookBlocks.Add(new BookBlockInfoViewModel(book, _currentCustomer, _customerActivitiesInfoType, _customerService, _wishListService, _cartListService));
				}
			}
			else
			{
				if (_customerActivitiesInfoType == CustomerActivitiesInfoType.WishList)
				{
					EmptyWishList = Visibility.Visible;
					EmptyCartList = Visibility.Hidden;
					BooksListVisible = Visibility.Hidden;
				}
				else
				{
					EmptyWishList = Visibility.Hidden;
					EmptyCartList = Visibility.Visible;
					BooksListVisible = Visibility.Hidden;
				}
			}
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
			get=>_selectedBook;
			set
			{
				_selectedBook = value;
				OnPropertyChanged(nameof(SelectedBook));
			}
		}
		private ObservableCollection<BookBlockInfoViewModel> _bookBlocks;
		public ICollection<BookBlockInfoViewModel> BookBlocks => _bookBlocks;
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
		private Visibility _emptyCartList;
		public Visibility EmptyCartList
		{
			get => _emptyCartList;
			set
			{
				_emptyCartList = value;
				OnPropertyChanged(nameof(EmptyCartList));
			}
		}
		private Visibility _emptyWishList;
		public Visibility EmptyWishList
		{
			get => _emptyWishList;
			set
			{
				_emptyWishList = value;
				OnPropertyChanged(nameof(EmptyWishList));
			}
		}
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
       // private static ICollection<CustomerEntity> cstmrsFromDb;
		private static ICollection<CartListEntity> customerCartLists;
		private static ICollection<WishListEntity> customerWishLists;
		private readonly CustomerActivitiesInfoType _customerActivitiesInfoType;
		private Visibility _booksLsitVisibility;
		public Visibility BooksListVisible 
		{
			get => _booksLsitVisibility;
			set
			{
				_booksLsitVisibility = value;
				OnPropertyChanged(nameof(BooksListVisible));
			}
		}
        public ICommand BackCommand { get; }
		public ICommand RefreshBooksCommand { get; }

    }
}

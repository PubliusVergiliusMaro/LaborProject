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
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LaborProjectOOP.Dekstop.ViewModels
{
	public class BookPageViewModel : ViewModelBase
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
		private readonly CustomerEntity _currentCustomer;
		private readonly bool _IsItAdmin;
		public BookPageViewModel(BookEntity selectedBook, NavigationStore navigationStore, CustomerEntity currentCustomer, bool IsItAdmin,ICatalogService catalogService, IBookService bookService, ICustomerService customerService, ILibrarianService librarianService, ICartListService cartListService, IWishListService wishListService, IAuthorService authorService, IOrderService orderService)

		{
			_IsItAdmin = IsItAdmin;
			_selectedBook = selectedBook;
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
			BackCommand = new DelegateCommand(Back);
			AddToCartCommand = new DelegateCommand(AddToCart,CanAddToCart);
			AddToWishListCommand = new DelegateCommand(AddToWishList,CanAddToWishList);
			InitializeAsync();
		}
		private async Task InitializeAsync()
		{
			_suchBookExistInCart =_cartListService.CheckIfExist(_currentCustomer.Id, _selectedBook.Id);
			_suchBookExistInWish = _wishListService.CheckIfExist(_currentCustomer.Id, _selectedBook.Id);
        }
		private bool CanAddToWishList()
		{
			return !_suchBookExistInWish && !_IsItAdmin;
		}

		private bool CanAddToCart()
		{
			return !_IsItAdmin;
		}
		
		private async void AddToWishList()
		{
			await _wishListService.Create(new WishListEntity
			{
				CustomerFK = _currentCustomer.Id,
				BookFK = _selectedBook.Id
			});
			_suchBookExistInWish = true;
			MessageBox.Show("Succesfully added");
		}

		private async void AddToCart()
		{
			await _cartListService.Create(new CartListEntity
			{
				CustomerFK = _currentCustomer.Id,
				BookFK = _selectedBook.Id
			});
			MessageBox.Show("Succesfully added");

		}

		private void Back() => _navigationStore.CurrentViewModel = new CustomerMainViewModel(_navigationStore, _currentCustomer,_IsItAdmin, _catalogService, _bookService, _customerService, _librarianService, _cartListService, _wishListService, _authorService, _orderService);
		//{
		//	MessageBox.Show("We go Back");
		//}
		private bool _suchBookExistInCart { get; set; }
		private bool _suchBookExistInWish { get; set; }

        public ICommand BackCommand { get; }
		public ICommand AddToCartCommand { get; }
		public ICommand AddToWishListCommand { get; }
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
	}
}

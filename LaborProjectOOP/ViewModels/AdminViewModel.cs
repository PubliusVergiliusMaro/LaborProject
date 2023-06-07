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
	public class AdminViewModel : ViewModelBase
	{
		private readonly IBookService _bookService;
		private readonly ICatalogService _catalogService;
		private readonly ICustomerService _customerService;
		private readonly ILibrarianService _librarianService;
		private readonly IAuthorService _authorService;
		private readonly IWishListService _wishListService;
		private readonly ICartListService _cartListService;
		private readonly IOrderService _orderService;
		private readonly CustomerEntity _currentCustomer;
		private readonly NavigationStore _navigationStore;
		public AdminViewModel(NavigationStore navigationStore, CustomerEntity currentCustomer, ICatalogService catalogService, IBookService bookService, ICustomerService customerService, ILibrarianService librarianService, ICartListService cartListService, IWishListService wishListService, IAuthorService authorService, IOrderService orderService)
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
			EditCustomersViewModel = new EditCustomersViewModel(customerService);
			EditBooksViewModel = new EditBooksViewModel(bookService, catalogService);
			EditCatalogsViewModel = new EditCatalogsViewModel(catalogService);
			EditLibrariansViewModel = new EditLibrariansViewModel(librarianService);
			EditOrdersViewModel = new EditOrdersViewModel(orderService);
			EditAuthorsViewModel = new EditAuthorsViewModel(authorService);
			AddBookViewModel = new AddBookViewModel(authorService,bookService);
			AddCatalogViewModel = new AddCatalogViewModel(catalogService);
			AddAuthorViewModel = new AddAuthorViewModel(authorService);
			AddLibrarianViewModel = new AddLibrarianViewModel(librarianService);
			BackCommand = new DelegateCommand(Back);
		}

		private void Back() => _navigationStore.CurrentViewModel = new AdminMenuViewModel(_navigationStore, _currentCustomer, _catalogService, _bookService, _customerService, _librarianService, _cartListService, _wishListService, _authorService, _orderService);
		
		public EditCustomersViewModel EditCustomersViewModel { get; }
		public EditBooksViewModel EditBooksViewModel { get; }
		public EditLibrariansViewModel EditLibrariansViewModel { get; }
		public EditOrdersViewModel EditOrdersViewModel { get; }
		public EditCatalogsViewModel EditCatalogsViewModel { get; }
		public EditAuthorsViewModel EditAuthorsViewModel { get; }
		public AddBookViewModel AddBookViewModel { get; }
		public AddCatalogViewModel AddCatalogViewModel { get; }
		public AddAuthorViewModel AddAuthorViewModel { get; }
		public AddLibrarianViewModel AddLibrarianViewModel { get; }

		public ICommand BackCommand { get; }
	}
}

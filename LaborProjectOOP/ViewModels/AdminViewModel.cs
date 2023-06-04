using LaborProjectOOP.Dekstop.Commands;
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
		private readonly ICustomerService _customerService;
		private readonly IBookService _bookService;
		private readonly ICatalogService _catalogService;
		private readonly ILibrarianService _librarianService;
		private readonly IAuthorService _authorService;
		private readonly ICartListService _cartListService;
		private readonly IWishListService _wishListService;
		private readonly IOrderService _orderService;
		public AdminViewModel(ICustomerService customerService, ILibrarianService librarianService, IAuthorService authorService, ICatalogService catalogService, IBookService bookService, ICartListService cartListService, IWishListService wishListService, IOrderService orderService) 
		{
			_customerService = customerService;
			_bookService = bookService;
			_catalogService = catalogService;
			_bookService = bookService;
			_librarianService = librarianService;
			_authorService = authorService;
			_orderService = orderService;
			_cartListService = cartListService;
			_wishListService = wishListService;
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

		private void Back()
		{
			MessageBox.Show("Back");
		}
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

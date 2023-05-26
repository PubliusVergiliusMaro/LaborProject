using LaborProjectOOP.Constants.Enums;
using LaborProjectOOP.Database.Models;
using LaborProjectOOP.Services.AuthorServices;
using LaborProjectOOP.Services.BookServices;
using LaborProjectOOP.Services.CartListServices;
using LaborProjectOOP.Services.CatalogServices;
using LaborProjectOOP.Services.CustomerServices;
using LaborProjectOOP.Services.LibrarianServices;
using LaborProjectOOP.Services.OrderHistoryServices;
using LaborProjectOOP.Services.WishListServices;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace LaborProjectOOP.Dekstop.Views.Pages
{
	/// <summary>
	/// Логика взаимодействия для AdminPage.xaml
	/// </summary>
	public partial class AdminPage : Page
	{
		// Лишні прибрати
		private readonly IBookService _bookService;
		private readonly ICatalogService _catalogService;
		private readonly ICustomerService _customerService;
		private readonly ILibrarianService _librarianService;
		private readonly IAuthorService _authorService;
		private readonly IWishListService _wishListService;
		private readonly ICartListService _cartListService;
		private readonly IOrderService _orderService;
		private readonly LibrarianEntity _adminEntity;
		private static List<BookGenreTypes> selectedBooksGenres;

		public AdminPage(IBookService bookService, ICatalogService catalogService, ICustomerService customerService, ILibrarianService librarianService, IOrderService orderService, IAuthorService authorService, IWishListService wishListService, ICartListService cartListService, LibrarianEntity adminEntity)
		{
			// Лишні прибрати
			_bookService = bookService;
			_catalogService = catalogService;
			_customerService = customerService;
			_librarianService = librarianService;
			_authorService = authorService;
			_adminEntity = adminEntity;
			_wishListService = wishListService;
			_cartListService = cartListService;
			_orderService = orderService;
			selectedBooksGenres = new List<BookGenreTypes>();
			InitializeComponent();

			EditCustomerView editCustomerView = new EditCustomerView(bookService, catalogService, customerService, librarianService, orderService, authorService, wishListService, cartListService);
			((Frame)editCustomerTabItem.Content).Content = editCustomerView;

			EditBooksView editBooksView = new EditBooksView(bookService, catalogService, customerService, librarianService, orderService, authorService, wishListService, cartListService);
			((Frame)editBooksTabItem.Content).Content = editBooksView;

			EditCatalogsView editCatalogsView = new EditCatalogsView(bookService, catalogService, customerService, librarianService, orderService, authorService, wishListService, cartListService);
			((Frame)editCatalogsTabItem.Content).Content = editCatalogsView;

			EditOrdersView editOrdersView = new EditOrdersView(bookService, catalogService, customerService, librarianService, orderService, authorService, wishListService, cartListService);
			((Frame)editOrdersTabItem.Content).Content = editOrdersView;

			EditLibrariansView editLibrariansView = new EditLibrariansView(bookService, catalogService, customerService, librarianService, orderService, authorService, wishListService, cartListService);
			((Frame)editLibrariansTabItem.Content).Content = editLibrariansView;

			EditAuthorsView editAuthorsView = new EditAuthorsView(bookService, catalogService, customerService, librarianService, orderService, authorService, wishListService, cartListService);
			((Frame)editAuthorsTabItem.Content).Content = editAuthorsView;

			AddAuthorView addAuthorView = new AddAuthorView(bookService, catalogService, customerService, librarianService, orderService, authorService, wishListService, cartListService);
			((Frame)addAuthorTabItem.Content).Content = addAuthorView;

			AddBookView addBookView = new AddBookView(bookService, catalogService, customerService, librarianService, orderService, authorService, wishListService, cartListService);
			((Frame)addBookTabItem.Content).Content = addBookView;

			AddCatalogView addCatalogView = new AddCatalogView(bookService, catalogService, customerService, librarianService, orderService, authorService, wishListService, cartListService);
			((Frame)addCatalogTabItem.Content).Content = addCatalogView;

			AddLibrarianView addLibrarianView = new AddLibrarianView(bookService, catalogService, customerService, librarianService, orderService, authorService, wishListService, cartListService);
			((Frame)addLibrarianTabItem.Content).Content = addLibrarianView;
		}

		private void BackToMenuBtn_Click(object sender, RoutedEventArgs e)
		{
			newPageGrid.Visibility = Visibility.Visible;
			adminPageGrid.Visibility = Visibility.Hidden;
			pagesFrame.Navigate(new AdminMenuPage(_bookService, _catalogService, _customerService, _librarianService, _orderService, _authorService, _wishListService, _cartListService, _adminEntity));
		}
	}
}

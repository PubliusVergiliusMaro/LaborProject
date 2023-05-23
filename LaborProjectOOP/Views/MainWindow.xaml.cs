using LaborProjectOOP.Dekstop.Views.Pages;
using LaborProjectOOP.Services.AuthorServices;
using LaborProjectOOP.Services.BookServices;
using LaborProjectOOP.Services.CartListServices;
using LaborProjectOOP.Services.CatalogServices;
using LaborProjectOOP.Services.CustomerServices;
using LaborProjectOOP.Services.LibrarianServices;
using LaborProjectOOP.Services.OrderHistoryServices;
using LaborProjectOOP.Services.OrderServices;
using LaborProjectOOP.Services.WishListServices;
using System.Windows;

namespace LaborProjectOOP.Dekstop.Views
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		// Лишні прибрати
		private readonly IBookService _bookService;
		private readonly ICatalogService _catalogService;
		private readonly ICustomerService _customerService;
		private readonly ILibrarianService _librarianService;
		private readonly IOrderListService _orderListService;
		private readonly IAuthorService _authorService;
		private readonly IWishListService _wishListService;
		private readonly ICartListService _cartListService;
		private readonly IOrderService _orderService;

		public MainWindow(IBookService bookService, ICatalogService catalogService, ICustomerService customerService, ILibrarianService librarianService, IOrderListService orderListService, IOrderService orderService, IAuthorService authorService, IWishListService wishListService, ICartListService cartListService)
		{
			// Лишні прибрати
			_bookService = bookService;
			_catalogService = catalogService;
			_customerService = customerService;
			_librarianService = librarianService;
			_orderListService = orderListService;
			_authorService = authorService;
			_wishListService= wishListService;
			_cartListService= cartListService;
			_orderService= orderService;
			InitializeComponent();
			pagesFrame.Navigate(new LoginPage(_bookService, _catalogService, _customerService, _librarianService,_orderListService, _orderService, _authorService,_wishListService,_cartListService));
		}
	}
}

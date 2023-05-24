using LaborProjectOOP.Database.Models;
using LaborProjectOOP.Dekstop.Views;
using LaborProjectOOP.EntityFramework;
using LaborProjectOOP.EntityFramework.Repository;
using LaborProjectOOP.Services.AuthorServices;
using LaborProjectOOP.Services.BookServices;
using LaborProjectOOP.Services.CartListServices;
using LaborProjectOOP.Services.CatalogServices;
using LaborProjectOOP.Services.CustomerServices;
using LaborProjectOOP.Services.LibrarianServices;
using LaborProjectOOP.Services.OrderHistoryServices;
using LaborProjectOOP.Services.WishListServices;
using System.Windows;

namespace LaborProjectOOP
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private readonly ApplicationDbContext _dbContext;
		private readonly IGenericRepository<BookEntity> _bookRepository;
		private readonly IGenericRepository<CatalogEntity> _catalogRepository;
		private readonly IGenericRepository<CustomerEntity> _customerRepository;
		private readonly IGenericRepository<LibrarianEntity> _librarianRepository;
		private readonly IGenericRepository<AuthorEntity> _authorRepository;
		private readonly IGenericRepository<WishListEntity> _wishListRepository;
		private readonly IGenericRepository<CartListEntity> _cartListRepository;
		private readonly IGenericRepository<OrderEntity> _orderRepository;

		private readonly IBookService _bookService;
		private readonly ICatalogService _catalogService;
		private readonly ICustomerService _customerService;
		private readonly ILibrarianService _librarianService;
		private readonly IAuthorService _authorService;
		private readonly IWishListService _wishListService;
		private readonly ICartListService _cartListService;
		private readonly IOrderService _orderService;

		public App()
		{
			_dbContext = new ApplicationDbContext();
			_bookRepository = new GenericRepository<BookEntity>(_dbContext);
			_catalogRepository = new GenericRepository<CatalogEntity>(_dbContext);
			_customerRepository = new GenericRepository<CustomerEntity>(_dbContext);
			_librarianRepository = new GenericRepository<LibrarianEntity>(_dbContext);
			_authorRepository = new GenericRepository<AuthorEntity>(_dbContext);
			_wishListRepository = new GenericRepository<WishListEntity>(_dbContext);
			_cartListRepository = new GenericRepository<CartListEntity>(_dbContext);
			_orderRepository = new GenericRepository<OrderEntity>(_dbContext);

			_bookService = new BookService(_bookRepository, _customerRepository, _orderRepository);
			_catalogService = new CatalogService(_catalogRepository);
			_customerService = new CustomerService(_customerRepository);
			_librarianService = new LibrarianService(_librarianRepository);
			_authorService = new AuthorService(_authorRepository);
			_wishListService = new WishListService(_wishListRepository);
			_cartListService = new CartListService(_cartListRepository);
			_orderService = new OrderService(_orderRepository);
		}
		protected override void OnStartup(StartupEventArgs e)
		{
			MainWindow = new MainWindow(_bookService, _catalogService, _customerService, _librarianService, _orderService, _authorService, _wishListService, _cartListService);
			MainWindow.Show();

			base.OnStartup(e);
		}
	}
}

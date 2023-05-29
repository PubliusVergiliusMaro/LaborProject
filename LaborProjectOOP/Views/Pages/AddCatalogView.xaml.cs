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
	/// Логика взаимодействия для AddCatalogView.xaml
	/// </summary>
	public partial class AddCatalogView : UserControl
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
		public AddCatalogView(IBookService bookService, ICatalogService catalogService, ICustomerService customerService, ILibrarianService librarianService, IOrderService orderService, IAuthorService authorService, IWishListService wishListService, ICartListService cartListService)
		{
			// Лишні прибрати
			_bookService = bookService;
			_catalogService = catalogService;
			_customerService = customerService;
			_librarianService = librarianService;
			_authorService = authorService;
			_wishListService = wishListService;
			_cartListService = cartListService;
			_orderService = orderService;
			selectedBooksGenres = new List<BookGenreTypes>();
			InitializeComponent();
		}
		public void RefreshData<T>(DataGrid dataGrid, List<T> items)
		{
			dataGrid.Items.Clear();
			foreach (T entity in items)
				dataGrid.Items.Add(entity);
		}
		public void RefreshData<T>(ComboBox comboBox, List<T> items)
		{
			comboBox.ItemsSource = items;
		}
		private void CreateCatalogBtn_Click(object sender, RoutedEventArgs e)
		{
			CatalogEntity catalog = new()
			{
				Name = nameCatalogTextBox.Text
			};
			_catalogService.Create(catalog);
			MessageBox.Show("Succesfully create a new Catalog");
			nameCatalogTextBox.Text = "";

			//List<CatalogEntity> catalogs = _catalogService.GetAll();
			//catalogs.Add(new CatalogEntity { _name = "None" });
			//RefreshData(catalogsComboBox, catalogs);
			//catalogsComboBox.DisplayMemberPath = "_name";
		}


	}
}

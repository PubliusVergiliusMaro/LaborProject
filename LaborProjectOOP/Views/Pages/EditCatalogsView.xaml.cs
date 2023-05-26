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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LaborProjectOOP.Dekstop.Views.Pages
{
	/// <summary>
	/// Логика взаимодействия для EditCatalogsView.xaml
	/// </summary>
	public partial class EditCatalogsView : UserControl
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
		public EditCatalogsView(IBookService bookService, ICatalogService catalogService, ICustomerService customerService, ILibrarianService librarianService, IOrderService orderService, IAuthorService authorService, IWishListService wishListService, ICartListService cartListService)
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
			//catalogListDataGrid.Items.Clear();
			//foreach (CatalogEntity catalog in _catalogService.GetAll())
			//	catalogListDataGrid.Items.Add(catalog);
			RefreshData(catalogListDataGrid, _catalogService.GetAll());
			sortingCatalogsComboBox.ItemsSource = Enum.GetValues(typeof(CatalogsSorting)).Cast<CatalogsSorting>();
			sortingCatalogsComboBox.SelectedIndex = 0;
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
		private void sortingCatalogComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			CatalogsSorting selectedSort = (CatalogsSorting)sortingCatalogsComboBox.SelectedItem;
			List<CatalogEntity> catalogs = _catalogService.GetAll();
			switch (selectedSort)
			{
				case CatalogsSorting.None:
					catalogListDataGrid.Items.Clear();
					foreach (CatalogEntity catalog in catalogs)
						catalogListDataGrid.Items.Add(catalog);
					break;
				default: break;
			}
		}
		private void RefreshCatalogsDataGrid_Click(object sender, RoutedEventArgs e)
		{
			RefreshData(catalogListDataGrid, _catalogService.GetAll());
		}
		private void DeleteCatalog_Click(object sender, RoutedEventArgs e)
		{
			CatalogEntity catalog = catalogListDataGrid.SelectedItem as CatalogEntity;

			_catalogService.Delete(catalog.Id);
			MessageBox.Show("Succesfully deleted");
		}

	}
}

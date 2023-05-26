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
	/// Логика взаимодействия для EditAuthorsView.xaml
	/// </summary>
	public partial class EditAuthorsView : UserControl
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
		public EditAuthorsView(IBookService bookService, ICatalogService catalogService, ICustomerService customerService, ILibrarianService librarianService, IOrderService orderService, IAuthorService authorService, IWishListService wishListService, ICartListService cartListService)
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

			RefreshData(authorsListDataGrid, _authorService.GetAll());
			sortingAuthorsComboBox.ItemsSource = Enum.GetValues(typeof(CatalogsSorting)).Cast<AuthorsSorting>();
			sortingAuthorsComboBox.SelectedIndex = 0;
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
		private void DeleteAuthorBtn_Click(object sender, RoutedEventArgs e)
		{
			AuthorEntity author = authorsListDataGrid.SelectedItem as AuthorEntity;

			_authorService.Delete(author.Id);
			MessageBox.Show("Succesfully deleted");
		}

		private void refreshAuthorDataGrid_Click(object sender, RoutedEventArgs e)
		{
            RefreshData(authorsListDataGrid, _authorService.GetAll());
		}

		private void sortingAuthorsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			AuthorsSorting selectedSort = (AuthorsSorting)sortingAuthorsComboBox.SelectedItem;
			List<AuthorEntity> authors = _authorService.GetAll();
			switch (selectedSort)
			{
				case AuthorsSorting.None:
					authorsListDataGrid.Items.Clear();
					foreach (AuthorEntity author in authors)
						authorsListDataGrid.Items.Add(author);
					break;
				default: break;
			}
		}
	}
}

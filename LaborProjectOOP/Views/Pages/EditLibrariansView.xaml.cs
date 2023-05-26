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
	/// Логика взаимодействия для EditLibrariansView.xaml
	/// </summary>
	public partial class EditLibrariansView : UserControl
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
		public EditLibrariansView(IBookService bookService, ICatalogService catalogService, ICustomerService customerService, ILibrarianService librarianService, IOrderService orderService, IAuthorService authorService, IWishListService wishListService, ICartListService cartListService)
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
			//librariansListDataGrid.Items.Clear();
			//foreach (LibrarianEntity librarian in _librarianService.GetAll())
			//librariansListDataGrid.Items.Add(librarian);
			RefreshData(librariansListDataGrid, _librarianService.GetAll());

			sortingLibrariansComboBox.ItemsSource = Enum.GetValues(typeof(LibrariansSorting)).Cast<LibrariansSorting>();
			sortingLibrariansComboBox.SelectedIndex = 0;
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
		private void sortingLibrariansComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			LibrariansSorting selectedSort = (LibrariansSorting)sortingLibrariansComboBox.SelectedItem;
			List<LibrarianEntity> librarians = _librarianService.GetAll();
			switch (selectedSort)
			{
				case LibrariansSorting.None:
					librariansListDataGrid.Items.Clear();
					foreach (LibrarianEntity librarian in librarians)
						librariansListDataGrid.Items.Add(librarian);
					break;
				case LibrariansSorting.AdminsOnly:
					librariansListDataGrid.Items.Clear();
					foreach (LibrarianEntity librarian in librarians.Where(librarian => librarian.IsAdmin == true))
						librariansListDataGrid.Items.Add(librarian);
					break;
				default: break;
			}
		}
		private void MakeAnAdminBtn_Click(object sender, RoutedEventArgs e)
		{
			LibrarianEntity selectedLibrarian = librariansListDataGrid.SelectedItem as LibrarianEntity;
			if (selectedLibrarian != null)
			{
				if (selectedLibrarian.IsAdmin == true)
				{
					MessageBox.Show("Such librarian is already an admin");
				}
				else
				{
					selectedLibrarian.IsAdmin = true;
					_librarianService.Update(selectedLibrarian);
					MessageBox.Show("Succesfully updated");
				}
			}
			else
			{
				MessageBox.Show("Select some librarian");
			}
		}
		private void RefreshLibrariansDataGrid_Click(object sender, RoutedEventArgs e)
		{
			RefreshData(librariansListDataGrid, _librarianService.GetAll());
		}
		private void DeleteSelectedLibrarianBtn_Click(object sender, RoutedEventArgs e)
		{
			LibrarianEntity selectedLibrarian = librariansListDataGrid.SelectedItem as LibrarianEntity;
			if (selectedLibrarian != null)
			{
				if (selectedLibrarian.Login != "admin")
				{
					_librarianService.Delete(selectedLibrarian.Id);
					MessageBox.Show("Succesfully deleted");
				}
				else
				{
					MessageBox.Show("You can`t delete an admin");
				}
			}
			else
			{
				MessageBox.Show("Select some librarian");
			}
		}
	}
}

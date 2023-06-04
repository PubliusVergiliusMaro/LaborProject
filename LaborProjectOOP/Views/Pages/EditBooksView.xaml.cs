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
	/// Логика взаимодействия для EditBooksView.xaml
	/// </summary>
	public partial class EditBooksView : UserControl
	{
		//// Лишні прибрати
		//private readonly IBookService _bookService;
		//private readonly ICatalogService _orderService;
		//private readonly ICustomerService _orderService;
		//private readonly ILibrarianService _orderService;
		//private readonly IAuthorService _authorService;
		//private readonly IWishListService _wishListService;
		//private readonly ICartListService _cartListService;
		//private readonly IOrderService _orderService;
		//private readonly LibrarianEntity _adminEntity;
		//private static List<BookGenreTypes> selectedBooksGenres;
		public EditBooksView()//IBookService bookService, ICatalogService catalogService, ICustomerService customerService, ILibrarianService librarianService, IOrderService orderService, IAuthorService authorService, IWishListService wishListService, ICartListService cartListService)
		{
			//// Лишні прибрати
			//_bookService = bookService;
			//_orderService = catalogService;
			//_orderService = customerService;
			//_orderService = librarianService;
			//_authorService = authorService;
			//_wishListService = wishListService;
			//_cartListService = cartListService;
			//_orderService = orderService;
			//selectedBooksGenres = new List<BookGenreTypes>();
			InitializeComponent();

			////booksListDataGrid.Items.Clear();
			////foreach (BookEntity book in _bookService.GetAll())
			////	booksListDataGrid.Items.Add(book);
			//RefreshData(booksListDataGrid, _bookService.GetAll());
			////catalogsComboBox.ItemsSource = catalogs;


			//List<CatalogEntity> catalogs = _orderService.GetAll();
			//catalogs.Add(new CatalogEntity { Name = "None" });
			//RefreshData(catalogsComboBox, catalogs);
			//catalogsComboBox.DisplayMemberPath = "Name";
			
			//sortingBooksComboBox.ItemsSource = Enum.GetValues(typeof(BooksSorting)).Cast<BooksSorting>();
			//sortingBooksComboBox.SelectedIndex = 0;
		}
		//public void RefreshData<T>(DataGrid dataGrid, List<T> items)
		//{
		//	dataGrid.Items.Clear();
		//	foreach (T entity in items)
		//		dataGrid.Items.Add(entity);
		//}
		//public void RefreshData<T>(ComboBox comboBox, List<T> items)
		//{
		//	comboBox.ItemsSource = items;
		//}
		//private void sortingBooksComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		//{
		//	BooksSorting selectedSort = (BooksSorting)sortingBooksComboBox.SelectedItem;
		//	List<BookEntity> books = _bookService.GetAll();
		//	switch (selectedSort)
		//	{
		//		case BooksSorting.None:
		//			booksListDataGrid.Items.Clear();
		//			foreach (BookEntity book in books)
		//				booksListDataGrid.Items.Add(book);
		//			break;
		//		default: break;
		//	}
		//}
		//private void RefreshBooksDataGrid_Click(object sender, RoutedEventArgs e)
		//{
		//	RefreshData(booksListDataGrid, _bookService.GetAll());
		//}
		//private void AddBookToCatalogBtn_Click(object sender, RoutedEventArgs e)
		//{
		//	CatalogEntity selectedCatalog = catalogsComboBox.SelectedItem as CatalogEntity;
		//	BookEntity selectedBook = booksListDataGrid.SelectedItem as BookEntity;
		//	if (selectedCatalog != null)
		//	{
		//		if (selectedBook != null)
		//		{
		//			if (selectedCatalog.Name == "None")
		//			{
		//				selectedBook.CatalogFK = null;
		//				_bookService.Update(selectedBook);
		//				MessageBox.Show("Succesfully added");
		//			}
		//			else
		//			{
		//				selectedBook.CatalogFK = selectedCatalog.Id;
		//				_bookService.Update(selectedBook);
		//				MessageBox.Show("Succesfully added");
		//			}
		//		}
		//	}
		//}
		//private void DeleteSelectedBookBtn_Click(object sender, RoutedEventArgs e)
		//{
		//	BookEntity selectedBook = booksListDataGrid.SelectedItem as BookEntity;
		//	if (selectedBook != null)
		//	{
		//		_bookService.Delete(selectedBook.Id);
		//		MessageBox.Show("Succesfully delete");
		//	}
		//	else
		//	{
		//		MessageBox.Show("Select some book");
		//	}
		//}


	}
}

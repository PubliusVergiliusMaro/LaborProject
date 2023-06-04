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
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace LaborProjectOOP.Dekstop.Views.Pages
{
	/// <summary>
	/// Логика взаимодействия для AddBookView.xaml
	/// </summary>
	public partial class AddBookView : UserControl
	{
		// Лишні прибрати
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
		//private static string bookImagePath = "";
		public AddBookView()//IBookService bookService, ICatalogService catalogService, ICustomerService customerService, ILibrarianService librarianService, IOrderService orderService, IAuthorService authorService, IWishListService wishListService, ICartListService cartListService)
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

			//RefreshData(authorsComboBox, _authorService.GetAll());
			//authorsComboBox.DisplayMemberPath = "FullName";

			//genresComboBox.ItemsSource = Enum.GetValues(typeof(BookGenreTypes)).Cast<BookGenreTypes>();


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
		//private void AddGenreForBook_Click(object sender, RoutedEventArgs e)
		//{
		//	BookGenreTypes selectedGenre = (BookGenreTypes)Enum.Parse(typeof(BookGenreTypes), genresComboBox.SelectedItem.ToString());
		//	selectedBooksGenres.Add(selectedGenre);

		//	selectedGenresListView.ItemsSource = null;
		//	List<string> enumNames = new List<string>();
		//	foreach (var value in selectedBooksGenres)
		//	{
		//		enumNames.Add(Enum.GetName(typeof(BookGenreTypes), value));
		//	}
		//	selectedGenresListView.ItemsSource = enumNames;
		//}
		//private void AddBookBtn_Click(object sender, RoutedEventArgs e)
		//{
		//	AuthorEntity selectedAuthor = authorsComboBox.SelectedItem as AuthorEntity;

		//	BookEntity book = new()
		//	{
		//		Title = titleTextBox.Text,
		//		Description = descriptionTextBox.Text,
		//		Price = Convert.ToInt32(priceTextBox.Text),
		//		ImagePath = bookImagePath,
		//		AuthorFK = selectedAuthor.Id,
		//		Genres = selectedBooksGenres,
		//	};
		//	_bookService.Create(book);
		//	MessageBox.Show("Succesfully create a new Book");
		//	titleTextBox.Text = "";
		//	descriptionTextBox.Text = "";
		//	priceTextBox.Text = "";
		//	bookImage.Source = new BitmapImage();
		//	authorsComboBox.SelectedItem = null;
		//	genresComboBox.SelectedItem = null;
		//	selectedGenresListView.ItemsSource = null;

		//}
		//private void AddImageBtn_Click(object sender, RoutedEventArgs e)
		//{
		//	OpenFileDialog openFileDialog = new()
		//	{
		//		Multiselect = false,
		//		Filter = "Images|*.png;*.jpg"
		//	};
		//	openFileDialog.ShowDialog();

		//	if (!openFileDialog.CheckFileExists || string.IsNullOrEmpty(openFileDialog.FileName))
		//	{
		//		//MessageBox.Show("Error with getting image.");
		//		return;
		//	}
		//	BitmapImage image = new(new Uri(openFileDialog.FileName));
		//	bookImage.Source = image;
		//	bookImagePath = openFileDialog.FileName;
		//}

	}
}

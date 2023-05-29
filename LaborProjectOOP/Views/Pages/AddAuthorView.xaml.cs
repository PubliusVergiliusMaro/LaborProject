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
	/// Логика взаимодействия для AddAuthorView.xaml
	/// </summary>
	public partial class AddAuthorView : UserControl
	{
		//// Лишні прибрати
		//private readonly IBookService _bookService;
		//private readonly ICatalogService _catalogService;
		//private readonly ICustomerService _customerService;
		//private readonly ILibrarianService _librarianService;
		//private readonly IAuthorService _authorService;
		//private readonly IWishListService _wishListService;
		//private readonly ICartListService _cartListService;
		//private readonly IOrderService _orderService;
		//private readonly LibrarianEntity _adminEntity;
		//private static List<BookGenreTypes> selectedBooksGenres;
		public AddAuthorView()//IBookService bookService, ICatalogService catalogService, ICustomerService customerService, ILibrarianService librarianService, IOrderService orderService, IAuthorService authorService, IWishListService wishListService, ICartListService cartListService)
		{
			////28
			//// Лишні прибрати
			//_bookService = bookService;
			//_catalogService = catalogService;
			//_customerService = customerService;
			//_librarianService = librarianService;
			//_authorService = authorService;
			//_wishListService = wishListService;
			//_cartListService = cartListService;
			//_orderService = orderService;
			//selectedBooksGenres = new List<BookGenreTypes>();
			InitializeComponent();
			//List <AuthorEntity> authors = _authorService.GetAll();
			//authorsComboBox.ItemsSource = authors;
			
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
		//private void AddAuthorBtn_Click(object sender, RoutedEventArgs e)
		//{
		//	AuthorEntity authorEntity = new()
		//	{
		//		Name = nameAuthorTextBox.Text,
		//		Surname = surnameAuthorTextBox.Text,
		//	};
		//	_authorService.Create(authorEntity);
		//	MessageBox.Show("Succesfully create a new Author");
		//	nameAuthorTextBox.Text = "";
		//	surnameAuthorTextBox.Text = "";

		//	//RefreshData(authorsComboBox, _authorService.GetAll());
		//	//authorsComboBox.DisplayMemberPath = "FullName";
		//}
	}
}

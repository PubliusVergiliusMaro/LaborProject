using LaborProjectOOP.Constants.Enums;
using LaborProjectOOP.Database.Models;
using LaborProjectOOP.Services.AuthorServices;
using LaborProjectOOP.Services.BookServices;
using LaborProjectOOP.Services.CartListServices;
using LaborProjectOOP.Services.CatalogServices;
using LaborProjectOOP.Services.CustomerServices;
using LaborProjectOOP.Services.LibrarianServices;
using LaborProjectOOP.Services.OrderServices;
using LaborProjectOOP.Services.WishListServices;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace LaborProjectOOP.Dekstop.Views.Pages
{
	/// <summary>
	/// Логика взаимодействия для CustomerMainPage.xaml
	/// </summary>
	public partial class CustomerMainPage : Page
	{
		private readonly IBookService _bookService;
		private readonly ICatalogService _catalogService;
		private readonly ICustomerService _customerService;
		private readonly ILibrarianService _librarianService;
		private readonly IOrderService _orderService;
		private readonly IAuthorService _authorService;
		private readonly IWishListService _wishListService;
		private readonly ICartListService _cartListService;
		private readonly CustomerEntity _currentCustomer;
		private readonly bool _isItAdmin;

		//private static BookEntity selectedBook;
		public CustomerMainPage(
			IBookService bookService, ICatalogService catalogService, ICustomerService customerService, ILibrarianService librarianService, IOrderService orderService, IAuthorService authorService, IWishListService wishListService, ICartListService cartListService,
			CustomerEntity currentEntity, bool isItAdmin)
		{
			_bookService = bookService;
			_catalogService = catalogService;
			_customerService = customerService;
			_librarianService = librarianService;
			_orderService = orderService;
			_currentCustomer = currentEntity;
			_authorService = authorService;
			_wishListService = wishListService;
			_cartListService = cartListService;
			this._isItAdmin = isItAdmin;

			InitializeComponent();
			bookList.ItemsSource = _bookService.GetAll();
			if (_isItAdmin) AdminFunctionality();
		}
		private void AdminFunctionality()
		{
			takeSelectedBtn.IsEnabled = false;
		}
		private void TakeSelectedBtn_Click(object sender, RoutedEventArgs e)
		{

			if (_currentCustomer.CartList.Count != 0)
			{
				bookPageGrid.Visibility = Visibility.Hidden;
				newPageGrid.Visibility = Visibility.Visible;
				pagesFrame.Navigate(new OrderPage(_bookService, _catalogService, _customerService, _librarianService, _orderService, _authorService, _wishListService, _cartListService, _currentCustomer));
			}
			else MessageBox.Show("Select some book for making order");
		}

		private void BackBtn_Click(object sender, RoutedEventArgs e)
		{

			bookPageGrid.Visibility = Visibility.Hidden;
			newPageGrid.Visibility = Visibility.Visible;

			bool isAdmin = false;
			LibrarianEntity currentLibrarian = new LibrarianEntity();
			foreach (LibrarianEntity libr in _librarianService.GetAll())
			{
				if (_currentCustomer.Login == libr.Login && _currentCustomer.Password == libr.Password)
				{
					isAdmin = true;
					currentLibrarian = libr;
				}
			}
			if (isAdmin)
			{
				pagesFrame.Navigate(new AdminMenuPage(_bookService, _catalogService, _customerService, _librarianService, _orderService, _authorService, _wishListService, _cartListService, currentLibrarian));
			}
			else
				pagesFrame.Navigate(new LoginPage(_bookService, _catalogService, _customerService, _librarianService, _orderService, _authorService, _wishListService, _cartListService));
		}

		// Customer Orders
		//private void myOrderBtn_Click(object sender, RoutedEventArgs e)
		//{
		//	bookPageGrid.Visibility = Visibility.Hidden;
		//	newPageGrid.Visibility = Visibility.Visible;
		//	pagesFrame.Navigate(new CustomerOrderPage(_bookService, _catalogService, _customerService, _librarianService, _orderService, _authorService, _currentCustomer, _customerCart));
		//}

		private void bookList_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			BookEntity selectedBook = bookList.SelectedItem as BookEntity;
			bookPageGrid.Visibility = Visibility.Hidden;
			newPageGrid.Visibility = Visibility.Visible;
			pagesFrame.Navigate(new BookPage(_bookService, _catalogService, _customerService, _librarianService, _orderService, _authorService, _wishListService, _cartListService, _currentCustomer, selectedBook, _isItAdmin));

		}

		private void Ellipse_Loaded(object sender, RoutedEventArgs e)
		{
			if (!_isItAdmin)
			{
				Ellipse ellipse = (Ellipse)sender;
				ImageBrush imageBrush = (ImageBrush)ellipse.Fill;
				imageBrush.ImageSource = new BitmapImage(new Uri(_currentCustomer.AvatarImagePath));
			}
		}

		private void ProfileBtn_Click(object sender, RoutedEventArgs e)
		{
			bookPageGrid.Visibility = Visibility.Hidden;
			newPageGrid.Visibility = Visibility.Visible;
			pagesFrame.Navigate(new EditProfilePage(_bookService, _catalogService, _customerService, _librarianService, _orderService, _authorService, _wishListService, _cartListService, _currentCustomer));

		}

		private void WishListBtn_Click(object sender, RoutedEventArgs e)
		{
			bookPageGrid.Visibility = Visibility.Hidden;
			newPageGrid.Visibility = Visibility.Visible;
			pagesFrame.Navigate(new CustomerActivitiesPage(_bookService, _catalogService, _customerService, _librarianService, _orderService, _authorService, _wishListService, _cartListService, _currentCustomer, _isItAdmin, CustomerActivitiesInfoType.WishList));
		}

		private void booksInWishListTextBlock_Loaded(object sender, RoutedEventArgs e)
		{

			if (!_isItAdmin)
			{
				TextBlock textBlock = (TextBlock)sender;
				textBlock.Text = _currentCustomer.WishList.Count.ToString();
			}
		}

		private void CustomerActivitiesBtn_Click(object sender, RoutedEventArgs e)
		{

			bookPageGrid.Visibility = Visibility.Hidden;
			newPageGrid.Visibility = Visibility.Visible;
			pagesFrame.Navigate(new CustomerActivitiesPage(_bookService, _catalogService, _customerService, _librarianService, _orderService, _authorService, _wishListService, _cartListService, _currentCustomer, _isItAdmin, CustomerActivitiesInfoType.CartList));

		}

		private void bookBuyNumberTextBlock_Loaded(object sender, RoutedEventArgs e)
		{
			if (!_isItAdmin)
			{
				TextBlock textBlock = (TextBlock)sender;
				textBlock.Text = _currentCustomer.CartList.Count.ToString();
			}
		}

		private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			bookList.ItemsSource = _bookService.GetAll().Where(book => book.Title.Contains(searchTextBox.Text));
		}

		private void GenreComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			bookList.ItemsSource = _bookService.GetAll().Where(book => book.Title.Contains(searchTextBox.Text));
		}

		private void OrdersHistoryButton_Click(object sender, RoutedEventArgs e)
		{
			newPageGrid.Visibility = Visibility.Visible;
			bookPageGrid.Visibility = Visibility.Hidden;
			pagesFrame.Navigate(new CustomerOrdersHistoryPage(_bookService, _catalogService, _customerService, _librarianService, _orderService, _authorService, _wishListService, _cartListService, _currentCustomer,_isItAdmin));
		}
	}
}

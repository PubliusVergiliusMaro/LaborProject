using LaborProjectOOP.Database.Models;
using LaborProjectOOP.Services.AuthorServices;
using LaborProjectOOP.Services.BookServices;
using LaborProjectOOP.Services.CatalogServices;
using LaborProjectOOP.Services.CustomerServices;
using LaborProjectOOP.Services.LibrarianServices;
using LaborProjectOOP.Services.OrderServices;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
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
		private readonly CustomerEntity _currentCustomer;
		private static List<BookEntity> _customerCart;
		private readonly bool _isItAdmin;
		
		//private static BookEntity selectedBook;
		public CustomerMainPage(
			IBookService bookService, ICatalogService catalogService, ICustomerService customerService, ILibrarianService librarianService, IOrderService orderService, IAuthorService authorService,
			CustomerEntity currentEntity, bool isItAdmin, List<BookEntity> customerCart)
		{
			_bookService = bookService;
			_catalogService = catalogService;
			_customerService = customerService;
			_librarianService = librarianService;
			_orderService = orderService;
			_currentCustomer = currentEntity;
			_authorService = authorService;
			_customerCart = customerCart;
			this._isItAdmin = isItAdmin;
			//CustomerAvatar = _currentCustomer.AvatarImagePath;
			
			InitializeComponent();

			bookBuyNumberTextBlock.Text = customerCart.Count.ToString();
			bookList.ItemsSource = _bookService.GetAll();
			//foreach (CatalogEntity catalog in _catalogService.GetAll())
			//{
			//foreach (BookEntity book in catalog.Books)
			//{

			//BooksList.Items.Add(book);
			//}
			//}
			
			if (_isItAdmin) AdminFunctionality();
		}
		private void AdminFunctionality()
		{
			takeSelectedBtn.IsEnabled = false;	
		}
		private void TakeSelectedBtn_Click(object sender, RoutedEventArgs e)
		{

			if (_customerCart.Count != 0)
			{
				bookPageGrid.Visibility = Visibility.Hidden;
				newPageGrid.Visibility = Visibility.Visible;
				pagesFrame.Navigate(new OrderPage(_bookService, _catalogService, _customerService, _librarianService, _orderService, _authorService, _currentCustomer, _customerCart));
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
				pagesFrame.Navigate(new AdminMenuPage(_bookService, _catalogService, _customerService, _librarianService, _orderService, _authorService, currentLibrarian));
			}
			else
				pagesFrame.Navigate(new LoginPage(_bookService, _catalogService, _customerService, _librarianService, _orderService, _authorService));
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
			pagesFrame.Navigate(new BookPage(_bookService, _catalogService, _customerService, _librarianService, _orderService, _authorService, _currentCustomer, selectedBook, _isItAdmin, _customerCart));

		}

		private void Grid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			bookPageGrid.Visibility = Visibility.Hidden;
			newPageGrid.Visibility = Visibility.Visible;
			pagesFrame.Navigate(new CustomerCartPage(_bookService, _catalogService, _customerService, _librarianService, _orderService, _authorService, _currentCustomer, _isItAdmin, _customerCart));
		}

		private void Ellipse_Loaded(object sender, RoutedEventArgs e)
		{
			Ellipse ellipse = (Ellipse)sender;
			ImageBrush imageBrush = (ImageBrush)ellipse.Fill;
			imageBrush.ImageSource = new BitmapImage(new Uri(_currentCustomer.AvatarImagePath));
		}

		private void ProfileBtn_Click(object sender, RoutedEventArgs e)
		{
			bookPageGrid.Visibility = Visibility.Hidden;
			newPageGrid.Visibility = Visibility.Visible;
			pagesFrame.Navigate(new EditProfilePage(_bookService, _catalogService, _customerService, _librarianService, _orderService, _authorService, _currentCustomer, _customerCart));

		}
	}
}

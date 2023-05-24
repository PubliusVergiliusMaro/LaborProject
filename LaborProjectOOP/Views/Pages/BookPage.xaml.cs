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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace LaborProjectOOP.Dekstop.Views.Pages
{
	/// <summary>
	/// Логика взаимодействия для BookPage.xaml
	/// </summary>
	public partial class BookPage : Page
	{
		private readonly BookEntity _selectedBook;
		private readonly IBookService _bookService;
		private readonly ICatalogService _catalogService;
		private readonly ICustomerService _customerService;
		private readonly ILibrarianService _librarianService;
		private readonly IAuthorService _authorService;
		private readonly IWishListService _wishListService;
		private readonly ICartListService _cartListService;
		private readonly IOrderService _orderService;
		private readonly CustomerEntity _currentCustomer;
		private readonly bool _isItAdmin;
		public BookPage(IBookService bookService, ICatalogService catalogService, ICustomerService customerService, ILibrarianService librarianService, IOrderService orderService, IAuthorService authorService, IWishListService wishListService, ICartListService cartListService,
			CustomerEntity currentCustomer, BookEntity selectedBook,bool isItAdmin)
		{
			_bookService = bookService;
			_catalogService = catalogService;
			_customerService = customerService;
			_librarianService = librarianService;
			_currentCustomer = currentCustomer;
			_authorService = authorService;
			_wishListService = wishListService;
			_selectedBook = selectedBook;
			_cartListService = cartListService;
			_isItAdmin = isItAdmin;
			_orderService = orderService;
			InitializeComponent();
			titleTextBlock.Text = _selectedBook.Title;
			descriptionTextBlock.Text = _selectedBook.Description;
			priceTextBlock.Text = _selectedBook.Price.ToString();

			if (_wishListService.CheckIfExist(_currentCustomer.Id, _selectedBook.Id))
				addToWishListListBtn.IsEnabled = false;
				// Create image.
				BitmapImage image = new BitmapImage(new Uri(_selectedBook.ImagePath));
			bookImage.Source = image;
		}

		private void backBtn_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			bookPageGrid.Visibility = System.Windows.Visibility.Hidden;
			newPageGrid.Visibility = System.Windows.Visibility.Visible;
			pagesFrame.Navigate(new CustomerMainPage(_bookService, _catalogService, _customerService, _librarianService, _orderService, _authorService,_wishListService, _cartListService, _currentCustomer,_isItAdmin));

		}

		private void addToBuyListBtn_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			_cartListService.Create(new CartListEntity
			{
				CustomerFK = _currentCustomer.Id,
				BookFK = _selectedBook.Id
			});
			MessageBox.Show("Succesfully added");
			bookPageGrid.Visibility = System.Windows.Visibility.Hidden;
			newPageGrid.Visibility = System.Windows.Visibility.Visible;
			pagesFrame.Navigate(new CustomerMainPage(_bookService, _catalogService, _customerService, _librarianService, _orderService, _authorService,_wishListService, _cartListService, _currentCustomer,_isItAdmin));

		}

		private void addToWishListListBtn_Click(object sender, RoutedEventArgs e)
		{
			_wishListService.Create(new WishListEntity
			{
				CustomerFK = _currentCustomer.Id,
				BookFK = _selectedBook.Id
			});
			MessageBox.Show("Succesfully added");
			addToWishListListBtn.IsEnabled = false;
		}
	}
}

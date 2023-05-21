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
		private readonly IOrderService _orderService;
		private readonly IAuthorService _authorService;
		private readonly CustomerEntity _currentCustomer;
		private readonly List<BookEntity> _customerBuyList;
		private readonly bool _isItAdmin;
		public BookPage(IBookService bookService, ICatalogService catalogService, ICustomerService customerService, ILibrarianService librarianService, IOrderService orderService, IAuthorService authorService, CustomerEntity currentCustomer, BookEntity selectedBook,bool isItAdmin, List<BookEntity> customerBuyList)
		{
			_bookService = bookService;
			_catalogService = catalogService;
			_customerService = customerService;
			_librarianService = librarianService;
			_orderService = orderService;
			_currentCustomer = currentCustomer;
			_authorService = authorService;
			_selectedBook = selectedBook;
			_customerBuyList = customerBuyList;
			_isItAdmin = isItAdmin;
			InitializeComponent();
			titleTextBlock.Text = _selectedBook.Title;
			descriptionTextBlock.Text = _selectedBook.Description;
			priceTextBlock.Text = _selectedBook.Price.ToString();

			// Create image.
			BitmapImage image = new BitmapImage(new Uri(_selectedBook.ImagePath));
			bookImage.Source = image;
			_customerBuyList = customerBuyList;
		}

		private void backBtn_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			bookPageGrid.Visibility = System.Windows.Visibility.Hidden;
			newPageGrid.Visibility = System.Windows.Visibility.Visible;
			pagesFrame.Navigate(new CustomerMainPage(_bookService, _catalogService, _customerService, _librarianService, _orderService, _authorService, _currentCustomer,_isItAdmin, _customerBuyList));

		}

		private void addToBuyListBtn_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			_customerBuyList.Add(_selectedBook);
			MessageBox.Show("Succesfully added");
			bookPageGrid.Visibility = System.Windows.Visibility.Hidden;
			newPageGrid.Visibility = System.Windows.Visibility.Visible;
			pagesFrame.Navigate(new CustomerMainPage(_bookService, _catalogService, _customerService, _librarianService, _orderService, _authorService, _currentCustomer,_isItAdmin, _customerBuyList));

		}
	}
}

using LaborProjectOOP.Database.Models;
using LaborProjectOOP.Services.AuthorServices;
using LaborProjectOOP.Services.BookServices;
using LaborProjectOOP.Services.CartListServices;
using LaborProjectOOP.Services.CatalogServices;
using LaborProjectOOP.Services.CustomerServices;
using LaborProjectOOP.Services.LibrarianServices;
using LaborProjectOOP.Services.OrderHistoryServices;
using LaborProjectOOP.Services.OrderServices;
using LaborProjectOOP.Services.WishListServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LaborProjectOOP.Dekstop.Views.Pages
{
	/// <summary>
	/// Логика взаимодействия для CustomerOrdersHistoryPage.xaml
	/// </summary>
	public partial class CustomerOrdersHistoryPage : Page
	{
		// Лишні прибрати
		private readonly IBookService _bookService;
		private readonly ICatalogService _catalogService;
		private readonly ICustomerService _customerService;
		private readonly ILibrarianService _librarianService;
		private readonly IOrderListService _orderListService;
		private readonly IAuthorService _authorService;
		private readonly IWishListService _wishListService;
		private readonly ICartListService _cartListService;
		private readonly IOrderService _orderService;
		private readonly CustomerEntity _currentCustomer;
		private readonly bool _isItAdmin;
		public CustomerOrdersHistoryPage(IBookService bookService, ICatalogService catalogService, ICustomerService customerService, ILibrarianService librarianService, IOrderListService orderListService, IOrderService orderService, IAuthorService authorService, IWishListService wishListService, ICartListService cartListService, CustomerEntity customer,bool isItAdmin)
		{
			// Лишні прибрати
			_bookService = bookService;
			_catalogService = catalogService;
			_customerService = customerService;
			_librarianService = librarianService;
			_orderListService = orderListService;
			_currentCustomer = customer;
			_authorService = authorService;
			_wishListService = wishListService;
			_cartListService = cartListService;
			_isItAdmin = isItAdmin;
			_orderService = orderService;
			InitializeComponent();
		}
		private void booksViewPanel_Loaded(object sender, RoutedEventArgs e)
		{

			List<BookEntity> books = new List<BookEntity>();
			foreach (OrderEntity order in _currentCustomer.OrderList.Orders)
					books.Add(order.Book);
			if (books.Count != 0)
			{
				foreach (var book in books)
				{
					StackPanel bookPanel = new StackPanel()
					{
						Orientation = Orientation.Horizontal,
						Width = 1000,
						Height = 200,
						Margin = new Thickness(0, 0, 0, 40),
						Name = $"panel_{book.Id}",
					};
					Border informarionBorder = new Border()
					{
						Width = booksViewContainer.ActualWidth / 9 * 4,
						Height = 200,
						Padding = new Thickness(10),
						BorderBrush = Brushes.Gray,

						BorderThickness = new Thickness(1),

					};
					TextBlock textBlock = new TextBlock()
					{
						HorizontalAlignment = HorizontalAlignment.Left,
						VerticalAlignment = VerticalAlignment.Top,
						Text = $"Id:\t{book.Id}\nTitle:\t{book.Title}\nPrice:\t{book.Price}\nAuthor Name:\t{book.Author?.Name}\nAuthor Surname:\t{book.Author?.Surname}",
						FontSize = 22,

					};
					informarionBorder.Child = textBlock;
					bookPanel.Children.Add(informarionBorder);

					Border imageBorder = new Border()
					{
						Width = 300,
						Height = 200,
						Padding = new Thickness(10),
						HorizontalAlignment = HorizontalAlignment.Right,
						BorderBrush = Brushes.Gray,
						BorderThickness = new Thickness(1),
					};
					if (File.Exists(book.ImagePath))
					{
						BitmapImage bitmap;

						bitmap = new BitmapImage(new Uri(System.IO.Path.GetFullPath(book.ImagePath)));

						Image image = new Image()
						{
							Source = bitmap,
							Name = $"image{book.Id}",
						};
						imageBorder.Child = image;
						bookPanel.Children.Add(imageBorder);
					}
					booksViewPanel.Children.Add(bookPanel);
				}
			}
			else
			{
				emptyOrdersPageGrid.Visibility = Visibility.Visible;
				booksViewContainer.Visibility = Visibility.Hidden;
			}
		}

		private void CancelBtn_Click(object sender, RoutedEventArgs e)
		{
			newPageGrid.Visibility = Visibility.Visible;
			customerOrdersPageGrid.Visibility = Visibility.Hidden;
			pagesFrame.Navigate(new CustomerMainPage(_bookService, _catalogService, _customerService, _librarianService, _orderListService, _orderService, _authorService, _wishListService, _cartListService, _currentCustomer,_isItAdmin));
		}

	}
}

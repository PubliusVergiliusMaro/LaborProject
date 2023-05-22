﻿using LaborProjectOOP.Constants.Enums;
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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LaborProjectOOP.Dekstop.Views.Pages
{
	/// <summary>
	/// Логика взаимодействия для CustomerActivitiesPage.xaml
	/// </summary>
	public partial class CustomerActivitiesPage : Page
	{
		private readonly IBookService _bookService;
		private readonly ICatalogService _catalogService;
		private readonly ICustomerService _customerService;
		private readonly ILibrarianService _librarianService;
		private readonly IOrderService _orderService;
		private readonly IAuthorService _authorService;
		private readonly IWishListService _wishListService;
		private readonly ICartListService _cartListService;
		private readonly CustomerActivitiesInfoType _customerActivitiesInfoType;
		private readonly CustomerEntity _currentCustomer;
		private readonly bool _isItAdmin;
		public CustomerActivitiesPage(
			IBookService bookService, ICatalogService catalogService, ICustomerService customerService, ILibrarianService librarianService, IOrderService orderService, IAuthorService authorService, IWishListService wishListService, ICartListService cartListService,
			CustomerEntity currentCustomer, bool isItAdmin,CustomerActivitiesInfoType customerActivitiesInfoType)
		{

			_bookService = bookService;
			_catalogService = catalogService;
			_customerService = customerService;
			_librarianService = librarianService;
			_orderService = orderService;
			_currentCustomer = currentCustomer;
			_authorService = authorService;
			_wishListService = wishListService;
			_cartListService = cartListService;
			_isItAdmin = isItAdmin;
			_customerActivitiesInfoType = customerActivitiesInfoType;
			
			InitializeComponent();
		}

		#region Розібратися з ции
		private void booksViewPanel_Loaded(object sender, RoutedEventArgs e)
		{
			List<BookEntity> books = new List<BookEntity>();
			if (_customerActivitiesInfoType == CustomerActivitiesInfoType.WishList)
			{
				List<WishListEntity> customerWishLists = _wishListService.GetWishListByCustomerId(_currentCustomer.Id);
			    foreach (WishListEntity wishBook in customerWishLists)
				books.Add(wishBook.Book);
			}
			else
			{
				List<CartListEntity> customerCartLists = _cartListService.GetCartListByCustomerId(_currentCustomer.Id);
				foreach (CartListEntity cartBook in customerCartLists)
					books.Add(cartBook.Book);
			}

			if(books.Count != 0)
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

					Border removeBorder = new Border()
					{
						Width = 100,
						Height = 200,
						Padding = new Thickness(10),
						HorizontalAlignment = HorizontalAlignment.Right,
						BorderBrush = Brushes.Gray,
						BorderThickness = new Thickness(1),
					};
					Label removeLabel = new Label()
					{
						Content = "X",
						HorizontalAlignment = HorizontalAlignment.Center,
						VerticalAlignment = VerticalAlignment.Center,
					};

					removeLabel.MouseLeftButtonDown += async (object sender, MouseButtonEventArgs e) => await RemoveHandler(book.Id, $"image{book.Id}");
					removeBorder.Child = removeLabel;
					bookPanel.Children.Add(removeBorder);

					booksViewPanel.Children.Add(bookPanel);
				}
			}
			else
			{
				if (_customerActivitiesInfoType == CustomerActivitiesInfoType.CartList)
				{
					emptyCartBooksGrid.Visibility = Visibility.Visible;
					booksViewContainer.Visibility = Visibility.Hidden;
				}
				else
				{
					emptyWishListGrid.Visibility = Visibility.Visible;
					booksViewContainer.Visibility = Visibility.Hidden;
				}
			}
		}
		private async Task RemoveHandler(int id, string imageName)
		{
			MessageBoxButton button = MessageBoxButton.YesNo;
			MessageBoxResult result = MessageBox.Show("Do you want delete?", string.Empty, button);

			if (result == MessageBoxResult.Yes)
			{
				Image imageToDelete = FindChild<Image>(this, imageName);
				if (imageToDelete == null)
				{
					return;
				}

				imageToDelete.Source = null;
				if (_customerActivitiesInfoType == CustomerActivitiesInfoType.WishList)
				{
					_wishListService.Delete(_wishListService.GetByCustomerIdAndBookId(_currentCustomer.Id, id).Id);
				}
				else
					_cartListService.Delete(_cartListService.GetByCustomerIdAndBookId(_currentCustomer.Id, id).Id);
				
				var panels = booksViewPanel.Children.OfType<StackPanel>();
				var panelToDelete = panels.FirstOrDefault(panel => panel.Name == $"panel_{id}");
				booksViewPanel.Children.Remove(panelToDelete);

				MessageBox.Show("Succesfully deleted");
				
				if (_customerActivitiesInfoType == CustomerActivitiesInfoType.WishList)
				{
					if (_currentCustomer.WishList.Count == 0)
					{
						emptyWishListGrid.Visibility = Visibility.Visible;
						booksViewContainer.Visibility = Visibility.Hidden;
					}
				}
				else
				{
					if(_currentCustomer.CartList.Count == 0)
					{
						emptyCartBooksGrid.Visibility = Visibility.Visible;
						booksViewContainer.Visibility = Visibility.Hidden;
					}
				}
				return;
			}
		}

		public static T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
		{
			for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
			{
				var child = VisualTreeHelper.GetChild(parent, i);

				if (child != null && child is T && child.GetValue(FrameworkElement.NameProperty) as string == childName)
				{
					return (T)child;
				}
				else
				{
					var result = FindChild<T>(child, childName);
					if (result != null)
					{
						return result;
					}
				}
			}

			return null;
		}
		#endregion

		private void backBtn_Click(object sender, RoutedEventArgs e)
		{
			cartPageGrid.Visibility = System.Windows.Visibility.Hidden;
			newPageGrid.Visibility = System.Windows.Visibility.Visible;
			pagesFrame.Navigate(new CustomerMainPage(_bookService, _catalogService, _customerService, _librarianService, _orderService, _authorService, _wishListService, _cartListService, _currentCustomer, _isItAdmin));
		}
	}
}
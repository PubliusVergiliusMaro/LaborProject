using LaborProjectOOP.Database.Models;
using LaborProjectOOP.Services.AuthorServices;
using LaborProjectOOP.Services.BookServices;
using LaborProjectOOP.Services.CartListServices;
using LaborProjectOOP.Services.CatalogServices;
using LaborProjectOOP.Services.CustomerServices;
using LaborProjectOOP.Services.Helpers;
using LaborProjectOOP.Services.LibrarianServices;
using LaborProjectOOP.Services.OrderHistoryServices;
using LaborProjectOOP.Services.WishListServices;
using Microsoft.Win32;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace LaborProjectOOP.Dekstop.Views.Pages
{
	/// <summary>
	/// Логика взаимодействия для RegistrationPage.xaml
	/// </summary>
	public partial class RegistrationPage : Page
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
		private static string ImagePath;
		public RegistrationPage(IBookService bookService, ICatalogService catalogService, ICustomerService customerService, ILibrarianService librarianService, IOrderService orderService, IAuthorService authorService, IWishListService wishListService, ICartListService cartListService)
		{
			// Лишні прибрати
			_bookService = bookService;
			_catalogService = catalogService;
			_customerService = customerService;
			_librarianService = librarianService;
			_authorService = authorService;
			_wishListService = wishListService;
			_cartListService = cartListService;
			_orderService= orderService;
			InitializeComponent();
			avatarImageGrid.Visibility = Visibility.Hidden;
		}

		private void SaveBtn_Click(object sender, RoutedEventArgs e)
		{
			if (string.IsNullOrEmpty(loginTextBox.Text) || string.IsNullOrEmpty(passwordTextBox.Text) || string.IsNullOrEmpty(emailTextBox.Text) || string.IsNullOrEmpty(phoneTextBox.Text))
			{
				MessageBox.Show("Fill all lines");
			}
			else
			{
				if (defaultImageCheckBox.IsChecked.Value)
				{
					CustomerEntity customer = new CustomerEntity
					{
						Login = loginTextBox.Text,
						Password = HashService.GetMD5Hash(passwordTextBox.Text),
						Email = emailTextBox.Text,
						Phone = phoneTextBox.Text,
						AvatarImagePath = "C:\\GAmes\\Курси\\LaborProjectOOP\\LaborProjectOOP\\LaborProjectOOP\\Images\\defaultAvatarIcon.png"
					};
					_customerService.Create(customer);
				}
				else
				{
				_customerService.Create(new CustomerEntity
				{
					Login = loginTextBox.Text,
					Password = HashService.GetMD5Hash(passwordTextBox.Text),
					Email = emailTextBox.Text,
					Phone = phoneTextBox.Text,
					AvatarImagePath = ImagePath
				});
				}

				//List<CustomerEntity> customers = _customerService.GetAll();
				//customers.Add(customer);
				//_customerService.SaveCustomers(customers);//Переробити на бд
				MessageBox.Show("Succesfuly created");
				registrationPageGrid.Visibility = Visibility.Hidden;
				newPageGrid.Visibility = Visibility.Visible;
				pagesFrame.Navigate(new LoginPage(_bookService, _catalogService, _customerService, _librarianService,  _orderService, _authorService,_wishListService, _cartListService));
			}

		}

		private void CancelBtn_Click(object sender, RoutedEventArgs e)
		{
			registrationPageGrid.Visibility = Visibility.Hidden;
			newPageGrid.Visibility = Visibility.Visible;
			pagesFrame.Navigate(new LoginPage(_bookService, _catalogService, _customerService, _librarianService,  _orderService, _authorService, _wishListService, _cartListService));
		}

		private void selectAvatarImage_Click(object sender, RoutedEventArgs e)
		{
			avatarImageGrid.Visibility = Visibility.Visible;
			OpenFileDialog openFileDialog = new()
			{
				Multiselect = false,
				Filter = "Images|*.png;*.jpg"
			};
			openFileDialog.ShowDialog();

			if (!openFileDialog.CheckFileExists || string.IsNullOrEmpty(openFileDialog.FileName))
			{
				openFileDialog.FileName = @"C:\GAmes\Курси\LaborProjectOOP\LaborProjectOOP\LaborProjectOOP\Images\defaultAvatarIcon.png";
			}
			BitmapImage image = new(new Uri(openFileDialog.FileName));
			ImagePath = openFileDialog.FileName;
			customerAvatarImage.ImageSource = image;

		}
	}
}

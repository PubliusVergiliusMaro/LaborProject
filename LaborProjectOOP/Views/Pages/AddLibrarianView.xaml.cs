using LaborProjectOOP.Constants.Enums;
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
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace LaborProjectOOP.Dekstop.Views.Pages
{
	/// <summary>
	/// Логика взаимодействия для AddLibrarianView.xaml
	/// </summary>
	public partial class AddLibrarianView : UserControl
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
		public AddLibrarianView()//IBookService bookService, ICatalogService catalogService, ICustomerService customerService, ILibrarianService librarianService, IOrderService orderService, IAuthorService authorService, IWishListService wishListService, ICartListService cartListService)
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
		//private void CreateLibrarianBtn_Click(object sender, RoutedEventArgs e)
		//{
		//	if (!string.IsNullOrEmpty(loginTextBox.Text) && !string.IsNullOrEmpty(passwordTextBox.Text) && !string.IsNullOrEmpty(salaryTextBox.Text))
		//	{
		//		LibrarianEntity librarianEntity = new()
		//		{
		//			Login = loginTextBox.Text,
		//			Password = HashService.GetMD5Hash(passwordTextBox.Text),
		//			Salary = Convert.ToInt32(salaryTextBox.Text),
		//			WorkExperience = Convert.ToByte(experienceTextBox.Text),
		//			IsAdmin = adminCheckBox.IsChecked.Value,
		//		};
		//		_orderService.Create(librarianEntity);
		//		MessageBox.Show("Succesfully create");

		//		adminCheckBox.IsChecked = false;
		//		loginTextBox.Text = "";
		//		passwordTextBox.Text = "";
		//		salaryTextBox.Text = "";
		//		experienceTextBox.Text = "";
		//	}
		//	else
		//	{
		//		MessageBox.Show("Fill all Fields");
		//	}
		//}

	}
}

using LaborProjectOOP.Database.Models;
using LaborProjectOOP.Services.AuthorServices;
using LaborProjectOOP.Services.BookServices;
using LaborProjectOOP.Services.CatalogServices;
using LaborProjectOOP.Services.CustomerServices;
using LaborProjectOOP.Services.LibrarianServices;
using LaborProjectOOP.Services.OrderServices;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace LaborProjectOOP.Dekstop.Views.Pages
{
	/// <summary>
	/// Логика взаимодействия для AdminPage.xaml
	/// </summary>
	public partial class AdminPage : Page
	{
		// Лишні прибрати
		private readonly IBookService _bookService;
		private readonly ICatalogService _catalogService;
		private readonly ICustomerService _customerService;
		private readonly ILibrarianService _librarianService;
		private readonly IOrderService _orderService;
		private readonly IAuthorService _authorService;
		private readonly LibrarianEntity _adminEntity;
		private static string bookImagePath = "";
		public AdminPage(IBookService bookService, ICatalogService catalogService, ICustomerService customerService, ILibrarianService librarianService, IOrderService orderService, IAuthorService authorService, LibrarianEntity adminEntity)
		{
			// Лишні прибрати
			_bookService = bookService;
			_catalogService = catalogService;
			_customerService = customerService;
			_librarianService = librarianService;
			_orderService = orderService;
			_authorService = authorService;
			_adminEntity = adminEntity;
			InitializeComponent();

			//CatalogService catalogService = new CatalogService();
			//for(int i = 0; i < 10; i++)
			//{
			//	Books.Add(new BookEntity($"Name{i}",$"desc",100+i,true,new AuthorEntity() { Name=$"Grd{i}",Surname=$"FRfdfsd{i}"}));
			//}


			//CatalogEntity catalog = new CatalogEntity()
			//{
			//	Id = 1,
			//	Name = "First CatalogEntity",
			//	Books= Books
			//};
			//List<CatalogEntity> catalogs = new List<CatalogEntity>() { catalog };
			//CatalogService.SaveCatalogs(catalogs);

			UpdateData();

		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			newPageGrid.Visibility = Visibility.Visible;
			adminPageGrid.Visibility = Visibility.Hidden;
			pagesFrame.Navigate(new AdminMenuPage(_bookService, _catalogService, _customerService, _librarianService, _orderService, _authorService, _adminEntity));
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{
			//Переробити на бд
			//List<CustomerEntity> customers = _customerService.GetAll();
			////CustomerEntity selectedCustomer = customers.Where(c=>c.Login == customersComboBox.SelectedItem.ToString()).FirstOrDefault();
			//customers.Remove(customers.Where(c => c.Login == customersComboBox.SelectedItem.ToString()).FirstOrDefault());
			//_customerService.SaveCustomers(customers);
			UpdateData();
		}
		private void Button_Click_2(object sender, RoutedEventArgs e)
		{
			//Переробити на бд

			//List<CustomerEntity> customers = _customerService.GetAll();
			//CustomerEntity selectedCustomer = customers.Where(c => c.Login == customersComboBox.SelectedItem.ToString()).FirstOrDefault();
			//selectedCustomer.IsBanned = true;
			//customers.Remove(customers.Where(c => c.Login == customersComboBox.SelectedItem.ToString()).FirstOrDefault());
			//customers.Add(selectedCustomer);
			//foreach (CustomerEntity customer in customers)
			//	_customerService.Update(customer);
			UpdateData();
		}
		private void UpdateData()
		{
			catalogListDataGrid.Items.Clear();
			customerListDataGrid.Items.Clear();
			//customersComboBox.Items.Clear();
			List<CatalogEntity> catalogsFromFile = _catalogService.GetAll();
			foreach (CatalogEntity catalog in catalogsFromFile)
			{
				catalogListDataGrid.Items.Add(catalog);
				foreach (BookEntity book in catalog.Books)
					booksListDataGrid.Items.Add(book);
			}

			List<CustomerEntity> customersFromFile = _customerService.GetAll();
			foreach (CustomerEntity customer in customersFromFile)
			{
				customerListDataGrid.Items.Add(customer);
				//customersComboBox.Items.Add(customer.Login);
			}

			List<AuthorEntity> authors = _authorService.GetAll();
			authorsComboBox.ItemsSource = authors;
			authorsComboBox.DisplayMemberPath = "FullName";
			//foreach(AuthorEntity author in authors)
			//{
			//	//authorsComboBox.Items.Add(author);
			//	authorsComboBox.Items.Add(author.Surname + " " + author.Name);
			//}
		}

		private void addImageBtn_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Multiselect = false;
			openFileDialog.Filter = "Images|*.png;*.jpg";
			openFileDialog.ShowDialog();

			if (!openFileDialog.CheckFileExists || string.IsNullOrEmpty(openFileDialog.FileName))
			{
				//MessageBox.Show("Error with getting image.");
				return;
			}
			BitmapImage image = new BitmapImage(new Uri(openFileDialog.FileName));
			bookImage.Source = image;
			bookImagePath = openFileDialog.FileName;
		}

		private void addBookBtn_Click(object sender, RoutedEventArgs e)
		{
			List<AuthorEntity> authors = _authorService.GetAll();
			AuthorEntity selectedAuthor = authorsComboBox.SelectedItem as AuthorEntity;

			BookEntity book = new BookEntity()
			{
				Title = titleTextBox.Text,
				Description = descriptionTextBox.Text,
				Price = Convert.ToInt32(priceTextBox.Text),
				ImagePath = bookImagePath,
				AuthorFK = selectedAuthor.Id,

			};
			_bookService.Create(book);
			MessageBox.Show("Succesfully create a new Book");
			titleTextBox.Text = "";
			descriptionTextBox.Text = "";
			priceTextBox.Text = "";
			bookImage.Source = new BitmapImage();

		}

		private void AddAuthorBtn_Click(object sender, RoutedEventArgs e)
		{
			AuthorEntity authorEntity = new AuthorEntity
			{
				Name = nameAuthorTextBox.Text,
				Surname = surnameAuthorTextBox.Text,
			};
			_authorService.Create(authorEntity);
			MessageBox.Show("Succesfully create a new Author");
			nameAuthorTextBox.Text = "";
			surnameAuthorTextBox.Text = "";
			UpdateData();
		}

		private void CreateCatalogBtn_Click(object sender, RoutedEventArgs e)
		{
			CatalogEntity catalog = new CatalogEntity()
			{
				Name = nameCatalogTextBox.Text
			};
			_catalogService.Create(catalog);
			MessageBox.Show("Succesfully create a new Catalog");
			nameCatalogTextBox.Text = "";
			UpdateData();
		}
	}
}

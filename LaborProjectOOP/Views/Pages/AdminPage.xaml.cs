using LaborProjectOOP.Constants.Enums;
using LaborProjectOOP.Database.Models;
using LaborProjectOOP.Services.AuthorServices;
using LaborProjectOOP.Services.BookServices;
using LaborProjectOOP.Services.CatalogServices;
using LaborProjectOOP.Services.CustomerServices;
using LaborProjectOOP.Services.Helpers;
using LaborProjectOOP.Services.LibrarianServices;
using LaborProjectOOP.Services.OrderServices;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
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

			customerListDataGrid.Items.Clear();
			foreach (CustomerEntity customer in _customerService.GetAll())
				customerListDataGrid.Items.Add(customer);
			catalogListDataGrid.Items.Clear();
			foreach (CatalogEntity catalog in _catalogService.GetAll())
				catalogListDataGrid.Items.Add(catalog);
			booksListDataGrid.Items.Clear();
			foreach (BookEntity book in _bookService.GetAll())
				booksListDataGrid.Items.Add(book);
			librariansListDataGrid.Items.Clear();
			foreach (LibrarianEntity librarian in _librarianService.GetAll())
			librariansListDataGrid.Items.Add(librarian);
			foreach (OrderEntity order in _orderService.GetAll())
			ordersListDataGrid.Items.Add(order);

			List<AuthorEntity> authors = _authorService.GetAll();
			authorsComboBox.ItemsSource = authors;
			authorsComboBox.DisplayMemberPath = "FullName";

			List<CatalogEntity> catalogs = _catalogService.GetAll();
			catalogs.Add(new CatalogEntity { Name = "None" });
			catalogsComboBox.ItemsSource = catalogs;
			catalogsComboBox.DisplayMemberPath = "Name";

			sortingCustomersComboBox.ItemsSource = Enum.GetValues(typeof(CustomersSorting)).Cast<CustomersSorting>();
			sortingCatalogsComboBox.ItemsSource = Enum.GetValues(typeof(CatalogsSorting)).Cast<CatalogsSorting>();
			sortingBooksComboBox.ItemsSource = Enum.GetValues(typeof(BooksSorting)).Cast<BooksSorting>();
			sortingLibrariansComboBox.ItemsSource = Enum.GetValues(typeof(LibrariansSorting)).Cast<LibrariansSorting>();
			sortingOrdersComboBox.ItemsSource = Enum.GetValues(typeof(OrdersSorting)).Cast<OrdersSorting>();

			sortingCustomersComboBox.SelectedIndex = 0;
			sortingCatalogsComboBox.SelectedIndex = 0;
			sortingBooksComboBox.SelectedIndex = 0;
			sortingLibrariansComboBox.SelectedIndex = 0;
			sortingOrdersComboBox.SelectedIndex = 0;
		}

		#region Navigation Methods
		private void BackToMenuBtn_Click(object sender, RoutedEventArgs e)
		{
			newPageGrid.Visibility = Visibility.Visible;
			adminPageGrid.Visibility = Visibility.Hidden;
			pagesFrame.Navigate(new AdminMenuPage(_bookService, _catalogService, _customerService, _librarianService, _orderService, _authorService, _adminEntity));
		}
		#endregion
		#region Deleting methods

	    private void DeleteCustomer_Click(object sender, RoutedEventArgs e)
		{

			CustomerEntity selectedCustomer = customerListDataGrid.SelectedItem as CustomerEntity;
			if(selectedCustomer == null)
			{
				MessageBox.Show("Select Customer");
			}
			else
			_customerService.Delete(selectedCustomer.Id);

			//Переробити на бд
			//List<CustomerEntity> customers = _customerService.GetAll();
			////CustomerEntity selectedCustomer = customers.Where(c=>c.Login == customersComboBox.SelectedItem.ToString()).FirstOrDefault();
			//customers.Remove(customers.Where(c => c.Login == customersComboBox.SelectedItem.ToString()).FirstOrDefault());
			//_customerService.SaveCustomers(customers);
			//UpdateData();
		}
		private void DeleteCatalog_Click(object sender, RoutedEventArgs e)
		{
			CatalogEntity catalog = catalogListDataGrid.SelectedItem as CatalogEntity;

			_catalogService.Delete(catalog.Id);
		}
		private void DeleteSelectedLibrarianBtn_Click(object sender, RoutedEventArgs e)
		{
			LibrarianEntity selectedLibrarian = librariansListDataGrid.SelectedItem as LibrarianEntity;
			if (selectedLibrarian != null)
			{
				if (selectedLibrarian.Login != "admin")
				{
					_librarianService.Delete(selectedLibrarian.Id);
					MessageBox.Show("Succesfully deleted");
				}
				else
				{
					MessageBox.Show("You can`t delete an admin");
				}
			}
			else
			{
				MessageBox.Show("Select some librarian");
			}
		}
		private void DeleteSelectedOrderBtn_Click(object sender, RoutedEventArgs e)
		{
			OrderEntity selectedOrder = ordersListDataGrid.SelectedItem as OrderEntity;
			if (selectedOrder != null)
			{
				_orderService.Delete(selectedOrder.Id);
				MessageBox.Show("Succesfully delete");
			}
			else
			{
				MessageBox.Show("Select some order");
			}
		}

		private void DeleteSelectedBookBtn_Click(object sender, RoutedEventArgs e)
		{
			BookEntity selectedBook = booksListDataGrid.SelectedItem as BookEntity;
			if (selectedBook != null)
			{
				_bookService.Delete(selectedBook.Id);
				MessageBox.Show("Succesfully delete");
			}
			else
			{
				MessageBox.Show("Select some book");
			}
		}
		#endregion
		#region Add/Create methods
		private void AddToBlackList_Click(object sender, RoutedEventArgs e)
		{
			CustomerEntity selectedCustomer = customerListDataGrid.SelectedItem as CustomerEntity;
			if (selectedCustomer == null)
			{
				MessageBox.Show("Select Customer");
			}
			else
			{
				if (selectedCustomer.IsBanned)
				{
					selectedCustomer.IsBanned = false;
				}
				else
					selectedCustomer.IsBanned = true;

				_customerService.Update(selectedCustomer);
			}
			//Переробити на бд

			//List<CustomerEntity> customers = _customerService.GetAll();
			//CustomerEntity selectedCustomer = customers.Where(c => c.Login == customersComboBox.SelectedItem.ToString()).FirstOrDefault();
			//selectedCustomer.IsBanned = true;
			//customers.Remove(customers.Where(c => c.Login == customersComboBox.SelectedItem.ToString()).FirstOrDefault());
			//customers.Add(selectedCustomer);
			//foreach (CustomerEntity customer in customers)
			//	_customerService.Update(customer);

			
		}
		private void AddImageBtn_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new()
			{
				Multiselect = false,
				Filter = "Images|*.png;*.jpg"
			};
			openFileDialog.ShowDialog();

			if (!openFileDialog.CheckFileExists || string.IsNullOrEmpty(openFileDialog.FileName))
			{
				//MessageBox.Show("Error with getting image.");
				return;
			}
			BitmapImage image = new(new Uri(openFileDialog.FileName));
			bookImage.Source = image;
			bookImagePath = openFileDialog.FileName;
		}

		private void AddBookBtn_Click(object sender, RoutedEventArgs e)
		{
			//List<AuthorEntity> authors = _authorService.GetAll();
			AuthorEntity selectedAuthor = authorsComboBox.SelectedItem as AuthorEntity;

			BookEntity book = new()
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
			AuthorEntity authorEntity = new()
			{
				Name = nameAuthorTextBox.Text,
				Surname = surnameAuthorTextBox.Text,
			};
			_authorService.Create(authorEntity);
			MessageBox.Show("Succesfully create a new Author");
			nameAuthorTextBox.Text = "";
			surnameAuthorTextBox.Text = "";


			List<AuthorEntity> authors = _authorService.GetAll();
			authorsComboBox.ItemsSource = authors;
			authorsComboBox.DisplayMemberPath = "FullName";
		}
		private void CreateCatalogBtn_Click(object sender, RoutedEventArgs e)
		{
			CatalogEntity catalog = new()
			{
				Name = nameCatalogTextBox.Text
			};
			_catalogService.Create(catalog);
			MessageBox.Show("Succesfully create a new Catalog");
			nameCatalogTextBox.Text = "";
		}
		private void CreateLibrarianBtn_Click(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrEmpty(loginTextBox.Text) && !string.IsNullOrEmpty(passwordTextBox.Text) && !string.IsNullOrEmpty(salaryTextBox.Text))
			{
				LibrarianEntity librarianEntity = new()
				{
					Login = loginTextBox.Text,
					Password = HashService.GetMD5Hash(passwordTextBox.Text),
					Salary = Convert.ToInt32(salaryTextBox.Text),
					WorkExperience = Convert.ToByte(experienceTextBox.Text),
					IsAdmin = adminCheckBox.IsChecked.Value,
				};
				_librarianService.Create(librarianEntity);
				MessageBox.Show("Succesfully create");

				adminCheckBox.IsChecked = false;
				loginTextBox.Text = "";
				passwordTextBox.Text = "";
				salaryTextBox.Text = "";
				experienceTextBox.Text = "";
			}
			else
			{
				MessageBox.Show("Fill all Fields");
			}
		}
		private void AddBookToCatalogBtn_Click(object sender, RoutedEventArgs e)
		{
			CatalogEntity selectedCatalog = catalogsComboBox.SelectedItem as CatalogEntity;
			BookEntity selectedBook = booksListDataGrid.SelectedItem as BookEntity;
			if (selectedCatalog != null)
			{
				if (selectedBook != null)
				{
					if (selectedCatalog.Name == "None")
					{
						selectedBook.CatalogFK = null;
						_bookService.Update(selectedBook);
						MessageBox.Show("Succesfully added");
					}
					else
					{
						selectedBook.CatalogFK = selectedCatalog.Id;
						_bookService.Update(selectedBook);
						MessageBox.Show("Succesfully added");
					}
				}
			}
		}
		#endregion
		#region Refresh methods

		private void RefreshCustomerDataGrid_Click(object sender, RoutedEventArgs e)
		{
			customerListDataGrid.Items.Clear();
			foreach (CustomerEntity customer in _customerService.GetAll())
				customerListDataGrid.Items.Add(customer);
		}

		private void RefreshCatalogsDataGrid_Click(object sender, RoutedEventArgs e)
		{
			catalogListDataGrid.Items.Clear();
			foreach (CatalogEntity catalog in _catalogService.GetAll())
				catalogListDataGrid.Items.Add(catalog);
		}
		
		private void RefreshBooksDataGrid_Click(object sender, RoutedEventArgs e)
		{
			booksListDataGrid.Items.Clear();
			foreach (BookEntity book in _bookService.GetAll())
				booksListDataGrid.Items.Add(book);
		}

		private void RefreshLibrariansDataGrid_Click(object sender, RoutedEventArgs e)
		{
			librariansListDataGrid.Items.Clear();
			foreach (LibrarianEntity librarian in _librarianService.GetAll())
				librariansListDataGrid.Items.Add(librarian);
		}
		private void RefreshOrdersDataGrid_Click(object sender, RoutedEventArgs e)
		{
			ordersListDataGrid.Items.Clear();
			foreach(OrderEntity order in _orderService.GetAll())
				ordersListDataGrid.Items.Add(order);
		}
		#endregion
		#region Make Methods


		private void MakeAnAdminBtn_Click(object sender, RoutedEventArgs e)
		{
			LibrarianEntity selectedLibrarian = librariansListDataGrid.SelectedItem as LibrarianEntity;
			if (selectedLibrarian != null)
			{
				if (selectedLibrarian.IsAdmin == true)
				{
					MessageBox.Show("Such librarian is already an admin");
				}
				else
				{
					selectedLibrarian.IsAdmin = true;
					_librarianService.Update(selectedLibrarian);
					MessageBox.Show("Succesfully updated");
				}
			}
			else
			{
				MessageBox.Show("Select some librarian");
			}
		}

		
		private void MakeNotActiveSelectedOrderBtn_Click(object sender, RoutedEventArgs e)
		{
			OrderEntity selectedOrder = ordersListDataGrid.SelectedItem as OrderEntity;
			if (selectedOrder != null)
			{
				selectedOrder.IsActual = false;
				_orderService.Update(selectedOrder);
				MessageBox.Show("Succesfully make not active");
			}
			else
			{
				MessageBox.Show("Select some order");
			}
		}

		#endregion

		#region Sorting methods
		private void sortingCustomersComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			CustomersSorting selectedSort = (CustomersSorting)sortingCustomersComboBox.SelectedItem;
			List<CustomerEntity> customers = _customerService.GetAll();
			switch (selectedSort)
			{
				case CustomersSorting.None:
					customerListDataGrid.Items.Clear();
					foreach (CustomerEntity customer in customers)
						customerListDataGrid.Items.Add(customer);
					break;
				case CustomersSorting.BannedCustomers:
					customerListDataGrid.Items.Clear();
					foreach (CustomerEntity customer in customers.Where(customer=>customer.IsBanned==true))
						customerListDataGrid.Items.Add(customer);
					break;
				default:
					break;
			}
		}
		private void sortingCatalogComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			CatalogsSorting selectedSort = (CatalogsSorting)sortingCatalogsComboBox.SelectedItem;
			List<CatalogEntity> catalogs = _catalogService.GetAll();
			switch (selectedSort)
			{
				case CatalogsSorting.None:
					catalogListDataGrid.Items.Clear();
					foreach(CatalogEntity catalog in catalogs)
						catalogListDataGrid.Items.Add(catalog);
					break;
				default : break;
			}
		}
		private void sortingBooksComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			BooksSorting selectedSort = (BooksSorting)sortingBooksComboBox.SelectedItem;
			List<BookEntity> books = _bookService.GetAll();
			switch (selectedSort)
			{
				case BooksSorting.None:
					booksListDataGrid.Items.Clear();
					foreach (BookEntity book in books)
						booksListDataGrid.Items.Add(book);
					break;
				default: break;
			}
		}

		private void sortingLibrariansComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			LibrariansSorting selectedSort = (LibrariansSorting)sortingLibrariansComboBox.SelectedItem;
			List<LibrarianEntity> librarians = _librarianService.GetAll();
			switch (selectedSort)
			{
				case LibrariansSorting.None:
					librariansListDataGrid.Items.Clear();
					foreach (LibrarianEntity librarian in librarians)
						librariansListDataGrid.Items.Add(librarian);
					break;
				case LibrariansSorting.AdminsOnly:
					librariansListDataGrid.Items.Clear();
					foreach (LibrarianEntity librarian in librarians.Where(librarian=>librarian.IsAdmin==true))
						librariansListDataGrid.Items.Add(librarian);
					break;
				default: break;
			}
		}

		private void sortingOrdersComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			OrdersSorting selectedSort = (OrdersSorting)sortingOrdersComboBox.SelectedItem;
			List<OrderEntity> orders = _orderService.GetAll();
			switch (selectedSort)
			{
				case OrdersSorting.None:
					ordersListDataGrid.Items.Clear();
					foreach (OrderEntity order in orders)
						ordersListDataGrid.Items.Add(order);
					break;
                case OrdersSorting.ActiveOnly:
					ordersListDataGrid.Items.Clear();
					foreach (OrderEntity order in orders.Where(order => order.IsActual == true))
						ordersListDataGrid.Items.Add(order);
					break;
				default: break;
			}
		}
		#endregion

		private void CustomerListDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
		{
			DeleteCustomerBtn.IsEnabled = true;
			AddToBlackListCustomerBtn.IsEnabled = true;
		}

		
	}
}

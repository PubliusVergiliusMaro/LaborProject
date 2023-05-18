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

			//UpdateData();
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
			catalogComboBox.ItemsSource = catalogs;
			catalogComboBox.DisplayMemberPath = "Name";
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			newPageGrid.Visibility = Visibility.Visible;
			adminPageGrid.Visibility = Visibility.Hidden;
			pagesFrame.Navigate(new AdminMenuPage(_bookService, _catalogService, _customerService, _librarianService, _orderService, _authorService, _adminEntity));
		}

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

		private void CustomerListDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
		{
			DeleteCustomerBtn.IsEnabled = true;
			AddToBlackListCustomerBtn.IsEnabled = true;
		}
		
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

		private void AddBookToCatalogBtn_Click(object sender, RoutedEventArgs e)
		{
			CatalogEntity selectedCatalog = catalogComboBox.SelectedItem as CatalogEntity;
			BookEntity selectedBook = booksListDataGrid.SelectedItem as BookEntity;
			if (selectedCatalog != null)
			{
				if(selectedBook != null)
				{
					if(selectedCatalog.Name == "None")
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

		private void CreateLibrarianBtn_Click(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrEmpty(loginTextBox.Text) && !string.IsNullOrEmpty(passwordTextBox.Text) && !string.IsNullOrEmpty(salaryTextBox.Text))
			{
				LibrarianEntity librarianEntity = new()
				{
					IsAdmin = adminCheckBox.IsChecked.Value,
					Login = loginTextBox.Text,
					Password = passwordTextBox.Text,
					Salary = Convert.ToInt32(salaryTextBox.Text),
					WorkExperience = Convert.ToByte(experienceTextBox.Text),
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
	}
}

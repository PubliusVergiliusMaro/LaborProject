using LaborProjectOOP.Constants.Enums;
using LaborProjectOOP.Database.Models;
using LaborProjectOOP.Dekstop.Commands;
using LaborProjectOOP.Dekstop.Views.Pages;
using LaborProjectOOP.Services.BookServices;
using LaborProjectOOP.Services.CatalogServices;
using LaborProjectOOP.Services.CustomerServices;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Windows;
using System.Windows.Input;

namespace LaborProjectOOP.Dekstop.ViewModels
{
	public class CustomerMainViewModel : ViewModelBase
	{
		private readonly CustomerEntity _currentCustomer;
		private readonly IBookService _bookService;
		public CustomerMainViewModel(ICatalogService catalogService, IBookService bookService, ICustomerService customerService)
		{
			_currentCustomer = customerService.GetById(3);
			_bookService = bookService;
			_books = new ObservableCollection<BookEntity>(_bookService.GetAll());
			
			CartCommand = new DelegateCommand(OpenCart);
			WishListCommand = new DelegateCommand(OpenWishList);
			OrdersHistoryCommand = new DelegateCommand(OpenOrdersHistory);
			ProfileCommand = new DelegateCommand(OpenProfile);
			TakeSelectedCommand = new DelegateCommand(TakeSelected);
			BackCommand = new DelegateCommand(Back);
			ClearFiltersCommand = new DelegateCommand(ClearFilters);
			
			//_orders = //catalogService.GetAll();
			//_genres = Enum.GetValues(typeof(BookGenreTypes)).Cast<BookGenreTypes>();
			_genres = new ObservableCollection<BookGenreTypes>(Enum.GetValues(typeof(BookGenreTypes)).Cast<BookGenreTypes>().ToArray());
			
			SelectedGenre = BookGenreTypes.None;
			_catalogs = new ObservableCollection<CatalogEntity>(catalogService.GetAll())
			{
				new CatalogEntity { Name = "None" }
			};
			SelectedCatalog = _catalogs.Where(c => c.Name == "None").FirstOrDefault();
			SearchLineText = "";
			CustomerAvatarSource = _currentCustomer.AvatarImagePath;
			NotFoundBook = Visibility.Hidden;
			BooksList = Visibility.Visible;
		}

		private void ShowBookInfo()
		{
			MessageBox.Show(SelectedBook.Title);
		}

		private void ClearFilters()
		{
			SelectedCatalog = _catalogs.Where(c => c.Name == "None").FirstOrDefault();
			SelectedGenre = 0;
		}

		private void Back()
		{
			MessageBox.Show("We go Back");
		}

		private void OpenProfile()
		{
			MessageBox.Show("We go to Profile");
		}

		private void OpenOrdersHistory()
		{
			MessageBox.Show("We go to History");
		}

		private void OpenWishList()
		{
			MessageBox.Show("We go to wishList"); ;
		}

		private void OpenCart()
		{
			MessageBox.Show("We go to Cart");
		}

		private void TakeSelected()
		{
			if (_currentCustomer.CartList.Count != 0)
			{
				MessageBox.Show("We go to Selected");
			}
			else MessageBox.Show("Select some book for making order");
		}
		public int _booksInCart;
		public int BooksInCart
		{
			get => _booksInCart;
			set
			{
				_booksInCart = value;
				OnPropertyChanged(nameof(BooksInCart));
			}
		}
		public int _booksInWishList;
		public int BooksInWishList
		{
			get => _booksInWishList;
			set
			{
				_booksInWishList = value;
				OnPropertyChanged(nameof(BooksInWishList));
			}
		}
		public string _customerAvatarSource;
		public string CustomerAvatarSource
		{
			get => _customerAvatarSource;
			set
			{
				_customerAvatarSource = value;
				OnPropertyChanged(nameof(CustomerAvatarSource));
			}
		}
		public string _searchLineText;
		public string SearchLineText
		{
			get => _searchLineText;
			set
			{
				SelectedBook = null;
				NotFoundBook = Visibility.Hidden;
				BooksList = Visibility.Visible;
				if (_searchLineText != value)
				{
					_searchLineText = value;
					OnPropertyChanged(nameof(SearchLineText));
					SetBooksToListBox();
				}
			}
		}
		public Visibility _notFoundBook;
		public Visibility NotFoundBook
		{
			get => _notFoundBook;
			set
			{
				_notFoundBook = value;
				OnPropertyChanged(nameof(NotFoundBook));
			}
		}
		public Visibility _booksList;
		public Visibility BooksList
		{
			get => _booksList;
			set
			{
				_booksList = value;
				OnPropertyChanged(nameof(BooksList));
			}
		}
		private static ObservableCollection<CatalogEntity> _catalogs;
		public static IEnumerable<CatalogEntity> Catalogs => _catalogs;
		public CatalogEntity _selectedCatalog;
		public CatalogEntity SelectedCatalog
		{
			get => _selectedCatalog;
			set
			{
				SelectedBook = null;
				NotFoundBook = Visibility.Hidden;
				BooksList = Visibility.Visible;
				if (_selectedCatalog != value)
				{
					_selectedCatalog = value;
					OnPropertyChanged(nameof(SelectedCatalog));
					SetBooksToListBox();
				}
			}
		}

		private static ObservableCollection<BookGenreTypes> _genres;
		public static IEnumerable<BookGenreTypes> Genres => _genres;
		private static BookGenreTypes _selectedGenre;
		public BookGenreTypes SelectedGenre
		{
			get => _selectedGenre;
			set
			{
				SelectedBook = null;
				NotFoundBook = Visibility.Hidden;
				BooksList = Visibility.Visible;
				if (_selectedGenre != value)
				{
					_selectedGenre = value;
					OnPropertyChanged(nameof(SelectedGenre));
					SetBooksToListBox();
				}
			}
		}

		private static ObservableCollection<BookEntity> _books;
		public static ICollection<BookEntity> Books => _books;
		
		private static BookEntity _selectedBook;
		public BookEntity SelectedBook
		{
			get => _selectedBook;
			set
			{
				if (_selectedBook != value)
				{
					_selectedBook = value;
					OnPropertyChanged(nameof(SelectedBook));

					//Navigation
					if(SelectedBook != null)
					MessageBox.Show($"We go to {SelectedBook.Title}");
				}
			}
		}
		private void SetBooksToListBox()
		{
			//Update the _books collection with the filtered results
			if (_books != null && SelectedGenre != null && SelectedCatalog != null && SearchLineText != null)
			{
				_books.Clear();
				if (SelectedGenre != BookGenreTypes.None)
				{
					if (SelectedCatalog.Name != "None")
					{
						foreach (BookEntity book in _bookService.GetAll()
							.Where(book => book.Catalog == SelectedCatalog && book.Genres.Contains(SelectedGenre))
							.Where(book => book.Title.Contains(SearchLineText)))
						{
							_books.Add(book);
						}
					}
					else
					{
						foreach (BookEntity book in _bookService.GetAll()
							.Where(book => book.Genres.Contains(SelectedGenre))
							.Where(book => book.Title.Contains(SearchLineText)))
						{
							_books.Add(book);
						}
					}
				}
				else
				{
					if (SelectedCatalog.Name != "None")
					{
						foreach (BookEntity book in _bookService.GetAll()
							.Where(book => book.Catalog == SelectedCatalog)
							.Where(book => book.Title.Contains(SearchLineText)))
						{
							_books.Add(book);
						}
					}
					else
					{
						foreach (BookEntity book in _bookService
							.GetAll()
							.Where(book => book.Title.Contains(SearchLineText)))
						{
							_books.Add(book);
						}
					}
				}
				OnPropertyChanged(nameof(Books));
				if (Books.Count == 0)
				{
					NotFoundBook = Visibility.Visible;
					BooksList = Visibility.Hidden;
				}
			}
		}
		
		public ICommand CartCommand { get; }
		public ICommand WishListCommand { get; }
		public ICommand OrdersHistoryCommand { get; }
		public ICommand ProfileCommand { get; }
		public ICommand TakeSelectedCommand { get; }
		public ICommand BackCommand { get; }
		public ICommand ClearFiltersCommand { get; }
	}
}

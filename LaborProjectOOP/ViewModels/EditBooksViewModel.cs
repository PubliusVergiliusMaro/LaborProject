using LaborProjectOOP.Constants.Enums;
using LaborProjectOOP.Database.Models;
using LaborProjectOOP.Dekstop.Commands;
using LaborProjectOOP.Services.BookServices;
using LaborProjectOOP.Services.CatalogServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace LaborProjectOOP.Dekstop.ViewModels
{
	public class EditBooksViewModel : ViewModelBase
	{
		public EditBooksViewModel(IBookService bookService,ICatalogService catalogService)
		{
		     _bookService = bookService;
			_catalogService = catalogService;
			_books = new ObservableCollection<BookEntity>(_bookService.GetAll());
			_catalogs = new ObservableCollection<CatalogEntity>(_catalogService.GetAll())
			{
				new CatalogEntity { Name = "None" }
			};
			_sortingTypes = new ObservableCollection<BooksSorting>(Enum.GetValues(typeof(BooksSorting)).Cast<BooksSorting>().ToArray());
			
			DeleteSelectedBookCommand = new DelegateCommand(DeleteSelectedBook, CanDelete);
			AddBookToCatalogCommand = new DelegateCommand(AddBookToCatalog,CanAddToCatalog);
			RefreshBooksCommand = new DelegateCommand(RefreshBooks);
			//SelectedOrder = _orders.Where(c => c.Name == "None").FirstOrDefault();
		}

		private void RefreshBooks()
		{
			Books.Clear();
			foreach (BookEntity book in _bookService.GetAll())
				Books.Add(book);
		}

		private bool CanAddToCatalog()
		{
			return SelectedBook!= null && SelectedCatalog!=null;
		}

		private void AddBookToCatalog()
		{
			if(SelectedCatalog.Name == "None")
			{
				SelectedBook.CatalogFK = null;
				_bookService.Update(SelectedBook);
				MessageBox.Show("Succesfully added");
			}
			else
			{
				SelectedBook.CatalogFK = SelectedCatalog.Id;
				_bookService.Update(SelectedBook);
				MessageBox.Show("Succesfully added");
			}
		}

		private bool CanDelete()
		{
			return SelectedBook != null;
		}

		private void DeleteSelectedBook()
		{
			_bookService.Delete(SelectedBook.Id);
		}

		private ObservableCollection<BookEntity> _books;
		public ICollection<BookEntity> Books=> _books;
		private BookEntity _selectedBook;
		public BookEntity SelectedBook
		{
			get => _selectedBook;
			set
			{
				_selectedBook = value;
				OnPropertyChanged(nameof(SelectedBook));
			}
		}
		private ObservableCollection<BooksSorting> _sortingTypes;
		public ICollection<BooksSorting> SortingTypes => _sortingTypes;
		private BooksSorting _selectedSortType;
		public BooksSorting SelectedSortType
		{
			get => _selectedSortType;
			set
			{
				if (_selectedSortType != value)
				{
					_selectedSortType = value;
					OnPropertyChanged(nameof(SelectedSortType));
					switch (_selectedSortType)
					{
						case BooksSorting.None:
							Books.Clear();
							foreach (BookEntity book in _bookService.GetAll())
								Books.Add(book);
							break;
						default: break;
					}
				}
			}
		}
		private ObservableCollection<CatalogEntity> _catalogs;
		public ICollection<CatalogEntity> Catalogs => _catalogs;
		private CatalogEntity _selectedCatalog;
		public CatalogEntity SelectedCatalog
		{
			get => _selectedCatalog;
			set
			{
				_selectedCatalog = value;
				OnPropertyChanged(nameof(SelectedCatalog));
			}
		}
		private readonly IBookService _bookService;
		private readonly ICatalogService _catalogService;
		public ICommand DeleteSelectedBookCommand { get; }
		public ICommand AddBookToCatalogCommand { get; }
		public ICommand RefreshBooksCommand { get; }
	}
}

using LaborProjectOOP.Constants.Enums;
using LaborProjectOOP.Database.Models;
using LaborProjectOOP.Dekstop.Commands;
using LaborProjectOOP.Services.AuthorServices;
using LaborProjectOOP.Services.BookServices;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace LaborProjectOOP.Dekstop.ViewModels
{
	public class AddBookViewModel : ViewModelBase
	{
		public AddBookViewModel(IAuthorService authorService, IBookService bookService)
		{
			_authorService = authorService;
			_bookService = bookService;          
            athrFromDb = _authorService.GetAll();
            _authors = new ObservableCollection<AuthorEntity>(athrFromDb);
			_genres = new ObservableCollection<BookGenreTypes>(Enum.GetValues(typeof(BookGenreTypes)).Cast<BookGenreTypes>().ToArray());
			BookImagePath = DefaultImagePath;
			_selectedGenres = new ObservableCollection<BookGenreTypes>();
			AddImageCommand = new DelegateCommand(AddImage);
            AddBookCommand = new DelegateCommand(AddBook, CanAddBook);
            AddGenreCommand = new DelegateCommand(AddGenre, CanAddGenre);
			UpdateAuthorsCommand = new DelegateCommand(UpdateAuthors);

        }

        private void UpdateAuthors()
        {
            athrFromDb = _authorService.GetAll();
			_authors.Clear();
			foreach(AuthorEntity author in athrFromDb)
			{
				_authors.Add(author);
			}
        }
        private void AddGenre()
		{
			SelectedGenres.Add(SelectedGenre);
		}

		private bool CanAddGenre()
		{
			return 
				!string.IsNullOrEmpty(SelectedGenre.ToString()) &&
				!SelectedGenres.Contains(SelectedGenre);
		}

		private bool CanAddBook()
		{
			return
				!string.IsNullOrEmpty(Title) &&
				!string.IsNullOrEmpty(Description) &&
				SelectedGenres.Count() > 0 &&
				Price > 0 &&
				SelectedAuthor!= null &&
				BookImagePath != DefaultImagePath;
		}

		private async void AddBook()
		{
		    await _bookService.Create(
				new BookEntity
				{
					Title = Title,
					Description = Description,
					Price = Price,
					AuthorFK = SelectedAuthor.Id,
					Genres = SelectedGenres.ToList(),
					ImagePath = BookImagePath
				});
			MessageBox.Show("Succesfully create a new Book");
			Title = "";
			Description = "";
			Price= 0;
			BookImagePath = DefaultImagePath;
			SelectedAuthor = null;
			_selectedGenre = BookGenreTypes.None;
			_selectedGenres.Clear();
		}

		private void AddImage()
		{
			OpenFileDialog openFileDialog = new()
			{
				Multiselect = false,
				Filter = "Images|*.png;*.jpg"
			};
			openFileDialog.ShowDialog();

			if (openFileDialog.CheckFileExists && !string.IsNullOrEmpty(openFileDialog.FileName))
			{
				BitmapImage image = new(new Uri(openFileDialog.FileName));
				BookImagePath = openFileDialog.FileName;				
			}
			else
			{
				BookImagePath = DefaultImagePath;
			}
		}

		private string _title;
		public string Title
		{
			get => _title;
			set
			{
				_title = value;
				OnPropertyChanged(nameof(Title));
			}
		}
		private string _description;
		public string Description
		{
			get =>	_description;
			set
			{
				_description= value;
				OnPropertyChanged(nameof(Description));
			}
		}
		private double _price;
		public double Price
		{
			get => _price;
			set
			{
				_price = value;
				OnPropertyChanged(nameof(Price));
			}
		}
		private string _bookImagePath;
		public string BookImagePath
		{
			get => _bookImagePath;
			set
			{
				_bookImagePath = value;
				OnPropertyChanged(nameof(BookImagePath));
			}
		}
		private string DefaultImagePath = @"/Images/PagesIcons/SelectSomeImage.jpg";
		private static ObservableCollection<BookGenreTypes> _genres;
		public static IEnumerable<BookGenreTypes> Genres => _genres;
		private static BookGenreTypes _selectedGenre;
		public BookGenreTypes SelectedGenre
		{
			get => _selectedGenre;
			set
			{
				_selectedGenre = value;
				OnPropertyChanged(nameof(SelectedGenre));
			}
		}
		private static ObservableCollection<AuthorEntity> _authors;
		public static IEnumerable<AuthorEntity> Authors => _authors;
		private static AuthorEntity _selectedAuthor;
		public AuthorEntity SelectedAuthor
		{
			get => _selectedAuthor;
			set
			{
				_selectedAuthor = value;
				OnPropertyChanged(nameof(SelectedAuthor));
			}
		}
		
		private static ObservableCollection<BookGenreTypes> _selectedGenres;
		public static ICollection<BookGenreTypes> SelectedGenres => _selectedGenres;
		private static ICollection<AuthorEntity> athrFromDb;
		public ICommand AddBookCommand { get; private set; }
		public ICommand AddImageCommand { get; private set; }
		public ICommand AddGenreCommand { get; private set; }
        public ICommand UpdateAuthorsCommand { get; private set; }

        private readonly IBookService _bookService;
		private readonly IAuthorService _authorService;
	}
}

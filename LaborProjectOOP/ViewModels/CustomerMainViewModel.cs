using LaborProjectOOP.Constants.Enums;
using LaborProjectOOP.Dekstop.Commands;
using LaborProjectOOP.Services.CatalogServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace LaborProjectOOP.Dekstop.ViewModels
{
	public class CustomerMainViewModel : ViewModelBase
	{
		public CustomerMainViewModel(ICatalogService catalogService)
		{
			CartCommand = new DelegateCommand(OpenCart);
			WishListCommand = new DelegateCommand(OpenWishList);
			OrdersHistoryCommand = new DelegateCommand(OpenOrdersHistory);
			ProfileCommand = new DelegateCommand(OpenProfile);
			TakeSelectedCommand = new DelegateCommand(TakeSelected);
			BackCommand = new DelegateCommand(Back);
			ClearFiltersCommand = new DelegateCommand(ClearFilters);
			//_catalogs = //catalogService.GetAll();
			//_genres = Enum.GetValues(typeof(BookGenreTypes)).Cast<BookGenreTypes>();
			_genres = new ObservableCollection<BookGenreTypes>(Enum.GetValues(typeof(BookGenreTypes)).Cast<BookGenreTypes>().ToArray());

		}

		private void ClearFilters()
		{
			throw new NotImplementedException();
		}

		private void Back()
		{
			throw new NotImplementedException();
		}

		private void OpenProfile()
		{
			throw new NotImplementedException();
		}

		private void OpenOrdersHistory()
		{
			throw new NotImplementedException();
		}

		private void OpenWishList()
		{
			throw new NotImplementedException();
		}

		private void OpenCart()
		{
			throw new NotImplementedException();
		}

		private void TakeSelected()
		{

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
				_searchLineText = value;
				OnPropertyChanged(nameof(SearchLineText));
			}
		}
		public ICommand CartCommand { get; }
		public ICommand WishListCommand { get; }
		public ICommand OrdersHistoryCommand { get; }
		public ICommand ProfileCommand { get; }
		public ICommand TakeSelectedCommand { get; }
		public ICommand BackCommand { get; }
		public ICommand ClearFiltersCommand { get; }

		private static ObservableCollection<CatalogViewModel> _catalogs;
		public static IEnumerable<CatalogViewModel> Catalogs => _catalogs;
		public CatalogViewModel _selectedCatalog;
		public CatalogViewModel SelectedCatalog
		{
			get => _selectedCatalog;
			set
			{
				_selectedCatalog = value;
				OnPropertyChanged(nameof(SelectedCatalog));
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
				_selectedGenre = value;
				OnPropertyChanged(nameof(SelectedGenre));
			}
		}
	}
}

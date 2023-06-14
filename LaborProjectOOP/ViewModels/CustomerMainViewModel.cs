using LaborProjectOOP.Constants.Enums;
using LaborProjectOOP.Database.Models;
using LaborProjectOOP.Dekstop.Commands;
using LaborProjectOOP.Dekstop.NavigationServices.Stores;
using LaborProjectOOP.Services.AuthorServices;
using LaborProjectOOP.Services.BookServices;
using LaborProjectOOP.Services.CartListServices;
using LaborProjectOOP.Services.CatalogServices;
using LaborProjectOOP.Services.CustomerServices;
using LaborProjectOOP.Services.LibrarianServices;
using LaborProjectOOP.Services.OrderHistoryServices;
using LaborProjectOOP.Services.WishListServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LaborProjectOOP.Dekstop.ViewModels
{
    public class CustomerMainViewModel : ViewModelBase
    {
        private readonly CustomerEntity _currentCustomer;
        private readonly IBookService _bookService;
        private readonly ICatalogService _catalogService;
        private readonly ICustomerService _customerService;
        private readonly ILibrarianService _librarianService;
        private readonly IAuthorService _authorService;
        private readonly IWishListService _wishListService;
        private readonly ICartListService _cartListService;
        private readonly IOrderService _orderService;
        private readonly NavigationStore _navigationStore;
        private readonly bool _IsAdmin;
        public CustomerMainViewModel(NavigationStore navigationStore, CustomerEntity currentCustomer, bool IsAdmin, ICatalogService catalogService, IBookService bookService, ICustomerService customerService, ILibrarianService librarianService, ICartListService cartListService, IWishListService wishListService, IAuthorService authorService, IOrderService orderService)
        {
            _currentCustomer = currentCustomer;
            _customerService = customerService;
            _librarianService = librarianService;
            _navigationStore = navigationStore;
            _cartListService = cartListService;
            _wishListService = wishListService;
            _catalogService = catalogService;
            _bookService = bookService;
            _authorService = authorService;
            _orderService = orderService;
            _IsAdmin = IsAdmin;
            bksFromDb = _bookService.GetAll();
            
            _catalogs = new ObservableCollection<CatalogEntity>(_catalogService.GetAll())
            {
                new CatalogEntity { Name = "None" }
            };
            _books = new ObservableCollection<BookEntity>(bksFromDb);
            bksFromDb = _books.ToList();
            BooksInCart = _currentCustomer.CartList.Count;
            BooksInWishList = _currentCustomer.WishList.Count;
            SelectedGenre = BookGenreTypes.None;
            SelectedCatalog = _catalogs.Where(c => c.Name == "None").FirstOrDefault();
            SearchLineText = "";
            if (_IsAdmin)
                CustomerAvatarSource =  @"/Images/Avatars/adminAvatar.png";
            else
                CustomerAvatarSource = _currentCustomer.AvatarImagePath;
            NotFoundBook = Visibility.Hidden;
            BooksList = Visibility.Visible;

         
            _genres = new ObservableCollection<BookGenreTypes>(Enum.GetValues(typeof(BookGenreTypes)).Cast<BookGenreTypes>().ToArray());
            CartCommand = new DelegateCommand(OpenCart);
            WishListCommand = new DelegateCommand(OpenWishList);
            OrdersHistoryCommand = new DelegateCommand(OpenOrdersHistory);
            ProfileCommand = new DelegateCommand(OpenProfile, CanOpe);
            MakeOrderCommand = new DelegateCommand(MakeOrder, CanOpen);
            BackCommand = new DelegateCommand(Back);
            ClearFiltersCommand = new DelegateCommand(ClearFilters);
        }
        private bool CanOpen()=> _currentCustomer.CartList.Count != 0 && !_IsAdmin;
        private bool CanOpe() => !_IsAdmin;

        private void ShowBookInfo() => _navigationStore.CurrentViewModel = new BookPageViewModel(SelectedBook, _navigationStore, _currentCustomer, _IsAdmin, _catalogService, _bookService, _customerService, _librarianService, _cartListService, _wishListService, _authorService, _orderService);

        private void ClearFilters()
        {
            SelectedCatalog = _catalogs.Where(c => c.Name == "None").FirstOrDefault();
            SelectedGenre = 0;
        }

        private void Back()
        {
            if (_IsAdmin)
            {
                _navigationStore.CurrentViewModel = new AdminMenuViewModel(_navigationStore, _currentCustomer, _catalogService, _bookService, _customerService, _librarianService, _cartListService, _wishListService, _authorService, _orderService);
            }
            else
                _navigationStore.CurrentViewModel = new LoginViewModel(_navigationStore, _customerService, _librarianService, _wishListService, _cartListService, _catalogService, _bookService, _authorService, _orderService);

        }//{
         //	MessageBox.Show("We go Back");
         //}

        private void OpenProfile() => _navigationStore.CurrentViewModel = new EditProfileViewModel(_navigationStore, _currentCustomer, _catalogService, _bookService, _customerService, _librarianService, _cartListService, _wishListService, _authorService, _orderService);
        //{
        //	MessageBox.Show("We go to Profile");
        //}

        private void OpenOrdersHistory() => _navigationStore.CurrentViewModel = new CustomerOrderHistoryViewModel(_navigationStore, _currentCustomer, _IsAdmin, _catalogService, _bookService, _customerService, _librarianService, _cartListService, _wishListService, _authorService, _orderService);
        //{
        //	MessageBox.Show("We go to History");
        //}

        private void OpenWishList() => _navigationStore.CurrentViewModel = new CustomerActivitiesViewModel(_navigationStore, _currentCustomer, _IsAdmin, CustomerActivitiesInfoType.WishList, _catalogService, _bookService, _customerService, _librarianService, _cartListService, _wishListService, _authorService, _orderService);
        //{
        //	MessageBox.Show("We go to wishList"); ;
        //}

        private void OpenCart() => _navigationStore.CurrentViewModel = new CustomerActivitiesViewModel(_navigationStore, _currentCustomer, _IsAdmin, CustomerActivitiesInfoType.CartList, _catalogService, _bookService, _customerService, _librarianService, _cartListService, _wishListService, _authorService, _orderService);


        private void MakeOrder()
        {
            if (_currentCustomer.CartList.Count != 0)
            {
                _navigationStore.CurrentViewModel = new MakeOrderViewModel(_navigationStore, _currentCustomer, _catalogService, _bookService, _customerService, _librarianService, _cartListService, _wishListService, _authorService, _orderService);
            }
            else MessageBox.Show("Error");
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
                    if (SelectedBook != null)
                        _navigationStore.CurrentViewModel = new BookPageViewModel(SelectedBook, _navigationStore, _currentCustomer, _IsAdmin, _catalogService, _bookService, _customerService, _librarianService, _cartListService, _wishListService, _authorService, _orderService);
                }
            }
        }
        private async void SetBooksToListBox()
        {
            //Update the _books collection with the filtered results
            if (_books != null && SelectedGenre != null && SelectedCatalog != null && SearchLineText != null)
            {
                _books.Clear();
                if (SelectedGenre != BookGenreTypes.None)
                {
                    if (SelectedCatalog.Name != "None")
                    {
                        foreach (BookEntity book in bksFromDb
                            .Where(book => book.Catalog == SelectedCatalog && book.Genres.Contains(SelectedGenre))
                            .Where(book => book.Title.Contains(SearchLineText)))
                        {
                            _books.Add(book);
                        }
                    }
                    else
                    {
                        foreach (BookEntity book in bksFromDb
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
                        foreach (BookEntity book in bksFromDb
                            .Where(book => book.Catalog == SelectedCatalog)
                            .Where(book => book.Title.Contains(SearchLineText)))
                        {
                            _books.Add(book);
                        }
                    }
                    else
                    {
                        foreach (BookEntity book in bksFromDb
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
        private static ICollection<BookEntity> bksFromDb;
        public ICommand CartCommand { get; private set; }
        public ICommand WishListCommand { get; private set; }
        public ICommand OrdersHistoryCommand { get; private set; }
        public ICommand ProfileCommand { get; private set; }
        public ICommand MakeOrderCommand { get; private set; }
        public ICommand BackCommand { get; private set; }
        public ICommand ClearFiltersCommand { get; private set; }
    }
}

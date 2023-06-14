using LaborProjectOOP.Database.Models;
using LaborProjectOOP.Dekstop.Commands;
using LaborProjectOOP.Dekstop.NavigationServices.Stores;
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
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace LaborProjectOOP.Dekstop.ViewModels
{
	class SignUpViewModel : ViewModelBase
	{
		private readonly IBookService _bookService;
		private readonly ICatalogService _catalogService;
		private readonly ICustomerService _customerService;
		private readonly ILibrarianService _librarianService;
		private readonly IAuthorService _authorService;
		private readonly IWishListService _wishListService;
		private readonly ICartListService _cartListService;
		private readonly IOrderService _orderService;
		private readonly NavigationStore _navigationStore;
		public SignUpViewModel(NavigationStore navigationStore, ICustomerService customerService,
			ILibrarianService librarianService, IWishListService wishListService, ICartListService cartListService, ICatalogService catalogService, IBookService bookService,IAuthorService authorService, IOrderService orderService)
		{
			_customerService = customerService;
			_librarianService = librarianService;
			_navigationStore = navigationStore;
			_cartListService = cartListService;
			_wishListService = wishListService;
			_catalogService = catalogService;
			_bookService = bookService;
			_authorService = authorService;
			_orderService = orderService;
			CancelCommand = new DelegateCommand(Cancel);
			SaveCommand = new DelegateCommand(Save, CanSave);
			SelectAvtrImgCommand = new DelegateCommand(SelectAvatarImage);
			DefaultAvatarCommand = new DelegateCommand(SelectDefaultAvatarImage);
			AvatarImage = SelectAvatarImagePath;
			SelectedAvatarPath = SelectAvatarImagePath;
		}

		private void SelectDefaultAvatarImage()
		{
			if (UseDefaultAvatar == true)
			{
				AvatarImage = SelectAvatarImagePath;
				SelectedAvatarPath = SelectAvatarImagePath;
				CheckedDefaultAvatar = false;
				UseDefaultAvatar = false;
			}
			else
			{
				AvatarImage = DefaultAvatarPath;
				SelectedAvatarPath = DefaultAvatarPath;
				CheckedDefaultAvatar = true;
                UseDefaultAvatar = true;
			}
		}

		private void SelectAvatarImage()
		{
			CheckedDefaultAvatar = false;
			OpenFileDialog openFileDialog = new()
			{
				Multiselect = false,
				Filter = "Images|*.png;*.jpg"
			};
			openFileDialog.ShowDialog();

			if (openFileDialog.CheckFileExists && !string.IsNullOrEmpty(openFileDialog.FileName))
			{
			  BitmapImage image = new(new Uri(openFileDialog.FileName));
			  AvatarImage = openFileDialog.FileName;
			  SelectedAvatarPath = openFileDialog.FileName;
			}
			else
			{
				AvatarImage = SelectAvatarImagePath;
				SelectedAvatarPath = SelectAvatarImagePath;
			}
		}

		private bool CanSave()
		{
			//Add Validation On Existing 
			return
				!string.IsNullOrEmpty(Login) &&
				!string.IsNullOrEmpty(Password) &&
				!string.IsNullOrEmpty(Email) &&
				!string.IsNullOrEmpty(Phone)&&
				SelectedAvatarPath != SelectAvatarImagePath;
		}

		private async void Save()
		{
            await _customerService.Create(new CustomerEntity
	 		{
				Login = Login,
				Password = HashService.GetMD5Hash(Password),
				Email = Email,
				Phone = Phone,
				AvatarImagePath = SelectedAvatarPath
     		});
			MessageBox.Show("Succesfully Added");
		    _navigationStore.CurrentViewModel = new LoginViewModel(_navigationStore, _customerService, _librarianService, _wishListService, _cartListService, _catalogService, _bookService, _authorService, _orderService);
		}

		private void Cancel()
		{
			_navigationStore.CurrentViewModel = new LoginViewModel(_navigationStore, _customerService, _librarianService, _wishListService, _cartListService, _catalogService, _bookService, _authorService, _orderService);
		}

		private string _login;
		public string Login
		{
			get => _login;
			set
			{
				_login = value;
				OnPropertyChanged(nameof(Login));
			}
		}
		private string _password;
		public string Password
		{
			get => _password;
			set
			{
				_password = value;
				OnPropertyChanged(nameof(Password));
			}
		}
		private string _phone;
		public string Phone
		{
			get => _phone;
			set
			{
				_phone = value;
				OnPropertyChanged(nameof(Phone));
			}
		}
		private string _email;
		public string Email
		{
			get => _email;
			set
			{
				_email = value;
				OnPropertyChanged(nameof(Email));
			}
		}
		private string _avatarImage;
		public string AvatarImage
		{
			get => _avatarImage;
			set
			{
				_avatarImage = value;
				OnPropertyChanged(nameof(AvatarImage));
			}
		}
		private bool _checkedDefaultAvatar;
		public bool CheckedDefaultAvatar 
		{
			get => _checkedDefaultAvatar;
			set
			{
				_checkedDefaultAvatar = value;
				OnPropertyChanged(nameof(CheckedDefaultAvatar));
			}
		}
		private bool UseDefaultAvatar { get; set; }
		private string SelectAvatarImagePath { get; } = @"/Images/PagesIcons/SelectSomeImage.jpg";
		private string DefaultAvatarPath { get; } = @"/Images/PagesIcons/defaultAvatarIcon.png";
		private string SelectedAvatarPath { get; set; }
		public ICommand CancelCommand { get; }
		public ICommand SaveCommand { get; }
		public ICommand SelectAvtrImgCommand { get; }
		public ICommand DefaultAvatarCommand { get; }
	}
}

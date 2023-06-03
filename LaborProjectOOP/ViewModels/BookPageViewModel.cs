using LaborProjectOOP.Database.Models;
using LaborProjectOOP.Dekstop.Commands;
using LaborProjectOOP.Services.BookServices;
using LaborProjectOOP.Services.CartListServices;
using LaborProjectOOP.Services.CustomerServices;
using LaborProjectOOP.Services.WishListServices;
using System;
using System.Windows;
using System.Windows.Input;

namespace LaborProjectOOP.Dekstop.ViewModels
{
	public class BookPageViewModel : ViewModelBase
	{
		public BookPageViewModel(BookEntity selectedBook, CustomerEntity currentCustomer, ICartListService cartListService,IWishListService wishListService) 
		{
			_selectedBook = selectedBook;
			_currentCustomer = currentCustomer;
			_cartListService = cartListService;
			_wishListService = wishListService;
			BackCommand = new DelegateCommand(Back);
			AddToCartCommand = new DelegateCommand(AddToCart,CanAddToCart);
			AddToWishListCommand = new DelegateCommand(AddToWishList,CanAddToWishList);

		}

		private bool CanAddToWishList()
		{
			return !_wishListService.CheckIfExist(_currentCustomer.Id, _selectedBook.Id);
		}

		private bool CanAddToCart()
		{
			return !_cartListService.CheckIfExist(_currentCustomer.Id, _selectedBook.Id);
		}
		
		private void AddToWishList()
		{
			_wishListService.Create(new WishListEntity
			{
				CustomerFK = _currentCustomer.Id,
				BookFK = _selectedBook.Id
			});
			MessageBox.Show("Succesfully added");
		}

		private void AddToCart()
		{
			_cartListService.Create(new CartListEntity
			{
				CustomerFK = _currentCustomer.Id,
				BookFK = _selectedBook.Id
			});
			MessageBox.Show("Succesfully added");

		}

		private void Back()
		{
			MessageBox.Show("We go Back");
		}

		public ICommand BackCommand { get; }
		public ICommand AddToCartCommand { get; }
		public ICommand AddToWishListCommand { get; }
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
		private readonly CustomerEntity _currentCustomer;
		private readonly ICartListService _cartListService;
		private readonly IWishListService _wishListService;
	}
}

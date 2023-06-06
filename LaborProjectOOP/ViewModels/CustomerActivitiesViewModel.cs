using LaborProjectOOP.Constants.Enums;
using LaborProjectOOP.Database.Models;
using LaborProjectOOP.Dekstop.Commands;
using LaborProjectOOP.Services.CartListServices;
using LaborProjectOOP.Services.CustomerServices;
using LaborProjectOOP.Services.WishListServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace LaborProjectOOP.Dekstop.ViewModels
{
	public class CustomerActivitiesViewModel : ViewModelBase
	{
		public CustomerActivitiesViewModel(CustomerEntity curentCustomer,IWishListService wishListService,ICartListService cartListService, CustomerActivitiesInfoType customerActivitiesInfoType)//ICustomerService customerService)
		{
			_currentCustomer = curentCustomer;
			_books = new ObservableCollection<BookEntity>();
			if (customerActivitiesInfoType == CustomerActivitiesInfoType.WishList)
			{
				List<WishListEntity> customerWishLists = wishListService.GetWishListByCustomerId(_currentCustomer.Id);
				foreach (WishListEntity wishBook in customerWishLists)
					_books.Add(wishBook.Book);
			}
			else
			{
				List<CartListEntity> customerCartLists = cartListService.GetCartListByCustomerId(_currentCustomer.Id);
				foreach (CartListEntity cartBook in customerCartLists)
					_books.Add(cartBook.Book);
			}
			
			if (_books.Count > 0)
			{
				_bookBlocks = new ObservableCollection<BookBlockInfoViewModel>();
				foreach (BookEntity book in _books)
				{
					_bookBlocks.Add(new BookBlockInfoViewModel(book, _currentCustomer));
				}
				EmptyWishList = Visibility.Hidden;
				EmptyCartList = Visibility.Hidden;
			}
			else
			{
				if(customerActivitiesInfoType == CustomerActivitiesInfoType.WishList) 
				{
					EmptyWishList = Visibility.Visible;
					EmptyCartList = Visibility.Hidden;
				}
				else
				{
					EmptyWishList = Visibility.Hidden;
					EmptyCartList = Visibility.Visible;
				}
			}
			BackCommand = new DelegateCommand(Back);
		}
		
		private void Back()
		{
			MessageBox.Show("Go Back");
		}

		private ObservableCollection<BookEntity> _books;
		public ICollection<BookEntity> Books => _books;
		private BookEntity _selectedBook;
		public BookEntity SelectedBook
		{
			get=>_selectedBook;
			set
			{
				_selectedBook = value;
				OnPropertyChanged(nameof(SelectedBook));
			}
		}
		private ObservableCollection<BookBlockInfoViewModel> _bookBlocks;
		public ICollection<BookBlockInfoViewModel> BookBlocks => _bookBlocks;
		private CustomerEntity _currentCustomer;
		public CustomerEntity CurrentCustomer
		{
			get => _currentCustomer;
			set
			{
				_currentCustomer = value;
				OnPropertyChanged(nameof(CurrentCustomer));
			}
		}
		private Visibility _emptyCartList;
		public Visibility EmptyCartList
		{
			get => _emptyCartList;
			set
			{
				_emptyCartList = value;
				OnPropertyChanged(nameof(EmptyCartList));
			}
		}
		private Visibility _emptyWishList;
		public Visibility EmptyWishList
		{
			get => _emptyWishList;
			set
			{
				_emptyWishList = value;
				OnPropertyChanged(nameof(EmptyWishList));
			}
		}
		public ICommand BackCommand { get; }
	}
}

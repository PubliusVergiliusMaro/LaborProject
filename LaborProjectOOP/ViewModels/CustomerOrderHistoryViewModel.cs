using LaborProjectOOP.Constants.Enums;
using LaborProjectOOP.Database.Models;
using LaborProjectOOP.Dekstop.Commands;
using LaborProjectOOP.Services.CartListServices;
using LaborProjectOOP.Services.WishListServices;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using LaborProjectOOP.Services.OrderHistoryServices;

namespace LaborProjectOOP.Dekstop.ViewModels
{
	public class CustomerOrderHistoryViewModel : ViewModelBase
	{
		public CustomerOrderHistoryViewModel(CustomerEntity curentCustomer)//ICustomerService customerService)
		{
			_currentCustomer = curentCustomer;
			_books = new ObservableCollection<BookEntity>();
			ICollection<OrderEntity> orders= _currentCustomer.Orders;
			if (orders != null)
			{
				foreach (OrderEntity order in orders)
					_books.Add(order.Book);
				if (_books.Count <= 0)
				{
					IsOrdersListEmpty = Visibility.Visible;
				}
				else IsOrdersListEmpty = Visibility.Hidden;
			}
			else
			{
				MessageBox.Show("Error");
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
			get => _selectedBook;
			set
			{
				_selectedBook = value;
				OnPropertyChanged(nameof(SelectedBook));
			}
		}
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
		private Visibility _isOrdersLsitEmpty;
		public Visibility IsOrdersListEmpty
		{
			get => _isOrdersLsitEmpty;
			set
			{
				_isOrdersLsitEmpty = value;
				OnPropertyChanged(nameof(IsOrdersListEmpty));
			}
		}
		public ICommand BackCommand { get; }
		public ICommand RemoveBookCommand { get; }
	}
}


using LaborProjectOOP.Database.Models;
using LaborProjectOOP.Dekstop.Commands;
using LaborProjectOOP.Services.BookServices;
using LaborProjectOOP.Services.CartListServices;
using LaborProjectOOP.Services.CustomerServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.RightsManagement;
using System.Windows;
using System.Windows.Input;

namespace LaborProjectOOP.Dekstop.ViewModels
{
	public class MakeOrderViewModel : ViewModelBase
	{
		public MakeOrderViewModel(CustomerEntity currentCustomer, IBookService bookService,ICartListService cartListService,ICustomerService customerService)//,ICartListService cartService,IBookService bookService)
		{
			_currentCustomer = currentCustomer;
			_bookService = bookService;
			_cartListService = cartListService;
			_customerService = customerService;
			_books = new ObservableCollection<BookEntity>();
			foreach(var cart in _currentCustomer.CartList)
			{
				_books.Add(cart.Book);
			}
			CancelCommand = new DelegateCommand(Cancel);
			EditCartCommand = new DelegateCommand(EditCart);
			MakeOrderCommand = new DelegateCommand(MakeOrder);
		}

		private void MakeOrder()
		{
			_bookService.PurchaseBooks(_currentCustomer.Id, _books);
			MessageBox.Show("Succesfuly created");
			foreach (CartListEntity customerCart in _currentCustomer.CartList.ToList())
			{
				_cartListService.Delete(customerCart.Id);
			}
			_customerService.Update(_currentCustomer);
			//Navigation 
		}

		private void EditCart()
		{
			MessageBox.Show("Go To EditCart");
		}

		private void Cancel()
		{
			MessageBox.Show("Cancel");
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
		private ObservableCollection<CartListEntity> _cartList;
		public IEnumerable<CartListEntity> CartList => _cartList;
		private ObservableCollection<BookEntity> _books;
		public ICollection<BookEntity> Books => _books;
		private readonly IBookService _bookService;
		private readonly ICartListService _cartListService;
		private readonly ICustomerService _customerService;
		public ICommand CancelCommand { get; }
		public ICommand EditCartCommand { get; }
		public ICommand MakeOrderCommand { get; }
	}
}

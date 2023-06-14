using LaborProjectOOP.Constants.Enums;
using LaborProjectOOP.Database.Models;
using LaborProjectOOP.Dekstop.Commands;
using LaborProjectOOP.Services.CatalogServices;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using LaborProjectOOP.Services.CustomerServices;
using System.Linq;
using System.Windows;
using System.Threading.Tasks;

namespace LaborProjectOOP.Dekstop.ViewModels
{
	public class EditCustomersViewModel : ViewModelBase
	{
		public EditCustomersViewModel(ICustomerService customerService)
		{

			_customerService = customerService;
            cstmrsFromDb = _customerService.GetAll();
            _customers = new ObservableCollection<CustomerEntity>(cstmrsFromDb);
			_sortingTypes = new ObservableCollection<CustomersSorting>(Enum.GetValues(typeof(CustomersSorting)).Cast<CustomersSorting>().ToArray());
          
			DeleteSelectedCustomerCommand = new DelegateCommand(DeleteSelectedCustomer, IsSelected);
			BannedSelectedCustomerCommand = new DelegateCommand(BanSelectedCustomer, IsSelected);
			RefreshCustomersCommand = new DelegateCommand(RefreshCustomers);
		}
		
		private async void BanSelectedCustomer()
		{
			if (_selectedCustomer.IsBanned)
			{
				_selectedCustomer.IsBanned = false;
			}
			else
				_selectedCustomer.IsBanned = true;
			await _customerService.Update(_selectedCustomer);
			MessageBox.Show("Succesfully bannde. Update list");
		}

		private async void RefreshCustomers()
		{
            cstmrsFromDb = _customerService.GetAll();
            Customers.Clear();
			foreach (CustomerEntity customer in cstmrsFromDb)
				Customers.Add(customer);
		}

		private bool IsSelected()
		{
			return SelectedCustomer != null;
		}

		private async void DeleteSelectedCustomer()
		{
            await _customerService.Delete(SelectedCustomer.Id);
            MessageBox.Show("Succesfully deleted. Update list.");
        }

		private ObservableCollection<CustomersSorting> _sortingTypes;
		public ICollection<CustomersSorting> SortingTypes => _sortingTypes;
		private CustomersSorting _selectedSortType;
		public CustomersSorting SelectedSortType
		{
			get => _selectedSortType;
			set
			{
				if (_selectedSortType != value)
				{
					
					_selectedSortType = value;
					OnPropertyChanged(nameof(SelectedSortType));
					Customers.Clear();
					switch (_selectedSortType)
					{
						case CustomersSorting.None:
							foreach (CustomerEntity customer in cstmrsFromDb)
								Customers.Add(customer);
							break;
						case CustomersSorting.BannedCustomers:
							foreach(CustomerEntity customer in cstmrsFromDb.Where(customer => customer.IsBanned))
							{
								Customers.Add(customer);
							}
							break;
						default: break;
					}
				}
			}
		}
		private ObservableCollection<CustomerEntity> _customers;
		public ICollection<CustomerEntity> Customers => _customers;
		private CustomerEntity _selectedCustomer;
		public CustomerEntity SelectedCustomer
		{
			get => _selectedCustomer;
			set
			{
				_selectedCustomer = value;
				OnPropertyChanged(nameof(SelectedCustomer));
			}
		}
		private readonly ICustomerService _customerService;
		private static ICollection<CustomerEntity> cstmrsFromDb;
		public ICommand DeleteSelectedCustomerCommand { get; }
		public ICommand BannedSelectedCustomerCommand { get; }
		public ICommand RefreshCustomersCommand { get; }
	}
}

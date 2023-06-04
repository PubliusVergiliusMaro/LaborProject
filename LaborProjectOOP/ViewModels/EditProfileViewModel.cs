using LaborProjectOOP.Database.Models;
using LaborProjectOOP.Dekstop.Commands;
using LaborProjectOOP.Services.CustomerServices;
using LaborProjectOOP.Services.Helpers;
using System;
using System.Windows;
using System.Windows.Input;

namespace LaborProjectOOP.Dekstop.ViewModels
{
	public class EditProfileViewModel : ViewModelBase
	{
		public EditProfileViewModel(CustomerEntity currentCustomer,ICustomerService customerService) 
		{
		    _currentCustomer= currentCustomer;
			_customerService = customerService;
			CancelCommand = new DelegateCommand(Cancel);
			SaveCommand = new DelegateCommand(Save, CanSave);
			
		}

		private bool CanSave()
		{
			return !string.IsNullOrEmpty(_currentCustomer.Login) &&
				!string.IsNullOrEmpty(_currentCustomer.Email) &&
				!string.IsNullOrEmpty(_currentCustomer.Phone);

		}

		private void Save()
		{
			// add data validation
			if (string.IsNullOrEmpty(_oldPassword))
			{
				_customerService.Update(_currentCustomer);
				MessageBox.Show("Succesfully updated");
			}
			else
			{
				if (HashService.GetMD5Hash(_oldPassword) == _currentCustomer.Password)
				{
					if (!string.IsNullOrEmpty(_newPassword))
					{
						_currentCustomer.Password = HashService.GetMD5Hash(_newPassword);
					_customerService.Update(_currentCustomer);
					MessageBox.Show("Succesfully updated");
					}
				}
				else
				{
					MessageBox.Show("Password is not right");
				}
			}
		}

		private void Cancel()
		{
			MessageBox.Show("Cancel");
		}

		public CustomerEntity _currentCustomer;
		public CustomerEntity CurrentCustomer
		{
			get => _currentCustomer;
			set
			{
				_currentCustomer = value;
				OnPropertyChanged(nameof(CurrentCustomer));
			}
		}
		private string _oldPassword;
		public string OldPassword
		{
			get => _oldPassword;
			set
			{
				_oldPassword = value;
				OnPropertyChanged(nameof(OldPassword));
			}
		}
		private string _newPassword;
		public string NewPassword
		{
			get => _newPassword;
			set
			{
				_newPassword = value;
				OnPropertyChanged(nameof(NewPassword));
			}
		}
		private readonly ICustomerService _customerService;
		public ICommand CancelCommand { get; }
		public ICommand SaveCommand { get; }
	}
}

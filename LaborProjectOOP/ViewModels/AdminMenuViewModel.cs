using LaborProjectOOP.Dekstop.Commands;
using System;
using System.Windows;
using System.Windows.Input;

namespace LaborProjectOOP.Dekstop.ViewModels
{
	public class AdminMenuViewModel : ViewModelBase
	{
		public AdminMenuViewModel() 
		{
			EnterAsAdminCommand = new DelegateCommand(EnterAsAdmin);
			EnterAsCustomerCommand = new DelegateCommand(EnterAsCustomer);
			BackCommand = new DelegateCommand(Back);
		}

		private void Back()
		{
			MessageBox.Show("Go Back");
		}

		private void EnterAsCustomer()
		{
			MessageBox.Show("Go As a Customer");
		}

		private void EnterAsAdmin()
		{
			MessageBox.Show("Go As a Admin");
		}

		public ICommand EnterAsAdminCommand { get; }
		public ICommand EnterAsCustomerCommand { get; }
		public ICommand BackCommand { get; }
	}
}

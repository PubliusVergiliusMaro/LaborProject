using LaborProjectOOP.Dekstop.Commands;
using LaborProjectOOP.Services.CustomerServices;
using System;
using System.Windows;
using System.Windows.Input;

namespace LaborProjectOOP.Dekstop.ViewModels
{
	public class AdminViewModel : ViewModelBase
	{
		public AdminViewModel(ICustomerService customerService) 
		{
			EditCustomersViewModel editCustomersViewModel = new EditCustomersViewModel(customerService);
			BackCommand = new DelegateCommand(Back);
		}

		private void Back()
		{
			MessageBox.Show("Back");
		}

		public ICommand BackCommand { get; }
	}
}

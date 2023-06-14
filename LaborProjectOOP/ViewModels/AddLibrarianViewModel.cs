using LaborProjectOOP.Database.Models;
using LaborProjectOOP.Dekstop.Commands;
using LaborProjectOOP.Services.LibrarianServices;
using System;
using System.Windows;
using System.Windows.Input;

namespace LaborProjectOOP.Dekstop.ViewModels
{
	public class AddLibrarianViewModel : ViewModelBase
	{
		public AddLibrarianViewModel(ILibrarianService librarianService)
		{
			_librarianService = librarianService;
			CreateLibrarian = new DelegateCommand(Create, CanCreate);
		}

		private async void Create()
		{
			await _librarianService.Create(new LibrarianEntity
			{
				Login = Login,
				Password = Password,
				Salary = Salary,
				WorkExperience = Experience,
				IsAdmin = IsAdmin,
			});
			MessageBox.Show("Successfully created");
			Login = "";
			Password = "";
			Salary = 0;
			Experience = 0;
			IsAdmin = false;
		}

		private bool CanCreate()
		{
			return !string.IsNullOrEmpty(Login) &&
				!string.IsNullOrEmpty(Password) &&
				Salary > 0 && Salary < 100000 &&
				Experience > 0 && Experience < 100;			
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
		private int _salary;
		public int Salary
		{
			get => _salary;
			set
			{
				_salary = value;
				OnPropertyChanged(nameof(Salary));
			}
		}
		private byte _experience;
		public byte Experience
		{
			get => _experience;
			set
			{
				_experience = value;
				OnPropertyChanged(nameof(Experience));
			}
		}
		private bool _isAdmin;
		public bool IsAdmin
		{
			get => _isAdmin;
			set
			{
				_isAdmin = value;
				OnPropertyChanged(nameof(IsAdmin));
			}
		}
		public ICommand CreateLibrarian { get; }
		private readonly ILibrarianService _librarianService;
	}
}

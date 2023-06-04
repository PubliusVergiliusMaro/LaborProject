using LaborProjectOOP.Dekstop.Commands;
using LaborProjectOOP.Services.AuthorServices;
using System;
using System.Windows;
using System.Windows.Input;

namespace LaborProjectOOP.Dekstop.ViewModels
{
	public class AddAuthorViewModel : ViewModelBase
	{
		public readonly IAuthorService _authorService;
		public AddAuthorViewModel(IAuthorService authorService)
		{
			_authorService = authorService;
			AddAuthorCommand = new DelegateCommand(AddAuthor);
		}

		private void AddAuthor()
		{
			MessageBox.Show("Author have ben added");
		}

		public string _name;
		public string Name
		{
			get => _name; 
			set 
			{ 
				_name = value; 
			    OnPropertyChanged(nameof(Name));
			}
		}

		public string _surname;
		public string Surname
		{ 
			get => _surname; 
			set
			{
				_surname = value;
				OnPropertyChanged(nameof(Surname));
			}
		}

		public ICommand AddAuthorCommand { get; }
	}
}

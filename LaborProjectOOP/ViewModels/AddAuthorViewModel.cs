using LaborProjectOOP.Database.Models;
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
			AddAuthorCommand = new DelegateCommand(AddAuthor,CanAdd);
		}

		private bool CanAdd()
		{
			return
				!string.IsNullOrEmpty(Name) &&
				!string.IsNullOrEmpty(Surname);
		}

		private void AddAuthor()
		{
			_authorService.Create(
				new AuthorEntity()
				{
					Name = Name,
					Surname = Surname,
				});
			MessageBox.Show("Author has been added");
			Name = "";
			Surname = "";
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

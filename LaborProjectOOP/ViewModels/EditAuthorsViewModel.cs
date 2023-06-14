using LaborProjectOOP.Constants.Enums;
using LaborProjectOOP.Database.Models;
using LaborProjectOOP.Dekstop.Commands;
using LaborProjectOOP.Services.AuthorServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LaborProjectOOP.Dekstop.ViewModels
{
	public class EditAuthorsViewModel : ViewModelBase
	{
		public EditAuthorsViewModel(IAuthorService authorService)
		{
			_authorService = authorService;
			athrsFromDb = _authorService.GetAll();
			_authors = new ObservableCollection<AuthorEntity>(athrsFromDb);
			_sortingTypes = new ObservableCollection<AuthorsSorting>(Enum.GetValues(typeof(AuthorsSorting)).Cast<AuthorsSorting>().ToArray());
			
			DeleteSelectedAuthorCommand = new DelegateCommand(DeleteSelectedAuthor,CanDelete);
			RefreshAuthorsCommand = new DelegateCommand(RefreshAuthors);	
		}
		
		private void RefreshAuthors()
		{  
            _authors.Clear();
            athrsFromDb = _authorService.GetAll();
			foreach(AuthorEntity author in athrsFromDb)
			{
				_authors.Add(author);
			}
        }

		private bool CanDelete()
		{
			return SelectedAuthor != null;
		}

		private async void DeleteSelectedAuthor()
		{
            await _authorService.Delete(SelectedAuthor.Id);
            MessageBox.Show("Succesfully deleted. Update list.");
        }

		private ObservableCollection<AuthorEntity> _authors;
		public ICollection<AuthorEntity> Authors => _authors;
		private readonly IAuthorService _authorService;
		private AuthorEntity _selectedAuthor;
		public AuthorEntity SelectedAuthor
		{
			get => _selectedAuthor;
			set
			{
				_selectedAuthor = value;
				OnPropertyChanged(nameof(SelectedAuthor));
			}
		}

		private ObservableCollection<AuthorsSorting> _sortingTypes;
		public IEnumerable<AuthorsSorting> SortingTypes => _sortingTypes;
		private AuthorsSorting _selectedSortType;
		public AuthorsSorting SelectedSortType
		{
			get => _selectedSortType;
			set
			{
				if (_selectedSortType != value)
				{
                    _selectedSortType = value;
					OnPropertyChanged(nameof(SelectedSortType));
					Authors.Clear();
                    switch (_selectedSortType)
					{
						case AuthorsSorting.None:
							foreach (AuthorEntity author in athrsFromDb)
								Authors.Add(author);
							break;
						default: break;
					}
				}
			}
		}
		private static ICollection<AuthorEntity> athrsFromDb;
		public ICommand DeleteSelectedAuthorCommand { get; }
		public ICommand RefreshAuthorsCommand { get; }
	}
}

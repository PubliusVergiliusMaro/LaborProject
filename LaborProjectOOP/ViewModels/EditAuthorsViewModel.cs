using LaborProjectOOP.Constants.Enums;
using LaborProjectOOP.Database.Models;
using LaborProjectOOP.Dekstop.Commands;
using LaborProjectOOP.Services.AuthorServices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace LaborProjectOOP.Dekstop.ViewModels
{
	public class EditAuthorsViewModel : ViewModelBase
	{
		public EditAuthorsViewModel(IAuthorService authorService)
		{
			_authorService = authorService;
			_authors = new ObservableCollection<AuthorEntity>(_authorService.GetAll());
			_sortingTypes = new ObservableCollection<AuthorsSorting>(Enum.GetValues(typeof(AuthorsSorting)).Cast<AuthorsSorting>().ToArray());
			DeleteSelectedAuthorCommand = new DelegateCommand(DeleteSelectedAuthor,CanDelete);
			RefreshAuthorsCommand = new DelegateCommand(RefreshAuthors);
		}

		private void RefreshAuthors()
		{
			_authors = new ObservableCollection<AuthorEntity>(_authorService.GetAll());
		}

		private bool CanDelete()
		{
			return SelectedAuthor != null;
		}

		private void DeleteSelectedAuthor()
		{
			int id = SelectedAuthor.Id;
			//_authorService.Delete(SelectedAuthor.Id);
			MessageBox.Show("Succesfully deleted");
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
					//switch (selectedSort)
					//	{
					//		case AuthorsSorting.None:
					//			authorsListDataGrid.Items.Clear();
					//			foreach (AuthorEntity author in authors)
					//				authorsListDataGrid.Items.Add(author);
					//			break;
					//		default: break;
					//	}
					_selectedSortType = value;
					OnPropertyChanged(nameof(SelectedSortType));
					Authors.Clear();
					foreach (AuthorEntity author in _authorService.GetAll())
					{
						Authors.Add(author);
					}
				}
			}
		}
		public ICommand DeleteSelectedAuthorCommand { get; }
		public ICommand RefreshAuthorsCommand { get; }
	}
}

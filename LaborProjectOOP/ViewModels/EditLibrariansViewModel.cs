using LaborProjectOOP.Constants.Enums;
using LaborProjectOOP.Database.Models;
using LaborProjectOOP.Dekstop.Commands;
using LaborProjectOOP.Services.CustomerServices;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using System;
using LaborProjectOOP.Services.LibrarianServices;
using System.Linq;
using System.Threading.Tasks;

namespace LaborProjectOOP.Dekstop.ViewModels
{
	public class EditLibrariansViewModel : ViewModelBase
	{
		public EditLibrariansViewModel(ILibrarianService librarianService)
		{
			_librarianService = librarianService;
            lbrnsFromDb = _librarianService.GetAll();
            _librarians = new ObservableCollection<LibrarianEntity>(lbrnsFromDb);
			_sortingTypes = new ObservableCollection<LibrariansSorting>(Enum.GetValues(typeof(LibrariansSorting)).Cast<LibrariansSorting>().ToArray());
			DeleteSelectedLibrarianCommand = new DelegateCommand(DeleteSelectedCustomer, IsSelected);
			MakeAnAdminCommand = new DelegateCommand(MakeAnAdminSelected, IsSelected);
			RefreshLibrariansCommand = new DelegateCommand(RefreshLibrarians);
		}
		
		private async void MakeAnAdminSelected()
		{
			if (_selectedLibrarian.IsAdmin == true)
			{
				MessageBox.Show("Such librarian is already an admin");
			}
			else
			{
				_selectedLibrarian.IsAdmin = true;
                await _librarianService.Update(_selectedLibrarian);
				MessageBox.Show("Succesfully updated");
			}
		}

		private async void RefreshLibrarians()
		{
			Librarians.Clear();
            lbrnsFromDb = _librarianService.GetAll();
            foreach (LibrarianEntity librarian in lbrnsFromDb)
                _librarians.Add(librarian);
        }

		private bool IsSelected()
		{
			return SelectedLibrarian != null;
		}

		private void DeleteSelectedCustomer()
		{
			if (_selectedLibrarian.Login == "admin")
			{
				MessageBox.Show("You cannot delete Main Admin");
			}
			else
			{
				_librarianService.Delete(SelectedLibrarian.Id);
                MessageBox.Show("Succesfully deleted. Update list.");
            }
		}

		private ObservableCollection<LibrariansSorting> _sortingTypes;
		public ICollection<LibrariansSorting> SortingTypes => _sortingTypes;
		private LibrariansSorting _selectedSortType;
		public LibrariansSorting SelectedSortType
		{
			get => _selectedSortType;
			set
			{
				if (_selectedSortType != value)
				{
					_selectedSortType = value;
					OnPropertyChanged(nameof(SelectedSortType));
					Librarians.Clear();
					switch (_selectedSortType)
					{
						case LibrariansSorting.None:
							foreach (LibrarianEntity librarian in lbrnsFromDb)
								Librarians.Add(librarian);
							break;
						case LibrariansSorting.AdminsOnly:
							foreach (LibrarianEntity librarian in lbrnsFromDb.Where(librarian => librarian.IsAdmin))
							{
								Librarians.Add(librarian);
							}
							break;
						default: break;
					}
				}
			}
		}
		private static ICollection<LibrarianEntity> lbrnsFromDb;
		private ObservableCollection<LibrarianEntity> _librarians;
		public ICollection<LibrarianEntity> Librarians => _librarians;
		private LibrarianEntity _selectedLibrarian;
		public LibrarianEntity SelectedLibrarian
		{
			get => _selectedLibrarian;
			set
			{
				_selectedLibrarian = value;
				OnPropertyChanged(nameof(SelectedLibrarian));
			}
		}
		private readonly ILibrarianService _librarianService;
		public ICommand DeleteSelectedLibrarianCommand { get; }
		public ICommand MakeAnAdminCommand { get; }
		public ICommand RefreshLibrariansCommand { get; }
	}
}

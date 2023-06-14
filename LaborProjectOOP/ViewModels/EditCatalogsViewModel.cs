using LaborProjectOOP.Constants.Enums;
using LaborProjectOOP.Database.Models;
using LaborProjectOOP.Dekstop.Commands;
using LaborProjectOOP.Services.BookServices;
using LaborProjectOOP.Services.CatalogServices;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace LaborProjectOOP.Dekstop.ViewModels
{
	public class EditCatalogsViewModel : ViewModelBase
	{
		public EditCatalogsViewModel(ICatalogService catalogService)
		{
			_catalogService = catalogService;
		    ctlgsFromDb =  _catalogService.GetAll();
            _sortingTypes = new ObservableCollection<CatalogsSorting>(Enum.GetValues(typeof(CatalogsSorting)).Cast<CatalogsSorting>().ToArray());
            _catalogs = new ObservableCollection<CatalogEntity>(ctlgsFromDb);
			
			DeleteSelectedCatalogCommand = new DelegateCommand(DeleteSelectedCatalog, CanDelete);
			RefreshCatalogsCommand = new DelegateCommand(RefreshCatalogs);
			//SelectedOrder = _orders.Where(c => c.Name == "None").FirstOrDefault();
		}
		
		private void RefreshCatalogs()
		{
            Catalogs.Clear();
            ctlgsFromDb = _catalogService.GetAll();
            foreach (CatalogEntity catalog in ctlgsFromDb)
                Catalogs.Add(catalog);
        }

		private bool CanDelete()
		{
			return SelectedCatalog != null;
		}

		private async void DeleteSelectedCatalog()
		{
			await _catalogService.Delete(SelectedCatalog.Id);
            MessageBox.Show("Succesfully deleted. Update list.");
        }

		private ObservableCollection<CatalogsSorting> _sortingTypes;
		public ICollection<CatalogsSorting> SortingTypes => _sortingTypes;
		private CatalogsSorting _selectedSortType;
		public CatalogsSorting SelectedSortType
		{
			get => _selectedSortType;
			set
			{
				if (_selectedSortType != value)
				{
					_selectedSortType = value;
					OnPropertyChanged(nameof(SelectedSortType));
					Catalogs.Clear();
					switch (_selectedSortType)
					{
						case CatalogsSorting.None:
							foreach (CatalogEntity catalog in ctlgsFromDb)
								Catalogs.Add(catalog);
							break;
						default: break;
					}
				}
			}
		}
		private ObservableCollection<CatalogEntity> _catalogs;
		public ICollection<CatalogEntity> Catalogs => _catalogs;
		private CatalogEntity _selectedCatalog;
		public CatalogEntity SelectedCatalog
		{
			get => _selectedCatalog;
			set
			{
				_selectedCatalog = value;
				OnPropertyChanged(nameof(SelectedCatalog));
			}
		}
		private readonly ICatalogService _catalogService;
		private static ICollection<CatalogEntity> ctlgsFromDb;
		public ICommand DeleteSelectedCatalogCommand { get; }
		public ICommand RefreshCatalogsCommand { get; }
	}
}

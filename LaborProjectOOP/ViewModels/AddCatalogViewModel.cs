using LaborProjectOOP.Database.Models;
using LaborProjectOOP.Dekstop.Commands;
using LaborProjectOOP.Services.CatalogServices;
using System;
using System.Windows.Input;

namespace LaborProjectOOP.Dekstop.ViewModels
{
	public class AddCatalogViewModel : ViewModelBase
	{
		public AddCatalogViewModel(ICatalogService catalogService) 
		{
			_catalogService = catalogService;
			CreateCatalogCommand = new DelegateCommand(CreateCatalog,CanCreate);
		}

		private bool CanCreate()
		{
			return !string.IsNullOrEmpty(Name);
		}

		private void CreateCatalog()
		{
			_catalogService.Create(new CatalogEntity
			{
				Name = Name,
			});	
		}

		private string _name;
		public string Name
		{
			get => _name;
			set
			{
				_name= value;
				OnPropertyChanged(nameof(Name));
			}
		}

		public ICommand CreateCatalogCommand { get; }
		private readonly ICatalogService _catalogService;
	}
}

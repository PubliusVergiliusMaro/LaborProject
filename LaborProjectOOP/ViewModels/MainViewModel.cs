using LaborProjectOOP.Dekstop.NavigationServices.Stores;
using LaborProjectOOP.Services.AuthorServices;
using LaborProjectOOP.Services.CustomerServices;
using LaborProjectOOP.Services.LibrarianServices;
using System;

namespace LaborProjectOOP.Dekstop.ViewModels
{
	public class MainViewModel : ViewModelBase
	{
		private readonly NavigationStore _navigationStore;
		public ViewModelBase CurrentViewModel { get; }//=> _navigationStore.CurrentViewModel;

		public MainViewModel(ICustomerService customerService, ILibrarianService librarianService, IAuthorService authorService)//NavigationStore navigationStore)
		{
			//_navigationStore = navigationStore;
			//_navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
			CurrentViewModel = new CustomerMainViewModel();		
		}

		private void OnCurrentViewModelChanged()
		{
			OnPropertyChanged(nameof(CurrentViewModel));
		}
	}
}

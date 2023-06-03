using LaborProjectOOP.Dekstop.NavigationServices.Stores;
using LaborProjectOOP.Services.AuthorServices;
using LaborProjectOOP.Services.BookServices;
using LaborProjectOOP.Services.CatalogServices;
using LaborProjectOOP.Services.CustomerServices;
using LaborProjectOOP.Services.LibrarianServices;
using System;

namespace LaborProjectOOP.Dekstop.ViewModels
{
	public class MainViewModel : ViewModelBase
	{
		private readonly NavigationStore _navigationStore;
		public ViewModelBase CurrentViewModel { get; }//=> _navigationStore.CurrentViewModel;

		public MainViewModel(ICustomerService customerService, ILibrarianService librarianService, IAuthorService authorService,ICatalogService catalogService,IBookService bookService)//NavigationStore navigationStore)
		{
			//_navigationStore = navigationStore;
			//_navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
			CurrentViewModel = new CustomerMainViewModel(catalogService,bookService,customerService);	
		}

		private void OnCurrentViewModelChanged()
		{
			OnPropertyChanged(nameof(CurrentViewModel));
		}
	}
}

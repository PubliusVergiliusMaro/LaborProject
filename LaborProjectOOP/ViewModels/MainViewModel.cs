using LaborProjectOOP.Constants.Enums;
using LaborProjectOOP.Dekstop.NavigationServices.Stores;
using LaborProjectOOP.Services.AuthorServices;
using LaborProjectOOP.Services.BookServices;
using LaborProjectOOP.Services.CartListServices;
using LaborProjectOOP.Services.CatalogServices;
using LaborProjectOOP.Services.CustomerServices;
using LaborProjectOOP.Services.LibrarianServices;
using LaborProjectOOP.Services.OrderHistoryServices;
using LaborProjectOOP.Services.WishListServices;
using System;

namespace LaborProjectOOP.Dekstop.ViewModels
{
	public class MainViewModel : ViewModelBase
	{
		private readonly NavigationStore _navigationStore;
		public ViewModelBase CurrentViewModel { get; }//=> _navigationStore.CurrentViewModel;

		public MainViewModel(ICustomerService customerService, ILibrarianService librarianService, IAuthorService authorService,ICatalogService catalogService,IBookService bookService,ICartListService cartListService,IWishListService wishListService, IOrderService orderService)//NavigationStore navigationStore)
		{
			//_navigationStore = navigationStore;
			//_navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

			CurrentViewModel = new CustomerActivitiesViewModel(customerService.GetById(3),wishListService,cartListService, CustomerActivitiesInfoType.WishList);
			//CurrentViewModel = new AdminViewModel(customerService,librarianService,authorService,catalogService,bookService,cartListService,wishListService,orderService);
		}

		private void OnCurrentViewModelChanged()
		{
			OnPropertyChanged(nameof(CurrentViewModel));
		}
	}
}

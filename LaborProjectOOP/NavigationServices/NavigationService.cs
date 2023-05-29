using LaborProjectOOP.Dekstop.NavigationServices.Stores;
using LaborProjectOOP.Dekstop.ViewModels;
using System;

namespace LaborProjectOOP.Dekstop.NavigationServices
{
	public class NavigationService : INavigationService
	{
		private readonly NavigationStore _navigateStore;
		private readonly Func<ViewModelBase> createViewModel;

		public NavigationService(NavigationStore navigateStore, Func<ViewModelBase> createViewModel)
		{
			_navigateStore = navigateStore;
			this.createViewModel = createViewModel;
		}

		public void Navigate()
		{
			_navigateStore.CurrentViewModel = createViewModel();
		}
	}
}

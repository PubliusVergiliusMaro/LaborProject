using LaborProjectOOP.Dekstop.ViewModels;
using System;

namespace LaborProjectOOP.Dekstop.NavigationServices.Stores
{
	public class NavigationStore
	{
		public ViewModelBase _currentViewModel;
		public ViewModelBase CurrentViewModel
		{
			get { return _currentViewModel; }
			set
			{
				_currentViewModel = value;
				OnCurrentViewModelChanged();
			}
		}

		public event Action CurrentViewModelChanged;
		private void OnCurrentViewModelChanged()
		{
			CurrentViewModelChanged?.Invoke();
		}

	}
}

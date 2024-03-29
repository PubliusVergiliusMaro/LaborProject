﻿using LaborProjectOOP.Constants.Enums;
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
		public ViewModelBase CurrentViewModel => _navigationStore.CurrentViewModel;

		public MainViewModel(NavigationStore navigationStore)
		{
			_navigationStore = navigationStore;
			_navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
		}
		private void OnCurrentViewModelChanged()
		{
			OnPropertyChanged(nameof(CurrentViewModel));
		}
	}
}

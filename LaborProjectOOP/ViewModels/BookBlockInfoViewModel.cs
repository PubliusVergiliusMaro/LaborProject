using LaborProjectOOP.Constants.Enums;
using LaborProjectOOP.Database.Models;
using LaborProjectOOP.Dekstop.Commands;
using LaborProjectOOP.Services.CartListServices;
using LaborProjectOOP.Services.CustomerServices;
using LaborProjectOOP.Services.WishListServices;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LaborProjectOOP.Dekstop.ViewModels
{
    public class BookBlockInfoViewModel : ViewModelBase
    {
		public BookBlockInfoViewModel(BookEntity currentBook,CustomerEntity currentCustomer,CustomerActivitiesInfoType customerActivitiesInfoType,ICustomerService customerService,IWishListService wishListService,ICartListService cartListService) 
        {
            _currentCustomer = currentCustomer;
            _title = currentBook.Title;
            _author = currentBook.Author.FullName;
            _price = currentBook.Price;
            _imagePath = currentBook.ImagePath;
            _currentBook = currentBook;
            _wishListService = wishListService;
            _cartListService = cartListService;
            _customerService = customerService;
            _customerActivitiesInfoType = customerActivitiesInfoType;
            RemoveBookCommand = new DelegateCommand(Remove);
        }

        private async void Remove()
        {
            if (_customerActivitiesInfoType == CustomerActivitiesInfoType.WishList)
            {
                WishListEntity wshLst = await _wishListService.GetByCustomerIdAndBookId(_currentCustomer.Id, _currentBook.Id);
                if(wshLst == null) 
                {
                   MessageBox.Show("Update this page, such book was already removed");
                    return;
                }
                else
                await _wishListService.Delete(wshLst.Id);
            }
            else
            {
                CartListEntity crtLst = await _cartListService.GetByCustomerIdAndBookId(_currentCustomer.Id, _currentBook.Id);
                if (crtLst == null)
                {
                    MessageBox.Show("Update this page, such book was already removed");
                    return;
                }
                else
                    await _cartListService.Delete(crtLst.Id);
            }
            MessageBox.Show($"{_title} was removed.\n Update list of books");
        }

		private string _title;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        private string _author;
        public string Author
        {
            get => _author;
            set
            {
                _author = value;
                OnPropertyChanged(nameof(Author));
            }
        }
        private double _price;
        public double Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged(nameof(Price));
            }
        }
        public string _imagePath;
        public string ImagePath
        {
            get => _imagePath;
            set
            {
                _imagePath = value;
                OnPropertyChanged(nameof(ImagePath));
            }
        }
   
        private readonly CustomerEntity _currentCustomer;
        private readonly CustomerActivitiesInfoType _customerActivitiesInfoType;
        private readonly BookEntity _currentBook;
        private readonly IWishListService _wishListService;
        private readonly ICartListService _cartListService;
        private readonly ICustomerService _customerService;
       public ICommand RemoveBookCommand { get; }
	}
}

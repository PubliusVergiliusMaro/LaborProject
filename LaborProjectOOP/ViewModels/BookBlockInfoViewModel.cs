using LaborProjectOOP.Database.Models;
using LaborProjectOOP.Dekstop.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LaborProjectOOP.Dekstop.ViewModels
{
    public class BookBlockInfoViewModel : ViewModelBase
    {
		public BookBlockInfoViewModel(BookEntity bookEntity,CustomerEntity currentCustomer) 
        {
            _title = bookEntity.Title;
            _author = bookEntity.Author.FullName;
            _price = bookEntity.Price;
            _imagePath = bookEntity.ImagePath;
            RemoveBookCommand = new DelegateCommand(Remove);
        }

		private void Remove()
		{
            MessageBox.Show($"Remove {_title}");
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
       public ICommand RemoveBookCommand { get; }






	}
}

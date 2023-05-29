using LaborProjectOOP.Database.Models;

namespace LaborProjectOOP.Dekstop.ViewModels
{
	public class WishListViewModel : ViewModelBase
	{
		public WishListEntity _wishList;
		public WishListViewModel(WishListEntity wishList)
		{
			_wishList = wishList;
			Customer = new CustomerViewModel(_wishList.Customer);
			Book = new BookViewModel(_wishList.Book);
		}
		public int Id => _wishList.Id;
		public int CustomerFK => _wishList.CustomerFK;
		public CustomerViewModel Customer { get; }
		public int BookFK => _wishList.BookFK;
		public BookViewModel Book { get; }
	}
}

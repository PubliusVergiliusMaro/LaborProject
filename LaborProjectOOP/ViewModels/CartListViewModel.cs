using LaborProjectOOP.Database.Models;

namespace LaborProjectOOP.Dekstop.ViewModels
{
	public class CartListViewModel : ViewModelBase
	{
		private readonly CartListEntity _cartListEntity;
		public CartListViewModel(CartListEntity cartListEntity)
		{
			_cartListEntity = cartListEntity;
			Customer = new CustomerViewModel(_cartListEntity.Customer);
			Book = new BookViewModel(_cartListEntity.Book);
		}
		public int Id => _cartListEntity.Id;
		public int CustomerFK => _cartListEntity.CustomerFK;
		public CustomerViewModel Customer { get; }
		public int BookFK => _cartListEntity.BookFK;
		public BookViewModel Book { get; }
	}
}

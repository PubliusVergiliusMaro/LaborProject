using LaborProjectOOP.Constants.Enums;
using LaborProjectOOP.Database.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;

namespace LaborProjectOOP.Dekstop.ViewModels
{
	public class BookViewModel : ViewModelBase
	{
		private readonly BookEntity _bookEntity;
		public BookViewModel(BookEntity bookEntity)
		{
			_bookEntity= bookEntity;
		    Author = new AuthorViewModel(_bookEntity.Author);
			WishList = _bookEntity.WishLists.Select(wishList=>new WishListViewModel(wishList)).ToList();	
            CartList = _bookEntity.CartLists.Select(cartList => new CartListViewModel(cartList)).ToList();
			OrderList = _bookEntity.OrderList.Select(order => new OrderViewModel(order)).ToList();
		}
		public int Id => _bookEntity.Id;
		public string Title => _bookEntity.Title;
		public string Description => _bookEntity.Description;
		public double Price => _bookEntity.Price;
		public string ImagePath => _bookEntity.ImagePath;
		public AuthorViewModel? Author { get; }
		public int? AuthorFK => _bookEntity.AuthorFK;
		public CatalogViewModel? Catalog { get; }
		public int? CatalogFK => _bookEntity.CatalogFK;
		public List<BookGenreTypes> Genres => _bookEntity.Genres;
		public ICollection<WishListViewModel> WishList { get; }
		public ICollection<CartListViewModel> CartList { get; }
		public ICollection<OrderViewModel> OrderList { get; }
	}
}

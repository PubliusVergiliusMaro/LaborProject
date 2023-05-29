using LaborProjectOOP.Database.Models;
using System;

namespace LaborProjectOOP.Dekstop.ViewModels
{
	public class OrderViewModel
	{
		public OrderEntity _order;
		public OrderViewModel(OrderEntity order)
		{
			_order = order;
			Customer = new CustomerViewModel(_order.Customer);
			Book = new BookViewModel(_order.Book);
		}
		public int Id => _order.Id;
		public DateTime CreatedOn => _order.CreatedOn;
		public bool IsActual => _order.IsActual;
		public int CustomerFK => _order.CustomerFK;
		public CustomerViewModel Customer { get; }
		public int? BookFK => _order.BookFK;
		public BookViewModel Book { get; }
	}
}

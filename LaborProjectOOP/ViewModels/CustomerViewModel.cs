using LaborProjectOOP.Database.Models;
using System.Collections.Generic;
using System.Linq;

namespace LaborProjectOOP.Dekstop.ViewModels
{
	public class CustomerViewModel : ViewModelBase
	{
		private readonly CustomerEntity _customer;
		public CustomerViewModel(CustomerEntity customer)
		{
			_customer = customer;
			Orders = customer.Orders.Select(order => new OrderViewModel(order)).ToList();
            WishList = customer.WishList.Select(wishList => new WishListViewModel(wishList)).ToList();
			CartList = customer.CartList.Select(cartList => new CartListViewModel(cartList)).ToList();
		}
		public int Id => _customer.Id;
		public string Login => _customer.Login;
		public string Password => _customer.Password;
		public string Email => _customer.Email;
		public string Phone => _customer.Phone;
		public string AvatarImagePath => _customer.AvatarImagePath;
		public bool IsBanned => _customer.IsBanned;
		public int OrderListFK => _customer.OrderListFK;
		public ICollection<OrderViewModel> Orders { get; }
		public ICollection<WishListViewModel> WishList { get; }
		public ICollection<CartListViewModel> CartList { get; }
	}
}

using LaborProjectOOP.Database.Base;

namespace LaborProjectOOP.Database.Models
{
	public class CustomerEntity : Human
	{
		public CustomerEntity() 
		{
			WishList = new List<WishListEntity>();
			CartList= new List<CartListEntity>();
		}
		public string Email { get; set; }
		public string Phone { get; set; }
		public string AvatarImagePath { get; set; }
		public bool IsBanned { get; set; }
		public OrderListEntity OrderList { get; set; }
		public ICollection<WishListEntity> WishList { get; set; }
		public ICollection<CartListEntity> CartList { get; set; }
	}
}

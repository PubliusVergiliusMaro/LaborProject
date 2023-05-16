using LaborProjectOOP.Database.Base;

namespace LaborProjectOOP.Database.Models
{
	public class CustomerEntity : Human
	{
		public string Email { get; set; }
		public string Phone { get; set; }
		public bool IsBanned { get; set; }
		public List<OrderEntity> Orders { get; set; }
	}
}

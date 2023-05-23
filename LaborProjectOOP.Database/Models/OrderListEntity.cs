namespace LaborProjectOOP.Database.Models
{
	public class OrderListEntity
	{
		public OrderListEntity()
		{
			Orders = new List<OrderEntity>();	
		}
		public int Id { get; set; }
		public int CustomerFK { get; set; }
		public CustomerEntity Customer { get; set; }
		public ICollection<OrderEntity> Orders { get; set; }
	}
}

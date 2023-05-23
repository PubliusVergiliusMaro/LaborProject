namespace LaborProjectOOP.Database.Models
{
	public class OrderEntity
	{
		public int Id { get; set; }
		public DateTime CreatedOn { get; set; }
		public bool IsActual { get; set; }
		public int? OrderListFK { get; set; }
		public OrderListEntity OrderList { get; set; }
		public int? BookFK { get; set; }
		public BookEntity Book { get; set; }
	}
}

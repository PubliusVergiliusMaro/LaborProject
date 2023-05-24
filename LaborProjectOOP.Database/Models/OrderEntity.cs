namespace LaborProjectOOP.Database.Models
{
	public class OrderEntity
	{
		public int Id { get; set; }
		public DateTime CreatedOn { get; set; }
		public bool IsActual { get; set; }
		public int CustomerFK { get; set; }
		public CustomerEntity Customer { get; set; }
		public int? BookFK { get; set; }
		public BookEntity Book { get; set; }
	}
}

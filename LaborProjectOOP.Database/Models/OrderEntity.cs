namespace LaborProjectOOP.Database.Models
{
	public class OrderEntity
	{
		public OrderEntity()
		{
			Books = new List<BookEntity>();	
		}
		public int Id { get; set; }
		public DateTime CreatedOn { get; set; }
		public bool IsActual { get; set; }
		public CustomerEntity Customer { get; set; }
		public int CustomerFK { get; set; }
		public IList<BookEntity> Books { get; set; }
	}
}

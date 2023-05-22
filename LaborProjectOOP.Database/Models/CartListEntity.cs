namespace LaborProjectOOP.Database.Models
{
	public class CartListEntity
	{
		public int Id { get; set; }
		public int CustomerFK { get; set; }
		public CustomerEntity Customer { get; set; }
		public int BookFK { get; set; }
		public BookEntity Book { get; set; }
	}
}

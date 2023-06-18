namespace LaborProjectOOP.Database.Models
{
	public class CatalogEntity
	{
		public CatalogEntity()
		{
			Books = new List<BookEntity>();
		}
		public int Id { get; set; }
		public string Name { get; set; }
		public ICollection<BookEntity> Books { get; set; }
	}
}

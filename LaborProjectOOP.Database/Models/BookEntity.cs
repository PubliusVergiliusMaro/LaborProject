namespace LaborProjectOOP.Database.Models
{
	public class BookEntity
	{
		public BookEntity() { }
		public BookEntity(string title, string description, double price, bool istakes, string imagePath, AuthorEntity author)
		{
			Title = title;
			Description = description;
			Price = price;
			IsTaken = istakes;
			Author = author;
			ImagePath = imagePath;
		}
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public double Price { get; set; }
		public bool IsTaken { get; set; }
		public string ImagePath { get; set; }

		public AuthorEntity? Author { get; set; }
		public int? AuthorFK { get; set; }

		public CatalogEntity? Catalog { get; set; }
		public int? CatalogFK { get; set; }

		public OrderEntity? Order { get; set; }
		public int? OrderFK { get; set; }
	}
}

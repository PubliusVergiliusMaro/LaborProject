namespace LaborProjectOOP.Database.Models
{
	public class AuthorEntity
	{
		public AuthorEntity()
		{
			Books = new List<BookEntity>();
		}
		public int Id { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public List<BookEntity> Books { get; set; }
		public string FullName
		{
			get => $"{Name} {Surname}";
		}
	}
}

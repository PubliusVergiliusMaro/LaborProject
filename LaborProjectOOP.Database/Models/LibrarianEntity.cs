using LaborProjectOOP.Database.Base;

namespace LaborProjectOOP.Database.Models
{
	public class LibrarianEntity : Human
	{
		public int Salary { get; set; }
		public byte WorkExperience { get; set; }
		public bool IsAdmin { get; set; }
	}
}

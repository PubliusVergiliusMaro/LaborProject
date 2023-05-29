using LaborProjectOOP.Database.Models;

namespace LaborProjectOOP.Dekstop.ViewModels
{
	public class LibrarianViewModel : ViewModelBase
	{
		private readonly LibrarianEntity _librarian;
		public LibrarianViewModel(LibrarianEntity librarian)
		{
			_librarian = librarian;
		}
		public int Id => _librarian.Id;
		public string Login => _librarian.Login;
		public string Password => _librarian.Password;
		public int Salary => _librarian.Salary;
		public byte WorkExperience => _librarian.WorkExperience;
		public bool IsAdmin => _librarian.IsAdmin;
	}
}

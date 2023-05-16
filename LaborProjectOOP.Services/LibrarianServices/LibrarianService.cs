using LaborProjectOOP.Database.Models;
using LaborProjectOOP.EntityFramework.Repository;
using Newtonsoft.Json;

namespace LaborProjectOOP.Services.LibrarianServices
{
	public class LibrarianService : ILibrarianService
	{
		private readonly IGenericRepository<LibrarianEntity> _librarianRepository;

		public LibrarianService(IGenericRepository<LibrarianEntity> librarianRepository)
		{
			_librarianRepository = librarianRepository;
		}

		public void Create(LibrarianEntity librarian)
		{
			_librarianRepository.Create(librarian);
		}
		public bool Delete(int id)
		{
			LibrarianEntity dbRecord = _librarianRepository.Table
				.FirstOrDefault(b => b.Id == id);
			if (dbRecord == null)
			{
				return false;
			}
			_librarianRepository.Remove(dbRecord);
			return true;
		}
		public List<LibrarianEntity> GetAll()
		{
			List<LibrarianEntity> dbRecord = _librarianRepository.Table
				 .ToList();
			if (dbRecord == null)
			{
				return null;
			}
			return dbRecord;
		}
		public LibrarianEntity GetById(int id)
		{
			LibrarianEntity dbRecord = _librarianRepository.Table
				.Where(libr => libr.Id == id)
				.FirstOrDefault();
			if (dbRecord == null)
			{
				return null;
			}
			return dbRecord;
		}
		public bool Update(LibrarianEntity librarian)
		{
			try
			{
				LibrarianEntity dbRecord = _librarianRepository.Table
					.Where(libr => libr.Id == librarian.Id)
					.FirstOrDefault();
				if (dbRecord == null)
				{
					return false;
				}
				dbRecord.Login = librarian.Login;
				dbRecord.Password = librarian.Password;
				dbRecord.Salary = librarian.Salary;
				dbRecord.WorkExperience = librarian.WorkExperience;
				dbRecord.IsAdmin = librarian.IsAdmin;

				_librarianRepository.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public static List<LibrarianEntity> GetLibrarians() =>
			JsonConvert.DeserializeObject<List<LibrarianEntity>>(File.ReadAllText(Constants.Constants.Constants.LIBRARIAN_FILE_PATH));
		public static void SaveLibrarians(List<LibrarianEntity> librarians) =>
			File.WriteAllText(Constants.Constants.Constants.LIBRARIAN_FILE_PATH, JsonConvert.SerializeObject(librarians));
	}
}

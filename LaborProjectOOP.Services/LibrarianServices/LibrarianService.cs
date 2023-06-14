using LaborProjectOOP.Database.Models;
using LaborProjectOOP.EntityFramework.Repository;
using Microsoft.EntityFrameworkCore;
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

		public async Task Create(LibrarianEntity librarian)=> await	_librarianRepository.Create(librarian);
		
		public async Task<bool> Delete(int id)
		{
			LibrarianEntity dbRecord = await _librarianRepository.Table
				.FirstOrDefaultAsync(b => b.Id == id);
			if (dbRecord == null)
			{
				return false;
			}
			await _librarianRepository.Delete(dbRecord);
			return true;
		}
		public List<LibrarianEntity> GetAll()
		{
			List<LibrarianEntity> dbRecord =  _librarianRepository.Table
				 .ToList();
			if (dbRecord == null)
			{
				return null;
			}
			return dbRecord;
		}
		public async Task<LibrarianEntity> GetById(int id)
		{
			LibrarianEntity dbRecord = await _librarianRepository.Table
				.Where(libr => libr.Id == id)
				.FirstOrDefaultAsync();
			if (dbRecord == null)
			{
				return null;
			}
			return dbRecord;
		}
		public async Task<bool> Update(LibrarianEntity librarian)
		{
			try
			{
				LibrarianEntity dbRecord = await _librarianRepository.Table
					.Where(libr => libr.Id == librarian.Id)
					.FirstOrDefaultAsync();
				if (dbRecord == null)
				{
					return false;
				}
				dbRecord.Login = librarian.Login;
				dbRecord.Password = librarian.Password;
				dbRecord.Salary = librarian.Salary;
				dbRecord.WorkExperience = librarian.WorkExperience;
				dbRecord.IsAdmin = librarian.IsAdmin;

				await _librarianRepository.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public async static Task<List<LibrarianEntity>> GetLibrarians() =>
	    JsonConvert.DeserializeObject<List<LibrarianEntity>>(await File.ReadAllTextAsync(Constants.Constants.Constants.LIBRARIAN_FILE_PATH));
		public async static Task SaveLibrarians(List<LibrarianEntity> librarians) =>
		await File.WriteAllTextAsync(Constants.Constants.Constants.LIBRARIAN_FILE_PATH, JsonConvert.SerializeObject(librarians));
	}
}

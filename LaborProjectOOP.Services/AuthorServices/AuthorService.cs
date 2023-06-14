using LaborProjectOOP.Database.Models;
using LaborProjectOOP.EntityFramework.Repository;
using Microsoft.EntityFrameworkCore;

namespace LaborProjectOOP.Services.AuthorServices
{
	public class AuthorService : IAuthorService
	{
		private readonly IGenericRepository<AuthorEntity> _authorRepository;

		public AuthorService(IGenericRepository<AuthorEntity> authorRepository)
		{
			_authorRepository = authorRepository;
		}

		public async Task Create(AuthorEntity author)
		{
			await _authorRepository.Create(author);
		}
		public async Task<bool> Delete(int id)
		{
			AuthorEntity dbRecord = await _authorRepository.Table
				.FirstOrDefaultAsync(author => author.Id == id);
			if (dbRecord == null)
			{
				return false;
			}
			await _authorRepository.Delete(dbRecord);
			return true;
		}
		public List<AuthorEntity> GetAll()
		{
			List<AuthorEntity> dbRecord = _authorRepository.Table
				.ToList();
			if (dbRecord == null)
			{
				return null;
			}
			return dbRecord;
		}
		public async Task<AuthorEntity> GetById(int id)
		{
			AuthorEntity dbRecord = await _authorRepository.Table
					.FirstOrDefaultAsync(author => author.Id == id);
			if (dbRecord == null)
			{
				return null;
			}
			return dbRecord;
		}
		public async Task<bool> Update(AuthorEntity author)
		{
			try
			{
				AuthorEntity dbRecord = await _authorRepository.Table
					.Where(a => a.Id == author.Id)
					.FirstOrDefaultAsync();
				if (dbRecord == null)
				{
					return false;
				}
				dbRecord.Name = author.Name;
				dbRecord.Surname = author.Surname;
				dbRecord.Books = author.Books;

				await _authorRepository.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
	}
}

using LaborProjectOOP.Database.Models;

namespace LaborProjectOOP.Services.AuthorServices
{
	public interface IAuthorService
	{
		Task Create(AuthorEntity author);
		Task<bool> Delete(int id);
		List<AuthorEntity> GetAll();
		Task<AuthorEntity> GetById(int id);
		Task<bool> Update(AuthorEntity author);
	}
}

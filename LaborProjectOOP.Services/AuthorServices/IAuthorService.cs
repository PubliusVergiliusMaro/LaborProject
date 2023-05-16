using LaborProjectOOP.Database.Models;

namespace LaborProjectOOP.Services.AuthorServices
{
	public interface IAuthorService
	{
		void Create(AuthorEntity author);
		bool Delete(int id);
		List<AuthorEntity> GetAll();
		AuthorEntity GetById(int id);
		bool Update(AuthorEntity author);
	}
}

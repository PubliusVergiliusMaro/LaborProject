using LaborProjectOOP.Database.Models;
using LaborProjectOOP.EntityFramework.Repository;

namespace LaborProjectOOP.Services.BaseService
{
	public class BaseService<T> where T: class
	{
		//private readonly IGenericRepository<T> _entityRepository;

		//public (IGenericRepository<T> entityRepository)
		//{
			
		//}
		//public void Create(T entity)
		//{
		//	_authorRepository.Create(author);

		//}
		//public bool Delete(int id)
		//{
		//	AuthorEntity dbRecord = _authorRepository.Table
		//		.FirstOrDefault(author => author.Id == id);
		//	if (dbRecord == null)
		//	{
		//		return false;
		//	}
		//	_authorRepository.Remove(dbRecord);
		//	return true;
		//}
		//public List<AuthorEntity> GetAll()
		//{
		//	List<AuthorEntity> dbRecord = _authorRepository.Table
		//		.ToList();
		//	if (dbRecord == null)
		//	{
		//		return null;
		//	}
		//	return dbRecord;
		//}
		//public AuthorEntity GetById(int id)
		//{
		//	AuthorEntity dbRecord = _authorRepository.Table
		//			.FirstOrDefault(author => author.Id == id);
		//	if (dbRecord == null)
		//	{
		//		return null;
		//	}
		//	return dbRecord;
		//}
		//public bool Update(AuthorEntity author)
		//{
		//	try
		//	{
		//		AuthorEntity dbRecord = _authorRepository.Table
		//			.Where(a => a.Id == author.Id)
		//			.FirstOrDefault();
		//		if (dbRecord == null)
		//		{
		//			return false;
		//		}
		//		dbRecord.Name = author.Name;
		//		dbRecord.Surname = author.Surname;
		//		dbRecord.Books = author.Books;

		//		_authorRepository.SaveChanges();
		//		return true;
		//	}
		//	catch (Exception ex)
		//	{
		//		return false;
		//	}
		//}
	}
}

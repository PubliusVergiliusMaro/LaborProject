using LaborProjectOOP.Database.Models;

namespace LaborProjectOOP.Services.BookServices
{
	public interface IBookService
	{
		Task Create(BookEntity book);
		Task<bool> Delete(int id);
		List<BookEntity> GetAll();
		Task<BookEntity> GetById(int id);
		Task<bool> Update(BookEntity book);
		Task PurchaseBooks(int CustomerId, ICollection<BookEntity> bookEntities);
	}
}

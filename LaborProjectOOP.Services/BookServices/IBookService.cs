using LaborProjectOOP.Database.Models;

namespace LaborProjectOOP.Services.BookServices
{
	public interface IBookService
	{
		void Create(BookEntity book);
		bool Delete(int id);
		List<BookEntity> GetAll();
		BookEntity GetById(int id);
		bool Update(BookEntity book);
		void PurchaseBooks(int CustomerId, List<BookEntity> bookEntities);
	}
}

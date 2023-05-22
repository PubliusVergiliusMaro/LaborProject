using LaborProjectOOP.Database.Models;
using LaborProjectOOP.EntityFramework.Repository;
using Microsoft.EntityFrameworkCore;

namespace LaborProjectOOP.Services.BookServices
{
	public class BookService : IBookService
	{
		private readonly IGenericRepository<BookEntity> _bookRepository;
		private readonly IGenericRepository<CustomerEntity> _customerRepository;
		private readonly IGenericRepository<OrderEntity> _orderRepository;
		public BookService(IGenericRepository<BookEntity> bookRepository, IGenericRepository<CustomerEntity> customerRepository, IGenericRepository<OrderEntity> orderRepository)
		{
			_bookRepository = bookRepository;
			_customerRepository = customerRepository;
			_orderRepository = orderRepository;
		}

		public void Create(BookEntity book)
		{
			_bookRepository.Create(book);

		}
		public bool Delete(int id)
		{
			BookEntity dbRecord = _bookRepository.Table
				.Include(b => b.Author)
				.Include(b => b.Catalog)
				.Include(b => b.Order)
				.Include(b => b.WishLists)
				.FirstOrDefault(b => b.Id == id);
			if (dbRecord == null)
			{
				return false;
			}
			_bookRepository.Remove(dbRecord);
			return true;
		}
		public List<BookEntity> GetAll()
		{
			List<BookEntity> dbRecord = _bookRepository.Table
				.Include(b => b.Author)
				.Include(b => b.Catalog)
				.Include(b => b.Order)
				.Include(b => b.WishLists)
				.ToList();
			if (dbRecord == null)
			{
				return null;
			}
			return dbRecord;
		}
		public BookEntity GetById(int id)
		{
			BookEntity dbRecord = _bookRepository.Table
					.Include(b => b.Author)
					.Include(b => b.Catalog)
					.Include(b => b.Order)
					.Include(b => b.WishLists)
					.FirstOrDefault(book => book.Id == id);
			if (dbRecord == null)
			{
				return null;
			}
			return dbRecord;
		}
		public bool Update(BookEntity book)
		{
			try
			{
				BookEntity dbRecord = _bookRepository.Table
					.Where(b => b.Id == book.Id)
					.Include(b => b.Author)
					.Include(b => b.Catalog)
					.Include(b => b.Order)
					.Include(b => b.WishLists)
					.FirstOrDefault();
				if (dbRecord == null)
				{
					return false;
				}
				dbRecord.Title = book.Title;
				dbRecord.Description = book.Description;
				dbRecord.Price = book.Price;
				dbRecord.IsTaken = book.IsTaken;
				dbRecord.Author = book.Author;
				dbRecord.AuthorFK = book.AuthorFK;
				dbRecord.CatalogFK = book.CatalogFK;
				dbRecord.OrderFK = book.OrderFK;

				_bookRepository.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
		public bool AddBookToCatalog(int bookId, int CatalogId)
		{

			return true;
		}
		public void PurchaseBooks(int CustomerId, List<BookEntity> bookEntities)
		{
			CustomerEntity dbRecord = _customerRepository.Table
				.Where(customer => customer.Id == CustomerId)
				.FirstOrDefault();
			if (dbRecord != null)
			{
				OrderEntity order = new OrderEntity()
				{
					Books = bookEntities,
					CreatedOn = DateTime.UtcNow,
					Customer = dbRecord,
					IsActual = true
				};
				foreach (BookEntity bookEntity in bookEntities)
				{
					bookEntity.OrderFK = order.Id;
				}
				_orderRepository.Create(order);
			}
		}
	}
}

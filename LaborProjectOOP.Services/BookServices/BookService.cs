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

		public async Task Create(BookEntity book) => await _bookRepository.Create(book);

		public async Task<bool> Delete(int id)
		{
			BookEntity dbRecord = await _bookRepository.Table
				.Include(b => b.Author)
				.Include(b => b.Catalog)
				.Include(b => b.WishLists)
				.Include(b => b.CartLists)
				.Include(b => b.OrderList)
				.FirstOrDefaultAsync(b => b.Id == id);
			if (dbRecord == null)
			{
				return false;
			}
			await _bookRepository.Delete(dbRecord);
			return true;
		}
		public List<BookEntity> GetAll()
		{
			List<BookEntity> dbRecord =  _bookRepository.Table
				.Include(b => b.Author)
				.Include(b => b.Catalog)
				.Include(b => b.WishLists)
				.Include(b => b.CartLists)
				.Include(b => b.OrderList)
				.ToList();
			if (dbRecord == null)
			{
				return null;
			}
			return dbRecord;
		}
		public async Task<BookEntity> GetById(int id)
		{
			BookEntity dbRecord = await _bookRepository.Table
					.Include(b => b.Author)
					.Include(b => b.Catalog)
					.Include(b => b.WishLists)
					.Include(b => b.CartLists)
					.Include(b => b.OrderList)
					.FirstOrDefaultAsync(book => book.Id == id);
			if (dbRecord == null)
			{
				return null;
			}
			return dbRecord;
		}
		public async Task<bool> Update(BookEntity book)
		{
			try
			{
				BookEntity dbRecord = await _bookRepository.Table
					.Where(b => b.Id == book.Id)
					.Include(b => b.Author)
					.Include(b => b.Catalog)
					.Include(b => b.WishLists)
					.Include(b => b.CartLists)
					.Include(b => b.OrderList)
					.FirstOrDefaultAsync();
				if (dbRecord == null)
				{
					return false;
				}
				dbRecord.Title = book.Title;
				dbRecord.Description = book.Description;
				dbRecord.Price = book.Price;
				dbRecord.Author = book.Author;
				dbRecord.AuthorFK = book.AuthorFK;
				dbRecord.CatalogFK = book.CatalogFK;
				dbRecord.ImagePath = book.ImagePath;
					
				await _bookRepository.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
		public async Task PurchaseBooks(int CustomerId, ICollection<BookEntity> bookEntities)
		{
			CustomerEntity dbRecord = await _customerRepository.Table
				.Where(customer => customer.Id == CustomerId)
				.Include(b => b.WishList)
				.Include(b => b.CartList)
				.Include(b => b.Orders)
				.FirstOrDefaultAsync();
			if (dbRecord != null)
			{
				foreach (BookEntity bookEntity in bookEntities) 
				{
					OrderEntity order = new()
					{
						CustomerFK = dbRecord.Id,
						BookFK = bookEntity.Id,
						CreatedOn = DateTime.UtcNow,
						IsActual = true
					};
					await _orderRepository.Create(order);
				}
			}
		}
	}
}

using LaborProjectOOP.Database.Models;
using LaborProjectOOP.EntityFramework.Repository;
using Microsoft.EntityFrameworkCore;

namespace LaborProjectOOP.Services.CartListServices
{
	public class CartListService : ICartListService
	{
		private readonly IGenericRepository<CartListEntity> _cartListRepository;

		public CartListService(IGenericRepository<CartListEntity> cartListRepository)
		{
			_cartListRepository = cartListRepository;
		}

		public void Create(CartListEntity cartList)
		{
			_cartListRepository.Create(cartList);
		}
		public bool Delete(int id)
		{
			CartListEntity dbRecord = _cartListRepository.Table
				.Where(c => c.Id == id)
				.Include(c => c.Customer)
				.Include(c => c.Book)
				.FirstOrDefault();
			if (dbRecord == null)
			{
				return false;
			}
			_cartListRepository.Remove(dbRecord);
			return true;
		}
		public List<CartListEntity> GetAll()
		{
			List<CartListEntity> dbRecord = _cartListRepository.Table
				.Include(c => c.Customer)
				.Include(c => c.Book)
				.ToList();
			if (dbRecord == null)
			{
				return null;
			}
			return dbRecord;
		}

		public List<CartListEntity> GetCartListByCustomerId(int id)
		{	
			List<CartListEntity> dbRecord = _cartListRepository.Table
			.Where(c => c.CustomerFK == id)
			.Include(c => c.Customer)
			.Include(c => c.Book)
			.ToList();
			if (dbRecord == null)
			{
				return null;
			}
			return dbRecord;
		}


		public CartListEntity GetById(int id)
		{
			CartListEntity dbRecord = _cartListRepository.Table
				.Where(c => c.Id == id)
				.Include(c => c.Customer)
				.Include(c => c.Book)
				.FirstOrDefault();
			if (dbRecord == null)
			{
				return null;
			}
			return dbRecord;
		}
		public bool Update(CartListEntity cartList)
		{
			try
			{
				CartListEntity dbRecord = _cartListRepository.Table
				.Where(c => c.Id == cartList.Id)
				.Include(c => c.Customer)
				.Include(c => c.Book)
				.FirstOrDefault();
				if (dbRecord == null)
				{
					return false;
				}
				dbRecord.CustomerFK = cartList.CustomerFK;
				dbRecord.BookFK = cartList.BookFK;

				_cartListRepository.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
		public CartListEntity GetByCustomerIdAndBookId(int customerId, int bookId)
		{
			CartListEntity dbRecord = _cartListRepository.Table
				.Include(c => c.Customer)
				.Include(c => c.Book)
				.Where(c => c.Customer.Id == customerId && c.Book.Id == bookId)
				.FirstOrDefault();
			if (dbRecord == null)
			{
				return null;
			}

			return dbRecord;
		}
		public bool CheckIfExist(int customerId, int bookId)
		{
			CartListEntity dbRecord = _cartListRepository.Table
				.Include(c => c.Customer)
				.Include(c => c.Book)
				.Where(c => c.Customer.Id == customerId && c.Book.Id == bookId)
				.FirstOrDefault();
			if (dbRecord == null)
			{
				return false;
			}

			return true;
		}

	}
}

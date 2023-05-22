using LaborProjectOOP.Database.Models;
using LaborProjectOOP.EntityFramework.Repository;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace LaborProjectOOP.Services.WishListServices
{
	public class WishListService : IWishListService
	{
		private readonly IGenericRepository<WishListEntity> _wishListRepository;

		public WishListService(IGenericRepository<WishListEntity> wishListRepository)
		{
			_wishListRepository = wishListRepository;
		}

		public void Create(WishListEntity wishList)
		{
			_wishListRepository.Create(wishList);
		}
		public bool Delete(int id)
		{
			WishListEntity dbRecord = _wishListRepository.Table
				.Where(w => w.Id == id)
				.Include(w=>w.Customer)
				.Include(w=>w.Book)
				.FirstOrDefault();
			if (dbRecord == null)
			{
				return false;
			}
			_wishListRepository.Remove(dbRecord);
			return true;
		}
		public List<WishListEntity> GetAll()
		{
			List<WishListEntity> dbRecord = _wishListRepository.Table
				.Include(w => w.Customer)
				.Include(w => w.Book)
				.ToList();
			if (dbRecord == null)
			{
				return null;
			}
			return dbRecord;
		}

		public List<WishListEntity> GetWishListByCustomerId(int id)
		{
			List<WishListEntity> dbRecord = _wishListRepository.Table
			.Where(w => w.CustomerFK == id)
			.Include(w => w.Customer)
			.Include(w => w.Book)
			.ToList();
			if (dbRecord == null)
			{
				return null;
			}
			return dbRecord;
		}
		

		public WishListEntity GetById(int id)
		{
			WishListEntity dbRecord = _wishListRepository.Table
				.Where(w => w.Id == id)
				.Include(w => w.Customer)
				.Include(w => w.Book)
				.FirstOrDefault();
			if (dbRecord == null)
			{
				return null;
			}
			return dbRecord;
		}
		public bool Update(WishListEntity wishList)
		{
			try
			{
				WishListEntity dbRecord = _wishListRepository.Table
				.Where(w => w.Id == wishList.Id)
				.Include(w => w.Customer)
				.Include(w => w.Book)
				.FirstOrDefault();
				if (dbRecord == null)
				{
					return false;
				}
				dbRecord.CustomerFK = wishList.CustomerFK;
				dbRecord.BookFK = wishList.BookFK;

				_wishListRepository.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
		public WishListEntity GetByCustomerIdAndBookId(int customerId, int bookId)
		{
			WishListEntity dbRecord = _wishListRepository.Table
				.Include(w => w.Customer)
				.Include(w => w.Book)
				.Where(w => w.Customer.Id == customerId && w.Book.Id == bookId)
				.FirstOrDefault();
			if (dbRecord == null)
			{
				return null;
			}

			return dbRecord;
		}
		public bool CheckIfExist(int customerId,int bookId)
		{
			WishListEntity dbRecord = _wishListRepository.Table
				.Include(w => w.Customer)
				.Include(w => w.Book)
				.Where(w=>w.Customer.Id == customerId && w.Book.Id == bookId)
				.FirstOrDefault();
			if(dbRecord == null)
			{
				return false;
			}

			return true;
		}

	}
}

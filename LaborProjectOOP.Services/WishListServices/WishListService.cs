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

		public async Task Create(WishListEntity wishList) => await _wishListRepository.Create(wishList);

		public async Task<bool> Delete(int id)
		{
			WishListEntity dbRecord = await _wishListRepository.Table
				.Where(w => w.Id == id)
				.Include(w=>w.Customer)
				.Include(w=>w.Book)
				.FirstOrDefaultAsync();
			if (dbRecord == null)
			{
				return false;
			}
			await _wishListRepository.Delete(dbRecord);
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

		public async Task<List<WishListEntity>> GetWishListByCustomerId(int id)
		{
			List<WishListEntity> dbRecord = await _wishListRepository.Table
			.Where(w => w.CustomerFK == id)
			.Include(w => w.Customer)
			.Include(w => w.Book)
			.ToListAsync();
			if (dbRecord == null)
			{
				return null;
			}
			return dbRecord;
		}
		

		public async Task<WishListEntity> GetById(int id)
		{
			WishListEntity dbRecord = await _wishListRepository.Table
				.Where(w => w.Id == id)
				.Include(w => w.Customer)
				.Include(w => w.Book)
				.FirstOrDefaultAsync();
			if (dbRecord == null)
			{
				return null;
			}
			return dbRecord;
		}
		public async Task<bool> Update(WishListEntity wishList)
		{
			try
			{
				WishListEntity dbRecord = await _wishListRepository.Table
				.Where(w => w.Id == wishList.Id)
				.Include(w => w.Customer)
				.Include(w => w.Book)
				.FirstOrDefaultAsync();
				if (dbRecord == null)
				{
					return false;
				}
				dbRecord.CustomerFK = wishList.CustomerFK;
				dbRecord.BookFK = wishList.BookFK;

				await _wishListRepository.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
		public async Task<WishListEntity> GetByCustomerIdAndBookId(int customerId, int bookId)
		{
			WishListEntity dbRecord = await _wishListRepository.Table
				.Include(w => w.Customer)
				.Include(w => w.Book)
				.Where(w => w.CustomerFK == customerId && w.BookFK == bookId)
				.FirstOrDefaultAsync();
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
				.Where(w=>w.CustomerFK == customerId && w.BookFK == bookId)
				.FirstOrDefault();
			if(dbRecord == null)
			{
				return false;
			}
			return true;
		}

	}
}

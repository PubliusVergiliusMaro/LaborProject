using LaborProjectOOP.Database.Models;

namespace LaborProjectOOP.Services.WishListServices
{
	public interface IWishListService
	{
		Task Create(WishListEntity wishList);
		Task<bool> Delete(int id);
		List<WishListEntity> GetAll();
		Task<List<WishListEntity>> GetWishListByCustomerId(int id);
		Task<WishListEntity> GetById(int id);
		Task<WishListEntity> GetByCustomerIdAndBookId(int customerId, int bookId);
		Task<bool> Update(WishListEntity wishList);
		bool CheckIfExist(int customerId, int bookId);
	}
}

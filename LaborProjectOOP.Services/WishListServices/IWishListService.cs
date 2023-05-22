using LaborProjectOOP.Database.Models;

namespace LaborProjectOOP.Services.WishListServices
{
	public interface IWishListService
	{
		void Create(WishListEntity wishList);
		bool Delete(int id);
		List<WishListEntity> GetAll();
		List<WishListEntity> GetWishListByCustomerId(int id);
		WishListEntity GetById(int id);
		WishListEntity GetByCustomerIdAndBookId(int customerId, int bookId);
		bool Update(WishListEntity wishList);
		bool CheckIfExist(int customerId, int bookId);
	}
}

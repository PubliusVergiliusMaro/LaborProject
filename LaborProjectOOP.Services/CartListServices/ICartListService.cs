using LaborProjectOOP.Database.Models;

namespace LaborProjectOOP.Services.CartListServices
{
	public interface ICartListService
	{
		void Create(CartListEntity cartList);
		bool Delete(int id);
		List<CartListEntity> GetAll();
		List<CartListEntity> GetCartListByCustomerId(int id);
		CartListEntity GetById(int id);
		CartListEntity GetByCustomerIdAndBookId(int customerId, int bookId);
		bool Update(CartListEntity cartList);
		bool CheckIfExist(int customerId, int bookId);
	}
}

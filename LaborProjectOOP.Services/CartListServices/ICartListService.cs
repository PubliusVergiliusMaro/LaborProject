using LaborProjectOOP.Database.Models;

namespace LaborProjectOOP.Services.CartListServices
{
	public interface ICartListService
	{
		Task Create(CartListEntity cartList);
		Task<bool> Delete(int id);
		List<CartListEntity> GetAll();
		Task<List<CartListEntity>> GetCartListByCustomerId(int id);
		Task<CartListEntity> GetById(int id);
		Task<CartListEntity> GetByCustomerIdAndBookId(int customerId, int bookId);
		Task<bool> Update(CartListEntity cartList);
		bool CheckIfExist(int customerId, int bookId);
	}
}

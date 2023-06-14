using LaborProjectOOP.Database.Models;

namespace LaborProjectOOP.Services.OrderHistoryServices
{
	public interface IOrderService
	{
		Task Create(OrderEntity order);
		Task<bool> Delete(int id);
		List<OrderEntity> GetAll();
		Task<OrderEntity> GetById(int id);
		Task<bool> Update(OrderEntity order);
	}
}

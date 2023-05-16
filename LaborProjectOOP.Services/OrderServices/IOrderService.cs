using LaborProjectOOP.Database.Models;

namespace LaborProjectOOP.Services.OrderServices
{
	public interface IOrderService
	{
		void Create(OrderEntity order);
		bool Delete(int id);
		List<OrderEntity> GetAll();
		OrderEntity GetById(int id);
		bool Update(OrderEntity order);
		List<OrderEntity> GetOrders();
		void SaveOrders(List<OrderEntity> orders);
	}
}

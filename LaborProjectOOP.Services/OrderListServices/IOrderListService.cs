using LaborProjectOOP.Database.Models;

namespace LaborProjectOOP.Services.OrderServices
{
	public interface IOrderListService
	{
		void Create(OrderListEntity order);
		bool Delete(int id);
		List<OrderListEntity> GetAll();
		OrderListEntity GetById(int id);
		bool Update(OrderListEntity order);
		List<OrderListEntity> GetOrders();
		void SaveOrders(List<OrderListEntity> orders);
	}
}

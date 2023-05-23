using LaborProjectOOP.Database.Models;
using LaborProjectOOP.EntityFramework.Repository;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace LaborProjectOOP.Services.OrderServices
{
	public class OrderListService : IOrderListService
	{
		public readonly IGenericRepository<OrderListEntity> _orderRepository;

		public OrderListService(IGenericRepository<OrderListEntity> orderRepository)
		{
			_orderRepository = orderRepository;
		}

		public void Create(OrderListEntity order)
		{
			_orderRepository.Create(order);
		}
		public bool Delete(int id)
		{
			OrderListEntity dbRecord = _orderRepository.Table
				.Include(orderList => orderList.Orders)
				.FirstOrDefault(order => order.Id == id);
			if (dbRecord == null)
			{
				return false;
			}
			_orderRepository.Remove(dbRecord);
			return true;
		}
		public List<OrderListEntity> GetAll()
		{
			List<OrderListEntity> dbRecord = _orderRepository.Table
				.Include(orderList => orderList.Orders)
				.ToList();
			if (dbRecord == null)
			{
				return null;
			}
			return dbRecord;
		}
		public OrderListEntity GetById(int id)
		{
			OrderListEntity dbRecord = _orderRepository.Table
				.Where(catalog => catalog.Id == id)
				.Include(orderList => orderList.Orders)
				.FirstOrDefault();
			if (dbRecord == null)
			{
				return null;
			}
			return dbRecord;
		}
		public bool Update(OrderListEntity order)
		{
			try
			{
				OrderListEntity dbRecord = _orderRepository.Table
					.Where(ord => ord.Id == order.Id)
					.Include(orderList => orderList.Orders)
					.FirstOrDefault();
				if (dbRecord == null)
				{
					return false;
				}
				dbRecord.CustomerFK = order.CustomerFK;
				
				_orderRepository.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public List<OrderListEntity> GetOrders()
			=> JsonConvert.DeserializeObject<List<OrderListEntity>>(File.ReadAllText(Constants.Constants.Constants.ORDER_FILE_PATH));
		public void SaveOrders(List<OrderListEntity> orders)
			=> File.WriteAllText(Constants.Constants.Constants.ORDER_FILE_PATH, JsonConvert.SerializeObject(orders));
	}
}

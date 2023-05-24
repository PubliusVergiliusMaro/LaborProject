using LaborProjectOOP.Database.Models;
using LaborProjectOOP.EntityFramework.Repository;
using Microsoft.EntityFrameworkCore;

namespace LaborProjectOOP.Services.OrderHistoryServices
{
	public class OrderService : IOrderService
	{
		public readonly IGenericRepository<OrderEntity> _orderRepository;

		public OrderService(IGenericRepository<OrderEntity> orderRepository)
		{
			_orderRepository = orderRepository;
		}

		public void Create(OrderEntity order)
		{
			_orderRepository.Create(order);
		}
		public bool Delete(int id)
		{
			OrderEntity dbRecord = _orderRepository.Table
				.Include(ord => ord.Customer)
				.Include(ord => ord.Book)
				.FirstOrDefault(order => order.Id == id);
			if (dbRecord == null)
			{
				return false;
			}
			_orderRepository.Remove(dbRecord);
			return true;
		}
		public List<OrderEntity> GetAll()
		{
			List<OrderEntity> dbRecord = _orderRepository.Table
				.Include(ord => ord.Customer)
				.Include(ord => ord.Book)
			    .ToList();
			if (dbRecord == null)
			{
				return null;
			}
			return dbRecord;
		}
		public OrderEntity GetById(int id)
		{
			OrderEntity dbRecord = _orderRepository.Table
				.Include(ord => ord.Customer)
					.Include(ord => ord.Book)
				.Where(catalog => catalog.Id == id)
				.FirstOrDefault();
			if (dbRecord == null)
			{
				return null;
			}
			return dbRecord;
		}
		public bool Update(OrderEntity order)
		{
			try
			{
				OrderEntity dbRecord = _orderRepository.Table
					.Include(ord => ord.Customer)
					.Include(ord => ord.Book)
					.Where(ord => ord.Id == order.Id)
					.FirstOrDefault();
				if (dbRecord == null)
				{
					return false;
				}
				dbRecord.Customer = order.Customer;
				dbRecord.BookFK = order.BookFK;
				dbRecord.CreatedOn = order.CreatedOn;
				dbRecord.IsActual = order.IsActual;
				_orderRepository.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
	}
}

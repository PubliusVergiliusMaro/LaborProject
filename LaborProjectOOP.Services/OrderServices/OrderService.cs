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

		public async Task Create(OrderEntity order) => await _orderRepository.Create(order);
		public async Task<bool> Delete(int id)
		{
			OrderEntity dbRecord = await _orderRepository.Table
				.Include(ord => ord.Customer)
				.Include(ord => ord.Book)
				.FirstOrDefaultAsync(order => order.Id == id);
			if (dbRecord == null)
			{
				return false;
			}
			await _orderRepository.Delete(dbRecord);
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
		public async Task<OrderEntity> GetById(int id)
		{
			OrderEntity dbRecord = await _orderRepository.Table
				.Include(ord => ord.Customer)
					.Include(ord => ord.Book)
				.Where(catalog => catalog.Id == id)
				.FirstOrDefaultAsync();
			if (dbRecord == null)
			{
				return null;
			}
			return dbRecord;
		}
		public async Task<bool> Update(OrderEntity order)
		{
			try
			{
				OrderEntity dbRecord = await _orderRepository.Table
					.Include(ord => ord.Customer)
					.Include(ord => ord.Book)
					.Where(ord => ord.Id == order.Id)
					.FirstOrDefaultAsync();
				if (dbRecord == null)
				{
					return false;
				}
				dbRecord.Customer = order.Customer;
				dbRecord.BookFK = order.BookFK;
				dbRecord.CreatedOn = order.CreatedOn;
				dbRecord.IsActual = order.IsActual;
				await _orderRepository.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
	}
}

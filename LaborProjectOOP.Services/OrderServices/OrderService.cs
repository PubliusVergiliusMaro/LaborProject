using LaborProjectOOP.Constants.Constants;
using LaborProjectOOP.Database.Models;
using LaborProjectOOP.EntityFramework.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaborProjectOOP.Services.OrderServices
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
					.Where(ord => ord.Id == order.Id)
					.FirstOrDefault();
				if (dbRecord == null)
				{
					return false;
				}
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

		public List<OrderEntity> GetOrders()
			=> JsonConvert.DeserializeObject<List<OrderEntity>>(File.ReadAllText(Constants.Constants.Constants.ORDER_FILE_PATH));
		public void SaveOrders(List<OrderEntity> orders)
			=> File.WriteAllText(Constants.Constants.Constants.ORDER_FILE_PATH, JsonConvert.SerializeObject(orders));
	}
}

using LaborProjectOOP.Database.Models;
using LaborProjectOOP.EntityFramework.Repository;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace LaborProjectOOP.Services.CustomerServices
{
	public class CustomerService : ICustomerService
	{
		private readonly IGenericRepository<CustomerEntity> _customerRepository;

		public CustomerService(IGenericRepository<CustomerEntity> customerRepository)
		{
			_customerRepository = customerRepository;
		}

		public void Create(CustomerEntity customer)
		{
			_customerRepository.Create(customer);
		}
		public bool Delete(int id)
		{
			CustomerEntity dbRecord = _customerRepository.Table
				.Where(customer => customer.Id == id)
				.Include(customer => customer.Orders)
				.FirstOrDefault();
			if (dbRecord == null)
			{
				return false;
			}
			_customerRepository.Remove(dbRecord);
			return true;
		}
		public List<CustomerEntity> GetAll()
		{
			List<CustomerEntity> dbRecord = _customerRepository.Table
				.ToList();
			if (dbRecord == null)
			{
				return null;
			}
			return dbRecord;
		}
		public CustomerEntity GetById(int id)
		{
			CustomerEntity dbRecord = _customerRepository.Table
				.Where(customer => customer.Id == id)
				.Include(c => c.Orders)
				.FirstOrDefault();
			if (dbRecord == null)
			{
				return null;
			}
			return dbRecord;
		}
		public bool Update(CustomerEntity customer)
		{
			try
			{
				CustomerEntity dbRecord = _customerRepository.Table
					.Where(catalogus => catalogus.Id == customer.Id)
					.Include(c => c.Orders)
					.FirstOrDefault();
				if (dbRecord == null)
				{
					return false;
				}
				dbRecord.Login = customer.Login;
				dbRecord.Password = customer.Password;
				dbRecord.Email = customer.Email;
				dbRecord.Phone = customer.Phone;
				dbRecord.IsBanned = customer.IsBanned;
				dbRecord.Orders = customer.Orders;

				_customerRepository.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public List<CustomerEntity> GetCustomers() =>
			JsonConvert.DeserializeObject<List<CustomerEntity>>(File.ReadAllText(Constants.Constants.Constants.CUSTOMER_FILE_PATH));
		public void SaveCustomers(List<CustomerEntity> customers) =>
			File.WriteAllText(Constants.Constants.Constants.CUSTOMER_FILE_PATH, JsonConvert.SerializeObject(customers));
	}
}

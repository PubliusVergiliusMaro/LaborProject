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

		public async Task Create(CustomerEntity customer)=>	await _customerRepository.Create(customer);
		
		public async Task<bool> Delete(int id)
		{
			CustomerEntity dbRecord = await _customerRepository.Table
				.Where(customer => customer.Id == id)
				.Include(customer => customer.WishList)
					.ThenInclude(order => order.Book)
						.ThenInclude(book=>book.Author)
				.Include(customer => customer.CartList)
					.ThenInclude(order => order.Book)
						.ThenInclude(book => book.Author)
				.Include(customer => customer.Orders)
					.ThenInclude(order => order.Book)
						.ThenInclude(book => book.Author)
				.FirstOrDefaultAsync();
			if (dbRecord == null)
			{
				return false;
			}
			await _customerRepository.Delete(dbRecord);
			return true;
		}
		public List<CustomerEntity> GetAll()
		{
			List<CustomerEntity> dbRecord = _customerRepository.Table
				.Include(customer => customer.WishList)
				.Include(customer => customer.CartList)
				.Include(customer => customer.Orders)
				.ToList();
			if (dbRecord == null)
			{
				return null;
			}
			return dbRecord;
		}
		public async Task<CustomerEntity> GetById(int id)
		{
			CustomerEntity dbRecord = await _customerRepository.Table
				.Where(customer => customer.Id == id)
				.Include(customer => customer.WishList)
					.ThenInclude(order => order.Book)
						.ThenInclude(book => book.Author)
				.Include(customer => customer.CartList)
					.ThenInclude(order => order.Book)
						.ThenInclude(book => book.Author)
				.Include(customer => customer.Orders)
					.ThenInclude(order => order.Book)
						.ThenInclude(book => book.Author)
				.FirstOrDefaultAsync();
			if (dbRecord == null)
			{
				return null;
			}
			return dbRecord;
		}
		public async Task<bool> Update(CustomerEntity customer)
		{
			try
			{
				CustomerEntity dbRecord = await _customerRepository.Table
					.Where(catalogus => catalogus.Id == customer.Id)
					.Include(customer => customer.WishList)
					.Include(customer => customer.CartList)
					.Include(customer => customer.Orders)
					.FirstOrDefaultAsync();
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
				dbRecord.WishList = customer.WishList;
				dbRecord.CartList = customer.CartList;
				await _customerRepository.SaveChanges();
				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public async Task<List<CustomerEntity>> GetCustomersFromFile() =>
			JsonConvert.DeserializeObject<List<CustomerEntity>>(File.ReadAllText(Constants.Constants.Constants.CUSTOMER_FILE_PATH));
		public async Task SaveCustomersToFile(List<CustomerEntity> customers) =>
			File.WriteAllText(Constants.Constants.Constants.CUSTOMER_FILE_PATH, JsonConvert.SerializeObject(customers));
	}
}

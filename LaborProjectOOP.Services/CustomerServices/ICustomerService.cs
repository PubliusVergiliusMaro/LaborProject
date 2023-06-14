using LaborProjectOOP.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaborProjectOOP.Services.CustomerServices
{
	public interface ICustomerService
	{
		Task Create(CustomerEntity customer);
		Task<bool> Delete(int id);
		List<CustomerEntity> GetAll();
		Task<CustomerEntity> GetById(int id);
		Task<bool> Update(CustomerEntity customer);
		Task<List<CustomerEntity>> GetCustomersFromFile();
		Task SaveCustomersToFile(List<CustomerEntity> customers);
	}
}

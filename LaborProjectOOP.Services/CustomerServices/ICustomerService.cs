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
		void Create(CustomerEntity customer);
		bool Delete(int id);
		List<CustomerEntity> GetAll();
		CustomerEntity GetById(int id);
		bool Update(CustomerEntity customer);
		List<CustomerEntity> GetCustomers();
		void SaveCustomers(List<CustomerEntity> customers);
	}
}

using LaborProjectOOP.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaborProjectOOP.Services.LibrarianServices
{
	public interface ILibrarianService
	{
		Task Create(LibrarianEntity librarian);
		Task<bool> Delete(int id);
		List<LibrarianEntity> GetAll();
		Task<LibrarianEntity> GetById(int id);
		Task<bool> Update(LibrarianEntity librarian);

	}
}

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
		void Create(LibrarianEntity librarian);
		bool Delete(int id);
		List<LibrarianEntity> GetAll();
		LibrarianEntity GetById(int id);
		bool Update(LibrarianEntity librarian);

	}
}

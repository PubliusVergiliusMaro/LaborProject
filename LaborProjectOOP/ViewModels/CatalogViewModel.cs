using LaborProjectOOP.Database.Models;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LaborProjectOOP.Dekstop.ViewModels
{
	public class CatalogViewModel : ViewModelBase
	{
		private readonly CatalogEntity _catalog;
		public CatalogViewModel(CatalogEntity catalog)
		{
			_catalog = catalog;
			Books =	_catalog.Books.Select(book => new BookViewModel(book)).ToList();
		}
		public int Id => _catalog.Id;
		public string Name => _catalog.Name;
		public ICollection<BookViewModel> Books { get;}
	}
}

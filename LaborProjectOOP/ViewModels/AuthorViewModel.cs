using LaborProjectOOP.Database.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;

namespace LaborProjectOOP.Dekstop.ViewModels
{
	public class AuthorViewModel : ViewModelBase
	{
		private readonly AuthorEntity _authorEntity;
		public AuthorViewModel(AuthorEntity authorEntity)
		{
			_authorEntity = authorEntity;
			Books = _authorEntity.Books.Select(book => new BookViewModel(book)).ToList();
		}
		public int Id => _authorEntity.Id;
		public string Name => _authorEntity.Name;
		public string Surname => _authorEntity.Surname;
		public ICollection<BookViewModel> Books { get; }
	}
}

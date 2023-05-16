namespace LaborProjectOOP.Database.Models
{
	public class CatalogEntity //: IEnumerator 
	{
		public CatalogEntity()
		{
			Books = new List<BookEntity>();
		}
		public int Id { get; set; }
		public string Name { get; set; }
		public List<BookEntity> Books { get; set; }


		#region Lab3

		//private int _position=-1;
		//public object Current
		//{
		//	get
		//	{
		//		try
		//		{
		//			return Books[_position];
		//		}
		//		catch(Exception e)
		//		{
		//			throw new ArgumentException($"Error: {e.Message}");
		//		}
		//	}
		//}

		//public bool MoveNext()
		//{
		//	_position++;
		//	return (_position < Books.Count );
		//}

		//public void Reset()
		//{
		//	_position = -1;
		//}
		#endregion
	}
}

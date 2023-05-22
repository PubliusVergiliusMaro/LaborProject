﻿namespace LaborProjectOOP.Database.Models
{
	public class BookEntity
	{
		public BookEntity() 
		{
		    WishLists = new List<WishListEntity>();
			CartLists= new List<CartListEntity>();
		}
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public double Price { get; set; }
		public bool IsTaken { get; set; }
		public string ImagePath { get; set; }

		public AuthorEntity? Author { get; set; }
		public int? AuthorFK { get; set; }

		public CatalogEntity? Catalog { get; set; }
		public int? CatalogFK { get; set; }

		public OrderEntity? Order { get; set; }
		public int? OrderFK { get; set; }
		public ICollection<WishListEntity> WishLists { get; set; }
		public ICollection<CartListEntity> CartLists { get; set; }
	}
}

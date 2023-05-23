using LaborProjectOOP.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LaborProjectOOP.EntityFramework.Configurations
{
	public class BookConfiguration : IEntityTypeConfiguration<BookEntity>
	{
		public void Configure(EntityTypeBuilder<BookEntity> builder)
		{
			builder
				.ToTable("Books")
				.HasKey(b => b.Id);
			builder
				.HasOne(book => book.Catalog)
				.WithMany(catalog => catalog.Books)
				.HasForeignKey(book => book.CatalogFK)
				.OnDelete(DeleteBehavior.Cascade);
			builder
				.HasOne(book => book.Author)
				.WithMany(author => author.Books)
				.HasForeignKey(book => book.AuthorFK)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}

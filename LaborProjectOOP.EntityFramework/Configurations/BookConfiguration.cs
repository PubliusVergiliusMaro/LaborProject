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
				.HasOne<CatalogEntity>(book => book.Catalog)
				.WithMany(catalog => catalog.Books)
				.HasForeignKey(book => book.CatalogFK)
				.OnDelete(DeleteBehavior.Cascade);

			builder
				.HasOne<OrderEntity>(book => book.Order)
				.WithMany(order => order.Books)
				.HasForeignKey(book => book.OrderFK)
				.OnDelete(DeleteBehavior.Cascade);

			builder
				.HasOne<AuthorEntity>(book => book.Author)
				.WithMany(author => author.Books)
				.HasForeignKey(book => book.AuthorFK)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}

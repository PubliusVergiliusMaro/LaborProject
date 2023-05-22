using LaborProjectOOP.Database.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LaborProjectOOP.EntityFramework.Configurations
{
	internal class CartListConfiguration : IEntityTypeConfiguration<CartListEntity>
	{
		public void Configure(EntityTypeBuilder<CartListEntity> builder)
		{
			builder.ToTable("CartLists")
				.HasKey(cart => cart.Id);

			builder
				.HasOne(cart => cart.Customer)
				.WithMany(cust=>cust.CartList)
				.HasForeignKey(cart => cart.CustomerFK)
				.OnDelete(DeleteBehavior.Cascade);
				
				
			builder
				.HasOne(cart => cart.Book)
				.WithMany(book => book.CartLists)
				.HasForeignKey(cart => cart.BookFK)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}

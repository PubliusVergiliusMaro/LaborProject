using LaborProjectOOP.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LaborProjectOOP.EntityFramework.Configurations
{
	public class WishListConfiguration : IEntityTypeConfiguration<WishListEntity>
	{
		public void Configure(EntityTypeBuilder<WishListEntity> builder)
		{
			builder.ToTable("WishLists")
				.HasKey(w => w.Id);

            builder
				.HasOne(w => w.Customer)
				.WithMany(c=>c.WishList)
				.HasForeignKey(w=>w.CustomerFK)
				.OnDelete(DeleteBehavior.Cascade);
			builder
				.HasOne(w => w.Book)
				.WithMany(b => b.WishLists)
				.HasForeignKey(w => w.BookFK)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}

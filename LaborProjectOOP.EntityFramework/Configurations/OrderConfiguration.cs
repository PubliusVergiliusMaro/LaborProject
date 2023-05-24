using LaborProjectOOP.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LaborProjectOOP.EntityFramework.Configurations
{
	public class OrderConfiguration : IEntityTypeConfiguration<OrderEntity>
	{
		public void Configure(EntityTypeBuilder<OrderEntity> builder)
		{
			builder
				.ToTable("Orders")
				.HasKey(orderHistor => orderHistor.Id);
			builder
				.HasOne(o => o.Customer)
				.WithMany(cust => cust.Orders)
				.HasForeignKey(o => o.CustomerFK)
				.OnDelete(DeleteBehavior.Cascade);
			builder
				.HasOne(order => order.Book)
				.WithMany(book => book.OrderList)
				.HasForeignKey(order => order.BookFK)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}

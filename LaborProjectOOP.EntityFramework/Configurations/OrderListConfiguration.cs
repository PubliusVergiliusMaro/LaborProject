using LaborProjectOOP.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LaborProjectOOP.EntityFramework.Configurations
{
	public class OrderListConfiguration : IEntityTypeConfiguration<OrderListEntity>
	{
		public void Configure(EntityTypeBuilder<OrderListEntity> builder)
		{
			builder.ToTable("OrderLists")
			.HasKey(ord => ord.Id);

			builder
			.HasOne<CustomerEntity>(order => order.Customer)
			.WithOne(customer => customer.OrderList)
			.HasForeignKey<OrderListEntity>(order => order.CustomerFK)
			.OnDelete(DeleteBehavior.Cascade);
		}
	}
}

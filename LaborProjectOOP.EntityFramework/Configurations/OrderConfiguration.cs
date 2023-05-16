using LaborProjectOOP.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LaborProjectOOP.EntityFramework.Configurations
{
	public class OrderConfiguration : IEntityTypeConfiguration<OrderEntity>
	{
		public void Configure(EntityTypeBuilder<OrderEntity> builder)
		{
			builder.ToTable("Orders")
			.HasKey(ord => ord.Id);

			builder
			.HasOne<CustomerEntity>(order => order.Customer)
			.WithMany(customer => customer.Orders)
			.HasForeignKey(order => order.CustomerFK)
			.OnDelete(DeleteBehavior.Cascade);
		}
	}
}

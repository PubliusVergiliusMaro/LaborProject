using LaborProjectOOP.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LaborProjectOOP.EntityFramework.Configurations
{
	public class CustomerConfiguration : IEntityTypeConfiguration<CustomerEntity>
	{
		public void Configure(EntityTypeBuilder<CustomerEntity> builder)
		{
			builder.ToTable("Customers")
				.HasKey(cust => cust.Id);

			//builder
			//	.HasOne<OrderEntity>(cust => cust.Order)
			//	.WithOne(order => order.Customer)
			//	.HasForeignKey<CustomerEntity>(cust => cust.OrderFK)
			//	.OnDelete(DeleteBehavior.Cascade);
		}
	}
}

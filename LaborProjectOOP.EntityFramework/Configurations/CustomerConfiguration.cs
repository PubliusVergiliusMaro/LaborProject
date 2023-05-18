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
		}
	}
}

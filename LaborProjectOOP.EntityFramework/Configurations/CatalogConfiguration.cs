using LaborProjectOOP.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LaborProjectOOP.EntityFramework.Configurations
{
	public class CatalogConfiguration : IEntityTypeConfiguration<CatalogEntity>
	{
		public void Configure(EntityTypeBuilder<CatalogEntity> builder)
		{
			builder
				.ToTable("Catalogs")
				.HasKey(c => c.Id);
		}
	}
}

using LaborProjectOOP.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LaborProjectOOP.EntityFramework.Configurations
{
	public class LibrarianConfiguration : IEntityTypeConfiguration<LibrarianEntity>
	{
		public void Configure(EntityTypeBuilder<LibrarianEntity> builder)
		{
			builder.ToTable("Librarians")
				.HasKey(libr => libr.Id);
		}
	}
}

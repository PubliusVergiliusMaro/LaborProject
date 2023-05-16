using LaborProjectOOP.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LaborProjectOOP.EntityFramework.Configurations
{
	public class AuthorConfiguration : IEntityTypeConfiguration<AuthorEntity>
	{
		public void Configure(EntityTypeBuilder<AuthorEntity> builder)
		{
			builder
				.ToTable("Authors")
				.HasKey(a => a.Id);
			builder
				.HasMany(author => author.Books)
				.WithOne(book => book.Author)
				.HasForeignKey(book => book.AuthorFK)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}

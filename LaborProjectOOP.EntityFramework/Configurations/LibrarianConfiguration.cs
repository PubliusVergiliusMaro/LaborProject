using LaborProjectOOP.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Security.Cryptography;
using System.Text;

namespace LaborProjectOOP.EntityFramework.Configurations
{
	public class LibrarianConfiguration : IEntityTypeConfiguration<LibrarianEntity>
	{
		public void Configure(EntityTypeBuilder<LibrarianEntity> builder)
		{
			builder.ToTable("Librarians")
				.HasKey(libr => libr.Id);

			builder.HasData(new LibrarianEntity
			{
				Id = 1,
				Login = "admin",
				Password = GetMD5Hash("1"),
				IsAdmin= true,
			});
		}
		public static string GetMD5Hash(string key)
		{
			using (MD5 md5 = MD5.Create())
			{
				byte[] inputBytes = Encoding.UTF8.GetBytes(key);
				byte[] hashBytes = md5.ComputeHash(inputBytes);
				string hashString = BitConverter.ToString(hashBytes).Replace("-", string.Empty);
				return hashString;
			}
		}
	}

}

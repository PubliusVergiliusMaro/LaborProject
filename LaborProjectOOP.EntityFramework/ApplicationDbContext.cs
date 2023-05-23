using LaborProjectOOP.Constants.DbConfiguration;
using LaborProjectOOP.Database.Models;
using LaborProjectOOP.EntityFramework.Configurations;
using Microsoft.EntityFrameworkCore;

namespace LaborProjectOOP.EntityFramework
{
	public class ApplicationDbContext : DbContext
	{
		public DbSet<AuthorEntity> Authors { get; set; }
		public DbSet<BookEntity> Books { get; set; }
		public DbSet<CatalogEntity> Catalogs { get; set; }
		public DbSet<CustomerEntity> Customers { get; set; }
		public DbSet<LibrarianEntity> Librarians { get; set; }
		public DbSet<OrderListEntity> OrderLists { get; set; }
		public DbSet<WishListEntity> WishLists { get; set; }
		public DbSet<CartListEntity> CartLists { get; set; }
		public DbSet<OrderEntity> Orders { get; set; }
		public ApplicationDbContext()
		{
			Database.Migrate();
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseNpgsql(DbConfiguration.CONECTION_STRING_POSTGRESQL);
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new AuthorConfiguration());
			modelBuilder.ApplyConfiguration(new BookConfiguration());
			modelBuilder.ApplyConfiguration(new CatalogConfiguration());
			modelBuilder.ApplyConfiguration(new CustomerConfiguration());
			modelBuilder.ApplyConfiguration(new LibrarianConfiguration());
			modelBuilder.ApplyConfiguration(new OrderListConfiguration());
			modelBuilder.ApplyConfiguration(new WishListConfiguration());
			modelBuilder.ApplyConfiguration(new CartListConfiguration());
			modelBuilder.ApplyConfiguration(new OrderConfiguration());
		}
	}
}

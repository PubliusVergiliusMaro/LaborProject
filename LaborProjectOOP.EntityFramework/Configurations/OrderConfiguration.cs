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
				.HasOne(oh => oh.OrderList)
				.WithMany(orderList => orderList.Orders)
				.HasForeignKey(oh => oh.OrderListFK)
				.OnDelete(DeleteBehavior.Cascade);
			builder
				.HasOne(order => order.Book)
				.WithMany(book => book.OrderList)
				.HasForeignKey(order => order.BookFK)
				.OnDelete(DeleteBehavior.Cascade);
			/*
			 * Npgsql.PostgresException: "23503: insert або update в таблиці "Orders" 
			 * порушує обмеження зовнішнього ключа "FK_Orders_Books_BookFK"

               DETAIL: Detail redacted as it may contain sensitive data. 
			Specify 'Include Error Detail' in the connection string to include this information."
			 */
		}
	}
}

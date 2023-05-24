using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaborProjectOOP.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCustomerConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderListFK",
                table: "Customers",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderListFK",
                table: "Customers");
        }
    }
}

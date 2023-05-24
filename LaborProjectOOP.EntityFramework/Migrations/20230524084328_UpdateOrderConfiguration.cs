using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LaborProjectOOP.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_OrderLists_OrderListFK",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "OrderLists");

            migrationBuilder.DropIndex(
                name: "IX_Orders_OrderListFK",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderListFK",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "CustomerFK",
                table: "Orders",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerFK",
                table: "Orders",
                column: "CustomerFK");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_CustomerFK",
                table: "Orders",
                column: "CustomerFK",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_CustomerFK",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CustomerFK",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CustomerFK",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "OrderListFK",
                table: "Orders",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrderLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerFK = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderLists_Customers_CustomerFK",
                        column: x => x.CustomerFK,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_OrderListFK",
                table: "Orders",
                column: "OrderListFK");

            migrationBuilder.CreateIndex(
                name: "IX_OrderLists_CustomerFK",
                table: "OrderLists",
                column: "CustomerFK",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_OrderLists_OrderListFK",
                table: "Orders",
                column: "OrderListFK",
                principalTable: "OrderLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

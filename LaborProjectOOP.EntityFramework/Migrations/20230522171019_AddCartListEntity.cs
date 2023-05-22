using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LaborProjectOOP.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddCartListEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CartLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerFK = table.Column<int>(type: "integer", nullable: false),
                    BookFK = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartLists_Books_BookFK",
                        column: x => x.BookFK,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartLists_Customers_CustomerFK",
                        column: x => x.CustomerFK,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartLists_BookFK",
                table: "CartLists",
                column: "BookFK");

            migrationBuilder.CreateIndex(
                name: "IX_CartLists_CustomerFK",
                table: "CartLists",
                column: "CustomerFK");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartLists");
        }
    }
}

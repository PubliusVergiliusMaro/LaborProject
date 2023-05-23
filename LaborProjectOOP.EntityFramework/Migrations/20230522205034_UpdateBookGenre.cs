using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaborProjectOOP.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBookGenre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int[]>(
                name: "Genres",
                table: "Books",
                type: "integer[]",
                nullable: false,
                defaultValue: new int[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Genres",
                table: "Books");
        }
    }
}

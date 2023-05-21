using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaborProjectOOP.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Librarians",
                columns: new[] { "Id", "IsAdmin", "Login", "Password", "Salary", "WorkExperience" },
                values: new object[] { 1, true, "admin", "C4CA4238A0B923820DCC509A6F75849B", 0, (byte)0 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Librarians",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}

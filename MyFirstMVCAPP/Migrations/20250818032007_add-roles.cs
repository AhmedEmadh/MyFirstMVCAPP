using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyFirstMVCAPP.Migrations
{
    /// <inheritdoc />
    public partial class addroles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "74509dd5-479e-4642-88aa-69d4766b4627", "f56285d3-0deb-453a-90b2-c27a938aa89a", "Admin", "admin" },
                    { "f75cbefc-74e4-44cc-9793-ababb29c825f", "d57e6aa4-6816-424b-954b-7c557f81d181", "User", "user" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "74509dd5-479e-4642-88aa-69d4766b4627");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f75cbefc-74e4-44cc-9793-ababb29c825f");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFirstMVCAPP.Migrations
{
    /// <inheritdoc />
    public partial class addimagepathtoItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "imagePath",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "imagePath",
                table: "Items");
        }
    }
}

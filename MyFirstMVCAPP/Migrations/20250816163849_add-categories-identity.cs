using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyFirstMVCAPP.Migrations
{
    /// <inheritdoc />
    public partial class addcategoriesidentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // 1. Drop foreign key from Items to Categories
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Categories_CategoryId",
                table: "Items");

            // 2. Drop primary key on Categories
            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            // 3. Drop the old Id column
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Categories");

            // 4. Add Id column back as IDENTITY
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            // 5. Re-add primary key
            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            // 6. Re-add foreign key
            migrationBuilder.AddForeignKey(
                name: "FK_Items_Categories_CategoryId",
                table: "Items",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // 1. Drop foreign key
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Categories_CategoryId",
                table: "Items");

            // 2. Drop primary key
            migrationBuilder.DropPrimaryKey(
                name: "PK_Categories",
                table: "Categories");

            // 3. Drop Id column (IDENTITY version)
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Categories");

            // 4. Recreate Id column as normal int (no identity)
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            // 5. Re-add primary key
            migrationBuilder.AddPrimaryKey(
                name: "PK_Categories",
                table: "Categories",
                column: "Id");

            // 6. Re-add foreign key
            migrationBuilder.AddForeignKey(
                name: "FK_Items_Categories_CategoryId",
                table: "Items",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }


    }
}

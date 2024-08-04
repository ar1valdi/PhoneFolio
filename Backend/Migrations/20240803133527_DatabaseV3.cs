using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendRestAPI.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowCustomSubcategories",
                table: "Categories");

            migrationBuilder.AddColumn<short>(
                name: "Policy",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Policy",
                table: "Categories");

            migrationBuilder.AddColumn<bool>(
                name: "AllowCustomSubcategories",
                table: "Categories",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WineTime.Infrastructure.Migrations
{
    public partial class SortData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Products",
                newName: "Sort");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Sort",
                table: "Products",
                newName: "Type");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WineTime.Infrastructure.Migrations
{
    public partial class DegUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.AddColumn<int>(
                name: "DegustationId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DegustationId",
                table: "AspNetUsers",
                column: "DegustationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Degustations_DegustationId",
                table: "AspNetUsers",
                column: "DegustationId",
                principalTable: "Degustations",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Degustations_DegustationId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DegustationId",
                table: "AspNetUsers");

        }
    }
}

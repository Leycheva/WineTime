using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WineTime.Infrastructure.Migrations
{
    public partial class UserDegustation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Degustations_DegustationId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DegustationId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DegustationId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "UserDegustatuions",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DegustationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDegustatuions", x => new { x.DegustationId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserDegustatuions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserDegustatuions_Degustations_DegustationId",
                        column: x => x.DegustationId,
                        principalTable: "Degustations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserDegustatuions_UserId",
                table: "UserDegustatuions",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserDegustatuions");

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
    }
}

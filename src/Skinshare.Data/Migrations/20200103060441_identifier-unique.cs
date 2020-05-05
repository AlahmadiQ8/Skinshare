using Microsoft.EntityFrameworkCore.Migrations;

namespace Skinshare.Data.Migrations
{
    public partial class identifierunique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Routines_Identifier",
                table: "Routines");

            migrationBuilder.CreateIndex(
                name: "IX_Routines_Identifier",
                table: "Routines",
                column: "Identifier",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Routines_Identifier",
                table: "Routines");

            migrationBuilder.CreateIndex(
                name: "IX_Routines_Identifier",
                table: "Routines",
                column: "Identifier");
        }
    }
}

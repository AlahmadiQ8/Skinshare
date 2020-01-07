using Microsoft.EntityFrameworkCore.Migrations;
using Skinshare.Core.Entities;

namespace Skinshare.Data.Migrations
{
    public partial class use_PartOfDay_enums : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"ALTER TABLE ""Steps"" ALTER ""PartOfDay"" TYPE smallint USING (""PartOfDay""::smallint), ALTER ""PartOfDay"" SET NOT NULL;");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Steps",
                maxLength: 300,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Routines",
                maxLength: 140,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Routines",
                maxLength: 280,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PartOfDay",
                table: "Steps",
                type: "text",
                nullable: false,
                oldClrType: typeof(byte));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Steps",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 300);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Routines",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 140);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Routines",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 280,
                oldNullable: true);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitAsystent.Migrations
{
    /// <inheritdoc />
    public partial class DodanieDaty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthRecords_AspNetUsers_UserID",
                table: "HealthRecords");

            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "HealthRecords",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_HealthRecords_UserID",
                table: "HealthRecords",
                newName: "IX_HealthRecords_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "HealthRecords",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataPomiaru",
                table: "HealthRecords",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_HealthRecords_AspNetUsers_UserId",
                table: "HealthRecords",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HealthRecords_AspNetUsers_UserId",
                table: "HealthRecords");

            migrationBuilder.DropColumn(
                name: "DataPomiaru",
                table: "HealthRecords");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "HealthRecords",
                newName: "UserID");

            migrationBuilder.RenameIndex(
                name: "IX_HealthRecords_UserId",
                table: "HealthRecords",
                newName: "IX_HealthRecords_UserID");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "HealthRecords",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HealthRecords_AspNetUsers_UserID",
                table: "HealthRecords",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

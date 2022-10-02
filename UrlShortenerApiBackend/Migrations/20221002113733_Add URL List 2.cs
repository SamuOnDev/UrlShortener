using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrlShortenerApiBackend.Migrations
{
    public partial class AddURLList2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "UrlLists");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "UrlLists");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "UrlLists");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "UrlLists");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "UrlLists");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "UrlLists");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "UrlLists");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "UrlLists",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "UrlLists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "UrlLists",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "UrlLists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "UrlLists",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "UrlLists",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "UrlLists",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

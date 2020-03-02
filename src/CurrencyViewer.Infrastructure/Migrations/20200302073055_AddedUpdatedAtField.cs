using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CurrencyViewer.Infrastructure.Migrations
{
    public partial class AddedUpdatedAtField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "CurrencyRates",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "CurrencyRates");
        }
    }
}

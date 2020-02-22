using Microsoft.EntityFrameworkCore.Migrations;

namespace CurrencyViewer.Infrastructure.Migrations
{
    public partial class Bid_and_Ask_Values : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "CurrencyRates");

            migrationBuilder.AddColumn<double>(
                name: "AskValue",
                table: "CurrencyRates",
                nullable: false);

            migrationBuilder.AddColumn<double>(
                name: "BidValue",
                table: "CurrencyRates",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AskValue",
                table: "CurrencyRates");

            migrationBuilder.DropColumn(
                name: "BidValue",
                table: "CurrencyRates");

            migrationBuilder.AddColumn<double>(
                name: "Value",
                table: "CurrencyRates",
                type: "double precision",
                nullable: false);
        }
    }
}

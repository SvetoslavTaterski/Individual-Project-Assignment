using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GepardOOD.Data.Migrations
{
    public partial class AddingIsActiveColumnForSoftDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Wines",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Whiskeys",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Sodas",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Beers",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.UpdateData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://www.sid-shop.com/media/catalog/product/cache/c0afec666176687e071d6d0731b8af90/p/i/pirinsko-ken_doyif7byweomzrew.png");

            migrationBuilder.UpdateData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://upload.wikimedia.org/wikipedia/commons/3/3a/Ariana_beer.BG.2.JPG");

            migrationBuilder.UpdateData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "https://images.unsplash.com/photo-1618885472179-5e474019f2a9?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=387&q=80");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Wines");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Whiskeys");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Sodas");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Beers");

            migrationBuilder.UpdateData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://www.carlsberggroup.com/products/pirinsko/pirinsko-pivo/");

            migrationBuilder.UpdateData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 2,
                column: "ImageUrl",
                value: "https://www.monde-selection.com/product/ariana-lager/");

            migrationBuilder.UpdateData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 3,
                column: "ImageUrl",
                value: "https://www.wikiwand.com/en/Heineken");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GepardOOD.Data.Migrations
{
    public partial class SeedDatabase1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BeerCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Lager" },
                    { 2, "Ale" },
                    { 3, "Hybrid" }
                });

            migrationBuilder.InsertData(
                table: "SodaCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "With Sugar" },
                    { 2, "Zero Sugar" }
                });

            migrationBuilder.InsertData(
                table: "WhiskeyCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Scotch" },
                    { 2, "Bourbon" }
                });

            migrationBuilder.InsertData(
                table: "WineCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Red" },
                    { 2, "White" },
                    { 3, "Rose" }
                });

            migrationBuilder.InsertData(
                table: "Beers",
                columns: new[] { "Id", "AssociateId", "BeerCategoryId", "ClientId", "Description", "ImageUrl", "Manufacturer", "Name", "Price" },
                values: new object[] { 1, new Guid("48942044-ce1f-4743-9fec-15c6808bb427"), 1, new Guid("61a22398-32d9-4adb-edaf-08db7b0b2a29"), "Pirinsko Lager is a refreshing pale lager with a well-balanced bitterness, fine hops aroma and a light amber colour. This beer is a thirst quencher and complements a wide variety of dishes well.", "https://www.carlsberggroup.com/products/pirinsko/pirinsko-pivo/", "Carlsberg", "Pirinsko", 2m });

            migrationBuilder.InsertData(
                table: "Beers",
                columns: new[] { "Id", "AssociateId", "BeerCategoryId", "ClientId", "Description", "ImageUrl", "Manufacturer", "Name", "Price" },
                values: new object[] { 2, new Guid("48942044-ce1f-4743-9fec-15c6808bb427"), 1, new Guid("61a22398-32d9-4adb-edaf-08db7b0b2a29"), "Ariana is a Bulgarian beer brand, produced by the Zagorka Brewery since 2004.", "https://www.monde-selection.com/product/ariana-lager/", "Zagorka", "Ariana", 2m });

            migrationBuilder.InsertData(
                table: "Beers",
                columns: new[] { "Id", "AssociateId", "BeerCategoryId", "ClientId", "Description", "ImageUrl", "Manufacturer", "Name", "Price" },
                values: new object[] { 3, new Guid("48942044-ce1f-4743-9fec-15c6808bb427"), 1, new Guid("61a22398-32d9-4adb-edaf-08db7b0b2a29"), "Heineken Lager Beer (Dutch: Heineken Pilsener), or simply Heineken (pronounced [ˈɦɛinəkə(n)]), is a Dutch pale lager beer with 5% alcohol by volume produced by the Dutch brewing company Heineken N.V.", "https://www.wikiwand.com/en/Heineken", "Heineken N.V.", "Heineken", 3m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BeerCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BeerCategories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Beers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SodaCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SodaCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "WhiskeyCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WhiskeyCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "WineCategories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WineCategories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "WineCategories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BeerCategories",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}

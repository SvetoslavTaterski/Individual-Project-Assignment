using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GepardOOD.Data.Migrations
{
    public partial class orders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "Wines",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "Whiskeys",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "Sodas",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrderId",
                table: "Beers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Wines_OrderId",
                table: "Wines",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Whiskeys_OrderId",
                table: "Whiskeys",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Sodas_OrderId",
                table: "Sodas",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Beers_OrderId",
                table: "Beers",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Order_UserId",
                table: "Order",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Beers_Order_OrderId",
                table: "Beers",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sodas_Order_OrderId",
                table: "Sodas",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Whiskeys_Order_OrderId",
                table: "Whiskeys",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Wines_Order_OrderId",
                table: "Wines",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Beers_Order_OrderId",
                table: "Beers");

            migrationBuilder.DropForeignKey(
                name: "FK_Sodas_Order_OrderId",
                table: "Sodas");

            migrationBuilder.DropForeignKey(
                name: "FK_Whiskeys_Order_OrderId",
                table: "Whiskeys");

            migrationBuilder.DropForeignKey(
                name: "FK_Wines_Order_OrderId",
                table: "Wines");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Wines_OrderId",
                table: "Wines");

            migrationBuilder.DropIndex(
                name: "IX_Whiskeys_OrderId",
                table: "Whiskeys");

            migrationBuilder.DropIndex(
                name: "IX_Sodas_OrderId",
                table: "Sodas");

            migrationBuilder.DropIndex(
                name: "IX_Beers_OrderId",
                table: "Beers");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Wines");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Whiskeys");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Sodas");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Beers");
        }
    }
}

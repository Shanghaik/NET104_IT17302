using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shopping_Project.Migrations
{
    public partial class acc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BillDetails_Products_ProductId1",
                table: "BillDetails");

            migrationBuilder.DropIndex(
                name: "IX_BillDetails_ProductId1",
                table: "BillDetails");

            migrationBuilder.DropColumn(
                name: "ProductId1",
                table: "BillDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProductId1",
                table: "BillDetails",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BillDetails_ProductId1",
                table: "BillDetails",
                column: "ProductId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BillDetails_Products_ProductId1",
                table: "BillDetails",
                column: "ProductId1",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}

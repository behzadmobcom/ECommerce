using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ecommerce.API.Migrations
{
    public partial class AddSanadCodeCustomrToTraqnsaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrders_Transactions_TransactionId",
                table: "PurchaseOrders");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrders_TransactionId",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "PurchaseOrders");

            migrationBuilder.AddColumn<int>(
                name: "PurchaseOrderId",
                table: "Transactions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SanadCodeCustomer",
                table: "Transactions",
                type: "int",
                nullable: true);

       
            migrationBuilder.CreateIndex(
                name: "IX_Transactions_PurchaseOrderId",
                table: "Transactions",
                column: "PurchaseOrderId",
                unique: true,
                filter: "[PurchaseOrderId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_PurchaseOrders_PurchaseOrderId",
                table: "Transactions",
                column: "PurchaseOrderId",
                principalTable: "PurchaseOrders",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_PurchaseOrders_PurchaseOrderId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_PurchaseOrderId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "PurchaseOrderId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "SanadCodeCustomer",
                table: "Transactions");

            migrationBuilder.AddColumn<int>(
                name: "TransactionId",
                table: "PurchaseOrders",
                type: "int",
                nullable: true);

          migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_TransactionId",
                table: "PurchaseOrders",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrders_Transactions_TransactionId",
                table: "PurchaseOrders",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id");
        }
    }
}

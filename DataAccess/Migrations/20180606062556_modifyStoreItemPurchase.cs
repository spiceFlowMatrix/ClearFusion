using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class modifyStoreItemPurchase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "StoreItemPurchases");

            migrationBuilder.RenameColumn(
                name: "ImageName",
                table: "StoreItemPurchases",
                newName: "InvoiceFileType");

            migrationBuilder.RenameColumn(
                name: "ImageGuid",
                table: "StoreItemPurchases",
                newName: "InvoiceFileName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "InvoiceFileType",
                table: "StoreItemPurchases",
                newName: "ImageName");

            migrationBuilder.RenameColumn(
                name: "InvoiceFileName",
                table: "StoreItemPurchases",
                newName: "ImageGuid");

            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "StoreItemPurchases",
                nullable: true);
        }
    }
}

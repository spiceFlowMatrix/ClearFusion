using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class itemSpecificationTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ItemSpecificationMaster",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemSpecificationMasterId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ItemSpecificationField = table.Column<string>(nullable: true),
                    OfficeId = table.Column<int>(nullable: false),
                    ItemTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemSpecificationMaster", x => x.ItemSpecificationMasterId);
                    table.ForeignKey(
                        name: "FK_ItemSpecificationMaster_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemSpecificationMaster_InventoryItemType_ItemTypeId",
                        column: x => x.ItemTypeId,
                        principalTable: "InventoryItemType",
                        principalColumn: "ItemType",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemSpecificationMaster_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemSpecificationMaster_OfficeDetail_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "OfficeDetail",
                        principalColumn: "OfficeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemSpecificationDetails",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemSpecificationDetailsId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ItemSpecificationMasterId = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemSpecificationDetails", x => x.ItemSpecificationDetailsId);
                    table.ForeignKey(
                        name: "FK_ItemSpecificationDetails_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemSpecificationDetails_ItemSpecificationMaster_ItemSpecificationMasterId",
                        column: x => x.ItemSpecificationMasterId,
                        principalTable: "ItemSpecificationMaster",
                        principalColumn: "ItemSpecificationMasterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemSpecificationDetails_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemSpecificationDetails_CreatedById",
                table: "ItemSpecificationDetails",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSpecificationDetails_ItemSpecificationMasterId",
                table: "ItemSpecificationDetails",
                column: "ItemSpecificationMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSpecificationDetails_ModifiedById",
                table: "ItemSpecificationDetails",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSpecificationMaster_CreatedById",
                table: "ItemSpecificationMaster",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSpecificationMaster_ItemTypeId",
                table: "ItemSpecificationMaster",
                column: "ItemTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSpecificationMaster_ModifiedById",
                table: "ItemSpecificationMaster",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSpecificationMaster_OfficeId",
                table: "ItemSpecificationMaster",
                column: "OfficeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemSpecificationDetails");

            migrationBuilder.DropTable(
                name: "ItemSpecificationMaster");
        }
    }
}

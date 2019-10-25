using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class AddedTableLogisticItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectLogisticItems",
                columns: table => new
                {
                    LogisticItemId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Quantity = table.Column<long>(nullable: false),
                    EstimatedCost = table.Column<double>(nullable: false),
                    ItemId = table.Column<long>(nullable: false),
                    LogisticRequestsId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectLogisticItems", x => x.LogisticItemId);
                    table.ForeignKey(
                        name: "FK_ProjectLogisticItems_InventoryItems_ItemId",
                        column: x => x.ItemId,
                        principalTable: "InventoryItems",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectLogisticItems_ProjectLogisticRequests_LogisticReques~",
                        column: x => x.LogisticRequestsId,
                        principalTable: "ProjectLogisticRequests",
                        principalColumn: "LogisticRequestsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectLogisticItems_ItemId",
                table: "ProjectLogisticItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectLogisticItems_LogisticRequestsId",
                table: "ProjectLogisticItems",
                column: "LogisticRequestsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectLogisticItems");
        }
    }
}

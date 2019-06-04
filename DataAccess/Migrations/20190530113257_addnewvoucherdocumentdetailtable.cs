using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class addnewvoucherdocumentdetailtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VoucherDocumentDetail",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    VoucherDocumentId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    VoucherNo = table.Column<long>(nullable: false),
                    DocumentFileId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoucherDocumentDetail", x => x.VoucherDocumentId);
                    table.ForeignKey(
                        name: "FK_VoucherDocumentDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VoucherDocumentDetail_DocumentFileDetail_DocumentFileId",
                        column: x => x.DocumentFileId,
                        principalTable: "DocumentFileDetail",
                        principalColumn: "DocumentFileId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VoucherDocumentDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VoucherDocumentDetail_VoucherDetail_VoucherNo",
                        column: x => x.VoucherNo,
                        principalTable: "VoucherDetail",
                        principalColumn: "VoucherNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VoucherDocumentDetail_CreatedById",
                table: "VoucherDocumentDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherDocumentDetail_DocumentFileId",
                table: "VoucherDocumentDetail",
                column: "DocumentFileId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherDocumentDetail_ModifiedById",
                table: "VoucherDocumentDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherDocumentDetail_VoucherNo",
                table: "VoucherDocumentDetail",
                column: "VoucherNo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VoucherDocumentDetail");
        }
    }
}

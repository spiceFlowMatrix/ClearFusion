using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class removeoldvoucherdocumentdetailtable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VoucherDocumentDetail");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VoucherDocumentDetail",
                columns: table => new
                {
                    DocumentID = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    DocumentDate = table.Column<DateTime>(nullable: true),
                    DocumentFilePath = table.Column<string>(nullable: true),
                    DocumentGUID = table.Column<string>(nullable: true),
                    DocumentName = table.Column<string>(maxLength: 100, nullable: true),
                    DocumentType = table.Column<int>(nullable: true),
                    Extension = table.Column<string>(nullable: true),
                    FilePath = table.Column<byte[]>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    VoucherNo = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoucherDocumentDetail", x => x.DocumentID);
                    table.ForeignKey(
                        name: "FK_VoucherDocumentDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "IX_VoucherDocumentDetail_ModifiedById",
                table: "VoucherDocumentDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherDocumentDetail_VoucherNo",
                table: "VoucherDocumentDetail",
                column: "VoucherNo");
        }
    }
}

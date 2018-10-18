using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class EmployeePaymentTypesForeignKeyAdvanceId1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePaymentTypes_AdvanceDetail_AdvanceId",
                table: "EmployeePaymentTypes");

            migrationBuilder.DropTable(
                name: "AdvanceDetail");

            migrationBuilder.AlterColumn<int>(
                name: "AdvanceId",
                table: "EmployeePaymentTypes",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePaymentTypes_Advances_AdvanceId",
                table: "EmployeePaymentTypes",
                column: "AdvanceId",
                principalTable: "Advances",
                principalColumn: "AdvancesId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePaymentTypes_Advances_AdvanceId",
                table: "EmployeePaymentTypes");

            migrationBuilder.AlterColumn<long>(
                name: "AdvanceId",
                table: "EmployeePaymentTypes",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "AdvanceDetail",
                columns: table => new
                {
                    AdvanceId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AdvanceAmount = table.Column<float>(nullable: true),
                    AdvanceDate = table.Column<DateTime>(nullable: true),
                    ApprovedBy = table.Column<string>(maxLength: 50, nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CurrencyCode = table.Column<string>(maxLength: 10, nullable: true),
                    Description = table.Column<string>(maxLength: 200, nullable: true),
                    EmployeeId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ModeOfReturn = table.Column<string>(maxLength: 50, nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    RegCode = table.Column<string>(maxLength: 10, nullable: true),
                    RequestAmount = table.Column<float>(nullable: true),
                    VoucherReferenceNo = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvanceDetail", x => x.AdvanceId);
                    table.ForeignKey(
                        name: "FK_AdvanceDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdvanceDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdvanceDetail_CreatedById",
                table: "AdvanceDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AdvanceDetail_ModifiedById",
                table: "AdvanceDetail",
                column: "ModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePaymentTypes_AdvanceDetail_AdvanceId",
                table: "EmployeePaymentTypes",
                column: "AdvanceId",
                principalTable: "AdvanceDetail",
                principalColumn: "AdvanceId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

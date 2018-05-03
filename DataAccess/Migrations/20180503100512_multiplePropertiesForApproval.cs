using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataAccess.Migrations
{
    public partial class multiplePropertiesForApproval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdvanceDetail");

            //migrationBuilder.DropIndex(
            //    name: "IX_Advances_EmployeeId",
            //    table: "Advances");

            migrationBuilder.AddColumn<double>(
                name: "AdvanceAmount",
                table: "EmployeePaymentTypes",
                type: "float8",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "IsAdvanceApproved",
                table: "EmployeePaymentTypes",
                type: "bool",
                nullable: false,
                defaultValue: false);

            //migrationBuilder.CreateIndex(
            //    name: "IX_Advances_EmployeeId",
            //    table: "Advances",
            //    column: "EmployeeId",
            //    unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropIndex(
            //    name: "IX_Advances_EmployeeId",
            //    table: "Advances");

            migrationBuilder.DropColumn(
                name: "AdvanceAmount",
                table: "EmployeePaymentTypes");

            migrationBuilder.DropColumn(
                name: "IsAdvanceApproved",
                table: "EmployeePaymentTypes");

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
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    CurrencyCode = table.Column<string>(maxLength: 10, nullable: true),
                    Description = table.Column<string>(maxLength: 200, nullable: true),
                    EmployeeId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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

            //migrationBuilder.CreateIndex(
            //    name: "IX_Advances_EmployeeId",
            //    table: "Advances",
            //    column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvanceDetail_CreatedById",
                table: "AdvanceDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AdvanceDetail_ModifiedById",
                table: "AdvanceDetail",
                column: "ModifiedById");
        }
    }
}

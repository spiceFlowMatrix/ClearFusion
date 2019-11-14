using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class AddTblLogisticSupplier : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HiringRequestCandidateStatus_CandidateDetails_CandidateId",
                table: "HiringRequestCandidateStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_HiringRequestCandidateStatus_EmployeeDetail_EmployeeID",
                table: "HiringRequestCandidateStatus");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeID",
                table: "HiringRequestCandidateStatus",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "CandidateId",
                table: "HiringRequestCandidateStatus",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.CreateTable(
                name: "ProjectLogisticSuppliers",
                columns: table => new
                {
                    SupplierId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    SupplierName = table.Column<string>(nullable: true),
                    Quantity = table.Column<long>(nullable: false),
                    FinalPrice = table.Column<double>(nullable: false),
                    LogisticRequestsId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectLogisticSuppliers", x => x.SupplierId);
                    table.ForeignKey(
                        name: "FK_ProjectLogisticSuppliers_ProjectLogisticRequests_LogisticRe~",
                        column: x => x.LogisticRequestsId,
                        principalTable: "ProjectLogisticRequests",
                        principalColumn: "LogisticRequestsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectLogisticSuppliers_LogisticRequestsId",
                table: "ProjectLogisticSuppliers",
                column: "LogisticRequestsId");

            migrationBuilder.AddForeignKey(
                name: "FK_HiringRequestCandidateStatus_CandidateDetails_CandidateId",
                table: "HiringRequestCandidateStatus",
                column: "CandidateId",
                principalTable: "CandidateDetails",
                principalColumn: "CandidateId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_HiringRequestCandidateStatus_EmployeeDetail_EmployeeID",
                table: "HiringRequestCandidateStatus",
                column: "EmployeeID",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HiringRequestCandidateStatus_CandidateDetails_CandidateId",
                table: "HiringRequestCandidateStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_HiringRequestCandidateStatus_EmployeeDetail_EmployeeID",
                table: "HiringRequestCandidateStatus");

            migrationBuilder.DropTable(
                name: "ProjectLogisticSuppliers");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeID",
                table: "HiringRequestCandidateStatus",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "CandidateId",
                table: "HiringRequestCandidateStatus",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_HiringRequestCandidateStatus_CandidateDetails_CandidateId",
                table: "HiringRequestCandidateStatus",
                column: "CandidateId",
                principalTable: "CandidateDetails",
                principalColumn: "CandidateId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HiringRequestCandidateStatus_EmployeeDetail_EmployeeID",
                table: "HiringRequestCandidateStatus",
                column: "EmployeeID",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

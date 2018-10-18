using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace DataAccess.Migrations
{
    public partial class VoucherDetailRelationsFCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.CreateTable(
			 name: "VoucherDetail",
			 columns: table => new
			 {
				 CreatedDate = table.Column<DateTime>(nullable: true),
				 ModifiedDate = table.Column<DateTime>(nullable: true),
				 CreatedById = table.Column<string>(nullable: true),
				 ModifiedById = table.Column<string>(nullable: true),
				 IsDeleted = table.Column<bool>(nullable: true),
				 VoucherNo = table.Column<long>(type: "serial", nullable: false)
					 .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
				 CurrencyId = table.Column<int>(nullable: true),
				 VoucherDate = table.Column<DateTime>(nullable: true),
				 ChequeNo = table.Column<string>(maxLength: 10, nullable: true),
				 ReferenceNo = table.Column<string>(maxLength: 20, nullable: true),
				 Description = table.Column<string>(nullable: true),
				 JournalCode = table.Column<int>(nullable: true),
				 VoucherTypeId = table.Column<int>(nullable: true),
				 OfficeId = table.Column<int>(nullable: true),
				 ProjectId = table.Column<long>(nullable: true),
				 BudgetLineId = table.Column<long>(nullable: true),
				 FinancialYearId = table.Column<int>(nullable: true),
				 CurrencyCode = table.Column<string>(nullable: true),
				 VoucherType = table.Column<string>(nullable: true),
				 VoucherMode = table.Column<string>(nullable: true),
				 OfficeCode = table.Column<string>(nullable: true),
				 ChartAccountDetailAccountCode = table.Column<int>(nullable: true)
			 },
			 constraints: table =>
			 {
				 table.PrimaryKey("PK_VoucherDetail", x => x.VoucherNo);
				 table.ForeignKey(
					 name: "FK_VoucherDetail_ProjectBudgetLine_BudgetLineId",
					 column: x => x.BudgetLineId,
					 principalTable: "ProjectBudgetLine",
					 principalColumn: "BudgetLineId",
					 onDelete: ReferentialAction.Restrict);
				 table.ForeignKey(
					 name: "FK_VoucherDetail_ChartAccountDetail_ChartAccountDetailAccountCode",
					 column: x => x.ChartAccountDetailAccountCode,
					 principalTable: "ChartAccountDetail",
					 principalColumn: "AccountCode",
					 onDelete: ReferentialAction.Restrict);
				 table.ForeignKey(
					 name: "FK_VoucherDetail_AspNetUsers_CreatedById",
					 column: x => x.CreatedById,
					 principalTable: "AspNetUsers",
					 principalColumn: "Id",
					 onDelete: ReferentialAction.Restrict);
				 table.ForeignKey(
					 name: "FK_VoucherDetail_CurrencyDetails_CurrencyId",
					 column: x => x.CurrencyId,
					 principalTable: "CurrencyDetails",
					 principalColumn: "CurrencyId",
					 onDelete: ReferentialAction.Restrict);
				 table.ForeignKey(
					 name: "FK_VoucherDetail_FinancialYearDetail_FinancialYearId",
					 column: x => x.FinancialYearId,
					 principalTable: "FinancialYearDetail",
					 principalColumn: "FinancialYearId",
					 onDelete: ReferentialAction.Restrict);
				 table.ForeignKey(
					 name: "FK_VoucherDetail_JournalDetail_JournalCode",
					 column: x => x.JournalCode,
					 principalTable: "JournalDetail",
					 principalColumn: "JournalCode",
					 onDelete: ReferentialAction.Restrict);
				 table.ForeignKey(
					 name: "FK_VoucherDetail_AspNetUsers_ModifiedById",
					 column: x => x.ModifiedById,
					 principalTable: "AspNetUsers",
					 principalColumn: "Id",
					 onDelete: ReferentialAction.Restrict);
				 table.ForeignKey(
					 name: "FK_VoucherDetail_OfficeDetail_OfficeId",
					 column: x => x.OfficeId,
					 principalTable: "OfficeDetail",
					 principalColumn: "OfficeId",
					 onDelete: ReferentialAction.Restrict);
				 table.ForeignKey(
					 name: "FK_VoucherDetail_ProjectDetails_ProjectId",
					 column: x => x.ProjectId,
					 principalTable: "ProjectDetails",
					 principalColumn: "ProjectId",
					 onDelete: ReferentialAction.Restrict);
				 table.ForeignKey(
					 name: "FK_VoucherDetail_VoucherType_VoucherTypeId",
					 column: x => x.VoucherTypeId,
					 principalTable: "VoucherType",
					 principalColumn: "VoucherTypeId",
					 onDelete: ReferentialAction.Restrict);
			 });
		}

		protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.DropTable(
				name: "VoucherDetail");
		}
    }
}

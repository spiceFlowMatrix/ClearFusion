using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class financialProjectDetailAndRemoveDonorCriteriaColoum11122018 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DonorFinancingHistory",
                table: "DonorCriteriaDetail");

            migrationBuilder.DropColumn(
                name: "PoliticalStanding",
                table: "DonorCriteriaDetail");

            migrationBuilder.DropColumn(
                name: "ReligiousStanding",
                table: "DonorCriteriaDetail");

            migrationBuilder.AlterColumn<string>(
                name: "OtherActivity",
                table: "PurposeofInitiativeCriteria",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Product",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Service",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OtherDeliverableType",
                table: "DonorCriteriaDetail",
                nullable: true,
                oldClrType: typeof(bool),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "FinancialProjectDetail",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    FinancialProjectDetailId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ProjectId = table.Column<long>(nullable: false),
                    ProjectSelectionId = table.Column<int>(nullable: true),
                    ProjectName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialProjectDetail", x => x.FinancialProjectDetailId);
                    table.ForeignKey(
                        name: "FK_FinancialProjectDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinancialProjectDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinancialProjectDetail_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RiskCriteriaDetail_ProjectId",
                table: "RiskCriteriaDetail",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_PurposeofInitiativeCriteria_ProjectId",
                table: "PurposeofInitiativeCriteria",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_PriorityCriteriaDetail_ProjectId",
                table: "PriorityCriteriaDetail",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialCriteriaDetail_ProjectId",
                table: "FinancialCriteriaDetail",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_FeasibilityCriteriaDetail_ProjectId",
                table: "FeasibilityCriteriaDetail",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_EligibilityCriteriaDetail_ProjectId",
                table: "EligibilityCriteriaDetail",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_DonorCriteriaDetail_ProjectId",
                table: "DonorCriteriaDetail",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialProjectDetail_CreatedById",
                table: "FinancialProjectDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialProjectDetail_ModifiedById",
                table: "FinancialProjectDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialProjectDetail_ProjectId",
                table: "FinancialProjectDetail",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_DonorCriteriaDetail_ProjectDetail_ProjectId",
                table: "DonorCriteriaDetail",
                column: "ProjectId",
                principalTable: "ProjectDetail",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EligibilityCriteriaDetail_ProjectDetail_ProjectId",
                table: "EligibilityCriteriaDetail",
                column: "ProjectId",
                principalTable: "ProjectDetail",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FeasibilityCriteriaDetail_ProjectDetail_ProjectId",
                table: "FeasibilityCriteriaDetail",
                column: "ProjectId",
                principalTable: "ProjectDetail",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialCriteriaDetail_ProjectDetail_ProjectId",
                table: "FinancialCriteriaDetail",
                column: "ProjectId",
                principalTable: "ProjectDetail",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PriorityCriteriaDetail_ProjectDetail_ProjectId",
                table: "PriorityCriteriaDetail",
                column: "ProjectId",
                principalTable: "ProjectDetail",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurposeofInitiativeCriteria_ProjectDetail_ProjectId",
                table: "PurposeofInitiativeCriteria",
                column: "ProjectId",
                principalTable: "ProjectDetail",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RiskCriteriaDetail_ProjectDetail_ProjectId",
                table: "RiskCriteriaDetail",
                column: "ProjectId",
                principalTable: "ProjectDetail",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DonorCriteriaDetail_ProjectDetail_ProjectId",
                table: "DonorCriteriaDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_EligibilityCriteriaDetail_ProjectDetail_ProjectId",
                table: "EligibilityCriteriaDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_FeasibilityCriteriaDetail_ProjectDetail_ProjectId",
                table: "FeasibilityCriteriaDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_FinancialCriteriaDetail_ProjectDetail_ProjectId",
                table: "FinancialCriteriaDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_PriorityCriteriaDetail_ProjectDetail_ProjectId",
                table: "PriorityCriteriaDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_PurposeofInitiativeCriteria_ProjectDetail_ProjectId",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropForeignKey(
                name: "FK_RiskCriteriaDetail_ProjectDetail_ProjectId",
                table: "RiskCriteriaDetail");

            migrationBuilder.DropTable(
                name: "FinancialProjectDetail");

            migrationBuilder.DropIndex(
                name: "IX_RiskCriteriaDetail_ProjectId",
                table: "RiskCriteriaDetail");

            migrationBuilder.DropIndex(
                name: "IX_PurposeofInitiativeCriteria_ProjectId",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropIndex(
                name: "IX_PriorityCriteriaDetail_ProjectId",
                table: "PriorityCriteriaDetail");

            migrationBuilder.DropIndex(
                name: "IX_FinancialCriteriaDetail_ProjectId",
                table: "FinancialCriteriaDetail");

            migrationBuilder.DropIndex(
                name: "IX_FeasibilityCriteriaDetail_ProjectId",
                table: "FeasibilityCriteriaDetail");

            migrationBuilder.DropIndex(
                name: "IX_EligibilityCriteriaDetail_ProjectId",
                table: "EligibilityCriteriaDetail");

            migrationBuilder.DropIndex(
                name: "IX_DonorCriteriaDetail_ProjectId",
                table: "DonorCriteriaDetail");

            migrationBuilder.DropColumn(
                name: "Product",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "Service",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.AlterColumn<bool>(
                name: "OtherActivity",
                table: "PurposeofInitiativeCriteria",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "OtherDeliverableType",
                table: "DonorCriteriaDetail",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DonorFinancingHistory",
                table: "DonorCriteriaDetail",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PoliticalStanding",
                table: "DonorCriteriaDetail",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ReligiousStanding",
                table: "DonorCriteriaDetail",
                nullable: true);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class AddedTblStatementSubmission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DesignationId",
                table: "TechnicalQuestion",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ComparativeStatementSubmission",
                columns: table => new
                {
                    SubmissionId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    SupplierIds = table.Column<long[]>(nullable: true),
                    LogisticRequestsId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComparativeStatementSubmission", x => x.SubmissionId);
                    table.ForeignKey(
                        name: "FK_ComparativeStatementSubmission_ProjectLogisticRequests_Logi~",
                        column: x => x.LogisticRequestsId,
                        principalTable: "ProjectLogisticRequests",
                        principalColumn: "LogisticRequestsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalQuestion_DesignationId",
                table: "TechnicalQuestion",
                column: "DesignationId");

            migrationBuilder.CreateIndex(
                name: "IX_ComparativeStatementSubmission_LogisticRequestsId",
                table: "ComparativeStatementSubmission",
                column: "LogisticRequestsId");

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicalQuestion_DesignationDetail_DesignationId",
                table: "TechnicalQuestion",
                column: "DesignationId",
                principalTable: "DesignationDetail",
                principalColumn: "DesignationId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TechnicalQuestion_DesignationDetail_DesignationId",
                table: "TechnicalQuestion");

            migrationBuilder.DropTable(
                name: "ComparativeStatementSubmission");

            migrationBuilder.DropIndex(
                name: "IX_TechnicalQuestion_DesignationId",
                table: "TechnicalQuestion");

            migrationBuilder.DropColumn(
                name: "DesignationId",
                table: "TechnicalQuestion");
        }
    }
}

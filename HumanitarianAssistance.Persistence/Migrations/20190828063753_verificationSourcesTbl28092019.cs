using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class verificationSourcesTbl28092019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "ProjectIndicators",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "QuestionType",
                table: "ProjectIndicatorQuestions",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "VerificationSources",
                columns: table => new
                {
                    VerificationSourceId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    VerificationSourceName = table.Column<string>(nullable: true),
                    IndicatorQuestionId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VerificationSources", x => x.VerificationSourceId);
                    table.ForeignKey(
                        name: "FK_VerificationSources_ProjectIndicatorQuestions_IndicatorQues~",
                        column: x => x.IndicatorQuestionId,
                        principalTable: "ProjectIndicatorQuestions",
                        principalColumn: "IndicatorQuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VerificationSources_IndicatorQuestionId",
                table: "VerificationSources",
                column: "IndicatorQuestionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VerificationSources");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "ProjectIndicators");

            migrationBuilder.DropColumn(
                name: "QuestionType",
                table: "ProjectIndicatorQuestions");
        }
    }
}

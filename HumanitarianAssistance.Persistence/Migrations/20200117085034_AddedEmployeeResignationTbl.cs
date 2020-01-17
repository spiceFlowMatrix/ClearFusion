using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class AddedEmployeeResignationTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsResigned",
                table: "EmployeeDetail",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ResignationStatus",
                table: "EmployeeDetail",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EmployeeResignationDetail",
                columns: table => new
                {
                    EmployeeResignationId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ResignDate = table.Column<DateTime>(nullable: false),
                    IsIssueUnresolved = table.Column<bool>(nullable: false),
                    CommentsIssues = table.Column<string>(nullable: true),
                    EmployeeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeResignationDetail", x => x.EmployeeResignationId);
                    table.ForeignKey(
                        name: "FK_EmployeeResignationDetail_EmployeeDetail_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeResignationQuestionDetail",
                columns: table => new
                {
                    ResignationQuestionId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    QuestionId = table.Column<int>(nullable: false),
                    Answer = table.Column<int>(nullable: false),
                    ResignationId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeResignationQuestionDetail", x => x.ResignationQuestionId);
                    table.ForeignKey(
                        name: "FK_EmployeeResignationQuestionDetail_ExitInterviewQuestionsMas~",
                        column: x => x.QuestionId,
                        principalTable: "ExitInterviewQuestionsMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeResignationQuestionDetail_EmployeeResignationDetail~",
                        column: x => x.ResignationId,
                        principalTable: "EmployeeResignationDetail",
                        principalColumn: "EmployeeResignationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeResignationDetail_EmployeeID",
                table: "EmployeeResignationDetail",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeResignationQuestionDetail_QuestionId",
                table: "EmployeeResignationQuestionDetail",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeResignationQuestionDetail_ResignationId",
                table: "EmployeeResignationQuestionDetail",
                column: "ResignationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeResignationQuestionDetail");

            migrationBuilder.DropTable(
                name: "EmployeeResignationDetail");

            migrationBuilder.DropColumn(
                name: "IsResigned",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "ResignationStatus",
                table: "EmployeeDetail");
        }
    }
}

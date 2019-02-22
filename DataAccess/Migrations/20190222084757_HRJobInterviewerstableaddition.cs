using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class HRJobInterviewerstableaddition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HRJobInterviewers",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    HRJobInterviewerId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    EmployeeId = table.Column<int>(nullable: false),
                    InterviewDetailsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HRJobInterviewers", x => x.HRJobInterviewerId);
                    table.ForeignKey(
                        name: "FK_HRJobInterviewers_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HRJobInterviewers_EmployeeDetail_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HRJobInterviewers_InterviewDetails_InterviewDetailsId",
                        column: x => x.InterviewDetailsId,
                        principalTable: "InterviewDetails",
                        principalColumn: "InterviewDetailsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HRJobInterviewers_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HRJobInterviewers_CreatedById",
                table: "HRJobInterviewers",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_HRJobInterviewers_EmployeeId",
                table: "HRJobInterviewers",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_HRJobInterviewers_InterviewDetailsId",
                table: "HRJobInterviewers",
                column: "InterviewDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_HRJobInterviewers_ModifiedById",
                table: "HRJobInterviewers",
                column: "ModifiedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HRJobInterviewers");
        }
    }
}

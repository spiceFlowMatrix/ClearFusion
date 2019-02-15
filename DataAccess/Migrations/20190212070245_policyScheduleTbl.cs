using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class policyScheduleTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PolicySchedules",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    PolicyScheduleId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    PolicyId = table.Column<long>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Frequency = table.Column<int>(nullable: true),
                    ByMonth = table.Column<int>(nullable: true),
                    ByWeek = table.Column<int>(nullable: true),
                    ByDay = table.Column<int>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    StartTime = table.Column<TimeSpan>(nullable: false),
                    EndTime = table.Column<TimeSpan>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    isActive = table.Column<bool>(nullable: false),
                    isDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicySchedules", x => x.PolicyScheduleId);
                    table.ForeignKey(
                        name: "FK_PolicySchedules_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PolicySchedules_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PolicySchedules_PolicyDetails_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "PolicyDetails",
                        principalColumn: "PolicyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PolicySchedules_CreatedById",
                table: "PolicySchedules",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PolicySchedules_ModifiedById",
                table: "PolicySchedules",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PolicySchedules_PolicyId",
                table: "PolicySchedules",
                column: "PolicyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PolicySchedules");
        }
    }
}

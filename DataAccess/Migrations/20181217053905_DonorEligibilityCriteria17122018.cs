using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class DonorEligibilityCriteria17122018 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DonorEligibilityCriteria",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    DonorEligibilityDetailId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true),
                    ProjectId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonorEligibilityCriteria", x => x.DonorEligibilityDetailId);
                    table.ForeignKey(
                        name: "FK_DonorEligibilityCriteria_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DonorEligibilityCriteria_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DonorEligibilityCriteria_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DonorEligibilityCriteria_CreatedById",
                table: "DonorEligibilityCriteria",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DonorEligibilityCriteria_ModifiedById",
                table: "DonorEligibilityCriteria",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_DonorEligibilityCriteria_ProjectId",
                table: "DonorEligibilityCriteria",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DonorEligibilityCriteria");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class hiringReuestCandidate17062019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HiringRequestCandidates",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    CandidateId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    HiringRequestId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HiringRequestCandidates", x => x.CandidateId);
                    table.ForeignKey(
                        name: "FK_HiringRequestCandidates_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HiringRequestCandidates_ProjectHiringRequestDetail_HiringRequestId",
                        column: x => x.HiringRequestId,
                        principalTable: "ProjectHiringRequestDetail",
                        principalColumn: "HiringRequestId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HiringRequestCandidates_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HiringRequestCandidates_CreatedById",
                table: "HiringRequestCandidates",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_HiringRequestCandidates_HiringRequestId",
                table: "HiringRequestCandidates",
                column: "HiringRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_HiringRequestCandidates_ModifiedById",
                table: "HiringRequestCandidates",
                column: "ModifiedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HiringRequestCandidates");
        }
    }
}

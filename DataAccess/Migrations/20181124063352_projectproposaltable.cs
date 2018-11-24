using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class projectproposaltable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectProposalDetail",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ProjectProposaldetailId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FolderName = table.Column<string>(nullable: true),
                    FolderId = table.Column<string>(nullable: true),
                    ProposalFileName = table.Column<string>(nullable: true),
                    ProjectId = table.Column<long>(nullable: false),
                    ProposalFileId = table.Column<string>(nullable: true),
                    EDIFileName = table.Column<string>(nullable: true),
                    EdiFileId = table.Column<string>(nullable: true),
                    BudgetFileName = table.Column<string>(nullable: true),
                    BudgetFileId = table.Column<string>(nullable: true),
                    ConceptFileName = table.Column<string>(nullable: true),
                    ConceptFileId = table.Column<string>(nullable: true),
                    PresentationFileName = table.Column<string>(nullable: true),
                    PresentationFileId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectProposalDetail", x => x.ProjectProposaldetailId);
                    table.ForeignKey(
                        name: "FK_ProjectProposalDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectProposalDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectProposalDetail_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProposalDetail_CreatedById",
                table: "ProjectProposalDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProposalDetail_ModifiedById",
                table: "ProjectProposalDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProposalDetail_ProjectId",
                table: "ProjectProposalDetail",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectProposalDetail");
        }
    }
}

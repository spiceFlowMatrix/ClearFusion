using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class projectProposalDocumentsTbl21082019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectProposalDocument",
                columns: table => new
                {
                    ProjectProposalDocumnetId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ProposalDocumentName = table.Column<string>(nullable: true),
                    ProjectId = table.Column<long>(nullable: false),
                    ProposalWebLink = table.Column<string>(nullable: true),
                    ProposalExtType = table.Column<string>(nullable: true),
                    ProposalDocumentTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectProposalDocument", x => x.ProjectProposalDocumnetId);
                    table.ForeignKey(
                        name: "FK_ProjectProposalDocument_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectProposalDocument_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectProposalDocument_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProposalDocument_CreatedById",
                table: "ProjectProposalDocument",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProposalDocument_ModifiedById",
                table: "ProjectProposalDocument",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProposalDocument_ProjectId",
                table: "ProjectProposalDocument",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectProposalDocument");
        }
    }
}

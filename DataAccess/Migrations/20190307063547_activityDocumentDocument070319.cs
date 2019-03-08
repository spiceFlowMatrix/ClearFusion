using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class activityDocumentDocument070319 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivityDocumentsDetail",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ActtivityDocumentId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ActivityDocumentsFilePath = table.Column<string>(nullable: true),
                    StatusId = table.Column<int>(nullable: true),
                    ActivityId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityDocumentsDetail", x => x.ActtivityDocumentId);
                    table.ForeignKey(
                        name: "FK_ActivityDocumentsDetail_ProjectActivityDetail_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "ProjectActivityDetail",
                        principalColumn: "ActivityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivityDocumentsDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActivityDocumentsDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActivityDocumentsDetail_ActivityStatusDetail_StatusId",
                        column: x => x.StatusId,
                        principalTable: "ActivityStatusDetail",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityDocumentsDetail_ActivityId",
                table: "ActivityDocumentsDetail",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityDocumentsDetail_CreatedById",
                table: "ActivityDocumentsDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityDocumentsDetail_ModifiedById",
                table: "ActivityDocumentsDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityDocumentsDetail_StatusId",
                table: "ActivityDocumentsDetail",
                column: "StatusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityDocumentsDetail");
        }
    }
}

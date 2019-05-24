using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class ProjectOpportunityControlTblAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectOpportunityControl",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ProjectId = table.Column<long>(nullable: false),
                    UserID = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectOpportunityControl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectOpportunityControl_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectOpportunityControl_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectOpportunityControl_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectOpportunityControl_UserDetails_UserID",
                        column: x => x.UserID,
                        principalTable: "UserDetails",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOpportunityControl_CreatedById",
                table: "ProjectOpportunityControl",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOpportunityControl_ModifiedById",
                table: "ProjectOpportunityControl",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOpportunityControl_ProjectId",
                table: "ProjectOpportunityControl",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOpportunityControl_UserID",
                table: "ProjectOpportunityControl",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectOpportunityControl");
        }
    }
}

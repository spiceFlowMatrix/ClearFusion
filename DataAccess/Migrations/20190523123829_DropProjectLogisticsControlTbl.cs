using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class DropProjectLogisticsControlTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectLogisticsControl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectLogisticsControl",
                columns: table => new
                {
                    Id = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    ProjectId = table.Column<long>(nullable: false),
                    RoleId = table.Column<int>(nullable: false),
                    UserID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectLogisticsControl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectLogisticsControl_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectLogisticsControl_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectLogisticsControl_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectLogisticsControl_UserDetails_UserID",
                        column: x => x.UserID,
                        principalTable: "UserDetails",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectLogisticsControl_CreatedById",
                table: "ProjectLogisticsControl",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectLogisticsControl_ModifiedById",
                table: "ProjectLogisticsControl",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectLogisticsControl_ProjectId",
                table: "ProjectLogisticsControl",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectLogisticsControl_UserID",
                table: "ProjectLogisticsControl",
                column: "UserID");
        }
    }
}

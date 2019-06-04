using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class peopleControlTblsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectActivitiesControl",
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
                    table.PrimaryKey("PK_ProjectActivitiesControl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectActivitiesControl_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectActivitiesControl_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectActivitiesControl_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectActivitiesControl_UserDetails_UserID",
                        column: x => x.UserID,
                        principalTable: "UserDetails",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectHiringControl",
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
                    table.PrimaryKey("PK_ProjectHiringControl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectHiringControl_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectHiringControl_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectHiringControl_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectHiringControl_UserDetails_UserID",
                        column: x => x.UserID,
                        principalTable: "UserDetails",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivitiesControl_CreatedById",
                table: "ProjectActivitiesControl",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivitiesControl_ModifiedById",
                table: "ProjectActivitiesControl",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivitiesControl_ProjectId",
                table: "ProjectActivitiesControl",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivitiesControl_UserID",
                table: "ProjectActivitiesControl",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectHiringControl_CreatedById",
                table: "ProjectHiringControl",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectHiringControl_ModifiedById",
                table: "ProjectHiringControl",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectHiringControl_ProjectId",
                table: "ProjectHiringControl",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectHiringControl_UserID",
                table: "ProjectHiringControl",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectActivitiesControl");

            migrationBuilder.DropTable(
                name: "ProjectHiringControl");
        }
    }
}

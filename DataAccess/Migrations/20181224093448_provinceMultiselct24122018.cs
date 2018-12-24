using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class provinceMultiselct24122018 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DistrictMultiSelect",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    DistrictMultiSelectId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ProjectId = table.Column<long>(nullable: false),
                    DistrictID = table.Column<long>(nullable: false),
                    DistrictSelectionId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistrictMultiSelect", x => x.DistrictMultiSelectId);
                    table.ForeignKey(
                        name: "FK_DistrictMultiSelect_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DistrictMultiSelect_DistrictDetail_DistrictID",
                        column: x => x.DistrictID,
                        principalTable: "DistrictDetail",
                        principalColumn: "DistrictID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DistrictMultiSelect_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DistrictMultiSelect_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProvinceMultiSelect",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ProvinceMultiSelectId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ProjectId = table.Column<long>(nullable: false),
                    ProvinceId = table.Column<int>(nullable: false),
                    ProvinceSelectionId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProvinceMultiSelect", x => x.ProvinceMultiSelectId);
                    table.ForeignKey(
                        name: "FK_ProvinceMultiSelect_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProvinceMultiSelect_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProvinceMultiSelect_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProvinceMultiSelect_ProvinceDetails_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "ProvinceDetails",
                        principalColumn: "ProvinceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SecurityConsiderationMultiSelect",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    SecurityConsiderationMultiSelectId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ProjectId = table.Column<long>(nullable: false),
                    SecurityConsiderationId = table.Column<long>(nullable: false),
                    SecurityConsiderationSelectedId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityConsiderationMultiSelect", x => x.SecurityConsiderationMultiSelectId);
                    table.ForeignKey(
                        name: "FK_SecurityConsiderationMultiSelect_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SecurityConsiderationMultiSelect_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SecurityConsiderationMultiSelect_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SecurityConsiderationMultiSelect_SecurityConsiderationDetail_SecurityConsiderationId",
                        column: x => x.SecurityConsiderationId,
                        principalTable: "SecurityConsiderationDetail",
                        principalColumn: "SecurityConsiderationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DistrictMultiSelect_CreatedById",
                table: "DistrictMultiSelect",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DistrictMultiSelect_DistrictID",
                table: "DistrictMultiSelect",
                column: "DistrictID");

            migrationBuilder.CreateIndex(
                name: "IX_DistrictMultiSelect_ModifiedById",
                table: "DistrictMultiSelect",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_DistrictMultiSelect_ProjectId",
                table: "DistrictMultiSelect",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProvinceMultiSelect_CreatedById",
                table: "ProvinceMultiSelect",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProvinceMultiSelect_ModifiedById",
                table: "ProvinceMultiSelect",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProvinceMultiSelect_ProjectId",
                table: "ProvinceMultiSelect",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProvinceMultiSelect_ProvinceId",
                table: "ProvinceMultiSelect",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityConsiderationMultiSelect_CreatedById",
                table: "SecurityConsiderationMultiSelect",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityConsiderationMultiSelect_ModifiedById",
                table: "SecurityConsiderationMultiSelect",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityConsiderationMultiSelect_ProjectId",
                table: "SecurityConsiderationMultiSelect",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityConsiderationMultiSelect_SecurityConsiderationId",
                table: "SecurityConsiderationMultiSelect",
                column: "SecurityConsiderationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DistrictMultiSelect");

            migrationBuilder.DropTable(
                name: "ProvinceMultiSelect");

            migrationBuilder.DropTable(
                name: "SecurityConsiderationMultiSelect");
        }
    }
}

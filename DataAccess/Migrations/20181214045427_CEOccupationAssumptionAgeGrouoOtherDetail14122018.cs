using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class CEOccupationAssumptionAgeGrouoOtherDetail14122018 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CEAgeGroupDetail",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    AgeGroupOtherDetailId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true),
                    ProjectId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CEAgeGroupDetail", x => x.AgeGroupOtherDetailId);
                    table.ForeignKey(
                        name: "FK_CEAgeGroupDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CEAgeGroupDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CEAgeGroupDetail_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CEAssumptionDetail",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    AssumptionDetailId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true),
                    ProjectId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CEAssumptionDetail", x => x.AssumptionDetailId);
                    table.ForeignKey(
                        name: "FK_CEAssumptionDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CEAssumptionDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CEAssumptionDetail_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CEFeasibilityExpertOtherDetail",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ExpertOtherDetailId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true),
                    ProjectId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CEFeasibilityExpertOtherDetail", x => x.ExpertOtherDetailId);
                    table.ForeignKey(
                        name: "FK_CEFeasibilityExpertOtherDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CEFeasibilityExpertOtherDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CEFeasibilityExpertOtherDetail_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CEOccupationDetail",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    OccupationOtherDetailId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true),
                    ProjectId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CEOccupationDetail", x => x.OccupationOtherDetailId);
                    table.ForeignKey(
                        name: "FK_CEOccupationDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CEOccupationDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CEOccupationDetail_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CEAgeGroupDetail_CreatedById",
                table: "CEAgeGroupDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_CEAgeGroupDetail_ModifiedById",
                table: "CEAgeGroupDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_CEAgeGroupDetail_ProjectId",
                table: "CEAgeGroupDetail",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_CEAssumptionDetail_CreatedById",
                table: "CEAssumptionDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_CEAssumptionDetail_ModifiedById",
                table: "CEAssumptionDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_CEAssumptionDetail_ProjectId",
                table: "CEAssumptionDetail",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_CEFeasibilityExpertOtherDetail_CreatedById",
                table: "CEFeasibilityExpertOtherDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_CEFeasibilityExpertOtherDetail_ModifiedById",
                table: "CEFeasibilityExpertOtherDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_CEFeasibilityExpertOtherDetail_ProjectId",
                table: "CEFeasibilityExpertOtherDetail",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_CEOccupationDetail_CreatedById",
                table: "CEOccupationDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_CEOccupationDetail_ModifiedById",
                table: "CEOccupationDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_CEOccupationDetail_ProjectId",
                table: "CEOccupationDetail",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CEAgeGroupDetail");

            migrationBuilder.DropTable(
                name: "CEAssumptionDetail");

            migrationBuilder.DropTable(
                name: "CEFeasibilityExpertOtherDetail");

            migrationBuilder.DropTable(
                name: "CEOccupationDetail");
        }
    }
}

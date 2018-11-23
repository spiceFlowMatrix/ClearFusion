using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddProjectotherDetailTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectOtherDetail",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ProjectOtherDetailId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ProjectId = table.Column<long>(nullable: false),
                    opportunityNo = table.Column<string>(nullable: true),
                    opportunity = table.Column<string>(nullable: true),
                    opportunitydescription = table.Column<string>(nullable: true),
                    ProvinceId = table.Column<string>(nullable: true),
                    DistrictID = table.Column<int>(nullable: true),
                    OfficeId = table.Column<int>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: true),
                    budget = table.Column<string>(nullable: true),
                    beneficiaryMale = table.Column<string>(nullable: true),
                    beneficiaryFemale = table.Column<string>(nullable: true),
                    projectGoal = table.Column<string>(nullable: true),
                    projectObjective = table.Column<string>(nullable: true),
                    mainActivities = table.Column<string>(nullable: true),
                    DonorId = table.Column<long>(nullable: true),
                    SubmissionDate = table.Column<DateTime>(nullable: true),
                    REOIReceiveDate = table.Column<DateTime>(nullable: true),
                    StrengthConsiderationId = table.Column<long>(nullable: true),
                    GenderConsiderationId = table.Column<long>(nullable: true),
                    GenderRemarks = table.Column<string>(nullable: true),
                    SecurityId = table.Column<long>(nullable: true),
                    SecurityConsiderationId = table.Column<long>(nullable: true),
                    SecurityRemarks = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectOtherDetail", x => x.ProjectOtherDetailId);
                    table.ForeignKey(
                        name: "FK_ProjectOtherDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectOtherDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectOtherDetail_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOtherDetail_CreatedById",
                table: "ProjectOtherDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOtherDetail_ModifiedById",
                table: "ProjectOtherDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOtherDetail_ProjectId",
                table: "ProjectOtherDetail",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectOtherDetail");
        }
    }
}

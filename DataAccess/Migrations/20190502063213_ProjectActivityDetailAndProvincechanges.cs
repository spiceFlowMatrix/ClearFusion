using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class ProjectActivityDetailAndProvincechanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectActivityDetail_OfficeDetail_OfficeId",
                table: "ProjectActivityDetail");

            migrationBuilder.DropIndex(
                name: "IX_ProjectActivityDetail_OfficeId",
                table: "ProjectActivityDetail");

            migrationBuilder.DropColumn(
                name: "ActualEndDate",
                table: "ProjectActivityDetail");

            migrationBuilder.DropColumn(
                name: "ActualStartDate",
                table: "ProjectActivityDetail");

            migrationBuilder.DropColumn(
                name: "Comments",
                table: "ProjectActivityDetail");

            migrationBuilder.DropColumn(
                name: "ExtensionEndDate",
                table: "ProjectActivityDetail");

            migrationBuilder.DropColumn(
                name: "ExtensionStartDate",
                table: "ProjectActivityDetail");

            migrationBuilder.DropColumn(
                name: "ImplementationChalanges",
                table: "ProjectActivityDetail");

            migrationBuilder.DropColumn(
                name: "ImplementationMethod",
                table: "ProjectActivityDetail");

            migrationBuilder.DropColumn(
                name: "ImplementationProgress",
                table: "ProjectActivityDetail");

            migrationBuilder.DropColumn(
                name: "ImplementationStatus",
                table: "ProjectActivityDetail");

            migrationBuilder.DropColumn(
                name: "MonitoringChallenges",
                table: "ProjectActivityDetail");

            migrationBuilder.DropColumn(
                name: "MonitoringFrequency",
                table: "ProjectActivityDetail");

            migrationBuilder.DropColumn(
                name: "MonitoringProgress",
                table: "ProjectActivityDetail");

            migrationBuilder.DropColumn(
                name: "MonitoringScore",
                table: "ProjectActivityDetail");

            migrationBuilder.DropColumn(
                name: "MonitoringStatus",
                table: "ProjectActivityDetail");

            migrationBuilder.DropColumn(
                name: "OfficeId",
                table: "ProjectActivityDetail");

            migrationBuilder.DropColumn(
                name: "OvercomingChallanges",
                table: "ProjectActivityDetail");

            migrationBuilder.DropColumn(
                name: "Recommendation",
                table: "ProjectActivityDetail");

            migrationBuilder.DropColumn(
                name: "Strengths",
                table: "ProjectActivityDetail");

            migrationBuilder.DropColumn(
                name: "VerificationSource",
                table: "ProjectActivityDetail");

            migrationBuilder.DropColumn(
                name: "Weeknesses",
                table: "ProjectActivityDetail");

            migrationBuilder.CreateTable(
                name: "ProjectActivityProvinceDetail",
                columns: table => new
                {
                    ActivityProvinceId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ActivityId = table.Column<long>(nullable: true),
                    ProvinceId = table.Column<int>(nullable: true),
                    DistrictID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectActivityProvinceDetail", x => x.ActivityProvinceId);
                    table.ForeignKey(
                        name: "FK_ProjectActivityProvinceDetail_ProjectActivityDetail_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "ProjectActivityDetail",
                        principalColumn: "ActivityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectActivityProvinceDetail_DistrictDetail_DistrictID",
                        column: x => x.DistrictID,
                        principalTable: "DistrictDetail",
                        principalColumn: "DistrictID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectActivityProvinceDetail_ProvinceDetails_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "ProvinceDetails",
                        principalColumn: "ProvinceId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivityProvinceDetail_ActivityId",
                table: "ProjectActivityProvinceDetail",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivityProvinceDetail_DistrictID",
                table: "ProjectActivityProvinceDetail",
                column: "DistrictID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivityProvinceDetail_ProvinceId",
                table: "ProjectActivityProvinceDetail",
                column: "ProvinceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectActivityProvinceDetail");

            migrationBuilder.AddColumn<DateTime>(
                name: "ActualEndDate",
                table: "ProjectActivityDetail",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ActualStartDate",
                table: "ProjectActivityDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "ProjectActivityDetail",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExtensionEndDate",
                table: "ProjectActivityDetail",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExtensionStartDate",
                table: "ProjectActivityDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImplementationChalanges",
                table: "ProjectActivityDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImplementationMethod",
                table: "ProjectActivityDetail",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "ImplementationProgress",
                table: "ProjectActivityDetail",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ImplementationStatus",
                table: "ProjectActivityDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MonitoringChallenges",
                table: "ProjectActivityDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MonitoringFrequency",
                table: "ProjectActivityDetail",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "MonitoringProgress",
                table: "ProjectActivityDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MonitoringScore",
                table: "ProjectActivityDetail",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "MonitoringStatus",
                table: "ProjectActivityDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OfficeId",
                table: "ProjectActivityDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OvercomingChallanges",
                table: "ProjectActivityDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Recommendation",
                table: "ProjectActivityDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Strengths",
                table: "ProjectActivityDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerificationSource",
                table: "ProjectActivityDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Weeknesses",
                table: "ProjectActivityDetail",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivityDetail_OfficeId",
                table: "ProjectActivityDetail",
                column: "OfficeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectActivityDetail_OfficeDetail_OfficeId",
                table: "ProjectActivityDetail",
                column: "OfficeId",
                principalTable: "OfficeDetail",
                principalColumn: "OfficeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

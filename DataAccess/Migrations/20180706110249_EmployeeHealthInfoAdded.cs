using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class EmployeeHealthInfoAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeHealthDetail");

            migrationBuilder.CreateTable(
                name: "EmployeeHealthInfo",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    EmployeeHealthInfoId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    EmployeeId = table.Column<int>(nullable: false),
                    PhysicanName = table.Column<string>(nullable: true),
                    HospitalName = table.Column<string>(nullable: true),
                    HospitalAddress = table.Column<string>(nullable: true),
                    Height = table.Column<float>(nullable: true),
                    Weight = table.Column<float>(nullable: true),
                    BloodPressure = table.Column<float>(nullable: true),
                    VisualWithoutGlassesR = table.Column<float>(nullable: true),
                    VisualWithoutGlassesL = table.Column<float>(nullable: true),
                    VisualWithGlassesR = table.Column<float>(nullable: true),
                    VisualWithGlassesL = table.Column<float>(nullable: true),
                    HearingR = table.Column<float>(nullable: true),
                    HearingRType = table.Column<int>(nullable: true),
                    HearingL = table.Column<float>(nullable: true),
                    HearingLType = table.Column<int>(nullable: true),
                    HistoryOfPastIllness = table.Column<string>(nullable: true),
                    HealthPresentCondition = table.Column<string>(nullable: true),
                    ResultOfChestXRay = table.Column<string>(nullable: true),
                    BloodGroup = table.Column<int>(nullable: true),
                    Hbs = table.Column<int>(nullable: true),
                    Hcv = table.Column<int>(nullable: true),
                    OverallHealthCondition = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeHealthInfo", x => x.EmployeeHealthInfoId);
                    table.ForeignKey(
                        name: "FK_EmployeeHealthInfo_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeHealthInfo_EmployeeDetail_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeHealthInfo_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeHealthInfo_CreatedById",
                table: "EmployeeHealthInfo",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeHealthInfo_EmployeeId",
                table: "EmployeeHealthInfo",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeHealthInfo_ModifiedById",
                table: "EmployeeHealthInfo",
                column: "ModifiedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeHealthInfo");

            migrationBuilder.CreateTable(
                name: "EmployeeHealthDetail",
                columns: table => new
                {
                    HealthInfoId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AllergicSubstance = table.Column<bool>(nullable: false),
                    BloodGroup = table.Column<string>(maxLength: 20, nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    FamilyHistory = table.Column<bool>(nullable: false),
                    Insurance = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MeasureDiseases = table.Column<bool>(nullable: false),
                    MedicalHistory = table.Column<string>(nullable: true),
                    MedicalInsurance = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    SmokeAndDrink = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeHealthDetail", x => x.HealthInfoId);
                    table.ForeignKey(
                        name: "FK_EmployeeHealthDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeHealthDetail_EmployeeDetail_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeHealthDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeHealthDetail_CreatedById",
                table: "EmployeeHealthDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeHealthDetail_EmployeeId",
                table: "EmployeeHealthDetail",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeHealthDetail_ModifiedById",
                table: "EmployeeHealthDetail",
                column: "ModifiedById");
        }
    }
}

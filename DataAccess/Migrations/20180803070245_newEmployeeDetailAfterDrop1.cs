using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class newEmployeeDetailAfterDrop1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeDetail",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    EmployeeID = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    EmployeeCode = table.Column<string>(nullable: true),
                    RegCode = table.Column<string>(nullable: true),
                    EmployeeName = table.Column<string>(nullable: true),
                    IDCard = table.Column<string>(nullable: true),
                    FatherName = table.Column<string>(nullable: true),
                    PermanentAddress = table.Column<string>(nullable: true),
                    CurrentAddress = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    District = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Fax = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    ReferBy = table.Column<string>(nullable: true),
                    Passport = table.Column<string>(nullable: true),
                    Language = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    PlaceOfBirth = table.Column<string>(nullable: true),
                    PreviousWork = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    EmployeePhoto = table.Column<string>(nullable: true),
                    Sex = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Province = table.Column<string>(nullable: true),
                    Qualification = table.Column<string>(nullable: true),
                    Profession = table.Column<string>(nullable: true),
                    Grade = table.Column<string>(nullable: true),
                    Experience = table.Column<string>(nullable: true),
                    Nationality = table.Column<string>(nullable: true),
                    SpeakLanguageList = table.Column<string>(nullable: true),
                    CloseRelativeList = table.Column<string>(nullable: true),
                    RefereeList = table.Column<string>(nullable: true),
                    EducationList = table.Column<string>(nullable: true),
                    NationalEmploymentList = table.Column<string>(nullable: true),
                    InternationalEmploymentList = table.Column<string>(nullable: true),
                    OtherSkillList = table.Column<string>(nullable: true),
                    MaritalStatus = table.Column<string>(nullable: true),
                    NoOfChildren = table.Column<int>(nullable: true),
                    ProjectId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeDetail", x => x.EmployeeID);
                    table.ForeignKey(
                        name: "FK_EmployeeDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetail_CreatedById",
                table: "EmployeeDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetail_ModifiedById",
                table: "EmployeeDetail",
                column: "ModifiedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeDetail");
        }
    }
}

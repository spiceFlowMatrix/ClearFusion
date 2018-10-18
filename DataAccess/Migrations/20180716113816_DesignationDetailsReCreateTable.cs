using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class DesignationDetailsReCreateTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DesignationDetail",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    DesignationId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Designation = table.Column<string>(maxLength: 100, nullable: true),
                    DesignationDari = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignationDetail", x => x.DesignationId);
                    table.ForeignKey(
                        name: "FK_DesignationDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DesignationDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProfessionalDetail_DesignationId",
                table: "EmployeeProfessionalDetail",
                column: "DesignationId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeContract_Designation",
                table: "EmployeeContract",
                column: "Designation");

            migrationBuilder.CreateIndex(
                name: "IX_DesignationDetail_CreatedById",
                table: "DesignationDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DesignationDetail_ModifiedById",
                table: "DesignationDetail",
                column: "ModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeContract_DesignationDetail_Designation",
                table: "EmployeeContract",
                column: "Designation",
                principalTable: "DesignationDetail",
                principalColumn: "DesignationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProfessionalDetail_DesignationDetail_DesignationId",
                table: "EmployeeProfessionalDetail",
                column: "DesignationId",
                principalTable: "DesignationDetail",
                principalColumn: "DesignationId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeContract_DesignationDetail_Designation",
                table: "EmployeeContract");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProfessionalDetail_DesignationDetail_DesignationId",
                table: "EmployeeProfessionalDetail");

            migrationBuilder.DropTable(
                name: "DesignationDetail");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeProfessionalDetail_DesignationId",
                table: "EmployeeProfessionalDetail");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeContract_Designation",
                table: "EmployeeContract");
        }
    }
}

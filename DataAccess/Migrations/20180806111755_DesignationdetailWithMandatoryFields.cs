using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class DesignationdetailWithMandatoryFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_DesignationDetail_AspNetUsers_CreatedById",
            //    table: "DesignationDetail");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_DesignationDetail_AspNetUsers_ModifiedById",
            //    table: "DesignationDetail");

            //migrationBuilder.DropIndex(
            //    name: "IX_DesignationDetail_CreatedById",
            //    table: "DesignationDetail");

            //migrationBuilder.DropIndex(
            //    name: "IX_DesignationDetail_ModifiedById",
            //    table: "DesignationDetail");

            //migrationBuilder.DropColumn(
            //    name: "CreatedById",
            //    table: "DesignationDetail");

            //migrationBuilder.DropColumn(
            //    name: "CreatedDate",
            //    table: "DesignationDetail");

            //migrationBuilder.DropColumn(
            //    name: "IsDeleted",
            //    table: "DesignationDetail");

            //migrationBuilder.DropColumn(
            //    name: "ModifiedById",
            //    table: "DesignationDetail");

            //migrationBuilder.DropColumn(
            //    name: "ModifiedDate",
            //    table: "DesignationDetail");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetail_HigherQualificationId",
                table: "EmployeeDetail",
                column: "HigherQualificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeDetail_QualificationDetails_HigherQualificationId",
                table: "EmployeeDetail",
                column: "HigherQualificationId",
                principalTable: "QualificationDetails",
                principalColumn: "QualificationId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeDetail_QualificationDetails_HigherQualificationId",
                table: "EmployeeDetail");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeDetail_HigherQualificationId",
                table: "EmployeeDetail");

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "DesignationDetail",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "DesignationDetail",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "DesignationDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedById",
                table: "DesignationDetail",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "DesignationDetail",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DesignationDetail_CreatedById",
                table: "DesignationDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DesignationDetail_ModifiedById",
                table: "DesignationDetail",
                column: "ModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_DesignationDetail_AspNetUsers_CreatedById",
                table: "DesignationDetail",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DesignationDetail_AspNetUsers_ModifiedById",
                table: "DesignationDetail",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

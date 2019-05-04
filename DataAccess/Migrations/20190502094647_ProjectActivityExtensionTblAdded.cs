using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class ProjectActivityExtensionTblAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectActivityProvinceDetail_ProjectActivityDetail_ActivityId",
                table: "ProjectActivityProvinceDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectActivityProvinceDetail_ProvinceDetails_ProvinceId",
                table: "ProjectActivityProvinceDetail");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "ProjectActivityDetail",
                newName: "ActualStartDate");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "ProjectActivityDetail",
                newName: "ActualEndDate");

            migrationBuilder.AlterColumn<int>(
                name: "ProvinceId",
                table: "ProjectActivityProvinceDetail",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ActivityId",
                table: "ProjectActivityProvinceDetail",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "ProjectActivityProvinceDetail",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "ProjectActivityProvinceDetail",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ProjectActivityProvinceDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedById",
                table: "ProjectActivityProvinceDetail",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "ProjectActivityProvinceDetail",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ActivityName",
                table: "ProjectActivityDetail",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ChallengesAndSolutions",
                table: "ProjectActivityDetail",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "ProjectActivityDetail",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ProjectActivityExtensions",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ExtensionId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ActivityId = table.Column<long>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectActivityExtensions", x => x.ExtensionId);
                    table.ForeignKey(
                        name: "FK_ProjectActivityExtensions_ProjectActivityDetail_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "ProjectActivityDetail",
                        principalColumn: "ActivityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectActivityExtensions_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectActivityExtensions_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivityProvinceDetail_CreatedById",
                table: "ProjectActivityProvinceDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivityProvinceDetail_ModifiedById",
                table: "ProjectActivityProvinceDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivityExtensions_ActivityId",
                table: "ProjectActivityExtensions",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivityExtensions_CreatedById",
                table: "ProjectActivityExtensions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivityExtensions_ModifiedById",
                table: "ProjectActivityExtensions",
                column: "ModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectActivityProvinceDetail_ProjectActivityDetail_ActivityId",
                table: "ProjectActivityProvinceDetail",
                column: "ActivityId",
                principalTable: "ProjectActivityDetail",
                principalColumn: "ActivityId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectActivityProvinceDetail_AspNetUsers_CreatedById",
                table: "ProjectActivityProvinceDetail",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectActivityProvinceDetail_AspNetUsers_ModifiedById",
                table: "ProjectActivityProvinceDetail",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectActivityProvinceDetail_ProvinceDetails_ProvinceId",
                table: "ProjectActivityProvinceDetail",
                column: "ProvinceId",
                principalTable: "ProvinceDetails",
                principalColumn: "ProvinceId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectActivityProvinceDetail_ProjectActivityDetail_ActivityId",
                table: "ProjectActivityProvinceDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectActivityProvinceDetail_AspNetUsers_CreatedById",
                table: "ProjectActivityProvinceDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectActivityProvinceDetail_AspNetUsers_ModifiedById",
                table: "ProjectActivityProvinceDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_ProjectActivityProvinceDetail_ProvinceDetails_ProvinceId",
                table: "ProjectActivityProvinceDetail");

            migrationBuilder.DropTable(
                name: "ProjectActivityExtensions");

            migrationBuilder.DropIndex(
                name: "IX_ProjectActivityProvinceDetail_CreatedById",
                table: "ProjectActivityProvinceDetail");

            migrationBuilder.DropIndex(
                name: "IX_ProjectActivityProvinceDetail_ModifiedById",
                table: "ProjectActivityProvinceDetail");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "ProjectActivityProvinceDetail");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "ProjectActivityProvinceDetail");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ProjectActivityProvinceDetail");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "ProjectActivityProvinceDetail");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "ProjectActivityProvinceDetail");

            migrationBuilder.DropColumn(
                name: "ChallengesAndSolutions",
                table: "ProjectActivityDetail");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "ProjectActivityDetail");

            migrationBuilder.RenameColumn(
                name: "ActualStartDate",
                table: "ProjectActivityDetail",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "ActualEndDate",
                table: "ProjectActivityDetail",
                newName: "EndDate");

            migrationBuilder.AlterColumn<int>(
                name: "ProvinceId",
                table: "ProjectActivityProvinceDetail",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "ActivityId",
                table: "ProjectActivityProvinceDetail",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<string>(
                name: "ActivityName",
                table: "ProjectActivityDetail",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectActivityProvinceDetail_ProjectActivityDetail_ActivityId",
                table: "ProjectActivityProvinceDetail",
                column: "ActivityId",
                principalTable: "ProjectActivityDetail",
                principalColumn: "ActivityId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectActivityProvinceDetail_ProvinceDetails_ProvinceId",
                table: "ProjectActivityProvinceDetail",
                column: "ProvinceId",
                principalTable: "ProvinceDetails",
                principalColumn: "ProvinceId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

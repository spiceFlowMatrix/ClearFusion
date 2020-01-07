using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class addBaseEntityInGeneratorItemDetailTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "GeneratorItemDetail",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "GeneratorItemDetail",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "GeneratorItemDetail",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedById",
                table: "GeneratorItemDetail",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "GeneratorItemDetail",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "GeneratorItemDetail");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "GeneratorItemDetail");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "GeneratorItemDetail");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "GeneratorItemDetail");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "GeneratorItemDetail");
        }
    }
}

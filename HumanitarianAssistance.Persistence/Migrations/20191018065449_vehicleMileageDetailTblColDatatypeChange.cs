using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class vehicleMileageDetailTblColDatatypeChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Month",
                table: "VehicleMileageDetail");

            migrationBuilder.AddColumn<DateTime>(
                name: "MileageMonth",
                table: "VehicleMileageDetail",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MileageMonth",
                table: "VehicleMileageDetail");

            migrationBuilder.AddColumn<int>(
                name: "Month",
                table: "VehicleMileageDetail",
                nullable: false,
                defaultValue: 0);
        }
    }
}

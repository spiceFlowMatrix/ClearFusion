using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class DataTransfer10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.AddColumn<int>(
				name: "AccountNote",
				table: "ChartAccountDetail",
				nullable: true);

			migrationBuilder.AddColumn<string>(
				name: "DepMethod",
				table: "ChartAccountDetail",
				nullable: true);

			migrationBuilder.AddColumn<float>(
				name: "DepRate",
				table: "ChartAccountDetail",
				nullable: true);

			migrationBuilder.AddColumn<string>(
				name: "MDCode",
				table: "ChartAccountDetail",
				nullable: true);

			migrationBuilder.AddColumn<bool>(
				name: "Show",
				table: "ChartAccountDetail",
				nullable: true);
		}

        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.DropColumn(
				name: "AccountNote",
				table: "ChartAccountDetail");

			migrationBuilder.DropColumn(
				name: "DepMethod",
				table: "ChartAccountDetail");

			migrationBuilder.DropColumn(
				name: "DepRate",
				table: "ChartAccountDetail");

			migrationBuilder.DropColumn(
				name: "MDCode",
				table: "ChartAccountDetail");

			migrationBuilder.DropColumn(
				name: "Show",
				table: "ChartAccountDetail");
		}
    }
}

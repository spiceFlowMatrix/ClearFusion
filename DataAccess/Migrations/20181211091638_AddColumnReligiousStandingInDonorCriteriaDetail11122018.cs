using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddColumnReligiousStandingInDonorCriteriaDetail11122018 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DonorFinancingHistory",
                table: "DonorCriteriaDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PoliticalStanding",
                table: "DonorCriteriaDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReligiousStanding",
                table: "DonorCriteriaDetail",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DonorFinancingHistory",
                table: "DonorCriteriaDetail");

            migrationBuilder.DropColumn(
                name: "PoliticalStanding",
                table: "DonorCriteriaDetail");

            migrationBuilder.DropColumn(
                name: "ReligiousStanding",
                table: "DonorCriteriaDetail");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class jobDetailsCHange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "ContractId",
                table: "JobDetails",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobDetails_ContractId",
                table: "JobDetails",
                column: "ContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobDetails_ContractDetails_ContractId",
                table: "JobDetails",
                column: "ContractId",
                principalTable: "ContractDetails",
                principalColumn: "ContractId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobDetails_ContractDetails_ContractId",
                table: "JobDetails");

            migrationBuilder.DropIndex(
                name: "IX_JobDetails_ContractId",
                table: "JobDetails");

            migrationBuilder.AlterColumn<int>(
                name: "ContractId",
                table: "JobDetails",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);
        }
    }
}

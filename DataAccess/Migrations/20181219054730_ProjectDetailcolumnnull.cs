using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class ProjectDetailcolumnnull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsProposalComplate",
                table: "ProjectDetail",
                nullable: true,
                oldClrType: typeof(bool));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsProposalComplate",
                table: "ProjectDetail",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);
        }
    }
}

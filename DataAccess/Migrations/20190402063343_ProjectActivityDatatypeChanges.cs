using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class ProjectActivityDatatypeChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "MonitoringProgress",
                table: "ProjectActivityDetail",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<float>(
                name: "ImplementationProgress",
                table: "ProjectActivityDetail",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MonitoringProgress",
                table: "ProjectActivityDetail",
                nullable: true,
                oldClrType: typeof(float),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ImplementationProgress",
                table: "ProjectActivityDetail",
                nullable: true,
                oldClrType: typeof(float),
                oldNullable: true);
        }
    }
}

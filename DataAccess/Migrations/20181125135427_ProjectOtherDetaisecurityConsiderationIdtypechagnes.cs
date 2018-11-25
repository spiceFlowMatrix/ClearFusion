using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class ProjectOtherDetaisecurityConsiderationIdtypechagnes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SecurityConsiderationId",
                table: "ProjectOtherDetail",
                nullable: true,
                oldClrType: typeof(long),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "SecurityConsiderationId",
                table: "ProjectOtherDetail",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}

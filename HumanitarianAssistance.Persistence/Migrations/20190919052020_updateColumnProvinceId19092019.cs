using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class updateColumnProvinceId19092019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectHiringRequestDetail_ProvinceDetails_ProvinceDetailsP~",
                table: "ProjectHiringRequestDetail");

            migrationBuilder.DropColumn(
                name: "ProviceId",
                table: "ProjectHiringRequestDetail");

            migrationBuilder.RenameColumn(
                name: "ProvinceDetailsProvinceId",
                table: "ProjectHiringRequestDetail",
                newName: "ProvinceId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectHiringRequestDetail_ProvinceDetailsProvinceId",
                table: "ProjectHiringRequestDetail",
                newName: "IX_ProjectHiringRequestDetail_ProvinceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectHiringRequestDetail_ProvinceDetails_ProvinceId",
                table: "ProjectHiringRequestDetail",
                column: "ProvinceId",
                principalTable: "ProvinceDetails",
                principalColumn: "ProvinceId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProjectHiringRequestDetail_ProvinceDetails_ProvinceId",
                table: "ProjectHiringRequestDetail");

            migrationBuilder.RenameColumn(
                name: "ProvinceId",
                table: "ProjectHiringRequestDetail",
                newName: "ProvinceDetailsProvinceId");

            migrationBuilder.RenameIndex(
                name: "IX_ProjectHiringRequestDetail_ProvinceId",
                table: "ProjectHiringRequestDetail",
                newName: "IX_ProjectHiringRequestDetail_ProvinceDetailsProvinceId");

            migrationBuilder.AddColumn<int>(
                name: "ProviceId",
                table: "ProjectHiringRequestDetail",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProjectHiringRequestDetail_ProvinceDetails_ProvinceDetailsP~",
                table: "ProjectHiringRequestDetail",
                column: "ProvinceDetailsProvinceId",
                principalTable: "ProvinceDetails",
                principalColumn: "ProvinceId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

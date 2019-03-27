using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class updateScheduleTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ChannelId",
                table: "ScheduleDetails",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "MediumId",
                table: "ScheduleDetails",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleDetails_ChannelId",
                table: "ScheduleDetails",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleDetails_MediumId",
                table: "ScheduleDetails",
                column: "MediumId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleDetails_Channel_ChannelId",
                table: "ScheduleDetails",
                column: "ChannelId",
                principalTable: "Channel",
                principalColumn: "ChannelId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduleDetails_Mediums_MediumId",
                table: "ScheduleDetails",
                column: "MediumId",
                principalTable: "Mediums",
                principalColumn: "MediumId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleDetails_Channel_ChannelId",
                table: "ScheduleDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduleDetails_Mediums_MediumId",
                table: "ScheduleDetails");

            migrationBuilder.DropIndex(
                name: "IX_ScheduleDetails_ChannelId",
                table: "ScheduleDetails");

            migrationBuilder.DropIndex(
                name: "IX_ScheduleDetails_MediumId",
                table: "ScheduleDetails");

            migrationBuilder.DropColumn(
                name: "ChannelId",
                table: "ScheduleDetails");

            migrationBuilder.DropColumn(
                name: "MediumId",
                table: "ScheduleDetails");
        }
    }
}

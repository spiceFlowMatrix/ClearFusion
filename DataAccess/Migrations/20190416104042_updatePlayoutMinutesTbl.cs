using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class updatePlayoutMinutesTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayoutMinutes_PolicyDetails_PolicyId",
                table: "PlayoutMinutes");

            migrationBuilder.RenameColumn(
                name: "PolicyId",
                table: "PlayoutMinutes",
                newName: "ScheduleId");

            migrationBuilder.RenameIndex(
                name: "IX_PlayoutMinutes_PolicyId",
                table: "PlayoutMinutes",
                newName: "IX_PlayoutMinutes_ScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayoutMinutes_ScheduleDetails_ScheduleId",
                table: "PlayoutMinutes",
                column: "ScheduleId",
                principalTable: "ScheduleDetails",
                principalColumn: "ScheduleId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayoutMinutes_ScheduleDetails_ScheduleId",
                table: "PlayoutMinutes");

            migrationBuilder.RenameColumn(
                name: "ScheduleId",
                table: "PlayoutMinutes",
                newName: "PolicyId");

            migrationBuilder.RenameIndex(
                name: "IX_PlayoutMinutes_ScheduleId",
                table: "PlayoutMinutes",
                newName: "IX_PlayoutMinutes_PolicyId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayoutMinutes_PolicyDetails_PolicyId",
                table: "PlayoutMinutes",
                column: "PolicyId",
                principalTable: "PolicyDetails",
                principalColumn: "PolicyId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

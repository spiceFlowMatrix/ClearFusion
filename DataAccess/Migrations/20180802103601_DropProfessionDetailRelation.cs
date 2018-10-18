using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class DropProfessionDetailRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_JobHiringDetails_ProfessionDetails_ProfessionId",
            //    table: "JobHiringDetails");

            //migrationBuilder.DropTable(
            //    name: "ProfessionDetails");

            migrationBuilder.DropIndex(
                name: "IX_JobHiringDetails_ProfessionId",
                table: "JobHiringDetails");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "ProfessionDetails",
            //    columns: table => new
            //    {
            //        ProfessionId = table.Column<int>(type: "serial", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
            //        CreatedById = table.Column<string>(nullable: true),
            //        CreatedDate = table.Column<DateTime>(nullable: true),
            //        IsDeleted = table.Column<bool>(nullable: true),
            //        ModifiedById = table.Column<string>(nullable: true),
            //        ModifiedDate = table.Column<DateTime>(nullable: true),
            //        ProfessionDari = table.Column<string>(nullable: true),
            //        ProfessionName = table.Column<string>(maxLength: 100, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ProfessionDetails", x => x.ProfessionId);
            //        table.ForeignKey(
            //            name: "FK_ProfessionDetails_AspNetUsers_CreatedById",
            //            column: x => x.CreatedById,
            //            principalTable: "AspNetUsers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //        table.ForeignKey(
            //            name: "FK_ProfessionDetails_AspNetUsers_ModifiedById",
            //            column: x => x.ModifiedById,
            //            principalTable: "AspNetUsers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Restrict);
            //    });

            migrationBuilder.CreateIndex(
                name: "IX_JobHiringDetails_ProfessionId",
                table: "JobHiringDetails",
                column: "ProfessionId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ProfessionDetails_CreatedById",
            //    table: "ProfessionDetails",
            //    column: "CreatedById");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ProfessionDetails_ModifiedById",
            //    table: "ProfessionDetails",
            //    column: "ModifiedById");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_JobHiringDetails_ProfessionDetails_ProfessionId",
            //    table: "JobHiringDetails",
            //    column: "ProfessionId",
            //    principalTable: "ProfessionDetails",
            //    principalColumn: "ProfessionId",
            //    onDelete: ReferentialAction.Restrict);
        }
    }
}

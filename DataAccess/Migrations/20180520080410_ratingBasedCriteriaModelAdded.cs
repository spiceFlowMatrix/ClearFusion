using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class ratingBasedCriteriaModelAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RatingBasedCriteria",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    RatingBasedCriteriaId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    InterviewDetailsId = table.Column<int>(nullable: false),
                    CriteriaQuestion = table.Column<string>(nullable: true),
                    Rating = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingBasedCriteria", x => x.RatingBasedCriteriaId);
                    table.ForeignKey(
                        name: "FK_RatingBasedCriteria_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RatingBasedCriteria_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RatingBasedCriteria_CreatedById",
                table: "RatingBasedCriteria",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_RatingBasedCriteria_ModifiedById",
                table: "RatingBasedCriteria",
                column: "ModifiedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RatingBasedCriteria");
        }
    }
}

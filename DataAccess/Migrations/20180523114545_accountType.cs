using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class accountType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnalyticalDetail");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnalyticalDetail",
                columns: table => new
                {
                    AnalyticalId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Area = table.Column<string>(maxLength: 10, nullable: true),
                    Attachment = table.Column<string>(maxLength: 100, nullable: true),
                    BLAmount = table.Column<float>(nullable: false),
                    BLCurrCode = table.Column<string>(maxLength: 5, nullable: true),
                    BLType = table.Column<byte>(nullable: false),
                    CostBook = table.Column<string>(maxLength: 10, nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    DonorCode = table.Column<string>(maxLength: 50, nullable: true),
                    EndDate = table.Column<DateTime>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Job = table.Column<string>(maxLength: 10, nullable: true),
                    MDCode = table.Column<string>(maxLength: 10, nullable: true),
                    MemoCode = table.Column<string>(maxLength: 10, nullable: true),
                    MemoName = table.Column<string>(maxLength: 200, nullable: true),
                    MemoType = table.Column<byte>(nullable: false),
                    ModifiedById = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    Program = table.Column<string>(maxLength: 10, nullable: true),
                    Project = table.Column<string>(maxLength: 10, nullable: true),
                    ReceivedAmount = table.Column<float>(nullable: false),
                    Sector = table.Column<string>(maxLength: 10, nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnalyticalDetail", x => x.AnalyticalId);
                    table.ForeignKey(
                        name: "FK_AnalyticalDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnalyticalDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnalyticalDetail_CreatedById",
                table: "AnalyticalDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AnalyticalDetail_ModifiedById",
                table: "AnalyticalDetail",
                column: "ModifiedById");
        }
    }
}

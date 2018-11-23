using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class ChartOdAccountCodeFieldAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoucherDetail_ChartOfAccountNew_ChartOfAccountNewId",
                table: "VoucherDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_VoucherTransactions_ChartOfAccountNew_ChartOfAccountNewId",
                table: "VoucherTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_VoucherTransactions_ChartOfAccountNew_ChartOfAccountNewId1",
                table: "VoucherTransactions");

            migrationBuilder.DropIndex(
                name: "IX_VoucherTransactions_ChartOfAccountNewId",
                table: "VoucherTransactions");

            migrationBuilder.DropIndex(
                name: "IX_VoucherTransactions_ChartOfAccountNewId1",
                table: "VoucherTransactions");

            migrationBuilder.DropIndex(
                name: "IX_VoucherDetail_ChartOfAccountNewId",
                table: "VoucherDetail");

            migrationBuilder.DropColumn(
                name: "ChartOfAccountNewId",
                table: "VoucherTransactions");

            migrationBuilder.DropColumn(
                name: "ChartOfAccountNewId1",
                table: "VoucherTransactions");

            migrationBuilder.DropColumn(
                name: "ChartOfAccountNewId",
                table: "VoucherDetail");

            migrationBuilder.AddColumn<long>(
                name: "ProjectId",
                table: "ProgramDetail",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "ChartOfAccountNewCode",
                table: "ChartOfAccountNew",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ApproveProjectDetails",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ApproveProjrctId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ProjectId = table.Column<long>(nullable: false),
                    CommentText = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    FilePath = table.Column<string>(nullable: true),
                    IsApproved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApproveProjectDetails", x => x.ApproveProjrctId);
                    table.ForeignKey(
                        name: "FK_ApproveProjectDetails_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApproveProjectDetails_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApproveProjectDetails_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WinProjectDetails",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    WinProjectId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ProjectId = table.Column<long>(nullable: false),
                    CommentText = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    FilePath = table.Column<string>(nullable: true),
                    IsWin = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WinProjectDetails", x => x.WinProjectId);
                    table.ForeignKey(
                        name: "FK_WinProjectDetails_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WinProjectDetails_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WinProjectDetails_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApproveProjectDetails_CreatedById",
                table: "ApproveProjectDetails",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ApproveProjectDetails_ModifiedById",
                table: "ApproveProjectDetails",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ApproveProjectDetails_ProjectId",
                table: "ApproveProjectDetails",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_WinProjectDetails_CreatedById",
                table: "WinProjectDetails",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_WinProjectDetails_ModifiedById",
                table: "WinProjectDetails",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_WinProjectDetails_ProjectId",
                table: "WinProjectDetails",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApproveProjectDetails");

            migrationBuilder.DropTable(
                name: "WinProjectDetails");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "ProgramDetail");

            migrationBuilder.DropColumn(
                name: "ChartOfAccountNewCode",
                table: "ChartOfAccountNew");

            migrationBuilder.AddColumn<long>(
                name: "ChartOfAccountNewId",
                table: "VoucherTransactions",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChartOfAccountNewId1",
                table: "VoucherTransactions",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ChartOfAccountNewId",
                table: "VoucherDetail",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactions_ChartOfAccountNewId",
                table: "VoucherTransactions",
                column: "ChartOfAccountNewId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactions_ChartOfAccountNewId1",
                table: "VoucherTransactions",
                column: "ChartOfAccountNewId1");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherDetail_ChartOfAccountNewId",
                table: "VoucherDetail",
                column: "ChartOfAccountNewId");

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherDetail_ChartOfAccountNew_ChartOfAccountNewId",
                table: "VoucherDetail",
                column: "ChartOfAccountNewId",
                principalTable: "ChartOfAccountNew",
                principalColumn: "ChartOfAccountNewId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherTransactions_ChartOfAccountNew_ChartOfAccountNewId",
                table: "VoucherTransactions",
                column: "ChartOfAccountNewId",
                principalTable: "ChartOfAccountNew",
                principalColumn: "ChartOfAccountNewId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VoucherTransactions_ChartOfAccountNew_ChartOfAccountNewId1",
                table: "VoucherTransactions",
                column: "ChartOfAccountNewId1",
                principalTable: "ChartOfAccountNew",
                principalColumn: "ChartOfAccountNewId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

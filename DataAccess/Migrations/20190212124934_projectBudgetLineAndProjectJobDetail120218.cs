using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class projectBudgetLineAndProjectJobDetail120218 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProjectJobDetail",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ProjectJobId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ProjectJobCode = table.Column<string>(nullable: true),
                    ProjectJobName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectJobDetail", x => x.ProjectJobId);
                    table.ForeignKey(
                        name: "FK_ProjectJobDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectJobDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectBudgetLineDetail",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    BudgetLineId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    BudgetCode = table.Column<string>(nullable: true),
                    BudgetName = table.Column<string>(nullable: true),
                    ProjectJobId = table.Column<long>(nullable: true),
                    InitialBudget = table.Column<double>(nullable: true),
                    ProjectId = table.Column<long>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectBudgetLineDetail", x => x.BudgetLineId);
                    table.ForeignKey(
                        name: "FK_ProjectBudgetLineDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectBudgetLineDetail_CurrencyDetails_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "CurrencyDetails",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectBudgetLineDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectBudgetLineDetail_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectBudgetLineDetail_ProjectJobDetail_ProjectJobId",
                        column: x => x.ProjectJobId,
                        principalTable: "ProjectJobDetail",
                        principalColumn: "ProjectJobId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectBudgetLineDetail_CreatedById",
                table: "ProjectBudgetLineDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectBudgetLineDetail_CurrencyId",
                table: "ProjectBudgetLineDetail",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectBudgetLineDetail_ModifiedById",
                table: "ProjectBudgetLineDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectBudgetLineDetail_ProjectId",
                table: "ProjectBudgetLineDetail",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectBudgetLineDetail_ProjectJobId",
                table: "ProjectBudgetLineDetail",
                column: "ProjectJobId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectJobDetail_CreatedById",
                table: "ProjectJobDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectJobDetail_ModifiedById",
                table: "ProjectJobDetail",
                column: "ModifiedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectBudgetLineDetail");

            migrationBuilder.DropTable(
                name: "ProjectJobDetail");
        }
    }
}

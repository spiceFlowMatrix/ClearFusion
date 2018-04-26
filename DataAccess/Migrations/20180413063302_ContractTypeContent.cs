using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DataAccess.Migrations
{
    public partial class ContractTypeContent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContractTypeContent",
                columns: table => new
                {
                    ContractTypeContentId = table.Column<int>(type: "int4", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ContentDari = table.Column<string>(type: "text", nullable: true),
                    ContentEnglish = table.Column<string>(type: "text", nullable: true),
                    CreatedById = table.Column<string>(type: "text", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    EmployeeContractTypeId = table.Column<int>(type: "int4", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bool", nullable: false),
                    ModifiedById = table.Column<string>(type: "text", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractTypeContent", x => x.ContractTypeContentId);
                    table.ForeignKey(
                        name: "FK_ContractTypeContent_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContractTypeContent_EmployeeContractType_EmployeeContractTypeId",
                        column: x => x.EmployeeContractTypeId,
                        principalTable: "EmployeeContractType",
                        principalColumn: "EmployeeContractTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContractTypeContent_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContractTypeContent_CreatedById",
                table: "ContractTypeContent",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ContractTypeContent_EmployeeContractTypeId",
                table: "ContractTypeContent",
                column: "EmployeeContractTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractTypeContent_ModifiedById",
                table: "ContractTypeContent",
                column: "ModifiedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractTypeContent");
        }
    }
}

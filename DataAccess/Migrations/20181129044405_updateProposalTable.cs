using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class updateProposalTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BudgetFileExtType",
                table: "ProjectProposalDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConceptFileExtType",
                table: "ProjectProposalDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EDIFileExtType",
                table: "ProjectProposalDetail",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsProposalAccept",
                table: "ProjectProposalDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PresentationExtType",
                table: "ProjectProposalDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectAssignTo",
                table: "ProjectProposalDetail",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProposalBudget",
                table: "ProjectProposalDetail",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ProposalDueDate",
                table: "ProjectProposalDetail",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ProposalExtType",
                table: "ProjectProposalDetail",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ProposalStartDate",
                table: "ProjectProposalDetail",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BudgetFileExtType",
                table: "ProjectProposalDetail");

            migrationBuilder.DropColumn(
                name: "ConceptFileExtType",
                table: "ProjectProposalDetail");

            migrationBuilder.DropColumn(
                name: "EDIFileExtType",
                table: "ProjectProposalDetail");

            migrationBuilder.DropColumn(
                name: "IsProposalAccept",
                table: "ProjectProposalDetail");

            migrationBuilder.DropColumn(
                name: "PresentationExtType",
                table: "ProjectProposalDetail");

            migrationBuilder.DropColumn(
                name: "ProjectAssignTo",
                table: "ProjectProposalDetail");

            migrationBuilder.DropColumn(
                name: "ProposalBudget",
                table: "ProjectProposalDetail");

            migrationBuilder.DropColumn(
                name: "ProposalDueDate",
                table: "ProjectProposalDetail");

            migrationBuilder.DropColumn(
                name: "ProposalExtType",
                table: "ProjectProposalDetail");

            migrationBuilder.DropColumn(
                name: "ProposalStartDate",
                table: "ProjectProposalDetail");
        }
    }
}

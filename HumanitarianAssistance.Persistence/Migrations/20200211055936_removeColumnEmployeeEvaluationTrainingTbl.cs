using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class removeColumnEmployeeEvaluationTrainingTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CatchLevel",
                table: "EmployeeEvaluationTraining");

            migrationBuilder.DropColumn(
                name: "Participated",
                table: "EmployeeEvaluationTraining");

            migrationBuilder.DropColumn(
                name: "RefresherTrm",
                table: "EmployeeEvaluationTraining");

            migrationBuilder.DropColumn(
                name: "TrainingProgram",
                table: "EmployeeEvaluationTraining");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CatchLevel",
                table: "EmployeeEvaluationTraining",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Participated",
                table: "EmployeeEvaluationTraining",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefresherTrm",
                table: "EmployeeEvaluationTraining",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TrainingProgram",
                table: "EmployeeEvaluationTraining",
                nullable: true);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class EmployeedetailColumnsAddition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_ChartAccountDetail_ChartAccountDetail_ParentID",
            //    table: "ChartAccountDetail");

            //migrationBuilder.DropIndex(
            //    name: "IX_ChartAccountDetail_ParentID",
            //    table: "ChartAccountDetail");

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BirthPlace",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DocumentGUID",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DocumentType",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeTypeId",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExperienceMonth",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExperienceYear",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Extension",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GradeId",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HigherQualificationId",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IssuePlace",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NationalityId",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PassportNo",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProvinceId",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "QualificationDetailsQualificationId",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Resume",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SexId",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "University",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "ParentID",
                table: "ChartAccountDetail",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ProfessionDetails",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ProfessionId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ProfessionName = table.Column<string>(maxLength: 100, nullable: true),
                    ProfessionDari = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessionDetails", x => x.ProfessionId);
                    table.ForeignKey(
                        name: "FK_ProfessionDetails_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProfessionDetails_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StorePurchaseOrders_IssuedToEmployeeId",
                table: "StorePurchaseOrders",
                column: "IssuedToEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreItemPurchases_PurchasedById",
                table: "StoreItemPurchases",
                column: "PurchasedById");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewScheduleDetails_EmployeeId",
                table: "InterviewScheduleDetails",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExistInterviewDetails_EmployeeID",
                table: "ExistInterviewDetails",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaryDetails_EmployeeId",
                table: "EmployeeSalaryDetails",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaryBudget_EmployeeID",
                table: "EmployeeSalaryBudget",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaryAnalyticalInfo_EmployeeID",
                table: "EmployeeSalaryAnalyticalInfo",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRelativeInfo_EmployeeID",
                table: "EmployeeRelativeInfo",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProfessionalDetail_EmployeeId",
                table: "EmployeeProfessionalDetail",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayrollMonth_EmployeeID",
                table: "EmployeePayrollMonth",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayrollForMonth_EmployeeID",
                table: "EmployeePayrollForMonth",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayroll_EmployeeID",
                table: "EmployeePayroll",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePaymentTypes_EmployeeID",
                table: "EmployeePaymentTypes",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeOtherSkills_EmployeeID",
                table: "EmployeeOtherSkills",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeMonthlyPayroll_EmployeeID",
                table: "EmployeeMonthlyPayroll",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeInfoReferences_EmployeeID",
                table: "EmployeeInfoReferences",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeHistoryOutsideOrganization_EmployeeID",
                table: "EmployeeHistoryOutsideOrganization",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeHistoryOutsideCountry_EmployeeID",
                table: "EmployeeHistoryOutsideCountry",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeHistoryDetail_EmployeeID",
                table: "EmployeeHistoryDetail",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeHealthInfo_EmployeeId",
                table: "EmployeeHealthInfo",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEducations_EmployeeID",
                table: "EmployeeEducations",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDocumentDetail_EmployeeID",
                table: "EmployeeDocumentDetail",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetail_CountryId",
                table: "EmployeeDetail",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetail_EmployeeTypeId",
                table: "EmployeeDetail",
                column: "EmployeeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetail_NationalityId",
                table: "EmployeeDetail",
                column: "NationalityId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetail_ProvinceId",
                table: "EmployeeDetail",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetail_QualificationDetailsQualificationId",
                table: "EmployeeDetail",
                column: "QualificationDetailsQualificationId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAttendance_EmployeeId",
                table: "EmployeeAttendance",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAppraisalDetails_EmployeeId",
                table: "EmployeeAppraisalDetails",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeApplyLeave_EmployeeId",
                table: "EmployeeApplyLeave",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetLineEmployees_EmployeeId",
                table: "BudgetLineEmployees",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignLeaveToEmployee_EmployeeId",
                table: "AssignLeaveToEmployee",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessionDetails_CreatedById",
                table: "ProfessionDetails",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessionDetails_ModifiedById",
                table: "ProfessionDetails",
                column: "ModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_AssignLeaveToEmployee_EmployeeDetail_EmployeeId",
                table: "AssignLeaveToEmployee",
                column: "EmployeeId",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BudgetLineEmployees_EmployeeDetail_EmployeeId",
                table: "BudgetLineEmployees",
                column: "EmployeeId",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeApplyLeave_EmployeeDetail_EmployeeId",
                table: "EmployeeApplyLeave",
                column: "EmployeeId",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeAppraisalDetails_EmployeeDetail_EmployeeId",
                table: "EmployeeAppraisalDetails",
                column: "EmployeeId",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeAttendance_EmployeeDetail_EmployeeId",
                table: "EmployeeAttendance",
                column: "EmployeeId",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeDetail_CountryDetails_CountryId",
                table: "EmployeeDetail",
                column: "CountryId",
                principalTable: "CountryDetails",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeDetail_EmployeeType_EmployeeTypeId",
                table: "EmployeeDetail",
                column: "EmployeeTypeId",
                principalTable: "EmployeeType",
                principalColumn: "EmployeeTypeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeDetail_NationalityDetails_NationalityId",
                table: "EmployeeDetail",
                column: "NationalityId",
                principalTable: "NationalityDetails",
                principalColumn: "NationalityId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeDetail_ProvinceDetails_ProvinceId",
                table: "EmployeeDetail",
                column: "ProvinceId",
                principalTable: "ProvinceDetails",
                principalColumn: "ProvinceId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeDetail_QualificationDetails_QualificationDetailsQualificationId",
                table: "EmployeeDetail",
                column: "QualificationDetailsQualificationId",
                principalTable: "QualificationDetails",
                principalColumn: "QualificationId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeDocumentDetail_EmployeeDetail_EmployeeID",
                table: "EmployeeDocumentDetail",
                column: "EmployeeID",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeEducations_EmployeeDetail_EmployeeID",
                table: "EmployeeEducations",
                column: "EmployeeID",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeHealthInfo_EmployeeDetail_EmployeeId",
                table: "EmployeeHealthInfo",
                column: "EmployeeId",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeHistoryDetail_EmployeeDetail_EmployeeID",
                table: "EmployeeHistoryDetail",
                column: "EmployeeID",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeHistoryOutsideCountry_EmployeeDetail_EmployeeID",
                table: "EmployeeHistoryOutsideCountry",
                column: "EmployeeID",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeHistoryOutsideOrganization_EmployeeDetail_EmployeeID",
                table: "EmployeeHistoryOutsideOrganization",
                column: "EmployeeID",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeInfoReferences_EmployeeDetail_EmployeeID",
                table: "EmployeeInfoReferences",
                column: "EmployeeID",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeMonthlyPayroll_EmployeeDetail_EmployeeID",
                table: "EmployeeMonthlyPayroll",
                column: "EmployeeID",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeOtherSkills_EmployeeDetail_EmployeeID",
                table: "EmployeeOtherSkills",
                column: "EmployeeID",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePaymentTypes_EmployeeDetail_EmployeeID",
                table: "EmployeePaymentTypes",
                column: "EmployeeID",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePayroll_EmployeeDetail_EmployeeID",
                table: "EmployeePayroll",
                column: "EmployeeID",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePayrollForMonth_EmployeeDetail_EmployeeID",
                table: "EmployeePayrollForMonth",
                column: "EmployeeID",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePayrollMonth_EmployeeDetail_EmployeeID",
                table: "EmployeePayrollMonth",
                column: "EmployeeID",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProfessionalDetail_EmployeeDetail_EmployeeId",
                table: "EmployeeProfessionalDetail",
                column: "EmployeeId",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeRelativeInfo_EmployeeDetail_EmployeeID",
                table: "EmployeeRelativeInfo",
                column: "EmployeeID",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeSalaryAnalyticalInfo_EmployeeDetail_EmployeeID",
                table: "EmployeeSalaryAnalyticalInfo",
                column: "EmployeeID",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeSalaryBudget_EmployeeDetail_EmployeeID",
                table: "EmployeeSalaryBudget",
                column: "EmployeeID",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeSalaryDetails_EmployeeDetail_EmployeeId",
                table: "EmployeeSalaryDetails",
                column: "EmployeeId",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExistInterviewDetails_EmployeeDetail_EmployeeID",
                table: "ExistInterviewDetails",
                column: "EmployeeID",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InterviewScheduleDetails_EmployeeDetail_EmployeeId",
                table: "InterviewScheduleDetails",
                column: "EmployeeId",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreItemPurchases_EmployeeDetail_PurchasedById",
                table: "StoreItemPurchases",
                column: "PurchasedById",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StorePurchaseOrders_EmployeeDetail_IssuedToEmployeeId",
                table: "StorePurchaseOrders",
                column: "IssuedToEmployeeId",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AssignLeaveToEmployee_EmployeeDetail_EmployeeId",
                table: "AssignLeaveToEmployee");

            migrationBuilder.DropForeignKey(
                name: "FK_BudgetLineEmployees_EmployeeDetail_EmployeeId",
                table: "BudgetLineEmployees");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeApplyLeave_EmployeeDetail_EmployeeId",
                table: "EmployeeApplyLeave");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeAppraisalDetails_EmployeeDetail_EmployeeId",
                table: "EmployeeAppraisalDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeAttendance_EmployeeDetail_EmployeeId",
                table: "EmployeeAttendance");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeDetail_CountryDetails_CountryId",
                table: "EmployeeDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeDetail_EmployeeType_EmployeeTypeId",
                table: "EmployeeDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeDetail_NationalityDetails_NationalityId",
                table: "EmployeeDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeDetail_ProvinceDetails_ProvinceId",
                table: "EmployeeDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeDetail_QualificationDetails_QualificationDetailsQualificationId",
                table: "EmployeeDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeDocumentDetail_EmployeeDetail_EmployeeID",
                table: "EmployeeDocumentDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeEducations_EmployeeDetail_EmployeeID",
                table: "EmployeeEducations");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeHealthInfo_EmployeeDetail_EmployeeId",
                table: "EmployeeHealthInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeHistoryDetail_EmployeeDetail_EmployeeID",
                table: "EmployeeHistoryDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeHistoryOutsideCountry_EmployeeDetail_EmployeeID",
                table: "EmployeeHistoryOutsideCountry");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeHistoryOutsideOrganization_EmployeeDetail_EmployeeID",
                table: "EmployeeHistoryOutsideOrganization");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeInfoReferences_EmployeeDetail_EmployeeID",
                table: "EmployeeInfoReferences");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeMonthlyPayroll_EmployeeDetail_EmployeeID",
                table: "EmployeeMonthlyPayroll");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeOtherSkills_EmployeeDetail_EmployeeID",
                table: "EmployeeOtherSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePaymentTypes_EmployeeDetail_EmployeeID",
                table: "EmployeePaymentTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePayroll_EmployeeDetail_EmployeeID",
                table: "EmployeePayroll");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePayrollForMonth_EmployeeDetail_EmployeeID",
                table: "EmployeePayrollForMonth");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePayrollMonth_EmployeeDetail_EmployeeID",
                table: "EmployeePayrollMonth");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProfessionalDetail_EmployeeDetail_EmployeeId",
                table: "EmployeeProfessionalDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeRelativeInfo_EmployeeDetail_EmployeeID",
                table: "EmployeeRelativeInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeSalaryAnalyticalInfo_EmployeeDetail_EmployeeID",
                table: "EmployeeSalaryAnalyticalInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeSalaryBudget_EmployeeDetail_EmployeeID",
                table: "EmployeeSalaryBudget");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeSalaryDetails_EmployeeDetail_EmployeeId",
                table: "EmployeeSalaryDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ExistInterviewDetails_EmployeeDetail_EmployeeID",
                table: "ExistInterviewDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_InterviewScheduleDetails_EmployeeDetail_EmployeeId",
                table: "InterviewScheduleDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreItemPurchases_EmployeeDetail_PurchasedById",
                table: "StoreItemPurchases");

            migrationBuilder.DropForeignKey(
                name: "FK_StorePurchaseOrders_EmployeeDetail_IssuedToEmployeeId",
                table: "StorePurchaseOrders");

            migrationBuilder.DropTable(
                name: "ProfessionDetails");

            migrationBuilder.DropIndex(
                name: "IX_StorePurchaseOrders_IssuedToEmployeeId",
                table: "StorePurchaseOrders");

            migrationBuilder.DropIndex(
                name: "IX_StoreItemPurchases_PurchasedById",
                table: "StoreItemPurchases");

            migrationBuilder.DropIndex(
                name: "IX_InterviewScheduleDetails_EmployeeId",
                table: "InterviewScheduleDetails");

            migrationBuilder.DropIndex(
                name: "IX_ExistInterviewDetails_EmployeeID",
                table: "ExistInterviewDetails");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeSalaryDetails_EmployeeId",
                table: "EmployeeSalaryDetails");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeSalaryBudget_EmployeeID",
                table: "EmployeeSalaryBudget");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeSalaryAnalyticalInfo_EmployeeID",
                table: "EmployeeSalaryAnalyticalInfo");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeRelativeInfo_EmployeeID",
                table: "EmployeeRelativeInfo");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeProfessionalDetail_EmployeeId",
                table: "EmployeeProfessionalDetail");

            migrationBuilder.DropIndex(
                name: "IX_EmployeePayrollMonth_EmployeeID",
                table: "EmployeePayrollMonth");

            migrationBuilder.DropIndex(
                name: "IX_EmployeePayrollForMonth_EmployeeID",
                table: "EmployeePayrollForMonth");

            migrationBuilder.DropIndex(
                name: "IX_EmployeePayroll_EmployeeID",
                table: "EmployeePayroll");

            migrationBuilder.DropIndex(
                name: "IX_EmployeePaymentTypes_EmployeeID",
                table: "EmployeePaymentTypes");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeOtherSkills_EmployeeID",
                table: "EmployeeOtherSkills");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeMonthlyPayroll_EmployeeID",
                table: "EmployeeMonthlyPayroll");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeInfoReferences_EmployeeID",
                table: "EmployeeInfoReferences");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeHistoryOutsideOrganization_EmployeeID",
                table: "EmployeeHistoryOutsideOrganization");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeHistoryOutsideCountry_EmployeeID",
                table: "EmployeeHistoryOutsideCountry");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeHistoryDetail_EmployeeID",
                table: "EmployeeHistoryDetail");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeHealthInfo_EmployeeId",
                table: "EmployeeHealthInfo");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeEducations_EmployeeID",
                table: "EmployeeEducations");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeDocumentDetail_EmployeeID",
                table: "EmployeeDocumentDetail");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeDetail_CountryId",
                table: "EmployeeDetail");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeDetail_EmployeeTypeId",
                table: "EmployeeDetail");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeDetail_NationalityId",
                table: "EmployeeDetail");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeDetail_ProvinceId",
                table: "EmployeeDetail");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeDetail_QualificationDetailsQualificationId",
                table: "EmployeeDetail");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeAttendance_EmployeeId",
                table: "EmployeeAttendance");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeAppraisalDetails_EmployeeId",
                table: "EmployeeAppraisalDetails");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeApplyLeave_EmployeeId",
                table: "EmployeeApplyLeave");

            migrationBuilder.DropIndex(
                name: "IX_BudgetLineEmployees_EmployeeId",
                table: "BudgetLineEmployees");

            migrationBuilder.DropIndex(
                name: "IX_AssignLeaveToEmployee_EmployeeId",
                table: "AssignLeaveToEmployee");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "BirthPlace",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "DocumentGUID",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "DocumentType",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "EmployeeTypeId",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "ExperienceMonth",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "ExperienceYear",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "Extension",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "GradeId",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "HigherQualificationId",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "IssuePlace",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "NationalityId",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "PassportNo",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "ProvinceId",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "QualificationDetailsQualificationId",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "Resume",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "SexId",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "University",
                table: "EmployeeDetail");

            migrationBuilder.AlterColumn<int>(
                name: "ParentID",
                table: "ChartAccountDetail",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.CreateIndex(
                name: "IX_ChartAccountDetail_ParentID",
                table: "ChartAccountDetail",
                column: "ParentID");

            migrationBuilder.AddForeignKey(
                name: "FK_ChartAccountDetail_ChartAccountDetail_ParentID",
                table: "ChartAccountDetail",
                column: "ParentID",
                principalTable: "ChartAccountDetail",
                principalColumn: "AccountCode",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

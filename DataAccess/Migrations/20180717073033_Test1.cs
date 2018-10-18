using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class Test1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BankAccount",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "BasicPay",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "CapacityBuildingDeductibles",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CasualLeaveAllowance",
                table: "EmployeeDetail",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CityDari",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CloseRelativeList",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ContractEndDate",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContractEndDateDari",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ContractNumber",
                table: "EmployeeDetail",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "ContractPeriod",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "ContractPeriodDari",
                table: "EmployeeDetail",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTime>(
                name: "ContractStartDate",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContractStartDateDari",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContractStatus",
                table: "EmployeeDetail",
                maxLength: 5,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CurrencyCode",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Designation",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DesignationDari",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ETN",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EducationList",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmergencyLeaveAllowance",
                table: "EmployeeDetail",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeCodeDari",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeeNameDari",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Experience",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Extended",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FatherNameDari",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FineReason",
                table: "EmployeeDetail",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "FinesDeductibles",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "FireOn",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FireReason",
                table: "EmployeeDetail",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "FoodAllowance",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Grade",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "HireOn",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InternationalEmploymentList",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobDescription",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaternityLeaveAllowance",
                table: "EmployeeDetail",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "MedicalAllowance",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MedicalLeaveAllowance",
                table: "EmployeeDetail",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "MonthlyBasicPay",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NationalEmploymentList",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nationality",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NoOfChildren",
                table: "EmployeeDetail",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OfficeCode",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Other1Allowance",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Other1Description",
                table: "EmployeeDetail",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "Other2Allowance",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Other2Description",
                table: "EmployeeDetail",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "OtherDeductibles",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherSkillList",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "PensionDeductibles",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PeriodType",
                table: "EmployeeDetail",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PeriodTypeDari",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PoliticalPartyMembership",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreviousExperience",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Profession",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectID",
                table: "EmployeeDetail",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Province",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProvinceDari",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Qualification",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefereeList",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ResignedOn",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResignedReason",
                table: "EmployeeDetail",
                maxLength: 200,
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "SecurityDeductibles",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sex",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpeakLangSECTuageList",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpeakLanguageList",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TinGenerateDate",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TinNo",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "TrAllowance",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Training",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Village",
                table: "EmployeeDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WorkType",
                table: "EmployeeDetail",
                maxLength: 20,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankAccount",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "BasicPay",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "CapacityBuildingDeductibles",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "CasualLeaveAllowance",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "CityDari",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "CloseRelativeList",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "Comments",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "ContractEndDate",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "ContractEndDateDari",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "ContractNumber",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "ContractPeriod",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "ContractPeriodDari",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "ContractStartDate",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "ContractStartDateDari",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "ContractStatus",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "CurrencyCode",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "Department",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "Designation",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "DesignationDari",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "ETN",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "EducationList",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "EmergencyLeaveAllowance",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "EmployeeCodeDari",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "EmployeeNameDari",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "Experience",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "Extended",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "FatherNameDari",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "FineReason",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "FinesDeductibles",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "FireOn",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "FireReason",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "FoodAllowance",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "Grade",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "HireOn",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "InternationalEmploymentList",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "JobDescription",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "MaternityLeaveAllowance",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "MedicalAllowance",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "MedicalLeaveAllowance",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "MonthlyBasicPay",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "NationalEmploymentList",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "Nationality",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "NoOfChildren",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "OfficeCode",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "Other1Allowance",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "Other1Description",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "Other2Allowance",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "Other2Description",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "OtherDeductibles",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "OtherSkillList",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "PensionDeductibles",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "PeriodType",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "PeriodTypeDari",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "PoliticalPartyMembership",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "PreviousExperience",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "Profession",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "ProjectID",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "Province",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "ProvinceDari",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "Qualification",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "RefereeList",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "ResignedOn",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "ResignedReason",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "SecurityDeductibles",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "Sex",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "SpeakLangSECTuageList",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "SpeakLanguageList",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "TinGenerateDate",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "TinNo",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "TrAllowance",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "Training",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "Village",
                table: "EmployeeDetail");

            migrationBuilder.DropColumn(
                name: "WorkType",
                table: "EmployeeDetail");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class DropEmployeeDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            //migrationBuilder.DropForeignKey(
            //    name: "FK_EmployeeDocumentDetail_EmployeeDetail_EmployeeID",
            //    table: "EmployeeDocumentDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeEducations_EmployeeDetail_EmployeeID",
                table: "EmployeeEducations");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeHealthInfo_EmployeeDetail_EmployeeId",
                table: "EmployeeHealthInfo");

            //migrationBuilder.DropForeignKey(
            //    name: "FK_EmployeeHistoryDetail_EmployeeDetail_EmployeeID",
            //    table: "EmployeeHistoryDetail");

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
                name: "EmployeeDetail");

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

            //migrationBuilder.DropIndex(
            //    name: "IX_EmployeeHistoryDetail_EmployeeID",
            //    table: "EmployeeHistoryDetail");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeHealthInfo_EmployeeId",
                table: "EmployeeHealthInfo");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeEducations_EmployeeID",
                table: "EmployeeEducations");

            //migrationBuilder.DropIndex(
            //    name: "IX_EmployeeDocumentDetail_EmployeeID",
            //    table: "EmployeeDocumentDetail");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeDetail",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Age = table.Column<int>(nullable: true),
                    AttendanceId = table.Column<long>(nullable: true),
                    BirthPlace = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    CloseRelativeList = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    CountryId = table.Column<int>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    CurrentAddress = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<string>(nullable: true),
                    District = table.Column<string>(nullable: true),
                    DocumentGUID = table.Column<string>(nullable: true),
                    DocumentType = table.Column<int>(nullable: true),
                    EducationList = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    EmployeeCode = table.Column<string>(nullable: true),
                    EmployeeName = table.Column<string>(nullable: true),
                    EmployeePhoto = table.Column<string>(nullable: true),
                    EmployeeProfessionalId = table.Column<long>(nullable: true),
                    EmployeeTypeId = table.Column<int>(nullable: true),
                    Experience = table.Column<string>(nullable: true),
                    ExperienceMonth = table.Column<int>(nullable: true),
                    ExperienceYear = table.Column<int>(nullable: true),
                    Extension = table.Column<string>(nullable: true),
                    FatherName = table.Column<string>(nullable: true),
                    Fax = table.Column<string>(nullable: true),
                    Grade = table.Column<string>(nullable: true),
                    GradeId = table.Column<int>(nullable: true),
                    HigherQualificationId = table.Column<int>(nullable: true),
                    IDCard = table.Column<string>(nullable: true),
                    InternationalEmploymentList = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    IssuePlace = table.Column<string>(nullable: true),
                    Language = table.Column<string>(nullable: true),
                    MaritalStatus = table.Column<int>(nullable: true),
                    MaritalStatusString = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    NationalEmploymentList = table.Column<string>(nullable: true),
                    Nationality = table.Column<string>(nullable: true),
                    NationalityId = table.Column<int>(nullable: true),
                    NoOfChildren = table.Column<string>(nullable: true),
                    OtherSkillList = table.Column<string>(nullable: true),
                    Passport = table.Column<string>(nullable: true),
                    PassportNo = table.Column<string>(nullable: true),
                    PayrollId = table.Column<long>(nullable: true),
                    PermanentAddress = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    PlaceOfBirth = table.Column<string>(nullable: true),
                    PreviousWork = table.Column<string>(nullable: true),
                    Profession = table.Column<string>(nullable: true),
                    ProjectId = table.Column<long>(nullable: true),
                    Province = table.Column<string>(nullable: true),
                    ProvinceId = table.Column<int>(nullable: true),
                    Qualification = table.Column<string>(nullable: true),
                    ReferBy = table.Column<string>(nullable: true),
                    RefreeList = table.Column<string>(nullable: true),
                    RegCode = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    Resume = table.Column<string>(nullable: true),
                    SalaryId = table.Column<long>(nullable: true),
                    ScheduleId = table.Column<long>(nullable: true),
                    Sex = table.Column<string>(nullable: true),
                    SexId = table.Column<int>(nullable: true),
                    SpeakLanguageList = table.Column<string>(nullable: true),
                    University = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeDetail", x => x.EmployeeID);
                    table.ForeignKey(
                        name: "FK_EmployeeDetail_CountryDetails_CountryId",
                        column: x => x.CountryId,
                        principalTable: "CountryDetails",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeDetail_EmployeeProfessionalDetail_EmployeeProfessionalId",
                        column: x => x.EmployeeProfessionalId,
                        principalTable: "EmployeeProfessionalDetail",
                        principalColumn: "EmployeeProfessionalId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeDetail_EmployeeType_EmployeeTypeId",
                        column: x => x.EmployeeTypeId,
                        principalTable: "EmployeeType",
                        principalColumn: "EmployeeTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeDetail_QualificationDetails_HigherQualificationId",
                        column: x => x.HigherQualificationId,
                        principalTable: "QualificationDetails",
                        principalColumn: "QualificationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeDetail_NationalityDetails_NationalityId",
                        column: x => x.NationalityId,
                        principalTable: "NationalityDetails",
                        principalColumn: "NationalityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeDetail_ProvinceDetails_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "ProvinceDetails",
                        principalColumn: "ProvinceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeDetail_EmployeeSalaryDetails_SalaryId",
                        column: x => x.SalaryId,
                        principalTable: "EmployeeSalaryDetails",
                        principalColumn: "SalaryId",
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
                column: "EmployeeId");

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
                name: "IX_EmployeeDetail_CountryId",
                table: "EmployeeDetail",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetail_CreatedById",
                table: "EmployeeDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetail_EmployeeProfessionalId",
                table: "EmployeeDetail",
                column: "EmployeeProfessionalId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetail_EmployeeTypeId",
                table: "EmployeeDetail",
                column: "EmployeeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetail_HigherQualificationId",
                table: "EmployeeDetail",
                column: "HigherQualificationId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetail_ModifiedById",
                table: "EmployeeDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetail_NationalityId",
                table: "EmployeeDetail",
                column: "NationalityId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetail_ProvinceId",
                table: "EmployeeDetail",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetail_SalaryId",
                table: "EmployeeDetail",
                column: "SalaryId",
                unique: true);

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
    }
}

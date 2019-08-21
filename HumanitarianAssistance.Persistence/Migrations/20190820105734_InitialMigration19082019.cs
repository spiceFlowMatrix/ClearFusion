using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class InitialMigration19082019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountFilterType",
                columns: table => new
                {
                    AccountFilterTypeId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    AccountFilterTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountFilterType", x => x.AccountFilterTypeId);
                });

            migrationBuilder.CreateTable(
                name: "AccountHeadType",
                columns: table => new
                {
                    AccountHeadTypeId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    AccountHeadTypeName = table.Column<string>(nullable: true),
                    IsCreditBalancetype = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountHeadType", x => x.AccountHeadTypeId);
                });

            migrationBuilder.CreateTable(
                name: "AccountLevel",
                columns: table => new
                {
                    AccountLevelId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    AccountLevelName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountLevel", x => x.AccountLevelId);
                });

            migrationBuilder.CreateTable(
                name: "ActivityStatusDetail",
                columns: table => new
                {
                    StatusId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    StatusName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityStatusDetail", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "ActivityTypes",
                columns: table => new
                {
                    ActivityTypeId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ActivityName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityTypes", x => x.ActivityTypeId);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationPages",
                columns: table => new
                {
                    PageId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PageName = table.Column<string>(nullable: true),
                    ModuleName = table.Column<string>(nullable: true),
                    ModuleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationPages", x => x.PageId);
                });

            migrationBuilder.CreateTable(
                name: "AppraisalGeneralQuestions",
                columns: table => new
                {
                    AppraisalGeneralQuestionsId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    SequenceNo = table.Column<int>(nullable: true),
                    Question = table.Column<string>(nullable: true),
                    DariQuestion = table.Column<string>(nullable: true),
                    OfficeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppraisalGeneralQuestions", x => x.AppraisalGeneralQuestionsId);
                });

            migrationBuilder.CreateTable(
                name: "AreaDetail",
                columns: table => new
                {
                    AreaId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    AreaName = table.Column<string>(nullable: true),
                    AreaCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreaDetail", x => x.AreaId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AttendanceGroupMaster",
                columns: table => new
                {
                    AttendanceGroupId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceGroupMaster", x => x.AttendanceGroupId);
                });

            migrationBuilder.CreateTable(
                name: "BudgetLineType",
                columns: table => new
                {
                    BudgetLineTypeId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    BudgetLineTypeName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetLineType", x => x.BudgetLineTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CategoryName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "CodeType",
                columns: table => new
                {
                    CodeTypeId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CodeTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodeType", x => x.CodeTypeId);
                });

            migrationBuilder.CreateTable(
                name: "CountryDetails",
                columns: table => new
                {
                    CountryId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CountryName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryDetails", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "CurrencyDetails",
                columns: table => new
                {
                    CurrencyId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CurrencyCode = table.Column<string>(maxLength: 5, nullable: true),
                    CurrencyName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyDetails", x => x.CurrencyId);
                });

            migrationBuilder.CreateTable(
                name: "DesignationDetail",
                columns: table => new
                {
                    DesignationId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Designation = table.Column<string>(maxLength: 100, nullable: true),
                    DesignationDari = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignationDetail", x => x.DesignationId);
                });

            migrationBuilder.CreateTable(
                name: "DistrictDetail",
                columns: table => new
                {
                    DistrictID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    District = table.Column<string>(maxLength: 50, nullable: true),
                    ProvinceID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistrictDetail", x => x.DistrictID);
                });

            migrationBuilder.CreateTable(
                name: "DocumentFileDetail",
                columns: table => new
                {
                    DocumentFileId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ModuleId = table.Column<int>(nullable: false),
                    PageId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    RawFileMimeType = table.Column<string>(nullable: true),
                    RawFileSizeBytes = table.Column<long>(nullable: false),
                    StorageDirectoryPath = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentFileDetail", x => x.DocumentFileId);
                });

            migrationBuilder.CreateTable(
                name: "DonorDetail",
                columns: table => new
                {
                    DonorId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ContactPerson = table.Column<string>(nullable: true),
                    ContactDesignation = table.Column<string>(nullable: true),
                    ContactPersonEmail = table.Column<string>(nullable: true),
                    ContactPersonCell = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonorDetail", x => x.DonorId);
                });

            migrationBuilder.CreateTable(
                name: "EmailType",
                columns: table => new
                {
                    EmailTypeId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    EmailTypeName = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailType", x => x.EmailTypeId);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeAppraisalTeamMember",
                columns: table => new
                {
                    EmployeeAppraisalTeamMemberId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    EmployeeAppraisalDetailsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeAppraisalTeamMember", x => x.EmployeeAppraisalTeamMemberId);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeContractType",
                columns: table => new
                {
                    EmployeeContractTypeId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    EmployeeContractTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeContractType", x => x.EmployeeContractTypeId);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeEvaluation",
                columns: table => new
                {
                    EmployeeEvaluationId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CurrentAppraisalDate = table.Column<DateTime>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    FinalResultQues1 = table.Column<string>(nullable: true),
                    FinalResultQues2 = table.Column<string>(nullable: true),
                    FinalResultQues3 = table.Column<string>(nullable: true),
                    FinalResultQues4 = table.Column<string>(nullable: true),
                    FinalResultQues5 = table.Column<string>(nullable: true),
                    DirectSupervisor = table.Column<int>(nullable: false),
                    AppraisalTeamMember1 = table.Column<string>(nullable: true),
                    AppraisalTeamMember2 = table.Column<string>(nullable: true),
                    CommentsByEmployee = table.Column<string>(nullable: true),
                    EvaluationStatus = table.Column<string>(nullable: true),
                    EmployeeAppraisalDetailsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeEvaluation", x => x.EmployeeEvaluationId);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeEvaluationTraining",
                columns: table => new
                {
                    EmployeeEvaluationTrainingId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    TrainingProgram = table.Column<string>(nullable: true),
                    Program = table.Column<string>(nullable: true),
                    Participated = table.Column<string>(nullable: true),
                    CatchLevel = table.Column<string>(nullable: true),
                    RefresherTrm = table.Column<string>(nullable: true),
                    OthRecommendation = table.Column<string>(nullable: true),
                    EmployeeAppraisalDetailsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeEvaluationTraining", x => x.EmployeeEvaluationTrainingId);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeHealthQuestion",
                columns: table => new
                {
                    EmployeeHealthQuestionId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    Question = table.Column<string>(nullable: true),
                    Answer = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeHealthQuestion", x => x.EmployeeHealthQuestionId);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeType",
                columns: table => new
                {
                    EmployeeTypeId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    EmployeeTypeName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeType", x => x.EmployeeTypeId);
                });

            migrationBuilder.CreateTable(
                name: "ExchangeRateDetail",
                columns: table => new
                {
                    ExchangeRateId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    FromCurrency = table.Column<int>(nullable: false),
                    ToCurrency = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Rate = table.Column<decimal>(nullable: false),
                    OfficeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeRateDetail", x => x.ExchangeRateId);
                });

            migrationBuilder.CreateTable(
                name: "ExchangeRateVerifications",
                columns: table => new
                {
                    ExRateVerificationId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    IsVerified = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeRateVerifications", x => x.ExRateVerificationId);
                });

            migrationBuilder.CreateTable(
                name: "FinancialYearDetail",
                columns: table => new
                {
                    FinancialYearId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    FinancialYearName = table.Column<string>(maxLength: 50, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsDefault = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialYearDetail", x => x.FinancialYearId);
                });

            migrationBuilder.CreateTable(
                name: "GenderConsiderationDetail",
                columns: table => new
                {
                    GenderConsiderationId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    GenderConsiderationName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenderConsiderationDetail", x => x.GenderConsiderationId);
                });

            migrationBuilder.CreateTable(
                name: "InterviewTechnicalQuestions",
                columns: table => new
                {
                    InterviewTechnicalQuestionsId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Question = table.Column<string>(nullable: true),
                    OfficeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterviewTechnicalQuestions", x => x.InterviewTechnicalQuestionsId);
                });

            migrationBuilder.CreateTable(
                name: "InventoryItemType",
                columns: table => new
                {
                    ItemType = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    TypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryItemType", x => x.ItemType);
                });

            migrationBuilder.CreateTable(
                name: "JobGrade",
                columns: table => new
                {
                    GradeId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    GradeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobGrade", x => x.GradeId);
                });

            migrationBuilder.CreateTable(
                name: "JobPhases",
                columns: table => new
                {
                    JobPhaseId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Phase = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobPhases", x => x.JobPhaseId);
                });

            migrationBuilder.CreateTable(
                name: "JournalDetail",
                columns: table => new
                {
                    JournalCode = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    JournalName = table.Column<string>(maxLength: 100, nullable: true),
                    JournalType = table.Column<byte>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalDetail", x => x.JournalCode);
                });

            migrationBuilder.CreateTable(
                name: "LanguageDetail",
                columns: table => new
                {
                    LanguageId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LanguageName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageDetail", x => x.LanguageId);
                });

            migrationBuilder.CreateTable(
                name: "LeaveReasonDetail",
                columns: table => new
                {
                    LeaveReasonId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ReasonName = table.Column<string>(maxLength: 50, nullable: true),
                    Unit = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveReasonDetail", x => x.LeaveReasonId);
                });

            migrationBuilder.CreateTable(
                name: "LoggerDetails",
                columns: table => new
                {
                    LoggerDetailsId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    NotificationId = table.Column<int>(nullable: false),
                    IsRead = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    LoggedDetail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoggerDetails", x => x.LoggerDetailsId);
                });

            migrationBuilder.CreateTable(
                name: "MediaCategories",
                columns: table => new
                {
                    MediaCategoryId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CategoryName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaCategories", x => x.MediaCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Mediums",
                columns: table => new
                {
                    MediumId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    MediumName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mediums", x => x.MediumId);
                });

            migrationBuilder.CreateTable(
                name: "NationalityDetails",
                columns: table => new
                {
                    NationalityId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    NationalityName = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NationalityDetails", x => x.NationalityId);
                });

            migrationBuilder.CreateTable(
                name: "Natures",
                columns: table => new
                {
                    NatureId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    NatureName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Natures", x => x.NatureId);
                });

            migrationBuilder.CreateTable(
                name: "OfficeDetail",
                columns: table => new
                {
                    OfficeId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OfficeCode = table.Column<string>(maxLength: 5, nullable: true),
                    OfficeName = table.Column<string>(maxLength: 100, nullable: true),
                    SupervisorName = table.Column<string>(maxLength: 100, nullable: true),
                    PhoneNo = table.Column<string>(maxLength: 50, nullable: true),
                    FaxNo = table.Column<string>(maxLength: 50, nullable: true),
                    OfficeKey = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfficeDetail", x => x.OfficeId);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTypes",
                columns: table => new
                {
                    PaymentId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ChartOfAccountNewId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTypes", x => x.PaymentId);
                });

            migrationBuilder.CreateTable(
                name: "PayrollAccountHead",
                columns: table => new
                {
                    PayrollHeadId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PayrollHeadTypeId = table.Column<int>(nullable: false),
                    PayrollHeadName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    AccountNo = table.Column<long>(nullable: true),
                    TransactionTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayrollAccountHead", x => x.PayrollHeadId);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PermissionsInRoles",
                columns: table => new
                {
                    RoleId = table.Column<string>(nullable: false),
                    PermissionId = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PermissionsInRolesId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    IsGrant = table.Column<bool>(nullable: false),
                    CurrentPermissionId = table.Column<string>(nullable: true),
                    PageId = table.Column<int>(nullable: true),
                    ModuleId = table.Column<int>(nullable: false),
                    CanView = table.Column<bool>(nullable: false),
                    CanEdit = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionsInRoles", x => new { x.RoleId, x.PermissionId });
                    table.UniqueConstraint("AK_PermissionsInRoles_PermissionId_PermissionsInRolesId_RoleId", x => new { x.PermissionId, x.PermissionsInRolesId, x.RoleId });
                });

            migrationBuilder.CreateTable(
                name: "Producers",
                columns: table => new
                {
                    ProducerId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ProducerName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producers", x => x.ProducerId);
                });

            migrationBuilder.CreateTable(
                name: "ProfessionDetails",
                columns: table => new
                {
                    ProfessionId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ProfessionName = table.Column<string>(maxLength: 100, nullable: true),
                    ProfessionDari = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessionDetails", x => x.ProfessionId);
                });

            migrationBuilder.CreateTable(
                name: "ProgramDetail",
                columns: table => new
                {
                    ProgramId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ProjectId = table.Column<long>(nullable: false),
                    ProgramName = table.Column<string>(nullable: true),
                    ProgramCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramDetail", x => x.ProgramId);
                });

            migrationBuilder.CreateTable(
                name: "ProjectIndicators",
                columns: table => new
                {
                    ProjectIndicatorId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IndicatorName = table.Column<string>(nullable: true),
                    IndicatorCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectIndicators", x => x.ProjectIndicatorId);
                });

            migrationBuilder.CreateTable(
                name: "ProjectMonitoringReviewDetail",
                columns: table => new
                {
                    ProjectMonitoringReviewId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PostivePoints = table.Column<string>(nullable: true),
                    NegativePoints = table.Column<string>(nullable: true),
                    Recommendations = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    ProjectId = table.Column<long>(nullable: false),
                    ActivityId = table.Column<long>(nullable: false),
                    MonitoringDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectMonitoringReviewDetail", x => x.ProjectMonitoringReviewId);
                });

            migrationBuilder.CreateTable(
                name: "ProjectPhaseDetails",
                columns: table => new
                {
                    ProjectPhaseDetailsId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ProjectPhase = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectPhaseDetails", x => x.ProjectPhaseDetailsId);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseUnitType",
                columns: table => new
                {
                    UnitTypeId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UnitTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseUnitType", x => x.UnitTypeId);
                });

            migrationBuilder.CreateTable(
                name: "QualificationDetails",
                columns: table => new
                {
                    QualificationId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    QualificationName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QualificationDetails", x => x.QualificationId);
                });

            migrationBuilder.CreateTable(
                name: "Qualities",
                columns: table => new
                {
                    QualityId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    QualityName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qualities", x => x.QualityId);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptType",
                columns: table => new
                {
                    ReceiptTypeId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ReceiptTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptType", x => x.ReceiptTypeId);
                });

            migrationBuilder.CreateTable(
                name: "SalaryHeadDetails",
                columns: table => new
                {
                    SalaryHeadId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    HeadTypeId = table.Column<int>(nullable: false),
                    HeadName = table.Column<string>(maxLength: 50, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    AccountNo = table.Column<long>(nullable: true),
                    TransactionTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryHeadDetails", x => x.SalaryHeadId);
                });

            migrationBuilder.CreateTable(
                name: "SalaryTaxReportContent",
                columns: table => new
                {
                    SalaryTaxReportContentId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    EmployerAuthorizedOfficerName = table.Column<string>(nullable: true),
                    PositionAuthorizedOfficer = table.Column<string>(nullable: true),
                    OfficeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryTaxReportContent", x => x.SalaryTaxReportContentId);
                });

            migrationBuilder.CreateTable(
                name: "SectorDetails",
                columns: table => new
                {
                    SectorId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    SectorName = table.Column<string>(nullable: true),
                    SectorCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectorDetails", x => x.SectorId);
                });

            migrationBuilder.CreateTable(
                name: "SecurityConsiderationDetail",
                columns: table => new
                {
                    SecurityConsiderationId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    SecurityConsiderationName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityConsiderationDetail", x => x.SecurityConsiderationId);
                });

            migrationBuilder.CreateTable(
                name: "SecurityDetail",
                columns: table => new
                {
                    SecurityId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    SecurityName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityDetail", x => x.SecurityId);
                });

            migrationBuilder.CreateTable(
                name: "StatusAtTimeOfIssue",
                columns: table => new
                {
                    StatusAtTimeOfIssueId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    StatusName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusAtTimeOfIssue", x => x.StatusAtTimeOfIssueId);
                });

            migrationBuilder.CreateTable(
                name: "StrengthConsiderationDetail",
                columns: table => new
                {
                    StrengthConsiderationId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    StrengthConsiderationName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StrengthConsiderationDetail", x => x.StrengthConsiderationId);
                });

            migrationBuilder.CreateTable(
                name: "StrongandWeakPoints",
                columns: table => new
                {
                    StrongPointsId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CurrentAppraisalDate = table.Column<DateTime>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    EmployeeAppraisalDetailsId = table.Column<int>(nullable: false),
                    Point = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StrongandWeakPoints", x => x.StrongPointsId);
                });

            migrationBuilder.CreateTable(
                name: "TargetBeneficiaryDetail",
                columns: table => new
                {
                    TargetId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ProjectId = table.Column<long>(nullable: false),
                    TargetType = table.Column<int>(nullable: false),
                    TargetName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TargetBeneficiaryDetail", x => x.TargetId);
                });

            migrationBuilder.CreateTable(
                name: "TechnicalQuestion",
                columns: table => new
                {
                    TechnicalQuestionId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Question = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicalQuestion", x => x.TechnicalQuestionId);
                });

            migrationBuilder.CreateTable(
                name: "TimeCategories",
                columns: table => new
                {
                    TimeCategoryId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    TimeCategoryName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeCategories", x => x.TimeCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "UserDetailOffices",
                columns: table => new
                {
                    UserOfficesId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OfficeId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDetailOffices", x => x.UserOfficesId);
                });

            migrationBuilder.CreateTable(
                name: "UserDetails",
                columns: table => new
                {
                    UserID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Username = table.Column<string>(maxLength: 50, nullable: true),
                    Password = table.Column<string>(maxLength: 50, nullable: true),
                    FirstName = table.Column<string>(maxLength: 50, nullable: true),
                    LastName = table.Column<string>(maxLength: 50, nullable: true),
                    UserType = table.Column<byte>(nullable: true),
                    OfficeCode = table.Column<string>(maxLength: 10, nullable: true),
                    DepartmentId = table.Column<int>(nullable: false),
                    Status = table.Column<byte>(nullable: true),
                    AspNetUserId = table.Column<string>(nullable: true),
                    OfficeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDetails", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "VoucherType",
                columns: table => new
                {
                    VoucherTypeId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    VoucherTypeName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoucherType", x => x.VoucherTypeId);
                });

            migrationBuilder.CreateTable(
                name: "AccountType",
                columns: table => new
                {
                    AccountTypeId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    AccountTypeName = table.Column<string>(maxLength: 100, nullable: true),
                    AccountCategory = table.Column<int>(nullable: true),
                    AccountHeadTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountType", x => x.AccountTypeId);
                    table.ForeignKey(
                        name: "FK_AccountType_AccountHeadType_AccountHeadTypeId",
                        column: x => x.AccountHeadTypeId,
                        principalTable: "AccountHeadType",
                        principalColumn: "AccountHeadTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AgreeDisagreePermission",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PageId = table.Column<int>(nullable: false),
                    Agree = table.Column<bool>(nullable: false),
                    Disagree = table.Column<bool>(nullable: false),
                    RoleId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgreeDisagreePermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgreeDisagreePermission_ApplicationPages_PageId",
                        column: x => x.PageId,
                        principalTable: "ApplicationPages",
                        principalColumn: "PageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApproveRejectPermission",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PageId = table.Column<int>(nullable: false),
                    Approve = table.Column<bool>(nullable: false),
                    Reject = table.Column<bool>(nullable: false),
                    RoleId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApproveRejectPermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApproveRejectPermission_ApplicationPages_PageId",
                        column: x => x.PageId,
                        principalTable: "ApplicationPages",
                        principalColumn: "PageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderSchedulePermission",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PageId = table.Column<int>(nullable: false),
                    OrderSchedule = table.Column<bool>(nullable: false),
                    RoleId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderSchedulePermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderSchedulePermission_ApplicationPages_PageId",
                        column: x => x.PageId,
                        principalTable: "ApplicationPages",
                        principalColumn: "PageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissions",
                columns: table => new
                {
                    RolesPermissionId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    RoleId = table.Column<string>(nullable: true),
                    IsGrant = table.Column<bool>(nullable: false),
                    CurrentPermissionId = table.Column<string>(nullable: true),
                    PageId = table.Column<int>(nullable: true),
                    ModuleId = table.Column<int>(nullable: false),
                    CanView = table.Column<bool>(nullable: false),
                    CanEdit = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissions", x => x.RolesPermissionId);
                    table.ForeignKey(
                        name: "FK_RolePermissions_ApplicationPages_PageId",
                        column: x => x.PageId,
                        principalTable: "ApplicationPages",
                        principalColumn: "PageId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeAppraisalQuestions",
                columns: table => new
                {
                    EmployeeAppraisalQuestionsId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    AppraisalGeneralQuestionsId = table.Column<int>(nullable: false),
                    Score = table.Column<int>(nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    CurrentAppraisalDate = table.Column<DateTime>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeAppraisalQuestions", x => x.EmployeeAppraisalQuestionsId);
                    table.ForeignKey(
                        name: "FK_EmployeeAppraisalQuestions_AppraisalGeneralQuestions_Apprai~",
                        column: x => x.AppraisalGeneralQuestionsId,
                        principalTable: "AppraisalGeneralQuestions",
                        principalColumn: "AppraisalGeneralQuestionsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientDetails",
                columns: table => new
                {
                    ClientId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ClientCode = table.Column<string>(nullable: true),
                    ClientName = table.Column<string>(nullable: true),
                    FocalPoint = table.Column<string>(nullable: true),
                    Position = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PhysicialAddress = table.Column<string>(nullable: true),
                    History = table.Column<string>(nullable: true),
                    ClientBackground = table.Column<string>(nullable: true),
                    CategoryId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientDetails", x => x.ClientId);
                    table.ForeignKey(
                        name: "FK_ClientDetails_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StoreSourceCodeDetail",
                columns: table => new
                {
                    SourceCodeId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CodeTypeId = table.Column<int>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 100, nullable: true),
                    Address = table.Column<string>(maxLength: 200, nullable: true),
                    Phone = table.Column<string>(maxLength: 50, nullable: true),
                    Fax = table.Column<string>(maxLength: 50, nullable: true),
                    EmailAddress = table.Column<string>(maxLength: 100, nullable: true),
                    Guarantor = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreSourceCodeDetail", x => x.SourceCodeId);
                    table.ForeignKey(
                        name: "FK_StoreSourceCodeDetail_CodeType_CodeTypeId",
                        column: x => x.CodeTypeId,
                        principalTable: "CodeType",
                        principalColumn: "CodeTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProvinceDetails",
                columns: table => new
                {
                    ProvinceId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ProvinceName = table.Column<string>(maxLength: 50, nullable: true),
                    CountryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProvinceDetails", x => x.ProvinceId);
                    table.ForeignKey(
                        name: "FK_ProvinceDetails_CountryDetails_CountryId",
                        column: x => x.CountryId,
                        principalTable: "CountryDetails",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EntitySourceDocumentDetails",
                columns: table => new
                {
                    EntitySourceDocumentId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    EntityId = table.Column<long>(nullable: false),
                    DocumentFileId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntitySourceDocumentDetails", x => x.EntitySourceDocumentId);
                    table.ForeignKey(
                        name: "FK_EntitySourceDocumentDetails_DocumentFileDetail_DocumentFile~",
                        column: x => x.DocumentFileId,
                        principalTable: "DocumentFileDetail",
                        principalColumn: "DocumentFileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmailSettingDetail",
                columns: table => new
                {
                    EmailId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    SenderEmail = table.Column<string>(maxLength: 100, nullable: true),
                    EmailTypeId = table.Column<int>(nullable: true),
                    SenderPassword = table.Column<string>(maxLength: 100, nullable: true),
                    SmtpPort = table.Column<int>(nullable: false),
                    SmtpServer = table.Column<string>(maxLength: 100, nullable: true),
                    EnableSSL = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailSettingDetail", x => x.EmailId);
                    table.ForeignKey(
                        name: "FK_EmailSettingDetail_EmailType_EmailTypeId",
                        column: x => x.EmailTypeId,
                        principalTable: "EmailType",
                        principalColumn: "EmailTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ContractTypeContent",
                columns: table => new
                {
                    ContractTypeContentId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    EmployeeContractTypeId = table.Column<int>(nullable: false),
                    ContentEnglish = table.Column<string>(nullable: true),
                    ContentDari = table.Column<string>(nullable: true),
                    OfficeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractTypeContent", x => x.ContractTypeContentId);
                    table.ForeignKey(
                        name: "FK_ContractTypeContent_EmployeeContractType_EmployeeContractTy~",
                        column: x => x.EmployeeContractTypeId,
                        principalTable: "EmployeeContractType",
                        principalColumn: "EmployeeContractTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeePensionRate",
                columns: table => new
                {
                    EmployeePensionRateId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    FinancialYearId = table.Column<int>(nullable: false),
                    PensionRate = table.Column<double>(nullable: true),
                    IsDefault = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePensionRate", x => x.EmployeePensionRateId);
                    table.ForeignKey(
                        name: "FK_EmployeePensionRate_FinancialYearDetail_FinancialYearId",
                        column: x => x.FinancialYearId,
                        principalTable: "FinancialYearDetail",
                        principalColumn: "FinancialYearId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeLanguages",
                columns: table => new
                {
                    SpeakLanguageId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LanguageId = table.Column<int>(nullable: false),
                    Reading = table.Column<int>(nullable: false),
                    Writing = table.Column<int>(nullable: false),
                    Speaking = table.Column<int>(nullable: false),
                    Listening = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeLanguages", x => x.SpeakLanguageId);
                    table.ForeignKey(
                        name: "FK_EmployeeLanguages_LanguageDetail_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "LanguageDetail",
                        principalColumn: "LanguageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Channel",
                columns: table => new
                {
                    ChannelId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ChannelName = table.Column<string>(nullable: true),
                    MediumId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Channel", x => x.ChannelId);
                    table.ForeignKey(
                        name: "FK_Channel_Mediums_MediumId",
                        column: x => x.MediumId,
                        principalTable: "Mediums",
                        principalColumn: "MediumId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DepartmentName = table.Column<string>(nullable: true),
                    OfficeCode = table.Column<string>(nullable: true),
                    OfficeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.DepartmentId);
                    table.ForeignKey(
                        name: "FK_Department_OfficeDetail_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "OfficeDetail",
                        principalColumn: "OfficeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExchangeRate",
                columns: table => new
                {
                    ExchangeRateId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Date = table.Column<DateTime>(nullable: true),
                    FromCurrency = table.Column<int>(nullable: true),
                    ToCurrency = table.Column<int>(nullable: true),
                    Rate = table.Column<double>(nullable: true),
                    CurrencyCode = table.Column<string>(nullable: true),
                    OfficeId = table.Column<int>(nullable: true),
                    OfficeCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeRate", x => x.ExchangeRateId);
                    table.ForeignKey(
                        name: "FK_ExchangeRate_CurrencyDetails_FromCurrency",
                        column: x => x.FromCurrency,
                        principalTable: "CurrencyDetails",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExchangeRate_OfficeDetail_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "OfficeDetail",
                        principalColumn: "OfficeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExchangeRate_CurrencyDetails_ToCurrency",
                        column: x => x.ToCurrency,
                        principalTable: "CurrencyDetails",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HolidayDetails",
                columns: table => new
                {
                    HolidayId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    HolidayName = table.Column<string>(maxLength: 50, nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    FinancialYearId = table.Column<int>(nullable: true),
                    OfficeId = table.Column<int>(nullable: true),
                    HolidayType = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolidayDetails", x => x.HolidayId);
                    table.ForeignKey(
                        name: "FK_HolidayDetails_FinancialYearDetail_FinancialYearId",
                        column: x => x.FinancialYearId,
                        principalTable: "FinancialYearDetail",
                        principalColumn: "FinancialYearId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HolidayDetails_OfficeDetail_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "OfficeDetail",
                        principalColumn: "OfficeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HolidayWeeklyDetails",
                columns: table => new
                {
                    HolidayWeeklyId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Day = table.Column<string>(maxLength: 30, nullable: true),
                    OfficeId = table.Column<int>(nullable: false),
                    FinancialYearId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolidayWeeklyDetails", x => x.HolidayWeeklyId);
                    table.ForeignKey(
                        name: "FK_HolidayWeeklyDetails_FinancialYearDetail_FinancialYearId",
                        column: x => x.FinancialYearId,
                        principalTable: "FinancialYearDetail",
                        principalColumn: "FinancialYearId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HolidayWeeklyDetails_OfficeDetail_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "OfficeDetail",
                        principalColumn: "OfficeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemSpecificationMaster",
                columns: table => new
                {
                    ItemSpecificationMasterId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemSpecificationField = table.Column<string>(nullable: true),
                    OfficeId = table.Column<int>(nullable: false),
                    ItemTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemSpecificationMaster", x => x.ItemSpecificationMasterId);
                    table.ForeignKey(
                        name: "FK_ItemSpecificationMaster_InventoryItemType_ItemTypeId",
                        column: x => x.ItemTypeId,
                        principalTable: "InventoryItemType",
                        principalColumn: "ItemType",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemSpecificationMaster_OfficeDetail_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "OfficeDetail",
                        principalColumn: "OfficeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PayrollMonthlyHourDetail",
                columns: table => new
                {
                    PayrollMonthlyHourID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OfficeId = table.Column<int>(nullable: false),
                    PayrollMonth = table.Column<int>(nullable: true),
                    PayrollYear = table.Column<int>(nullable: true),
                    Hours = table.Column<int>(nullable: true),
                    InTime = table.Column<DateTime>(nullable: true),
                    OutTime = table.Column<DateTime>(nullable: true),
                    WorkingTime = table.Column<int>(nullable: true),
                    WorkingDay = table.Column<int>(nullable: true),
                    AttendanceGroupId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayrollMonthlyHourDetail", x => x.PayrollMonthlyHourID);
                    table.ForeignKey(
                        name: "FK_PayrollMonthlyHourDetail_AttendanceGroupMaster_AttendanceGr~",
                        column: x => x.AttendanceGroupId,
                        principalTable: "AttendanceGroupMaster",
                        principalColumn: "AttendanceGroupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PayrollMonthlyHourDetail_OfficeDetail_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "OfficeDetail",
                        principalColumn: "OfficeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PolicyDetails",
                columns: table => new
                {
                    PolicyId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PolicyName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    PolicyCode = table.Column<string>(nullable: true),
                    LanguageId = table.Column<long>(nullable: true),
                    LanguagesLanguageId = table.Column<int>(nullable: true),
                    MediumId = table.Column<long>(nullable: true),
                    MediaCategoryId = table.Column<long>(nullable: true),
                    ProducerId = table.Column<long>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    RepeatDays = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyDetails", x => x.PolicyId);
                    table.ForeignKey(
                        name: "FK_PolicyDetails_LanguageDetail_LanguagesLanguageId",
                        column: x => x.LanguagesLanguageId,
                        principalTable: "LanguageDetail",
                        principalColumn: "LanguageId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PolicyDetails_MediaCategories_MediaCategoryId",
                        column: x => x.MediaCategoryId,
                        principalTable: "MediaCategories",
                        principalColumn: "MediaCategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PolicyDetails_Mediums_MediumId",
                        column: x => x.MediumId,
                        principalTable: "Mediums",
                        principalColumn: "MediumId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PolicyDetails_Producers_ProducerId",
                        column: x => x.ProducerId,
                        principalTable: "Producers",
                        principalColumn: "ProducerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectIndicatorQuestions",
                columns: table => new
                {
                    IndicatorQuestionId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IndicatorQuestion = table.Column<string>(nullable: true),
                    ProjectIndicatorId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectIndicatorQuestions", x => x.IndicatorQuestionId);
                    table.ForeignKey(
                        name: "FK_ProjectIndicatorQuestions_ProjectIndicators_ProjectIndicato~",
                        column: x => x.ProjectIndicatorId,
                        principalTable: "ProjectIndicators",
                        principalColumn: "ProjectIndicatorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectMonitoringIndicatorDetail",
                columns: table => new
                {
                    MonitoringIndicatorId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ProjectIndicatorId = table.Column<long>(nullable: false),
                    ProjectMonitoringReviewId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectMonitoringIndicatorDetail", x => x.MonitoringIndicatorId);
                    table.ForeignKey(
                        name: "FK_ProjectMonitoringIndicatorDetail_ProjectIndicators_ProjectI~",
                        column: x => x.ProjectIndicatorId,
                        principalTable: "ProjectIndicators",
                        principalColumn: "ProjectIndicatorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectMonitoringIndicatorDetail_ProjectMonitoringReviewDet~",
                        column: x => x.ProjectMonitoringReviewId,
                        principalTable: "ProjectMonitoringReviewDetail",
                        principalColumn: "ProjectMonitoringReviewId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectDetail",
                columns: table => new
                {
                    ProjectId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ProjectCode = table.Column<string>(nullable: true),
                    ProjectName = table.Column<string>(nullable: true),
                    ProjectDescription = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    ProjectPhaseDetailsId = table.Column<long>(nullable: true),
                    IsProposalComplate = table.Column<bool>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsCriteriaEvaluationSubmit = table.Column<bool>(nullable: true),
                    ReviewerId = table.Column<int>(nullable: true),
                    DirectorId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectDetail", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_ProjectDetail_ProjectPhaseDetails_ProjectPhaseDetailsId",
                        column: x => x.ProjectPhaseDetailsId,
                        principalTable: "ProjectPhaseDetails",
                        principalColumn: "ProjectPhaseDetailsId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UnitRates",
                columns: table => new
                {
                    UnitRateId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UnitRates = table.Column<double>(nullable: false),
                    CurrencyId = table.Column<long>(nullable: true),
                    CurrencyDetailsCurrencyId = table.Column<int>(nullable: true),
                    MediumId = table.Column<long>(nullable: true),
                    TimeCategoryId = table.Column<long>(nullable: true),
                    NatureId = table.Column<long>(nullable: true),
                    QualityId = table.Column<long>(nullable: true),
                    ActivityTypeId = table.Column<long>(nullable: true),
                    MediaCategoryId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitRates", x => x.UnitRateId);
                    table.ForeignKey(
                        name: "FK_UnitRates_ActivityTypes_ActivityTypeId",
                        column: x => x.ActivityTypeId,
                        principalTable: "ActivityTypes",
                        principalColumn: "ActivityTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UnitRates_CurrencyDetails_CurrencyDetailsCurrencyId",
                        column: x => x.CurrencyDetailsCurrencyId,
                        principalTable: "CurrencyDetails",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UnitRates_MediaCategories_MediaCategoryId",
                        column: x => x.MediaCategoryId,
                        principalTable: "MediaCategories",
                        principalColumn: "MediaCategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UnitRates_Mediums_MediumId",
                        column: x => x.MediumId,
                        principalTable: "Mediums",
                        principalColumn: "MediumId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UnitRates_Natures_NatureId",
                        column: x => x.NatureId,
                        principalTable: "Natures",
                        principalColumn: "NatureId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UnitRates_Qualities_QualityId",
                        column: x => x.QualityId,
                        principalTable: "Qualities",
                        principalColumn: "QualityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UnitRates_TimeCategories_TimeCategoryId",
                        column: x => x.TimeCategoryId,
                        principalTable: "TimeCategories",
                        principalColumn: "TimeCategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChartOfAccountNew",
                columns: table => new
                {
                    ChartOfAccountNewId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ChartOfAccountNewCode = table.Column<string>(nullable: true),
                    IsCreditBalancetype = table.Column<bool>(nullable: true),
                    AccountName = table.Column<string>(maxLength: 100, nullable: true),
                    ParentID = table.Column<long>(nullable: false),
                    AccountLevelId = table.Column<int>(nullable: false),
                    AccountHeadTypeId = table.Column<int>(nullable: false),
                    AccountTypeId = table.Column<int>(nullable: true),
                    AccountFilterTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChartOfAccountNew", x => x.ChartOfAccountNewId);
                    table.ForeignKey(
                        name: "FK_ChartOfAccountNew_AccountFilterType_AccountFilterTypeId",
                        column: x => x.AccountFilterTypeId,
                        principalTable: "AccountFilterType",
                        principalColumn: "AccountFilterTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChartOfAccountNew_AccountLevel_AccountLevelId",
                        column: x => x.AccountLevelId,
                        principalTable: "AccountLevel",
                        principalColumn: "AccountLevelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChartOfAccountNew_AccountType_AccountTypeId",
                        column: x => x.AccountTypeId,
                        principalTable: "AccountType",
                        principalColumn: "AccountTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeDetail",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    EmployeeCode = table.Column<string>(nullable: true),
                    RegCode = table.Column<string>(nullable: true),
                    EmployeeName = table.Column<string>(nullable: true),
                    IDCard = table.Column<string>(nullable: true),
                    FatherName = table.Column<string>(nullable: true),
                    PermanentAddress = table.Column<string>(nullable: true),
                    CurrentAddress = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    District = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Fax = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    ReferBy = table.Column<string>(nullable: true),
                    Passport = table.Column<string>(nullable: true),
                    Language = table.Column<string>(nullable: true),
                    DateOfBirth = table.Column<DateTime>(nullable: true),
                    PlaceOfBirth = table.Column<string>(nullable: true),
                    PreviousWork = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    EmployeePhoto = table.Column<string>(nullable: true),
                    Sex = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Province = table.Column<string>(nullable: true),
                    Qualification = table.Column<string>(nullable: true),
                    Profession = table.Column<string>(nullable: true),
                    Experience = table.Column<string>(nullable: true),
                    Nationality = table.Column<string>(nullable: true),
                    SpeakLanguageList = table.Column<string>(nullable: true),
                    CloseRelativeList = table.Column<string>(nullable: true),
                    RefereeList = table.Column<string>(nullable: true),
                    EducationList = table.Column<string>(nullable: true),
                    NationalEmploymentList = table.Column<string>(nullable: true),
                    InternationalEmploymentList = table.Column<string>(nullable: true),
                    OtherSkillList = table.Column<string>(nullable: true),
                    MaritalStatus = table.Column<string>(nullable: true),
                    ProjectId = table.Column<long>(nullable: true),
                    CountryId = table.Column<int>(nullable: true),
                    GradeId = table.Column<int>(nullable: true),
                    NationalityId = table.Column<int>(nullable: true),
                    HigherQualificationId = table.Column<int>(nullable: true),
                    Extension = table.Column<string>(nullable: true),
                    DocumentGUID = table.Column<string>(nullable: true),
                    DocumentType = table.Column<int>(nullable: true),
                    SexId = table.Column<int>(nullable: true),
                    EmployeeTypeId = table.Column<int>(nullable: true),
                    Age = table.Column<int>(nullable: true),
                    ExperienceYear = table.Column<int>(nullable: true),
                    ExperienceMonth = table.Column<int>(nullable: true),
                    PassportNo = table.Column<string>(nullable: true),
                    University = table.Column<string>(nullable: true),
                    BirthPlace = table.Column<string>(nullable: true),
                    IssuePlace = table.Column<string>(nullable: true),
                    ProvinceId = table.Column<int>(nullable: true),
                    Resume = table.Column<string>(nullable: true),
                    MaritalStatusId = table.Column<int>(nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "ChatDetail",
                columns: table => new
                {
                    ChatId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ChatSourceEntityId = table.Column<int>(nullable: false),
                    EntityId = table.Column<int>(nullable: false),
                    Message = table.Column<string>(nullable: true),
                    EntitySourceDocumentId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatDetail", x => x.ChatId);
                    table.ForeignKey(
                        name: "FK_ChatDetail_EntitySourceDocumentDetails_EntitySourceDocument~",
                        column: x => x.EntitySourceDocumentId,
                        principalTable: "EntitySourceDocumentDetails",
                        principalColumn: "EntitySourceDocumentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemSpecificationDetails",
                columns: table => new
                {
                    ItemSpecificationDetailsId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemSpecificationMasterId = table.Column<int>(nullable: false),
                    ItemId = table.Column<string>(nullable: true),
                    ItemSpecificationValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemSpecificationDetails", x => x.ItemSpecificationDetailsId);
                    table.ForeignKey(
                        name: "FK_ItemSpecificationDetails_ItemSpecificationMaster_ItemSpecif~",
                        column: x => x.ItemSpecificationMasterId,
                        principalTable: "ItemSpecificationMaster",
                        principalColumn: "ItemSpecificationMasterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PolicyDaySchedules",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PolicyId = table.Column<long>(nullable: true),
                    Monday = table.Column<bool>(nullable: false),
                    Tuesday = table.Column<bool>(nullable: false),
                    Wednesday = table.Column<bool>(nullable: false),
                    Thursday = table.Column<bool>(nullable: false),
                    Friday = table.Column<bool>(nullable: false),
                    Saturday = table.Column<bool>(nullable: false),
                    Sunday = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyDaySchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PolicyDaySchedules_PolicyDetails_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "PolicyDetails",
                        principalColumn: "PolicyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PolicyOrderSchedules",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PolicyId = table.Column<long>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    RequestSchedule = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyOrderSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PolicyOrderSchedules_PolicyDetails_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "PolicyDetails",
                        principalColumn: "PolicyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PolicySchedules",
                columns: table => new
                {
                    PolicyScheduleId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PolicyId = table.Column<long>(nullable: true),
                    ScheduleCode = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Frequency = table.Column<int>(nullable: true),
                    ByMonth = table.Column<int>(nullable: true),
                    ByWeek = table.Column<int>(nullable: true),
                    ByDay = table.Column<int>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    StartTime = table.Column<TimeSpan>(nullable: false),
                    EndTime = table.Column<TimeSpan>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    isActive = table.Column<bool>(nullable: false),
                    isDeleted = table.Column<bool>(nullable: false),
                    RepeatDays = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicySchedules", x => x.PolicyScheduleId);
                    table.ForeignKey(
                        name: "FK_PolicySchedules_PolicyDetails_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "PolicyDetails",
                        principalColumn: "PolicyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PolicyTimeSchedules",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PolicyId = table.Column<long>(nullable: true),
                    TimeScheduleCode = table.Column<string>(nullable: true),
                    StartTime = table.Column<TimeSpan>(nullable: false),
                    EndTime = table.Column<TimeSpan>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyTimeSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PolicyTimeSchedules_PolicyDetails_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "PolicyDetails",
                        principalColumn: "PolicyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectMonitoringIndicatorQuestions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    QuestionId = table.Column<long>(nullable: false),
                    VerificationId = table.Column<int>(nullable: true),
                    Verification = table.Column<string>(nullable: true),
                    Score = table.Column<int>(nullable: true),
                    MonitoringIndicatorId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectMonitoringIndicatorQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectMonitoringIndicatorQuestions_ProjectMonitoringIndica~",
                        column: x => x.MonitoringIndicatorId,
                        principalTable: "ProjectMonitoringIndicatorDetail",
                        principalColumn: "MonitoringIndicatorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectMonitoringIndicatorQuestions_ProjectIndicatorQuestio~",
                        column: x => x.QuestionId,
                        principalTable: "ProjectIndicatorQuestions",
                        principalColumn: "IndicatorQuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApproveProjectDetails",
                columns: table => new
                {
                    ApproveProjrctId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ProjectId = table.Column<long>(nullable: false),
                    CommentText = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    FilePath = table.Column<string>(nullable: true),
                    IsApproved = table.Column<bool>(nullable: true),
                    UploadedFile = table.Column<byte[]>(nullable: true),
                    ReviewCompletionDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApproveProjectDetails", x => x.ApproveProjrctId);
                    table.ForeignKey(
                        name: "FK_ApproveProjectDetails_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CEAgeGroupDetail",
                columns: table => new
                {
                    AgeGroupOtherDetailId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ProjectId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CEAgeGroupDetail", x => x.AgeGroupOtherDetailId);
                    table.ForeignKey(
                        name: "FK_CEAgeGroupDetail_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CEAssumptionDetail",
                columns: table => new
                {
                    AssumptionDetailId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ProjectId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CEAssumptionDetail", x => x.AssumptionDetailId);
                    table.ForeignKey(
                        name: "FK_CEAssumptionDetail_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CEFeasibilityExpertOtherDetail",
                columns: table => new
                {
                    ExpertOtherDetailId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ProjectId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CEFeasibilityExpertOtherDetail", x => x.ExpertOtherDetailId);
                    table.ForeignKey(
                        name: "FK_CEFeasibilityExpertOtherDetail_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CountryMultiSelectDetails",
                columns: table => new
                {
                    CountryMultiSelectId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ProjectId = table.Column<long>(nullable: false),
                    CountryId = table.Column<int>(nullable: true),
                    CountrySelectionId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryMultiSelectDetails", x => x.CountryMultiSelectId);
                    table.ForeignKey(
                        name: "FK_CountryMultiSelectDetails_CountryDetails_CountryId",
                        column: x => x.CountryId,
                        principalTable: "CountryDetails",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CountryMultiSelectDetails_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DistrictMultiSelect",
                columns: table => new
                {
                    DistrictMultiSelectId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ProjectId = table.Column<long>(nullable: false),
                    DistrictID = table.Column<long>(nullable: false),
                    DistrictSelectionId = table.Column<long>(nullable: true),
                    ProvinceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistrictMultiSelect", x => x.DistrictMultiSelectId);
                    table.ForeignKey(
                        name: "FK_DistrictMultiSelect_DistrictDetail_DistrictID",
                        column: x => x.DistrictID,
                        principalTable: "DistrictDetail",
                        principalColumn: "DistrictID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DistrictMultiSelect_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DistrictMultiSelect_ProvinceDetails_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "ProvinceDetails",
                        principalColumn: "ProvinceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DonorCriteriaDetail",
                columns: table => new
                {
                    DonorCEId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ProjectId = table.Column<long>(nullable: false),
                    MethodOfFunding = table.Column<int>(nullable: true),
                    PastFundingExperience = table.Column<bool>(nullable: true),
                    ProposalAccepted = table.Column<bool>(nullable: true),
                    ProposalExperience = table.Column<bool>(nullable: true),
                    Professional = table.Column<bool>(nullable: true),
                    FundsOnTime = table.Column<bool>(nullable: true),
                    EffectiveCommunication = table.Column<bool>(nullable: true),
                    Dispute = table.Column<bool>(nullable: true),
                    OtherDeliverable = table.Column<bool>(nullable: true),
                    OtherDeliverableType = table.Column<string>(nullable: true),
                    PastWorkingExperience = table.Column<bool>(nullable: true),
                    CriticismPerformance = table.Column<bool>(nullable: true),
                    TimeManagement = table.Column<bool>(nullable: true),
                    MoneyAllocation = table.Column<bool>(nullable: true),
                    Accountability = table.Column<bool>(nullable: true),
                    DeliverableQuality = table.Column<bool>(nullable: true),
                    DonorFinancingHistory = table.Column<int>(nullable: true),
                    ReligiousStanding = table.Column<int>(nullable: true),
                    PoliticalStanding = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonorCriteriaDetail", x => x.DonorCEId);
                    table.ForeignKey(
                        name: "FK_DonorCriteriaDetail_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DonorEligibilityCriteria",
                columns: table => new
                {
                    DonorEligibilityDetailId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ProjectId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonorEligibilityCriteria", x => x.DonorEligibilityDetailId);
                    table.ForeignKey(
                        name: "FK_DonorEligibilityCriteria_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EligibilityCriteriaDetail",
                columns: table => new
                {
                    EligibilityId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ProjectId = table.Column<long>(nullable: false),
                    DonorCriteriaMet = table.Column<bool>(nullable: true),
                    EligibilityDealine = table.Column<bool>(nullable: true),
                    CoPartnership = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EligibilityCriteriaDetail", x => x.EligibilityId);
                    table.ForeignKey(
                        name: "FK_EligibilityCriteriaDetail_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FeasibilityCriteriaDetail",
                columns: table => new
                {
                    FeasibilityId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ProjectId = table.Column<long>(nullable: false),
                    CapacityAvailableForProject = table.Column<bool>(nullable: true),
                    TrainedStaff = table.Column<bool>(nullable: true),
                    ByEquipment = table.Column<bool>(nullable: true),
                    ExpandScope = table.Column<bool>(nullable: true),
                    GeoGraphicalPresence = table.Column<bool>(nullable: true),
                    ThirdPartyContract = table.Column<bool>(nullable: true),
                    CostOfCompensationMonth = table.Column<int>(nullable: true),
                    CostOfCompensationMoney = table.Column<double>(nullable: true),
                    AnyInKindComponent = table.Column<bool>(nullable: true),
                    UseableByOrganisation = table.Column<bool>(nullable: true),
                    FeasibleExpertDeploy = table.Column<bool>(nullable: true),
                    FeasibilityExpert = table.Column<bool>(nullable: true),
                    EnoughTimeForProject = table.Column<bool>(nullable: true),
                    ProjectAllowedBylaw = table.Column<bool>(nullable: true),
                    ProjectByLeadership = table.Column<bool>(nullable: true),
                    IsProjectPractical = table.Column<bool>(nullable: true),
                    PresenceCoverageInProject = table.Column<bool>(nullable: true),
                    ProjectInLineWithOrgFocus = table.Column<bool>(nullable: true),
                    EnoughTimeToPrepareProposal = table.Column<bool>(nullable: true),
                    ProjectRealCost = table.Column<double>(nullable: true),
                    IsCostGreaterthenBudget = table.Column<bool>(nullable: true),
                    PerCostGreaterthenBudget = table.Column<int>(nullable: true),
                    IsFinancialContribution = table.Column<bool>(nullable: true),
                    IsSecurity = table.Column<bool>(nullable: true),
                    IsGeographical = table.Column<bool>(nullable: true),
                    IsSeasonal = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeasibilityCriteriaDetail", x => x.FeasibilityId);
                    table.ForeignKey(
                        name: "FK_FeasibilityCriteriaDetail_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FinancialCriteriaDetail",
                columns: table => new
                {
                    FinancialCriteriaDetailId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ProjectId = table.Column<long>(nullable: false),
                    Total = table.Column<double>(nullable: true),
                    ProjectActivities = table.Column<double>(nullable: true),
                    Operational = table.Column<double>(nullable: true),
                    Overhead_Admin = table.Column<double>(nullable: true),
                    Lump_Sum = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialCriteriaDetail", x => x.FinancialCriteriaDetailId);
                    table.ForeignKey(
                        name: "FK_FinancialCriteriaDetail_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FinancialProjectDetail",
                columns: table => new
                {
                    FinancialProjectDetailId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ProjectId = table.Column<long>(nullable: false),
                    ProjectSelectionId = table.Column<long>(nullable: true),
                    ProjectName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialProjectDetail", x => x.FinancialProjectDetailId);
                    table.ForeignKey(
                        name: "FK_FinancialProjectDetail_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PriorityCriteriaDetail",
                columns: table => new
                {
                    PriorityCriteriaDetailId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ProjectId = table.Column<long>(nullable: false),
                    IncreaseEligibility = table.Column<bool>(nullable: true),
                    IncreaseReputation = table.Column<bool>(nullable: true),
                    ImproveDonorRelationship = table.Column<bool>(nullable: true),
                    GoodCause = table.Column<bool>(nullable: true),
                    ImprovePerformancecapacity = table.Column<bool>(nullable: true),
                    SkillImprove = table.Column<bool>(nullable: true),
                    FillingFundingGap = table.Column<bool>(nullable: true),
                    NewSoftware = table.Column<bool>(nullable: true),
                    NewEquipment = table.Column<bool>(nullable: true),
                    CoverageAreaExpansion = table.Column<bool>(nullable: true),
                    NewTraining = table.Column<bool>(nullable: true),
                    ExpansionGoal = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriorityCriteriaDetail", x => x.PriorityCriteriaDetailId);
                    table.ForeignKey(
                        name: "FK_PriorityCriteriaDetail_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PriorityOtherDetail",
                columns: table => new
                {
                    PriorityOtherDetailId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ProjectId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriorityOtherDetail", x => x.PriorityOtherDetailId);
                    table.ForeignKey(
                        name: "FK_PriorityOtherDetail_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectActivitiesControl",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ProjectId = table.Column<long>(nullable: false),
                    UserID = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectActivitiesControl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectActivitiesControl_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectActivitiesControl_UserDetails_UserID",
                        column: x => x.UserID,
                        principalTable: "UserDetails",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectArea",
                columns: table => new
                {
                    ProjectAreaId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ProjectId = table.Column<long>(nullable: false),
                    AreaId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectArea", x => x.ProjectAreaId);
                    table.ForeignKey(
                        name: "FK_ProjectArea_AreaDetail_AreaId",
                        column: x => x.AreaId,
                        principalTable: "AreaDetail",
                        principalColumn: "AreaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectArea_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectCommunication",
                columns: table => new
                {
                    PCId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ProjectId = table.Column<long>(nullable: false),
                    ProjectDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectCommunication", x => x.PCId);
                    table.ForeignKey(
                        name: "FK_ProjectCommunication_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectHiringControl",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ProjectId = table.Column<long>(nullable: false),
                    UserID = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectHiringControl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectHiringControl_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectHiringControl_UserDetails_UserID",
                        column: x => x.UserID,
                        principalTable: "UserDetails",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectJobDetail",
                columns: table => new
                {
                    ProjectJobId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ProjectJobCode = table.Column<string>(nullable: true),
                    ProjectJobName = table.Column<string>(nullable: true),
                    ProjectId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectJobDetail", x => x.ProjectJobId);
                    table.ForeignKey(
                        name: "FK_ProjectJobDetail_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectLogisticsControl",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ProjectId = table.Column<long>(nullable: false),
                    UserID = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectLogisticsControl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectLogisticsControl_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectLogisticsControl_UserDetails_UserID",
                        column: x => x.UserID,
                        principalTable: "UserDetails",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectOpportunityControl",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ProjectId = table.Column<long>(nullable: false),
                    UserID = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectOpportunityControl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectOpportunityControl_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectOpportunityControl_UserDetails_UserID",
                        column: x => x.UserID,
                        principalTable: "UserDetails",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectOtherDetail",
                columns: table => new
                {
                    ProjectOtherDetailId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ProjectId = table.Column<long>(nullable: false),
                    opportunityNo = table.Column<string>(nullable: true),
                    opportunity = table.Column<string>(nullable: true),
                    opportunitydescription = table.Column<string>(nullable: true),
                    ProvinceId = table.Column<string>(nullable: true),
                    DistrictID = table.Column<string>(nullable: true),
                    OfficeId = table.Column<int>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: true),
                    budget = table.Column<string>(nullable: true),
                    beneficiaryMale = table.Column<int>(nullable: true),
                    beneficiaryFemale = table.Column<int>(nullable: true),
                    projectGoal = table.Column<string>(nullable: true),
                    projectObjective = table.Column<string>(nullable: true),
                    mainActivities = table.Column<string>(nullable: true),
                    DonorId = table.Column<long>(nullable: true),
                    SubmissionDate = table.Column<DateTime>(nullable: true),
                    REOIReceiveDate = table.Column<DateTime>(nullable: true),
                    StrengthConsiderationId = table.Column<long>(nullable: true),
                    GenderConsiderationId = table.Column<long>(nullable: true),
                    GenderRemarks = table.Column<string>(nullable: true),
                    SecurityId = table.Column<long>(nullable: true),
                    SecurityConsiderationId = table.Column<string>(nullable: true),
                    SecurityRemarks = table.Column<string>(nullable: true),
                    InDirectBeneficiaryFemale = table.Column<int>(nullable: true),
                    InDirectBeneficiaryMale = table.Column<int>(nullable: true),
                    OpportunityType = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectOtherDetail", x => x.ProjectOtherDetailId);
                    table.ForeignKey(
                        name: "FK_ProjectOtherDetail_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectPhaseTime",
                columns: table => new
                {
                    ProjectPhaseTimeId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ProjectId = table.Column<long>(nullable: false),
                    ProjectPhaseDetailsId = table.Column<long>(nullable: false),
                    PhaseStartData = table.Column<DateTime>(nullable: true),
                    PhaseEndDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectPhaseTime", x => x.ProjectPhaseTimeId);
                    table.ForeignKey(
                        name: "FK_ProjectPhaseTime_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectPhaseTime_ProjectPhaseDetails_ProjectPhaseDetailsId",
                        column: x => x.ProjectPhaseDetailsId,
                        principalTable: "ProjectPhaseDetails",
                        principalColumn: "ProjectPhaseDetailsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectProgram",
                columns: table => new
                {
                    ProjectProgramId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ProjectId = table.Column<long>(nullable: false),
                    ProgramId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectProgram", x => x.ProjectProgramId);
                    table.ForeignKey(
                        name: "FK_ProjectProgram_ProgramDetail_ProgramId",
                        column: x => x.ProgramId,
                        principalTable: "ProgramDetail",
                        principalColumn: "ProgramId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectProgram_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectProposalDetail",
                columns: table => new
                {
                    ProjectProposaldetailId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    FolderName = table.Column<string>(nullable: true),
                    FolderId = table.Column<string>(nullable: true),
                    ProposalFileName = table.Column<string>(nullable: true),
                    ProjectId = table.Column<long>(nullable: false),
                    ProposalFileId = table.Column<string>(nullable: true),
                    EDIFileName = table.Column<string>(nullable: true),
                    EdiFileId = table.Column<string>(nullable: true),
                    BudgetFileName = table.Column<string>(nullable: true),
                    BudgetFileId = table.Column<string>(nullable: true),
                    ConceptFileName = table.Column<string>(nullable: true),
                    ConceptFileId = table.Column<string>(nullable: true),
                    PresentationFileName = table.Column<string>(nullable: true),
                    PresentationFileId = table.Column<string>(nullable: true),
                    ProposalWebLink = table.Column<string>(nullable: true),
                    EDIFileWebLink = table.Column<string>(nullable: true),
                    BudgetFileWebLink = table.Column<string>(nullable: true),
                    ConceptFileWebLink = table.Column<string>(nullable: true),
                    PresentationFileWebLink = table.Column<string>(nullable: true),
                    ProposalExtType = table.Column<string>(nullable: true),
                    EDIFileExtType = table.Column<string>(nullable: true),
                    BudgetFileExtType = table.Column<string>(nullable: true),
                    ConceptFileExtType = table.Column<string>(nullable: true),
                    PresentationExtType = table.Column<string>(nullable: true),
                    ProposalStartDate = table.Column<DateTime>(nullable: true),
                    ProposalBudget = table.Column<double>(nullable: true),
                    ProposalDueDate = table.Column<DateTime>(nullable: true),
                    ProjectAssignTo = table.Column<int>(nullable: true),
                    IsProposalAccept = table.Column<bool>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: true),
                    UserId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectProposalDetail", x => x.ProjectProposaldetailId);
                    table.ForeignKey(
                        name: "FK_ProjectProposalDetail_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectSector",
                columns: table => new
                {
                    ProjectSectorId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ProjectId = table.Column<long>(nullable: false),
                    SectorId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectSector", x => x.ProjectSectorId);
                    table.ForeignKey(
                        name: "FK_ProjectSector_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectSector_SectorDetails_SectorId",
                        column: x => x.SectorId,
                        principalTable: "SectorDetails",
                        principalColumn: "SectorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurposeofInitiativeCriteria",
                columns: table => new
                {
                    ProductServiceId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Awareness = table.Column<bool>(nullable: true),
                    ProjectId = table.Column<long>(nullable: false),
                    Infrastructure = table.Column<bool>(nullable: true),
                    CapacityBuilding = table.Column<bool>(nullable: true),
                    IncomeGeneration = table.Column<bool>(nullable: true),
                    Mobilization = table.Column<bool>(nullable: true),
                    PeaceBuilding = table.Column<bool>(nullable: true),
                    SocialProtection = table.Column<bool>(nullable: true),
                    SustainableLivelihood = table.Column<bool>(nullable: true),
                    Advocacy = table.Column<bool>(nullable: true),
                    Literacy = table.Column<bool>(nullable: true),
                    EducationCapacityBuilding = table.Column<bool>(nullable: true),
                    SchoolUpgrading = table.Column<bool>(nullable: true),
                    EducationInEmergency = table.Column<bool>(nullable: true),
                    OnlineEducation = table.Column<bool>(nullable: true),
                    CommunityBasedEducation = table.Column<bool>(nullable: true),
                    AcceleratedLearningProgram = table.Column<bool>(nullable: true),
                    PrimaryHealthServices = table.Column<bool>(nullable: true),
                    ReproductiveHealth = table.Column<bool>(nullable: true),
                    Immunization = table.Column<bool>(nullable: true),
                    InfantandYoungChildFeeding = table.Column<bool>(nullable: true),
                    Nutrition = table.Column<bool>(nullable: true),
                    CommunicableDisease = table.Column<bool>(nullable: true),
                    Hygiene = table.Column<bool>(nullable: true),
                    EnvironmentalHealth = table.Column<bool>(nullable: true),
                    MentalHealthandDisabilityService = table.Column<bool>(nullable: true),
                    HealthCapacityBuilding = table.Column<bool>(nullable: true),
                    Telemedicine = table.Column<bool>(nullable: true),
                    MitigationProjects = table.Column<bool>(nullable: true),
                    WaterSupply = table.Column<bool>(nullable: true),
                    Sanitation = table.Column<bool>(nullable: true),
                    DisasterRiskHygiene = table.Column<bool>(nullable: true),
                    DisasterCapacityBuilding = table.Column<bool>(nullable: true),
                    EmergencyResponse = table.Column<bool>(nullable: true),
                    RenewableEnergy = table.Column<bool>(nullable: true),
                    Shelter = table.Column<bool>(nullable: true),
                    NaturalResourceManagement = table.Column<bool>(nullable: true),
                    AggriculutreCapacityBuilding = table.Column<bool>(nullable: true),
                    LivestockManagement = table.Column<bool>(nullable: true),
                    FoodSecurity = table.Column<bool>(nullable: true),
                    ResearchandPublication = table.Column<bool>(nullable: true),
                    Horticulture = table.Column<bool>(nullable: true),
                    Irrigation = table.Column<bool>(nullable: true),
                    Livelihood = table.Column<bool>(nullable: true),
                    ValueChain = table.Column<bool>(nullable: true),
                    Children = table.Column<bool>(nullable: true),
                    Disabled = table.Column<bool>(nullable: true),
                    IDPs = table.Column<bool>(nullable: true),
                    Returnees = table.Column<bool>(nullable: true),
                    Kuchis = table.Column<bool>(nullable: true),
                    Widows = table.Column<bool>(nullable: true),
                    Men = table.Column<bool>(nullable: true),
                    Women = table.Column<bool>(nullable: true),
                    Youth = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurposeofInitiativeCriteria", x => x.ProductServiceId);
                    table.ForeignKey(
                        name: "FK_PurposeofInitiativeCriteria_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RiskCriteriaDetail",
                columns: table => new
                {
                    RiskCriteriaDetailId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ProjectId = table.Column<long>(nullable: false),
                    Security = table.Column<bool>(nullable: true),
                    Staff = table.Column<bool>(nullable: true),
                    ProjectAssets = table.Column<bool>(nullable: true),
                    Suppliers = table.Column<bool>(nullable: true),
                    Beneficiaries = table.Column<bool>(nullable: true),
                    OverallOrganization = table.Column<bool>(nullable: true),
                    DeliveryFaiLure = table.Column<bool>(nullable: true),
                    PrematureSeizure = table.Column<bool>(nullable: true),
                    GovernmentConfiscation = table.Column<bool>(nullable: true),
                    DesctructionByTerroristActivity = table.Column<bool>(nullable: true),
                    Reputation = table.Column<bool>(nullable: true),
                    Religious = table.Column<bool>(nullable: true),
                    Sectarian = table.Column<bool>(nullable: true),
                    Ethinc = table.Column<bool>(nullable: true),
                    Social = table.Column<bool>(nullable: true),
                    Traditional = table.Column<bool>(nullable: true),
                    FocusDivertingrisk = table.Column<bool>(nullable: true),
                    Financiallosses = table.Column<bool>(nullable: true),
                    Opportunityloss = table.Column<bool>(nullable: true),
                    ProjectSelection = table.Column<string>(nullable: true),
                    Probablydelaysinfunding = table.Column<bool>(nullable: true),
                    OtherOrganizationalHarms = table.Column<bool>(nullable: true),
                    OrganizationalDescription = table.Column<string>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: true),
                    Geographical = table.Column<bool>(nullable: true),
                    Insecurity = table.Column<bool>(nullable: true),
                    Season = table.Column<bool>(nullable: true),
                    Ethnicity = table.Column<bool>(nullable: true),
                    Culture = table.Column<bool>(nullable: true),
                    ReligiousBeliefs = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskCriteriaDetail", x => x.RiskCriteriaDetailId);
                    table.ForeignKey(
                        name: "FK_RiskCriteriaDetail_CurrencyDetails_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "CurrencyDetails",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RiskCriteriaDetail_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SecurityConsiderationMultiSelect",
                columns: table => new
                {
                    SecurityConsiderationMultiSelectId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ProjectId = table.Column<long>(nullable: false),
                    SecurityConsiderationId = table.Column<long>(nullable: false),
                    SecurityConsiderationSelectedId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityConsiderationMultiSelect", x => x.SecurityConsiderationMultiSelectId);
                    table.ForeignKey(
                        name: "FK_SecurityConsiderationMultiSelect_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SecurityConsiderationMultiSelect_SecurityConsiderationDetai~",
                        column: x => x.SecurityConsiderationId,
                        principalTable: "SecurityConsiderationDetail",
                        principalColumn: "SecurityConsiderationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WinProjectDetails",
                columns: table => new
                {
                    WinProjectId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ProjectId = table.Column<long>(nullable: false),
                    CommentText = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    FilePath = table.Column<string>(nullable: true),
                    IsWin = table.Column<bool>(nullable: true),
                    UploadedFile = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WinProjectDetails", x => x.WinProjectId);
                    table.ForeignKey(
                        name: "FK_WinProjectDetails_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContractDetails",
                columns: table => new
                {
                    ContractId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ContractCode = table.Column<string>(nullable: true),
                    ClientId = table.Column<long>(nullable: true),
                    ClientName = table.Column<string>(nullable: true),
                    ActivityTypeId = table.Column<long>(nullable: true),
                    UnitRateId = table.Column<long>(nullable: true),
                    UnitRate = table.Column<double>(nullable: false),
                    CurrencyId = table.Column<int>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    LanguageId = table.Column<long>(nullable: true),
                    LanguageDetailLanguageId = table.Column<int>(nullable: true),
                    MediumId = table.Column<long>(nullable: true),
                    NatureId = table.Column<long>(nullable: true),
                    TimeCategoryId = table.Column<long>(nullable: true),
                    MediaCategoryId = table.Column<long>(nullable: true),
                    QualityId = table.Column<long>(nullable: true),
                    IsCompleted = table.Column<bool>(nullable: false),
                    IsApproved = table.Column<bool>(nullable: false),
                    IsDeclined = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractDetails", x => x.ContractId);
                    table.ForeignKey(
                        name: "FK_ContractDetails_ActivityTypes_ActivityTypeId",
                        column: x => x.ActivityTypeId,
                        principalTable: "ActivityTypes",
                        principalColumn: "ActivityTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContractDetails_ClientDetails_ClientId",
                        column: x => x.ClientId,
                        principalTable: "ClientDetails",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContractDetails_CurrencyDetails_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "CurrencyDetails",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContractDetails_LanguageDetail_LanguageDetailLanguageId",
                        column: x => x.LanguageDetailLanguageId,
                        principalTable: "LanguageDetail",
                        principalColumn: "LanguageId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContractDetails_MediaCategories_MediaCategoryId",
                        column: x => x.MediaCategoryId,
                        principalTable: "MediaCategories",
                        principalColumn: "MediaCategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContractDetails_Mediums_MediumId",
                        column: x => x.MediumId,
                        principalTable: "Mediums",
                        principalColumn: "MediumId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContractDetails_Natures_NatureId",
                        column: x => x.NatureId,
                        principalTable: "Natures",
                        principalColumn: "NatureId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContractDetails_Qualities_QualityId",
                        column: x => x.QualityId,
                        principalTable: "Qualities",
                        principalColumn: "QualityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContractDetails_TimeCategories_TimeCategoryId",
                        column: x => x.TimeCategoryId,
                        principalTable: "TimeCategories",
                        principalColumn: "TimeCategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContractDetails_UnitRates_UnitRateId",
                        column: x => x.UnitRateId,
                        principalTable: "UnitRates",
                        principalColumn: "UnitRateId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GainLossSelectedAccounts",
                columns: table => new
                {
                    GainLossSelectedAccountId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ChartOfAccountNewId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GainLossSelectedAccounts", x => x.GainLossSelectedAccountId);
                    table.ForeignKey(
                        name: "FK_GainLossSelectedAccounts_ChartOfAccountNew_ChartOfAccountNe~",
                        column: x => x.ChartOfAccountNewId,
                        principalTable: "ChartOfAccountNew",
                        principalColumn: "ChartOfAccountNewId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PensionDebitAccountMaster",
                columns: table => new
                {
                    PensionDebitAccountId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ChartOfAccountNewId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PensionDebitAccountMaster", x => x.PensionDebitAccountId);
                    table.ForeignKey(
                        name: "FK_PensionDebitAccountMaster_ChartOfAccountNew_ChartOfAccountN~",
                        column: x => x.ChartOfAccountNewId,
                        principalTable: "ChartOfAccountNew",
                        principalColumn: "ChartOfAccountNewId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StoreInventories",
                columns: table => new
                {
                    InventoryId = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    InventoryCode = table.Column<string>(nullable: true),
                    InventoryName = table.Column<string>(nullable: true),
                    InventoryDescription = table.Column<string>(nullable: true),
                    AssetType = table.Column<int>(nullable: false),
                    InventoryDebitAccount = table.Column<long>(nullable: false),
                    InventoryCreditAccount = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreInventories", x => x.InventoryId);
                    table.ForeignKey(
                        name: "FK_StoreInventories_ChartOfAccountNew_InventoryCreditAccount",
                        column: x => x.InventoryCreditAccount,
                        principalTable: "ChartOfAccountNew",
                        principalColumn: "ChartOfAccountNewId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreInventories_ChartOfAccountNew_InventoryDebitAccount",
                        column: x => x.InventoryDebitAccount,
                        principalTable: "ChartOfAccountNew",
                        principalColumn: "ChartOfAccountNewId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Advances",
                columns: table => new
                {
                    AdvancesId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    AdvanceDate = table.Column<DateTime>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    EmployeeCode = table.Column<string>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: false),
                    VoucherReferenceNo = table.Column<long>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ModeOfReturn = table.Column<string>(nullable: true),
                    ApprovedBy = table.Column<int>(nullable: false),
                    RequestAmount = table.Column<double>(nullable: false),
                    AdvanceAmount = table.Column<double>(nullable: false),
                    OfficeId = table.Column<int>(nullable: false),
                    IsApproved = table.Column<bool>(nullable: false),
                    IsDeducted = table.Column<bool>(nullable: false),
                    AppraisalApprovedDate = table.Column<DateTime>(nullable: false),
                    DeductedDate = table.Column<DateTime>(nullable: false),
                    IsAdvanceRecovery = table.Column<bool>(nullable: false),
                    AdvanceRecoveryDate = table.Column<DateTime>(nullable: false),
                    NumberOfInstallments = table.Column<int>(nullable: true),
                    RecoveredAmount = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Advances", x => x.AdvancesId);
                    table.ForeignKey(
                        name: "FK_Advances_CurrencyDetails_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "CurrencyDetails",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Advances_EmployeeDetail_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssignLeaveToEmployee",
                columns: table => new
                {
                    LeaveId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    LeaveReasonId = table.Column<int>(nullable: false),
                    AssignUnit = table.Column<int>(nullable: true),
                    FinancialYearId = table.Column<int>(nullable: false),
                    Status = table.Column<string>(maxLength: 50, nullable: true),
                    UsedLeaveUnit = table.Column<int>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignLeaveToEmployee", x => x.LeaveId);
                    table.ForeignKey(
                        name: "FK_AssignLeaveToEmployee_EmployeeDetail_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignLeaveToEmployee_FinancialYearDetail_FinancialYearId",
                        column: x => x.FinancialYearId,
                        principalTable: "FinancialYearDetail",
                        principalColumn: "FinancialYearId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignLeaveToEmployee_LeaveReasonDetail_LeaveReasonId",
                        column: x => x.LeaveReasonId,
                        principalTable: "LeaveReasonDetail",
                        principalColumn: "LeaveReasonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeApplyLeave",
                columns: table => new
                {
                    ApplyLeaveId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: true),
                    FromDate = table.Column<DateTime>(nullable: false),
                    ToDate = table.Column<DateTime>(nullable: false),
                    LeaveReasonId = table.Column<int>(nullable: false),
                    ApplyLeaveStatusId = table.Column<int>(maxLength: 30, nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    FinancialYearId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeApplyLeave", x => x.ApplyLeaveId);
                    table.ForeignKey(
                        name: "FK_EmployeeApplyLeave_EmployeeDetail_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeApplyLeave_FinancialYearDetail_FinancialYearId",
                        column: x => x.FinancialYearId,
                        principalTable: "FinancialYearDetail",
                        principalColumn: "FinancialYearId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeApplyLeave_LeaveReasonDetail_LeaveReasonId",
                        column: x => x.LeaveReasonId,
                        principalTable: "LeaveReasonDetail",
                        principalColumn: "LeaveReasonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeAppraisalDetails",
                columns: table => new
                {
                    EmployeeAppraisalDetailsId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    EmployeeCode = table.Column<string>(nullable: true),
                    EmployeeName = table.Column<string>(nullable: true),
                    FatherName = table.Column<string>(nullable: true),
                    Position = table.Column<string>(nullable: true),
                    Department = table.Column<string>(nullable: true),
                    Qualification = table.Column<string>(nullable: true),
                    DutyStation = table.Column<string>(nullable: true),
                    RecruitmentDate = table.Column<DateTime>(nullable: false),
                    AppraisalPeriod = table.Column<int>(nullable: false),
                    CurrentAppraisalDate = table.Column<DateTime>(nullable: false),
                    OfficeId = table.Column<int>(nullable: false),
                    AppraisalStatus = table.Column<bool>(nullable: false),
                    TotalScore = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeAppraisalDetails", x => x.EmployeeAppraisalDetailsId);
                    table.ForeignKey(
                        name: "FK_EmployeeAppraisalDetails_EmployeeDetail_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeAttendance",
                columns: table => new
                {
                    AttendanceId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    InTime = table.Column<DateTime>(nullable: true),
                    OutTime = table.Column<DateTime>(nullable: true),
                    TotalWorkTime = table.Column<string>(nullable: true),
                    HoverTimeHours = table.Column<int>(nullable: true),
                    AttendanceTypeId = table.Column<int>(nullable: false),
                    LeaveReasonId = table.Column<int>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    HolidayId = table.Column<long>(nullable: true),
                    WorkTimeMinutes = table.Column<int>(nullable: false),
                    OverTimeMinutes = table.Column<int>(nullable: false),
                    FinancialYearId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeAttendance", x => x.AttendanceId);
                    table.ForeignKey(
                        name: "FK_EmployeeAttendance_EmployeeDetail_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeAttendance_FinancialYearDetail_FinancialYearId",
                        column: x => x.FinancialYearId,
                        principalTable: "FinancialYearDetail",
                        principalColumn: "FinancialYearId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeAttendance_HolidayDetails_HolidayId",
                        column: x => x.HolidayId,
                        principalTable: "HolidayDetails",
                        principalColumn: "HolidayId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeAttendance_LeaveReasonDetail_LeaveReasonId",
                        column: x => x.LeaveReasonId,
                        principalTable: "LeaveReasonDetail",
                        principalColumn: "LeaveReasonId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeContract",
                columns: table => new
                {
                    EmployeeContractId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    FatherName = table.Column<string>(nullable: true),
                    EmployeeCode = table.Column<string>(nullable: true),
                    Designation = table.Column<int>(nullable: true),
                    ContractStartDate = table.Column<DateTime>(nullable: true),
                    ContractEndDate = table.Column<DateTime>(nullable: true),
                    DurationOfContract = table.Column<int>(nullable: true),
                    Salary = table.Column<double>(nullable: true),
                    Grade = table.Column<int>(nullable: true),
                    DutyStation = table.Column<int>(nullable: true),
                    Country = table.Column<int>(nullable: true),
                    Province = table.Column<int>(nullable: true),
                    Project = table.Column<int>(nullable: true),
                    BudgetLine = table.Column<long>(nullable: true),
                    Job = table.Column<string>(nullable: true),
                    WorkTime = table.Column<int>(nullable: true),
                    WorkDayHours = table.Column<int>(nullable: true),
                    ContractStatus = table.Column<string>(nullable: true),
                    PeriodType = table.Column<string>(nullable: true),
                    ContractNumber = table.Column<float>(nullable: true),
                    ContractPeriod = table.Column<float>(nullable: true),
                    CountryDari = table.Column<string>(nullable: true),
                    DesignationDari = table.Column<string>(nullable: true),
                    DutyStationDari = table.Column<string>(nullable: true),
                    FatherNameDari = table.Column<string>(nullable: true),
                    GradeDari = table.Column<string>(nullable: true),
                    JobDari = table.Column<string>(nullable: true),
                    ProvinceDari = table.Column<string>(nullable: true),
                    EmployeeNameDari = table.Column<string>(nullable: true),
                    ProjectNameDari = table.Column<string>(nullable: true),
                    BudgetLineDari = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeContract", x => x.EmployeeContractId);
                    table.ForeignKey(
                        name: "FK_EmployeeContract_EmployeeDetail_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeContract_JobGrade_Grade",
                        column: x => x.Grade,
                        principalTable: "JobGrade",
                        principalColumn: "GradeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeDocumentDetail",
                columns: table => new
                {
                    DocumentID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DocumentName = table.Column<string>(maxLength: 100, nullable: true),
                    DocumentDate = table.Column<DateTime>(nullable: true),
                    EmployeeID = table.Column<int>(nullable: true),
                    FilePath = table.Column<byte[]>(nullable: true),
                    DocumentGUID = table.Column<string>(nullable: true),
                    Extension = table.Column<string>(nullable: true),
                    DocumentFilePath = table.Column<string>(nullable: true),
                    DocumentType = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeDocumentDetail", x => x.DocumentID);
                    table.ForeignKey(
                        name: "FK_EmployeeDocumentDetail_EmployeeDetail_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeEducations",
                columns: table => new
                {
                    EmployeeEducationsId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    EducationFrom = table.Column<DateTime>(nullable: true),
                    EducationTo = table.Column<DateTime>(nullable: true),
                    FieldOfStudy = table.Column<string>(nullable: true),
                    Institute = table.Column<string>(nullable: true),
                    Degree = table.Column<string>(nullable: true),
                    EmployeeID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeEducations", x => x.EmployeeEducationsId);
                    table.ForeignKey(
                        name: "FK_EmployeeEducations_EmployeeDetail_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeHealthInfo",
                columns: table => new
                {
                    EmployeeHealthInfoId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: true),
                    PhysicanName = table.Column<string>(nullable: true),
                    HospitalName = table.Column<string>(nullable: true),
                    HospitalAddress = table.Column<string>(nullable: true),
                    Height = table.Column<float>(nullable: true),
                    Weight = table.Column<float>(nullable: true),
                    BloodPressure = table.Column<float>(nullable: true),
                    VisualWithoutGlassesR = table.Column<float>(nullable: true),
                    VisualWithoutGlassesL = table.Column<float>(nullable: true),
                    VisualWithGlassesR = table.Column<float>(nullable: true),
                    VisualWithGlassesL = table.Column<float>(nullable: true),
                    HearingR = table.Column<float>(nullable: true),
                    HearingRType = table.Column<string>(nullable: true),
                    HearingL = table.Column<float>(nullable: true),
                    HearingLType = table.Column<string>(nullable: true),
                    HistoryOfPastIllness = table.Column<string>(nullable: true),
                    HealthPresentCondition = table.Column<string>(nullable: true),
                    ResultOfChestXRay = table.Column<string>(nullable: true),
                    BloodGroup = table.Column<string>(nullable: true),
                    Hbs = table.Column<string>(nullable: true),
                    Hcv = table.Column<string>(nullable: true),
                    OverallHealthCondition = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeHealthInfo", x => x.EmployeeHealthInfoId);
                    table.ForeignKey(
                        name: "FK_EmployeeHealthInfo_EmployeeDetail_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeHistoryDetail",
                columns: table => new
                {
                    HistoryID = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    EmployeeID = table.Column<int>(nullable: true),
                    HistoryDate = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeHistoryDetail", x => x.HistoryID);
                    table.ForeignKey(
                        name: "FK_EmployeeHistoryDetail_EmployeeDetail_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeHistoryOutsideCountry",
                columns: table => new
                {
                    EmployeeHistoryOutsideCountryId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    EmploymentFrom = table.Column<DateTime>(nullable: true),
                    EmploymentTo = table.Column<DateTime>(nullable: true),
                    Organization = table.Column<string>(nullable: true),
                    MonthlySalary = table.Column<string>(nullable: true),
                    ReasonForLeaving = table.Column<string>(nullable: true),
                    EmployeeID = table.Column<int>(nullable: true),
                    Position = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeHistoryOutsideCountry", x => x.EmployeeHistoryOutsideCountryId);
                    table.ForeignKey(
                        name: "FK_EmployeeHistoryOutsideCountry_EmployeeDetail_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeHistoryOutsideOrganization",
                columns: table => new
                {
                    EmployeeHistoryOutsideOrganizationId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    EmploymentFrom = table.Column<DateTime>(nullable: true),
                    EmploymentTo = table.Column<DateTime>(nullable: true),
                    Organization = table.Column<string>(nullable: true),
                    MonthlySalary = table.Column<string>(nullable: true),
                    ReasonForLeaving = table.Column<string>(nullable: true),
                    EmployeeID = table.Column<int>(nullable: true),
                    Position = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeHistoryOutsideOrganization", x => x.EmployeeHistoryOutsideOrganizationId);
                    table.ForeignKey(
                        name: "FK_EmployeeHistoryOutsideOrganization_EmployeeDetail_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeInfoReferences",
                columns: table => new
                {
                    EmployeeInfoReferencesId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Relationship = table.Column<string>(nullable: true),
                    Position = table.Column<string>(nullable: true),
                    Organization = table.Column<string>(nullable: true),
                    EmployeeID = table.Column<int>(nullable: false),
                    PhoneNo = table.Column<long>(nullable: false),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeInfoReferences", x => x.EmployeeInfoReferencesId);
                    table.ForeignKey(
                        name: "FK_EmployeeInfoReferences_EmployeeDetail_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeMonthlyPayroll",
                columns: table => new
                {
                    MonthlyPayrollId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    EmployeeID = table.Column<int>(nullable: false),
                    SalaryHeadId = table.Column<int>(nullable: false),
                    MonthlyAmount = table.Column<double>(nullable: false),
                    CurrencyId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeMonthlyPayroll", x => x.MonthlyPayrollId);
                    table.ForeignKey(
                        name: "FK_EmployeeMonthlyPayroll_CurrencyDetails_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "CurrencyDetails",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeMonthlyPayroll_EmployeeDetail_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeMonthlyPayroll_SalaryHeadDetails_SalaryHeadId",
                        column: x => x.SalaryHeadId,
                        principalTable: "SalaryHeadDetails",
                        principalColumn: "SalaryHeadId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeOtherSkills",
                columns: table => new
                {
                    EmployeeOtherSkillsId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    TypeOfSkill = table.Column<string>(nullable: true),
                    AbilityLevel = table.Column<string>(nullable: true),
                    Experience = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    EmployeeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeOtherSkills", x => x.EmployeeOtherSkillsId);
                    table.ForeignKey(
                        name: "FK_EmployeeOtherSkills_EmployeeDetail_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeePayroll",
                columns: table => new
                {
                    PayrollId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    EmployeeID = table.Column<int>(nullable: false),
                    SalaryHeadId = table.Column<int>(nullable: true),
                    MonthlyAmount = table.Column<double>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: true),
                    PaymentType = table.Column<int>(nullable: true),
                    HeadTypeId = table.Column<int>(nullable: true),
                    PensionRate = table.Column<double>(nullable: true),
                    CurrencyCode = table.Column<string>(nullable: true),
                    BasicPay = table.Column<double>(nullable: true),
                    AllowDeductionFlag = table.Column<int>(nullable: true),
                    AccountNo = table.Column<long>(nullable: true),
                    TransactionTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePayroll", x => x.PayrollId);
                    table.ForeignKey(
                        name: "FK_EmployeePayroll_CurrencyDetails_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "CurrencyDetails",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeePayroll_EmployeeDetail_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeePayroll_SalaryHeadDetails_SalaryHeadId",
                        column: x => x.SalaryHeadId,
                        principalTable: "SalaryHeadDetails",
                        principalColumn: "SalaryHeadId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeePayrollAccountHead",
                columns: table => new
                {
                    EmployeePayrollAccountId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PayrollHeadId = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    PayrollHeadTypeId = table.Column<int>(nullable: false),
                    PayrollHeadName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    AccountNo = table.Column<long>(nullable: true),
                    TransactionTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePayrollAccountHead", x => x.EmployeePayrollAccountId);
                    table.ForeignKey(
                        name: "FK_EmployeePayrollAccountHead_EmployeeDetail_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeePayrollAccountHead_PayrollAccountHead_PayrollHeadId",
                        column: x => x.PayrollHeadId,
                        principalTable: "PayrollAccountHead",
                        principalColumn: "PayrollHeadId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeePayrollMonth",
                columns: table => new
                {
                    MonthlyPayrollId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    EmployeeID = table.Column<int>(nullable: false),
                    SalaryHeadId = table.Column<int>(nullable: false),
                    MonthlyAmount = table.Column<double>(nullable: false),
                    CurrencyId = table.Column<int>(nullable: false),
                    PaymentType = table.Column<int>(nullable: true),
                    HeadTypeId = table.Column<int>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    AccountNo = table.Column<long>(nullable: true),
                    TransactionTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePayrollMonth", x => x.MonthlyPayrollId);
                    table.ForeignKey(
                        name: "FK_EmployeePayrollMonth_CurrencyDetails_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "CurrencyDetails",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeePayrollMonth_EmployeeDetail_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeePayrollMonth_SalaryHeadDetails_SalaryHeadId",
                        column: x => x.SalaryHeadId,
                        principalTable: "SalaryHeadDetails",
                        principalColumn: "SalaryHeadId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeProfessionalDetail",
                columns: table => new
                {
                    EmployeeProfessionalId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: true),
                    EmployeeTypeId = table.Column<int>(nullable: true),
                    Status = table.Column<string>(maxLength: 20, nullable: true),
                    OfficeId = table.Column<int>(nullable: true),
                    DesignationId = table.Column<int>(nullable: true),
                    DepartmentId = table.Column<int>(nullable: true),
                    HiredOn = table.Column<DateTime>(nullable: true),
                    FiredOn = table.Column<DateTime>(nullable: true),
                    FiredReason = table.Column<string>(nullable: true),
                    ResignationOn = table.Column<DateTime>(nullable: true),
                    ResignationReason = table.Column<string>(nullable: true),
                    JobDescription = table.Column<string>(nullable: true),
                    TrainingBenefits = table.Column<string>(nullable: true),
                    MembershipSupportInPoliticalParty = table.Column<string>(nullable: true),
                    EmployeeContractTypeId = table.Column<int>(nullable: true),
                    Designation = table.Column<string>(nullable: true),
                    Profession = table.Column<string>(nullable: true),
                    Departments = table.Column<string>(nullable: true),
                    WorkType = table.Column<string>(nullable: true),
                    ProjectId = table.Column<long>(nullable: true),
                    RegCode = table.Column<string>(nullable: true),
                    ContractStatus = table.Column<string>(nullable: true),
                    TinNumber = table.Column<string>(nullable: true),
                    ProfessionId = table.Column<int>(nullable: true),
                    AttendanceGroupId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeProfessionalDetail", x => x.EmployeeProfessionalId);
                    table.ForeignKey(
                        name: "FK_EmployeeProfessionalDetail_AttendanceGroupMaster_Attendance~",
                        column: x => x.AttendanceGroupId,
                        principalTable: "AttendanceGroupMaster",
                        principalColumn: "AttendanceGroupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeProfessionalDetail_Department_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Department",
                        principalColumn: "DepartmentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeProfessionalDetail_DesignationDetail_DesignationId",
                        column: x => x.DesignationId,
                        principalTable: "DesignationDetail",
                        principalColumn: "DesignationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeProfessionalDetail_EmployeeContractType_EmployeeCon~",
                        column: x => x.EmployeeContractTypeId,
                        principalTable: "EmployeeContractType",
                        principalColumn: "EmployeeContractTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeProfessionalDetail_EmployeeDetail_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeProfessionalDetail_EmployeeType_EmployeeTypeId",
                        column: x => x.EmployeeTypeId,
                        principalTable: "EmployeeType",
                        principalColumn: "EmployeeTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeProfessionalDetail_OfficeDetail_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "OfficeDetail",
                        principalColumn: "OfficeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeProfessionalDetail_ProfessionDetails_ProfessionId",
                        column: x => x.ProfessionId,
                        principalTable: "ProfessionDetails",
                        principalColumn: "ProfessionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeRelativeInfo",
                columns: table => new
                {
                    EmployeeRelativeInfoId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Relationship = table.Column<string>(nullable: true),
                    Position = table.Column<string>(nullable: true),
                    Organization = table.Column<string>(nullable: true),
                    EmployeeID = table.Column<int>(nullable: false),
                    PhoneNo = table.Column<long>(nullable: false),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeRelativeInfo", x => x.EmployeeRelativeInfoId);
                    table.ForeignKey(
                        name: "FK_EmployeeRelativeInfo_EmployeeDetail_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeSalaryBudget",
                columns: table => new
                {
                    EmployeeSalaryBudgetId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Year = table.Column<string>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: false),
                    SalaryBudget = table.Column<double>(nullable: false),
                    BudgetDisbursed = table.Column<double>(nullable: false),
                    EmployeeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeSalaryBudget", x => x.EmployeeSalaryBudgetId);
                    table.ForeignKey(
                        name: "FK_EmployeeSalaryBudget_EmployeeDetail_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeSalaryDetails",
                columns: table => new
                {
                    SalaryId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    CurrencyId = table.Column<int>(nullable: false),
                    TotalGeneralAmount = table.Column<double>(nullable: true),
                    TotalAllowance = table.Column<double>(nullable: true),
                    Totalduduction = table.Column<double>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    PaymentType = table.Column<int>(nullable: false),
                    PensionRate = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeSalaryDetails", x => x.SalaryId);
                    table.ForeignKey(
                        name: "FK_EmployeeSalaryDetails_CurrencyDetails_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "CurrencyDetails",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeSalaryDetails_EmployeeDetail_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExistInterviewDetails",
                columns: table => new
                {
                    ExistInterviewDetailsId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    EmployeeID = table.Column<int>(nullable: false),
                    EmployeeCode = table.Column<string>(nullable: true),
                    DutiesOfJob = table.Column<string>(nullable: true),
                    TrainingAndDevelopmentPrograms = table.Column<string>(nullable: true),
                    OpportunityAdvancement = table.Column<string>(nullable: true),
                    SalaryTreatment = table.Column<string>(nullable: true),
                    BenefitProgram = table.Column<string>(nullable: true),
                    WorkingConditions = table.Column<string>(nullable: true),
                    WorkingHours = table.Column<string>(nullable: true),
                    CoWorkers = table.Column<string>(nullable: true),
                    Supervisors = table.Column<string>(nullable: true),
                    GenderFriendlyEnvironment = table.Column<string>(nullable: true),
                    OverallJobSatisfaction = table.Column<string>(nullable: true),
                    Benefits = table.Column<bool>(nullable: false),
                    BetterJobOpportunity = table.Column<bool>(nullable: false),
                    FamilyReasons = table.Column<bool>(nullable: false),
                    NotChallenged = table.Column<bool>(nullable: false),
                    Pay = table.Column<bool>(nullable: false),
                    PersonalReasons = table.Column<bool>(nullable: false),
                    Relocation = table.Column<bool>(nullable: false),
                    ReturnToSchool = table.Column<bool>(nullable: false),
                    ConflictWithSuoervisors = table.Column<bool>(nullable: false),
                    ConflictWithOther = table.Column<bool>(nullable: false),
                    WorkRelationship = table.Column<bool>(nullable: false),
                    CompanyInstability = table.Column<bool>(nullable: false),
                    CareerChange = table.Column<bool>(nullable: false),
                    HealthIssue = table.Column<bool>(nullable: false),
                    HadGoodSynergy = table.Column<string>(nullable: true),
                    HadAdequateEquipment = table.Column<string>(nullable: true),
                    WasAdequatelyStaffed = table.Column<string>(nullable: true),
                    WasEfficient = table.Column<string>(nullable: true),
                    JobWasChallenging = table.Column<string>(nullable: true),
                    SkillsEffectivelyUsed = table.Column<string>(nullable: true),
                    JobOrientation = table.Column<string>(nullable: true),
                    WorkLoadReasonable = table.Column<string>(nullable: true),
                    SufficientResources = table.Column<string>(nullable: true),
                    WorkEnvironment = table.Column<string>(nullable: true),
                    ComfortableAppropriately = table.Column<string>(nullable: true),
                    Equipped = table.Column<string>(nullable: true),
                    HadKnowledgeOfJob = table.Column<string>(nullable: true),
                    HadKnowledgeSupervision = table.Column<string>(nullable: true),
                    WasOpenSuggestions = table.Column<string>(nullable: true),
                    RecognizedEmployeesContribution = table.Column<string>(nullable: true),
                    GaveFairTreatment = table.Column<string>(nullable: true),
                    WasAvailableToDiscuss = table.Column<string>(nullable: true),
                    WelcomedSuggestions = table.Column<string>(nullable: true),
                    MaintainedConsistent = table.Column<string>(nullable: true),
                    ProvidedRecognition = table.Column<string>(nullable: true),
                    EncouragedCooperation = table.Column<string>(nullable: true),
                    ProvidedDevelopment = table.Column<string>(nullable: true),
                    Question = table.Column<bool>(nullable: false),
                    Explain = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExistInterviewDetails", x => x.ExistInterviewDetailsId);
                    table.ForeignKey(
                        name: "FK_ExistInterviewDetails_EmployeeDetail_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InterviewDetails",
                columns: table => new
                {
                    InterviewDetailsId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    EmployeeID = table.Column<int>(nullable: false),
                    JobId = table.Column<long>(nullable: false),
                    PassportNo = table.Column<string>(nullable: true),
                    University = table.Column<string>(nullable: true),
                    PlaceOfBirth = table.Column<string>(nullable: true),
                    TazkiraIssuePlace = table.Column<string>(nullable: true),
                    MaritalStatus = table.Column<string>(nullable: true),
                    Experience = table.Column<string>(nullable: true),
                    ProfessionalCriteriaMarks = table.Column<string>(nullable: true),
                    MarksObtained = table.Column<string>(nullable: true),
                    WrittenTestMarks = table.Column<string>(nullable: true),
                    Ques1 = table.Column<string>(nullable: true),
                    Ques2 = table.Column<string>(nullable: true),
                    Ques3 = table.Column<string>(nullable: true),
                    PreferedLocation = table.Column<string>(nullable: true),
                    NoticePeriod = table.Column<string>(nullable: true),
                    JoiningDate = table.Column<DateTime>(nullable: false),
                    CurrentBase = table.Column<long>(nullable: false),
                    CurrentTransportation = table.Column<bool>(nullable: false),
                    CurrentMeal = table.Column<bool>(nullable: false),
                    CurrentOther = table.Column<long>(nullable: false),
                    ExpectationBase = table.Column<long>(nullable: false),
                    ExpectationTransportation = table.Column<bool>(nullable: false),
                    ExpectationMeal = table.Column<bool>(nullable: false),
                    ExpectationOther = table.Column<long>(nullable: false),
                    TotalMarksObtained = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Interviewer1 = table.Column<string>(nullable: true),
                    Interviewer2 = table.Column<string>(nullable: true),
                    Interviewer3 = table.Column<string>(nullable: true),
                    Interviewer4 = table.Column<string>(nullable: true),
                    InterviewStatus = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterviewDetails", x => x.InterviewDetailsId);
                    table.ForeignKey(
                        name: "FK_InterviewDetails_EmployeeDetail_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectAssignTo",
                columns: table => new
                {
                    ProjectAssignToId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ProjectId = table.Column<long>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectAssignTo", x => x.ProjectAssignToId);
                    table.ForeignKey(
                        name: "FK_ProjectAssignTo_EmployeeDetail_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectAssignTo_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProvinceMultiSelect",
                columns: table => new
                {
                    ProvinceMultiSelectId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ProjectId = table.Column<long>(nullable: false),
                    CountryMultiSelectId = table.Column<long>(nullable: true),
                    ProvinceId = table.Column<int>(nullable: false),
                    ProvinceSelectionId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProvinceMultiSelect", x => x.ProvinceMultiSelectId);
                    table.ForeignKey(
                        name: "FK_ProvinceMultiSelect_CountryMultiSelectDetails_CountryMultiS~",
                        column: x => x.CountryMultiSelectId,
                        principalTable: "CountryMultiSelectDetails",
                        principalColumn: "CountryMultiSelectId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProvinceMultiSelect_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProvinceMultiSelect_ProvinceDetails_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "ProvinceDetails",
                        principalColumn: "ProvinceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectCommunicationAttachment",
                columns: table => new
                {
                    PCAId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PCId = table.Column<long>(nullable: false),
                    ProjectId = table.Column<long>(nullable: false),
                    FileName = table.Column<string>(nullable: true),
                    FilePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectCommunicationAttachment", x => x.PCAId);
                    table.ForeignKey(
                        name: "FK_ProjectCommunicationAttachment_ProjectCommunication_PCId",
                        column: x => x.PCId,
                        principalTable: "ProjectCommunication",
                        principalColumn: "PCId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectCommunicationAttachment_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectBudgetLineDetail",
                columns: table => new
                {
                    BudgetLineId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                        name: "FK_ProjectBudgetLineDetail_CurrencyDetails_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "CurrencyDetails",
                        principalColumn: "CurrencyId",
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

            migrationBuilder.CreateTable(
                name: "JobDetails",
                columns: table => new
                {
                    JobId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    JobName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    JobCode = table.Column<string>(nullable: true),
                    ContractId = table.Column<long>(nullable: true),
                    JobPhaseId = table.Column<long>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsApproved = table.Column<bool>(nullable: false),
                    IsAgreementApproved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobDetails", x => x.JobId);
                    table.ForeignKey(
                        name: "FK_JobDetails_ContractDetails_ContractId",
                        column: x => x.ContractId,
                        principalTable: "ContractDetails",
                        principalColumn: "ContractId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobDetails_JobPhases_JobPhaseId",
                        column: x => x.JobPhaseId,
                        principalTable: "JobPhases",
                        principalColumn: "JobPhaseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StoreItemGroups",
                columns: table => new
                {
                    ItemGroupId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemGroupCode = table.Column<string>(nullable: true),
                    ItemGroupName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    InventoryId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreItemGroups", x => x.ItemGroupId);
                    table.ForeignKey(
                        name: "FK_StoreItemGroups_StoreInventories_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "StoreInventories",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeMonthlyAttendance",
                columns: table => new
                {
                    MonthlyAttendanceId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: true),
                    Month = table.Column<int>(nullable: true),
                    Year = table.Column<int>(nullable: true),
                    AttendanceHours = table.Column<int>(nullable: true),
                    OvertimeHours = table.Column<int>(nullable: true),
                    LeaveHours = table.Column<int>(nullable: true),
                    AbsentHours = table.Column<int>(nullable: true),
                    DeputationHours = table.Column<int>(nullable: true),
                    Status = table.Column<bool>(nullable: true),
                    Sent = table.Column<bool>(nullable: true),
                    TotalDuration = table.Column<int>(nullable: true),
                    OfficeId = table.Column<int>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: true),
                    PaymentType = table.Column<int>(nullable: true),
                    HourlyRate = table.Column<double>(nullable: true),
                    TotalGeneralAmount = table.Column<double>(nullable: true),
                    TotalAllowance = table.Column<double>(nullable: true),
                    TotalDeduction = table.Column<double>(nullable: true),
                    GrossSalary = table.Column<double>(nullable: true),
                    PensionRate = table.Column<double>(nullable: true),
                    PensionAmount = table.Column<double>(nullable: true),
                    SalaryTax = table.Column<double>(nullable: true),
                    NetSalary = table.Column<double>(nullable: true),
                    AdvanceAmount = table.Column<double>(nullable: false),
                    IsAdvanceApproved = table.Column<bool>(nullable: false),
                    IsAdvanceRecovery = table.Column<bool>(nullable: false),
                    AdvanceRecoveryAmount = table.Column<double>(nullable: false),
                    IsApproved = table.Column<bool>(nullable: false),
                    AdvanceId = table.Column<int>(nullable: true),
                    AttendanceMinutes = table.Column<int>(nullable: false),
                    OverTimeMinutes = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeMonthlyAttendance", x => x.MonthlyAttendanceId);
                    table.ForeignKey(
                        name: "FK_EmployeeMonthlyAttendance_Advances_AdvanceId",
                        column: x => x.AdvanceId,
                        principalTable: "Advances",
                        principalColumn: "AdvancesId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeMonthlyAttendance_EmployeeDetail_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeePaymentTypes",
                columns: table => new
                {
                    EmployeePaymentTypesId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OfficeId = table.Column<int>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: true),
                    FinancialYearDate = table.Column<DateTime>(nullable: true),
                    PaymentDate = table.Column<DateTime>(nullable: true),
                    EmployeeID = table.Column<int>(nullable: true),
                    EmployeeName = table.Column<string>(nullable: true),
                    PaymentType = table.Column<int>(nullable: true),
                    WorkingDays = table.Column<int>(nullable: true),
                    PresentDays = table.Column<int>(nullable: true),
                    AbsentDays = table.Column<int>(nullable: true),
                    LeaveDays = table.Column<int>(nullable: true),
                    TotalWorkHours = table.Column<int>(nullable: true),
                    HourlyRate = table.Column<double>(nullable: true),
                    TotalGeneralAmount = table.Column<double>(nullable: true),
                    TotalAllowance = table.Column<double>(nullable: true),
                    TotalDeduction = table.Column<double>(nullable: true),
                    GrossSalary = table.Column<double>(nullable: true),
                    OverTimeHours = table.Column<double>(nullable: true),
                    IsApproved = table.Column<bool>(nullable: true),
                    PensionRate = table.Column<double>(nullable: true),
                    PensionAmount = table.Column<double>(nullable: true),
                    SalaryTax = table.Column<double>(nullable: true),
                    NetSalary = table.Column<double>(nullable: true),
                    AdvanceAmount = table.Column<double>(nullable: true),
                    IsAdvanceApproved = table.Column<bool>(nullable: true),
                    IsAdvanceRecovery = table.Column<bool>(nullable: true),
                    AdvanceRecoveryAmount = table.Column<double>(nullable: true),
                    OfficeCode = table.Column<string>(nullable: true),
                    CurrencyCode = table.Column<string>(nullable: true),
                    PayrollYear = table.Column<int>(nullable: true),
                    PayrollMonth = table.Column<int>(nullable: true),
                    Attendance = table.Column<int>(nullable: true),
                    Absent = table.Column<int>(nullable: true),
                    TotalDuration = table.Column<int>(nullable: true),
                    BasicPay = table.Column<float>(nullable: true),
                    AdvanceId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePaymentTypes", x => x.EmployeePaymentTypesId);
                    table.ForeignKey(
                        name: "FK_EmployeePaymentTypes_Advances_AdvanceId",
                        column: x => x.AdvanceId,
                        principalTable: "Advances",
                        principalColumn: "AdvancesId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeePaymentTypes_EmployeeDetail_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HRJobInterviewers",
                columns: table => new
                {
                    HRJobInterviewerId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    InterviewDetailsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HRJobInterviewers", x => x.HRJobInterviewerId);
                    table.ForeignKey(
                        name: "FK_HRJobInterviewers_EmployeeDetail_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HRJobInterviewers_InterviewDetails_InterviewDetailsId",
                        column: x => x.InterviewDetailsId,
                        principalTable: "InterviewDetails",
                        principalColumn: "InterviewDetailsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InterviewLanguages",
                columns: table => new
                {
                    InterviewLanguagesId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    InterviewDetailsId = table.Column<int>(nullable: false),
                    LanguageName = table.Column<string>(nullable: true),
                    LanguageId = table.Column<int>(nullable: true),
                    Reading = table.Column<int>(nullable: true),
                    Writing = table.Column<int>(nullable: true),
                    Listening = table.Column<int>(nullable: true),
                    Speaking = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterviewLanguages", x => x.InterviewLanguagesId);
                    table.ForeignKey(
                        name: "FK_InterviewLanguages_InterviewDetails_InterviewDetailsId",
                        column: x => x.InterviewDetailsId,
                        principalTable: "InterviewDetails",
                        principalColumn: "InterviewDetailsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InterviewTechnicalQuestion",
                columns: table => new
                {
                    InterviewTechnicalQuestionId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    InterviewDetailsId = table.Column<int>(nullable: false),
                    Question = table.Column<string>(nullable: true),
                    Answer = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterviewTechnicalQuestion", x => x.InterviewTechnicalQuestionId);
                    table.ForeignKey(
                        name: "FK_InterviewTechnicalQuestion_InterviewDetails_InterviewDetail~",
                        column: x => x.InterviewDetailsId,
                        principalTable: "InterviewDetails",
                        principalColumn: "InterviewDetailsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InterviewTrainings",
                columns: table => new
                {
                    InterviewTrainingsId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    InterviewDetailsId = table.Column<int>(nullable: false),
                    TraininigType = table.Column<int>(nullable: false),
                    TrainingName = table.Column<string>(nullable: true),
                    StudyingCountry = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterviewTrainings", x => x.InterviewTrainingsId);
                    table.ForeignKey(
                        name: "FK_InterviewTrainings_InterviewDetails_InterviewDetailsId",
                        column: x => x.InterviewDetailsId,
                        principalTable: "InterviewDetails",
                        principalColumn: "InterviewDetailsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RatingBasedCriteria",
                columns: table => new
                {
                    RatingBasedCriteriaId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    InterviewDetailsId = table.Column<int>(nullable: false),
                    CriteriaQuestion = table.Column<string>(nullable: true),
                    Rating = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingBasedCriteria", x => x.RatingBasedCriteriaId);
                    table.ForeignKey(
                        name: "FK_RatingBasedCriteria_InterviewDetails_InterviewDetailsId",
                        column: x => x.InterviewDetailsId,
                        principalTable: "InterviewDetails",
                        principalColumn: "InterviewDetailsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectActivityDetail",
                columns: table => new
                {
                    ActivityId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ActivityName = table.Column<string>(maxLength: 200, nullable: true),
                    ActivityDescription = table.Column<string>(nullable: true),
                    ChallengesAndSolutions = table.Column<string>(nullable: true),
                    PlannedStartDate = table.Column<DateTime>(nullable: true),
                    PlannedEndDate = table.Column<DateTime>(nullable: true),
                    BudgetLineId = table.Column<long>(nullable: true),
                    EmployeeID = table.Column<int>(nullable: true),
                    StatusId = table.Column<int>(nullable: true),
                    Recurring = table.Column<bool>(nullable: true),
                    RecurringCount = table.Column<int>(nullable: true),
                    RecurrinTypeId = table.Column<int>(nullable: false),
                    ActualStartDate = table.Column<DateTime>(nullable: true),
                    ActualEndDate = table.Column<DateTime>(nullable: true),
                    IsCompleted = table.Column<bool>(nullable: false),
                    ParentId = table.Column<long>(nullable: true),
                    Target = table.Column<float>(nullable: true),
                    Achieved = table.Column<float>(nullable: true),
                    SubActivityTitle = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectActivityDetail", x => x.ActivityId);
                    table.ForeignKey(
                        name: "FK_ProjectActivityDetail_ProjectBudgetLineDetail_BudgetLineId",
                        column: x => x.BudgetLineId,
                        principalTable: "ProjectBudgetLineDetail",
                        principalColumn: "BudgetLineId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectActivityDetail_EmployeeDetail_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectActivityDetail_ProjectActivityDetail_ParentId",
                        column: x => x.ParentId,
                        principalTable: "ProjectActivityDetail",
                        principalColumn: "ActivityId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectActivityDetail_ActivityStatusDetail_StatusId",
                        column: x => x.StatusId,
                        principalTable: "ActivityStatusDetail",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectHiringRequestDetail",
                columns: table => new
                {
                    HiringRequestId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    HiringRequestCode = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ProfessionId = table.Column<int>(nullable: true),
                    Position = table.Column<string>(nullable: true),
                    TotalVacancies = table.Column<int>(nullable: true),
                    FilledVacancies = table.Column<int>(nullable: true),
                    BasicPay = table.Column<double>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: true),
                    BudgetLineId = table.Column<long>(nullable: true),
                    OfficeId = table.Column<int>(nullable: true),
                    GradeId = table.Column<int>(nullable: true),
                    EmployeeID = table.Column<int>(nullable: true),
                    ProjectId = table.Column<long>(nullable: true),
                    IsCompleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectHiringRequestDetail", x => x.HiringRequestId);
                    table.ForeignKey(
                        name: "FK_ProjectHiringRequestDetail_ProjectBudgetLineDetail_BudgetLi~",
                        column: x => x.BudgetLineId,
                        principalTable: "ProjectBudgetLineDetail",
                        principalColumn: "BudgetLineId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectHiringRequestDetail_CurrencyDetails_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "CurrencyDetails",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectHiringRequestDetail_EmployeeDetail_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectHiringRequestDetail_JobGrade_GradeId",
                        column: x => x.GradeId,
                        principalTable: "JobGrade",
                        principalColumn: "GradeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectHiringRequestDetail_OfficeDetail_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "OfficeDetail",
                        principalColumn: "OfficeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectHiringRequestDetail_ProfessionDetails_ProfessionId",
                        column: x => x.ProfessionId,
                        principalTable: "ProfessionDetails",
                        principalColumn: "ProfessionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectHiringRequestDetail_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VoucherDetail",
                columns: table => new
                {
                    VoucherNo = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CurrencyId = table.Column<int>(nullable: true),
                    VoucherDate = table.Column<DateTime>(nullable: false),
                    ChequeNo = table.Column<string>(maxLength: 10, nullable: true),
                    ReferenceNo = table.Column<string>(maxLength: 20, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    JournalCode = table.Column<int>(nullable: true),
                    VoucherTypeId = table.Column<int>(nullable: true),
                    OfficeId = table.Column<int>(nullable: true),
                    ProjectId = table.Column<long>(nullable: true),
                    BudgetLineId = table.Column<long>(nullable: true),
                    FinancialYearId = table.Column<int>(nullable: true),
                    CurrencyCode = table.Column<string>(nullable: true),
                    VoucherType = table.Column<string>(nullable: true),
                    VoucherMode = table.Column<string>(nullable: true),
                    OfficeCode = table.Column<string>(nullable: true),
                    IsExchangeGainLossVoucher = table.Column<bool>(nullable: false),
                    IsVoucherVerified = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoucherDetail", x => x.VoucherNo);
                    table.ForeignKey(
                        name: "FK_VoucherDetail_ProjectBudgetLineDetail_BudgetLineId",
                        column: x => x.BudgetLineId,
                        principalTable: "ProjectBudgetLineDetail",
                        principalColumn: "BudgetLineId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VoucherDetail_CurrencyDetails_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "CurrencyDetails",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VoucherDetail_FinancialYearDetail_FinancialYearId",
                        column: x => x.FinancialYearId,
                        principalTable: "FinancialYearDetail",
                        principalColumn: "FinancialYearId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VoucherDetail_JournalDetail_JournalCode",
                        column: x => x.JournalCode,
                        principalTable: "JournalDetail",
                        principalColumn: "JournalCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VoucherDetail_OfficeDetail_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "OfficeDetail",
                        principalColumn: "OfficeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VoucherDetail_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VoucherDetail_VoucherType_VoucherTypeId",
                        column: x => x.VoucherTypeId,
                        principalTable: "VoucherType",
                        principalColumn: "VoucherTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceApproval",
                columns: table => new
                {
                    InvoiceApprovalId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    JobId = table.Column<long>(nullable: true),
                    IsInvoiceApproved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceApproval", x => x.InvoiceApprovalId);
                    table.ForeignKey(
                        name: "FK_InvoiceApproval_JobDetails_JobId",
                        column: x => x.JobId,
                        principalTable: "JobDetails",
                        principalColumn: "JobId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceGeneration",
                columns: table => new
                {
                    InvoiceId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    JobId = table.Column<long>(nullable: true),
                    PlayoutMinutes = table.Column<long>(nullable: true),
                    TotalMinutes = table.Column<long>(nullable: true),
                    TotalPrice = table.Column<double>(nullable: true),
                    JobPrice = table.Column<long>(nullable: true),
                    CurrencyId = table.Column<long>(nullable: true),
                    CurrencyDetailsCurrencyId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceGeneration", x => x.InvoiceId);
                    table.ForeignKey(
                        name: "FK_InvoiceGeneration_CurrencyDetails_CurrencyDetailsCurrencyId",
                        column: x => x.CurrencyDetailsCurrencyId,
                        principalTable: "CurrencyDetails",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvoiceGeneration_JobDetails_JobId",
                        column: x => x.JobId,
                        principalTable: "JobDetails",
                        principalColumn: "JobId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JobPriceDetails",
                columns: table => new
                {
                    JobPriceId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UnitRate = table.Column<double>(nullable: false),
                    Units = table.Column<int>(nullable: false),
                    FinalRate = table.Column<double>(nullable: false),
                    FinalPrice = table.Column<double>(nullable: false),
                    Discount = table.Column<double>(nullable: false),
                    DiscountPercent = table.Column<float>(nullable: false),
                    TotalPrice = table.Column<double>(nullable: false),
                    IsInvoiceApproved = table.Column<bool>(nullable: false),
                    JobId = table.Column<long>(nullable: false),
                    Minutes = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobPriceDetails", x => x.JobPriceId);
                    table.ForeignKey(
                        name: "FK_JobPriceDetails_JobDetails_JobId",
                        column: x => x.JobId,
                        principalTable: "JobDetails",
                        principalColumn: "JobId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleDetails",
                columns: table => new
                {
                    ScheduleId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ScheduleType = table.Column<string>(nullable: true),
                    ScheduleName = table.Column<string>(nullable: true),
                    ScheduleCode = table.Column<string>(nullable: true),
                    PolicyId = table.Column<long>(nullable: true),
                    ProjectId = table.Column<long>(nullable: true),
                    JobId = table.Column<long>(nullable: true),
                    MediumId = table.Column<long>(nullable: true),
                    ChannelId = table.Column<long>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    StartTime = table.Column<TimeSpan>(nullable: false),
                    EndTime = table.Column<TimeSpan>(nullable: false),
                    Monday = table.Column<bool>(nullable: false),
                    Tuesday = table.Column<bool>(nullable: false),
                    Wednesday = table.Column<bool>(nullable: false),
                    Thursday = table.Column<bool>(nullable: false),
                    Friday = table.Column<bool>(nullable: false),
                    Saturday = table.Column<bool>(nullable: false),
                    Sunday = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleDetails", x => x.ScheduleId);
                    table.ForeignKey(
                        name: "FK_ScheduleDetails_Channel_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "Channel",
                        principalColumn: "ChannelId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScheduleDetails_JobDetails_JobId",
                        column: x => x.JobId,
                        principalTable: "JobDetails",
                        principalColumn: "JobId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScheduleDetails_Mediums_MediumId",
                        column: x => x.MediumId,
                        principalTable: "Mediums",
                        principalColumn: "MediumId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScheduleDetails_PolicyDetails_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "PolicyDetails",
                        principalColumn: "PolicyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ScheduleDetails_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InventoryItems",
                columns: table => new
                {
                    ItemId = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemInventory = table.Column<string>(nullable: true),
                    ItemName = table.Column<string>(nullable: true),
                    ItemCode = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ItemGroupId = table.Column<long>(nullable: true),
                    ItemType = table.Column<int>(nullable: false),
                    MasterInventoryCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryItems", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_InventoryItems_StoreItemGroups_ItemGroupId",
                        column: x => x.ItemGroupId,
                        principalTable: "StoreItemGroups",
                        principalColumn: "ItemGroupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryItems_StoreInventories_ItemInventory",
                        column: x => x.ItemInventory,
                        principalTable: "StoreInventories",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryItems_InventoryItemType_ItemType",
                        column: x => x.ItemType,
                        principalTable: "InventoryItemType",
                        principalColumn: "ItemType",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActivityDocumentsDetail",
                columns: table => new
                {
                    ActtivityDocumentId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ActivityDocumentsFilePath = table.Column<string>(nullable: true),
                    StatusId = table.Column<int>(nullable: true),
                    ActivityId = table.Column<long>(nullable: false),
                    MonitoringId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityDocumentsDetail", x => x.ActtivityDocumentId);
                    table.ForeignKey(
                        name: "FK_ActivityDocumentsDetail_ProjectActivityDetail_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "ProjectActivityDetail",
                        principalColumn: "ActivityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivityDocumentsDetail_ActivityStatusDetail_StatusId",
                        column: x => x.StatusId,
                        principalTable: "ActivityStatusDetail",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectActivityExtensions",
                columns: table => new
                {
                    ExtensionId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ActivityId = table.Column<long>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectActivityExtensions", x => x.ExtensionId);
                    table.ForeignKey(
                        name: "FK_ProjectActivityExtensions_ProjectActivityDetail_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "ProjectActivityDetail",
                        principalColumn: "ActivityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectActivityProvinceDetail",
                columns: table => new
                {
                    ActivityProvinceId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ActivityId = table.Column<long>(nullable: false),
                    ProvinceId = table.Column<int>(nullable: false),
                    DistrictID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectActivityProvinceDetail", x => x.ActivityProvinceId);
                    table.ForeignKey(
                        name: "FK_ProjectActivityProvinceDetail_ProjectActivityDetail_Activit~",
                        column: x => x.ActivityId,
                        principalTable: "ProjectActivityDetail",
                        principalColumn: "ActivityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectActivityProvinceDetail_DistrictDetail_DistrictID",
                        column: x => x.DistrictID,
                        principalTable: "DistrictDetail",
                        principalColumn: "DistrictID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectActivityProvinceDetail_ProvinceDetails_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "ProvinceDetails",
                        principalColumn: "ProvinceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeSalaryAnalyticalInfo",
                columns: table => new
                {
                    EmployeeSalaryAnalyticalInfoId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    AccountCode = table.Column<int>(nullable: true),
                    ProjectId = table.Column<long>(nullable: false),
                    BudgetlineId = table.Column<long>(nullable: false),
                    SalaryPercentage = table.Column<double>(nullable: false),
                    EmployeeID = table.Column<int>(nullable: false),
                    HiringRequestId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeSalaryAnalyticalInfo", x => x.EmployeeSalaryAnalyticalInfoId);
                    table.ForeignKey(
                        name: "FK_EmployeeSalaryAnalyticalInfo_ProjectBudgetLineDetail_Budget~",
                        column: x => x.BudgetlineId,
                        principalTable: "ProjectBudgetLineDetail",
                        principalColumn: "BudgetLineId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeSalaryAnalyticalInfo_EmployeeDetail_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeSalaryAnalyticalInfo_ProjectHiringRequestDetail_Hir~",
                        column: x => x.HiringRequestId,
                        principalTable: "ProjectHiringRequestDetail",
                        principalColumn: "HiringRequestId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeSalaryAnalyticalInfo_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HiringRequestCandidates",
                columns: table => new
                {
                    CandidateId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    HiringRequestId = table.Column<long>(nullable: false),
                    EmployeeID = table.Column<int>(nullable: true),
                    IsShortListed = table.Column<bool>(nullable: false),
                    IsSelected = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HiringRequestCandidates", x => x.CandidateId);
                    table.ForeignKey(
                        name: "FK_HiringRequestCandidates_EmployeeDetail_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HiringRequestCandidates_ProjectHiringRequestDetail_HiringRe~",
                        column: x => x.HiringRequestId,
                        principalTable: "ProjectHiringRequestDetail",
                        principalColumn: "HiringRequestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobHiringDetails",
                columns: table => new
                {
                    JobId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    JobCode = table.Column<string>(maxLength: 50, nullable: true),
                    JobDescription = table.Column<string>(nullable: true),
                    ProfessionId = table.Column<int>(nullable: true),
                    Unit = table.Column<int>(nullable: false),
                    OfficeId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    GradeId = table.Column<int>(nullable: true),
                    HiringRequestId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobHiringDetails", x => x.JobId);
                    table.ForeignKey(
                        name: "FK_JobHiringDetails_JobGrade_GradeId",
                        column: x => x.GradeId,
                        principalTable: "JobGrade",
                        principalColumn: "GradeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobHiringDetails_ProjectHiringRequestDetail_HiringRequestId",
                        column: x => x.HiringRequestId,
                        principalTable: "ProjectHiringRequestDetail",
                        principalColumn: "HiringRequestId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobHiringDetails_OfficeDetail_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "OfficeDetail",
                        principalColumn: "OfficeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeSalaryPaymentHistory",
                columns: table => new
                {
                    SalaryPaymentId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    Year = table.Column<int>(nullable: false),
                    Month = table.Column<int>(nullable: false),
                    VoucherNo = table.Column<long>(nullable: false),
                    IsSalaryReverse = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeSalaryPaymentHistory", x => x.SalaryPaymentId);
                    table.ForeignKey(
                        name: "FK_EmployeeSalaryPaymentHistory_EmployeeDetail_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeSalaryPaymentHistory_VoucherDetail_VoucherNo",
                        column: x => x.VoucherNo,
                        principalTable: "VoucherDetail",
                        principalColumn: "VoucherNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PensionPaymentHistory",
                columns: table => new
                {
                    PensionPaymentId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    PaymentDate = table.Column<DateTime>(nullable: false),
                    PaymentAmount = table.Column<decimal>(nullable: false),
                    VoucherReferenceNo = table.Column<string>(nullable: true),
                    VoucherNo = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PensionPaymentHistory", x => x.PensionPaymentId);
                    table.ForeignKey(
                        name: "FK_PensionPaymentHistory_EmployeeDetail_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PensionPaymentHistory_VoucherDetail_VoucherNo",
                        column: x => x.VoucherNo,
                        principalTable: "VoucherDetail",
                        principalColumn: "VoucherNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VoucherTransactions",
                columns: table => new
                {
                    TransactionId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    VoucherNo = table.Column<long>(nullable: true),
                    CreditAccount = table.Column<long>(nullable: true),
                    DebitAccount = table.Column<long>(nullable: true),
                    Amount = table.Column<double>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    TransactionDate = table.Column<DateTime>(nullable: true),
                    ChartOfAccountNewId = table.Column<long>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: true),
                    OfficeId = table.Column<int>(nullable: true),
                    FinancialYearId = table.Column<int>(nullable: true),
                    Donor = table.Column<string>(nullable: true),
                    Area = table.Column<string>(nullable: true),
                    Sector = table.Column<string>(nullable: true),
                    Program = table.Column<string>(nullable: true),
                    Project = table.Column<string>(nullable: true),
                    Job = table.Column<string>(nullable: true),
                    CostBook = table.Column<string>(nullable: true),
                    Debit = table.Column<double>(nullable: true),
                    Credit = table.Column<double>(nullable: true),
                    AFGAmount = table.Column<double>(nullable: true),
                    EURAmount = table.Column<double>(nullable: true),
                    USDAmount = table.Column<double>(nullable: true),
                    PKRAmount = table.Column<double>(nullable: true),
                    ProjectId = table.Column<long>(nullable: true),
                    BudgetLineId = table.Column<long>(nullable: true),
                    JobId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoucherTransactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_VoucherTransactions_ProjectBudgetLineDetail_BudgetLineId",
                        column: x => x.BudgetLineId,
                        principalTable: "ProjectBudgetLineDetail",
                        principalColumn: "BudgetLineId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VoucherTransactions_ChartOfAccountNew_ChartOfAccountNewId",
                        column: x => x.ChartOfAccountNewId,
                        principalTable: "ChartOfAccountNew",
                        principalColumn: "ChartOfAccountNewId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VoucherTransactions_CurrencyDetails_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "CurrencyDetails",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VoucherTransactions_ProjectJobDetail_JobId",
                        column: x => x.JobId,
                        principalTable: "ProjectJobDetail",
                        principalColumn: "ProjectJobId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VoucherTransactions_OfficeDetail_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "OfficeDetail",
                        principalColumn: "OfficeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VoucherTransactions_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VoucherTransactions_VoucherDetail_VoucherNo",
                        column: x => x.VoucherNo,
                        principalTable: "VoucherDetail",
                        principalColumn: "VoucherNo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PlayoutMinutes",
                columns: table => new
                {
                    PlayoutMinuteId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ScheduleId = table.Column<long>(nullable: true),
                    TotalMinutes = table.Column<long>(nullable: true),
                    DroppedMinutes = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayoutMinutes", x => x.PlayoutMinuteId);
                    table.ForeignKey(
                        name: "FK_PlayoutMinutes_ScheduleDetails_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "ScheduleDetails",
                        principalColumn: "ScheduleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StoreItemPurchases",
                columns: table => new
                {
                    PurchaseId = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    SerialNo = table.Column<string>(nullable: false),
                    InventoryItem = table.Column<string>(nullable: false),
                    PurchaseDate = table.Column<DateTime>(nullable: false),
                    DeliveryDate = table.Column<DateTime>(nullable: false),
                    Currency = table.Column<int>(nullable: false),
                    UnitType = table.Column<int>(nullable: false),
                    UnitCost = table.Column<long>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    ApplyDepreciation = table.Column<bool>(nullable: false),
                    DepreciationRate = table.Column<double>(nullable: false),
                    ImageFileType = table.Column<string>(nullable: true),
                    ImageFileName = table.Column<string>(nullable: true),
                    InvoiceFileType = table.Column<string>(nullable: true),
                    InvoiceFileName = table.Column<string>(nullable: true),
                    PurchasedById = table.Column<int>(nullable: false),
                    VoucherId = table.Column<long>(nullable: true),
                    VoucherDate = table.Column<DateTime>(nullable: false),
                    AssetTypeId = table.Column<int>(nullable: true),
                    InvoiceNo = table.Column<string>(nullable: true),
                    InvoiceDate = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: true),
                    ReceiptTypeId = table.Column<int>(nullable: true),
                    ReceivedFromLocation = table.Column<string>(nullable: true),
                    ProjectId = table.Column<long>(nullable: true),
                    BudgetLineId = table.Column<long>(nullable: true),
                    PaymentTypeId = table.Column<int>(nullable: true),
                    IsPurchaseVerified = table.Column<bool>(nullable: false),
                    VerifiedPurchaseVoucher = table.Column<long>(nullable: true),
                    OfficeId = table.Column<int>(nullable: true),
                    JournalCode = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreItemPurchases", x => x.PurchaseId);
                    table.ForeignKey(
                        name: "FK_StoreItemPurchases_CurrencyDetails_Currency",
                        column: x => x.Currency,
                        principalTable: "CurrencyDetails",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreItemPurchases_InventoryItems_InventoryItem",
                        column: x => x.InventoryItem,
                        principalTable: "InventoryItems",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreItemPurchases_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreItemPurchases_EmployeeDetail_PurchasedById",
                        column: x => x.PurchasedById,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreItemPurchases_ReceiptType_ReceiptTypeId",
                        column: x => x.ReceiptTypeId,
                        principalTable: "ReceiptType",
                        principalColumn: "ReceiptTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreItemPurchases_StatusAtTimeOfIssue_Status",
                        column: x => x.Status,
                        principalTable: "StatusAtTimeOfIssue",
                        principalColumn: "StatusAtTimeOfIssueId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreItemPurchases_PurchaseUnitType_UnitType",
                        column: x => x.UnitType,
                        principalTable: "PurchaseUnitType",
                        principalColumn: "UnitTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreItemPurchases_VoucherDetail_VoucherId",
                        column: x => x.VoucherId,
                        principalTable: "VoucherDetail",
                        principalColumn: "VoucherNo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InterviewScheduleDetails",
                columns: table => new
                {
                    ScheduleId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    JobId = table.Column<long>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    InterviewStatus = table.Column<int>(maxLength: 50, nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Approval1 = table.Column<bool>(nullable: true),
                    Approval2 = table.Column<bool>(nullable: true),
                    Approval3 = table.Column<bool>(nullable: true),
                    Approval4 = table.Column<bool>(nullable: true),
                    GradeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterviewScheduleDetails", x => x.ScheduleId);
                    table.ForeignKey(
                        name: "FK_InterviewScheduleDetails_EmployeeDetail_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InterviewScheduleDetails_JobGrade_GradeId",
                        column: x => x.GradeId,
                        principalTable: "JobGrade",
                        principalColumn: "GradeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InterviewScheduleDetails_JobHiringDetails_JobId",
                        column: x => x.JobId,
                        principalTable: "JobHiringDetails",
                        principalColumn: "JobId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StorePurchaseOrders",
                columns: table => new
                {
                    OrderId = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Purchase = table.Column<string>(nullable: true),
                    InventoryItem = table.Column<string>(nullable: true),
                    IssuedQuantity = table.Column<int>(nullable: false),
                    MustReturn = table.Column<bool>(nullable: false),
                    Returned = table.Column<bool>(nullable: false),
                    IssuedToEmployeeId = table.Column<int>(nullable: false),
                    IssueDate = table.Column<DateTime>(nullable: false),
                    ReturnedDate = table.Column<DateTime>(nullable: true),
                    IssueVoucherNo = table.Column<string>(nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    Project = table.Column<long>(nullable: false),
                    IssedToLocation = table.Column<string>(nullable: true),
                    StatusAtTimeOfIssue = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StorePurchaseOrders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_StorePurchaseOrders_InventoryItems_InventoryItem",
                        column: x => x.InventoryItem,
                        principalTable: "InventoryItems",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StorePurchaseOrders_EmployeeDetail_IssuedToEmployeeId",
                        column: x => x.IssuedToEmployeeId,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StorePurchaseOrders_StoreItemPurchases_Purchase",
                        column: x => x.Purchase,
                        principalTable: "StoreItemPurchases",
                        principalColumn: "PurchaseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "ApplicationPages",
                columns: new[] { "PageId", "CreatedById", "CreatedDate", "IsDeleted", "ModifiedById", "ModifiedDate", "ModuleId", "ModuleName", "PageName" },
                values: new object[,]
                {
                    { 1, null, null, false, null, null, 1, "Users", "Users" },
                    { 64, null, null, false, null, null, 6, "Marketing", "UnitRates" },
                    { 63, null, null, false, null, null, 6, "Marketing", "Clients" },
                    { 62, null, null, false, null, null, 7, "AccountingNew", "Vouchers" },
                    { 61, null, null, false, null, null, 7, "AccountingNew", "IncomeExpenseReport" },
                    { 60, null, null, false, null, null, 7, "AccountingNew", "BalanceSheet" },
                    { 59, null, null, false, null, null, 7, "AccountingNew", "Expense" },
                    { 58, null, null, false, null, null, 7, "AccountingNew", "Income" },
                    { 57, null, null, false, null, null, 7, "AccountingNew", "Liabilities" },
                    { 65, null, null, false, null, null, 6, "Marketing", "Jobs" },
                    { 56, null, null, false, null, null, 7, "AccountingNew", "Assets" },
                    { 54, null, null, false, null, null, 6, "Marketing", "MediaCategory" },
                    { 53, null, null, false, null, null, 6, "Marketing", "Medium" },
                    { 52, null, null, false, null, null, 6, "Marketing", "Nature" },
                    { 51, null, null, false, null, null, 6, "Marketing", "Phase" },
                    { 50, null, null, false, null, null, 6, "Marketing", "Quality" },
                    { 49, null, null, false, null, null, 6, "Marketing", "TimeCategory" },
                    { 48, null, null, false, null, null, 5, "Store", "DepreciationReport" },
                    { 47, null, null, false, null, null, 5, "Store", "ProcurementSummary" },
                    { 55, null, null, false, null, null, 6, "Marketing", "ActivityType" },
                    { 46, null, null, false, null, null, 5, "Store", "Store" },
                    { 66, null, null, false, null, null, 6, "Marketing", "Contracts" },
                    { 68, null, null, false, null, null, 8, "Projects", "Donors" },
                    { 86, null, null, false, null, null, 8, "Projects", "HiringRequests" },
                    { 85, null, null, false, null, null, 7, "AccountingNew", "VoucherSummaryReport" },
                    { 84, null, null, false, null, null, 8, "Projects", "ProjectPeople" },
                    { 83, null, null, false, null, null, 8, "Projects", "ProjectIndicators" },
                    { 82, null, null, false, null, null, 8, "Projects", "ProposalReport" },
                    { 81, null, null, false, null, null, 8, "Projects", "BroadCastPolicy" },
                    { 80, null, null, false, null, null, 8, "Projects", "ProjectBudgetLine" },
                    { 79, null, null, false, null, null, 8, "Projects", "ProjectCashFlow" },
                    { 67, null, null, false, null, null, 8, "Projects", "MyProjects" },
                    { 78, null, null, false, null, null, 8, "Projects", "ProjectDashboard" },
                    { 76, null, null, false, null, null, 6, "Marketing", "Channel" },
                    { 75, null, null, false, null, null, 8, "Projects", "ProjectActivities" },
                    { 74, null, null, false, null, null, 8, "Projects", "ProjectJobs" },
                    { 73, null, null, false, null, null, 6, "Marketing", "Policy" },
                    { 72, null, null, false, null, null, 6, "Marketing", "Producer" },
                    { 71, null, null, false, null, null, 8, "Projects", "CriteriaEvaluation" },
                    { 70, null, null, false, null, null, 8, "Projects", "Proposal" },
                    { 69, null, null, false, null, null, 8, "Projects", "ProjectDetails" },
                    { 77, null, null, false, null, null, 6, "Marketing", "Scheduler" },
                    { 45, null, null, false, null, null, 5, "Store", "PaymentTypes" },
                    { 44, null, null, false, null, null, 5, "Store", "StoreSourceCodes" },
                    { 43, null, null, false, null, null, 5, "Store", "Categories" },
                    { 19, null, null, false, null, null, 2, "Code", "SalaryHead" },
                    { 18, null, null, false, null, null, 2, "Code", "JobGrade" },
                    { 17, null, null, false, null, null, 2, "Code", "Designation" },
                    { 16, null, null, false, null, null, 2, "Code", "Qualification" },
                    { 15, null, null, false, null, null, 2, "Code", "Department" },
                    { 14, null, null, false, null, null, 2, "Code", "Profession" },
                    { 13, null, null, false, null, null, 2, "Code", "LeaveReason" },
                    { 12, null, null, false, null, null, 2, "Code", "ExchangeRate" },
                    { 20, null, null, false, null, null, 2, "Code", "SalaryTaxReportContent" },
                    { 11, null, null, false, null, null, 2, "Code", "EmailSettings" },
                    { 9, null, null, false, null, null, 2, "Code", "AppraisalQuestions" },
                    { 8, null, null, false, null, null, 2, "Code", "EmployeeContract" },
                    { 7, null, null, false, null, null, 2, "Code", "PensionRate" },
                    { 6, null, null, false, null, null, 2, "Code", "FinancialYear" },
                    { 5, null, null, false, null, null, 2, "Code", "OfficeCodes" },
                    { 4, null, null, false, null, null, 2, "Code", "CurrencyCodes" },
                    { 3, null, null, false, null, null, 2, "Code", "JournalCodes" },
                    { 2, null, null, false, null, null, 2, "Code", "ChartOfAccount" },
                    { 10, null, null, false, null, null, 2, "Code", "TechnicalQuestions" },
                    { 21, null, null, false, null, null, 2, "Code", "SetPayrollAccount" },
                    { 22, null, null, true, null, null, 3, "Accounting", "Vouchers" },
                    { 23, null, null, false, null, null, 3, "Accounting", "Journal" },
                    { 42, null, null, false, null, null, 4, "HR", "Summary" },
                    { 41, null, null, false, null, null, 4, "HR", "Advances" },
                    { 40, null, null, false, null, null, 4, "HR", "EmployeeAppraisal" },
                    { 39, null, null, false, null, null, 4, "HR", "Interview" },
                    { 38, null, null, false, null, null, 4, "HR", "Jobs" },
                    { 37, null, null, false, null, null, 4, "HR", "MonthlyPayrollRegister" },
                    { 36, null, null, false, null, null, 4, "HR", "ApproveLeave" },
                    { 35, null, null, false, null, null, 4, "HR", "Attendance" },
                    { 34, null, null, false, null, null, 4, "HR", "Holidays" },
                    { 33, null, null, false, null, null, 4, "HR", "PayrollDailyHours" },
                    { 32, null, null, false, null, null, 4, "HR", "Employees" },
                    { 31, null, null, false, null, null, 3, "Accounting", "PensionPayments" },
                    { 30, null, null, false, null, null, 3, "Accounting", "GainLossTransaction" },
                    { 29, null, null, false, null, null, 3, "Accounting", "ExchangeGainLoss" },
                    { 28, null, null, true, null, null, 3, "Accounting", "CategoryPopulator" },
                    { 27, null, null, false, null, null, 3, "Accounting", "FinancialReport" },
                    { 26, null, null, false, null, null, 3, "Accounting", "TrialBalance" },
                    { 25, null, null, false, null, null, 3, "Accounting", "BudgetBalance" },
                    { 24, null, null, false, null, null, 3, "Accounting", "LedgerStatement" },
                    { 87, null, null, false, null, null, 2, "Code", "PensionDebitAccount" },
                    { 88, null, null, false, null, null, 2, "Code", "AttendanceGroupMaster" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountType_AccountHeadTypeId",
                table: "AccountType",
                column: "AccountHeadTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityDocumentsDetail_ActivityId",
                table: "ActivityDocumentsDetail",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityDocumentsDetail_StatusId",
                table: "ActivityDocumentsDetail",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Advances_CurrencyId",
                table: "Advances",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Advances_EmployeeId",
                table: "Advances",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_AgreeDisagreePermission_PageId",
                table: "AgreeDisagreePermission",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_ApproveProjectDetails_ProjectId",
                table: "ApproveProjectDetails",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ApproveRejectPermission_PageId",
                table: "ApproveRejectPermission",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssignLeaveToEmployee_EmployeeId",
                table: "AssignLeaveToEmployee",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignLeaveToEmployee_FinancialYearId",
                table: "AssignLeaveToEmployee",
                column: "FinancialYearId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignLeaveToEmployee_LeaveReasonId",
                table: "AssignLeaveToEmployee",
                column: "LeaveReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_CEAgeGroupDetail_ProjectId",
                table: "CEAgeGroupDetail",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_CEAssumptionDetail_ProjectId",
                table: "CEAssumptionDetail",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_CEFeasibilityExpertOtherDetail_ProjectId",
                table: "CEFeasibilityExpertOtherDetail",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Channel_MediumId",
                table: "Channel",
                column: "MediumId");

            migrationBuilder.CreateIndex(
                name: "IX_ChartOfAccountNew_AccountFilterTypeId",
                table: "ChartOfAccountNew",
                column: "AccountFilterTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ChartOfAccountNew_AccountLevelId",
                table: "ChartOfAccountNew",
                column: "AccountLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_ChartOfAccountNew_AccountTypeId",
                table: "ChartOfAccountNew",
                column: "AccountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatDetail_EntitySourceDocumentId",
                table: "ChatDetail",
                column: "EntitySourceDocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientDetails_CategoryId",
                table: "ClientDetails",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractDetails_ActivityTypeId",
                table: "ContractDetails",
                column: "ActivityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractDetails_ClientId",
                table: "ContractDetails",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractDetails_CurrencyId",
                table: "ContractDetails",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractDetails_LanguageDetailLanguageId",
                table: "ContractDetails",
                column: "LanguageDetailLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractDetails_MediaCategoryId",
                table: "ContractDetails",
                column: "MediaCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractDetails_MediumId",
                table: "ContractDetails",
                column: "MediumId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractDetails_NatureId",
                table: "ContractDetails",
                column: "NatureId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractDetails_QualityId",
                table: "ContractDetails",
                column: "QualityId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractDetails_TimeCategoryId",
                table: "ContractDetails",
                column: "TimeCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractDetails_UnitRateId",
                table: "ContractDetails",
                column: "UnitRateId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractTypeContent_EmployeeContractTypeId",
                table: "ContractTypeContent",
                column: "EmployeeContractTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryMultiSelectDetails_CountryId",
                table: "CountryMultiSelectDetails",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CountryMultiSelectDetails_ProjectId",
                table: "CountryMultiSelectDetails",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Department_OfficeId",
                table: "Department",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_DistrictMultiSelect_DistrictID",
                table: "DistrictMultiSelect",
                column: "DistrictID");

            migrationBuilder.CreateIndex(
                name: "IX_DistrictMultiSelect_ProjectId",
                table: "DistrictMultiSelect",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_DistrictMultiSelect_ProvinceId",
                table: "DistrictMultiSelect",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_DonorCriteriaDetail_ProjectId",
                table: "DonorCriteriaDetail",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_DonorEligibilityCriteria_ProjectId",
                table: "DonorEligibilityCriteria",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_EligibilityCriteriaDetail_ProjectId",
                table: "EligibilityCriteriaDetail",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailSettingDetail_EmailTypeId",
                table: "EmailSettingDetail",
                column: "EmailTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeApplyLeave_EmployeeId",
                table: "EmployeeApplyLeave",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeApplyLeave_FinancialYearId",
                table: "EmployeeApplyLeave",
                column: "FinancialYearId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeApplyLeave_LeaveReasonId",
                table: "EmployeeApplyLeave",
                column: "LeaveReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAppraisalDetails_EmployeeId",
                table: "EmployeeAppraisalDetails",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAppraisalQuestions_AppraisalGeneralQuestionsId",
                table: "EmployeeAppraisalQuestions",
                column: "AppraisalGeneralQuestionsId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAttendance_EmployeeId",
                table: "EmployeeAttendance",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAttendance_FinancialYearId",
                table: "EmployeeAttendance",
                column: "FinancialYearId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAttendance_HolidayId",
                table: "EmployeeAttendance",
                column: "HolidayId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAttendance_LeaveReasonId",
                table: "EmployeeAttendance",
                column: "LeaveReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeContract_EmployeeId",
                table: "EmployeeContract",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeContract_Grade",
                table: "EmployeeContract",
                column: "Grade");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetail_CountryId",
                table: "EmployeeDetail",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetail_EmployeeTypeId",
                table: "EmployeeDetail",
                column: "EmployeeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetail_HigherQualificationId",
                table: "EmployeeDetail",
                column: "HigherQualificationId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetail_NationalityId",
                table: "EmployeeDetail",
                column: "NationalityId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetail_ProvinceId",
                table: "EmployeeDetail",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDocumentDetail_EmployeeID",
                table: "EmployeeDocumentDetail",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEducations_EmployeeID",
                table: "EmployeeEducations",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeHealthInfo_EmployeeId",
                table: "EmployeeHealthInfo",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeHistoryDetail_EmployeeID",
                table: "EmployeeHistoryDetail",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeHistoryOutsideCountry_EmployeeID",
                table: "EmployeeHistoryOutsideCountry",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeHistoryOutsideOrganization_EmployeeID",
                table: "EmployeeHistoryOutsideOrganization",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeInfoReferences_EmployeeID",
                table: "EmployeeInfoReferences",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLanguages_LanguageId",
                table: "EmployeeLanguages",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeMonthlyAttendance_AdvanceId",
                table: "EmployeeMonthlyAttendance",
                column: "AdvanceId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeMonthlyAttendance_EmployeeId",
                table: "EmployeeMonthlyAttendance",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeMonthlyPayroll_CurrencyId",
                table: "EmployeeMonthlyPayroll",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeMonthlyPayroll_EmployeeID",
                table: "EmployeeMonthlyPayroll",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeMonthlyPayroll_SalaryHeadId",
                table: "EmployeeMonthlyPayroll",
                column: "SalaryHeadId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeOtherSkills_EmployeeID",
                table: "EmployeeOtherSkills",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePaymentTypes_AdvanceId",
                table: "EmployeePaymentTypes",
                column: "AdvanceId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePaymentTypes_EmployeeID",
                table: "EmployeePaymentTypes",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayroll_CurrencyId",
                table: "EmployeePayroll",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayroll_EmployeeID",
                table: "EmployeePayroll",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayroll_SalaryHeadId",
                table: "EmployeePayroll",
                column: "SalaryHeadId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayrollAccountHead_EmployeeId",
                table: "EmployeePayrollAccountHead",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayrollAccountHead_PayrollHeadId",
                table: "EmployeePayrollAccountHead",
                column: "PayrollHeadId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayrollMonth_CurrencyId",
                table: "EmployeePayrollMonth",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayrollMonth_EmployeeID",
                table: "EmployeePayrollMonth",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayrollMonth_SalaryHeadId",
                table: "EmployeePayrollMonth",
                column: "SalaryHeadId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePensionRate_FinancialYearId",
                table: "EmployeePensionRate",
                column: "FinancialYearId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProfessionalDetail_AttendanceGroupId",
                table: "EmployeeProfessionalDetail",
                column: "AttendanceGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProfessionalDetail_DepartmentId",
                table: "EmployeeProfessionalDetail",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProfessionalDetail_DesignationId",
                table: "EmployeeProfessionalDetail",
                column: "DesignationId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProfessionalDetail_EmployeeContractTypeId",
                table: "EmployeeProfessionalDetail",
                column: "EmployeeContractTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProfessionalDetail_EmployeeId",
                table: "EmployeeProfessionalDetail",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProfessionalDetail_EmployeeTypeId",
                table: "EmployeeProfessionalDetail",
                column: "EmployeeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProfessionalDetail_OfficeId",
                table: "EmployeeProfessionalDetail",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProfessionalDetail_ProfessionId",
                table: "EmployeeProfessionalDetail",
                column: "ProfessionId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRelativeInfo_EmployeeID",
                table: "EmployeeRelativeInfo",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaryAnalyticalInfo_BudgetlineId",
                table: "EmployeeSalaryAnalyticalInfo",
                column: "BudgetlineId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaryAnalyticalInfo_EmployeeID",
                table: "EmployeeSalaryAnalyticalInfo",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaryAnalyticalInfo_HiringRequestId",
                table: "EmployeeSalaryAnalyticalInfo",
                column: "HiringRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaryAnalyticalInfo_ProjectId",
                table: "EmployeeSalaryAnalyticalInfo",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaryBudget_EmployeeID",
                table: "EmployeeSalaryBudget",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaryDetails_CurrencyId",
                table: "EmployeeSalaryDetails",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaryDetails_EmployeeId",
                table: "EmployeeSalaryDetails",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaryPaymentHistory_EmployeeId",
                table: "EmployeeSalaryPaymentHistory",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaryPaymentHistory_VoucherNo",
                table: "EmployeeSalaryPaymentHistory",
                column: "VoucherNo");

            migrationBuilder.CreateIndex(
                name: "IX_EntitySourceDocumentDetails_DocumentFileId",
                table: "EntitySourceDocumentDetails",
                column: "DocumentFileId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRate_Date",
                table: "ExchangeRate",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRate_FromCurrency",
                table: "ExchangeRate",
                column: "FromCurrency");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRate_OfficeId",
                table: "ExchangeRate",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRate_ToCurrency",
                table: "ExchangeRate",
                column: "ToCurrency");

            migrationBuilder.CreateIndex(
                name: "IX_ExistInterviewDetails_EmployeeID",
                table: "ExistInterviewDetails",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_FeasibilityCriteriaDetail_ProjectId",
                table: "FeasibilityCriteriaDetail",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialCriteriaDetail_ProjectId",
                table: "FinancialCriteriaDetail",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialProjectDetail_ProjectId",
                table: "FinancialProjectDetail",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_GainLossSelectedAccounts_ChartOfAccountNewId",
                table: "GainLossSelectedAccounts",
                column: "ChartOfAccountNewId");

            migrationBuilder.CreateIndex(
                name: "IX_HiringRequestCandidates_EmployeeID",
                table: "HiringRequestCandidates",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_HiringRequestCandidates_HiringRequestId",
                table: "HiringRequestCandidates",
                column: "HiringRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_HolidayDetails_FinancialYearId",
                table: "HolidayDetails",
                column: "FinancialYearId");

            migrationBuilder.CreateIndex(
                name: "IX_HolidayDetails_OfficeId",
                table: "HolidayDetails",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_HolidayWeeklyDetails_FinancialYearId",
                table: "HolidayWeeklyDetails",
                column: "FinancialYearId");

            migrationBuilder.CreateIndex(
                name: "IX_HolidayWeeklyDetails_OfficeId",
                table: "HolidayWeeklyDetails",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_HRJobInterviewers_EmployeeId",
                table: "HRJobInterviewers",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_HRJobInterviewers_InterviewDetailsId",
                table: "HRJobInterviewers",
                column: "InterviewDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewDetails_EmployeeID",
                table: "InterviewDetails",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewLanguages_InterviewDetailsId",
                table: "InterviewLanguages",
                column: "InterviewDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewScheduleDetails_EmployeeId",
                table: "InterviewScheduleDetails",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewScheduleDetails_GradeId",
                table: "InterviewScheduleDetails",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewScheduleDetails_JobId",
                table: "InterviewScheduleDetails",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewTechnicalQuestion_InterviewDetailsId",
                table: "InterviewTechnicalQuestion",
                column: "InterviewDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewTrainings_InterviewDetailsId",
                table: "InterviewTrainings",
                column: "InterviewDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_ItemGroupId",
                table: "InventoryItems",
                column: "ItemGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_ItemInventory",
                table: "InventoryItems",
                column: "ItemInventory");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_ItemType",
                table: "InventoryItems",
                column: "ItemType");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceApproval_JobId",
                table: "InvoiceApproval",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceGeneration_CurrencyDetailsCurrencyId",
                table: "InvoiceGeneration",
                column: "CurrencyDetailsCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceGeneration_JobId",
                table: "InvoiceGeneration",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSpecificationDetails_ItemSpecificationMasterId",
                table: "ItemSpecificationDetails",
                column: "ItemSpecificationMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSpecificationMaster_ItemTypeId",
                table: "ItemSpecificationMaster",
                column: "ItemTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSpecificationMaster_OfficeId",
                table: "ItemSpecificationMaster",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_JobDetails_ContractId",
                table: "JobDetails",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_JobDetails_JobPhaseId",
                table: "JobDetails",
                column: "JobPhaseId");

            migrationBuilder.CreateIndex(
                name: "IX_JobHiringDetails_GradeId",
                table: "JobHiringDetails",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_JobHiringDetails_HiringRequestId",
                table: "JobHiringDetails",
                column: "HiringRequestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobHiringDetails_OfficeId",
                table: "JobHiringDetails",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_JobPriceDetails_JobId",
                table: "JobPriceDetails",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderSchedulePermission_PageId",
                table: "OrderSchedulePermission",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollMonthlyHourDetail_AttendanceGroupId",
                table: "PayrollMonthlyHourDetail",
                column: "AttendanceGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollMonthlyHourDetail_OfficeId",
                table: "PayrollMonthlyHourDetail",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_PensionDebitAccountMaster_ChartOfAccountNewId",
                table: "PensionDebitAccountMaster",
                column: "ChartOfAccountNewId");

            migrationBuilder.CreateIndex(
                name: "IX_PensionPaymentHistory_EmployeeId",
                table: "PensionPaymentHistory",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PensionPaymentHistory_VoucherNo",
                table: "PensionPaymentHistory",
                column: "VoucherNo");

            migrationBuilder.CreateIndex(
                name: "IX_PlayoutMinutes_ScheduleId",
                table: "PlayoutMinutes",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyDaySchedules_PolicyId",
                table: "PolicyDaySchedules",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyDetails_LanguagesLanguageId",
                table: "PolicyDetails",
                column: "LanguagesLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyDetails_MediaCategoryId",
                table: "PolicyDetails",
                column: "MediaCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyDetails_MediumId",
                table: "PolicyDetails",
                column: "MediumId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyDetails_ProducerId",
                table: "PolicyDetails",
                column: "ProducerId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyOrderSchedules_PolicyId",
                table: "PolicyOrderSchedules",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicySchedules_PolicyId",
                table: "PolicySchedules",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyTimeSchedules_PolicyId",
                table: "PolicyTimeSchedules",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_PriorityCriteriaDetail_ProjectId",
                table: "PriorityCriteriaDetail",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_PriorityOtherDetail_ProjectId",
                table: "PriorityOtherDetail",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivitiesControl_ProjectId",
                table: "ProjectActivitiesControl",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivitiesControl_UserID",
                table: "ProjectActivitiesControl",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivityDetail_BudgetLineId",
                table: "ProjectActivityDetail",
                column: "BudgetLineId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivityDetail_EmployeeID",
                table: "ProjectActivityDetail",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivityDetail_ParentId",
                table: "ProjectActivityDetail",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivityDetail_StatusId",
                table: "ProjectActivityDetail",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivityExtensions_ActivityId",
                table: "ProjectActivityExtensions",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivityProvinceDetail_ActivityId",
                table: "ProjectActivityProvinceDetail",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivityProvinceDetail_DistrictID",
                table: "ProjectActivityProvinceDetail",
                column: "DistrictID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivityProvinceDetail_ProvinceId",
                table: "ProjectActivityProvinceDetail",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectArea_AreaId",
                table: "ProjectArea",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectArea_ProjectId",
                table: "ProjectArea",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectAssignTo_EmployeeId",
                table: "ProjectAssignTo",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectAssignTo_ProjectId",
                table: "ProjectAssignTo",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectBudgetLineDetail_CurrencyId",
                table: "ProjectBudgetLineDetail",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectBudgetLineDetail_ProjectId",
                table: "ProjectBudgetLineDetail",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectBudgetLineDetail_ProjectJobId",
                table: "ProjectBudgetLineDetail",
                column: "ProjectJobId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCommunication_ProjectId",
                table: "ProjectCommunication",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCommunicationAttachment_PCId",
                table: "ProjectCommunicationAttachment",
                column: "PCId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCommunicationAttachment_ProjectId",
                table: "ProjectCommunicationAttachment",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDetail_ProjectPhaseDetailsId",
                table: "ProjectDetail",
                column: "ProjectPhaseDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectHiringControl_ProjectId",
                table: "ProjectHiringControl",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectHiringControl_UserID",
                table: "ProjectHiringControl",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectHiringRequestDetail_BudgetLineId",
                table: "ProjectHiringRequestDetail",
                column: "BudgetLineId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectHiringRequestDetail_CurrencyId",
                table: "ProjectHiringRequestDetail",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectHiringRequestDetail_EmployeeID",
                table: "ProjectHiringRequestDetail",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectHiringRequestDetail_GradeId",
                table: "ProjectHiringRequestDetail",
                column: "GradeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectHiringRequestDetail_OfficeId",
                table: "ProjectHiringRequestDetail",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectHiringRequestDetail_ProfessionId",
                table: "ProjectHiringRequestDetail",
                column: "ProfessionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectHiringRequestDetail_ProjectId",
                table: "ProjectHiringRequestDetail",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectIndicatorQuestions_ProjectIndicatorId",
                table: "ProjectIndicatorQuestions",
                column: "ProjectIndicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectJobDetail_ProjectId",
                table: "ProjectJobDetail",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectLogisticsControl_ProjectId",
                table: "ProjectLogisticsControl",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectLogisticsControl_UserID",
                table: "ProjectLogisticsControl",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMonitoringIndicatorDetail_ProjectIndicatorId",
                table: "ProjectMonitoringIndicatorDetail",
                column: "ProjectIndicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMonitoringIndicatorDetail_ProjectMonitoringReviewId",
                table: "ProjectMonitoringIndicatorDetail",
                column: "ProjectMonitoringReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMonitoringIndicatorQuestions_MonitoringIndicatorId",
                table: "ProjectMonitoringIndicatorQuestions",
                column: "MonitoringIndicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMonitoringIndicatorQuestions_QuestionId",
                table: "ProjectMonitoringIndicatorQuestions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOpportunityControl_ProjectId",
                table: "ProjectOpportunityControl",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOpportunityControl_UserID",
                table: "ProjectOpportunityControl",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOtherDetail_ProjectId",
                table: "ProjectOtherDetail",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPhaseTime_ProjectId",
                table: "ProjectPhaseTime",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPhaseTime_ProjectPhaseDetailsId",
                table: "ProjectPhaseTime",
                column: "ProjectPhaseDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProgram_ProgramId",
                table: "ProjectProgram",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProgram_ProjectId",
                table: "ProjectProgram",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProposalDetail_ProjectId",
                table: "ProjectProposalDetail",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSector_ProjectId",
                table: "ProjectSector",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSector_SectorId",
                table: "ProjectSector",
                column: "SectorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProvinceDetails_CountryId",
                table: "ProvinceDetails",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProvinceMultiSelect_CountryMultiSelectId",
                table: "ProvinceMultiSelect",
                column: "CountryMultiSelectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProvinceMultiSelect_ProjectId",
                table: "ProvinceMultiSelect",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProvinceMultiSelect_ProvinceId",
                table: "ProvinceMultiSelect",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_PurposeofInitiativeCriteria_ProjectId",
                table: "PurposeofInitiativeCriteria",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingBasedCriteria_InterviewDetailsId",
                table: "RatingBasedCriteria",
                column: "InterviewDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskCriteriaDetail_CurrencyId",
                table: "RiskCriteriaDetail",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_RiskCriteriaDetail_ProjectId",
                table: "RiskCriteriaDetail",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PageId",
                table: "RolePermissions",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleDetails_ChannelId",
                table: "ScheduleDetails",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleDetails_JobId",
                table: "ScheduleDetails",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleDetails_MediumId",
                table: "ScheduleDetails",
                column: "MediumId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleDetails_PolicyId",
                table: "ScheduleDetails",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleDetails_ProjectId",
                table: "ScheduleDetails",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityConsiderationMultiSelect_ProjectId",
                table: "SecurityConsiderationMultiSelect",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityConsiderationMultiSelect_SecurityConsiderationId",
                table: "SecurityConsiderationMultiSelect",
                column: "SecurityConsiderationId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreInventories_InventoryCreditAccount",
                table: "StoreInventories",
                column: "InventoryCreditAccount");

            migrationBuilder.CreateIndex(
                name: "IX_StoreInventories_InventoryDebitAccount",
                table: "StoreInventories",
                column: "InventoryDebitAccount");

            migrationBuilder.CreateIndex(
                name: "IX_StoreItemGroups_InventoryId",
                table: "StoreItemGroups",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreItemPurchases_Currency",
                table: "StoreItemPurchases",
                column: "Currency");

            migrationBuilder.CreateIndex(
                name: "IX_StoreItemPurchases_InventoryItem",
                table: "StoreItemPurchases",
                column: "InventoryItem");

            migrationBuilder.CreateIndex(
                name: "IX_StoreItemPurchases_ProjectId",
                table: "StoreItemPurchases",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreItemPurchases_PurchasedById",
                table: "StoreItemPurchases",
                column: "PurchasedById");

            migrationBuilder.CreateIndex(
                name: "IX_StoreItemPurchases_ReceiptTypeId",
                table: "StoreItemPurchases",
                column: "ReceiptTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreItemPurchases_Status",
                table: "StoreItemPurchases",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_StoreItemPurchases_UnitType",
                table: "StoreItemPurchases",
                column: "UnitType");

            migrationBuilder.CreateIndex(
                name: "IX_StoreItemPurchases_VoucherId",
                table: "StoreItemPurchases",
                column: "VoucherId");

            migrationBuilder.CreateIndex(
                name: "IX_StorePurchaseOrders_InventoryItem",
                table: "StorePurchaseOrders",
                column: "InventoryItem");

            migrationBuilder.CreateIndex(
                name: "IX_StorePurchaseOrders_IssuedToEmployeeId",
                table: "StorePurchaseOrders",
                column: "IssuedToEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_StorePurchaseOrders_Purchase",
                table: "StorePurchaseOrders",
                column: "Purchase");

            migrationBuilder.CreateIndex(
                name: "IX_StoreSourceCodeDetail_CodeTypeId",
                table: "StoreSourceCodeDetail",
                column: "CodeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitRates_ActivityTypeId",
                table: "UnitRates",
                column: "ActivityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitRates_CurrencyDetailsCurrencyId",
                table: "UnitRates",
                column: "CurrencyDetailsCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitRates_MediaCategoryId",
                table: "UnitRates",
                column: "MediaCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitRates_MediumId",
                table: "UnitRates",
                column: "MediumId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitRates_NatureId",
                table: "UnitRates",
                column: "NatureId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitRates_QualityId",
                table: "UnitRates",
                column: "QualityId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitRates_TimeCategoryId",
                table: "UnitRates",
                column: "TimeCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherDetail_BudgetLineId",
                table: "VoucherDetail",
                column: "BudgetLineId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherDetail_CurrencyId",
                table: "VoucherDetail",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherDetail_FinancialYearId",
                table: "VoucherDetail",
                column: "FinancialYearId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherDetail_JournalCode",
                table: "VoucherDetail",
                column: "JournalCode");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherDetail_OfficeId",
                table: "VoucherDetail",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherDetail_ProjectId",
                table: "VoucherDetail",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherDetail_VoucherNo",
                table: "VoucherDetail",
                column: "VoucherNo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VoucherDetail_VoucherTypeId",
                table: "VoucherDetail",
                column: "VoucherTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactions_BudgetLineId",
                table: "VoucherTransactions",
                column: "BudgetLineId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactions_ChartOfAccountNewId",
                table: "VoucherTransactions",
                column: "ChartOfAccountNewId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactions_CurrencyId",
                table: "VoucherTransactions",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactions_JobId",
                table: "VoucherTransactions",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactions_OfficeId",
                table: "VoucherTransactions",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactions_ProjectId",
                table: "VoucherTransactions",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactions_TransactionId",
                table: "VoucherTransactions",
                column: "TransactionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactions_VoucherNo",
                table: "VoucherTransactions",
                column: "VoucherNo");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactions_TransactionDate_ChartOfAccountNewId",
                table: "VoucherTransactions",
                columns: new[] { "TransactionDate", "ChartOfAccountNewId" });

            migrationBuilder.CreateIndex(
                name: "IX_WinProjectDetails_ProjectId",
                table: "WinProjectDetails",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityDocumentsDetail");

            migrationBuilder.DropTable(
                name: "AgreeDisagreePermission");

            migrationBuilder.DropTable(
                name: "ApproveProjectDetails");

            migrationBuilder.DropTable(
                name: "ApproveRejectPermission");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AssignLeaveToEmployee");

            migrationBuilder.DropTable(
                name: "BudgetLineType");

            migrationBuilder.DropTable(
                name: "CEAgeGroupDetail");

            migrationBuilder.DropTable(
                name: "CEAssumptionDetail");

            migrationBuilder.DropTable(
                name: "CEFeasibilityExpertOtherDetail");

            migrationBuilder.DropTable(
                name: "ChatDetail");

            migrationBuilder.DropTable(
                name: "ContractTypeContent");

            migrationBuilder.DropTable(
                name: "DistrictMultiSelect");

            migrationBuilder.DropTable(
                name: "DonorCriteriaDetail");

            migrationBuilder.DropTable(
                name: "DonorDetail");

            migrationBuilder.DropTable(
                name: "DonorEligibilityCriteria");

            migrationBuilder.DropTable(
                name: "EligibilityCriteriaDetail");

            migrationBuilder.DropTable(
                name: "EmailSettingDetail");

            migrationBuilder.DropTable(
                name: "EmployeeApplyLeave");

            migrationBuilder.DropTable(
                name: "EmployeeAppraisalDetails");

            migrationBuilder.DropTable(
                name: "EmployeeAppraisalQuestions");

            migrationBuilder.DropTable(
                name: "EmployeeAppraisalTeamMember");

            migrationBuilder.DropTable(
                name: "EmployeeAttendance");

            migrationBuilder.DropTable(
                name: "EmployeeContract");

            migrationBuilder.DropTable(
                name: "EmployeeDocumentDetail");

            migrationBuilder.DropTable(
                name: "EmployeeEducations");

            migrationBuilder.DropTable(
                name: "EmployeeEvaluation");

            migrationBuilder.DropTable(
                name: "EmployeeEvaluationTraining");

            migrationBuilder.DropTable(
                name: "EmployeeHealthInfo");

            migrationBuilder.DropTable(
                name: "EmployeeHealthQuestion");

            migrationBuilder.DropTable(
                name: "EmployeeHistoryDetail");

            migrationBuilder.DropTable(
                name: "EmployeeHistoryOutsideCountry");

            migrationBuilder.DropTable(
                name: "EmployeeHistoryOutsideOrganization");

            migrationBuilder.DropTable(
                name: "EmployeeInfoReferences");

            migrationBuilder.DropTable(
                name: "EmployeeLanguages");

            migrationBuilder.DropTable(
                name: "EmployeeMonthlyAttendance");

            migrationBuilder.DropTable(
                name: "EmployeeMonthlyPayroll");

            migrationBuilder.DropTable(
                name: "EmployeeOtherSkills");

            migrationBuilder.DropTable(
                name: "EmployeePaymentTypes");

            migrationBuilder.DropTable(
                name: "EmployeePayroll");

            migrationBuilder.DropTable(
                name: "EmployeePayrollAccountHead");

            migrationBuilder.DropTable(
                name: "EmployeePayrollMonth");

            migrationBuilder.DropTable(
                name: "EmployeePensionRate");

            migrationBuilder.DropTable(
                name: "EmployeeProfessionalDetail");

            migrationBuilder.DropTable(
                name: "EmployeeRelativeInfo");

            migrationBuilder.DropTable(
                name: "EmployeeSalaryAnalyticalInfo");

            migrationBuilder.DropTable(
                name: "EmployeeSalaryBudget");

            migrationBuilder.DropTable(
                name: "EmployeeSalaryDetails");

            migrationBuilder.DropTable(
                name: "EmployeeSalaryPaymentHistory");

            migrationBuilder.DropTable(
                name: "ExchangeRate");

            migrationBuilder.DropTable(
                name: "ExchangeRateDetail");

            migrationBuilder.DropTable(
                name: "ExchangeRateVerifications");

            migrationBuilder.DropTable(
                name: "ExistInterviewDetails");

            migrationBuilder.DropTable(
                name: "FeasibilityCriteriaDetail");

            migrationBuilder.DropTable(
                name: "FinancialCriteriaDetail");

            migrationBuilder.DropTable(
                name: "FinancialProjectDetail");

            migrationBuilder.DropTable(
                name: "GainLossSelectedAccounts");

            migrationBuilder.DropTable(
                name: "GenderConsiderationDetail");

            migrationBuilder.DropTable(
                name: "HiringRequestCandidates");

            migrationBuilder.DropTable(
                name: "HolidayWeeklyDetails");

            migrationBuilder.DropTable(
                name: "HRJobInterviewers");

            migrationBuilder.DropTable(
                name: "InterviewLanguages");

            migrationBuilder.DropTable(
                name: "InterviewScheduleDetails");

            migrationBuilder.DropTable(
                name: "InterviewTechnicalQuestion");

            migrationBuilder.DropTable(
                name: "InterviewTechnicalQuestions");

            migrationBuilder.DropTable(
                name: "InterviewTrainings");

            migrationBuilder.DropTable(
                name: "InvoiceApproval");

            migrationBuilder.DropTable(
                name: "InvoiceGeneration");

            migrationBuilder.DropTable(
                name: "ItemSpecificationDetails");

            migrationBuilder.DropTable(
                name: "JobPriceDetails");

            migrationBuilder.DropTable(
                name: "LoggerDetails");

            migrationBuilder.DropTable(
                name: "OrderSchedulePermission");

            migrationBuilder.DropTable(
                name: "PaymentTypes");

            migrationBuilder.DropTable(
                name: "PayrollMonthlyHourDetail");

            migrationBuilder.DropTable(
                name: "PensionDebitAccountMaster");

            migrationBuilder.DropTable(
                name: "PensionPaymentHistory");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "PermissionsInRoles");

            migrationBuilder.DropTable(
                name: "PlayoutMinutes");

            migrationBuilder.DropTable(
                name: "PolicyDaySchedules");

            migrationBuilder.DropTable(
                name: "PolicyOrderSchedules");

            migrationBuilder.DropTable(
                name: "PolicySchedules");

            migrationBuilder.DropTable(
                name: "PolicyTimeSchedules");

            migrationBuilder.DropTable(
                name: "PriorityCriteriaDetail");

            migrationBuilder.DropTable(
                name: "PriorityOtherDetail");

            migrationBuilder.DropTable(
                name: "ProjectActivitiesControl");

            migrationBuilder.DropTable(
                name: "ProjectActivityExtensions");

            migrationBuilder.DropTable(
                name: "ProjectActivityProvinceDetail");

            migrationBuilder.DropTable(
                name: "ProjectArea");

            migrationBuilder.DropTable(
                name: "ProjectAssignTo");

            migrationBuilder.DropTable(
                name: "ProjectCommunicationAttachment");

            migrationBuilder.DropTable(
                name: "ProjectHiringControl");

            migrationBuilder.DropTable(
                name: "ProjectLogisticsControl");

            migrationBuilder.DropTable(
                name: "ProjectMonitoringIndicatorQuestions");

            migrationBuilder.DropTable(
                name: "ProjectOpportunityControl");

            migrationBuilder.DropTable(
                name: "ProjectOtherDetail");

            migrationBuilder.DropTable(
                name: "ProjectPhaseTime");

            migrationBuilder.DropTable(
                name: "ProjectProgram");

            migrationBuilder.DropTable(
                name: "ProjectProposalDetail");

            migrationBuilder.DropTable(
                name: "ProjectSector");

            migrationBuilder.DropTable(
                name: "ProvinceMultiSelect");

            migrationBuilder.DropTable(
                name: "PurposeofInitiativeCriteria");

            migrationBuilder.DropTable(
                name: "RatingBasedCriteria");

            migrationBuilder.DropTable(
                name: "RiskCriteriaDetail");

            migrationBuilder.DropTable(
                name: "RolePermissions");

            migrationBuilder.DropTable(
                name: "SalaryTaxReportContent");

            migrationBuilder.DropTable(
                name: "SecurityConsiderationMultiSelect");

            migrationBuilder.DropTable(
                name: "SecurityDetail");

            migrationBuilder.DropTable(
                name: "StorePurchaseOrders");

            migrationBuilder.DropTable(
                name: "StoreSourceCodeDetail");

            migrationBuilder.DropTable(
                name: "StrengthConsiderationDetail");

            migrationBuilder.DropTable(
                name: "StrongandWeakPoints");

            migrationBuilder.DropTable(
                name: "TargetBeneficiaryDetail");

            migrationBuilder.DropTable(
                name: "TechnicalQuestion");

            migrationBuilder.DropTable(
                name: "UserDetailOffices");

            migrationBuilder.DropTable(
                name: "VoucherTransactions");

            migrationBuilder.DropTable(
                name: "WinProjectDetails");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "EntitySourceDocumentDetails");

            migrationBuilder.DropTable(
                name: "EmailType");

            migrationBuilder.DropTable(
                name: "AppraisalGeneralQuestions");

            migrationBuilder.DropTable(
                name: "HolidayDetails");

            migrationBuilder.DropTable(
                name: "LeaveReasonDetail");

            migrationBuilder.DropTable(
                name: "Advances");

            migrationBuilder.DropTable(
                name: "PayrollAccountHead");

            migrationBuilder.DropTable(
                name: "SalaryHeadDetails");

            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "DesignationDetail");

            migrationBuilder.DropTable(
                name: "EmployeeContractType");

            migrationBuilder.DropTable(
                name: "JobHiringDetails");

            migrationBuilder.DropTable(
                name: "ItemSpecificationMaster");

            migrationBuilder.DropTable(
                name: "AttendanceGroupMaster");

            migrationBuilder.DropTable(
                name: "ScheduleDetails");

            migrationBuilder.DropTable(
                name: "ProjectActivityDetail");

            migrationBuilder.DropTable(
                name: "DistrictDetail");

            migrationBuilder.DropTable(
                name: "AreaDetail");

            migrationBuilder.DropTable(
                name: "ProjectCommunication");

            migrationBuilder.DropTable(
                name: "ProjectMonitoringIndicatorDetail");

            migrationBuilder.DropTable(
                name: "ProjectIndicatorQuestions");

            migrationBuilder.DropTable(
                name: "UserDetails");

            migrationBuilder.DropTable(
                name: "ProgramDetail");

            migrationBuilder.DropTable(
                name: "SectorDetails");

            migrationBuilder.DropTable(
                name: "CountryMultiSelectDetails");

            migrationBuilder.DropTable(
                name: "InterviewDetails");

            migrationBuilder.DropTable(
                name: "ApplicationPages");

            migrationBuilder.DropTable(
                name: "SecurityConsiderationDetail");

            migrationBuilder.DropTable(
                name: "StoreItemPurchases");

            migrationBuilder.DropTable(
                name: "CodeType");

            migrationBuilder.DropTable(
                name: "DocumentFileDetail");

            migrationBuilder.DropTable(
                name: "ProjectHiringRequestDetail");

            migrationBuilder.DropTable(
                name: "Channel");

            migrationBuilder.DropTable(
                name: "JobDetails");

            migrationBuilder.DropTable(
                name: "PolicyDetails");

            migrationBuilder.DropTable(
                name: "ActivityStatusDetail");

            migrationBuilder.DropTable(
                name: "ProjectMonitoringReviewDetail");

            migrationBuilder.DropTable(
                name: "ProjectIndicators");

            migrationBuilder.DropTable(
                name: "InventoryItems");

            migrationBuilder.DropTable(
                name: "ReceiptType");

            migrationBuilder.DropTable(
                name: "StatusAtTimeOfIssue");

            migrationBuilder.DropTable(
                name: "PurchaseUnitType");

            migrationBuilder.DropTable(
                name: "VoucherDetail");

            migrationBuilder.DropTable(
                name: "EmployeeDetail");

            migrationBuilder.DropTable(
                name: "JobGrade");

            migrationBuilder.DropTable(
                name: "ProfessionDetails");

            migrationBuilder.DropTable(
                name: "ContractDetails");

            migrationBuilder.DropTable(
                name: "JobPhases");

            migrationBuilder.DropTable(
                name: "Producers");

            migrationBuilder.DropTable(
                name: "StoreItemGroups");

            migrationBuilder.DropTable(
                name: "InventoryItemType");

            migrationBuilder.DropTable(
                name: "ProjectBudgetLineDetail");

            migrationBuilder.DropTable(
                name: "FinancialYearDetail");

            migrationBuilder.DropTable(
                name: "JournalDetail");

            migrationBuilder.DropTable(
                name: "OfficeDetail");

            migrationBuilder.DropTable(
                name: "VoucherType");

            migrationBuilder.DropTable(
                name: "EmployeeType");

            migrationBuilder.DropTable(
                name: "QualificationDetails");

            migrationBuilder.DropTable(
                name: "NationalityDetails");

            migrationBuilder.DropTable(
                name: "ProvinceDetails");

            migrationBuilder.DropTable(
                name: "ClientDetails");

            migrationBuilder.DropTable(
                name: "LanguageDetail");

            migrationBuilder.DropTable(
                name: "UnitRates");

            migrationBuilder.DropTable(
                name: "StoreInventories");

            migrationBuilder.DropTable(
                name: "ProjectJobDetail");

            migrationBuilder.DropTable(
                name: "CountryDetails");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "ActivityTypes");

            migrationBuilder.DropTable(
                name: "CurrencyDetails");

            migrationBuilder.DropTable(
                name: "MediaCategories");

            migrationBuilder.DropTable(
                name: "Mediums");

            migrationBuilder.DropTable(
                name: "Natures");

            migrationBuilder.DropTable(
                name: "Qualities");

            migrationBuilder.DropTable(
                name: "TimeCategories");

            migrationBuilder.DropTable(
                name: "ChartOfAccountNew");

            migrationBuilder.DropTable(
                name: "ProjectDetail");

            migrationBuilder.DropTable(
                name: "AccountFilterType");

            migrationBuilder.DropTable(
                name: "AccountLevel");

            migrationBuilder.DropTable(
                name: "AccountType");

            migrationBuilder.DropTable(
                name: "ProjectPhaseDetails");

            migrationBuilder.DropTable(
                name: "AccountHeadType");
        }
    }
}

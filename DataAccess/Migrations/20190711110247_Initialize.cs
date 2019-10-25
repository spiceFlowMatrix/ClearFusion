using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountLevel",
                columns: table => new
                {
                    AccountLevelId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AccountLevelName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountLevel", x => x.AccountLevelId);
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
                name: "CodeType",
                columns: table => new
                {
                    CodeTypeId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CodeTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CodeType", x => x.CodeTypeId);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeContractType",
                columns: table => new
                {
                    EmployeeContractTypeId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    EmployeeContractTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeContractType", x => x.EmployeeContractTypeId);
                });

            migrationBuilder.CreateTable(
                name: "UserDetails",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Id = table.Column<Guid>(nullable: false),
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
                    VoucherTypeId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    VoucherTypeName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoucherType", x => x.VoucherTypeId);
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
                name: "AccountFilterType",
                columns: table => new
                {
                    AccountFilterTypeId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    AccountFilterTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountFilterType", x => x.AccountFilterTypeId);
                    table.ForeignKey(
                        name: "FK_AccountFilterType_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountFilterType_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AccountHeadType",
                columns: table => new
                {
                    AccountHeadTypeId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    AccountHeadTypeName = table.Column<string>(nullable: true),
                    IsCreditBalancetype = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountHeadType", x => x.AccountHeadTypeId);
                    table.ForeignKey(
                        name: "FK_AccountHeadType_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountHeadType_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    IsDeleted = table.Column<bool>(nullable: true),
                    StatusName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityStatusDetail", x => x.StatusId);
                    table.ForeignKey(
                        name: "FK_ActivityStatusDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActivityStatusDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    IsDeleted = table.Column<bool>(nullable: true),
                    ActivityName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityTypes", x => x.ActivityTypeId);
                    table.ForeignKey(
                        name: "FK_ActivityTypes_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActivityTypes_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AnalyticalDetail",
                columns: table => new
                {
                    AnalyticalId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    MemoCode = table.Column<string>(maxLength: 10, nullable: true),
                    MemoType = table.Column<byte>(nullable: false),
                    Program = table.Column<string>(maxLength: 10, nullable: true),
                    Project = table.Column<string>(maxLength: 10, nullable: true),
                    Job = table.Column<string>(maxLength: 10, nullable: true),
                    Sector = table.Column<string>(maxLength: 10, nullable: true),
                    Area = table.Column<string>(maxLength: 10, nullable: true),
                    MDCode = table.Column<string>(maxLength: 10, nullable: true),
                    MemoName = table.Column<string>(maxLength: 200, nullable: true),
                    BLAmount = table.Column<float>(nullable: true),
                    BLCurrCode = table.Column<string>(maxLength: 5, nullable: true),
                    CostBook = table.Column<string>(maxLength: 10, nullable: true),
                    Status = table.Column<byte>(nullable: false),
                    DonorCode = table.Column<string>(maxLength: 50, nullable: true),
                    BLType = table.Column<byte>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    ReceivedAmount = table.Column<float>(nullable: true),
                    Attachment = table.Column<string>(maxLength: 100, nullable: true)
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

            migrationBuilder.CreateTable(
                name: "ApplicationPages",
                columns: table => new
                {
                    PageId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    PageName = table.Column<string>(nullable: true),
                    ModuleName = table.Column<string>(nullable: true),
                    ModuleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationPages", x => x.PageId);
                    table.ForeignKey(
                        name: "FK_ApplicationPages_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApplicationPages_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppraisalGeneralQuestions",
                columns: table => new
                {
                    AppraisalGeneralQuestionsId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    SequenceNo = table.Column<int>(nullable: true),
                    Question = table.Column<string>(nullable: true),
                    DariQuestion = table.Column<string>(nullable: true),
                    OfficeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppraisalGeneralQuestions", x => x.AppraisalGeneralQuestionsId);
                    table.ForeignKey(
                        name: "FK_AppraisalGeneralQuestions_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppraisalGeneralQuestions_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AreaDetail",
                columns: table => new
                {
                    AreaId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    AreaName = table.Column<string>(nullable: true),
                    AreaCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreaDetail", x => x.AreaId);
                    table.ForeignKey(
                        name: "FK_AreaDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AreaDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "AttendanceGroupMaster",
                columns: table => new
                {
                    AttendanceGroupId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceGroupMaster", x => x.AttendanceGroupId);
                    table.ForeignKey(
                        name: "FK_AttendanceGroupMaster_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AttendanceGroupMaster_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BudgetLineType",
                columns: table => new
                {
                    BudgetLineTypeId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    BudgetLineTypeName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetLineType", x => x.BudgetLineTypeId);
                    table.ForeignKey(
                        name: "FK_BudgetLineType_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BudgetLineType_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    IsDeleted = table.Column<bool>(nullable: true),
                    CategoryName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                    table.ForeignKey(
                        name: "FK_Categories_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Categories_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CountryDetails",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    CountryName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryDetails", x => x.CountryId);
                    table.ForeignKey(
                        name: "FK_CountryDetails_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CountryDetails_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    IsDeleted = table.Column<bool>(nullable: true),
                    CurrencyCode = table.Column<string>(maxLength: 5, nullable: true),
                    CurrencyName = table.Column<string>(maxLength: 50, nullable: true),
                    CurrencyRate = table.Column<float>(nullable: true),
                    Status = table.Column<bool>(nullable: true),
                    SalaryTaxFlag = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyDetails", x => x.CurrencyId);
                    table.ForeignKey(
                        name: "FK_CurrencyDetails_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CurrencyDetails_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    IsDeleted = table.Column<bool>(nullable: true),
                    Designation = table.Column<string>(maxLength: 100, nullable: true),
                    DesignationDari = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignationDetail", x => x.DesignationId);
                    table.ForeignKey(
                        name: "FK_DesignationDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DesignationDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DistrictDetail",
                columns: table => new
                {
                    DistrictID = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    District = table.Column<string>(maxLength: 50, nullable: true),
                    ProvinceID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistrictDetail", x => x.DistrictID);
                    table.ForeignKey(
                        name: "FK_DistrictDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DistrictDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    IsDeleted = table.Column<bool>(nullable: true),
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
                    table.ForeignKey(
                        name: "FK_DocumentFileDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DocumentFileDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DonorDetail",
                columns: table => new
                {
                    DonorId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ContactPerson = table.Column<string>(nullable: true),
                    ContactDesignation = table.Column<string>(nullable: true),
                    ContactPersonEmail = table.Column<string>(nullable: true),
                    ContactPersonCell = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonorDetail", x => x.DonorId);
                    table.ForeignKey(
                        name: "FK_DonorDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DonorDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmailType",
                columns: table => new
                {
                    EmailTypeId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    EmailTypeName = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailType", x => x.EmailTypeId);
                    table.ForeignKey(
                        name: "FK_EmailType_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmailType_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeAnalyticalDetail",
                columns: table => new
                {
                    AnalyticalID = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    EmployeeID = table.Column<int>(nullable: false),
                    Donor = table.Column<string>(maxLength: 10, nullable: true),
                    AccountCode = table.Column<string>(maxLength: 10, nullable: true),
                    Area = table.Column<string>(maxLength: 10, nullable: true),
                    Sector = table.Column<string>(maxLength: 10, nullable: true),
                    Program = table.Column<string>(maxLength: 10, nullable: true),
                    Project = table.Column<string>(maxLength: 10, nullable: true),
                    Job = table.Column<string>(maxLength: 10, nullable: true),
                    CostBook = table.Column<string>(maxLength: 10, nullable: true),
                    SalaryPercentage = table.Column<float>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeAnalyticalDetail", x => x.AnalyticalID);
                    table.ForeignKey(
                        name: "FK_EmployeeAnalyticalDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeAnalyticalDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeAppraisalTeamMember",
                columns: table => new
                {
                    EmployeeAppraisalTeamMemberId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    EmployeeId = table.Column<int>(nullable: false),
                    EmployeeAppraisalDetailsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeAppraisalTeamMember", x => x.EmployeeAppraisalTeamMemberId);
                    table.ForeignKey(
                        name: "FK_EmployeeAppraisalTeamMember_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeAppraisalTeamMember_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeDetailDT",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    EmployeeCode = table.Column<string>(nullable: true),
                    EmployeeName = table.Column<string>(maxLength: 100, nullable: true),
                    CurrencyCode = table.Column<string>(nullable: true),
                    RegCode = table.Column<string>(nullable: true),
                    BasicPay = table.Column<float>(nullable: true),
                    FoodAllowance = table.Column<double>(nullable: true),
                    MedicalAllowance = table.Column<double>(nullable: true),
                    TrAllowance = table.Column<double>(nullable: true),
                    OtherAllowance = table.Column<double>(nullable: true),
                    Other1Allowance = table.Column<double>(nullable: true),
                    Other2Allowance = table.Column<double>(nullable: true),
                    Other1Description = table.Column<string>(nullable: true),
                    Other2Description = table.Column<string>(nullable: true),
                    Designation = table.Column<string>(maxLength: 50, nullable: true),
                    FatherName = table.Column<string>(maxLength: 100, nullable: true),
                    sex = table.Column<string>(maxLength: 5, nullable: true),
                    PermanentAddress = table.Column<string>(maxLength: 200, nullable: true),
                    CurrentAddress = table.Column<string>(maxLength: 200, nullable: true),
                    Country = table.Column<string>(maxLength: 50, nullable: true),
                    Province = table.Column<string>(maxLength: 50, nullable: true),
                    District = table.Column<string>(maxLength: 50, nullable: true),
                    City = table.Column<string>(maxLength: 50, nullable: true),
                    Village = table.Column<string>(maxLength: 50, nullable: true),
                    Phone = table.Column<string>(maxLength: 20, nullable: true),
                    Fax = table.Column<string>(maxLength: 20, nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: true),
                    ReferBy = table.Column<string>(maxLength: 50, nullable: true),
                    HireOn = table.Column<DateTime>(nullable: true),
                    FireOn = table.Column<DateTime>(nullable: true),
                    ResignedOn = table.Column<DateTime>(nullable: true),
                    FireReason = table.Column<string>(maxLength: 200, nullable: true),
                    ResignedReason = table.Column<string>(maxLength: 200, nullable: true),
                    ContractStatus = table.Column<string>(maxLength: 5, nullable: true),
                    ContractStartDate = table.Column<DateTime>(nullable: true),
                    ContractEndDate = table.Column<DateTime>(nullable: true),
                    ContractNumber = table.Column<double>(nullable: true),
                    ContractPeriod = table.Column<double>(nullable: true),
                    PeriodType = table.Column<string>(maxLength: 10, nullable: true),
                    Profession = table.Column<string>(maxLength: 50, nullable: true),
                    Qualification = table.Column<string>(maxLength: 50, nullable: true),
                    grade = table.Column<string>(maxLength: 10, nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    Experience = table.Column<string>(maxLength: 50, nullable: true),
                    PreviousWork = table.Column<string>(maxLength: 50, nullable: true),
                    PreviousExperience = table.Column<string>(maxLength: 50, nullable: true),
                    Nationality = table.Column<string>(maxLength: 50, nullable: true),
                    Passport = table.Column<string>(maxLength: 50, nullable: true),
                    IdCard = table.Column<string>(maxLength: 20, nullable: true),
                    Language = table.Column<string>(maxLength: 30, nullable: true),
                    Age = table.Column<int>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: true),
                    PlaceOfBirth = table.Column<string>(maxLength: 50, nullable: true),
                    Department = table.Column<string>(maxLength: 50, nullable: true),
                    JobDescription = table.Column<string>(nullable: true),
                    Training = table.Column<string>(nullable: true),
                    Extended = table.Column<bool>(nullable: true),
                    CapacityBuildingDeductibles = table.Column<double>(nullable: true),
                    SecurityDeductibles = table.Column<double>(nullable: true),
                    FinesDeductibles = table.Column<double>(nullable: true),
                    OtherDeductibles = table.Column<double>(nullable: true),
                    FineReason = table.Column<string>(maxLength: 200, nullable: true),
                    PensionDeductibles = table.Column<double>(nullable: true),
                    EmployeePhoto = table.Column<int>(nullable: false),
                    WorkType = table.Column<string>(maxLength: 20, nullable: true),
                    MonthlyBasicPay = table.Column<double>(nullable: true),
                    TinNo = table.Column<string>(maxLength: 20, nullable: true),
                    BankAccount = table.Column<string>(maxLength: 100, nullable: true),
                    CasualLeaveAllowance = table.Column<int>(nullable: true),
                    MedicalLeaveAllowance = table.Column<int>(nullable: true),
                    EmergencyLeaveAllowance = table.Column<int>(nullable: true),
                    MaternityLeaveAllowance = table.Column<int>(nullable: true),
                    EmployeeNameDari = table.Column<string>(maxLength: 100, nullable: true),
                    EmployeeCodeDari = table.Column<string>(maxLength: 20, nullable: true),
                    FatherNameDari = table.Column<string>(maxLength: 100, nullable: true),
                    DesignationDari = table.Column<string>(maxLength: 50, nullable: true),
                    ProvinceDari = table.Column<string>(maxLength: 50, nullable: true),
                    CityDari = table.Column<string>(maxLength: 50, nullable: true),
                    ContractStartDateDari = table.Column<string>(maxLength: 50, nullable: true),
                    ContractEndDateDari = table.Column<string>(maxLength: 50, nullable: true),
                    ContractPeriodDari = table.Column<double>(nullable: true),
                    PeriodTypeDari = table.Column<string>(maxLength: 10, nullable: true),
                    NoOfChildren = table.Column<int>(nullable: true),
                    MaritalStatus = table.Column<string>(maxLength: 15, nullable: true),
                    SpeakLanguageList = table.Column<string>(nullable: true),
                    CloseRelativeList = table.Column<string>(nullable: true),
                    RefereeList = table.Column<string>(nullable: true),
                    EducationList = table.Column<string>(nullable: true),
                    NationalEmploymentList = table.Column<string>(nullable: true),
                    InternationalEmploymentList = table.Column<string>(nullable: true),
                    OtherSkillList = table.Column<string>(nullable: true),
                    ProjectId = table.Column<long>(nullable: true),
                    ETN = table.Column<string>(maxLength: 10, nullable: true),
                    SECT = table.Column<string>(maxLength: 10, nullable: true),
                    Comments = table.Column<string>(nullable: true),
                    PoliticalPartyMembership = table.Column<string>(nullable: true),
                    TingenerateDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeDetailDT", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_EmployeeDetailDT_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeDetailDT_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeEvaluation",
                columns: table => new
                {
                    EmployeeEvaluationId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                    table.ForeignKey(
                        name: "FK_EmployeeEvaluation_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeEvaluation_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeEvaluationTraining",
                columns: table => new
                {
                    EmployeeEvaluationTrainingId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                    table.ForeignKey(
                        name: "FK_EmployeeEvaluationTraining_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeEvaluationTraining_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeHealthQuestion",
                columns: table => new
                {
                    EmployeeHealthQuestionId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    EmployeeId = table.Column<int>(nullable: false),
                    Question = table.Column<string>(nullable: true),
                    Answer = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeHealthQuestion", x => x.EmployeeHealthQuestionId);
                    table.ForeignKey(
                        name: "FK_EmployeeHealthQuestion_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeHealthQuestion_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeePayrollDetailTest",
                columns: table => new
                {
                    PayrollId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    EmployeeId = table.Column<int>(nullable: true),
                    payrollmonth = table.Column<int>(nullable: true),
                    payrollyear = table.Column<int>(nullable: true),
                    currencycode = table.Column<string>(nullable: true),
                    regcode = table.Column<string>(nullable: true),
                    basicpay = table.Column<double>(nullable: true),
                    TotalAllowance = table.Column<double>(nullable: true),
                    TotalDeduction = table.Column<double>(nullable: true),
                    Sent = table.Column<bool>(nullable: true),
                    Status = table.Column<bool>(nullable: true),
                    Attendance = table.Column<int>(nullable: true),
                    Absent = table.Column<int>(nullable: true),
                    TotalDuration = table.Column<int>(nullable: true),
                    FoodAllowance = table.Column<float>(nullable: true),
                    TRAllowance = table.Column<float>(nullable: true),
                    OtherAllowance = table.Column<float>(nullable: true),
                    Medical = table.Column<float>(nullable: true),
                    Other1 = table.Column<float>(nullable: true),
                    Other2 = table.Column<float>(nullable: true),
                    AdvanceDeduction = table.Column<float>(nullable: true),
                    SalaryTaxDeduction = table.Column<float>(nullable: true),
                    FineDeduction = table.Column<float>(nullable: true),
                    OtherDeduction = table.Column<float>(nullable: true),
                    CapacityBuildingDeductibles = table.Column<float>(nullable: true),
                    SecurityDeduction = table.Column<float>(nullable: true),
                    PensionDeduction = table.Column<float>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePayrollDetailTest", x => x.PayrollId);
                    table.ForeignKey(
                        name: "FK_EmployeePayrollDetailTest_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeePayrollDetailTest_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeType",
                columns: table => new
                {
                    EmployeeTypeId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    EmployeeTypeName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeType", x => x.EmployeeTypeId);
                    table.ForeignKey(
                        name: "FK_EmployeeType_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeType_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "errorlog",
                columns: table => new
                {
                    ExceptionId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    Status = table.Column<int>(nullable: true),
                    stackTrace = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    Section = table.Column<int>(nullable: true),
                    ModuleName = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    DataXml = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_errorlog", x => x.ExceptionId);
                    table.ForeignKey(
                        name: "FK_errorlog_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_errorlog_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExchangeRateDetail",
                columns: table => new
                {
                    ExchangeRateId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    FromCurrency = table.Column<int>(nullable: false),
                    ToCurrency = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Rate = table.Column<decimal>(nullable: false),
                    OfficeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeRateDetail", x => x.ExchangeRateId);
                    table.ForeignKey(
                        name: "FK_ExchangeRateDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExchangeRateDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    IsDeleted = table.Column<bool>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    IsVerified = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExchangeRateVerifications", x => x.ExRateVerificationId);
                    table.ForeignKey(
                        name: "FK_ExchangeRateVerifications_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExchangeRateVerifications_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FinancialYearDetail",
                columns: table => new
                {
                    FinancialYearId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    FinancialYearName = table.Column<string>(maxLength: 50, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsDefault = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialYearDetail", x => x.FinancialYearId);
                    table.ForeignKey(
                        name: "FK_FinancialYearDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinancialYearDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GenderConsiderationDetail",
                columns: table => new
                {
                    GenderConsiderationId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    GenderConsiderationName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenderConsiderationDetail", x => x.GenderConsiderationId);
                    table.ForeignKey(
                        name: "FK_GenderConsiderationDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GenderConsiderationDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InterviewTechnicalQuestions",
                columns: table => new
                {
                    InterviewTechnicalQuestionsId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    Question = table.Column<string>(nullable: true),
                    OfficeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterviewTechnicalQuestions", x => x.InterviewTechnicalQuestionsId);
                    table.ForeignKey(
                        name: "FK_InterviewTechnicalQuestions_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InterviewTechnicalQuestions_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    IsDeleted = table.Column<bool>(nullable: true),
                    TypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryItemType", x => x.ItemType);
                    table.ForeignKey(
                        name: "FK_InventoryItemType_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryItemType_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JobGrade",
                columns: table => new
                {
                    GradeId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    GradeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobGrade", x => x.GradeId);
                    table.ForeignKey(
                        name: "FK_JobGrade_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobGrade_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    IsDeleted = table.Column<bool>(nullable: true),
                    Phase = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobPhases", x => x.JobPhaseId);
                    table.ForeignKey(
                        name: "FK_JobPhases_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobPhases_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JournalDetail",
                columns: table => new
                {
                    JournalCode = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    JournalName = table.Column<string>(maxLength: 100, nullable: true),
                    JournalType = table.Column<byte>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalDetail", x => x.JournalCode);
                    table.ForeignKey(
                        name: "FK_JournalDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JournalDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LanguageDetail",
                columns: table => new
                {
                    LanguageId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    LanguageName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageDetail", x => x.LanguageId);
                    table.ForeignKey(
                        name: "FK_LanguageDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LanguageDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    LanguageId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    LanguageName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.LanguageId);
                    table.ForeignKey(
                        name: "FK_Languages_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Languages_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LeaveReasonDetail",
                columns: table => new
                {
                    LeaveReasonId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ReasonName = table.Column<string>(maxLength: 50, nullable: true),
                    Unit = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveReasonDetail", x => x.LeaveReasonId);
                    table.ForeignKey(
                        name: "FK_LeaveReasonDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaveReasonDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    IsDeleted = table.Column<bool>(nullable: true),
                    NotificationId = table.Column<int>(nullable: false),
                    IsRead = table.Column<bool>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    LoggedDetail = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoggerDetails", x => x.LoggerDetailsId);
                    table.ForeignKey(
                        name: "FK_LoggerDetails_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LoggerDetails_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    IsDeleted = table.Column<bool>(nullable: true),
                    CategoryName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaCategories", x => x.MediaCategoryId);
                    table.ForeignKey(
                        name: "FK_MediaCategories_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MediaCategories_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    IsDeleted = table.Column<bool>(nullable: true),
                    MediumName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mediums", x => x.MediumId);
                    table.ForeignKey(
                        name: "FK_Mediums_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Mediums_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NationalityDetails",
                columns: table => new
                {
                    NationalityId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    NationalityName = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NationalityDetails", x => x.NationalityId);
                    table.ForeignKey(
                        name: "FK_NationalityDetails_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NationalityDetails_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    IsDeleted = table.Column<bool>(nullable: true),
                    NatureName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Natures", x => x.NatureId);
                    table.ForeignKey(
                        name: "FK_Natures_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Natures_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    IsDeleted = table.Column<bool>(nullable: true),
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
                    table.ForeignKey(
                        name: "FK_OfficeDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OfficeDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTypes",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ChartOfAccountNewId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTypes", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_PaymentTypes_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentTypes_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PayrollAccountHead",
                columns: table => new
                {
                    PayrollHeadId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    PayrollHeadTypeId = table.Column<int>(nullable: false),
                    PayrollHeadName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    AccountNo = table.Column<long>(nullable: true),
                    TransactionTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayrollAccountHead", x => x.PayrollHeadId);
                    table.ForeignKey(
                        name: "FK_PayrollAccountHead_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PayrollAccountHead_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permissions_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Permissions_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    IsDeleted = table.Column<bool>(nullable: true),
                    PermissionsInRolesId = table.Column<int>(type: "serial", nullable: false)
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
                    table.ForeignKey(
                        name: "FK_PermissionsInRoles_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PermissionsInRoles_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    IsDeleted = table.Column<bool>(nullable: true),
                    ProducerName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producers", x => x.ProducerId);
                    table.ForeignKey(
                        name: "FK_Producers_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Producers_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProfessionDetails",
                columns: table => new
                {
                    ProfessionId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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

            migrationBuilder.CreateTable(
                name: "ProgramDetail",
                columns: table => new
                {
                    ProgramId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ProjectId = table.Column<long>(nullable: false),
                    ProgramName = table.Column<string>(nullable: true),
                    ProgramCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgramDetail", x => x.ProgramId);
                    table.ForeignKey(
                        name: "FK_ProgramDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProgramDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectDetails",
                columns: table => new
                {
                    ProjectId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ProjectName = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    CurrencyId = table.Column<int>(nullable: true),
                    Budget = table.Column<double>(nullable: true),
                    ReceivableAmount = table.Column<double>(nullable: true),
                    PayableAmount = table.Column<double>(nullable: true),
                    CurrentBalance = table.Column<double>(nullable: true),
                    Status = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectDetails", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_ProjectDetails_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectDetails_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectIndicators",
                columns: table => new
                {
                    ProjectIndicatorId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    IndicatorName = table.Column<string>(nullable: true),
                    IndicatorCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectIndicators", x => x.ProjectIndicatorId);
                    table.ForeignKey(
                        name: "FK_ProjectIndicators_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectIndicators_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectMonitoringReviewDetail",
                columns: table => new
                {
                    ProjectMonitoringReviewId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                    table.ForeignKey(
                        name: "FK_ProjectMonitoringReviewDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectMonitoringReviewDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectPhaseDetails",
                columns: table => new
                {
                    ProjectPhaseDetailsId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ProjectPhase = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectPhaseDetails", x => x.ProjectPhaseDetailsId);
                    table.ForeignKey(
                        name: "FK_ProjectPhaseDetails_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectPhaseDetails_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    IsDeleted = table.Column<bool>(nullable: true),
                    UnitTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseUnitType", x => x.UnitTypeId);
                    table.ForeignKey(
                        name: "FK_PurchaseUnitType_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseUnitType_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QualificationDetails",
                columns: table => new
                {
                    QualificationId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    QualificationName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QualificationDetails", x => x.QualificationId);
                    table.ForeignKey(
                        name: "FK_QualificationDetails_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QualificationDetails_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    IsDeleted = table.Column<bool>(nullable: true),
                    QualityName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qualities", x => x.QualityId);
                    table.ForeignKey(
                        name: "FK_Qualities_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Qualities_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptType",
                columns: table => new
                {
                    ReceiptTypeId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ReceiptTypeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptType", x => x.ReceiptTypeId);
                    table.ForeignKey(
                        name: "FK_ReceiptType_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReceiptType_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalaryHeadDetails",
                columns: table => new
                {
                    SalaryHeadId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    HeadTypeId = table.Column<int>(nullable: false),
                    HeadName = table.Column<string>(maxLength: 50, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    AccountNo = table.Column<long>(nullable: true),
                    TransactionTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryHeadDetails", x => x.SalaryHeadId);
                    table.ForeignKey(
                        name: "FK_SalaryHeadDetails_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalaryHeadDetails_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalaryTaxReportContent",
                columns: table => new
                {
                    SalaryTaxReportContentId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    EmployerAuthorizedOfficerName = table.Column<string>(nullable: true),
                    PositionAuthorizedOfficer = table.Column<string>(nullable: true),
                    OfficeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryTaxReportContent", x => x.SalaryTaxReportContentId);
                    table.ForeignKey(
                        name: "FK_SalaryTaxReportContent_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalaryTaxReportContent_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SectorDetails",
                columns: table => new
                {
                    SectorId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    SectorName = table.Column<string>(nullable: true),
                    SectorCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectorDetails", x => x.SectorId);
                    table.ForeignKey(
                        name: "FK_SectorDetails_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SectorDetails_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SecurityConsiderationDetail",
                columns: table => new
                {
                    SecurityConsiderationId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    SecurityConsiderationName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityConsiderationDetail", x => x.SecurityConsiderationId);
                    table.ForeignKey(
                        name: "FK_SecurityConsiderationDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SecurityConsiderationDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SecurityDetail",
                columns: table => new
                {
                    SecurityId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    SecurityName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityDetail", x => x.SecurityId);
                    table.ForeignKey(
                        name: "FK_SecurityDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SecurityDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StatusAtTimeOfIssue",
                columns: table => new
                {
                    StatusAtTimeOfIssueId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    StatusName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusAtTimeOfIssue", x => x.StatusAtTimeOfIssueId);
                    table.ForeignKey(
                        name: "FK_StatusAtTimeOfIssue_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StatusAtTimeOfIssue_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StrengthConsiderationDetail",
                columns: table => new
                {
                    StrengthConsiderationId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    StrengthConsiderationName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StrengthConsiderationDetail", x => x.StrengthConsiderationId);
                    table.ForeignKey(
                        name: "FK_StrengthConsiderationDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StrengthConsiderationDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StrongandWeakPoints",
                columns: table => new
                {
                    StrongPointsId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    CurrentAppraisalDate = table.Column<DateTime>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    EmployeeAppraisalDetailsId = table.Column<int>(nullable: false),
                    Point = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StrongandWeakPoints", x => x.StrongPointsId);
                    table.ForeignKey(
                        name: "FK_StrongandWeakPoints_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StrongandWeakPoints_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TargetBeneficiaryDetail",
                columns: table => new
                {
                    TargetId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ProjectId = table.Column<long>(nullable: false),
                    TargetType = table.Column<int>(nullable: false),
                    TargetName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TargetBeneficiaryDetail", x => x.TargetId);
                    table.ForeignKey(
                        name: "FK_TargetBeneficiaryDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TargetBeneficiaryDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TechnicalQuestion",
                columns: table => new
                {
                    TechnicalQuestionId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    Question = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicalQuestion", x => x.TechnicalQuestionId);
                    table.ForeignKey(
                        name: "FK_TechnicalQuestion_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TechnicalQuestion_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    IsDeleted = table.Column<bool>(nullable: true),
                    TimeCategoryName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeCategories", x => x.TimeCategoryId);
                    table.ForeignKey(
                        name: "FK_TimeCategories_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TimeCategories_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserDetailOffices",
                columns: table => new
                {
                    UserOfficesId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    OfficeId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDetailOffices", x => x.UserOfficesId);
                    table.ForeignKey(
                        name: "FK_UserDetailOffices_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserDetailOffices_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
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
                    IsDeleted = table.Column<bool>(nullable: true),
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
                    table.ForeignKey(
                        name: "FK_StoreSourceCodeDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreSourceCodeDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
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
                    IsDeleted = table.Column<bool>(nullable: true),
                    EmployeeContractTypeId = table.Column<int>(nullable: false),
                    ContentEnglish = table.Column<string>(nullable: true),
                    ContentDari = table.Column<string>(nullable: true),
                    OfficeId = table.Column<int>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "AccountType",
                columns: table => new
                {
                    AccountTypeId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                    table.ForeignKey(
                        name: "FK_AccountType_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AccountType_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AgreeDisagreePermission",
                columns: table => new
                {
                    Id = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    PageId = table.Column<int>(nullable: false),
                    Agree = table.Column<bool>(nullable: false),
                    Disagree = table.Column<bool>(nullable: false),
                    RoleId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgreeDisagreePermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgreeDisagreePermission_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgreeDisagreePermission_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    Id = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    PageId = table.Column<int>(nullable: false),
                    Approve = table.Column<bool>(nullable: false),
                    Reject = table.Column<bool>(nullable: false),
                    RoleId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApproveRejectPermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApproveRejectPermission_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApproveRejectPermission_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    Id = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    PageId = table.Column<int>(nullable: false),
                    OrderSchedule = table.Column<bool>(nullable: false),
                    RoleId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderSchedulePermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderSchedulePermission_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderSchedulePermission_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    RolesPermissionId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_RolePermissions_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RolePermissions_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    EmployeeAppraisalQuestionsId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_EmployeeAppraisalQuestions_AppraisalGeneralQuestions_AppraisalGeneralQuestionsId",
                        column: x => x.AppraisalGeneralQuestionsId,
                        principalTable: "AppraisalGeneralQuestions",
                        principalColumn: "AppraisalGeneralQuestionsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeAppraisalQuestions_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeAppraisalQuestions_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClientDetails",
                columns: table => new
                {
                    ClientId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                    table.ForeignKey(
                        name: "FK_ClientDetails_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClientDetails_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProvinceDetails",
                columns: table => new
                {
                    ProvinceId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                    table.ForeignKey(
                        name: "FK_ProvinceDetails_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProvinceDetails_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmailSettingDetail",
                columns: table => new
                {
                    EmailId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_EmailSettingDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmailSettingDetail_EmailType_EmailTypeId",
                        column: x => x.EmailTypeId,
                        principalTable: "EmailType",
                        principalColumn: "EmailTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmailSettingDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeePensionRate",
                columns: table => new
                {
                    EmployeePensionRateId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    FinancialYearId = table.Column<int>(nullable: false),
                    PensionRate = table.Column<double>(nullable: true),
                    IsDefault = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePensionRate", x => x.EmployeePensionRateId);
                    table.ForeignKey(
                        name: "FK_EmployeePensionRate_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeePensionRate_FinancialYearDetail_FinancialYearId",
                        column: x => x.FinancialYearId,
                        principalTable: "FinancialYearDetail",
                        principalColumn: "FinancialYearId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeePensionRate_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeLanguages",
                columns: table => new
                {
                    SpeakLanguageId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_EmployeeLanguages_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeLanguages_LanguageDetail_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "LanguageDetail",
                        principalColumn: "LanguageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeLanguages_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    IsDeleted = table.Column<bool>(nullable: true),
                    ChannelName = table.Column<string>(nullable: true),
                    MediumId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Channel", x => x.ChannelId);
                    table.ForeignKey(
                        name: "FK_Channel_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Channel_Mediums_MediumId",
                        column: x => x.MediumId,
                        principalTable: "Mediums",
                        principalColumn: "MediumId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Channel_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    DepartmentName = table.Column<string>(nullable: true),
                    OfficeCode = table.Column<string>(nullable: true),
                    OfficeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.DepartmentId);
                    table.ForeignKey(
                        name: "FK_Department_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Department_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Department_OfficeDetail_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "OfficeDetail",
                        principalColumn: "OfficeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExchangeRates",
                columns: table => new
                {
                    ExchangeRateId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                    table.PrimaryKey("PK_ExchangeRates", x => x.ExchangeRateId);
                    table.ForeignKey(
                        name: "FK_ExchangeRates_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExchangeRates_CurrencyDetails_FromCurrency",
                        column: x => x.FromCurrency,
                        principalTable: "CurrencyDetails",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExchangeRates_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExchangeRates_OfficeDetail_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "OfficeDetail",
                        principalColumn: "OfficeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExchangeRates_CurrencyDetails_ToCurrency",
                        column: x => x.ToCurrency,
                        principalTable: "CurrencyDetails",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HolidayDetails",
                columns: table => new
                {
                    HolidayId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_HolidayDetails_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HolidayDetails_FinancialYearDetail_FinancialYearId",
                        column: x => x.FinancialYearId,
                        principalTable: "FinancialYearDetail",
                        principalColumn: "FinancialYearId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HolidayDetails_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
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
                    HolidayWeeklyId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    Day = table.Column<string>(maxLength: 30, nullable: true),
                    OfficeId = table.Column<int>(nullable: false),
                    FinancialYearId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HolidayWeeklyDetails", x => x.HolidayWeeklyId);
                    table.ForeignKey(
                        name: "FK_HolidayWeeklyDetails_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HolidayWeeklyDetails_FinancialYearDetail_FinancialYearId",
                        column: x => x.FinancialYearId,
                        principalTable: "FinancialYearDetail",
                        principalColumn: "FinancialYearId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HolidayWeeklyDetails_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    ItemSpecificationMasterId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ItemSpecificationField = table.Column<string>(nullable: true),
                    OfficeId = table.Column<int>(nullable: false),
                    ItemTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemSpecificationMaster", x => x.ItemSpecificationMasterId);
                    table.ForeignKey(
                        name: "FK_ItemSpecificationMaster_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemSpecificationMaster_InventoryItemType_ItemTypeId",
                        column: x => x.ItemTypeId,
                        principalTable: "InventoryItemType",
                        principalColumn: "ItemType",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemSpecificationMaster_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    PayrollMonthlyHourID = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_PayrollMonthlyHourDetail_AttendanceGroupMaster_AttendanceGroupId",
                        column: x => x.AttendanceGroupId,
                        principalTable: "AttendanceGroupMaster",
                        principalColumn: "AttendanceGroupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PayrollMonthlyHourDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PayrollMonthlyHourDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
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
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_PolicyDetails_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                        name: "FK_PolicyDetails_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PolicyDetails_Producers_ProducerId",
                        column: x => x.ProducerId,
                        principalTable: "Producers",
                        principalColumn: "ProducerId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectBudgetLine",
                columns: table => new
                {
                    BudgetLineId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ProjectId = table.Column<long>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    AmountReceivable = table.Column<double>(nullable: false),
                    AmountPayable = table.Column<double>(nullable: false),
                    BudgetLineTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectBudgetLine", x => x.BudgetLineId);
                    table.ForeignKey(
                        name: "FK_ProjectBudgetLine_BudgetLineType_BudgetLineTypeId",
                        column: x => x.BudgetLineTypeId,
                        principalTable: "BudgetLineType",
                        principalColumn: "BudgetLineTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectBudgetLine_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectBudgetLine_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectBudgetLine_ProjectDetails_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetails",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskMaster",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    TaskName = table.Column<string>(nullable: true),
                    Priority = table.Column<string>(maxLength: 20, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ProjectId = table.Column<long>(nullable: true),
                    Status = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskMaster", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_TaskMaster_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskMaster_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskMaster_ProjectDetails_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetails",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectIndicatorQuestions",
                columns: table => new
                {
                    IndicatorQuestionId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    IndicatorQuestion = table.Column<string>(nullable: true),
                    ProjectIndicatorId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectIndicatorQuestions", x => x.IndicatorQuestionId);
                    table.ForeignKey(
                        name: "FK_ProjectIndicatorQuestions_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectIndicatorQuestions_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectIndicatorQuestions_ProjectIndicators_ProjectIndicatorId",
                        column: x => x.ProjectIndicatorId,
                        principalTable: "ProjectIndicators",
                        principalColumn: "ProjectIndicatorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectMonitoringIndicatorDetail",
                columns: table => new
                {
                    MonitoringIndicatorId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ProjectIndicatorId = table.Column<long>(nullable: false),
                    ProjectMonitoringReviewId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectMonitoringIndicatorDetail", x => x.MonitoringIndicatorId);
                    table.ForeignKey(
                        name: "FK_ProjectMonitoringIndicatorDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectMonitoringIndicatorDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectMonitoringIndicatorDetail_ProjectIndicators_ProjectIndicatorId",
                        column: x => x.ProjectIndicatorId,
                        principalTable: "ProjectIndicators",
                        principalColumn: "ProjectIndicatorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectMonitoringIndicatorDetail_ProjectMonitoringReviewDetail_ProjectMonitoringReviewId",
                        column: x => x.ProjectMonitoringReviewId,
                        principalTable: "ProjectMonitoringReviewDetail",
                        principalColumn: "ProjectMonitoringReviewId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectDetail",
                columns: table => new
                {
                    ProjectId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_ProjectDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_UnitRates_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
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
                        name: "FK_UnitRates_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
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
                name: "CategoryPopulator",
                columns: table => new
                {
                    CategoryPopulatorId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    SubCategoryLabel = table.Column<string>(nullable: true),
                    ChartOfAccountCodeNew = table.Column<string>(nullable: true),
                    AccountTypeId = table.Column<int>(nullable: false),
                    ValueSource = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryPopulator", x => x.CategoryPopulatorId);
                    table.ForeignKey(
                        name: "FK_CategoryPopulator_AccountType_AccountTypeId",
                        column: x => x.AccountTypeId,
                        principalTable: "AccountType",
                        principalColumn: "AccountTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryPopulator_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CategoryPopulator_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChartOfAccountNew",
                columns: table => new
                {
                    ChartOfAccountNewId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                    table.ForeignKey(
                        name: "FK_ChartOfAccountNew_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChartOfAccountNew_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeDetail",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                    Grade = table.Column<string>(nullable: true),
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
                    NoOfChildren = table.Column<int>(nullable: true),
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
                        name: "FK_EmployeeDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
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
                });

            migrationBuilder.CreateTable(
                name: "ItemSpecificationDetails",
                columns: table => new
                {
                    ItemSpecificationDetailsId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ItemSpecificationMasterId = table.Column<int>(nullable: false),
                    ItemId = table.Column<string>(nullable: true),
                    ItemSpecificationValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemSpecificationDetails", x => x.ItemSpecificationDetailsId);
                    table.ForeignKey(
                        name: "FK_ItemSpecificationDetails_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemSpecificationDetails_ItemSpecificationMaster_ItemSpecificationMasterId",
                        column: x => x.ItemSpecificationMasterId,
                        principalTable: "ItemSpecificationMaster",
                        principalColumn: "ItemSpecificationMasterId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemSpecificationDetails_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_PolicyDaySchedules_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PolicyDaySchedules_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    IsDeleted = table.Column<bool>(nullable: true),
                    PolicyId = table.Column<long>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    RequestSchedule = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolicyOrderSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PolicyOrderSchedules_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PolicyOrderSchedules_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_PolicySchedules_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PolicySchedules_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_PolicyTimeSchedules_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PolicyTimeSchedules_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PolicyTimeSchedules_PolicyDetails_PolicyId",
                        column: x => x.PolicyId,
                        principalTable: "PolicyDetails",
                        principalColumn: "PolicyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BudgetReceivable",
                columns: table => new
                {
                    BudgetReceivalbeId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ProjectId = table.Column<long>(nullable: true),
                    BudgetLineId = table.Column<long>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    ExpectedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetReceivable", x => x.BudgetReceivalbeId);
                    table.ForeignKey(
                        name: "FK_BudgetReceivable_ProjectBudgetLine_BudgetLineId",
                        column: x => x.BudgetLineId,
                        principalTable: "ProjectBudgetLine",
                        principalColumn: "BudgetLineId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BudgetReceivable_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BudgetReceivable_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BudgetReceivable_ProjectDetails_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetails",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ActivityMaster",
                columns: table => new
                {
                    ActivityId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    TaskId = table.Column<int>(nullable: false),
                    ActivityName = table.Column<string>(nullable: true),
                    Priority = table.Column<string>(maxLength: 20, nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Status = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityMaster", x => x.ActivityId);
                    table.ForeignKey(
                        name: "FK_ActivityMaster_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActivityMaster_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActivityMaster_TaskMaster_TaskId",
                        column: x => x.TaskId,
                        principalTable: "TaskMaster",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectMonitoringIndicatorQuestions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_ProjectMonitoringIndicatorQuestions_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectMonitoringIndicatorQuestions_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectMonitoringIndicatorQuestions_ProjectMonitoringIndicatorDetail_MonitoringIndicatorId",
                        column: x => x.MonitoringIndicatorId,
                        principalTable: "ProjectMonitoringIndicatorDetail",
                        principalColumn: "MonitoringIndicatorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectMonitoringIndicatorQuestions_ProjectIndicatorQuestions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "ProjectIndicatorQuestions",
                        principalColumn: "IndicatorQuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApproveProjectDetails",
                columns: table => new
                {
                    ApproveProjrctId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                name: "CEAgeGroupDetail",
                columns: table => new
                {
                    AgeGroupOtherDetailId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ProjectId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CEAgeGroupDetail", x => x.AgeGroupOtherDetailId);
                    table.ForeignKey(
                        name: "FK_CEAgeGroupDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CEAgeGroupDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    AssumptionDetailId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ProjectId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CEAssumptionDetail", x => x.AssumptionDetailId);
                    table.ForeignKey(
                        name: "FK_CEAssumptionDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CEAssumptionDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    ExpertOtherDetailId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ProjectId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CEFeasibilityExpertOtherDetail", x => x.ExpertOtherDetailId);
                    table.ForeignKey(
                        name: "FK_CEFeasibilityExpertOtherDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CEFeasibilityExpertOtherDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CEFeasibilityExpertOtherDetail_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CEOccupationDetail",
                columns: table => new
                {
                    OccupationOtherDetailId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ProjectId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CEOccupationDetail", x => x.OccupationOtherDetailId);
                    table.ForeignKey(
                        name: "FK_CEOccupationDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CEOccupationDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CEOccupationDetail_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DistrictMultiSelect",
                columns: table => new
                {
                    DistrictMultiSelectId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ProjectId = table.Column<long>(nullable: false),
                    DistrictID = table.Column<long>(nullable: false),
                    DistrictSelectionId = table.Column<long>(nullable: true),
                    ProvinceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DistrictMultiSelect", x => x.DistrictMultiSelectId);
                    table.ForeignKey(
                        name: "FK_DistrictMultiSelect_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DistrictMultiSelect_DistrictDetail_DistrictID",
                        column: x => x.DistrictID,
                        principalTable: "DistrictDetail",
                        principalColumn: "DistrictID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DistrictMultiSelect_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    DonorCEId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_DonorCriteriaDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DonorCriteriaDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    DonorEligibilityDetailId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ProjectId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonorEligibilityCriteria", x => x.DonorEligibilityDetailId);
                    table.ForeignKey(
                        name: "FK_DonorEligibilityCriteria_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DonorEligibilityCriteria_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    EligibilityId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ProjectId = table.Column<long>(nullable: false),
                    DonorCriteriaMet = table.Column<bool>(nullable: true),
                    EligibilityDealine = table.Column<bool>(nullable: true),
                    CoPartnership = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EligibilityCriteriaDetail", x => x.EligibilityId);
                    table.ForeignKey(
                        name: "FK_EligibilityCriteriaDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EligibilityCriteriaDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    FeasibilityId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_FeasibilityCriteriaDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FeasibilityCriteriaDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    FinancialCriteriaDetailId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_FinancialCriteriaDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinancialCriteriaDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    FinancialProjectDetailId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ProjectId = table.Column<long>(nullable: false),
                    ProjectSelectionId = table.Column<long>(nullable: true),
                    ProjectName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialProjectDetail", x => x.FinancialProjectDetailId);
                    table.ForeignKey(
                        name: "FK_FinancialProjectDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinancialProjectDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    PriorityCriteriaDetailId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_PriorityCriteriaDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PriorityCriteriaDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    PriorityOtherDetailId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ProjectId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriorityOtherDetail", x => x.PriorityOtherDetailId);
                    table.ForeignKey(
                        name: "FK_PriorityOtherDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PriorityOtherDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    IsDeleted = table.Column<bool>(nullable: true),
                    ProjectId = table.Column<long>(nullable: false),
                    UserID = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectActivitiesControl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectActivitiesControl_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectActivitiesControl_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    ProjectAreaId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_ProjectArea_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectArea_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    PCId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ProjectId = table.Column<long>(nullable: false),
                    ProjectDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectCommunication", x => x.PCId);
                    table.ForeignKey(
                        name: "FK_ProjectCommunication_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectCommunication_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    IsDeleted = table.Column<bool>(nullable: true),
                    ProjectId = table.Column<long>(nullable: false),
                    UserID = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectHiringControl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectHiringControl_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectHiringControl_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    ProjectJobId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ProjectJobCode = table.Column<string>(nullable: true),
                    ProjectJobName = table.Column<string>(nullable: true),
                    ProjectId = table.Column<long>(nullable: false)
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
                    IsDeleted = table.Column<bool>(nullable: true),
                    ProjectId = table.Column<long>(nullable: false),
                    UserID = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectLogisticsControl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectLogisticsControl_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectLogisticsControl_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    IsDeleted = table.Column<bool>(nullable: true),
                    ProjectId = table.Column<long>(nullable: false),
                    UserID = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectOpportunityControl", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectOpportunityControl_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectOpportunityControl_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    ProjectOtherDetailId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_ProjectOtherDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectOtherDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    ProjectPhaseTimeId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ProjectId = table.Column<long>(nullable: false),
                    ProjectPhaseDetailsId = table.Column<long>(nullable: false),
                    PhaseStartData = table.Column<DateTime>(nullable: true),
                    PhaseEndDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectPhaseTime", x => x.ProjectPhaseTimeId);
                    table.ForeignKey(
                        name: "FK_ProjectPhaseTime_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectPhaseTime_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    ProjectProgramId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ProjectId = table.Column<long>(nullable: false),
                    ProgramId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectProgram", x => x.ProjectProgramId);
                    table.ForeignKey(
                        name: "FK_ProjectProgram_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectProgram_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    ProjectProposaldetailId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_ProjectProposalDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectProposalDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    ProjectSectorId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ProjectId = table.Column<long>(nullable: false),
                    SectorId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectSector", x => x.ProjectSectorId);
                    table.ForeignKey(
                        name: "FK_ProjectSector_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectSector_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                name: "ProvinceMultiSelect",
                columns: table => new
                {
                    ProvinceMultiSelectId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ProjectId = table.Column<long>(nullable: false),
                    ProvinceId = table.Column<int>(nullable: false),
                    ProvinceSelectionId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProvinceMultiSelect", x => x.ProvinceMultiSelectId);
                    table.ForeignKey(
                        name: "FK_ProvinceMultiSelect_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProvinceMultiSelect_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
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
                name: "PurposeofInitiativeCriteria",
                columns: table => new
                {
                    ProductServiceId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    Women = table.Column<bool>(nullable: true),
                    ProjectId = table.Column<long>(nullable: false),
                    Children = table.Column<bool>(nullable: true),
                    Awareness = table.Column<bool>(nullable: true),
                    Education = table.Column<bool>(nullable: true),
                    DrugAbuses = table.Column<bool>(nullable: true),
                    Right = table.Column<bool>(nullable: true),
                    Culture = table.Column<bool>(nullable: true),
                    Music = table.Column<bool>(nullable: true),
                    Documentaries = table.Column<bool>(nullable: true),
                    InvestigativeJournalism = table.Column<bool>(nullable: true),
                    HealthAndNutrition = table.Column<bool>(nullable: true),
                    News = table.Column<bool>(nullable: true),
                    SocioPolitiacalDebate = table.Column<bool>(nullable: true),
                    Studies = table.Column<bool>(nullable: true),
                    Reports = table.Column<bool>(nullable: true),
                    CommunityDevelopment = table.Column<bool>(nullable: true),
                    Aggriculture = table.Column<bool>(nullable: true),
                    DRR = table.Column<bool>(nullable: true),
                    ServiceEducation = table.Column<bool>(nullable: true),
                    ServiceHealthAndNutrition = table.Column<bool>(nullable: true),
                    RadioProduction = table.Column<bool>(nullable: true),
                    TVProgram = table.Column<bool>(nullable: true),
                    PrintedMedia = table.Column<bool>(nullable: true),
                    RoundTable = table.Column<bool>(nullable: true),
                    Others = table.Column<bool>(nullable: true),
                    OtherActivity = table.Column<string>(nullable: true),
                    TargetBenificaiaryWomen = table.Column<bool>(nullable: true),
                    TargetBenificiaryMen = table.Column<bool>(nullable: true),
                    TargetBenificiaryAgeGroup = table.Column<bool>(nullable: true),
                    TargetBenificiaryaOccupation = table.Column<bool>(nullable: true),
                    Service = table.Column<bool>(nullable: true),
                    Product = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurposeofInitiativeCriteria", x => x.ProductServiceId);
                    table.ForeignKey(
                        name: "FK_PurposeofInitiativeCriteria_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurposeofInitiativeCriteria_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    RiskCriteriaDetailId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                    OrganizationalDescription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskCriteriaDetail", x => x.RiskCriteriaDetailId);
                    table.ForeignKey(
                        name: "FK_RiskCriteriaDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RiskCriteriaDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
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
                    SecurityConsiderationMultiSelectId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ProjectId = table.Column<long>(nullable: false),
                    SecurityConsiderationId = table.Column<long>(nullable: false),
                    SecurityConsiderationSelectedId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecurityConsiderationMultiSelect", x => x.SecurityConsiderationMultiSelectId);
                    table.ForeignKey(
                        name: "FK_SecurityConsiderationMultiSelect_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SecurityConsiderationMultiSelect_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SecurityConsiderationMultiSelect_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SecurityConsiderationMultiSelect_SecurityConsiderationDetail_SecurityConsiderationId",
                        column: x => x.SecurityConsiderationId,
                        principalTable: "SecurityConsiderationDetail",
                        principalColumn: "SecurityConsiderationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WinProjectDetails",
                columns: table => new
                {
                    WinProjectId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_ContractDetails_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
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
                        name: "FK_ContractDetails_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
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
                    GainLossSelectedAccountId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ChartOfAccountNewId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GainLossSelectedAccounts", x => x.GainLossSelectedAccountId);
                    table.ForeignKey(
                        name: "FK_GainLossSelectedAccounts_ChartOfAccountNew_ChartOfAccountNewId",
                        column: x => x.ChartOfAccountNewId,
                        principalTable: "ChartOfAccountNew",
                        principalColumn: "ChartOfAccountNewId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GainLossSelectedAccounts_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GainLossSelectedAccounts_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "NotesMaster",
                columns: table => new
                {
                    NoteId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ChartOfAccountNewId = table.Column<long>(nullable: false),
                    Narration = table.Column<string>(nullable: true),
                    Notes = table.Column<int>(nullable: false),
                    BlanceType = table.Column<int>(nullable: false),
                    FinancialReportTypeId = table.Column<int>(nullable: false),
                    AccountTypeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotesMaster", x => x.NoteId);
                    table.ForeignKey(
                        name: "FK_NotesMaster_AccountType_AccountTypeId",
                        column: x => x.AccountTypeId,
                        principalTable: "AccountType",
                        principalColumn: "AccountTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NotesMaster_ChartOfAccountNew_ChartOfAccountNewId",
                        column: x => x.ChartOfAccountNewId,
                        principalTable: "ChartOfAccountNew",
                        principalColumn: "ChartOfAccountNewId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotesMaster_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NotesMaster_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PensionDebitAccountMaster",
                columns: table => new
                {
                    PensionDebitAccountId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ChartOfAccountNewId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PensionDebitAccountMaster", x => x.PensionDebitAccountId);
                    table.ForeignKey(
                        name: "FK_PensionDebitAccountMaster_ChartOfAccountNew_ChartOfAccountNewId",
                        column: x => x.ChartOfAccountNewId,
                        principalTable: "ChartOfAccountNew",
                        principalColumn: "ChartOfAccountNewId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PensionDebitAccountMaster_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PensionDebitAccountMaster_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_StoreInventories_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    table.ForeignKey(
                        name: "FK_StoreInventories_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Advances",
                columns: table => new
                {
                    AdvancesId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_Advances_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    table.ForeignKey(
                        name: "FK_Advances_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssignLeaveToEmployee",
                columns: table => new
                {
                    LeaveId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_AssignLeaveToEmployee_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    table.ForeignKey(
                        name: "FK_AssignLeaveToEmployee_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_EmployeeApplyLeave_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    table.ForeignKey(
                        name: "FK_EmployeeApplyLeave_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeAppraisalDetails",
                columns: table => new
                {
                    EmployeeAppraisalDetailsId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_EmployeeAppraisalDetails_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeAppraisalDetails_EmployeeDetail_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeAppraisalDetails_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_EmployeeAttendance_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    table.ForeignKey(
                        name: "FK_EmployeeAttendance_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeContract",
                columns: table => new
                {
                    EmployeeContractId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_EmployeeContract_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    table.ForeignKey(
                        name: "FK_EmployeeContract_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeDocumentDetail",
                columns: table => new
                {
                    DocumentID = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_EmployeeDocumentDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeDocumentDetail_EmployeeDetail_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeDocumentDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeEducations",
                columns: table => new
                {
                    EmployeeEducationsId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_EmployeeEducations_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeEducations_EmployeeDetail_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeEducations_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
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
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_EmployeeHealthInfo_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeHealthInfo_EmployeeDetail_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeHealthInfo_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeHistoryDetail",
                columns: table => new
                {
                    HistoryID = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    EmployeeID = table.Column<int>(nullable: true),
                    HistoryDate = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeHistoryDetail", x => x.HistoryID);
                    table.ForeignKey(
                        name: "FK_EmployeeHistoryDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeHistoryDetail_EmployeeDetail_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeHistoryDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeHistoryOutsideCountry",
                columns: table => new
                {
                    EmployeeHistoryOutsideCountryId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_EmployeeHistoryOutsideCountry_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeHistoryOutsideCountry_EmployeeDetail_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeHistoryOutsideCountry_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeHistoryOutsideOrganization",
                columns: table => new
                {
                    EmployeeHistoryOutsideOrganizationId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_EmployeeHistoryOutsideOrganization_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeHistoryOutsideOrganization_EmployeeDetail_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeHistoryOutsideOrganization_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeInfoReferences",
                columns: table => new
                {
                    EmployeeInfoReferencesId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_EmployeeInfoReferences_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeInfoReferences_EmployeeDetail_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeInfoReferences_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeMonthlyPayroll",
                columns: table => new
                {
                    MonthlyPayrollId = table.Column<long>(type: "serial", nullable: false)
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
                    EmployeeOtherSkillsId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_EmployeeOtherSkills_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeOtherSkills_EmployeeDetail_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeOtherSkills_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeePayroll",
                columns: table => new
                {
                    PayrollId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_EmployeePayroll_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                        name: "FK_EmployeePayroll_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    EmployeePayrollAccountId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_EmployeePayrollAccountHead_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeePayrollAccountHead_EmployeeDetail_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeePayrollAccountHead_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeePayrollAccountHead_PayrollAccountHead_PayrollHeadId",
                        column: x => x.PayrollHeadId,
                        principalTable: "PayrollAccountHead",
                        principalColumn: "PayrollHeadId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeePayrollForMonth",
                columns: table => new
                {
                    EmployeePaymentTypesId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    OfficeId = table.Column<int>(nullable: false),
                    CurrencyId = table.Column<int>(nullable: false),
                    FinancialYearDate = table.Column<DateTime>(nullable: false),
                    EmployeeID = table.Column<int>(nullable: false),
                    EmployeeName = table.Column<string>(nullable: true),
                    PaymentType = table.Column<int>(nullable: false),
                    WorkingDays = table.Column<int>(nullable: false),
                    PresentDays = table.Column<int>(nullable: false),
                    AbsentDays = table.Column<int>(nullable: false),
                    LeaveDays = table.Column<int>(nullable: false),
                    TotalWorkHours = table.Column<int>(nullable: false),
                    HourlyRate = table.Column<double>(nullable: true),
                    TotalGeneralAmount = table.Column<double>(nullable: true),
                    TotalAllowance = table.Column<double>(nullable: true),
                    TotalDeduction = table.Column<double>(nullable: true),
                    GrossSalary = table.Column<double>(nullable: true),
                    OverTimeHours = table.Column<double>(nullable: true),
                    IsApproved = table.Column<bool>(nullable: false),
                    PensionRate = table.Column<double>(nullable: true),
                    PensionAmount = table.Column<double>(nullable: true),
                    SalaryTax = table.Column<double>(nullable: true),
                    NetSalary = table.Column<double>(nullable: true),
                    AdvanceAmount = table.Column<double>(nullable: false),
                    IsAdvanceApproved = table.Column<bool>(nullable: false),
                    IsAdvanceRecovery = table.Column<bool>(nullable: false),
                    AdvanceRecoveryAmount = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePayrollForMonth", x => x.EmployeePaymentTypesId);
                    table.ForeignKey(
                        name: "FK_EmployeePayrollForMonth_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeePayrollForMonth_EmployeeDetail_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeePayrollForMonth_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeePayrollMonth",
                columns: table => new
                {
                    MonthlyPayrollId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_EmployeePayrollMonth_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                        name: "FK_EmployeePayrollMonth_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    EmployeeProfessionalId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_EmployeeProfessionalDetail_AttendanceGroupMaster_AttendanceGroupId",
                        column: x => x.AttendanceGroupId,
                        principalTable: "AttendanceGroupMaster",
                        principalColumn: "AttendanceGroupId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeProfessionalDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
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
                        name: "FK_EmployeeProfessionalDetail_EmployeeContractType_EmployeeContractTypeId",
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
                        name: "FK_EmployeeProfessionalDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
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
                    EmployeeRelativeInfoId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_EmployeeRelativeInfo_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeRelativeInfo_EmployeeDetail_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeRelativeInfo_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeSalaryBudget",
                columns: table => new
                {
                    EmployeeSalaryBudgetId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_EmployeeSalaryBudget_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeSalaryBudget_EmployeeDetail_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeSalaryBudget_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeSalaryDetails",
                columns: table => new
                {
                    SalaryId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_EmployeeSalaryDetails_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    table.ForeignKey(
                        name: "FK_EmployeeSalaryDetails_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExistInterviewDetails",
                columns: table => new
                {
                    ExistInterviewDetailsId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_ExistInterviewDetails_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExistInterviewDetails_EmployeeDetail_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExistInterviewDetails_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InterviewDetails",
                columns: table => new
                {
                    InterviewDetailsId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_InterviewDetails_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InterviewDetails_EmployeeDetail_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InterviewDetails_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectAssignTo",
                columns: table => new
                {
                    ProjectAssignToId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ProjectId = table.Column<long>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectAssignTo", x => x.ProjectAssignToId);
                    table.ForeignKey(
                        name: "FK_ProjectAssignTo_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectAssignTo_EmployeeDetail_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectAssignTo_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectAssignTo_ProjectDetail_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetail",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BudgetReceivedAmount",
                columns: table => new
                {
                    BudgetReceivedAmountId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    BudgetReceivalbeId = table.Column<long>(nullable: false),
                    ReceiptId = table.Column<long>(nullable: false),
                    ReceivedDate = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetReceivedAmount", x => x.BudgetReceivedAmountId);
                    table.ForeignKey(
                        name: "FK_BudgetReceivedAmount_BudgetReceivable_BudgetReceivalbeId",
                        column: x => x.BudgetReceivalbeId,
                        principalTable: "BudgetReceivable",
                        principalColumn: "BudgetReceivalbeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BudgetReceivedAmount_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BudgetReceivedAmount_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssignActivity",
                columns: table => new
                {
                    AssignActivityId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ProjectId = table.Column<long>(nullable: false),
                    TaskId = table.Column<int>(nullable: false),
                    ActivityId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    PlannedStartDate = table.Column<DateTime>(nullable: true),
                    PlannedEndDate = table.Column<DateTime>(nullable: true),
                    ActualStartDate = table.Column<DateTime>(nullable: true),
                    ActualEndDate = table.Column<DateTime>(nullable: true),
                    Status = table.Column<string>(maxLength: 20, nullable: true),
                    ApprovedStatus = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignActivity", x => x.AssignActivityId);
                    table.ForeignKey(
                        name: "FK_AssignActivity_ActivityMaster_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "ActivityMaster",
                        principalColumn: "ActivityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignActivity_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssignActivity_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssignActivity_ProjectDetails_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetails",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignActivity_TaskMaster_TaskId",
                        column: x => x.TaskId,
                        principalTable: "TaskMaster",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignActivity_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectCommunicationAttachment",
                columns: table => new
                {
                    PCAId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    PCId = table.Column<long>(nullable: false),
                    ProjectId = table.Column<long>(nullable: false),
                    FileName = table.Column<string>(nullable: true),
                    FilePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectCommunicationAttachment", x => x.PCAId);
                    table.ForeignKey(
                        name: "FK_ProjectCommunicationAttachment_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectCommunicationAttachment_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    BudgetLineId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_JobDetails_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobDetails_JobPhases_JobPhaseId",
                        column: x => x.JobPhaseId,
                        principalTable: "JobPhases",
                        principalColumn: "JobPhaseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobDetails_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StoreItemGroups",
                columns: table => new
                {
                    ItemGroupId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ItemGroupCode = table.Column<string>(nullable: true),
                    ItemGroupName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    InventoryId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreItemGroups", x => x.ItemGroupId);
                    table.ForeignKey(
                        name: "FK_StoreItemGroups_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreItemGroups_StoreInventories_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "StoreInventories",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreItemGroups_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeMonthlyAttendance",
                columns: table => new
                {
                    MonthlyAttendanceId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_EmployeeMonthlyAttendance_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeMonthlyAttendance_EmployeeDetail_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeMonthlyAttendance_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeePaymentTypes",
                columns: table => new
                {
                    EmployeePaymentTypesId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_EmployeePaymentTypes_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeePaymentTypes_EmployeeDetail_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeePaymentTypes_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HRJobInterviewers",
                columns: table => new
                {
                    HRJobInterviewerId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    EmployeeId = table.Column<int>(nullable: false),
                    InterviewDetailsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HRJobInterviewers", x => x.HRJobInterviewerId);
                    table.ForeignKey(
                        name: "FK_HRJobInterviewers_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    table.ForeignKey(
                        name: "FK_HRJobInterviewers_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InterviewLanguages",
                columns: table => new
                {
                    InterviewLanguagesId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_InterviewLanguages_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InterviewLanguages_InterviewDetails_InterviewDetailsId",
                        column: x => x.InterviewDetailsId,
                        principalTable: "InterviewDetails",
                        principalColumn: "InterviewDetailsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InterviewLanguages_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InterviewTechnicalQuestion",
                columns: table => new
                {
                    InterviewTechnicalQuestionId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    InterviewDetailsId = table.Column<int>(nullable: false),
                    Question = table.Column<string>(nullable: true),
                    Answer = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterviewTechnicalQuestion", x => x.InterviewTechnicalQuestionId);
                    table.ForeignKey(
                        name: "FK_InterviewTechnicalQuestion_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InterviewTechnicalQuestion_InterviewDetails_InterviewDetailsId",
                        column: x => x.InterviewDetailsId,
                        principalTable: "InterviewDetails",
                        principalColumn: "InterviewDetailsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InterviewTechnicalQuestion_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InterviewTrainings",
                columns: table => new
                {
                    InterviewTrainingsId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_InterviewTrainings_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InterviewTrainings_InterviewDetails_InterviewDetailsId",
                        column: x => x.InterviewDetailsId,
                        principalTable: "InterviewDetails",
                        principalColumn: "InterviewDetailsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InterviewTrainings_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RatingBasedCriteria",
                columns: table => new
                {
                    RatingBasedCriteriaId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    InterviewDetailsId = table.Column<int>(nullable: false),
                    CriteriaQuestion = table.Column<string>(nullable: true),
                    Rating = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingBasedCriteria", x => x.RatingBasedCriteriaId);
                    table.ForeignKey(
                        name: "FK_RatingBasedCriteria_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RatingBasedCriteria_InterviewDetails_InterviewDetailsId",
                        column: x => x.InterviewDetailsId,
                        principalTable: "InterviewDetails",
                        principalColumn: "InterviewDetailsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RatingBasedCriteria_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssignActivityApproveBy",
                columns: table => new
                {
                    AssignActivityApprovedById = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AssignActivityId = table.Column<long>(nullable: false),
                    ApprovedById = table.Column<string>(nullable: true),
                    Status = table.Column<string>(maxLength: 20, nullable: true),
                    Date = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignActivityApproveBy", x => x.AssignActivityApprovedById);
                    table.ForeignKey(
                        name: "FK_AssignActivityApproveBy_AspNetUsers_ApprovedById",
                        column: x => x.ApprovedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssignActivityApproveBy_AssignActivity_AssignActivityId",
                        column: x => x.AssignActivityId,
                        principalTable: "AssignActivity",
                        principalColumn: "AssignActivityId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssignActivityFeedback",
                columns: table => new
                {
                    FeedbackId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    AssignActivityId = table.Column<long>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Feedback = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssignActivityFeedback", x => x.FeedbackId);
                    table.ForeignKey(
                        name: "FK_AssignActivityFeedback_AssignActivity_AssignActivityId",
                        column: x => x.AssignActivityId,
                        principalTable: "AssignActivity",
                        principalColumn: "AssignActivityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssignActivityFeedback_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssignActivityFeedback_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssignActivityFeedback_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectActivityDetail",
                columns: table => new
                {
                    ActivityId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_ProjectActivityDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectActivityDetail_EmployeeDetail_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectActivityDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
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
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_ProjectHiringRequestDetail_ProjectBudgetLineDetail_BudgetLineId",
                        column: x => x.BudgetLineId,
                        principalTable: "ProjectBudgetLineDetail",
                        principalColumn: "BudgetLineId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectHiringRequestDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
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
                        name: "FK_ProjectHiringRequestDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
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
                    VoucherNo = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_VoucherDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
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
                        name: "FK_VoucherDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
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
                    IsDeleted = table.Column<bool>(nullable: true),
                    JobId = table.Column<long>(nullable: true),
                    IsInvoiceApproved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceApproval", x => x.InvoiceApprovalId);
                    table.ForeignKey(
                        name: "FK_InvoiceApproval_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvoiceApproval_JobDetails_JobId",
                        column: x => x.JobId,
                        principalTable: "JobDetails",
                        principalColumn: "JobId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InvoiceApproval_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
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
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_InvoiceGeneration_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    table.ForeignKey(
                        name: "FK_InvoiceGeneration_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
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
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_JobPriceDetails_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JobPriceDetails_JobDetails_JobId",
                        column: x => x.JobId,
                        principalTable: "JobDetails",
                        principalColumn: "JobId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobPriceDetails_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_ScheduleDetails_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
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
                        name: "FK_ScheduleDetails_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
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
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_InventoryItems_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    table.ForeignKey(
                        name: "FK_InventoryItems_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ActivityDocumentsDetail",
                columns: table => new
                {
                    ActtivityDocumentId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_ActivityDocumentsDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ActivityDocumentsDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    ExtensionId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                    table.ForeignKey(
                        name: "FK_ProjectActivityExtensions_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectActivityExtensions_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectActivityProvinceDetail",
                columns: table => new
                {
                    ActivityProvinceId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ActivityId = table.Column<long>(nullable: false),
                    ProvinceId = table.Column<int>(nullable: false),
                    DistrictID = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectActivityProvinceDetail", x => x.ActivityProvinceId);
                    table.ForeignKey(
                        name: "FK_ProjectActivityProvinceDetail_ProjectActivityDetail_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "ProjectActivityDetail",
                        principalColumn: "ActivityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectActivityProvinceDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectActivityProvinceDetail_DistrictDetail_DistrictID",
                        column: x => x.DistrictID,
                        principalTable: "DistrictDetail",
                        principalColumn: "DistrictID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectActivityProvinceDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
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
                    EmployeeSalaryAnalyticalInfoId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_EmployeeSalaryAnalyticalInfo_ProjectBudgetLineDetail_BudgetlineId",
                        column: x => x.BudgetlineId,
                        principalTable: "ProjectBudgetLineDetail",
                        principalColumn: "BudgetLineId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeSalaryAnalyticalInfo_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeSalaryAnalyticalInfo_EmployeeDetail_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeSalaryAnalyticalInfo_ProjectHiringRequestDetail_HiringRequestId",
                        column: x => x.HiringRequestId,
                        principalTable: "ProjectHiringRequestDetail",
                        principalColumn: "HiringRequestId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeSalaryAnalyticalInfo_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
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
                    IsDeleted = table.Column<bool>(nullable: true),
                    HiringRequestId = table.Column<long>(nullable: false),
                    EmployeeID = table.Column<int>(nullable: true),
                    IsShortListed = table.Column<bool>(nullable: false),
                    IsSelected = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HiringRequestCandidates", x => x.CandidateId);
                    table.ForeignKey(
                        name: "FK_HiringRequestCandidates_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HiringRequestCandidates_EmployeeDetail_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HiringRequestCandidates_ProjectHiringRequestDetail_HiringRequestId",
                        column: x => x.HiringRequestId,
                        principalTable: "ProjectHiringRequestDetail",
                        principalColumn: "HiringRequestId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HiringRequestCandidates_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "JobHiringDetails",
                columns: table => new
                {
                    JobId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_JobHiringDetails_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                        name: "FK_JobHiringDetails_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
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
                    SalaryPaymentId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_EmployeeSalaryPaymentHistory_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeSalaryPaymentHistory_EmployeeDetail_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeSalaryPaymentHistory_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    PensionPaymentId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_PensionPaymentHistory_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PensionPaymentHistory_EmployeeDetail_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PensionPaymentHistory_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PensionPaymentHistory_VoucherDetail_VoucherNo",
                        column: x => x.VoucherNo,
                        principalTable: "VoucherDetail",
                        principalColumn: "VoucherNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VoucherDocumentDetail",
                columns: table => new
                {
                    VoucherDocumentId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    VoucherNo = table.Column<long>(nullable: false),
                    DocumentFileId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoucherDocumentDetail", x => x.VoucherDocumentId);
                    table.ForeignKey(
                        name: "FK_VoucherDocumentDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VoucherDocumentDetail_DocumentFileDetail_DocumentFileId",
                        column: x => x.DocumentFileId,
                        principalTable: "DocumentFileDetail",
                        principalColumn: "DocumentFileId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VoucherDocumentDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VoucherDocumentDetail_VoucherDetail_VoucherNo",
                        column: x => x.VoucherNo,
                        principalTable: "VoucherDetail",
                        principalColumn: "VoucherNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VoucherTransactions",
                columns: table => new
                {
                    TransactionId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_VoucherTransactions_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
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
                        name: "FK_VoucherTransactions_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
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
                    IsDeleted = table.Column<bool>(nullable: true),
                    ScheduleId = table.Column<long>(nullable: true),
                    TotalMinutes = table.Column<long>(nullable: true),
                    DroppedMinutes = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayoutMinutes", x => x.PlayoutMinuteId);
                    table.ForeignKey(
                        name: "FK_PlayoutMinutes_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PlayoutMinutes_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_StoreItemPurchases_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                        name: "FK_StoreItemPurchases_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreItemPurchases_ProjectDetails_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetails",
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
                    ScheduleId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_InterviewScheduleDetails_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    table.ForeignKey(
                        name: "FK_InterviewScheduleDetails_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ItemPurchaseDocuments",
                columns: table => new
                {
                    DocumentId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    DocumentName = table.Column<string>(nullable: true),
                    File = table.Column<byte[]>(nullable: true),
                    FileType = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    DocumentGuid = table.Column<string>(nullable: true),
                    Purchase = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPurchaseDocuments", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_ItemPurchaseDocuments_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemPurchaseDocuments_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ItemPurchaseDocuments_StoreItemPurchases_Purchase",
                        column: x => x.Purchase,
                        principalTable: "StoreItemPurchases",
                        principalColumn: "PurchaseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseGenerators",
                columns: table => new
                {
                    GeneratorId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    Purchase = table.Column<string>(nullable: false),
                    GeneratorBrand = table.Column<string>(nullable: true),
                    GeneratorModel = table.Column<string>(nullable: true),
                    MakeYear = table.Column<string>(nullable: true),
                    SerialBarCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseGenerators", x => x.GeneratorId);
                    table.ForeignKey(
                        name: "FK_PurchaseGenerators_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseGenerators_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseGenerators_StoreItemPurchases_Purchase",
                        column: x => x.Purchase,
                        principalTable: "StoreItemPurchases",
                        principalColumn: "PurchaseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseVehicles",
                columns: table => new
                {
                    VehicleId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    Purchase = table.Column<string>(nullable: true),
                    VehicleDescription = table.Column<string>(nullable: true),
                    VehicleBrand = table.Column<string>(nullable: true),
                    VehicleModel = table.Column<string>(nullable: true),
                    VehicleMakeYear = table.Column<string>(nullable: true),
                    VehiclePlate = table.Column<string>(nullable: true),
                    VehicleSerialNo = table.Column<string>(nullable: true),
                    VehicleImageName = table.Column<string>(nullable: true),
                    VehicleImageFileName = table.Column<string>(nullable: true),
                    VehicleImageFileType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseVehicles", x => x.VehicleId);
                    table.ForeignKey(
                        name: "FK_PurchaseVehicles_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseVehicles_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseVehicles_StoreItemPurchases_Purchase",
                        column: x => x.Purchase,
                        principalTable: "StoreItemPurchases",
                        principalColumn: "PurchaseId",
                        onDelete: ReferentialAction.Restrict);
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
                    IsDeleted = table.Column<bool>(nullable: true),
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
                        name: "FK_StorePurchaseOrders_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                        name: "FK_StorePurchaseOrders_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StorePurchaseOrders_StoreItemPurchases_Purchase",
                        column: x => x.Purchase,
                        principalTable: "StoreItemPurchases",
                        principalColumn: "PurchaseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VehicleLocations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Vehicle = table.Column<int>(nullable: false),
                    Latitude = table.Column<long>(nullable: false),
                    Longitude = table.Column<long>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleLocations_PurchaseVehicles_Vehicle",
                        column: x => x.Vehicle,
                        principalTable: "PurchaseVehicles",
                        principalColumn: "VehicleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehicleMileages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Vehicle = table.Column<int>(nullable: false),
                    Mileage = table.Column<long>(nullable: false),
                    Verified = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleMileages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VehicleMileages_PurchaseVehicles_Vehicle",
                        column: x => x.Vehicle,
                        principalTable: "PurchaseVehicles",
                        principalColumn: "VehicleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MotorMaintenances",
                columns: table => new
                {
                    MaintenanceId = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    Order = table.Column<string>(nullable: true),
                    Generator = table.Column<int>(nullable: false),
                    Vehicle = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    StoreName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotorMaintenances", x => x.MaintenanceId);
                    table.ForeignKey(
                        name: "FK_MotorMaintenances_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MotorMaintenances_PurchaseGenerators_Generator",
                        column: x => x.Generator,
                        principalTable: "PurchaseGenerators",
                        principalColumn: "GeneratorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MotorMaintenances_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MotorMaintenances_StorePurchaseOrders_Order",
                        column: x => x.Order,
                        principalTable: "StorePurchaseOrders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MotorMaintenances_PurchaseVehicles_Vehicle",
                        column: x => x.Vehicle,
                        principalTable: "PurchaseVehicles",
                        principalColumn: "VehicleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MotorSpareParts",
                columns: table => new
                {
                    PartId = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    Order = table.Column<string>(nullable: true),
                    Generator = table.Column<int>(nullable: false),
                    Vehicle = table.Column<int>(nullable: false),
                    PartName = table.Column<string>(nullable: true),
                    PartDescription = table.Column<string>(nullable: true),
                    PartUsed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotorSpareParts", x => x.PartId);
                    table.ForeignKey(
                        name: "FK_MotorSpareParts_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MotorSpareParts_PurchaseGenerators_Generator",
                        column: x => x.Generator,
                        principalTable: "PurchaseGenerators",
                        principalColumn: "GeneratorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MotorSpareParts_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MotorSpareParts_StorePurchaseOrders_Order",
                        column: x => x.Order,
                        principalTable: "StorePurchaseOrders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MotorSpareParts_PurchaseVehicles_Vehicle",
                        column: x => x.Vehicle,
                        principalTable: "PurchaseVehicles",
                        principalColumn: "VehicleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VehicleFuel",
                columns: table => new
                {
                    FuelId = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    Order = table.Column<string>(nullable: true),
                    Vehicle = table.Column<int>(nullable: false),
                    Generator = table.Column<int>(nullable: false),
                    FuelQuantity = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleFuel", x => x.FuelId);
                    table.ForeignKey(
                        name: "FK_VehicleFuel_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehicleFuel_PurchaseGenerators_Generator",
                        column: x => x.Generator,
                        principalTable: "PurchaseGenerators",
                        principalColumn: "GeneratorId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VehicleFuel_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehicleFuel_StorePurchaseOrders_Order",
                        column: x => x.Order,
                        principalTable: "StorePurchaseOrders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VehicleFuel_PurchaseVehicles_Vehicle",
                        column: x => x.Vehicle,
                        principalTable: "PurchaseVehicles",
                        principalColumn: "VehicleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AccountFilterType",
                columns: new[] { "AccountFilterTypeId", "AccountFilterTypeName", "CreatedById", "CreatedDate", "IsDeleted", "ModifiedById", "ModifiedDate" },
                values: new object[,]
                {
                    { 2, "Salary Account", null, null, false, null, null },
                    { 1, "Inventory Account", null, null, false, null, null }
                });

            migrationBuilder.InsertData(
                table: "AccountHeadType",
                columns: new[] { "AccountHeadTypeId", "AccountHeadTypeName", "CreatedById", "CreatedDate", "IsCreditBalancetype", "IsDeleted", "ModifiedById", "ModifiedDate" },
                values: new object[,]
                {
                    { 5, "Expense", null, null, false, false, null, null },
                    { 1, "Assets", null, null, false, false, null, null },
                    { 3, "Donors Equity", null, null, true, false, null, null },
                    { 2, "Liabilities", null, null, true, false, null, null },
                    { 4, "Income", null, null, true, false, null, null }
                });

            migrationBuilder.InsertData(
                table: "AccountLevel",
                columns: new[] { "AccountLevelId", "AccountLevelName" },
                values: new object[,]
                {
                    { 1, "Main Level Accounts" },
                    { 2, "Control Level Accounts" },
                    { 3, "Sub Level Accounts" },
                    { 4, "Input Level Accounts" }
                });

            migrationBuilder.InsertData(
                table: "ActivityTypes",
                columns: new[] { "ActivityTypeId", "ActivityName", "CreatedById", "CreatedDate", "IsDeleted", "ModifiedById", "ModifiedDate" },
                values: new object[,]
                {
                    { 2L, "Production", null, null, false, null, null },
                    { 1L, "Broadcasting", null, null, false, null, null }
                });

            migrationBuilder.InsertData(
                table: "ApplicationPages",
                columns: new[] { "PageId", "CreatedById", "CreatedDate", "IsDeleted", "ModifiedById", "ModifiedDate", "ModuleId", "ModuleName", "PageName" },
                values: new object[,]
                {
                    { 58, null, null, false, null, null, 7, "AccountingNew", "Income" },
                    { 65, null, null, false, null, null, 6, "Marketing", "Jobs" },
                    { 64, null, null, false, null, null, 6, "Marketing", "UnitRates" },
                    { 63, null, null, false, null, null, 6, "Marketing", "Clients" },
                    { 62, null, null, false, null, null, 7, "AccountingNew", "Vouchers" },
                    { 61, null, null, false, null, null, 7, "AccountingNew", "IncomeExpenseReport" },
                    { 60, null, null, false, null, null, 7, "AccountingNew", "BalanceSheet" },
                    { 59, null, null, false, null, null, 7, "AccountingNew", "Expense" },
                    { 57, null, null, false, null, null, 7, "AccountingNew", "Liabilities" },
                    { 51, null, null, false, null, null, 6, "Marketing", "Phase" },
                    { 55, null, null, false, null, null, 6, "Marketing", "ActivityType" },
                    { 54, null, null, false, null, null, 6, "Marketing", "MediaCategory" },
                    { 53, null, null, false, null, null, 6, "Marketing", "Medium" },
                    { 52, null, null, false, null, null, 6, "Marketing", "Nature" },
                    { 66, null, null, false, null, null, 6, "Marketing", "Contracts" },
                    { 50, null, null, false, null, null, 6, "Marketing", "Quality" },
                    { 49, null, null, false, null, null, 6, "Marketing", "TimeCategory" },
                    { 48, null, null, false, null, null, 5, "Store", "DepreciationReport" },
                    { 56, null, null, false, null, null, 7, "AccountingNew", "Assets" },
                    { 67, null, null, false, null, null, 8, "Projects", "MyProjects" },
                    { 73, null, null, false, null, null, 6, "Marketing", "Policy" },
                    { 69, null, null, false, null, null, 8, "Projects", "ProjectDetails" },
                    { 88, null, null, false, null, null, 2, "Code", "AttendanceGroupMaster" },
                    { 87, null, null, false, null, null, 2, "Code", "PensionDebitAccount" },
                    { 86, null, null, false, null, null, 8, "Projects", "HiringRequests" },
                    { 85, null, null, false, null, null, 7, "AccountingNew", "VoucherSummaryReport" },
                    { 84, null, null, false, null, null, 8, "Projects", "ProjectPeople" },
                    { 83, null, null, false, null, null, 8, "Projects", "ProjectIndicators" },
                    { 82, null, null, false, null, null, 8, "Projects", "ProposalReport" },
                    { 81, null, null, false, null, null, 8, "Projects", "BroadCastPolicy" },
                    { 68, null, null, false, null, null, 8, "Projects", "Donors" },
                    { 80, null, null, false, null, null, 8, "Projects", "ProjectBudgetLine" },
                    { 78, null, null, false, null, null, 8, "Projects", "ProjectDashboard" },
                    { 77, null, null, false, null, null, 6, "Marketing", "Scheduler" },
                    { 76, null, null, false, null, null, 6, "Marketing", "Channel" },
                    { 74, null, null, false, null, null, 8, "Projects", "ProjectJobs" },
                    { 47, null, null, false, null, null, 5, "Store", "ProcurementSummary" },
                    { 72, null, null, false, null, null, 6, "Marketing", "Producer" },
                    { 71, null, null, false, null, null, 8, "Projects", "CriteriaEvaluation" },
                    { 70, null, null, false, null, null, 8, "Projects", "Proposal" },
                    { 79, null, null, false, null, null, 8, "Projects", "ProjectCashFlow" },
                    { 46, null, null, false, null, null, 5, "Store", "Store" },
                    { 75, null, null, false, null, null, 8, "Projects", "ProjectActivities" },
                    { 44, null, null, false, null, null, 5, "Store", "StoreSourceCodes" },
                    { 19, null, null, false, null, null, 2, "Code", "SalaryHead" },
                    { 18, null, null, false, null, null, 2, "Code", "JobGrade" },
                    { 17, null, null, false, null, null, 2, "Code", "Designation" },
                    { 16, null, null, false, null, null, 2, "Code", "Qualification" },
                    { 15, null, null, false, null, null, 2, "Code", "Department" },
                    { 14, null, null, false, null, null, 2, "Code", "Profession" },
                    { 13, null, null, false, null, null, 2, "Code", "LeaveReason" },
                    { 12, null, null, false, null, null, 2, "Code", "ExchangeRate" },
                    { 11, null, null, false, null, null, 2, "Code", "EmailSettings" },
                    { 10, null, null, false, null, null, 2, "Code", "TechnicalQuestions" },
                    { 9, null, null, false, null, null, 2, "Code", "AppraisalQuestions" },
                    { 8, null, null, false, null, null, 2, "Code", "EmployeeContract" },
                    { 7, null, null, false, null, null, 2, "Code", "PensionRate" },
                    { 6, null, null, false, null, null, 2, "Code", "FinancialYear" },
                    { 5, null, null, false, null, null, 2, "Code", "OfficeCodes" },
                    { 4, null, null, false, null, null, 2, "Code", "CurrencyCodes" },
                    { 2, null, null, false, null, null, 2, "Code", "ChartOfAccount" },
                    { 1, null, null, false, null, null, 1, "Users", "Users" },
                    { 45, null, null, false, null, null, 5, "Store", "PaymentTypes" },
                    { 20, null, null, false, null, null, 2, "Code", "SalaryTaxReportContent" },
                    { 21, null, null, false, null, null, 2, "Code", "SetPayrollAccount" },
                    { 3, null, null, false, null, null, 2, "Code", "JournalCodes" },
                    { 23, null, null, false, null, null, 3, "Accounting", "Journal" },
                    { 42, null, null, false, null, null, 4, "HR", "Summary" },
                    { 40, null, null, false, null, null, 4, "HR", "EmployeeAppraisal" },
                    { 39, null, null, false, null, null, 4, "HR", "Interview" },
                    { 38, null, null, false, null, null, 4, "HR", "Jobs" },
                    { 37, null, null, false, null, null, 4, "HR", "MonthlyPayrollRegister" },
                    { 41, null, null, false, null, null, 4, "HR", "Advances" },
                    { 35, null, null, false, null, null, 4, "HR", "Attendance" },
                    { 34, null, null, false, null, null, 4, "HR", "Holidays" },
                    { 33, null, null, false, null, null, 4, "HR", "PayrollDailyHours" },
                    { 36, null, null, false, null, null, 4, "HR", "ApproveLeave" },
                    { 31, null, null, false, null, null, 3, "Accounting", "PensionPayments" },
                    { 32, null, null, false, null, null, 4, "HR", "Employees" },
                    { 25, null, null, false, null, null, 3, "Accounting", "BudgetBalance" },
                    { 26, null, null, false, null, null, 3, "Accounting", "TrialBalance" },
                    { 27, null, null, false, null, null, 3, "Accounting", "FinancialReport" },
                    { 24, null, null, false, null, null, 3, "Accounting", "LedgerStatement" },
                    { 43, null, null, false, null, null, 5, "Store", "Categories" },
                    { 29, null, null, false, null, null, 3, "Accounting", "ExchangeGainLoss" },
                    { 30, null, null, false, null, null, 3, "Accounting", "GainLossTransaction" },
                    { 28, null, null, true, null, null, 3, "Accounting", "CategoryPopulator" },
                    { 22, null, null, true, null, null, 3, "Accounting", "Vouchers" }
                });

            migrationBuilder.InsertData(
                table: "CodeType",
                columns: new[] { "CodeTypeId", "CodeTypeName" },
                values: new object[,]
                {
                    { 1, "Organizations" },
                    { 2, "Suppliers" },
                    { 3, "Repair Shops" },
                    { 4, "Individual/Others" },
                    { 5, "Locations/Stores" }
                });

            migrationBuilder.InsertData(
                table: "CountryDetails",
                columns: new[] { "CountryId", "CountryName", "CreatedById", "CreatedDate", "IsDeleted", "ModifiedById", "ModifiedDate" },
                values: new object[,]
                {
                    { 1, "Afghanistan", null, null, false, null, null },
                    { 2, "United States", null, null, false, null, null }
                });

            migrationBuilder.InsertData(
                table: "CurrencyDetails",
                columns: new[] { "CurrencyId", "CreatedById", "CreatedDate", "CurrencyCode", "CurrencyName", "CurrencyRate", "IsDeleted", "ModifiedById", "ModifiedDate", "SalaryTaxFlag", "Status" },
                values: new object[,]
                {
                    { 4, null, null, "USD", "US Dollars", null, false, null, null, false, false },
                    { 2, null, null, "EUR", "European Curency", null, false, null, null, false, false },
                    { 1, null, null, "AFG", "Afghanistan", null, false, null, null, true, false },
                    { 3, null, null, "PKR", "Pakistani Rupees", null, false, null, null, false, true }
                });

            migrationBuilder.InsertData(
                table: "DistrictDetail",
                columns: new[] { "DistrictID", "CreatedById", "CreatedDate", "District", "IsDeleted", "ModifiedById", "ModifiedDate", "ProvinceID" },
                values: new object[,]
                {
                    { 92L, null, null, "Maryland", false, null, null, 52 },
                    { 91L, null, null, "Maine", false, null, null, 51 },
                    { 90L, null, null, "Louisiana", false, null, null, 50 },
                    { 89L, null, null, "Kentucky", false, null, null, 49 },
                    { 88L, null, null, "Lansa", false, null, null, 48 },
                    { 87L, null, null, "Iowa", false, null, null, 47 },
                    { 86L, null, null, "Undia", false, null, null, 46 },
                    { 85L, null, null, "Indiana", false, null, null, 46 },
                    { 84L, null, null, "Illinois", false, null, null, 45 },
                    { 83L, null, null, "Idaho", false, null, null, 44 },
                    { 82L, null, null, "Hawaii", false, null, null, 43 },
                    { 81L, null, null, "Georia", false, null, null, 42 },
                    { 80L, null, null, "Florida", false, null, null, 41 },
                    { 78L, null, null, "Connecticut", false, null, null, 39 },
                    { 77L, null, null, "Colorado", false, null, null, 38 },
                    { 76L, null, null, "Califor", false, null, null, 37 },
                    { 75L, null, null, "California", false, null, null, 37 },
                    { 74L, null, null, "Arkansas", false, null, null, 36 },
                    { 73L, null, null, "Arona", false, null, null, 35 },
                    { 72L, null, null, "Jurors", false, null, null, 35 },
                    { 71L, null, null, "Arizona", false, null, null, 35 },
                    { 70L, null, null, "Alabama", false, null, null, 34 },
                    { 69L, null, null, "Atghar", false, null, null, 33 },
                    { 68L, null, null, "Argahandab", false, null, null, 33 },
                    { 66L, null, null, "Bangi", false, null, null, 31 },
                    { 67L, null, null, "Uakhar", false, null, null, 32 },
                    { 93L, null, null, "Massachusetts", false, null, null, 53 },
                    { 79L, null, null, "Aelaware", false, null, null, 40 },
                    { 94L, null, null, "Michigan", false, null, null, 54 },
                    { 122L, null, null, "Wisconsin", false, null, null, 81 },
                    { 96L, null, null, "Mississippi", false, null, null, 56 },
                    { 65L, null, null, "Balkhab", false, null, null, 30 },
                    { 123L, null, null, "Wyoming", false, null, null, 82 },
                    { 121L, null, null, "Nouit Vinia", false, null, null, 80 },
                    { 120L, null, null, "West Virginia", false, null, null, 80 },
                    { 119L, null, null, "Washinn", false, null, null, 79 },
                    { 118L, null, null, "Virginia", false, null, null, 78 },
                    { 117L, null, null, "Oermont", false, null, null, 77 },
                    { 116L, null, null, "Wtaha", false, null, null, 76 },
                    { 115L, null, null, "Texas", false, null, null, 75 },
                    { 114L, null, null, "Tennessee", false, null, null, 74 },
                    { 113L, null, null, "South Dakota", false, null, null, 73 },
                    { 112L, null, null, "South Carolina", false, null, null, 72 },
                    { 111L, null, null, "Rhode Island", false, null, null, 71 },
                    { 110L, null, null, "Pennsylvania", false, null, null, 70 },
                    { 109L, null, null, "Tregon", false, null, null, 69 },
                    { 108L, null, null, "Oklahoma", false, null, null, 68 },
                    { 107L, null, null, "Ohio", false, null, null, 67 },
                    { 106L, null, null, "North Dakota", false, null, null, 66 },
                    { 105L, null, null, "North Carolina", false, null, null, 65 },
                    { 104L, null, null, "New York", false, null, null, 64 },
                    { 103L, null, null, "New Mexico", false, null, null, 63 },
                    { 102L, null, null, "New Jersey", false, null, null, 62 },
                    { 101L, null, null, "New Hampshire", false, null, null, 61 },
                    { 100L, null, null, "Yevada", false, null, null, 60 },
                    { 99L, null, null, "Nebraska", false, null, null, 59 },
                    { 98L, null, null, "Montana", false, null, null, 58 },
                    { 97L, null, null, "Missouri", false, null, null, 57 },
                    { 95L, null, null, "Minnesota", false, null, null, 55 },
                    { 64L, null, null, "Aybak", false, null, null, 29 },
                    { 50L, null, null, "Gardez", false, null, null, 25 },
                    { 62L, null, null, "Kohi Safi", false, null, null, 28 },
                    { 29L, null, null, "Bagrami", false, null, null, 13 },
                    { 28L, null, null, "Deh Sabz", false, null, null, 13 },
                    { 27L, null, null, "Chahar Asyab", false, null, null, 13 },
                    { 26L, null, null, "GuzDarzabara", false, null, null, 12 },
                    { 25L, null, null, "Fayzabad", false, null, null, 12 },
                    { 24L, null, null, "Aqcha", false, null, null, 12 },
                    { 1L, null, null, "Jawand", false, null, null, 1 },
                    { 22L, null, null, "Garmsir", false, null, null, 10 },
                    { 21L, null, null, "Baghran", false, null, null, 10 },
                    { 20L, null, null, "Tulak", false, null, null, 9 },
                    { 19L, null, null, "Shahrak", false, null, null, 9 },
                    { 18L, null, null, "Andar", false, null, null, 8 },
                    { 17L, null, null, "Ajristan", false, null, null, 8 },
                    { 30L, null, null, "Daman", false, null, null, 14 },
                    { 16L, null, null, "Bilchiragh", false, null, null, 7 },
                    { 14L, null, null, "Andkhoy", false, null, null, 7 },
                    { 13L, null, null, "Bakwa", false, null, null, 6 },
                    { 12L, null, null, "Bala Buluk", false, null, null, 6 },
                    { 11L, null, null, "Gizab", false, null, null, 5 },
                    { 10L, null, null, "Bamyan", false, null, null, 4 },
                    { 9L, null, null, "Shibar", false, null, null, 4 },
                    { 8L, null, null, "Panjab", false, null, null, 4 },
                    { 7L, null, null, "Chahar Kint", false, null, null, 3 },
                    { 6L, null, null, "Chahar Bolak", false, null, null, 3 },
                    { 5L, null, null, "Dahana i Ghuri", false, null, null, 2 },
                    { 4L, null, null, "Baghlani Jadid", false, null, null, 2 },
                    { 3L, null, null, "Qadis", false, null, null, 1 },
                    { 63L, null, null, "Salang", false, null, null, 28 },
                    { 15L, null, null, "Almar", false, null, null, 7 },
                    { 31L, null, null, "Ghorak", false, null, null, 14 },
                    { 23L, null, null, "Chishti Sharif", false, null, null, 11 },
                    { 33L, null, null, "Bak", false, null, null, 16 },
                    { 61L, null, null, "Jabal Saraj", false, null, null, 28 },
                    { 60L, null, null, "Chaharikar", false, null, null, 28 },
                    { 59L, null, null, "Bagram", false, null, null, 28 },
                    { 58L, null, null, "Anaba", false, null, null, 27 },
                    { 57L, null, null, "Chang", false, null, null, 26 },
                    { 32L, null, null, "Alasay", false, null, null, 15 },
                    { 55L, null, null, "Barmal", false, null, null, 26 },
                    { 54L, null, null, "Dila", false, null, null, 26 },
                    { 53L, null, null, "Wuza Zadran", false, null, null, 25 },
                    { 52L, null, null, "Zurmat", false, null, null, 25 },
                    { 51L, null, null, "Jaji", false, null, null, 25 },
                    { 49L, null, null, "Mandol", false, null, null, 24 },
                    { 48L, null, null, "Kamdesh", false, null, null, 24 },
                    { 56L, null, null, "Kal", false, null, null, 26 },
                    { 46L, null, null, "Kang", false, null, null, 23 },
                    { 34L, null, null, "Gurbuz", false, null, null, 16 },
                    { 47L, null, null, "Chakhansur", false, null, null, 23 },
                    { 35L, null, null, "Asadabad", false, null, null, 17 },
                    { 36L, null, null, "Bar Kunar", false, null, null, 17 },
                    { 37L, null, null, "Ali Abad", false, null, null, 18 },
                    { 38L, null, null, "Archi", false, null, null, 18 },
                    { 39L, null, null, "Alingar", false, null, null, 19 },
                    { 2L, null, null, "Muqur", false, null, null, 1 },
                    { 41L, null, null, "Baraki Barak", false, null, null, 20 },
                    { 42L, null, null, "Charkh", false, null, null, 20 },
                    { 43L, null, null, "Maidan Wardak", false, null, null, 21 },
                    { 44L, null, null, "Achin", false, null, null, 22 },
                    { 45L, null, null, "Bati Kot", false, null, null, 22 },
                    { 40L, null, null, "Alishing", false, null, null, 19 }
                });

            migrationBuilder.InsertData(
                table: "EmailType",
                columns: new[] { "EmailTypeId", "CreatedById", "CreatedDate", "EmailTypeName", "IsDeleted", "ModifiedById", "ModifiedDate" },
                values: new object[,]
                {
                    { 1, null, null, "General", false, null, null },
                    { 2, null, null, "Bidding Panel", false, null, null }
                });

            migrationBuilder.InsertData(
                table: "EmployeeContractType",
                columns: new[] { "EmployeeContractTypeId", "EmployeeContractTypeName" },
                values: new object[,]
                {
                    { 1, "Probationary" },
                    { 2, "PartTime" },
                    { 3, "Permanent" }
                });

            migrationBuilder.InsertData(
                table: "EmployeeType",
                columns: new[] { "EmployeeTypeId", "CreatedById", "CreatedDate", "EmployeeTypeName", "IsDeleted", "ModifiedById", "ModifiedDate" },
                values: new object[,]
                {
                    { 1, null, null, "Prospective", false, null, null },
                    { 2, null, null, "Active", false, null, null },
                    { 3, null, null, "Terminated", false, null, null }
                });

            migrationBuilder.InsertData(
                table: "FinancialYearDetail",
                columns: new[] { "FinancialYearId", "CreatedById", "CreatedDate", "Description", "EndDate", "FinancialYearName", "IsDefault", "IsDeleted", "ModifiedById", "ModifiedDate", "StartDate" },
                values: new object[] { 1, null, null, null, new DateTime(2019, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "2019 Financial Year", true, false, null, null, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "GenderConsiderationDetail",
                columns: new[] { "GenderConsiderationId", "CreatedById", "CreatedDate", "GenderConsiderationName", "IsDeleted", "ModifiedById", "ModifiedDate" },
                values: new object[,]
                {
                    { 7L, null, null, "5 % F - 95 % M Poor", false, null, null },
                    { 6L, null, null, "10 % F - 90 % M Poor", false, null, null },
                    { 5L, null, null, "20 % F - 80 % M Poor", false, null, null },
                    { 8L, null, null, "0 % F - 100 % M Poor", false, null, null },
                    { 4L, null, null, "25 % F - 75 % M Poor", false, null, null },
                    { 3L, null, null, "30 % F - 70 % M Good", false, null, null },
                    { 2L, null, null, "40 % F - 60 % M Very Good", false, null, null },
                    { 1L, null, null, "50 % F - 50 % M Excellent", false, null, null }
                });

            migrationBuilder.InsertData(
                table: "LanguageDetail",
                columns: new[] { "LanguageId", "CreatedById", "CreatedDate", "IsDeleted", "LanguageName", "ModifiedById", "ModifiedDate" },
                values: new object[,]
                {
                    { 4, null, null, false, "French", null, null },
                    { 7, null, null, false, "Russian", null, null },
                    { 1, null, null, false, "Arabic", null, null },
                    { 2, null, null, false, "Dari", null, null },
                    { 10, null, null, false, "Urdu", null, null },
                    { 9, null, null, false, "Turkmani", null, null },
                    { 3, null, null, false, "English", null, null },
                    { 5, null, null, false, "German", null, null },
                    { 6, null, null, false, "Pashto", null, null },
                    { 11, null, null, false, "Uzbek", null, null },
                    { 8, null, null, false, "Turkish", null, null }
                });

            migrationBuilder.InsertData(
                table: "LeaveReasonDetail",
                columns: new[] { "LeaveReasonId", "CreatedById", "CreatedDate", "IsDeleted", "ModifiedById", "ModifiedDate", "ReasonName", "Unit" },
                values: new object[,]
                {
                    { 1, null, null, false, null, null, "Casual Leave", 12 },
                    { 3, null, null, false, null, null, "Maternity Leave", 90 },
                    { 2, null, null, false, null, null, "Emergency Leave", 6 }
                });

            migrationBuilder.InsertData(
                table: "OfficeDetail",
                columns: new[] { "OfficeId", "CreatedById", "CreatedDate", "FaxNo", "IsDeleted", "ModifiedById", "ModifiedDate", "OfficeCode", "OfficeKey", "OfficeName", "PhoneNo", "SupervisorName" },
                values: new object[] { 1, null, null, null, false, null, null, "A0001", "AF", "Afghanistan", null, null });

            migrationBuilder.InsertData(
                table: "PayrollAccountHead",
                columns: new[] { "PayrollHeadId", "AccountNo", "CreatedById", "CreatedDate", "Description", "IsDeleted", "ModifiedById", "ModifiedDate", "PayrollHeadName", "PayrollHeadTypeId", "TransactionTypeId" },
                values: new object[,]
                {
                    { 4, null, null, null, null, true, null, null, "Gross Salary", 3, 2 },
                    { 3, null, null, null, null, false, null, null, "Salary Tax", 2, 1 },
                    { 2, null, null, null, null, false, null, null, "Advance Deduction", 2, 1 },
                    { 1, null, null, null, null, false, null, null, "Net Salary", 3, 1 },
                    { 5, null, null, null, null, false, null, null, "Pension", 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "ProjectPhaseDetails",
                columns: new[] { "ProjectPhaseDetailsId", "CreatedById", "CreatedDate", "IsDeleted", "ModifiedById", "ModifiedDate", "ProjectPhase" },
                values: new object[] { 1L, null, null, false, null, null, "Data Entry" });

            migrationBuilder.InsertData(
                table: "ReceiptType",
                columns: new[] { "ReceiptTypeId", "CreatedById", "CreatedDate", "IsDeleted", "ModifiedById", "ModifiedDate", "ReceiptTypeName" },
                values: new object[,]
                {
                    { 2, null, null, false, null, null, "Transfers" },
                    { 3, null, null, false, null, null, "Donation" },
                    { 4, null, null, false, null, null, "Take Over" },
                    { 5, null, null, false, null, null, "Loan" },
                    { 6, null, null, false, null, null, "Return" },
                    { 1, null, null, false, null, null, "Purchased" },
                    { 7, null, null, false, null, null, "Other" }
                });

            migrationBuilder.InsertData(
                table: "SalaryHeadDetails",
                columns: new[] { "SalaryHeadId", "AccountNo", "CreatedById", "CreatedDate", "Description", "HeadName", "HeadTypeId", "IsDeleted", "ModifiedById", "ModifiedDate", "TransactionTypeId" },
                values: new object[,]
                {
                    { 1, null, null, null, "Tr Allowance", "Tr Allowance", 1, false, null, null, 2 },
                    { 10, null, null, null, "Other2Allowance", "Other2Allowance", 1, false, null, null, 2 },
                    { 9, null, null, null, "Other1Allowance", "Other1Allowance", 1, false, null, null, 2 },
                    { 8, null, null, null, "Medical Allowance", "Medical Allowance", 1, false, null, null, 2 },
                    { 7, null, null, null, "Other Deduction", "Other Deduction", 2, false, null, null, 1 },
                    { 11, null, null, null, "Basic Pay (In hours)", "Basic Pay (In hours)", 3, false, null, null, 2 },
                    { 6, null, null, null, "Other Allowance", "Other Allowance", 1, false, null, null, 2 },
                    { 5, null, null, null, "Security Deduction", "Security Deduction", 2, false, null, null, 1 },
                    { 4, null, null, null, "Capacity Building Deduction", "Capacity Building Deduction", 2, false, null, null, 1 },
                    { 3, null, null, null, "Fine Deduction", "Fine Deduction", 2, false, null, null, 1 },
                    { 2, null, null, null, "Food Allowance", "Food Allowance", 1, false, null, null, 2 }
                });

            migrationBuilder.InsertData(
                table: "SecurityConsiderationDetail",
                columns: new[] { "SecurityConsiderationId", "CreatedById", "CreatedDate", "IsDeleted", "ModifiedById", "ModifiedDate", "SecurityConsiderationName" },
                values: new object[,]
                {
                    { 7L, null, null, false, null, null, "Resources can be deployed partially" },
                    { 1L, null, null, false, null, null, "Project Staff Cannot Visit Project Site" },
                    { 2L, null, null, false, null, null, "Beneficiaries cannot be reached" },
                    { 3L, null, null, false, null, null, "Resources cannot be deployed" },
                    { 4L, null, null, false, null, null, "Threat exit for future (Highly)" },
                    { 11L, null, null, false, null, null, "Future Threats expected" },
                    { 10L, null, null, false, null, null, "No obstacle for deploying Resources & office" },
                    { 9L, null, null, false, null, null, "No barrier for staff to access the area" },
                    { 8L, null, null, false, null, null, "Future Threats exits" },
                    { 6L, null, null, false, null, null, "Bonfires can be reached partially" },
                    { 5L, null, null, false, null, null, "Project staff access the are partially" }
                });

            migrationBuilder.InsertData(
                table: "SecurityDetail",
                columns: new[] { "SecurityId", "CreatedById", "CreatedDate", "IsDeleted", "ModifiedById", "ModifiedDate", "SecurityName" },
                values: new object[,]
                {
                    { 2L, null, null, false, null, null, "Partially Insecure" },
                    { 3L, null, null, false, null, null, "Secure (Green Area)" },
                    { 1L, null, null, false, null, null, "Insecure" }
                });

            migrationBuilder.InsertData(
                table: "StatusAtTimeOfIssue",
                columns: new[] { "StatusAtTimeOfIssueId", "CreatedById", "CreatedDate", "IsDeleted", "ModifiedById", "ModifiedDate", "StatusName" },
                values: new object[,]
                {
                    { 1, null, null, false, null, null, "New" },
                    { 2, null, null, false, null, null, "Useable" },
                    { 3, null, null, false, null, null, "To Repair" },
                    { 4, null, null, false, null, null, "Damage" },
                    { 5, null, null, false, null, null, "Sold" },
                    { 6, null, null, false, null, null, "Stolen" },
                    { 7, null, null, false, null, null, "Handover" },
                    { 8, null, null, false, null, null, "Demolished" },
                    { 9, null, null, false, null, null, "Broken" }
                });

            migrationBuilder.InsertData(
                table: "StrengthConsiderationDetail",
                columns: new[] { "StrengthConsiderationId", "CreatedById", "CreatedDate", "IsDeleted", "ModifiedById", "ModifiedDate", "StrengthConsiderationName" },
                values: new object[,]
                {
                    { 2L, null, null, false, null, null, "Not Gender Friendly" },
                    { 1L, null, null, false, null, null, "Gender Friendly" },
                    { 3L, null, null, false, null, null, "Not Applicable" }
                });

            migrationBuilder.InsertData(
                table: "VoucherType",
                columns: new[] { "VoucherTypeId", "VoucherTypeName" },
                values: new object[,]
                {
                    { 1, "Adjustment" },
                    { 2, "Journal" }
                });

            migrationBuilder.InsertData(
                table: "Department",
                columns: new[] { "DepartmentId", "CreatedById", "CreatedDate", "DepartmentName", "IsDeleted", "ModifiedById", "ModifiedDate", "OfficeCode", "OfficeId" },
                values: new object[] { 1, null, null, "Administration", false, null, null, null, 1 });

            migrationBuilder.InsertData(
                table: "ProvinceDetails",
                columns: new[] { "ProvinceId", "CountryId", "CreatedById", "CreatedDate", "IsDeleted", "ModifiedById", "ModifiedDate", "ProvinceName" },
                values: new object[,]
                {
                    { 61, 2, null, null, false, null, null, "Nevada" },
                    { 60, 2, null, null, false, null, null, "Nebraska" },
                    { 59, 2, null, null, false, null, null, "Montana" },
                    { 58, 2, null, null, false, null, null, "Missouri" },
                    { 57, 2, null, null, false, null, null, "Mississippi" },
                    { 56, 2, null, null, false, null, null, "Minnesota" },
                    { 55, 2, null, null, false, null, null, "Michigan" },
                    { 54, 2, null, null, false, null, null, "Massachusetts" },
                    { 53, 2, null, null, false, null, null, "Maryland" },
                    { 52, 2, null, null, false, null, null, "Maine" },
                    { 51, 2, null, null, false, null, null, "Louisiana" },
                    { 50, 2, null, null, false, null, null, "Kentucky" },
                    { 49, 2, null, null, false, null, null, "Kansas" },
                    { 48, 2, null, null, false, null, null, "Iowa" },
                    { 47, 2, null, null, false, null, null, "Indiana" },
                    { 46, 2, null, null, false, null, null, "Illinois" },
                    { 45, 2, null, null, false, null, null, "Idaho" },
                    { 62, 2, null, null, false, null, null, "New Hampshire" },
                    { 44, 2, null, null, false, null, null, "Hawaii" },
                    { 63, 2, null, null, false, null, null, "New Jersey" },
                    { 65, 2, null, null, false, null, null, "New York" },
                    { 82, 2, null, null, false, null, null, "Wisconsin" },
                    { 81, 2, null, null, false, null, null, "West Virginia" },
                    { 80, 2, null, null, false, null, null, "Washington" },
                    { 79, 2, null, null, false, null, null, "Virginia" },
                    { 78, 2, null, null, false, null, null, "Vermont" },
                    { 77, 2, null, null, false, null, null, "Utah" },
                    { 76, 2, null, null, false, null, null, "Texas" },
                    { 75, 2, null, null, false, null, null, "Tennessee" },
                    { 74, 2, null, null, false, null, null, "South Dakota" },
                    { 73, 2, null, null, false, null, null, "South Carolina" },
                    { 72, 2, null, null, false, null, null, "Rhode Island" },
                    { 71, 2, null, null, false, null, null, "Pennsylvania" },
                    { 70, 2, null, null, false, null, null, "Oregon" },
                    { 69, 2, null, null, false, null, null, "Oklahoma" },
                    { 68, 2, null, null, false, null, null, "Ohio" },
                    { 67, 2, null, null, false, null, null, "North Dakota" },
                    { 66, 2, null, null, false, null, null, "North Carolina" },
                    { 64, 2, null, null, false, null, null, "New Mexico" },
                    { 43, 2, null, null, false, null, null, "Georgia" },
                    { 42, 2, null, null, false, null, null, "Florida" },
                    { 41, 2, null, null, false, null, null, "Delaware" },
                    { 18, 1, null, null, false, null, null, "Kunduz" },
                    { 17, 1, null, null, false, null, null, "Kunar" },
                    { 16, 1, null, null, false, null, null, "Khost" },
                    { 15, 1, null, null, false, null, null, "Kapisa" },
                    { 14, 1, null, null, false, null, null, "Kandahar" },
                    { 13, 1, null, null, false, null, null, "Kabul" },
                    { 12, 1, null, null, false, null, null, "Jowzjan" },
                    { 11, 1, null, null, false, null, null, "Herat" },
                    { 10, 1, null, null, false, null, null, "Helmand" },
                    { 9, 1, null, null, false, null, null, "Ghor" },
                    { 8, 1, null, null, false, null, null, "Ghazni" },
                    { 7, 1, null, null, false, null, null, "Faryab" },
                    { 6, 1, null, null, false, null, null, "Farah" },
                    { 5, 1, null, null, false, null, null, "Daykundi" },
                    { 4, 1, null, null, false, null, null, "Bamyan" },
                    { 3, 1, null, null, false, null, null, "Balkh" },
                    { 2, 1, null, null, false, null, null, "Baghlan" },
                    { 19, 1, null, null, false, null, null, "Laghman" },
                    { 20, 1, null, null, false, null, null, "Logar" },
                    { 21, 1, null, null, false, null, null, "Maidan Wardak" },
                    { 22, 1, null, null, false, null, null, "Nangarhar" },
                    { 40, 2, null, null, false, null, null, "Connecticut" },
                    { 39, 2, null, null, false, null, null, "Colorado" },
                    { 38, 2, null, null, false, null, null, "California" },
                    { 37, 2, null, null, false, null, null, "Arkansas" },
                    { 36, 2, null, null, false, null, null, "Arizona" },
                    { 35, 2, null, null, false, null, null, "Alaska" },
                    { 34, 1, null, null, false, null, null, "Alabama" },
                    { 33, 1, null, null, false, null, null, "Zabul" },
                    { 83, 2, null, null, false, null, null, "Wyoming" },
                    { 32, 1, null, null, false, null, null, "Urozgan" },
                    { 30, 1, null, null, false, null, null, "Sar-e Pol" },
                    { 29, 1, null, null, false, null, null, "Samangan" },
                    { 28, 1, null, null, false, null, null, "Parwan" },
                    { 27, 1, null, null, false, null, null, "Panjshir" },
                    { 26, 1, null, null, false, null, null, "Paktika" },
                    { 25, 1, null, null, false, null, null, "Paktia" },
                    { 24, 1, null, null, false, null, null, "Nuristan" },
                    { 23, 1, null, null, false, null, null, "Nimruz" },
                    { 31, 1, null, null, false, null, null, "Takhar" },
                    { 1, 1, null, null, false, null, null, "Badghis" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccountFilterType_CreatedById",
                table: "AccountFilterType",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AccountFilterType_ModifiedById",
                table: "AccountFilterType",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_AccountHeadType_CreatedById",
                table: "AccountHeadType",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AccountHeadType_ModifiedById",
                table: "AccountHeadType",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_AccountType_AccountHeadTypeId",
                table: "AccountType",
                column: "AccountHeadTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountType_CreatedById",
                table: "AccountType",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AccountType_ModifiedById",
                table: "AccountType",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityDocumentsDetail_ActivityId",
                table: "ActivityDocumentsDetail",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityDocumentsDetail_CreatedById",
                table: "ActivityDocumentsDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityDocumentsDetail_ModifiedById",
                table: "ActivityDocumentsDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityDocumentsDetail_StatusId",
                table: "ActivityDocumentsDetail",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityMaster_CreatedById",
                table: "ActivityMaster",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityMaster_ModifiedById",
                table: "ActivityMaster",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityMaster_TaskId",
                table: "ActivityMaster",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityStatusDetail_CreatedById",
                table: "ActivityStatusDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityStatusDetail_ModifiedById",
                table: "ActivityStatusDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityTypes_CreatedById",
                table: "ActivityTypes",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityTypes_ModifiedById",
                table: "ActivityTypes",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Advances_CreatedById",
                table: "Advances",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Advances_CurrencyId",
                table: "Advances",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Advances_EmployeeId",
                table: "Advances",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Advances_ModifiedById",
                table: "Advances",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_AgreeDisagreePermission_CreatedById",
                table: "AgreeDisagreePermission",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AgreeDisagreePermission_ModifiedById",
                table: "AgreeDisagreePermission",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_AgreeDisagreePermission_PageId",
                table: "AgreeDisagreePermission",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_AnalyticalDetail_CreatedById",
                table: "AnalyticalDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AnalyticalDetail_ModifiedById",
                table: "AnalyticalDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationPages_CreatedById",
                table: "ApplicationPages",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationPages_ModifiedById",
                table: "ApplicationPages",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_AppraisalGeneralQuestions_CreatedById",
                table: "AppraisalGeneralQuestions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AppraisalGeneralQuestions_ModifiedById",
                table: "AppraisalGeneralQuestions",
                column: "ModifiedById");

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
                name: "IX_ApproveRejectPermission_CreatedById",
                table: "ApproveRejectPermission",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ApproveRejectPermission_ModifiedById",
                table: "ApproveRejectPermission",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ApproveRejectPermission_PageId",
                table: "ApproveRejectPermission",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_AreaDetail_CreatedById",
                table: "AreaDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AreaDetail_ModifiedById",
                table: "AreaDetail",
                column: "ModifiedById");

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
                name: "IX_AssignActivity_ActivityId",
                table: "AssignActivity",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignActivity_CreatedById",
                table: "AssignActivity",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AssignActivity_ModifiedById",
                table: "AssignActivity",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_AssignActivity_ProjectId",
                table: "AssignActivity",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignActivity_TaskId",
                table: "AssignActivity",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignActivity_UserId",
                table: "AssignActivity",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignActivityApproveBy_ApprovedById",
                table: "AssignActivityApproveBy",
                column: "ApprovedById");

            migrationBuilder.CreateIndex(
                name: "IX_AssignActivityApproveBy_AssignActivityId",
                table: "AssignActivityApproveBy",
                column: "AssignActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignActivityFeedback_AssignActivityId",
                table: "AssignActivityFeedback",
                column: "AssignActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignActivityFeedback_CreatedById",
                table: "AssignActivityFeedback",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AssignActivityFeedback_ModifiedById",
                table: "AssignActivityFeedback",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_AssignActivityFeedback_UserId",
                table: "AssignActivityFeedback",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AssignLeaveToEmployee_CreatedById",
                table: "AssignLeaveToEmployee",
                column: "CreatedById");

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
                name: "IX_AssignLeaveToEmployee_ModifiedById",
                table: "AssignLeaveToEmployee",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceGroupMaster_CreatedById",
                table: "AttendanceGroupMaster",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceGroupMaster_ModifiedById",
                table: "AttendanceGroupMaster",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetLineType_CreatedById",
                table: "BudgetLineType",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetLineType_ModifiedById",
                table: "BudgetLineType",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetReceivable_BudgetLineId",
                table: "BudgetReceivable",
                column: "BudgetLineId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetReceivable_CreatedById",
                table: "BudgetReceivable",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetReceivable_ModifiedById",
                table: "BudgetReceivable",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetReceivable_ProjectId",
                table: "BudgetReceivable",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetReceivedAmount_BudgetReceivalbeId",
                table: "BudgetReceivedAmount",
                column: "BudgetReceivalbeId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetReceivedAmount_CreatedById",
                table: "BudgetReceivedAmount",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetReceivedAmount_ModifiedById",
                table: "BudgetReceivedAmount",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CreatedById",
                table: "Categories",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ModifiedById",
                table: "Categories",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryPopulator_AccountTypeId",
                table: "CategoryPopulator",
                column: "AccountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryPopulator_CreatedById",
                table: "CategoryPopulator",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryPopulator_ModifiedById",
                table: "CategoryPopulator",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_CEAgeGroupDetail_CreatedById",
                table: "CEAgeGroupDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_CEAgeGroupDetail_ModifiedById",
                table: "CEAgeGroupDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_CEAgeGroupDetail_ProjectId",
                table: "CEAgeGroupDetail",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_CEAssumptionDetail_CreatedById",
                table: "CEAssumptionDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_CEAssumptionDetail_ModifiedById",
                table: "CEAssumptionDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_CEAssumptionDetail_ProjectId",
                table: "CEAssumptionDetail",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_CEFeasibilityExpertOtherDetail_CreatedById",
                table: "CEFeasibilityExpertOtherDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_CEFeasibilityExpertOtherDetail_ModifiedById",
                table: "CEFeasibilityExpertOtherDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_CEFeasibilityExpertOtherDetail_ProjectId",
                table: "CEFeasibilityExpertOtherDetail",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_CEOccupationDetail_CreatedById",
                table: "CEOccupationDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_CEOccupationDetail_ModifiedById",
                table: "CEOccupationDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_CEOccupationDetail_ProjectId",
                table: "CEOccupationDetail",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Channel_CreatedById",
                table: "Channel",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Channel_MediumId",
                table: "Channel",
                column: "MediumId");

            migrationBuilder.CreateIndex(
                name: "IX_Channel_ModifiedById",
                table: "Channel",
                column: "ModifiedById");

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
                name: "IX_ChartOfAccountNew_CreatedById",
                table: "ChartOfAccountNew",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ChartOfAccountNew_ModifiedById",
                table: "ChartOfAccountNew",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ClientDetails_CategoryId",
                table: "ClientDetails",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientDetails_CreatedById",
                table: "ClientDetails",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ClientDetails_ModifiedById",
                table: "ClientDetails",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ContractDetails_ActivityTypeId",
                table: "ContractDetails",
                column: "ActivityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractDetails_ClientId",
                table: "ContractDetails",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractDetails_CreatedById",
                table: "ContractDetails",
                column: "CreatedById");

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
                name: "IX_ContractDetails_ModifiedById",
                table: "ContractDetails",
                column: "ModifiedById");

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

            migrationBuilder.CreateIndex(
                name: "IX_CountryDetails_CreatedById",
                table: "CountryDetails",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_CountryDetails_ModifiedById",
                table: "CountryDetails",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyDetails_CreatedById",
                table: "CurrencyDetails",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyDetails_ModifiedById",
                table: "CurrencyDetails",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Department_CreatedById",
                table: "Department",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Department_ModifiedById",
                table: "Department",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Department_OfficeId",
                table: "Department",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_DesignationDetail_CreatedById",
                table: "DesignationDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DesignationDetail_ModifiedById",
                table: "DesignationDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_DistrictDetail_CreatedById",
                table: "DistrictDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DistrictDetail_ModifiedById",
                table: "DistrictDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_DistrictMultiSelect_CreatedById",
                table: "DistrictMultiSelect",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DistrictMultiSelect_DistrictID",
                table: "DistrictMultiSelect",
                column: "DistrictID");

            migrationBuilder.CreateIndex(
                name: "IX_DistrictMultiSelect_ModifiedById",
                table: "DistrictMultiSelect",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_DistrictMultiSelect_ProjectId",
                table: "DistrictMultiSelect",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_DistrictMultiSelect_ProvinceId",
                table: "DistrictMultiSelect",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentFileDetail_CreatedById",
                table: "DocumentFileDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentFileDetail_ModifiedById",
                table: "DocumentFileDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_DonorCriteriaDetail_CreatedById",
                table: "DonorCriteriaDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DonorCriteriaDetail_ModifiedById",
                table: "DonorCriteriaDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_DonorCriteriaDetail_ProjectId",
                table: "DonorCriteriaDetail",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_DonorDetail_CreatedById",
                table: "DonorDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DonorDetail_ModifiedById",
                table: "DonorDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_DonorEligibilityCriteria_CreatedById",
                table: "DonorEligibilityCriteria",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DonorEligibilityCriteria_ModifiedById",
                table: "DonorEligibilityCriteria",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_DonorEligibilityCriteria_ProjectId",
                table: "DonorEligibilityCriteria",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_EligibilityCriteriaDetail_CreatedById",
                table: "EligibilityCriteriaDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EligibilityCriteriaDetail_ModifiedById",
                table: "EligibilityCriteriaDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_EligibilityCriteriaDetail_ProjectId",
                table: "EligibilityCriteriaDetail",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailSettingDetail_CreatedById",
                table: "EmailSettingDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmailSettingDetail_EmailTypeId",
                table: "EmailSettingDetail",
                column: "EmailTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailSettingDetail_ModifiedById",
                table: "EmailSettingDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmailType_CreatedById",
                table: "EmailType",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmailType_ModifiedById",
                table: "EmailType",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAnalyticalDetail_CreatedById",
                table: "EmployeeAnalyticalDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAnalyticalDetail_ModifiedById",
                table: "EmployeeAnalyticalDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeApplyLeave_CreatedById",
                table: "EmployeeApplyLeave",
                column: "CreatedById");

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
                name: "IX_EmployeeApplyLeave_ModifiedById",
                table: "EmployeeApplyLeave",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAppraisalDetails_CreatedById",
                table: "EmployeeAppraisalDetails",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAppraisalDetails_EmployeeId",
                table: "EmployeeAppraisalDetails",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAppraisalDetails_ModifiedById",
                table: "EmployeeAppraisalDetails",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAppraisalQuestions_AppraisalGeneralQuestionsId",
                table: "EmployeeAppraisalQuestions",
                column: "AppraisalGeneralQuestionsId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAppraisalQuestions_CreatedById",
                table: "EmployeeAppraisalQuestions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAppraisalQuestions_ModifiedById",
                table: "EmployeeAppraisalQuestions",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAppraisalTeamMember_CreatedById",
                table: "EmployeeAppraisalTeamMember",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAppraisalTeamMember_ModifiedById",
                table: "EmployeeAppraisalTeamMember",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAttendance_CreatedById",
                table: "EmployeeAttendance",
                column: "CreatedById");

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
                name: "IX_EmployeeAttendance_ModifiedById",
                table: "EmployeeAttendance",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeContract_CreatedById",
                table: "EmployeeContract",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeContract_EmployeeId",
                table: "EmployeeContract",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeContract_Grade",
                table: "EmployeeContract",
                column: "Grade");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeContract_ModifiedById",
                table: "EmployeeContract",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetail_CountryId",
                table: "EmployeeDetail",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetail_CreatedById",
                table: "EmployeeDetail",
                column: "CreatedById");

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
                name: "IX_EmployeeDetailDT_CreatedById",
                table: "EmployeeDetailDT",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetailDT_ModifiedById",
                table: "EmployeeDetailDT",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDocumentDetail_CreatedById",
                table: "EmployeeDocumentDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDocumentDetail_EmployeeID",
                table: "EmployeeDocumentDetail",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDocumentDetail_ModifiedById",
                table: "EmployeeDocumentDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEducations_CreatedById",
                table: "EmployeeEducations",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEducations_EmployeeID",
                table: "EmployeeEducations",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEducations_ModifiedById",
                table: "EmployeeEducations",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEvaluation_CreatedById",
                table: "EmployeeEvaluation",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEvaluation_ModifiedById",
                table: "EmployeeEvaluation",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEvaluationTraining_CreatedById",
                table: "EmployeeEvaluationTraining",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeEvaluationTraining_ModifiedById",
                table: "EmployeeEvaluationTraining",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeHealthInfo_CreatedById",
                table: "EmployeeHealthInfo",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeHealthInfo_EmployeeId",
                table: "EmployeeHealthInfo",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeHealthInfo_ModifiedById",
                table: "EmployeeHealthInfo",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeHealthQuestion_CreatedById",
                table: "EmployeeHealthQuestion",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeHealthQuestion_ModifiedById",
                table: "EmployeeHealthQuestion",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeHistoryDetail_CreatedById",
                table: "EmployeeHistoryDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeHistoryDetail_EmployeeID",
                table: "EmployeeHistoryDetail",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeHistoryDetail_ModifiedById",
                table: "EmployeeHistoryDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeHistoryOutsideCountry_CreatedById",
                table: "EmployeeHistoryOutsideCountry",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeHistoryOutsideCountry_EmployeeID",
                table: "EmployeeHistoryOutsideCountry",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeHistoryOutsideCountry_ModifiedById",
                table: "EmployeeHistoryOutsideCountry",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeHistoryOutsideOrganization_CreatedById",
                table: "EmployeeHistoryOutsideOrganization",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeHistoryOutsideOrganization_EmployeeID",
                table: "EmployeeHistoryOutsideOrganization",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeHistoryOutsideOrganization_ModifiedById",
                table: "EmployeeHistoryOutsideOrganization",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeInfoReferences_CreatedById",
                table: "EmployeeInfoReferences",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeInfoReferences_EmployeeID",
                table: "EmployeeInfoReferences",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeInfoReferences_ModifiedById",
                table: "EmployeeInfoReferences",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLanguages_CreatedById",
                table: "EmployeeLanguages",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLanguages_LanguageId",
                table: "EmployeeLanguages",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLanguages_ModifiedById",
                table: "EmployeeLanguages",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeMonthlyAttendance_AdvanceId",
                table: "EmployeeMonthlyAttendance",
                column: "AdvanceId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeMonthlyAttendance_CreatedById",
                table: "EmployeeMonthlyAttendance",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeMonthlyAttendance_EmployeeId",
                table: "EmployeeMonthlyAttendance",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeMonthlyAttendance_ModifiedById",
                table: "EmployeeMonthlyAttendance",
                column: "ModifiedById");

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
                name: "IX_EmployeeOtherSkills_CreatedById",
                table: "EmployeeOtherSkills",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeOtherSkills_EmployeeID",
                table: "EmployeeOtherSkills",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeOtherSkills_ModifiedById",
                table: "EmployeeOtherSkills",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePaymentTypes_AdvanceId",
                table: "EmployeePaymentTypes",
                column: "AdvanceId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePaymentTypes_CreatedById",
                table: "EmployeePaymentTypes",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePaymentTypes_EmployeeID",
                table: "EmployeePaymentTypes",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePaymentTypes_ModifiedById",
                table: "EmployeePaymentTypes",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayroll_CreatedById",
                table: "EmployeePayroll",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayroll_CurrencyId",
                table: "EmployeePayroll",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayroll_EmployeeID",
                table: "EmployeePayroll",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayroll_ModifiedById",
                table: "EmployeePayroll",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayroll_SalaryHeadId",
                table: "EmployeePayroll",
                column: "SalaryHeadId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayrollAccountHead_CreatedById",
                table: "EmployeePayrollAccountHead",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayrollAccountHead_EmployeeId",
                table: "EmployeePayrollAccountHead",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayrollAccountHead_ModifiedById",
                table: "EmployeePayrollAccountHead",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayrollAccountHead_PayrollHeadId",
                table: "EmployeePayrollAccountHead",
                column: "PayrollHeadId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayrollDetailTest_CreatedById",
                table: "EmployeePayrollDetailTest",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayrollDetailTest_ModifiedById",
                table: "EmployeePayrollDetailTest",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayrollForMonth_CreatedById",
                table: "EmployeePayrollForMonth",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayrollForMonth_EmployeeID",
                table: "EmployeePayrollForMonth",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayrollForMonth_ModifiedById",
                table: "EmployeePayrollForMonth",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayrollMonth_CreatedById",
                table: "EmployeePayrollMonth",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayrollMonth_CurrencyId",
                table: "EmployeePayrollMonth",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayrollMonth_EmployeeID",
                table: "EmployeePayrollMonth",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayrollMonth_ModifiedById",
                table: "EmployeePayrollMonth",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePayrollMonth_SalaryHeadId",
                table: "EmployeePayrollMonth",
                column: "SalaryHeadId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePensionRate_CreatedById",
                table: "EmployeePensionRate",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePensionRate_FinancialYearId",
                table: "EmployeePensionRate",
                column: "FinancialYearId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePensionRate_ModifiedById",
                table: "EmployeePensionRate",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProfessionalDetail_AttendanceGroupId",
                table: "EmployeeProfessionalDetail",
                column: "AttendanceGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProfessionalDetail_CreatedById",
                table: "EmployeeProfessionalDetail",
                column: "CreatedById");

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
                name: "IX_EmployeeProfessionalDetail_ModifiedById",
                table: "EmployeeProfessionalDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProfessionalDetail_OfficeId",
                table: "EmployeeProfessionalDetail",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProfessionalDetail_ProfessionId",
                table: "EmployeeProfessionalDetail",
                column: "ProfessionId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRelativeInfo_CreatedById",
                table: "EmployeeRelativeInfo",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRelativeInfo_EmployeeID",
                table: "EmployeeRelativeInfo",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeRelativeInfo_ModifiedById",
                table: "EmployeeRelativeInfo",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaryAnalyticalInfo_BudgetlineId",
                table: "EmployeeSalaryAnalyticalInfo",
                column: "BudgetlineId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaryAnalyticalInfo_CreatedById",
                table: "EmployeeSalaryAnalyticalInfo",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaryAnalyticalInfo_EmployeeID",
                table: "EmployeeSalaryAnalyticalInfo",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaryAnalyticalInfo_HiringRequestId",
                table: "EmployeeSalaryAnalyticalInfo",
                column: "HiringRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaryAnalyticalInfo_ModifiedById",
                table: "EmployeeSalaryAnalyticalInfo",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaryAnalyticalInfo_ProjectId",
                table: "EmployeeSalaryAnalyticalInfo",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaryBudget_CreatedById",
                table: "EmployeeSalaryBudget",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaryBudget_EmployeeID",
                table: "EmployeeSalaryBudget",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaryBudget_ModifiedById",
                table: "EmployeeSalaryBudget",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaryDetails_CreatedById",
                table: "EmployeeSalaryDetails",
                column: "CreatedById");

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
                name: "IX_EmployeeSalaryDetails_ModifiedById",
                table: "EmployeeSalaryDetails",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaryPaymentHistory_CreatedById",
                table: "EmployeeSalaryPaymentHistory",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaryPaymentHistory_EmployeeId",
                table: "EmployeeSalaryPaymentHistory",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaryPaymentHistory_ModifiedById",
                table: "EmployeeSalaryPaymentHistory",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaryPaymentHistory_VoucherNo",
                table: "EmployeeSalaryPaymentHistory",
                column: "VoucherNo");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeType_CreatedById",
                table: "EmployeeType",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeType_ModifiedById",
                table: "EmployeeType",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_errorlog_CreatedById",
                table: "errorlog",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_errorlog_ModifiedById",
                table: "errorlog",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRateDetail_CreatedById",
                table: "ExchangeRateDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRateDetail_ModifiedById",
                table: "ExchangeRateDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRates_CreatedById",
                table: "ExchangeRates",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRates_Date",
                table: "ExchangeRates",
                column: "Date");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRates_FromCurrency",
                table: "ExchangeRates",
                column: "FromCurrency");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRates_ModifiedById",
                table: "ExchangeRates",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRates_OfficeId",
                table: "ExchangeRates",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRates_ToCurrency",
                table: "ExchangeRates",
                column: "ToCurrency");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRateVerifications_CreatedById",
                table: "ExchangeRateVerifications",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRateVerifications_ModifiedById",
                table: "ExchangeRateVerifications",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ExistInterviewDetails_CreatedById",
                table: "ExistInterviewDetails",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ExistInterviewDetails_EmployeeID",
                table: "ExistInterviewDetails",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_ExistInterviewDetails_ModifiedById",
                table: "ExistInterviewDetails",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_FeasibilityCriteriaDetail_CreatedById",
                table: "FeasibilityCriteriaDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FeasibilityCriteriaDetail_ModifiedById",
                table: "FeasibilityCriteriaDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_FeasibilityCriteriaDetail_ProjectId",
                table: "FeasibilityCriteriaDetail",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialCriteriaDetail_CreatedById",
                table: "FinancialCriteriaDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialCriteriaDetail_ModifiedById",
                table: "FinancialCriteriaDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialCriteriaDetail_ProjectId",
                table: "FinancialCriteriaDetail",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialProjectDetail_CreatedById",
                table: "FinancialProjectDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialProjectDetail_ModifiedById",
                table: "FinancialProjectDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialProjectDetail_ProjectId",
                table: "FinancialProjectDetail",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialYearDetail_CreatedById",
                table: "FinancialYearDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialYearDetail_ModifiedById",
                table: "FinancialYearDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_GainLossSelectedAccounts_ChartOfAccountNewId",
                table: "GainLossSelectedAccounts",
                column: "ChartOfAccountNewId");

            migrationBuilder.CreateIndex(
                name: "IX_GainLossSelectedAccounts_CreatedById",
                table: "GainLossSelectedAccounts",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_GainLossSelectedAccounts_ModifiedById",
                table: "GainLossSelectedAccounts",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_GenderConsiderationDetail_CreatedById",
                table: "GenderConsiderationDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_GenderConsiderationDetail_ModifiedById",
                table: "GenderConsiderationDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_HiringRequestCandidates_CreatedById",
                table: "HiringRequestCandidates",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_HiringRequestCandidates_EmployeeID",
                table: "HiringRequestCandidates",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_HiringRequestCandidates_HiringRequestId",
                table: "HiringRequestCandidates",
                column: "HiringRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_HiringRequestCandidates_ModifiedById",
                table: "HiringRequestCandidates",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_HolidayDetails_CreatedById",
                table: "HolidayDetails",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_HolidayDetails_FinancialYearId",
                table: "HolidayDetails",
                column: "FinancialYearId");

            migrationBuilder.CreateIndex(
                name: "IX_HolidayDetails_ModifiedById",
                table: "HolidayDetails",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_HolidayDetails_OfficeId",
                table: "HolidayDetails",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_HolidayWeeklyDetails_CreatedById",
                table: "HolidayWeeklyDetails",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_HolidayWeeklyDetails_FinancialYearId",
                table: "HolidayWeeklyDetails",
                column: "FinancialYearId");

            migrationBuilder.CreateIndex(
                name: "IX_HolidayWeeklyDetails_ModifiedById",
                table: "HolidayWeeklyDetails",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_HolidayWeeklyDetails_OfficeId",
                table: "HolidayWeeklyDetails",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_HRJobInterviewers_CreatedById",
                table: "HRJobInterviewers",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_HRJobInterviewers_EmployeeId",
                table: "HRJobInterviewers",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_HRJobInterviewers_InterviewDetailsId",
                table: "HRJobInterviewers",
                column: "InterviewDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_HRJobInterviewers_ModifiedById",
                table: "HRJobInterviewers",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewDetails_CreatedById",
                table: "InterviewDetails",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewDetails_EmployeeID",
                table: "InterviewDetails",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewDetails_ModifiedById",
                table: "InterviewDetails",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewLanguages_CreatedById",
                table: "InterviewLanguages",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewLanguages_InterviewDetailsId",
                table: "InterviewLanguages",
                column: "InterviewDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewLanguages_ModifiedById",
                table: "InterviewLanguages",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewScheduleDetails_CreatedById",
                table: "InterviewScheduleDetails",
                column: "CreatedById");

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
                name: "IX_InterviewScheduleDetails_ModifiedById",
                table: "InterviewScheduleDetails",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewTechnicalQuestion_CreatedById",
                table: "InterviewTechnicalQuestion",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewTechnicalQuestion_InterviewDetailsId",
                table: "InterviewTechnicalQuestion",
                column: "InterviewDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewTechnicalQuestion_ModifiedById",
                table: "InterviewTechnicalQuestion",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewTechnicalQuestions_CreatedById",
                table: "InterviewTechnicalQuestions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewTechnicalQuestions_ModifiedById",
                table: "InterviewTechnicalQuestions",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewTrainings_CreatedById",
                table: "InterviewTrainings",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewTrainings_InterviewDetailsId",
                table: "InterviewTrainings",
                column: "InterviewDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewTrainings_ModifiedById",
                table: "InterviewTrainings",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItems_CreatedById",
                table: "InventoryItems",
                column: "CreatedById");

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
                name: "IX_InventoryItems_ModifiedById",
                table: "InventoryItems",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItemType_CreatedById",
                table: "InventoryItemType",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItemType_ModifiedById",
                table: "InventoryItemType",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceApproval_CreatedById",
                table: "InvoiceApproval",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceApproval_JobId",
                table: "InvoiceApproval",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceApproval_ModifiedById",
                table: "InvoiceApproval",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceGeneration_CreatedById",
                table: "InvoiceGeneration",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceGeneration_CurrencyDetailsCurrencyId",
                table: "InvoiceGeneration",
                column: "CurrencyDetailsCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceGeneration_JobId",
                table: "InvoiceGeneration",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceGeneration_ModifiedById",
                table: "InvoiceGeneration",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPurchaseDocuments_CreatedById",
                table: "ItemPurchaseDocuments",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPurchaseDocuments_ModifiedById",
                table: "ItemPurchaseDocuments",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPurchaseDocuments_Purchase",
                table: "ItemPurchaseDocuments",
                column: "Purchase");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSpecificationDetails_CreatedById",
                table: "ItemSpecificationDetails",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSpecificationDetails_ItemSpecificationMasterId",
                table: "ItemSpecificationDetails",
                column: "ItemSpecificationMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSpecificationDetails_ModifiedById",
                table: "ItemSpecificationDetails",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSpecificationMaster_CreatedById",
                table: "ItemSpecificationMaster",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSpecificationMaster_ItemTypeId",
                table: "ItemSpecificationMaster",
                column: "ItemTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSpecificationMaster_ModifiedById",
                table: "ItemSpecificationMaster",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ItemSpecificationMaster_OfficeId",
                table: "ItemSpecificationMaster",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_JobDetails_ContractId",
                table: "JobDetails",
                column: "ContractId");

            migrationBuilder.CreateIndex(
                name: "IX_JobDetails_CreatedById",
                table: "JobDetails",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_JobDetails_JobPhaseId",
                table: "JobDetails",
                column: "JobPhaseId");

            migrationBuilder.CreateIndex(
                name: "IX_JobDetails_ModifiedById",
                table: "JobDetails",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_JobGrade_CreatedById",
                table: "JobGrade",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_JobGrade_ModifiedById",
                table: "JobGrade",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_JobHiringDetails_CreatedById",
                table: "JobHiringDetails",
                column: "CreatedById");

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
                name: "IX_JobHiringDetails_ModifiedById",
                table: "JobHiringDetails",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_JobHiringDetails_OfficeId",
                table: "JobHiringDetails",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_JobPhases_CreatedById",
                table: "JobPhases",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_JobPhases_ModifiedById",
                table: "JobPhases",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_JobPriceDetails_CreatedById",
                table: "JobPriceDetails",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_JobPriceDetails_JobId",
                table: "JobPriceDetails",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobPriceDetails_ModifiedById",
                table: "JobPriceDetails",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_JournalDetail_CreatedById",
                table: "JournalDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_JournalDetail_ModifiedById",
                table: "JournalDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageDetail_CreatedById",
                table: "LanguageDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageDetail_ModifiedById",
                table: "LanguageDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_CreatedById",
                table: "Languages",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_ModifiedById",
                table: "Languages",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveReasonDetail_CreatedById",
                table: "LeaveReasonDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveReasonDetail_ModifiedById",
                table: "LeaveReasonDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_LoggerDetails_CreatedById",
                table: "LoggerDetails",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_LoggerDetails_ModifiedById",
                table: "LoggerDetails",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_MediaCategories_CreatedById",
                table: "MediaCategories",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_MediaCategories_ModifiedById",
                table: "MediaCategories",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Mediums_CreatedById",
                table: "Mediums",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Mediums_ModifiedById",
                table: "Mediums",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_MotorMaintenances_CreatedById",
                table: "MotorMaintenances",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_MotorMaintenances_Generator",
                table: "MotorMaintenances",
                column: "Generator");

            migrationBuilder.CreateIndex(
                name: "IX_MotorMaintenances_ModifiedById",
                table: "MotorMaintenances",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_MotorMaintenances_Order",
                table: "MotorMaintenances",
                column: "Order");

            migrationBuilder.CreateIndex(
                name: "IX_MotorMaintenances_Vehicle",
                table: "MotorMaintenances",
                column: "Vehicle");

            migrationBuilder.CreateIndex(
                name: "IX_MotorSpareParts_CreatedById",
                table: "MotorSpareParts",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_MotorSpareParts_Generator",
                table: "MotorSpareParts",
                column: "Generator");

            migrationBuilder.CreateIndex(
                name: "IX_MotorSpareParts_ModifiedById",
                table: "MotorSpareParts",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_MotorSpareParts_Order",
                table: "MotorSpareParts",
                column: "Order");

            migrationBuilder.CreateIndex(
                name: "IX_MotorSpareParts_Vehicle",
                table: "MotorSpareParts",
                column: "Vehicle");

            migrationBuilder.CreateIndex(
                name: "IX_NationalityDetails_CreatedById",
                table: "NationalityDetails",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_NationalityDetails_ModifiedById",
                table: "NationalityDetails",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Natures_CreatedById",
                table: "Natures",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Natures_ModifiedById",
                table: "Natures",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_NotesMaster_AccountTypeId",
                table: "NotesMaster",
                column: "AccountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_NotesMaster_ChartOfAccountNewId",
                table: "NotesMaster",
                column: "ChartOfAccountNewId");

            migrationBuilder.CreateIndex(
                name: "IX_NotesMaster_CreatedById",
                table: "NotesMaster",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_NotesMaster_ModifiedById",
                table: "NotesMaster",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_OfficeDetail_CreatedById",
                table: "OfficeDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_OfficeDetail_ModifiedById",
                table: "OfficeDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_OrderSchedulePermission_CreatedById",
                table: "OrderSchedulePermission",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_OrderSchedulePermission_ModifiedById",
                table: "OrderSchedulePermission",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_OrderSchedulePermission_PageId",
                table: "OrderSchedulePermission",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTypes_CreatedById",
                table: "PaymentTypes",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTypes_ModifiedById",
                table: "PaymentTypes",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollAccountHead_CreatedById",
                table: "PayrollAccountHead",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollAccountHead_ModifiedById",
                table: "PayrollAccountHead",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollMonthlyHourDetail_AttendanceGroupId",
                table: "PayrollMonthlyHourDetail",
                column: "AttendanceGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollMonthlyHourDetail_CreatedById",
                table: "PayrollMonthlyHourDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollMonthlyHourDetail_ModifiedById",
                table: "PayrollMonthlyHourDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollMonthlyHourDetail_OfficeId",
                table: "PayrollMonthlyHourDetail",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_PensionDebitAccountMaster_ChartOfAccountNewId",
                table: "PensionDebitAccountMaster",
                column: "ChartOfAccountNewId");

            migrationBuilder.CreateIndex(
                name: "IX_PensionDebitAccountMaster_CreatedById",
                table: "PensionDebitAccountMaster",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PensionDebitAccountMaster_ModifiedById",
                table: "PensionDebitAccountMaster",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PensionPaymentHistory_CreatedById",
                table: "PensionPaymentHistory",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PensionPaymentHistory_EmployeeId",
                table: "PensionPaymentHistory",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_PensionPaymentHistory_ModifiedById",
                table: "PensionPaymentHistory",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PensionPaymentHistory_VoucherNo",
                table: "PensionPaymentHistory",
                column: "VoucherNo");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_CreatedById",
                table: "Permissions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_ModifiedById",
                table: "Permissions",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionsInRoles_CreatedById",
                table: "PermissionsInRoles",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionsInRoles_ModifiedById",
                table: "PermissionsInRoles",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PlayoutMinutes_CreatedById",
                table: "PlayoutMinutes",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PlayoutMinutes_ModifiedById",
                table: "PlayoutMinutes",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PlayoutMinutes_ScheduleId",
                table: "PlayoutMinutes",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyDaySchedules_CreatedById",
                table: "PolicyDaySchedules",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyDaySchedules_ModifiedById",
                table: "PolicyDaySchedules",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyDaySchedules_PolicyId",
                table: "PolicyDaySchedules",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyDetails_CreatedById",
                table: "PolicyDetails",
                column: "CreatedById");

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
                name: "IX_PolicyDetails_ModifiedById",
                table: "PolicyDetails",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyDetails_ProducerId",
                table: "PolicyDetails",
                column: "ProducerId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyOrderSchedules_CreatedById",
                table: "PolicyOrderSchedules",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyOrderSchedules_ModifiedById",
                table: "PolicyOrderSchedules",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyOrderSchedules_PolicyId",
                table: "PolicyOrderSchedules",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicySchedules_CreatedById",
                table: "PolicySchedules",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PolicySchedules_ModifiedById",
                table: "PolicySchedules",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PolicySchedules_PolicyId",
                table: "PolicySchedules",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyTimeSchedules_CreatedById",
                table: "PolicyTimeSchedules",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyTimeSchedules_ModifiedById",
                table: "PolicyTimeSchedules",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PolicyTimeSchedules_PolicyId",
                table: "PolicyTimeSchedules",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_PriorityCriteriaDetail_CreatedById",
                table: "PriorityCriteriaDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PriorityCriteriaDetail_ModifiedById",
                table: "PriorityCriteriaDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PriorityCriteriaDetail_ProjectId",
                table: "PriorityCriteriaDetail",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_PriorityOtherDetail_CreatedById",
                table: "PriorityOtherDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PriorityOtherDetail_ModifiedById",
                table: "PriorityOtherDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PriorityOtherDetail_ProjectId",
                table: "PriorityOtherDetail",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Producers_CreatedById",
                table: "Producers",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Producers_ModifiedById",
                table: "Producers",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessionDetails_CreatedById",
                table: "ProfessionDetails",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessionDetails_ModifiedById",
                table: "ProfessionDetails",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramDetail_CreatedById",
                table: "ProgramDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProgramDetail_ModifiedById",
                table: "ProgramDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivitiesControl_CreatedById",
                table: "ProjectActivitiesControl",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivitiesControl_ModifiedById",
                table: "ProjectActivitiesControl",
                column: "ModifiedById");

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
                name: "IX_ProjectActivityDetail_CreatedById",
                table: "ProjectActivityDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivityDetail_EmployeeID",
                table: "ProjectActivityDetail",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivityDetail_ModifiedById",
                table: "ProjectActivityDetail",
                column: "ModifiedById");

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
                name: "IX_ProjectActivityExtensions_CreatedById",
                table: "ProjectActivityExtensions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivityExtensions_ModifiedById",
                table: "ProjectActivityExtensions",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivityProvinceDetail_ActivityId",
                table: "ProjectActivityProvinceDetail",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivityProvinceDetail_CreatedById",
                table: "ProjectActivityProvinceDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivityProvinceDetail_DistrictID",
                table: "ProjectActivityProvinceDetail",
                column: "DistrictID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivityProvinceDetail_ModifiedById",
                table: "ProjectActivityProvinceDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectActivityProvinceDetail_ProvinceId",
                table: "ProjectActivityProvinceDetail",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectArea_AreaId",
                table: "ProjectArea",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectArea_CreatedById",
                table: "ProjectArea",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectArea_ModifiedById",
                table: "ProjectArea",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectArea_ProjectId",
                table: "ProjectArea",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectAssignTo_CreatedById",
                table: "ProjectAssignTo",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectAssignTo_EmployeeId",
                table: "ProjectAssignTo",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectAssignTo_ModifiedById",
                table: "ProjectAssignTo",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectAssignTo_ProjectId",
                table: "ProjectAssignTo",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectBudgetLine_BudgetLineTypeId",
                table: "ProjectBudgetLine",
                column: "BudgetLineTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectBudgetLine_CreatedById",
                table: "ProjectBudgetLine",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectBudgetLine_ModifiedById",
                table: "ProjectBudgetLine",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectBudgetLine_ProjectId",
                table: "ProjectBudgetLine",
                column: "ProjectId");

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
                name: "IX_ProjectCommunication_CreatedById",
                table: "ProjectCommunication",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCommunication_ModifiedById",
                table: "ProjectCommunication",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCommunication_ProjectId",
                table: "ProjectCommunication",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCommunicationAttachment_CreatedById",
                table: "ProjectCommunicationAttachment",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCommunicationAttachment_ModifiedById",
                table: "ProjectCommunicationAttachment",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCommunicationAttachment_PCId",
                table: "ProjectCommunicationAttachment",
                column: "PCId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectCommunicationAttachment_ProjectId",
                table: "ProjectCommunicationAttachment",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDetail_CreatedById",
                table: "ProjectDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDetail_ModifiedById",
                table: "ProjectDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDetail_ProjectPhaseDetailsId",
                table: "ProjectDetail",
                column: "ProjectPhaseDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDetails_CreatedById",
                table: "ProjectDetails",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDetails_ModifiedById",
                table: "ProjectDetails",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectHiringControl_CreatedById",
                table: "ProjectHiringControl",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectHiringControl_ModifiedById",
                table: "ProjectHiringControl",
                column: "ModifiedById");

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
                name: "IX_ProjectHiringRequestDetail_CreatedById",
                table: "ProjectHiringRequestDetail",
                column: "CreatedById");

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
                name: "IX_ProjectHiringRequestDetail_ModifiedById",
                table: "ProjectHiringRequestDetail",
                column: "ModifiedById");

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
                name: "IX_ProjectIndicatorQuestions_CreatedById",
                table: "ProjectIndicatorQuestions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectIndicatorQuestions_ModifiedById",
                table: "ProjectIndicatorQuestions",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectIndicatorQuestions_ProjectIndicatorId",
                table: "ProjectIndicatorQuestions",
                column: "ProjectIndicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectIndicators_CreatedById",
                table: "ProjectIndicators",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectIndicators_ModifiedById",
                table: "ProjectIndicators",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectJobDetail_CreatedById",
                table: "ProjectJobDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectJobDetail_ModifiedById",
                table: "ProjectJobDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectJobDetail_ProjectId",
                table: "ProjectJobDetail",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectLogisticsControl_CreatedById",
                table: "ProjectLogisticsControl",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectLogisticsControl_ModifiedById",
                table: "ProjectLogisticsControl",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectLogisticsControl_ProjectId",
                table: "ProjectLogisticsControl",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectLogisticsControl_UserID",
                table: "ProjectLogisticsControl",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMonitoringIndicatorDetail_CreatedById",
                table: "ProjectMonitoringIndicatorDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMonitoringIndicatorDetail_ModifiedById",
                table: "ProjectMonitoringIndicatorDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMonitoringIndicatorDetail_ProjectIndicatorId",
                table: "ProjectMonitoringIndicatorDetail",
                column: "ProjectIndicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMonitoringIndicatorDetail_ProjectMonitoringReviewId",
                table: "ProjectMonitoringIndicatorDetail",
                column: "ProjectMonitoringReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMonitoringIndicatorQuestions_CreatedById",
                table: "ProjectMonitoringIndicatorQuestions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMonitoringIndicatorQuestions_ModifiedById",
                table: "ProjectMonitoringIndicatorQuestions",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMonitoringIndicatorQuestions_MonitoringIndicatorId",
                table: "ProjectMonitoringIndicatorQuestions",
                column: "MonitoringIndicatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMonitoringIndicatorQuestions_QuestionId",
                table: "ProjectMonitoringIndicatorQuestions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMonitoringReviewDetail_CreatedById",
                table: "ProjectMonitoringReviewDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectMonitoringReviewDetail_ModifiedById",
                table: "ProjectMonitoringReviewDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOpportunityControl_CreatedById",
                table: "ProjectOpportunityControl",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOpportunityControl_ModifiedById",
                table: "ProjectOpportunityControl",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOpportunityControl_ProjectId",
                table: "ProjectOpportunityControl",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOpportunityControl_UserID",
                table: "ProjectOpportunityControl",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOtherDetail_CreatedById",
                table: "ProjectOtherDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOtherDetail_ModifiedById",
                table: "ProjectOtherDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOtherDetail_ProjectId",
                table: "ProjectOtherDetail",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPhaseDetails_CreatedById",
                table: "ProjectPhaseDetails",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPhaseDetails_ModifiedById",
                table: "ProjectPhaseDetails",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPhaseTime_CreatedById",
                table: "ProjectPhaseTime",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPhaseTime_ModifiedById",
                table: "ProjectPhaseTime",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPhaseTime_ProjectId",
                table: "ProjectPhaseTime",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectPhaseTime_ProjectPhaseDetailsId",
                table: "ProjectPhaseTime",
                column: "ProjectPhaseDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProgram_CreatedById",
                table: "ProjectProgram",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProgram_ModifiedById",
                table: "ProjectProgram",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProgram_ProgramId",
                table: "ProjectProgram",
                column: "ProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProgram_ProjectId",
                table: "ProjectProgram",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProposalDetail_CreatedById",
                table: "ProjectProposalDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProposalDetail_ModifiedById",
                table: "ProjectProposalDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectProposalDetail_ProjectId",
                table: "ProjectProposalDetail",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSector_CreatedById",
                table: "ProjectSector",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectSector_ModifiedById",
                table: "ProjectSector",
                column: "ModifiedById");

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
                name: "IX_ProvinceDetails_CreatedById",
                table: "ProvinceDetails",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProvinceDetails_ModifiedById",
                table: "ProvinceDetails",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProvinceMultiSelect_CreatedById",
                table: "ProvinceMultiSelect",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProvinceMultiSelect_ModifiedById",
                table: "ProvinceMultiSelect",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProvinceMultiSelect_ProjectId",
                table: "ProvinceMultiSelect",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProvinceMultiSelect_ProvinceId",
                table: "ProvinceMultiSelect",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseGenerators_CreatedById",
                table: "PurchaseGenerators",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseGenerators_ModifiedById",
                table: "PurchaseGenerators",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseGenerators_Purchase",
                table: "PurchaseGenerators",
                column: "Purchase");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseUnitType_CreatedById",
                table: "PurchaseUnitType",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseUnitType_ModifiedById",
                table: "PurchaseUnitType",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseVehicles_CreatedById",
                table: "PurchaseVehicles",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseVehicles_ModifiedById",
                table: "PurchaseVehicles",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseVehicles_Purchase",
                table: "PurchaseVehicles",
                column: "Purchase");

            migrationBuilder.CreateIndex(
                name: "IX_PurposeofInitiativeCriteria_CreatedById",
                table: "PurposeofInitiativeCriteria",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PurposeofInitiativeCriteria_ModifiedById",
                table: "PurposeofInitiativeCriteria",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PurposeofInitiativeCriteria_ProjectId",
                table: "PurposeofInitiativeCriteria",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_QualificationDetails_CreatedById",
                table: "QualificationDetails",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_QualificationDetails_ModifiedById",
                table: "QualificationDetails",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Qualities_CreatedById",
                table: "Qualities",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Qualities_ModifiedById",
                table: "Qualities",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_RatingBasedCriteria_CreatedById",
                table: "RatingBasedCriteria",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_RatingBasedCriteria_InterviewDetailsId",
                table: "RatingBasedCriteria",
                column: "InterviewDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_RatingBasedCriteria_ModifiedById",
                table: "RatingBasedCriteria",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptType_CreatedById",
                table: "ReceiptType",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptType_ModifiedById",
                table: "ReceiptType",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_RiskCriteriaDetail_CreatedById",
                table: "RiskCriteriaDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_RiskCriteriaDetail_ModifiedById",
                table: "RiskCriteriaDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_RiskCriteriaDetail_ProjectId",
                table: "RiskCriteriaDetail",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_CreatedById",
                table: "RolePermissions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_ModifiedById",
                table: "RolePermissions",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissions_PageId",
                table: "RolePermissions",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryHeadDetails_CreatedById",
                table: "SalaryHeadDetails",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryHeadDetails_ModifiedById",
                table: "SalaryHeadDetails",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryTaxReportContent_CreatedById",
                table: "SalaryTaxReportContent",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryTaxReportContent_ModifiedById",
                table: "SalaryTaxReportContent",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleDetails_ChannelId",
                table: "ScheduleDetails",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleDetails_CreatedById",
                table: "ScheduleDetails",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleDetails_JobId",
                table: "ScheduleDetails",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleDetails_MediumId",
                table: "ScheduleDetails",
                column: "MediumId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleDetails_ModifiedById",
                table: "ScheduleDetails",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleDetails_PolicyId",
                table: "ScheduleDetails",
                column: "PolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleDetails_ProjectId",
                table: "ScheduleDetails",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SectorDetails_CreatedById",
                table: "SectorDetails",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SectorDetails_ModifiedById",
                table: "SectorDetails",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityConsiderationDetail_CreatedById",
                table: "SecurityConsiderationDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityConsiderationDetail_ModifiedById",
                table: "SecurityConsiderationDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityConsiderationMultiSelect_CreatedById",
                table: "SecurityConsiderationMultiSelect",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityConsiderationMultiSelect_ModifiedById",
                table: "SecurityConsiderationMultiSelect",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityConsiderationMultiSelect_ProjectId",
                table: "SecurityConsiderationMultiSelect",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityConsiderationMultiSelect_SecurityConsiderationId",
                table: "SecurityConsiderationMultiSelect",
                column: "SecurityConsiderationId");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityDetail_CreatedById",
                table: "SecurityDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityDetail_ModifiedById",
                table: "SecurityDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_StatusAtTimeOfIssue_CreatedById",
                table: "StatusAtTimeOfIssue",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_StatusAtTimeOfIssue_ModifiedById",
                table: "StatusAtTimeOfIssue",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_StoreInventories_CreatedById",
                table: "StoreInventories",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_StoreInventories_InventoryCreditAccount",
                table: "StoreInventories",
                column: "InventoryCreditAccount");

            migrationBuilder.CreateIndex(
                name: "IX_StoreInventories_InventoryDebitAccount",
                table: "StoreInventories",
                column: "InventoryDebitAccount");

            migrationBuilder.CreateIndex(
                name: "IX_StoreInventories_ModifiedById",
                table: "StoreInventories",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_StoreItemGroups_CreatedById",
                table: "StoreItemGroups",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_StoreItemGroups_InventoryId",
                table: "StoreItemGroups",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreItemGroups_ModifiedById",
                table: "StoreItemGroups",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_StoreItemPurchases_CreatedById",
                table: "StoreItemPurchases",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_StoreItemPurchases_Currency",
                table: "StoreItemPurchases",
                column: "Currency");

            migrationBuilder.CreateIndex(
                name: "IX_StoreItemPurchases_InventoryItem",
                table: "StoreItemPurchases",
                column: "InventoryItem");

            migrationBuilder.CreateIndex(
                name: "IX_StoreItemPurchases_ModifiedById",
                table: "StoreItemPurchases",
                column: "ModifiedById");

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
                name: "IX_StorePurchaseOrders_CreatedById",
                table: "StorePurchaseOrders",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_StorePurchaseOrders_InventoryItem",
                table: "StorePurchaseOrders",
                column: "InventoryItem");

            migrationBuilder.CreateIndex(
                name: "IX_StorePurchaseOrders_IssuedToEmployeeId",
                table: "StorePurchaseOrders",
                column: "IssuedToEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_StorePurchaseOrders_ModifiedById",
                table: "StorePurchaseOrders",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_StorePurchaseOrders_Purchase",
                table: "StorePurchaseOrders",
                column: "Purchase");

            migrationBuilder.CreateIndex(
                name: "IX_StoreSourceCodeDetail_CodeTypeId",
                table: "StoreSourceCodeDetail",
                column: "CodeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_StoreSourceCodeDetail_CreatedById",
                table: "StoreSourceCodeDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_StoreSourceCodeDetail_ModifiedById",
                table: "StoreSourceCodeDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_StrengthConsiderationDetail_CreatedById",
                table: "StrengthConsiderationDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_StrengthConsiderationDetail_ModifiedById",
                table: "StrengthConsiderationDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_StrongandWeakPoints_CreatedById",
                table: "StrongandWeakPoints",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_StrongandWeakPoints_ModifiedById",
                table: "StrongandWeakPoints",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_TargetBeneficiaryDetail_CreatedById",
                table: "TargetBeneficiaryDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TargetBeneficiaryDetail_ModifiedById",
                table: "TargetBeneficiaryDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_TaskMaster_CreatedById",
                table: "TaskMaster",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TaskMaster_ModifiedById",
                table: "TaskMaster",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_TaskMaster_ProjectId",
                table: "TaskMaster",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalQuestion_CreatedById",
                table: "TechnicalQuestion",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalQuestion_ModifiedById",
                table: "TechnicalQuestion",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_TimeCategories_CreatedById",
                table: "TimeCategories",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TimeCategories_ModifiedById",
                table: "TimeCategories",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_UnitRates_ActivityTypeId",
                table: "UnitRates",
                column: "ActivityTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitRates_CreatedById",
                table: "UnitRates",
                column: "CreatedById");

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
                name: "IX_UnitRates_ModifiedById",
                table: "UnitRates",
                column: "ModifiedById");

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
                name: "IX_UserDetailOffices_CreatedById",
                table: "UserDetailOffices",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_UserDetailOffices_ModifiedById",
                table: "UserDetailOffices",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleFuel_CreatedById",
                table: "VehicleFuel",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleFuel_Generator",
                table: "VehicleFuel",
                column: "Generator");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleFuel_ModifiedById",
                table: "VehicleFuel",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleFuel_Order",
                table: "VehicleFuel",
                column: "Order");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleFuel_Vehicle",
                table: "VehicleFuel",
                column: "Vehicle");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleLocations_Vehicle",
                table: "VehicleLocations",
                column: "Vehicle");

            migrationBuilder.CreateIndex(
                name: "IX_VehicleMileages_Vehicle",
                table: "VehicleMileages",
                column: "Vehicle");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherDetail_BudgetLineId",
                table: "VoucherDetail",
                column: "BudgetLineId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherDetail_CreatedById",
                table: "VoucherDetail",
                column: "CreatedById");

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
                name: "IX_VoucherDetail_ModifiedById",
                table: "VoucherDetail",
                column: "ModifiedById");

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
                name: "IX_VoucherDocumentDetail_CreatedById",
                table: "VoucherDocumentDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherDocumentDetail_DocumentFileId",
                table: "VoucherDocumentDetail",
                column: "DocumentFileId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherDocumentDetail_ModifiedById",
                table: "VoucherDocumentDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherDocumentDetail_VoucherNo",
                table: "VoucherDocumentDetail",
                column: "VoucherNo");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactions_BudgetLineId",
                table: "VoucherTransactions",
                column: "BudgetLineId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactions_ChartOfAccountNewId",
                table: "VoucherTransactions",
                column: "ChartOfAccountNewId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactions_CreatedById",
                table: "VoucherTransactions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactions_CurrencyId",
                table: "VoucherTransactions",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactions_JobId",
                table: "VoucherTransactions",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactions_ModifiedById",
                table: "VoucherTransactions",
                column: "ModifiedById");

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
                name: "ActivityDocumentsDetail");

            migrationBuilder.DropTable(
                name: "AgreeDisagreePermission");

            migrationBuilder.DropTable(
                name: "AnalyticalDetail");

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
                name: "AssignActivityApproveBy");

            migrationBuilder.DropTable(
                name: "AssignActivityFeedback");

            migrationBuilder.DropTable(
                name: "AssignLeaveToEmployee");

            migrationBuilder.DropTable(
                name: "BudgetReceivedAmount");

            migrationBuilder.DropTable(
                name: "CategoryPopulator");

            migrationBuilder.DropTable(
                name: "CEAgeGroupDetail");

            migrationBuilder.DropTable(
                name: "CEAssumptionDetail");

            migrationBuilder.DropTable(
                name: "CEFeasibilityExpertOtherDetail");

            migrationBuilder.DropTable(
                name: "CEOccupationDetail");

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
                name: "EmployeeAnalyticalDetail");

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
                name: "EmployeeDetailDT");

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
                name: "EmployeePayrollDetailTest");

            migrationBuilder.DropTable(
                name: "EmployeePayrollForMonth");

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
                name: "errorlog");

            migrationBuilder.DropTable(
                name: "ExchangeRateDetail");

            migrationBuilder.DropTable(
                name: "ExchangeRates");

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
                name: "ItemPurchaseDocuments");

            migrationBuilder.DropTable(
                name: "ItemSpecificationDetails");

            migrationBuilder.DropTable(
                name: "JobPriceDetails");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "LoggerDetails");

            migrationBuilder.DropTable(
                name: "MotorMaintenances");

            migrationBuilder.DropTable(
                name: "MotorSpareParts");

            migrationBuilder.DropTable(
                name: "NotesMaster");

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
                name: "VehicleFuel");

            migrationBuilder.DropTable(
                name: "VehicleLocations");

            migrationBuilder.DropTable(
                name: "VehicleMileages");

            migrationBuilder.DropTable(
                name: "VoucherDocumentDetail");

            migrationBuilder.DropTable(
                name: "VoucherTransactions");

            migrationBuilder.DropTable(
                name: "WinProjectDetails");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AssignActivity");

            migrationBuilder.DropTable(
                name: "BudgetReceivable");

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
                name: "InterviewDetails");

            migrationBuilder.DropTable(
                name: "ApplicationPages");

            migrationBuilder.DropTable(
                name: "SecurityConsiderationDetail");

            migrationBuilder.DropTable(
                name: "CodeType");

            migrationBuilder.DropTable(
                name: "PurchaseGenerators");

            migrationBuilder.DropTable(
                name: "StorePurchaseOrders");

            migrationBuilder.DropTable(
                name: "PurchaseVehicles");

            migrationBuilder.DropTable(
                name: "DocumentFileDetail");

            migrationBuilder.DropTable(
                name: "ActivityMaster");

            migrationBuilder.DropTable(
                name: "ProjectBudgetLine");

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
                name: "StoreItemPurchases");

            migrationBuilder.DropTable(
                name: "TaskMaster");

            migrationBuilder.DropTable(
                name: "BudgetLineType");

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
                name: "InventoryItems");

            migrationBuilder.DropTable(
                name: "EmployeeDetail");

            migrationBuilder.DropTable(
                name: "ReceiptType");

            migrationBuilder.DropTable(
                name: "StatusAtTimeOfIssue");

            migrationBuilder.DropTable(
                name: "PurchaseUnitType");

            migrationBuilder.DropTable(
                name: "VoucherDetail");

            migrationBuilder.DropTable(
                name: "ProjectDetails");

            migrationBuilder.DropTable(
                name: "ClientDetails");

            migrationBuilder.DropTable(
                name: "LanguageDetail");

            migrationBuilder.DropTable(
                name: "UnitRates");

            migrationBuilder.DropTable(
                name: "StoreItemGroups");

            migrationBuilder.DropTable(
                name: "InventoryItemType");

            migrationBuilder.DropTable(
                name: "EmployeeType");

            migrationBuilder.DropTable(
                name: "QualificationDetails");

            migrationBuilder.DropTable(
                name: "NationalityDetails");

            migrationBuilder.DropTable(
                name: "ProvinceDetails");

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
                name: "Categories");

            migrationBuilder.DropTable(
                name: "ActivityTypes");

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
                name: "StoreInventories");

            migrationBuilder.DropTable(
                name: "CountryDetails");

            migrationBuilder.DropTable(
                name: "CurrencyDetails");

            migrationBuilder.DropTable(
                name: "ProjectJobDetail");

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

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class initialize1 : Migration
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
                name: "AccountNoteDetail",
                columns: table => new
                {
                    AccountCode = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Narration = table.Column<string>(nullable: true),
                    AccountNote = table.Column<long>(nullable: true),
                    BalanceType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountNoteDetail", x => x.AccountCode);
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
                    Id = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UserID = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    AccountFilterTypeId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    AccountHeadTypeId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AccountHeadTypeName = table.Column<string>(nullable: true)
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
                name: "ActivityTypes",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ActivityTypeId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    AnalyticalId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                name: "AppraisalGeneralQuestions",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    AppraisalGeneralQuestionsId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    AreaId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                name: "BudgetLineType",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    BudgetLineTypeId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    CategoryId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    CountryId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    DesignationId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    DistrictID = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                name: "DonorCriteriaDetail",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    DonorCEId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    OtherDeliverableType = table.Column<bool>(nullable: true),
                    PastWorkingExperience = table.Column<bool>(nullable: true),
                    CriticismPerformance = table.Column<bool>(nullable: true),
                    TimeManagement = table.Column<bool>(nullable: true),
                    MoneyAllocation = table.Column<bool>(nullable: true),
                    Accountability = table.Column<bool>(nullable: true),
                    DeliverableQuality = table.Column<bool>(nullable: true),
                    DonorFinancingHistory = table.Column<bool>(nullable: true),
                    ReligiousStanding = table.Column<bool>(nullable: true),
                    PoliticalStanding = table.Column<bool>(nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "DonorDetail",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    DonorId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    EmailTypeId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    AnalyticalID = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    EmployeeAppraisalTeamMemberId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    EmployeeId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    EmployeeEvaluationId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    EmployeeEvaluationTrainingId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    EmployeeHealthQuestionId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    PayrollId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    EmployeeTypeId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                name: "ExchangeRateDetail",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ExchangeRateId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                name: "FinancialYearDetail",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    FinancialYearId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    GenderConsiderationId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                name: "InterviewDetails",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    InterviewDetailsId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                        name: "FK_InterviewDetails_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InterviewTechnicalQuestions",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    InterviewTechnicalQuestionsId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ItemType = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    GradeId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    JobPhaseId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    JournalCode = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    LanguageId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    LanguageId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    LeaveReasonId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    LoggerDetailsId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    MediaCategoryId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    MediumId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    NationalityId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    NatureId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    OfficeId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                name: "PayrollAccountHead",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    PayrollHeadId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    PermissionsInRolesId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    RoleId = table.Column<string>(nullable: false),
                    PermissionId = table.Column<string>(nullable: false),
                    IsGrant = table.Column<bool>(nullable: false),
                    CurrentPermissionId = table.Column<string>(nullable: true)
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

            migrationBuilder.CreateTable(
                name: "ProgramDetail",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ProgramId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ProjectId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                name: "ProjectPhaseDetails",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ProjectPhaseDetailsId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    UnitTypeId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    QualificationId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    QualityId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                name: "RatingBasedCriteria",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    RatingBasedCriteriaId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                        name: "FK_RatingBasedCriteria_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptType",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ReceiptTypeId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    SalaryHeadId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    SalaryTaxReportContentId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    SectorId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    SecurityConsiderationId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    SecurityId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    StatusAtTimeOfIssueId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    StrengthConsiderationId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    StrongPointsId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                name: "TechnicalQuestion",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    TechnicalQuestionId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    TimeCategoryId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    UserOfficesId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    SourceCodeId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ContractTypeContentId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    AccountTypeId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                name: "EmployeeAppraisalQuestions",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    EmployeeAppraisalQuestionsId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ClientId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ProvinceId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    EmailId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    EmployeePensionRateId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                name: "InterviewLanguages",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    InterviewLanguagesId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    InterviewTechnicalQuestionId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    InterviewTrainingsId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                name: "JobDetails",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    JobId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    JobName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    JobCode = table.Column<string>(nullable: true),
                    ContractId = table.Column<int>(nullable: true),
                    JobPhaseId = table.Column<long>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsApproved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobDetails", x => x.JobId);
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobDetails_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeLanguages",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    SpeakLanguageId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                name: "Department",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    DepartmentId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ExchangeRateId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    HolidayId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    HolidayWeeklyId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ItemSpecificationMasterId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                name: "JobHiringDetails",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    JobId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    JobCode = table.Column<string>(maxLength: 50, nullable: true),
                    JobDescription = table.Column<string>(nullable: true),
                    ProfessionId = table.Column<int>(nullable: true),
                    Unit = table.Column<int>(nullable: false),
                    OfficeId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    GradeId = table.Column<int>(nullable: true)
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
                name: "PayrollMonthlyHourDetail",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    PayrollMonthlyHourID = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    OfficeId = table.Column<int>(nullable: false),
                    PayrollMonth = table.Column<int>(nullable: true),
                    PayrollYear = table.Column<int>(nullable: true),
                    Hours = table.Column<int>(nullable: true),
                    InTime = table.Column<DateTime>(nullable: true),
                    OutTime = table.Column<DateTime>(nullable: true),
                    WorkingTime = table.Column<int>(nullable: true),
                    WorkingDay = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PayrollMonthlyHourDetail", x => x.PayrollMonthlyHourID);
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
                name: "ProjectBudgetLine",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    BudgetLineId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    TaskId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                name: "ProjectDetail",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ProjectId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ProjectCode = table.Column<string>(nullable: true),
                    ProjectName = table.Column<string>(nullable: true),
                    ProjectDescription = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    ProjectPhaseDetailsId = table.Column<long>(nullable: true),
                    IsProposalComplate = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    UnitRateId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UnitRates = table.Column<double>(nullable: false),
                    CurrencyId = table.Column<long>(nullable: true),
                    CurrencyDetailsCurrencyId = table.Column<int>(nullable: true),
                    MediumId = table.Column<long>(nullable: true),
                    TimeCategoryId = table.Column<long>(nullable: true),
                    NatureId = table.Column<long>(nullable: true),
                    QualityId = table.Column<long>(nullable: true),
                    ActivityTypeId = table.Column<long>(nullable: true)
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    CategoryPopulatorId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    SubCategoryLabel = table.Column<string>(nullable: true),
                    ChartOfAccountCode = table.Column<long>(nullable: false),
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
                name: "ChartAccountDetail",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    AccountCode = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ChartOfAccountCode = table.Column<long>(nullable: false),
                    AccountName = table.Column<string>(maxLength: 100, nullable: true),
                    AccountLevelId = table.Column<int>(nullable: false),
                    AccountTypeId = table.Column<int>(nullable: true),
                    ParentID = table.Column<long>(nullable: false),
                    DepRate = table.Column<float>(nullable: false),
                    DepMethod = table.Column<string>(nullable: true),
                    AccountNote = table.Column<int>(nullable: true),
                    MDCode = table.Column<string>(nullable: true),
                    Show = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChartAccountDetail", x => x.AccountCode);
                    table.ForeignKey(
                        name: "FK_ChartAccountDetail_AccountLevel_AccountLevelId",
                        column: x => x.AccountLevelId,
                        principalTable: "AccountLevel",
                        principalColumn: "AccountLevelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChartAccountDetail_AccountType_AccountTypeId",
                        column: x => x.AccountTypeId,
                        principalTable: "AccountType",
                        principalColumn: "AccountTypeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChartAccountDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChartAccountDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChartOfAccountNew",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ChartOfAccountNewId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ChartOfAccountNewCode = table.Column<string>(nullable: true),
                    AccountName = table.Column<string>(maxLength: 100, nullable: true),
                    ParentID = table.Column<long>(nullable: false),
                    AccountLevelId = table.Column<int>(nullable: false),
                    AccountTypeId = table.Column<int>(nullable: true),
                    AccountHeadTypeId = table.Column<int>(nullable: false),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    EmployeeID = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                name: "JobPriceDetails",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    JobPriceId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UnitRate = table.Column<double>(nullable: false),
                    Units = table.Column<int>(nullable: false),
                    FinalRate = table.Column<double>(nullable: false),
                    FinalPrice = table.Column<double>(nullable: false),
                    Discount = table.Column<double>(nullable: false),
                    DiscountPercent = table.Column<float>(nullable: false),
                    TotalPrice = table.Column<double>(nullable: false),
                    IsInvoiceApproved = table.Column<bool>(nullable: false),
                    JobId = table.Column<long>(nullable: false)
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
                name: "ItemSpecificationDetails",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ItemSpecificationDetailsId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                name: "BudgetReceivable",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    BudgetReceivalbeId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ActivityId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                name: "ProjectArea",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ProjectAreaId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    PCId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                name: "ProjectOtherDetail",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ProjectOtherDetailId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    beneficiaryMale = table.Column<string>(nullable: true),
                    beneficiaryFemale = table.Column<string>(nullable: true),
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
                    SecurityRemarks = table.Column<string>(nullable: true)
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ProjectPhaseTimeId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ProjectProgramId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ProjectProposaldetailId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    ProposalBudget = table.Column<string>(nullable: true),
                    ProposalDueDate = table.Column<DateTime>(nullable: true),
                    ProjectAssignTo = table.Column<int>(nullable: true),
                    IsProposalAccept = table.Column<bool>(nullable: true)
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ProjectSectorId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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

            migrationBuilder.CreateTable(
                name: "ContractDetails",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ContractId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                        name: "FK_ContractDetails_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
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
                name: "NotesMaster",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    NoteId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AccountCode = table.Column<int>(nullable: true),
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
                        name: "FK_NotesMaster_ChartAccountDetail_AccountCode",
                        column: x => x.AccountCode,
                        principalTable: "ChartAccountDetail",
                        principalColumn: "AccountCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NotesMaster_AccountType_AccountTypeId",
                        column: x => x.AccountTypeId,
                        principalTable: "AccountType",
                        principalColumn: "AccountTypeId",
                        onDelete: ReferentialAction.Restrict);
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
                name: "StoreInventories",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    InventoryId = table.Column<string>(nullable: false),
                    InventoryCode = table.Column<string>(nullable: true),
                    InventoryName = table.Column<string>(nullable: true),
                    InventoryDescription = table.Column<string>(nullable: true),
                    AssetType = table.Column<int>(nullable: false),
                    InventoryDebitAccount = table.Column<int>(nullable: false),
                    InventoryCreditAccount = table.Column<int>(nullable: true)
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
                        name: "FK_StoreInventories_ChartAccountDetail_InventoryCreditAccount",
                        column: x => x.InventoryCreditAccount,
                        principalTable: "ChartAccountDetail",
                        principalColumn: "AccountCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StoreInventories_ChartAccountDetail_InventoryDebitAccount",
                        column: x => x.InventoryDebitAccount,
                        principalTable: "ChartAccountDetail",
                        principalColumn: "AccountCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreInventories_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VoucherDetail",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    VoucherNo = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CurrencyId = table.Column<int>(nullable: true),
                    VoucherDate = table.Column<DateTime>(nullable: true),
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
                    ChartAccountDetailAccountCode = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoucherDetail", x => x.VoucherNo);
                    table.ForeignKey(
                        name: "FK_VoucherDetail_ProjectBudgetLine_BudgetLineId",
                        column: x => x.BudgetLineId,
                        principalTable: "ProjectBudgetLine",
                        principalColumn: "BudgetLineId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VoucherDetail_ChartAccountDetail_ChartAccountDetailAccountCode",
                        column: x => x.ChartAccountDetailAccountCode,
                        principalTable: "ChartAccountDetail",
                        principalColumn: "AccountCode",
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
                        name: "FK_VoucherDetail_ProjectDetails_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetails",
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
                name: "Advances",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    AdvancesId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    LeaveId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ApplyLeaveId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    EmployeeAppraisalDetailsId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    AttendanceId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    EmployeeContractId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    ContractPeriod = table.Column<float>(nullable: true)
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    DocumentID = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    EmployeeEducationsId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    EmployeeHealthInfoId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    HistoryID = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    EmployeeHistoryOutsideCountryId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    EmploymentFrom = table.Column<DateTime>(nullable: true),
                    EmploymentTo = table.Column<DateTime>(nullable: true),
                    Organization = table.Column<string>(nullable: true),
                    MonthlySalary = table.Column<string>(nullable: true),
                    ReasonForLeaving = table.Column<string>(nullable: true),
                    EmployeeID = table.Column<int>(nullable: true)
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    EmployeeHistoryOutsideOrganizationId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    EmploymentFrom = table.Column<DateTime>(nullable: true),
                    EmploymentTo = table.Column<DateTime>(nullable: true),
                    Organization = table.Column<string>(nullable: true),
                    MonthlySalary = table.Column<string>(nullable: true),
                    ReasonForLeaving = table.Column<string>(nullable: true),
                    EmployeeID = table.Column<int>(nullable: true)
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    EmployeeInfoReferencesId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    EmployeeOtherSkillsId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    PayrollId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    EmployeePayrollAccountId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    EmployeePaymentTypesId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    MonthlyPayrollId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    EmployeeProfessionalId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    ProfessionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeProfessionalDetail", x => x.EmployeeProfessionalId);
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    EmployeeRelativeInfoId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                name: "EmployeeSalaryAnalyticalInfo",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    EmployeeSalaryAnalyticalInfoId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AccountCode = table.Column<int>(nullable: false),
                    ProjectId = table.Column<long>(nullable: false),
                    BudgetLineId = table.Column<long>(nullable: false),
                    SalaryPercentage = table.Column<double>(nullable: false),
                    EmployeeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeSalaryAnalyticalInfo", x => x.EmployeeSalaryAnalyticalInfoId);
                    table.ForeignKey(
                        name: "FK_EmployeeSalaryAnalyticalInfo_ProjectBudgetLine_BudgetLineId",
                        column: x => x.BudgetLineId,
                        principalTable: "ProjectBudgetLine",
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
                        name: "FK_EmployeeSalaryAnalyticalInfo_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeSalaryAnalyticalInfo_ProjectDetails_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetails",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeSalaryBudget",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    EmployeeSalaryBudgetId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    SalaryId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ExistInterviewDetailsId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                name: "InterviewScheduleDetails",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ScheduleId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                name: "ProjectAssignTo",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ProjectAssignToId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    BudgetReceivedAmountId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    AssignActivityId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    PCAId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                name: "InventoryItems",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ItemId = table.Column<string>(nullable: false),
                    ItemInventory = table.Column<string>(nullable: true),
                    ItemName = table.Column<string>(nullable: true),
                    ItemCode = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
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
                name: "EmployeeSalaryPaymentHistory",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    SalaryPaymentId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    PensionPaymentId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    DocumentID = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    DocumentName = table.Column<string>(maxLength: 100, nullable: true),
                    FilePath = table.Column<byte[]>(nullable: true),
                    DocumentDate = table.Column<DateTime>(nullable: true),
                    VoucherNo = table.Column<long>(nullable: false),
                    Extension = table.Column<string>(nullable: true),
                    DocumentGUID = table.Column<string>(nullable: true),
                    DocumentType = table.Column<int>(nullable: true),
                    DocumentFilePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoucherDocumentDetail", x => x.DocumentID);
                    table.ForeignKey(
                        name: "FK_VoucherDocumentDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    TransactionId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    VoucherNo = table.Column<long>(nullable: true),
                    CreditAccount = table.Column<int>(nullable: true),
                    DebitAccount = table.Column<int>(nullable: true),
                    Amount = table.Column<double>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    TransactionDate = table.Column<DateTime>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: true),
                    OfficeId = table.Column<int>(nullable: true),
                    FinancialYearId = table.Column<int>(nullable: true),
                    AccountNo = table.Column<int>(nullable: true),
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
                    ProjectId = table.Column<int>(nullable: true),
                    BudgetLineId = table.Column<int>(nullable: true),
                    ChartAccountDetailAccountCode = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoucherTransactions", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_VoucherTransactions_ChartAccountDetail_AccountNo",
                        column: x => x.AccountNo,
                        principalTable: "ChartAccountDetail",
                        principalColumn: "AccountCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VoucherTransactions_ChartAccountDetail_ChartAccountDetailAccountCode",
                        column: x => x.ChartAccountDetailAccountCode,
                        principalTable: "ChartAccountDetail",
                        principalColumn: "AccountCode",
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
                        name: "FK_VoucherTransactions_VoucherDetail_VoucherNo",
                        column: x => x.VoucherNo,
                        principalTable: "VoucherDetail",
                        principalColumn: "VoucherNo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeMonthlyAttendance",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    MonthlyAttendanceId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    AdvanceId = table.Column<int>(nullable: true)
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    EmployeePaymentTypesId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    FeedbackId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                name: "StoreItemPurchases",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    PurchaseId = table.Column<string>(nullable: false),
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
                    BudgetLineId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreItemPurchases", x => x.PurchaseId);
                    table.ForeignKey(
                        name: "FK_StoreItemPurchases_ProjectBudgetLine_BudgetLineId",
                        column: x => x.BudgetLineId,
                        principalTable: "ProjectBudgetLine",
                        principalColumn: "BudgetLineId",
                        onDelete: ReferentialAction.Restrict);
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
                name: "ItemPurchaseDocuments",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    DocumentId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    GeneratorId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    VehicleId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    OrderId = table.Column<string>(nullable: false),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    MaintenanceId = table.Column<string>(nullable: false),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    PartId = table.Column<string>(nullable: false),
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
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    FuelId = table.Column<string>(nullable: false),
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
                    { 1, "Inventory Account", null, null, false, null, null },
                    { 2, "Salary Account", null, null, false, null, null }
                });

            migrationBuilder.InsertData(
                table: "AccountHeadType",
                columns: new[] { "AccountHeadTypeId", "AccountHeadTypeName", "CreatedById", "CreatedDate", "IsDeleted", "ModifiedById", "ModifiedDate" },
                values: new object[,]
                {
                    { 1, "Assets", null, null, false, null, null },
                    { 2, "Liabilities", null, null, false, null, null },
                    { 3, "Donors Equity", null, null, false, null, null },
                    { 4, "Income", null, null, false, null, null },
                    { 5, "Expense", null, null, false, null, null }
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
                table: "CodeType",
                columns: new[] { "CodeTypeId", "CodeTypeName" },
                values: new object[,]
                {
                    { 3, "Repair Shops" },
                    { 4, "Individual/Others" },
                    { 5, "Locations/Stores" },
                    { 2, "Suppliers" },
                    { 1, "Organizations" }
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
                    { 3, null, null, "PKR", "Pakistani Rupees", null, false, null, null, false, true },
                    { 1, null, null, "AFG", "Afghanistan", null, false, null, null, true, false }
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
                    { 2, "PartTime" },
                    { 1, "Probationary" },
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
                table: "GenderConsiderationDetail",
                columns: new[] { "GenderConsiderationId", "CreatedById", "CreatedDate", "GenderConsiderationName", "IsDeleted", "ModifiedById", "ModifiedDate" },
                values: new object[,]
                {
                    { 7L, null, null, "5 % F - 95 % M Poor", false, null, null },
                    { 6L, null, null, "10 % F - 90 % M Poor", false, null, null },
                    { 5L, null, null, "20 % F - 80 % M Poor", false, null, null },
                    { 4L, null, null, "25 % F - 75 % M Poor", false, null, null },
                    { 2L, null, null, "40 % F - 60 % M Very Good", false, null, null },
                    { 1L, null, null, "50 % F - 50 % M Excellent", false, null, null },
                    { 8L, null, null, "0 % F - 100 % M Poor", false, null, null },
                    { 3L, null, null, "30 % F - 70 % M Good", false, null, null }
                });

            migrationBuilder.InsertData(
                table: "LanguageDetail",
                columns: new[] { "LanguageId", "CreatedById", "CreatedDate", "IsDeleted", "LanguageName", "ModifiedById", "ModifiedDate" },
                values: new object[,]
                {
                    { 11, null, null, false, "Uzbek", null, null },
                    { 1, null, null, false, "Arabic", null, null },
                    { 2, null, null, false, "Dari", null, null },
                    { 3, null, null, false, "English", null, null },
                    { 4, null, null, false, "French", null, null },
                    { 5, null, null, false, "German", null, null },
                    { 10, null, null, false, "Urdu", null, null },
                    { 6, null, null, false, "Pashto", null, null },
                    { 7, null, null, false, "Russian", null, null },
                    { 8, null, null, false, "Turkish", null, null },
                    { 9, null, null, false, "Turkmani", null, null }
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
                    { 4, null, null, false, null, null, "Take Over" },
                    { 2, null, null, false, null, null, "Transfers" },
                    { 5, null, null, false, null, null, "Loan" },
                    { 3, null, null, false, null, null, "Donation" },
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
                    { 6, null, null, null, "Other Allowance", "Other Allowance", 1, false, null, null, 2 },
                    { 5, null, null, null, "Security Deduction", "Security Deduction", 2, false, null, null, 1 },
                    { 4, null, null, null, "Capacity Building Deduction", "Capacity Building Deduction", 2, false, null, null, 1 },
                    { 3, null, null, null, "Fine Deduction", "Fine Deduction", 2, false, null, null, 1 },
                    { 2, null, null, null, "Food Allowance", "Food Allowance", 1, false, null, null, 2 },
                    { 11, null, null, null, "Basic Pay (In hours)", "Basic Pay (In hours)", 3, false, null, null, 2 }
                });

            migrationBuilder.InsertData(
                table: "SecurityConsiderationDetail",
                columns: new[] { "SecurityConsiderationId", "CreatedById", "CreatedDate", "IsDeleted", "ModifiedById", "ModifiedDate", "SecurityConsiderationName" },
                values: new object[,]
                {
                    { 7L, null, null, false, null, null, "Resources can be deployed partially" },
                    { 2L, null, null, false, null, null, "Beneficiaries cannot be reached" },
                    { 3L, null, null, false, null, null, "Resources cannot be deployed" },
                    { 4L, null, null, false, null, null, "Threat exit for future (Highly)" },
                    { 5L, null, null, false, null, null, "Project staff access the are partially" },
                    { 11L, null, null, false, null, null, "Future Threats expected" },
                    { 10L, null, null, false, null, null, "No obstacle for deploying Resources & office" },
                    { 9L, null, null, false, null, null, "No barrier for staff to access the area" },
                    { 8L, null, null, false, null, null, "Future Threats exits" },
                    { 1L, null, null, false, null, null, "Project Staff Cannot Visit Project Site" },
                    { 6L, null, null, false, null, null, "Bonfires can be reached partially" }
                });

            migrationBuilder.InsertData(
                table: "SecurityDetail",
                columns: new[] { "SecurityId", "CreatedById", "CreatedDate", "IsDeleted", "ModifiedById", "ModifiedDate", "SecurityName" },
                values: new object[,]
                {
                    { 3L, null, null, false, null, null, "Secure (Green Area)" },
                    { 2L, null, null, false, null, null, "Partially Insecure" },
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
                name: "IX_AnalyticalDetail_CreatedById",
                table: "AnalyticalDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AnalyticalDetail_ModifiedById",
                table: "AnalyticalDetail",
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
                name: "IX_ChartAccountDetail_AccountCode",
                table: "ChartAccountDetail",
                column: "AccountCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChartAccountDetail_AccountLevelId",
                table: "ChartAccountDetail",
                column: "AccountLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_ChartAccountDetail_AccountTypeId",
                table: "ChartAccountDetail",
                column: "AccountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ChartAccountDetail_CreatedById",
                table: "ChartAccountDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ChartAccountDetail_ModifiedById",
                table: "ChartAccountDetail",
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
                name: "IX_ContractDetails_LanguageId",
                table: "ContractDetails",
                column: "LanguageId");

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
                name: "IX_DonorCriteriaDetail_CreatedById",
                table: "DonorCriteriaDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DonorCriteriaDetail_ModifiedById",
                table: "DonorCriteriaDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_DonorDetail_CreatedById",
                table: "DonorDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_DonorDetail_ModifiedById",
                table: "DonorDetail",
                column: "ModifiedById");

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
                name: "IX_EmployeeSalaryAnalyticalInfo_BudgetLineId",
                table: "EmployeeSalaryAnalyticalInfo",
                column: "BudgetLineId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaryAnalyticalInfo_CreatedById",
                table: "EmployeeSalaryAnalyticalInfo",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeSalaryAnalyticalInfo_EmployeeID",
                table: "EmployeeSalaryAnalyticalInfo",
                column: "EmployeeID");

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
                name: "IX_FinancialYearDetail_CreatedById",
                table: "FinancialYearDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialYearDetail_ModifiedById",
                table: "FinancialYearDetail",
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
                name: "IX_InterviewDetails_CreatedById",
                table: "InterviewDetails",
                column: "CreatedById");

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
                name: "IX_NotesMaster_AccountCode",
                table: "NotesMaster",
                column: "AccountCode");

            migrationBuilder.CreateIndex(
                name: "IX_NotesMaster_AccountTypeId",
                table: "NotesMaster",
                column: "AccountTypeId");

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
                name: "IX_PayrollAccountHead_CreatedById",
                table: "PayrollAccountHead",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PayrollAccountHead_ModifiedById",
                table: "PayrollAccountHead",
                column: "ModifiedById");

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
                name: "IX_StoreItemPurchases_BudgetLineId",
                table: "StoreItemPurchases",
                column: "BudgetLineId");

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
                name: "IX_VoucherDetail_ChartAccountDetailAccountCode",
                table: "VoucherDetail",
                column: "ChartAccountDetailAccountCode");

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
                name: "IX_VoucherDocumentDetail_ModifiedById",
                table: "VoucherDocumentDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherDocumentDetail_VoucherNo",
                table: "VoucherDocumentDetail",
                column: "VoucherNo");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactions_AccountNo",
                table: "VoucherTransactions",
                column: "AccountNo");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactions_ChartAccountDetailAccountCode",
                table: "VoucherTransactions",
                column: "ChartAccountDetailAccountCode");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactions_CreatedById",
                table: "VoucherTransactions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactions_CurrencyId",
                table: "VoucherTransactions",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactions_ModifiedById",
                table: "VoucherTransactions",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactions_OfficeId",
                table: "VoucherTransactions",
                column: "OfficeId");

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
                name: "IX_VoucherTransactions_TransactionDate_AccountNo",
                table: "VoucherTransactions",
                columns: new[] { "TransactionDate", "AccountNo" });

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
                name: "AccountNoteDetail");

            migrationBuilder.DropTable(
                name: "AnalyticalDetail");

            migrationBuilder.DropTable(
                name: "ApproveProjectDetails");

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
                name: "ChartOfAccountNew");

            migrationBuilder.DropTable(
                name: "ContractDetails");

            migrationBuilder.DropTable(
                name: "ContractTypeContent");

            migrationBuilder.DropTable(
                name: "DistrictDetail");

            migrationBuilder.DropTable(
                name: "DonorCriteriaDetail");

            migrationBuilder.DropTable(
                name: "DonorDetail");

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
                name: "ExchangeRateDetail");

            migrationBuilder.DropTable(
                name: "ExchangeRates");

            migrationBuilder.DropTable(
                name: "ExistInterviewDetails");

            migrationBuilder.DropTable(
                name: "GenderConsiderationDetail");

            migrationBuilder.DropTable(
                name: "HolidayWeeklyDetails");

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
                name: "ItemPurchaseDocuments");

            migrationBuilder.DropTable(
                name: "ItemSpecificationDetails");

            migrationBuilder.DropTable(
                name: "JobPriceDetails");

            migrationBuilder.DropTable(
                name: "LoggerDetails");

            migrationBuilder.DropTable(
                name: "MotorMaintenances");

            migrationBuilder.DropTable(
                name: "MotorSpareParts");

            migrationBuilder.DropTable(
                name: "NotesMaster");

            migrationBuilder.DropTable(
                name: "PayrollMonthlyHourDetail");

            migrationBuilder.DropTable(
                name: "PensionPaymentHistory");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "PermissionsInRoles");

            migrationBuilder.DropTable(
                name: "ProjectArea");

            migrationBuilder.DropTable(
                name: "ProjectAssignTo");

            migrationBuilder.DropTable(
                name: "ProjectCommunicationAttachment");

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
                name: "RatingBasedCriteria");

            migrationBuilder.DropTable(
                name: "SalaryTaxReportContent");

            migrationBuilder.DropTable(
                name: "SecurityConsiderationDetail");

            migrationBuilder.DropTable(
                name: "SecurityDetail");

            migrationBuilder.DropTable(
                name: "StoreSourceCodeDetail");

            migrationBuilder.DropTable(
                name: "StrengthConsiderationDetail");

            migrationBuilder.DropTable(
                name: "StrongandWeakPoints");

            migrationBuilder.DropTable(
                name: "TechnicalQuestion");

            migrationBuilder.DropTable(
                name: "UserDetailOffices");

            migrationBuilder.DropTable(
                name: "UserDetails");

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
                name: "AccountFilterType");

            migrationBuilder.DropTable(
                name: "ClientDetails");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "MediaCategories");

            migrationBuilder.DropTable(
                name: "UnitRates");

            migrationBuilder.DropTable(
                name: "EmailType");

            migrationBuilder.DropTable(
                name: "AppraisalGeneralQuestions");

            migrationBuilder.DropTable(
                name: "HolidayDetails");

            migrationBuilder.DropTable(
                name: "LeaveReasonDetail");

            migrationBuilder.DropTable(
                name: "LanguageDetail");

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
                name: "ProfessionDetails");

            migrationBuilder.DropTable(
                name: "JobHiringDetails");

            migrationBuilder.DropTable(
                name: "InterviewDetails");

            migrationBuilder.DropTable(
                name: "ItemSpecificationMaster");

            migrationBuilder.DropTable(
                name: "JobDetails");

            migrationBuilder.DropTable(
                name: "AreaDetail");

            migrationBuilder.DropTable(
                name: "ProjectCommunication");

            migrationBuilder.DropTable(
                name: "ProgramDetail");

            migrationBuilder.DropTable(
                name: "SectorDetails");

            migrationBuilder.DropTable(
                name: "CodeType");

            migrationBuilder.DropTable(
                name: "PurchaseGenerators");

            migrationBuilder.DropTable(
                name: "StorePurchaseOrders");

            migrationBuilder.DropTable(
                name: "PurchaseVehicles");

            migrationBuilder.DropTable(
                name: "ActivityMaster");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "ActivityTypes");

            migrationBuilder.DropTable(
                name: "Mediums");

            migrationBuilder.DropTable(
                name: "Natures");

            migrationBuilder.DropTable(
                name: "Qualities");

            migrationBuilder.DropTable(
                name: "TimeCategories");

            migrationBuilder.DropTable(
                name: "JobGrade");

            migrationBuilder.DropTable(
                name: "JobPhases");

            migrationBuilder.DropTable(
                name: "ProjectDetail");

            migrationBuilder.DropTable(
                name: "StoreItemPurchases");

            migrationBuilder.DropTable(
                name: "TaskMaster");

            migrationBuilder.DropTable(
                name: "ProjectPhaseDetails");

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
                name: "StoreInventories");

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
                name: "ProjectBudgetLine");

            migrationBuilder.DropTable(
                name: "CurrencyDetails");

            migrationBuilder.DropTable(
                name: "FinancialYearDetail");

            migrationBuilder.DropTable(
                name: "JournalDetail");

            migrationBuilder.DropTable(
                name: "OfficeDetail");

            migrationBuilder.DropTable(
                name: "VoucherType");

            migrationBuilder.DropTable(
                name: "ChartAccountDetail");

            migrationBuilder.DropTable(
                name: "CountryDetails");

            migrationBuilder.DropTable(
                name: "BudgetLineType");

            migrationBuilder.DropTable(
                name: "ProjectDetails");

            migrationBuilder.DropTable(
                name: "AccountLevel");

            migrationBuilder.DropTable(
                name: "AccountType");

            migrationBuilder.DropTable(
                name: "AccountHeadType");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class initial : Migration
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
                name: "AccountType",
                columns: table => new
                {
                    AccountTypeId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    AccountTypeName = table.Column<string>(maxLength: 100, nullable: true),
                    AccountCategory = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountType", x => x.AccountTypeId);
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
                    CreatedDate = table.Column<DateTime>(nullable: false),
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
                name: "AnalyticalDetail",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                    BLAmount = table.Column<float>(nullable: false),
                    BLCurrCode = table.Column<string>(maxLength: 5, nullable: true),
                    CostBook = table.Column<string>(maxLength: 10, nullable: true),
                    Status = table.Column<byte>(nullable: false),
                    DonorCode = table.Column<string>(maxLength: 50, nullable: true),
                    BLType = table.Column<byte>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    ReceivedAmount = table.Column<float>(nullable: false),
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
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    AppraisalGeneralQuestionsId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    SequenceNo = table.Column<int>(nullable: false),
                    Question = table.Column<string>(nullable: true),
                    DariQuestion = table.Column<string>(nullable: true),
                    OfficeId = table.Column<int>(nullable: false)
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
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                name: "CategoryPopulator",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    AccountCode = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ChartOfAccountCode = table.Column<long>(nullable: false),
                    AccountName = table.Column<string>(maxLength: 100, nullable: true),
                    AccountLevelId = table.Column<int>(nullable: false),
                    AccountTypeId = table.Column<int>(nullable: true),
                    ParentID = table.Column<int>(nullable: true)
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
                    table.ForeignKey(
                        name: "FK_ChartAccountDetail_ChartAccountDetail_ParentID",
                        column: x => x.ParentID,
                        principalTable: "ChartAccountDetail",
                        principalColumn: "AccountCode",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CountryDetails",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CurrencyId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CurrencyCode = table.Column<string>(maxLength: 5, nullable: true),
                    CurrencyName = table.Column<string>(maxLength: 50, nullable: true),
                    CurrencyRate = table.Column<float>(nullable: true),
                    Status = table.Column<bool>(nullable: false)
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
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DesignationId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Designation = table.Column<string>(maxLength: 100, nullable: true)
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
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                name: "EmailType",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                name: "EmployeeEvaluation",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                name: "EmployeeType",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                name: "FinancialYearDetail",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                name: "InterviewDetails",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                name: "JournalDetail",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                name: "LeaveReasonDetail",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                name: "NationalityDetails",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                name: "OfficeDetail",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ProfessionId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ProfessionName = table.Column<string>(maxLength: 100, nullable: true)
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
                name: "ProjectDetails",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                name: "PurchaseUnitType",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                name: "RatingBasedCriteria",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                name: "SalaryHeadDetails",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    SalaryHeadId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    HeadTypeId = table.Column<int>(nullable: false),
                    HeadName = table.Column<string>(maxLength: 50, nullable: true),
                    Description = table.Column<string>(nullable: true)
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
                name: "StrongandWeakPoints",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                name: "UserDetailOffices",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                name: "ContractTypeContent",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                name: "EmployeeAppraisalQuestions",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                name: "NotesMaster",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                name: "ProvinceDetails",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                name: "ExchangeRates",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ExchangeRateId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    FromCurrency = table.Column<int>(nullable: false),
                    ToCurrency = table.Column<int>(nullable: false),
                    Rate = table.Column<double>(nullable: false)
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExchangeRates_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ExchangeRates_CurrencyDetails_ToCurrency",
                        column: x => x.ToCurrency,
                        principalTable: "CurrencyDetails",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmailSettingDetail",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                name: "Department",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                name: "HolidayDetails",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                name: "PayrollMonthlyHourDetail",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PayrollMonthlyHourID = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    OfficeId = table.Column<int>(nullable: false),
                    PayrollMonth = table.Column<int>(nullable: true),
                    PayrollYear = table.Column<int>(nullable: true),
                    Hours = table.Column<int>(nullable: true),
                    InTime = table.Column<DateTime>(nullable: true),
                    OutTime = table.Column<DateTime>(nullable: true)
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
                name: "JobHiringDetails",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                    table.ForeignKey(
                        name: "FK_JobHiringDetails_ProfessionDetails_ProfessionId",
                        column: x => x.ProfessionId,
                        principalTable: "ProfessionDetails",
                        principalColumn: "ProfessionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectBudget",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    BudgetId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ProjectId = table.Column<long>(nullable: false),
                    ReceivableAmount = table.Column<double>(nullable: false),
                    PayableAmount = table.Column<double>(nullable: false),
                    CurrentBalance = table.Column<double>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectBudget", x => x.BudgetId);
                    table.ForeignKey(
                        name: "FK_ProjectBudget_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectBudget_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectBudget_ProjectDetails_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetails",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectBudgetLine",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                name: "ProjectDocument",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ProjectDocumentId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    DocumentName = table.Column<string>(nullable: false),
                    DocumentDate = table.Column<DateTime>(nullable: false),
                    ProjectId = table.Column<long>(nullable: false),
                    FilePath = table.Column<byte[]>(nullable: false),
                    DocumentGUID = table.Column<string>(nullable: true),
                    Extension = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectDocument", x => x.ProjectDocumentId);
                    table.ForeignKey(
                        name: "FK_ProjectDocument_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectDocument_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProjectDocument_ProjectDetails_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetails",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskMaster",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                name: "EmployeeDetail",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    EmployeeID = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    EmployeeCode = table.Column<string>(maxLength: 20, nullable: true),
                    EmployeeTypeId = table.Column<int>(nullable: true),
                    EmployeeName = table.Column<string>(maxLength: 100, nullable: true),
                    IDCard = table.Column<string>(maxLength: 20, nullable: true),
                    FatherName = table.Column<string>(maxLength: 100, nullable: true),
                    GradeId = table.Column<int>(nullable: true),
                    PermanentAddress = table.Column<string>(maxLength: 200, nullable: true),
                    CurrentAddress = table.Column<string>(maxLength: 200, nullable: true),
                    City = table.Column<string>(maxLength: 50, nullable: true),
                    District = table.Column<string>(maxLength: 50, nullable: true),
                    ProvinceId = table.Column<int>(maxLength: 50, nullable: true),
                    CountryId = table.Column<int>(maxLength: 50, nullable: true),
                    Phone = table.Column<string>(maxLength: 50, nullable: true),
                    Fax = table.Column<string>(maxLength: 20, nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: true),
                    ReferBy = table.Column<string>(maxLength: 50, nullable: true),
                    Passport = table.Column<string>(maxLength: 50, nullable: true),
                    NationalityId = table.Column<int>(maxLength: 50, nullable: true),
                    Language = table.Column<string>(maxLength: 30, nullable: true),
                    SexId = table.Column<int>(maxLength: 5, nullable: true),
                    DateOfBirth = table.Column<string>(maxLength: 30, nullable: true),
                    Age = table.Column<int>(maxLength: 50, nullable: true),
                    PlaceOfBirth = table.Column<string>(nullable: true),
                    HigherQualificationId = table.Column<int>(maxLength: 50, nullable: true),
                    ProfessionId = table.Column<int>(maxLength: 50, nullable: true),
                    PreviousWork = table.Column<string>(maxLength: 50, nullable: true),
                    Remarks = table.Column<string>(nullable: true),
                    ExperienceYear = table.Column<int>(maxLength: 200, nullable: true),
                    ExperienceMonth = table.Column<int>(nullable: true),
                    Resume = table.Column<string>(nullable: true),
                    EmployeePhoto = table.Column<string>(nullable: true),
                    Extension = table.Column<string>(nullable: true),
                    DocumentGUID = table.Column<string>(nullable: true),
                    DocumentType = table.Column<int>(nullable: true),
                    EmployeePensionRateId = table.Column<int>(nullable: true),
                    MaritalStatus = table.Column<int>(nullable: false),
                    PassportNo = table.Column<string>(nullable: true),
                    University = table.Column<string>(nullable: true),
                    BirthPlace = table.Column<string>(nullable: true),
                    IssuePlace = table.Column<string>(nullable: true)
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
                        name: "FK_EmployeeDetail_EmployeePensionRate_EmployeePensionRateId",
                        column: x => x.EmployeePensionRateId,
                        principalTable: "EmployeePensionRate",
                        principalColumn: "EmployeePensionRateId",
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
                        name: "FK_EmployeeDetail_ProfessionDetails_ProfessionId",
                        column: x => x.ProfessionId,
                        principalTable: "ProfessionDetails",
                        principalColumn: "ProfessionId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeDetail_ProvinceDetails_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "ProvinceDetails",
                        principalColumn: "ProvinceId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BudgetPayable",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    BudgetPayableId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ProjectId = table.Column<long>(nullable: false),
                    ProjectBudgetBudgetId = table.Column<long>(nullable: true),
                    BudgetLineId = table.Column<long>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    ExpectedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetPayable", x => x.BudgetPayableId);
                    table.ForeignKey(
                        name: "FK_BudgetPayable_ProjectBudgetLine_BudgetLineId",
                        column: x => x.BudgetLineId,
                        principalTable: "ProjectBudgetLine",
                        principalColumn: "BudgetLineId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BudgetPayable_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BudgetPayable_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BudgetPayable_ProjectBudget_ProjectBudgetBudgetId",
                        column: x => x.ProjectBudgetBudgetId,
                        principalTable: "ProjectBudget",
                        principalColumn: "BudgetId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BudgetPayable_ProjectDetails_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "ProjectDetails",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BudgetReceivable",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                name: "VoucherDetail",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    VoucherNo = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CurrencyId = table.Column<int>(nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
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
                name: "ActivityMaster",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                name: "Advances",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                    AdvanceRecoveryDate = table.Column<DateTime>(nullable: false)
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
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                name: "BudgetLineEmployees",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    BudgetLineEmployeesId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    OfficeId = table.Column<int>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false),
                    BudgetLineId = table.Column<long>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    EmployeeName = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: true),
                    ProjectPercentage = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetLineEmployees", x => x.BudgetLineEmployeesId);
                    table.ForeignKey(
                        name: "FK_BudgetLineEmployees_ProjectBudgetLine_BudgetLineId",
                        column: x => x.BudgetLineId,
                        principalTable: "ProjectBudgetLine",
                        principalColumn: "BudgetLineId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BudgetLineEmployees_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BudgetLineEmployees_EmployeeDetail_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BudgetLineEmployees_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeApplyLeave",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ApplyLeaveId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    EmployeeId = table.Column<int>(nullable: false),
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
                        onDelete: ReferentialAction.Cascade);
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
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    EmployeeContractId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    EmployeeId = table.Column<int>(nullable: false),
                    FatherName = table.Column<string>(nullable: true),
                    EmployeeCode = table.Column<string>(nullable: true),
                    Designation = table.Column<int>(nullable: false),
                    ContractStartDate = table.Column<DateTime>(nullable: true),
                    ContractEndDate = table.Column<DateTime>(nullable: true),
                    DurationOfContract = table.Column<int>(nullable: false),
                    Salary = table.Column<double>(nullable: true),
                    Grade = table.Column<int>(nullable: true),
                    DutyStation = table.Column<int>(nullable: false),
                    Country = table.Column<int>(nullable: false),
                    Province = table.Column<int>(nullable: false),
                    Project = table.Column<int>(nullable: false),
                    BudgetLine = table.Column<long>(nullable: false),
                    Job = table.Column<string>(nullable: true),
                    WorkTime = table.Column<int>(nullable: false),
                    WorkDayHours = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeContract", x => x.EmployeeContractId);
                    table.ForeignKey(
                        name: "FK_EmployeeContract_ProjectBudgetLine_BudgetLine",
                        column: x => x.BudgetLine,
                        principalTable: "ProjectBudgetLine",
                        principalColumn: "BudgetLineId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeContract_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeContract_DesignationDetail_Designation",
                        column: x => x.Designation,
                        principalTable: "DesignationDetail",
                        principalColumn: "DesignationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeContract_OfficeDetail_DutyStation",
                        column: x => x.DutyStation,
                        principalTable: "OfficeDetail",
                        principalColumn: "OfficeId",
                        onDelete: ReferentialAction.Cascade);
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
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DocumentID = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    DocumentName = table.Column<string>(maxLength: 100, nullable: true),
                    DocumentDate = table.Column<DateTime>(nullable: true),
                    EmployeeID = table.Column<int>(nullable: false),
                    FilePath = table.Column<byte[]>(nullable: true),
                    DocumentGUID = table.Column<string>(nullable: true),
                    Extension = table.Column<string>(nullable: true)
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeDocumentDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeHealthDetail",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    HealthInfoId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    EmployeeId = table.Column<int>(nullable: false),
                    BloodGroup = table.Column<string>(maxLength: 20, nullable: true),
                    MedicalHistory = table.Column<string>(nullable: true),
                    SmokeAndDrink = table.Column<bool>(nullable: false),
                    Insurance = table.Column<bool>(nullable: false),
                    MedicalInsurance = table.Column<string>(nullable: true),
                    MeasureDiseases = table.Column<bool>(nullable: false),
                    AllergicSubstance = table.Column<bool>(nullable: false),
                    FamilyHistory = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeHealthDetail", x => x.HealthInfoId);
                    table.ForeignKey(
                        name: "FK_EmployeeHealthDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EmployeeHealthDetail_EmployeeDetail_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeHealthDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeHistoryDetail",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                name: "EmployeePaymentTypes",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                    table.PrimaryKey("PK_EmployeePaymentTypes", x => x.EmployeePaymentTypesId);
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeePaymentTypes_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeePayroll",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PayrollId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    EmployeeID = table.Column<int>(nullable: false),
                    SalaryHeadId = table.Column<int>(nullable: false),
                    MonthlyAmount = table.Column<double>(nullable: false),
                    CurrencyId = table.Column<int>(nullable: false),
                    PaymentType = table.Column<int>(nullable: false),
                    HeadTypeId = table.Column<int>(nullable: false),
                    PensionRate = table.Column<double>(nullable: true)
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
                        onDelete: ReferentialAction.Cascade);
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeePayrollForMonth",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                    EmployeeContractTypeId = table.Column<int>(nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "EmployeeSalaryDetails",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                name: "BudgetPayableAmount",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    BudgetPayableAmountId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    BudgetPayableId = table.Column<long>(nullable: false),
                    PaymentDate = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<double>(nullable: false),
                    Remark = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetPayableAmount", x => x.BudgetPayableAmountId);
                    table.ForeignKey(
                        name: "FK_BudgetPayableAmount_BudgetPayable_BudgetPayableId",
                        column: x => x.BudgetPayableId,
                        principalTable: "BudgetPayable",
                        principalColumn: "BudgetPayableId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BudgetPayableAmount_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BudgetPayableAmount_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BudgetReceivedAmount",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                name: "InventoryItems",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ItemId = table.Column<string>(nullable: false),
                    ItemInventory = table.Column<string>(nullable: true),
                    ItemName = table.Column<string>(nullable: true),
                    ItemCode = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Voucher = table.Column<long>(nullable: false),
                    ItemType = table.Column<int>(nullable: false)
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
                    table.ForeignKey(
                        name: "FK_InventoryItems_VoucherDetail_Voucher",
                        column: x => x.Voucher,
                        principalTable: "VoucherDetail",
                        principalColumn: "VoucherNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VoucherDocumentDetail",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DocumentID = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    DocumentName = table.Column<string>(maxLength: 100, nullable: true),
                    FilePath = table.Column<byte[]>(nullable: true),
                    DocumentDate = table.Column<DateTime>(nullable: true),
                    VoucherNo = table.Column<long>(nullable: false),
                    Extension = table.Column<string>(nullable: true),
                    DocumentGUID = table.Column<string>(nullable: true),
                    DocumentType = table.Column<int>(nullable: true)
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
                name: "VoucherTransactionDetails",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    TransactionId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    VoucherNo = table.Column<long>(nullable: false),
                    CreditAccount = table.Column<int>(nullable: true),
                    DebitAccount = table.Column<int>(nullable: true),
                    Amount = table.Column<double>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    TransactionDate = table.Column<DateTime>(nullable: false),
                    CurrencyId = table.Column<int>(nullable: true),
                    OfficeId = table.Column<int>(nullable: true),
                    FinancialYearId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoucherTransactionDetails", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_VoucherTransactionDetails_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VoucherTransactionDetails_ChartAccountDetail_CreditAccount",
                        column: x => x.CreditAccount,
                        principalTable: "ChartAccountDetail",
                        principalColumn: "AccountCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VoucherTransactionDetails_CurrencyDetails_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "CurrencyDetails",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VoucherTransactionDetails_ChartAccountDetail_DebitAccount",
                        column: x => x.DebitAccount,
                        principalTable: "ChartAccountDetail",
                        principalColumn: "AccountCode",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VoucherTransactionDetails_FinancialYearDetail_FinancialYearId",
                        column: x => x.FinancialYearId,
                        principalTable: "FinancialYearDetail",
                        principalColumn: "FinancialYearId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VoucherTransactionDetails_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VoucherTransactionDetails_OfficeDetail_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "OfficeDetail",
                        principalColumn: "OfficeId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VoucherTransactionDetails_VoucherDetail_VoucherNo",
                        column: x => x.VoucherNo,
                        principalTable: "VoucherDetail",
                        principalColumn: "VoucherNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssignActivity",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                name: "StoreItemPurchases",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                    DepreciationRate = table.Column<long>(nullable: false),
                    ImageFileType = table.Column<string>(nullable: true),
                    ImageFileName = table.Column<string>(nullable: true),
                    InvoiceFileType = table.Column<string>(nullable: true),
                    InvoiceFileName = table.Column<string>(nullable: true),
                    PurchasedById = table.Column<int>(nullable: false)
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
                        name: "FK_StoreItemPurchases_EmployeeDetail_PurchasedById",
                        column: x => x.PurchasedById,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreItemPurchases_PurchaseUnitType_UnitType",
                        column: x => x.UnitType,
                        principalTable: "PurchaseUnitType",
                        principalColumn: "UnitTypeId",
                        onDelete: ReferentialAction.Cascade);
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
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                name: "ItemPurchaseDocuments",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OrderId = table.Column<string>(nullable: false),
                    Purchase = table.Column<string>(nullable: true),
                    InventoryItem = table.Column<string>(nullable: true),
                    IssuedQuantity = table.Column<int>(nullable: false),
                    MustReturn = table.Column<bool>(nullable: false),
                    IssuedToEmployeeId = table.Column<int>(nullable: false),
                    IssueDate = table.Column<DateTime>(nullable: false),
                    ReturnedDate = table.Column<DateTime>(nullable: true)
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
                    CreatedDate = table.Column<DateTime>(nullable: false),
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
                    CreatedDate = table.Column<DateTime>(nullable: false),
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
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                column: "EmployeeId",
                unique: true);

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
                name: "IX_BudgetLineEmployees_BudgetLineId",
                table: "BudgetLineEmployees",
                column: "BudgetLineId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetLineEmployees_CreatedById",
                table: "BudgetLineEmployees",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetLineEmployees_EmployeeId",
                table: "BudgetLineEmployees",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetLineEmployees_ModifiedById",
                table: "BudgetLineEmployees",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetLineEmployees_OfficeId_ProjectId_BudgetLineId_IsActive",
                table: "BudgetLineEmployees",
                columns: new[] { "OfficeId", "ProjectId", "BudgetLineId", "IsActive" });

            migrationBuilder.CreateIndex(
                name: "IX_BudgetLineType_CreatedById",
                table: "BudgetLineType",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetLineType_ModifiedById",
                table: "BudgetLineType",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetPayable_BudgetLineId",
                table: "BudgetPayable",
                column: "BudgetLineId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetPayable_CreatedById",
                table: "BudgetPayable",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetPayable_ModifiedById",
                table: "BudgetPayable",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetPayable_ProjectBudgetBudgetId",
                table: "BudgetPayable",
                column: "ProjectBudgetBudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetPayable_ProjectId",
                table: "BudgetPayable",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetPayableAmount_BudgetPayableId",
                table: "BudgetPayableAmount",
                column: "BudgetPayableId");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetPayableAmount_CreatedById",
                table: "BudgetPayableAmount",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetPayableAmount_ModifiedById",
                table: "BudgetPayableAmount",
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
                name: "IX_ChartAccountDetail_ParentID",
                table: "ChartAccountDetail",
                column: "ParentID");

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
                name: "IX_EmployeeContract_BudgetLine",
                table: "EmployeeContract",
                column: "BudgetLine");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeContract_CreatedById",
                table: "EmployeeContract",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeContract_Designation",
                table: "EmployeeContract",
                column: "Designation");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeContract_DutyStation",
                table: "EmployeeContract",
                column: "DutyStation");

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
                name: "IX_EmployeeDetail_EmployeePensionRateId",
                table: "EmployeeDetail",
                column: "EmployeePensionRateId");

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
                name: "IX_EmployeeDetail_ProfessionId",
                table: "EmployeeDetail",
                column: "ProfessionId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDetail_ProvinceId",
                table: "EmployeeDetail",
                column: "ProvinceId");

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
                name: "IX_EmployeeHealthDetail_CreatedById",
                table: "EmployeeHealthDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeHealthDetail_EmployeeId",
                table: "EmployeeHealthDetail",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeHealthDetail_ModifiedById",
                table: "EmployeeHealthDetail",
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
                name: "IX_EmployeeType_CreatedById",
                table: "EmployeeType",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeType_ModifiedById",
                table: "EmployeeType",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRates_CreatedById",
                table: "ExchangeRates",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRates_FromCurrency",
                table: "ExchangeRates",
                column: "FromCurrency");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRates_ModifiedById",
                table: "ExchangeRates",
                column: "ModifiedById");

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
                name: "IX_InventoryItems_Voucher",
                table: "InventoryItems",
                column: "Voucher");

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
                name: "IX_JobHiringDetails_ProfessionId",
                table: "JobHiringDetails",
                column: "ProfessionId");

            migrationBuilder.CreateIndex(
                name: "IX_JournalDetail_CreatedById",
                table: "JournalDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_JournalDetail_ModifiedById",
                table: "JournalDetail",
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
                name: "IX_ProjectBudget_CreatedById",
                table: "ProjectBudget",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectBudget_ModifiedById",
                table: "ProjectBudget",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectBudget_ProjectId",
                table: "ProjectBudget",
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
                name: "IX_ProjectDetails_CreatedById",
                table: "ProjectDetails",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDetails_ModifiedById",
                table: "ProjectDetails",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDocument_CreatedById",
                table: "ProjectDocument",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDocument_ModifiedById",
                table: "ProjectDocument",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDocument_ProjectId",
                table: "ProjectDocument",
                column: "ProjectId");

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
                name: "IX_RatingBasedCriteria_CreatedById",
                table: "RatingBasedCriteria",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_RatingBasedCriteria_ModifiedById",
                table: "RatingBasedCriteria",
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
                name: "IX_StoreItemPurchases_PurchasedById",
                table: "StoreItemPurchases",
                column: "PurchasedById");

            migrationBuilder.CreateIndex(
                name: "IX_StoreItemPurchases_UnitType",
                table: "StoreItemPurchases",
                column: "UnitType");

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
                name: "IX_VoucherTransactionDetails_CreatedById",
                table: "VoucherTransactionDetails",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactionDetails_CreditAccount",
                table: "VoucherTransactionDetails",
                column: "CreditAccount");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactionDetails_CurrencyId",
                table: "VoucherTransactionDetails",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactionDetails_DebitAccount",
                table: "VoucherTransactionDetails",
                column: "DebitAccount");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactionDetails_FinancialYearId",
                table: "VoucherTransactionDetails",
                column: "FinancialYearId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactionDetails_ModifiedById",
                table: "VoucherTransactionDetails",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactionDetails_OfficeId",
                table: "VoucherTransactionDetails",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_VoucherTransactionDetails_VoucherNo",
                table: "VoucherTransactionDetails",
                column: "VoucherNo");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountNoteDetail");

            migrationBuilder.DropTable(
                name: "Advances");

            migrationBuilder.DropTable(
                name: "AnalyticalDetail");

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
                name: "BudgetLineEmployees");

            migrationBuilder.DropTable(
                name: "BudgetPayableAmount");

            migrationBuilder.DropTable(
                name: "BudgetReceivedAmount");

            migrationBuilder.DropTable(
                name: "CategoryPopulator");

            migrationBuilder.DropTable(
                name: "CodeType");

            migrationBuilder.DropTable(
                name: "ContractTypeContent");

            migrationBuilder.DropTable(
                name: "DistrictDetail");

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
                name: "EmployeeDocumentDetail");

            migrationBuilder.DropTable(
                name: "EmployeeEvaluation");

            migrationBuilder.DropTable(
                name: "EmployeeEvaluationTraining");

            migrationBuilder.DropTable(
                name: "EmployeeHealthDetail");

            migrationBuilder.DropTable(
                name: "EmployeeHistoryDetail");

            migrationBuilder.DropTable(
                name: "EmployeeMonthlyPayroll");

            migrationBuilder.DropTable(
                name: "EmployeePaymentTypes");

            migrationBuilder.DropTable(
                name: "EmployeePayroll");

            migrationBuilder.DropTable(
                name: "EmployeePayrollForMonth");

            migrationBuilder.DropTable(
                name: "EmployeePayrollMonth");

            migrationBuilder.DropTable(
                name: "EmployeeProfessionalDetail");

            migrationBuilder.DropTable(
                name: "EmployeeSalaryDetails");

            migrationBuilder.DropTable(
                name: "ExchangeRates");

            migrationBuilder.DropTable(
                name: "ExistInterviewDetails");

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
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "PermissionsInRoles");

            migrationBuilder.DropTable(
                name: "ProjectDocument");

            migrationBuilder.DropTable(
                name: "RatingBasedCriteria");

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
                name: "VoucherTransactionDetails");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AssignActivity");

            migrationBuilder.DropTable(
                name: "BudgetPayable");

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
                name: "InterviewDetails");

            migrationBuilder.DropTable(
                name: "PurchaseGenerators");

            migrationBuilder.DropTable(
                name: "StorePurchaseOrders");

            migrationBuilder.DropTable(
                name: "PurchaseVehicles");

            migrationBuilder.DropTable(
                name: "ActivityMaster");

            migrationBuilder.DropTable(
                name: "ProjectBudget");

            migrationBuilder.DropTable(
                name: "JobGrade");

            migrationBuilder.DropTable(
                name: "StoreItemPurchases");

            migrationBuilder.DropTable(
                name: "TaskMaster");

            migrationBuilder.DropTable(
                name: "InventoryItems");

            migrationBuilder.DropTable(
                name: "EmployeeDetail");

            migrationBuilder.DropTable(
                name: "PurchaseUnitType");

            migrationBuilder.DropTable(
                name: "StoreInventories");

            migrationBuilder.DropTable(
                name: "InventoryItemType");

            migrationBuilder.DropTable(
                name: "VoucherDetail");

            migrationBuilder.DropTable(
                name: "EmployeePensionRate");

            migrationBuilder.DropTable(
                name: "EmployeeType");

            migrationBuilder.DropTable(
                name: "QualificationDetails");

            migrationBuilder.DropTable(
                name: "NationalityDetails");

            migrationBuilder.DropTable(
                name: "ProfessionDetails");

            migrationBuilder.DropTable(
                name: "ProvinceDetails");

            migrationBuilder.DropTable(
                name: "ProjectBudgetLine");

            migrationBuilder.DropTable(
                name: "ChartAccountDetail");

            migrationBuilder.DropTable(
                name: "CurrencyDetails");

            migrationBuilder.DropTable(
                name: "JournalDetail");

            migrationBuilder.DropTable(
                name: "OfficeDetail");

            migrationBuilder.DropTable(
                name: "VoucherType");

            migrationBuilder.DropTable(
                name: "FinancialYearDetail");

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
                name: "AspNetUsers");
        }
    }
}

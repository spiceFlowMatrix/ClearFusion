using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class CriteriaTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EligibilityCriteriaDetail",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    EligibilityId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                });

            migrationBuilder.CreateTable(
                name: "FeasibilityCriteriaDetail",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    FeasibilityId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                });

            migrationBuilder.CreateTable(
                name: "FinancialCriteriaDetail",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    FinancialCriteriaDetailId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                });

            migrationBuilder.CreateTable(
                name: "PriorityCriteriaDetail",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    PriorityCriteriaDetailId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                });

            migrationBuilder.CreateTable(
                name: "PurposeofInitiativeCriteria",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ProductServiceId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    OtherActivity = table.Column<bool>(nullable: true),
                    TargetBenificaiaryWomen = table.Column<bool>(nullable: true),
                    TargetBenificiaryMen = table.Column<bool>(nullable: true),
                    TargetBenificiaryAgeGroup = table.Column<bool>(nullable: true),
                    TargetBenificiaryaOccupation = table.Column<bool>(nullable: true)
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
                });

            migrationBuilder.CreateTable(
                name: "RiskCriteriaDetail",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    RiskCriteriaDetailId = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                });

            migrationBuilder.CreateIndex(
                name: "IX_EligibilityCriteriaDetail_CreatedById",
                table: "EligibilityCriteriaDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_EligibilityCriteriaDetail_ModifiedById",
                table: "EligibilityCriteriaDetail",
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
                name: "IX_FinancialCriteriaDetail_CreatedById",
                table: "FinancialCriteriaDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialCriteriaDetail_ModifiedById",
                table: "FinancialCriteriaDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PriorityCriteriaDetail_CreatedById",
                table: "PriorityCriteriaDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PriorityCriteriaDetail_ModifiedById",
                table: "PriorityCriteriaDetail",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_PurposeofInitiativeCriteria_CreatedById",
                table: "PurposeofInitiativeCriteria",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PurposeofInitiativeCriteria_ModifiedById",
                table: "PurposeofInitiativeCriteria",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_RiskCriteriaDetail_CreatedById",
                table: "RiskCriteriaDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_RiskCriteriaDetail_ModifiedById",
                table: "RiskCriteriaDetail",
                column: "ModifiedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EligibilityCriteriaDetail");

            migrationBuilder.DropTable(
                name: "FeasibilityCriteriaDetail");

            migrationBuilder.DropTable(
                name: "FinancialCriteriaDetail");

            migrationBuilder.DropTable(
                name: "PriorityCriteriaDetail");

            migrationBuilder.DropTable(
                name: "PurposeofInitiativeCriteria");

            migrationBuilder.DropTable(
                name: "RiskCriteriaDetail");
        }
    }
}

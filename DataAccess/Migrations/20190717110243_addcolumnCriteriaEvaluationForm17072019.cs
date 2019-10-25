using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class addcolumnCriteriaEvaluationForm17072019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AcceleratedLearningProgram",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Advocacy",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "AggriculutreCapacityBuilding",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Awareness",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CapacityBuilding",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CommunicableDisease",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CommunityBasedEducation",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DisasterCapacityBuilding",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "DisasterRiskHygiene",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EducationCapacityBuilding",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EducationInEmergency",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmergencyResponse",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EnvironmentalHealth",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "FoodSecurity",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HealthCapacityBuilding",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Horticulture",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Hygiene",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Immunization",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IncomeGeneration",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "InfantandYoungChildFeeding",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Infrastructure",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Irrigation",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Literacy",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Livelihood",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "LivestockManagement",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "MentalHealthandDisabilityService",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "MitigationProjects",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Mobilization",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "NaturalResourceManagement",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Nutrition",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "OnlineEducation",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PeaceBuilding",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PrimaryHealthServices",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "RenewableEnergy",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ReproductiveHealth",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ResearchandPublication",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Sanitation",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SchoolUpgrading",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Shelter",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SocialProtection",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SustainableLivelihood",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Telemedicine",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ValueChain",
                table: "PurposeofInitiativeCriteria",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "WaterSupply",
                table: "PurposeofInitiativeCriteria",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcceleratedLearningProgram",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "Advocacy",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "AggriculutreCapacityBuilding",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "Awareness",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "CapacityBuilding",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "CommunicableDisease",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "CommunityBasedEducation",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "DisasterCapacityBuilding",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "DisasterRiskHygiene",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "EducationCapacityBuilding",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "EducationInEmergency",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "EmergencyResponse",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "EnvironmentalHealth",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "FoodSecurity",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "HealthCapacityBuilding",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "Horticulture",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "Hygiene",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "Immunization",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "IncomeGeneration",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "InfantandYoungChildFeeding",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "Infrastructure",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "Irrigation",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "Literacy",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "Livelihood",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "LivestockManagement",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "MentalHealthandDisabilityService",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "MitigationProjects",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "Mobilization",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "NaturalResourceManagement",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "Nutrition",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "OnlineEducation",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "PeaceBuilding",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "PrimaryHealthServices",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "RenewableEnergy",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "ReproductiveHealth",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "ResearchandPublication",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "Sanitation",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "SchoolUpgrading",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "Shelter",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "SocialProtection",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "SustainableLivelihood",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "Telemedicine",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "ValueChain",
                table: "PurposeofInitiativeCriteria");

            migrationBuilder.DropColumn(
                name: "WaterSupply",
                table: "PurposeofInitiativeCriteria");
        }
    }
}

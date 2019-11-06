using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class addCandidateDetailsTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CandidateDetails",
                columns: table => new
                {
                    CandidateId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    AccountStatus = table.Column<int>(nullable: false),
                    GenderId = table.Column<int>(nullable: false),
                    CountryId = table.Column<int>(nullable: false),
                    ProvinceId = table.Column<int>(nullable: false),
                    DistrictID = table.Column<long>(nullable: false),
                    ExperienceInMonth = table.Column<int>(nullable: false),
                    ExperienceInYear = table.Column<int>(nullable: false),
                    IsShortListed = table.Column<bool>(nullable: false),
                    IsSelected = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateDetails", x => x.CandidateId);
                    table.ForeignKey(
                        name: "FK_CandidateDetails_CountryDetails_CountryId",
                        column: x => x.CountryId,
                        principalTable: "CountryDetails",
                        principalColumn: "CountryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidateDetails_DistrictDetail_DistrictID",
                        column: x => x.DistrictID,
                        principalTable: "DistrictDetail",
                        principalColumn: "DistrictID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidateDetails_ProvinceDetails_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "ProvinceDetails",
                        principalColumn: "ProvinceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CandidateDetails_CountryId",
                table: "CandidateDetails",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateDetails_DistrictID",
                table: "CandidateDetails",
                column: "DistrictID");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateDetails_ProvinceId",
                table: "CandidateDetails",
                column: "ProvinceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidateDetails");
        }
    }
}

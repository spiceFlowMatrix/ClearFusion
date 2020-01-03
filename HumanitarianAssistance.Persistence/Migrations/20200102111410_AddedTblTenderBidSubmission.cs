using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class AddedTblTenderBidSubmission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MultiCurrencyOpeningPension",
                columns: table => new
                {
                    PensionId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    EmployeeID = table.Column<int>(nullable: true),
                    PensionStartDate = table.Column<DateTime>(nullable: true),
                    CurrencyId = table.Column<int>(nullable: true),
                    Amount = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultiCurrencyOpeningPension", x => x.PensionId);
                    table.ForeignKey(
                        name: "FK_MultiCurrencyOpeningPension_CurrencyDetails_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "CurrencyDetails",
                        principalColumn: "CurrencyId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MultiCurrencyOpeningPension_EmployeeDetail_EmployeeID",
                        column: x => x.EmployeeID,
                        principalTable: "EmployeeDetail",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TenderBidSubmission",
                columns: table => new
                {
                    BidId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Owner = table.Column<string>(nullable: true),
                    OpeningDate = table.Column<DateTime>(nullable: false),
                    SecurityDate = table.Column<DateTime>(nullable: false),
                    QuotedAmount = table.Column<double>(nullable: false),
                    SecurityAmount = table.Column<double>(nullable: false),
                    isResultQualified = table.Column<bool>(nullable: false),
                    Profile_Experience = table.Column<int>(nullable: false),
                    Securities_BankGuarantee = table.Column<int>(nullable: false),
                    OfferValidity = table.Column<int>(nullable: false),
                    OfferDocumentation = table.Column<int>(nullable: false),
                    TOR_SOWAcceptance = table.Column<int>(nullable: false),
                    Company_GoodsSpecification = table.Column<int>(nullable: false),
                    WorkExperience = table.Column<int>(nullable: false),
                    Service_Warranty = table.Column<int>(nullable: false),
                    DeliveryDate = table.Column<int>(nullable: false),
                    Certification_GMP_COPP = table.Column<int>(nullable: false),
                    LogisticRequestsId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TenderBidSubmission", x => x.BidId);
                    table.ForeignKey(
                        name: "FK_TenderBidSubmission_ProjectLogisticRequests_LogisticRequest~",
                        column: x => x.LogisticRequestsId,
                        principalTable: "ProjectLogisticRequests",
                        principalColumn: "LogisticRequestsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MultiCurrencyOpeningPension_CurrencyId",
                table: "MultiCurrencyOpeningPension",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_MultiCurrencyOpeningPension_EmployeeID",
                table: "MultiCurrencyOpeningPension",
                column: "EmployeeID");

            migrationBuilder.CreateIndex(
                name: "IX_TenderBidSubmission_LogisticRequestsId",
                table: "TenderBidSubmission",
                column: "LogisticRequestsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MultiCurrencyOpeningPension");

            migrationBuilder.DropTable(
                name: "TenderBidSubmission");
        }
    }
}

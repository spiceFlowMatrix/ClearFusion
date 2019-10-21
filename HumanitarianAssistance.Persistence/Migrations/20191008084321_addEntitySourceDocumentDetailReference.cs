using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class addEntitySourceDocumentDetailReference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EntitySourceDocumentDetails_DocumentFileId",
                table: "EntitySourceDocumentDetails");

            migrationBuilder.CreateIndex(
                name: "IX_EntitySourceDocumentDetails_DocumentFileId",
                table: "EntitySourceDocumentDetails",
                column: "DocumentFileId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EntitySourceDocumentDetails_DocumentFileId",
                table: "EntitySourceDocumentDetails");

            migrationBuilder.CreateIndex(
                name: "IX_EntitySourceDocumentDetails_DocumentFileId",
                table: "EntitySourceDocumentDetails",
                column: "DocumentFileId");
        }
    }
}

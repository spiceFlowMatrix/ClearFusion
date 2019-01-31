using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class districtSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DistrictDetail",
                columns: new[] { "DistrictID", "CreatedById", "CreatedDate", "District", "IsDeleted", "ModifiedById", "ModifiedDate", "ProvinceID" },
                values: new object[,]
                {
                    { 1L, null, null, "Jawand", false, null, null, 1 },
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
                    { 79L, null, null, "Aelaware", false, null, null, 40 },
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
                    { 67L, null, null, "Uakhar", false, null, null, 32 },
                    { 66L, null, null, "Bangi", false, null, null, 31 },
                    { 65L, null, null, "Balkhab", false, null, null, 30 },
                    { 92L, null, null, "Maryland", false, null, null, 52 },
                    { 64L, null, null, "Aybak", false, null, null, 29 },
                    { 93L, null, null, "Massachusetts", false, null, null, 53 },
                    { 95L, null, null, "Minnesota", false, null, null, 55 },
                    { 122L, null, null, "Wisconsin", false, null, null, 81 },
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
                    { 96L, null, null, "Mississippi", false, null, null, 56 },
                    { 94L, null, null, "Michigan", false, null, null, 54 },
                    { 63L, null, null, "Salang", false, null, null, 28 },
                    { 62L, null, null, "Kohi Safi", false, null, null, 28 },
                    { 61L, null, null, "Jabal Saraj", false, null, null, 28 },
                    { 28L, null, null, "Deh Sabz", false, null, null, 13 },
                    { 27L, null, null, "Chahar Asyab", false, null, null, 13 },
                    { 26L, null, null, "GuzDarzabara", false, null, null, 12 },
                    { 25L, null, null, "Fayzabad", false, null, null, 12 },
                    { 24L, null, null, "Aqcha", false, null, null, 12 },
                    { 23L, null, null, "Chishti Sharif", false, null, null, 11 },
                    { 22L, null, null, "Garmsir", false, null, null, 10 },
                    { 21L, null, null, "Baghran", false, null, null, 10 },
                    { 20L, null, null, "Tulak", false, null, null, 9 },
                    { 19L, null, null, "Shahrak", false, null, null, 9 },
                    { 18L, null, null, "Andar", false, null, null, 8 },
                    { 17L, null, null, "Ajristan", false, null, null, 8 },
                    { 16L, null, null, "Bilchiragh", false, null, null, 7 },
                    { 15L, null, null, "Almar", false, null, null, 7 },
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
                    { 2L, null, null, "Muqur", false, null, null, 1 },
                    { 29L, null, null, "Bagrami", false, null, null, 13 },
                    { 30L, null, null, "Daman", false, null, null, 14 },
                    { 31L, null, null, "Ghorak", false, null, null, 14 },
                    { 32L, null, null, "Alasay", false, null, null, 15 },
                    { 60L, null, null, "Chaharikar", false, null, null, 28 },
                    { 59L, null, null, "Bagram", false, null, null, 28 },
                    { 58L, null, null, "Anaba", false, null, null, 27 },
                    { 57L, null, null, "Chang", false, null, null, 26 },
                    { 56L, null, null, "Kal", false, null, null, 26 },
                    { 55L, null, null, "Barmal", false, null, null, 26 },
                    { 54L, null, null, "Dila", false, null, null, 26 },
                    { 53L, null, null, "Wuza Zadran", false, null, null, 25 },
                    { 52L, null, null, "Zurmat", false, null, null, 25 },
                    { 51L, null, null, "Jaji", false, null, null, 25 },
                    { 50L, null, null, "Gardez", false, null, null, 25 },
                    { 49L, null, null, "Mandol", false, null, null, 24 },
                    { 48L, null, null, "Kamdesh", false, null, null, 24 },
                    { 123L, null, null, "Wyoming", false, null, null, 82 },
                    { 47L, null, null, "Chakhansur", false, null, null, 23 },
                    { 45L, null, null, "Bati Kot", false, null, null, 22 },
                    { 44L, null, null, "Achin", false, null, null, 22 },
                    { 43L, null, null, "Maidan Wardak", false, null, null, 21 },
                    { 42L, null, null, "Charkh", false, null, null, 20 },
                    { 41L, null, null, "Baraki Barak", false, null, null, 20 },
                    { 40L, null, null, "Alishing", false, null, null, 19 },
                    { 39L, null, null, "Alingar", false, null, null, 19 },
                    { 38L, null, null, "Archi", false, null, null, 18 },
                    { 37L, null, null, "Ali Abad", false, null, null, 18 },
                    { 36L, null, null, "Bar Kunar", false, null, null, 17 },
                    { 35L, null, null, "Asadabad", false, null, null, 17 },
                    { 34L, null, null, "Gurbuz", false, null, null, 16 },
                    { 33L, null, null, "Bak", false, null, null, 16 },
                    { 46L, null, null, "Kang", false, null, null, 23 }
                });

            migrationBuilder.InsertData(
                table: "FinancialYearDetail",
                columns: new[] { "FinancialYearId", "CreatedById", "CreatedDate", "Description", "EndDate", "FinancialYearName", "IsDefault", "IsDeleted", "ModifiedById", "ModifiedDate", "StartDate" },
                values: new object[] { 1, null, null, null, new DateTime(2019, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), null, true, false, null, null, new DateTime(2019, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 15L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 16L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 17L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 18L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 19L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 20L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 21L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 22L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 23L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 24L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 25L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 26L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 27L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 28L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 29L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 30L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 31L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 32L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 33L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 34L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 35L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 36L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 37L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 38L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 39L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 40L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 41L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 42L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 43L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 44L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 45L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 46L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 47L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 48L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 49L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 50L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 51L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 52L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 53L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 54L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 55L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 56L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 57L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 58L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 59L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 60L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 61L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 62L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 63L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 64L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 65L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 66L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 67L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 68L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 69L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 70L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 71L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 72L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 73L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 74L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 75L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 76L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 77L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 78L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 79L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 80L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 81L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 82L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 83L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 84L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 85L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 86L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 87L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 88L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 89L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 90L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 91L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 92L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 93L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 94L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 95L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 96L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 97L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 98L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 99L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 100L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 101L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 102L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 103L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 104L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 105L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 106L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 107L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 108L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 109L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 110L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 111L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 112L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 113L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 114L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 115L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 116L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 117L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 118L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 119L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 120L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 121L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 122L);

            migrationBuilder.DeleteData(
                table: "DistrictDetail",
                keyColumn: "DistrictID",
                keyValue: 123L);

            migrationBuilder.DeleteData(
                table: "FinancialYearDetail",
                keyColumn: "FinancialYearId",
                keyValue: 1);
        }
    }
}

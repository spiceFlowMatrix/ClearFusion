using Microsoft.EntityFrameworkCore.Migrations;
using System.IO;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class updatedsp_211019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), @"SQL/sp_getbudgetlineexpenditurereport.sql")));
            migrationBuilder.Sql(System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), @"SQL/sp_getjournalreport.sql")));
            migrationBuilder.Sql(System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), @"SQL/sp_getledgerreport.sql")));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS public.get_budgetline_expenditure_report(integer, integer, text, text, integer[], integer[], bigint[], bigint[], bigint[], bigint[]);");
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS public.get_journal_report(integer, integer, text, text, integer[], integer[], bigint[])");
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS public.get_ledger_report(integer, text, text, integer, integer[], bigint[], boolean)");
        }
    }
}

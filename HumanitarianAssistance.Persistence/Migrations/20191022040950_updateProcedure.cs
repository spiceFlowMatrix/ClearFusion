using System.IO;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations {
    public partial class updateProcedure : Migration {
        protected override void Up (MigrationBuilder migrationBuilder) {
            migrationBuilder.Sql (System.IO.File.ReadAllText (Path.Combine (Directory.GetCurrentDirectory (), @"SQL/sp_getbudgetlinelist.sql")));
        }

        protected override void Down (MigrationBuilder migrationBuilder) {
            migrationBuilder.Sql ("DROP FUNCTION IF EXISTS public.get_budget_line_list(bigint);");
            migrationBuilder.Sql ("DROP FUNCTION IF EXISTS public.get_total_expenditure(bigint, integer, integer, timestamp without time zone)");
            migrationBuilder.Sql ("DROP FUNCTION IF EXISTS public.get_debit_percentage(bigint, double precision)");

        }
    }
}

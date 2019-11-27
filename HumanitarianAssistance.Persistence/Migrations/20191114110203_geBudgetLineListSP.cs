using System.IO;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class geBudgetLineListSP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), @"SQL/sp_getbudgetlinelist.sql")));

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS public.get_budget_line_list(bigint,text,text,text,text,text,text,text)");
        
        }
    }
}

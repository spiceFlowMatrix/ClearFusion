using System.IO;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class getProjectActivityListFilterSP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS public.get_activity_list_report_pdf(bigint[], bigint)");
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS public.get_project_projectactivitylist_filter(bigint, text, text, text, text, text, integer[], bigint[], boolean, boolean, boolean, integer, integer, integer, integer, integer, integer, boolean, boolean, boolean, integer)");
            migrationBuilder.Sql(System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), @"SQL/sp_getprojectactivitylistfilter.sql")));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS public.get_project_projectactivitylist_filter(bigint, text, text, text, text, text, integer[], bigint[], boolean, boolean, boolean, integer, integer, integer, integer, integer, integer, boolean, boolean, boolean, integer)");
        }
    }
}

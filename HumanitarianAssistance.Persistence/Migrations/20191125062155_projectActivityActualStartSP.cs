using System.IO;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class projectActivityActualStartSP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), @"SQL/sp_getprojectactivityactualstartdateminbyparentid.sql")));
            migrationBuilder.Sql(System.IO.File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), @"SQL/sp_getprojectactivityactualenddatemaxbyparentid.sql")));

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS public.get_project_activityactualstartdate_min_byparentid(bigint)");
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS public.get_project_activityactualenddate_max_byparentid(bigint)");
        }
    }
}

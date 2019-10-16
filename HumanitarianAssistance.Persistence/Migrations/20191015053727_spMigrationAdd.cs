using System;
using System.IO;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class spMigrationAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(System.IO.File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf("bin")), @"SQL/sp_get_activitylistreportpdf.sql")));
            migrationBuilder.Sql(System.IO.File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf("bin")), @"SQL/sp_getallvouchertransactionlist.sql")));
            migrationBuilder.Sql(System.IO.File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf("bin")), @"SQL/sp_getannualappraisalreport.sql")));
            migrationBuilder.Sql(System.IO.File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf("bin")), @"SQL/sp_getbudgetlinebreakdown.sql")));
            migrationBuilder.Sql(System.IO.File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf("bin")), @"SQL/sp_getbudgetlinelist.sql")));
            migrationBuilder.Sql(System.IO.File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf("bin")), @"SQL/sp_getdebitpercentage.sql")));
            migrationBuilder.Sql(System.IO.File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf("bin")), @"SQL/sp_getdetailofnotepdf.sql")));
            migrationBuilder.Sql(System.IO.File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf("bin")), @"SQL/sp_getexchangeratevalue.sql")));
            migrationBuilder.Sql(System.IO.File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf("bin")), @"SQL/sp_getjournalreport.sql")));
            migrationBuilder.Sql(System.IO.File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf("bin")), @"SQL/sp_getledgerreport.sql")));
            migrationBuilder.Sql(System.IO.File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf("bin")), @"SQL/sp_getprojectactivityactualenddatemaxbyparentid.sql")));
            migrationBuilder.Sql(System.IO.File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf("bin")), @"SQL/sp_getprojectactivityactualstartdateminbyparentid.sql")));
            migrationBuilder.Sql(System.IO.File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf("bin")), @"SQL/sp_getprojectactivitylist.sql")));
            migrationBuilder.Sql(System.IO.File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf("bin")), @"SQL/sp_getprojectactivityprogress.sql")));
            migrationBuilder.Sql(System.IO.File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf("bin")), @"SQL/sp_getprojectcashflow.sql")));
            migrationBuilder.Sql(System.IO.File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf("bin")), @"SQL/sp_getprojectotherdetailpdf.sql")));
            migrationBuilder.Sql(System.IO.File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf("bin")), @"SQL/sp_getprojectactivitylistfilter.sql")));
            migrationBuilder.Sql(System.IO.File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf("bin")), @"SQL/sp_getprojectproposalreport.sql")));
            migrationBuilder.Sql(System.IO.File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf("bin")), @"SQL/sp_getprojectproposalreportamountsummary.sql")));
            migrationBuilder.Sql(System.IO.File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf("bin")), @"SQL/sp_gettotalexpectedprojectbudget.sql")));
            migrationBuilder.Sql(System.IO.File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf("bin")), @"SQL/sp_gettotalexpenditure.sql")));
            migrationBuilder.Sql(System.IO.File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf("bin")), @"SQL/sp_gettransactionlist.sql")));
            migrationBuilder.Sql(System.IO.File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf("bin")), @"SQL/sp_gettrialbalancereport.sql")));
            migrationBuilder.Sql(System.IO.File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory.Substring(0, AppDomain.CurrentDomain.BaseDirectory.IndexOf("bin")), @"SQL/sp_getvouchersummaryreportvouchersbyfilter.sql")));


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS public.get_activity_list_report(bigint[], bigint);");
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS public.get_all_voucher_transaction_list(bigint)");
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS public.get_annual_appraisal_report(integer)");
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS public.get_budgetlinebreakdown(integer, bigint, text, text, bigint[]);");
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS public.get_budget_line_list(bigint);");
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS public.get_detailofnote_pdf(integer, text);");
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS public.get_exchangerate_value(double precision, integer, integer, timestamp without time zone);");
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS public.get_journal_report(integer, integer, text, text, integer[], integer[], bigint[]);");
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS public.get_ledger_report(integer, text, text, integer, integer[], bigint[], boolean);");
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS public.get_project_activityactualenddate_max_byparentid(bigint);");
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS public.get_project_activityactualstartdate_min_byparentid(bigint);");
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS public.get_project_projectactivitylist(integer);");
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS public.get_projectactivityprogress(bigint);");
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS public.get_projectcashflow(integer, bigint[], text, text, bigint);");
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS public.get_project_other_detail_pdf(integer);");
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS public.get_project_projectactivitylist_filter(bigint, text, text, text, text, text, integer[], bigint[], boolean, boolean, boolean, integer, integer, integer, integer, integer, integer, boolean, boolean, boolean,integer);");
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS public.get_projectproposalreport(character varying, text, text, integer, integer, integer, double precision, integer, boolean, boolean);");
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS public.get_projectproposalreportamountsummary(character varying, text, text, integer, integer, integer, double precision, integer, boolean, boolean);");
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS public.get_totalexpectedprojectbudget(bigint[], integer, text);");
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS public.get_total_expenditure(bigint,integer,integer,timestamp without time zone);");
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS public.get_transaction_list(bigint, integer);");
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS public.get_trialbalance_report(integer, text, text, integer, integer[], bigint[]);");
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS public.get_vouchersummaryreportvouchersbyfilter(bigint[], bigint[], integer, bigint[], integer[], bigint[], bigint[], integer);");
        }
    }
}

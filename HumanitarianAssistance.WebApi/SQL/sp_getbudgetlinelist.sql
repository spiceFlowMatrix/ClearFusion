DROP FUNCTION IF EXISTS public.get_total_expenditure(bigint, integer, integer, timestamp without time zone);
DROP FUNCTION IF EXISTS public.get_debit_percentage(bigint, double precision);
DROP FUNCTION IF EXISTS public.get_budget_line_list(bigint);

CREATE OR REPLACE FUNCTION public.get_budget_line_list(
	project_id bigint,
	budget_line_id text,
	budget_code text,
	budget_name text,
	project_job_id text,
	initial_budget text,
	date_value text,
	project_job_name text)
    RETURNS TABLE(budgetlineid bigint, budgetcode text, budgetname text, projectid bigint, currencyid integer, currencyname character varying, initialbudget double precision, projectjobcode text, projectjobid bigint, projectjobname text, createddate timestamp without time zone, debitpercentage double precision, expenditure double precision) 
    LANGUAGE 'plpgsql'

    COST 100
    VOLATILE 
    ROWS 1000
AS $BODY$

DECLARE 
project_id_value bigint = project_id;
debit_percentage_amt DOUBLE PRECISION;
expenditure_amt DOUBLE PRECISION;
debit_amt integer;
    _budget_line_id text = budget_line_id;
	_budget_code text = budget_code;
	_budget_name text= budget_name;
	_project_job_id text= project_job_id;
	_initial_budget text= initial_budget;
	_date_value text= date_value;
	_project_job_name text= project_job_name;
Begin
return Query select pbl."BudgetLineId",pbl."BudgetCode",pbl."BudgetName",pbl."ProjectId",pbl."CurrencyId",
cd."CurrencyName",pbl."InitialBudget",pjd."ProjectJobCode",pbl."ProjectJobId",pjd."ProjectJobName",
pbl."CreatedDate",(select (sum("Debit")/pbl."InitialBudget")*100 from "VoucherTransactions" where "BudgetLineId" = pbl."BudgetLineId" and "IsDeleted"=false) as "debit_percentage_amt"
,(select sum(get_exchangerate_value("Debit","CurrencyId",pbl."CurrencyId","TransactionDate")) from "VoucherTransactions" 
where "BudgetLineId" = pbl."BudgetLineId") as expenditure_amt		
from "ProjectBudgetLineDetail" as pbl
left join "CurrencyDetails" as cd
on pbl."CurrencyId" = cd."CurrencyId"
left join "ProjectJobDetail" as pjd
on pbl."ProjectJobId" = pjd."ProjectJobId"
where pbl."ProjectId" = project_id and pbl."IsDeleted" = false
AND (
 (
  CASE 
    WHEN _budget_line_id <> ''
    THEN pbl."BudgetLineId" = _budget_line_id ::bigint OR 
 							pbl."ProjectJobId" = _project_job_id::bigint OR 
 							pbl."InitialBudget" = _initial_budget::DOUBLE PRECISION OR 
 							pbl."ProjectJobId" = _project_job_id::bigint
    
    WHEN _budget_code <> ''
    THEN pbl."BudgetCode" ILIKE '%'||_budget_code||'%' OR pbl."BudgetName" ILIKE '%'||_budget_name||'%' 
   
    WHEN _budget_name <> ''
    THEN pbl."BudgetName" ILIKE '%'||_budget_name||'%'
 
 	WHEN _project_job_id <> ''	
 	THEN pbl."ProjectJobId" = _project_job_id::bigint
 
 	WHEN _initial_budget <> ''
 	THEN pbl."InitialBudget" = _initial_budget::DOUBLE PRECISION 
 
 	WHEN _date_value <> ''
 	THEN pbl."CreatedDate"::Date = _date_value::Date
 
 	WHEN _project_job_name <> ''
 	THEN pjd."ProjectJobName" ILIKE '%'||_project_job_name||'%'
 	
    ELSE
 	pbl."ProjectId" = project_id
 END
	)
)
order by pbl."CreatedDate";
END;

$BODY$;


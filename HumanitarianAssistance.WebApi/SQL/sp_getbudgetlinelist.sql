DROP FUNCTION IF EXISTS public.get_total_expenditure(bigint, integer, integer, timestamp without time zone);
DROP FUNCTION IF EXISTS public.get_debit_percentage(bigint, double precision);

CREATE OR REPLACE FUNCTION public.get_budget_line_list(project_id bigint)
returns TABLE (
budgetlineid bigint,
budgetcode text,
budgetname text,
projectid bigint,
currencyid integer,	
currencyname character varying(50),
initialbudget DOUBLE PRECISION,
projectjobcode text,	
projectjobid bigint,
projectjobname text,
createddate timestamp without time zone,
debitpercentage DOUBLE PRECISION,
expenditure DOUBLE PRECISION 
) 
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
order by pbl."CreatedDate";
END;
$BODY$;

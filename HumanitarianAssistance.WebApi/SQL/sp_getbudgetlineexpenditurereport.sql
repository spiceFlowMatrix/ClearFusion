CREATE OR REPLACE FUNCTION public.get_budgetline_expenditure_report(
	currencyid integer,
	recordtype integer,
	fromdate text,
	todate text,
	officelist integer[],
	journalno integer[],
	accountslist bigint[],
	project bigint[],
	budgetline bigint[],
	projectjob bigint[])
    RETURNS TABLE(chartofaccountnewcode text, description text, debit double precision, credit double precision, expenditure double precision) 
    LANGUAGE 'plpgsql'

    COST 100
    VOLATILE 
    ROWS 1000
AS $BODY$

-- Declaration
DECLARE 
	_currencyid integer = currencyid;
	_recordtype integer = recordtype; 
	_officelist integer[] = officelist; 
	_journalcode integer[] =  journalno; 
	_accountslist bigint[] = accountslist; 
    _fromdate text = fromdate; 
	_todate text = todate;
	_project bigint[] = project;
	_budgetline bigint[] = budgetline;
	_projectjob bigint[] = projectjob;
	Expenditure double precision;
	Debit double precision;
	Credit double precision;

BEGIN
	
	IF recordtype=1
	THEN
	RETURN QUERY SELECT ca."ChartOfAccountNewCode",vt."Description",vt."Debit",vt."Credit",
	(Select SUM(tempvt."Debit") 
	 from public."VoucherTransactions" as tempvt
	 Where (tempvt."TransactionId" <= vt."TransactionId") AND
	 (tempvt."IsDeleted"=false) AND (tempvt."BudgetLineId"=vt."BudgetLineId")
	 AND (tempvt."CurrencyId"=vt."CurrencyId")
	) as Expenditure
	FROM public."VoucherTransactions" as vt
	INNER JOIN public."ChartOfAccountNew" as ca on ca."ChartOfAccountNewId" = vt."ChartOfAccountNewId"
	INNER JOIN public."VoucherDetail" as vd on vd."VoucherNo"=vt."VoucherNo"
	WHERE vt."IsDeleted"=false AND vt."CurrencyId"=_currencyid
	AND (vd."VoucherDate" :: date >= _fromdate :: date
	   	AND  vd."VoucherDate" :: date <= _todate :: date)
	AND vt."ChartOfAccountNewId" = ANY(_accountslist)
 	AND vd."JournalCode" = ANY(_journalcode) 
 	AND vd."OfficeId" = ANY(_officelist)
	AND (_project = '{}' OR vt."ProjectId" = ANY(_project))
	AND (_budgetline = '{}' OR vt."BudgetLineId"= ANY(_budgetline))
	AND (_projectjob = '{}' OR vt."JobId"= ANY(_projectjob))
	ORDER BY vt."TransactionId";
	
	ELSE
	
	RETURN QUERY SELECT ca."ChartOfAccountNewCode",vt."Description",
	get_exchangerate_value(vt."Debit",vt."CurrencyId",_currencyid,vt."TransactionDate") as Debit,
	get_exchangerate_value(vt."Credit",vt."CurrencyId",_currencyid,vt."TransactionDate") as Credit,
	(Select SUM(get_exchangerate_value(tempvt."Debit",tempvt."CurrencyId",_currencyid,tempvt."TransactionDate")) 
	 from public."VoucherTransactions" as tempvt
	 Where (tempvt."TransactionId" <= vt."TransactionId") AND
	 (tempvt."IsDeleted"=false) AND (tempvt."BudgetLineId"=vt."BudgetLineId")
	) as Expenditure
	FROM public."VoucherTransactions" as vt
	INNER JOIN public."ChartOfAccountNew" as ca on ca."ChartOfAccountNewId" = vt."ChartOfAccountNewId"
	INNER JOIN public."VoucherDetail" as vd on vd."VoucherNo"=vt."VoucherNo"
	WHERE vt."IsDeleted"=false
	AND (vd."VoucherDate" :: date >= _fromdate :: date
	   	AND  vd."VoucherDate" :: date <= _todate :: date)
	AND vt."ChartOfAccountNewId" = ANY(_accountslist)
 	AND vd."JournalCode" = ANY(_journalcode) 
 	AND vd."OfficeId" = ANY(_officelist)
	AND (_project = '{}' OR vt."ProjectId" = ANY(_project))
	AND (_budgetline = '{}' OR vt."BudgetLineId"= ANY(_budgetline))
	AND (_projectjob = '{}' OR vt."JobId"= ANY(_projectjob))
	ORDER BY vt."TransactionId";
	
	END IF;
 
END;

$BODY$;
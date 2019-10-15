CREATE OR REPLACE FUNCTION public.get_journal_report(
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
    RETURNS TABLE(transactiondate date, voucherno bigint, referenceno text, transactiondescription text, currency integer, chartofaccountnewid integer, creditamount double precision, debitamount double precision, accountcode text, journalcode integer, accountname text, projectcode text, budgetcode text, projectjobcode text) 
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
	
--     fromdate Date :=  "fromdate" :: Date; 
-- 	todate Date := "todate" :: Date;
    --modelOfficeId integer:= officeid;
	--tocurrencyid integer;

BEGIN
CREATE TEMP TABLE temp_journalreport(
    	transactiondate date,
        voucherno bigint,
		referenceno text,
       	transactiondescription text,
		currency integer,
		chartofaccountnewid integer,
		creditamount double precision,
		debitamount double precision,
		accountcode text,
		journalcode integer,
	    accountname text,
		projectcode text,
		budgetcode	text,
		projectjobcode text
    );
	
	IF recordtype=1
	THEN
	INSERT INTO temp_journalreport
	SELECT vd."VoucherDate", vd."VoucherNo", vd."ReferenceNo", vt."Description",
	vd."CurrencyId", vt."ChartOfAccountNewId", 
	 ROUND(vt."Credit" :: numeric,4), ROUND(vt."Debit" :: numeric,4), coa."ChartOfAccountNewCode", vd."JournalCode", coa."AccountName",
	 pd."ProjectCode",pbd."BudgetCode",pjd."ProjectJobCode"
	FROM "VoucherDetail" vd
	LEFT JOIN "VoucherTransactions" vt ON vd."VoucherNo"= vt."VoucherNo"
	LEFT JOIN "ChartOfAccountNew" coa ON vt."ChartOfAccountNewId" = coa."ChartOfAccountNewId"
	LEFT JOIN "ProjectDetail" pd ON vt."ProjectId" =pd."ProjectId"
	LEFT JOIN "ProjectBudgetLineDetail" pbd ON vt."BudgetLineId" = pbd."BudgetLineId"
	LEFT JOIN "ProjectJobDetail" pjd ON vt."JobId" = pjd."ProjectJobId"
	WHERE vd."JournalCode" IN (select * from unnest(_journalcode)) AND vd."OfficeId" IN (select * from unnest(_officelist))  AND 
	vt."ChartOfAccountNewId" IN (select * from unnest(_accountslist)) AND vd."IsDeleted"= false AND
    vt."IsDeleted"= false AND coa."IsDeleted"= false AND
	vd."CurrencyId"= _currencyid AND vd."VoucherDate" :: Date >= _fromdate :: Date AND 
	vd."VoucherDate" :: Date <= _todate :: Date 
	AND (_project = '{}' OR vt."ProjectId" = ANY(_project))
	AND (_budgetline = '{}' OR vt."BudgetLineId"= ANY(_budgetline))
	AND (_projectjob = '{}' OR vt."JobId"= ANY(_projectjob))
	ORDER BY vt."TransactionDate" ASC;
	ELSE
	INSERT INTO temp_journalreport
	SELECT tt."VoucherDate", tt."VoucherNo", tt."ReferenceNo", tt."Description", 
	tt."CurrencyId", tt."ChartOfAccountNewId", ROUND(tt."Credit" :: numeric,4), ROUND(tt."Debit" :: numeric,4), tt."ChartOfAccountNewCode", 
	tt."JournalCode", tt."AccountName",tt."ProjectCode",tt."BudgetCode",tt."ProjectJobCode"
	FROM (SELECT vd."VoucherDate", vd."VoucherNo", vd."ReferenceNo", vt."Description",
	vd."CurrencyId", vt."ChartOfAccountNewId", 
	 vt."Credit" * (select "Rate" From "ExchangeRateDetail" Where "IsDeleted" = false AND
                        	  "FromCurrency" = vd."CurrencyId" AND
								"ToCurrency"= _currencyid AND
                              "Date":: Date <= vd."VoucherDate" :: Date 
                        order by "Date" DESC
                        limit 1) as "Credit", vt."Debit" * (select "Rate" From "ExchangeRateDetail" Where "IsDeleted" = false AND
                        	  "FromCurrency" = vd."CurrencyId" AND
								"ToCurrency"= _currencyid AND
                              "Date":: Date <= vd."VoucherDate" :: Date 
                        order by "Date" DESC
                        limit 1) as "Debit",coa."ChartOfAccountNewCode", vd."JournalCode", coa."AccountName",
		 				 pd."ProjectCode",pbd."BudgetCode",pjd."ProjectJobCode"
	FROM "VoucherDetail" vd
	LEFT JOIN "VoucherTransactions" vt ON vd."VoucherNo"= vt."VoucherNo"
	LEFT JOIN "ChartOfAccountNew" coa ON vt."ChartOfAccountNewId"=coa."ChartOfAccountNewId"
	LEFT JOIN "ProjectDetail" pd ON vt."ProjectId" =pd."ProjectId"
	LEFT JOIN "ProjectBudgetLineDetail" pbd ON vt."BudgetLineId" = pbd."BudgetLineId"
	LEFT JOIN "ProjectJobDetail" pjd ON vt."JobId" = pjd."ProjectJobId"
	WHERE vd."JournalCode" IN (select * from unnest(_journalcode)) AND vd."OfficeId" IN (select * from unnest(_officelist)) AND 
	vt."ChartOfAccountNewId" IN (select * from unnest(_accountslist))  AND vd."IsDeleted"= false 
		AND vt."IsDeleted"= false AND coa."IsDeleted"= false 
	AND vd."VoucherDate" :: Date >= _fromdate :: Date AND 
	vd."VoucherDate" :: Date <= _todate :: Date 
	AND (_project = '{}' OR vt."ProjectId" = ANY(_project))
	AND (_budgetline = '{}' OR vt."BudgetLineId"= ANY(_budgetline))
	AND (_projectjob = '{}' OR vt."JobId"= ANY(_projectjob))	  
	ORDER BY vt."TransactionDate" ASC) as tt;
	
	END IF;

	RETURN QUERY SELECT * from temp_journalreport;

 DROP TABLE temp_journalreport;
 
END;

$BODY$;
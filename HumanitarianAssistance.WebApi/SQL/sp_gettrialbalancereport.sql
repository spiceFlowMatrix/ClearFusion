CREATE OR REPLACE FUNCTION public.get_trialbalance_report(
	currency integer,
	fromdate text,
	todate text,
	recordtype integer,
	officelist integer[],
	accountslist bigint[])
    RETURNS TABLE(chartofaccountnewid bigint, accountname text, description text, currencyname text, debitamount double precision, creditamount double precision, transactiondate date, currencyid integer, chartofaccountnewcode text) 
    LANGUAGE 'plpgsql'

    COST 100
    VOLATILE 
    ROWS 1000
AS $BODY$

-- Declaration
DECLARE 
	_currencyid integer = currency;
	_officelist integer[] = officelist; 
	_accountslist bigint[] = accountslist; 
    _fromdate text = fromdate; 
	_todate text = todate;

BEGIN
CREATE TEMP TABLE temp_trialbalance_report(
    	chartofaccountnewid bigint,
		accountname text,
		description text,
		currencyname text,
		debitamount	double precision,
		creditamount double precision,
		transactiondate date,
		currencyid integer,
		chartofaccountnewcode text
    );
	
	IF recordtype=1
	THEN
	INSERT INTO temp_trialbalance_report
	SELECT vt."ChartOfAccountNewId", coa."AccountName", vt."Description",
	cd."CurrencyName",
	ROUND(vt."Debit" :: numeric,4), ROUND(vt."Credit" :: numeric,4), vd."VoucherDate", cd."CurrencyId", coa."ChartOfAccountNewCode"
	FROM "VoucherDetail" vd
	LEFT JOIN "VoucherTransactions" vt ON vd."VoucherNo"= vt."VoucherNo"
	LEFT JOIN "ChartOfAccountNew" coa ON vt."ChartOfAccountNewId" = coa."ChartOfAccountNewId"
	LEFT JOIN "CurrencyDetails" cd ON cd."CurrencyId"= vd."CurrencyId"
	WHERE vd."OfficeId" IN 
	(SELECT * FROM UNNEST(_officelist))  AND 
	vt."ChartOfAccountNewId" IN (SELECT * FROM UNNEST(_accountslist)) AND vd."IsDeleted"= false AND
	vt."IsDeleted"= false AND coa."IsDeleted"= false AND cd."IsDeleted"= false AND
	vd."CurrencyId"= _currencyid AND vd."VoucherDate" :: Date >= _fromdate :: Date AND 
	vd."VoucherDate" :: Date <= _todate :: Date ORDER BY vt."TransactionDate" ASC;
	ELSE
	INSERT INTO temp_trialbalance_report
	SELECT tt."ChartOfAccountNewId", tt."AccountName", tt."Description", 
	tt."CurrencyName", ROUND(tt."Debit" :: numeric,4), ROUND(tt."Credit" :: numeric,4), tt."VoucherDate", tt."CurrencyId", tt."ChartOfAccountNewCode"
	FROM (SELECT vt."ChartOfAccountNewId", coa."AccountName", vt."Description", cd."CurrencyName", 
	vt."Debit" * (SELECT "Rate" FROM "ExchangeRateDetail" WHERE "IsDeleted" = false AND
                        	  "FromCurrency" = vd."CurrencyId" AND
								"ToCurrency"= _currencyid AND
                              "Date":: Date <= vd."VoucherDate" :: Date 
                        ORDER BY "Date" DESC
                        LIMIT 1) as "Debit", vt."Credit" * (SELECT "Rate" FROM "ExchangeRateDetail" Where 
															"IsDeleted" = false AND
                        	  "FromCurrency" = vd."CurrencyId" AND
								"ToCurrency"= _currencyid AND
                              "Date":: Date <= vd."VoucherDate" :: Date 
                        ORDER BY "Date" DESC
                        LIMIT 1) as "Credit", vd."VoucherDate", cd."CurrencyId", coa."ChartOfAccountNewCode"
	FROM "VoucherDetail" vd
	LEFT JOIN "VoucherTransactions" vt ON vd."VoucherNo"= vt."VoucherNo"
	LEFT JOIN "ChartOfAccountNew" coa ON vt."ChartOfAccountNewId"=coa."ChartOfAccountNewId"
	LEFT JOIN "CurrencyDetails" cd ON cd."CurrencyId"= vd."CurrencyId"
	WHERE vd."OfficeId" IN (select * from unnest(_officelist)) AND 
	vt."ChartOfAccountNewId" IN (select * from unnest(_accountslist))  AND vd."IsDeleted"= false 
		AND vt."IsDeleted"= false AND coa."IsDeleted"= false AND "cd"."IsDeleted"= false
	AND vd."VoucherDate" :: Date >= _fromdate :: Date AND 
	vd."VoucherDate" :: Date <= _todate :: Date ORDER BY vt."TransactionDate" ASC) as tt;
	
	END IF;

	RETURN QUERY SELECT * from temp_trialbalance_report;

 DROP TABLE temp_trialbalance_report;
 
END;

$BODY$;
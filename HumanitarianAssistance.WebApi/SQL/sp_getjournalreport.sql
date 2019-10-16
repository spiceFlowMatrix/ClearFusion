-- FUNCTION: public.get_journal_report(integer, integer, text, text, integer[], integer[], bigint[])

-- DROP FUNCTION public.get_journal_report(integer, integer, text, text, integer[], integer[], bigint[]);

CREATE OR REPLACE FUNCTION public.get_journal_report(
	currencyid integer,
	recordtype integer,
	fromdate text,
	todate text,
	officelist integer[],
	journalno integer[],
	accountslist bigint[])
    RETURNS TABLE(transactiondate date, voucherno bigint, referenceno text, transactiondescription text, currency integer, chartofaccountnewid integer, creditamount double precision, debitamount double precision, accountcode text, journalcode integer, accountname text) 
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
	    accountname text
    );
	
	IF recordtype=1
	THEN
	INSERT INTO temp_journalreport
	SELECT vd."VoucherDate", vd."VoucherNo", vd."ReferenceNo", vt."Description",
	vd."CurrencyId", vt."ChartOfAccountNewId", 
	 ROUND(vt."Credit" :: numeric,4), ROUND(vt."Debit" :: numeric,4), coa."ChartOfAccountNewCode", vd."JournalCode", coa."AccountName"
	FROM "VoucherDetail" vd
	LEFT JOIN "VoucherTransactions" vt ON vd."VoucherNo"= vt."VoucherNo"
	LEFT JOIN "ChartOfAccountNew" coa ON vt."ChartOfAccountNewId" = coa."ChartOfAccountNewId"
	WHERE vd."JournalCode" IN (select * from unnest(_journalcode)) AND vd."OfficeId" IN (select * from unnest(_officelist))  AND 
	vt."ChartOfAccountNewId" IN (select * from unnest(_accountslist)) AND vd."IsDeleted"= false AND
    vt."IsDeleted"= false AND coa."IsDeleted"= false AND
	vd."CurrencyId"= _currencyid AND vd."VoucherDate" :: Date >= _fromdate :: Date AND 
	vd."VoucherDate" :: Date <= _todate :: Date ORDER BY vt."TransactionDate" ASC;
	ELSE
	INSERT INTO temp_journalreport
	SELECT tt."VoucherDate", tt."VoucherNo", tt."ReferenceNo", tt."Description", 
	tt."CurrencyId", tt."ChartOfAccountNewId", ROUND(tt."Credit" :: numeric,4), ROUND(tt."Debit" :: numeric,4), tt."ChartOfAccountNewCode", 
	tt."JournalCode", tt."AccountName"
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
                        limit 1) as "Debit",coa."ChartOfAccountNewCode", vd."JournalCode", coa."AccountName"
	FROM "VoucherDetail" vd
	LEFT JOIN "VoucherTransactions" vt ON vd."VoucherNo"= vt."VoucherNo"
	LEFT JOIN "ChartOfAccountNew" coa ON vt."ChartOfAccountNewId"=coa."ChartOfAccountNewId"
	WHERE vd."JournalCode" IN (select * from unnest(_journalcode)) AND vd."OfficeId" IN (select * from unnest(_officelist)) AND 
	vt."ChartOfAccountNewId" IN (select * from unnest(_accountslist))  AND vd."IsDeleted"= false 
		AND vt."IsDeleted"= false AND coa."IsDeleted"= false 
	AND vd."VoucherDate" :: Date >= _fromdate :: Date AND 
	vd."VoucherDate" :: Date <= _todate :: Date ORDER BY vt."TransactionDate" ASC) as tt;
	
	END IF;

	RETURN QUERY SELECT * from temp_journalreport;

 DROP TABLE temp_journalreport;
 
END;

$BODY$;
CREATE OR REPLACE FUNCTION public.get_ledger_report(
	currency integer,
	fromdate text,
	todate text,
	recordtype integer,
	officelist integer[],
	accountslist bigint[],
	openingbalance boolean)
    RETURNS TABLE(chartofaccountnewid bigint, accountname text, voucherno bigint, voucherdate timestamp without time zone, isvoucherverified boolean, description text, voucherreferenceno text, currencyname text, debitamount double precision, creditamount double precision, transactiondate date, currencyid integer, chartofaccountnewcode text) 
    LANGUAGE 'plpgsql'

    COST 100
    VOLATILE 
    ROWS 1000
AS $BODY$

-- Declaration
DECLARE 
_recordtype integer=recordtype; _currencyid integer = currency;_officelist         integer[] = officelist;_accountslist       bigint[] = accountslist;_fromdate text = fromdate;_todate text = todate;_openingbalance boolean = openingbalance;BEGIN
  CREATE temp TABLE temp_ledger_report( chartofaccountnewid BIGINT, accountname TEXT, voucherno BIGINT,voucherdate timestamp without time zone, isvoucherverified boolean,description TEXT, voucherreferenceno TEXT, currencyname TEXT, debitamount double precision, creditamount double precision, transactiondate DATE, currencyid INTEGER, chartofaccountnewcode TEXT );
  IF _recordtype=1 THEN 
  INSERT INTO temp_ledger_report 
  SELECT    vt."ChartOfAccountNewId", 
            coa."AccountName", 
            vd."VoucherNo",
			vd."VoucherDate",
			vd."IsVoucherVerified",
            vt."Description", 
            vd."ReferenceNo", 
            cd."CurrencyName", 
            round( vt."Debit" :: numeric, 4), 
            round( vt."Credit" :: numeric, 4), 
            vd."VoucherDate", 
            cd."CurrencyId", 
            coa."ChartOfAccountNewCode" 
  FROM      "VoucherDetail" vd 
  LEFT JOIN "VoucherTransactions" vt 
  ON        vd."VoucherNo"= vt."VoucherNo" 
  LEFT JOIN "ChartOfAccountNew" coa 
  ON        vt."ChartOfAccountNewId" = coa."ChartOfAccountNewId" 
  LEFT JOIN "CurrencyDetails" cd 
  ON        cd."CurrencyId"= vd."CurrencyId" 
  WHERE   
  
  CASE _openingbalance 
                                       WHEN true THEN vd."VoucherDate" :: date < _fromdate :: date
                                       ELSE vd."VoucherDate" :: date >= _fromdate :: date
                                       AND  vd."VoucherDate" :: date <= _todate :: date
                             END 
  
  AND vd."OfficeId" IN 
            ( 
                   SELECT * 
                   FROM   Unnest(_officelist)) 
  AND       vt."ChartOfAccountNewId" IN 
            ( 
                   SELECT * 
                   FROM   Unnest(_accountslist)) 
  AND       vd."IsDeleted"= false 
  AND		vt."IsDeleted"= false
  AND 		coa."IsDeleted"= false
  AND 		cd."IsDeleted"= false
  AND       vd."CurrencyId"= _currencyid 
  ORDER BY  vt."TransactionDate" ASC; 
   
  ELSE 
  INSERT INTO temp_ledger_report 
  SELECT tt."ChartOfAccountNewId", 
         tt."AccountName", 
         tt."VoucherNo",
		 tt."VoucherDate",
		 tt."IsVoucherVerified",
         tt."Description", 
         tt."ReferenceNo", 
         tt."CurrencyName", 
         round( tt."Debit" :: numeric, 4), 
         round( tt."Credit" :: numeric, 4), 
         tt."VoucherDate", 
         tt."CurrencyId", 
         tt."ChartOfAccountNewCode" 
  FROM   ( 
                   SELECT    vt."ChartOfAccountNewId", 
                             coa."AccountName", 
                             vd."VoucherNo",
	  						 vd."IsVoucherVerified",
                             vt."Description", 
                             vd."ReferenceNo", 
                             cd."CurrencyName", 
                             vt."Debit" * 
                             ( 
                                      SELECT   "Rate" 
                                      FROM     "ExchangeRateDetail" 
                                      WHERE    "IsDeleted" = false 
                                      AND      "FromCurrency" = vd."CurrencyId" 
                                      AND      "ToCurrency"= _currencyid 
                                      AND      "Date":: date <= vd."VoucherDate" :: date 
                                      ORDER BY "Date" DESC limit 1) AS "Debit", 
                             vt."Credit" * 
                             ( 
                                      SELECT   "Rate" 
                                      FROM     "ExchangeRateDetail" 
                                      WHERE    "IsDeleted" = false 
                                      AND      "FromCurrency" = vd."CurrencyId" 
                                      AND      "ToCurrency"= _currencyid 
                                      AND      "Date":: date <= vd."VoucherDate" :: date 
                                      ORDER BY "Date" DESC limit 1) AS "Credit", 
                             vd."VoucherDate", 
                             cd."CurrencyId", 
                             coa."ChartOfAccountNewCode" 
                   FROM      "VoucherDetail" vd 
                   LEFT JOIN "VoucherTransactions" vt 
                   ON        vd."VoucherNo"= vt."VoucherNo" 
                   LEFT JOIN "ChartOfAccountNew" coa 
                   ON        vt."ChartOfAccountNewId"=coa."ChartOfAccountNewId" 
                   LEFT JOIN "CurrencyDetails" cd 
                   ON        vd."CurrencyId"= cd."CurrencyId" 
                   WHERE 
                             CASE _openingbalance 
                                       WHEN true THEN vd."VoucherDate" :: date < _fromdate :: date
                                       ELSE vd."VoucherDate" ::           date >= _fromdate :: date
                                       AND       vd."VoucherDate" ::      date <= _todate :: date
                             END 
                   AND       vd."OfficeId" IN 
                             ( 
                                    SELECT * 
                                    FROM   Unnest(_officelist)) 
                   AND       vt."ChartOfAccountNewId" IN 
                             ( 
                                    SELECT * 
                                    FROM   Unnest(_accountslist)) 
                   AND       vd."IsDeleted"= false 
                   AND       vt."IsDeleted"= false
				   AND		 coa."IsDeleted"= false
				   AND       cd."IsDeleted"= false
                   ORDER BY  vt."TransactionDate" ASC) AS tt; 

END IF;
RETURN query SELECT * 
FROM   temp_ledger_report;DROP TABLE temp_ledger_report;END;

$BODY$;
CREATE
OR REPLACE FUNCTION public.get_transaction_list(budgetline_id bigint, currency_id integer) returns TABLE (
    currencyid integer,
    currencyname character varying(50),
    credit DOUBLE PRECISION,
    debit DOUBLE PRECISION,
    transactiondate timestamp without time zone,
    createddate timestamp without time zone
) LANGUAGE 'plpgsql' COST 100 VOLATILE ROWS 1000 AS $Body$ DECLARE credit_amt DOUBLE PRECISION;

debit_amt DOUBLE PRECISION;

Begin return Query
select
    pbl."CurrencyId",
    cd."CurrencyName",
    get_exchangerate_value(
        vt."Credit",
        vt."CurrencyId",
        currency_id,
        vt."TransactionDate"
    ) as credit_amt,
    get_exchangerate_value(
        vt."Debit",
        vt."CurrencyId",
        currency_id,
        vt."TransactionDate"
    ) as debit_amt,
    vt."TransactionDate",
    vt."CreatedDate"
from
    "ProjectBudgetLineDetail" as pbl
    inner join "VoucherTransactions" as vt on pbl."BudgetLineId" = vt."BudgetLineId"
    inner join "CurrencyDetails" as cd on pbl."CurrencyId" = cd."CurrencyId"
where
    pbl."BudgetLineId" = budgetline_id
    and pbl."IsDeleted" = false
order by
    pbl."BudgetLineId";

END;

$Body$;
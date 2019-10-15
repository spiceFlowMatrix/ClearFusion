CREATE
OR REPLACE FUNCTION public.get_vouchersummaryreportvouchersbyfilter(
    accounts bigint [],
    budgetlines bigint [],
    currencyid integer,
    journals bigint [],
    offices integer [],
    projectjobs bigint [],
    projects bigint [],
    recordtype integer
) RETURNS TABLE(
    voucherno bigint,
    vouchercode character varying,
    voucherdescription text,
    voucherdate text
) LANGUAGE 'plpgsql' COST 100 VOLATILE ROWS 1000 AS $Body$ BEGIN RETURN QUERY
SELECT
    DISTINCT vd."VoucherNo",
    vd."ReferenceNo" AS vouchercode,
    vd."Description" AS voucherdescription,
    Cast(vd."VoucherDate" AS TEXT) as voucherdate
FROM
    "VoucherDetail" AS vd
    JOIN "VoucherTransactions" AS vt ON vd."VoucherNo" = vt."VoucherNo"
WHERE
    vd."IsDeleted" = false
    AND vt."IsDeleted" = false
    AND vt."ChartOfAccountNewId" = ANY(accounts)
    AND (
        recordtype = 2
        OR vd."CurrencyId" = currencyid
    )
    AND vd."JournalCode" = ANY(journals)
    AND vd."OfficeId" = ANY(offices)
    OR vt."ProjectId" = ANY(projects)
    OR vt."JobId" = ANY(projectjobs)
    OR vt."BudgetLineId" = ANY(budgetlines)
ORDER BY
    voucherdate DESC;

END;

$Body$;
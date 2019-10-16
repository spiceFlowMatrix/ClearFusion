CREATE
OR REPLACE FUNCTION public.get_all_voucher_transaction_list(voucherid bigint) RETURNS TABLE(
    voucherno bigint,
    referenceno character varying,
    chequeno character varying,
    voucherdate timestamp without time zone,
    journalname character varying,
    officename character varying,
    currencycode character varying,
    voucherdescription text,
    transactiondescription text,
    credit double precision,
    debit double precision,
    chartofaccountnewcode text,
    projectcode text,
    budgetcode text,
    projectjobcode text,
    sectorcode text
) LANGUAGE 'plpgsql' COST 100 VOLATILE ROWS 1000 AS $Body$ DECLARE Credit DOUBLE PRECISION;

Debit DOUBLE PRECISION;

TransactionDescription text;

Begin RETURN QUERY
SELECT
    vd."VoucherNo",
    vd."ReferenceNo",
    vd."ChequeNo",
    vd."VoucherDate",
    jd."JournalName",
    od."OfficeName",
    cd."CurrencyCode",
    vd."Description",
    vt."Description" as TransactionDescription,
    vt."Credit" as Credit,
    vt."Debit" as Debit,
    ca."ChartOfAccountNewCode",
    pd."ProjectCode",
    pbld."BudgetCode",
    pjd."ProjectJobCode",
    sd."SectorCode"
from
    public."VoucherTransactions" as vt
    inner join public."ChartOfAccountNew" as ca on ca."ChartOfAccountNewId" = vt."ChartOfAccountNewId"
    inner join public."VoucherDetail" as vd on vd."VoucherNo" = vt."VoucherNo"
    inner join public."JournalDetail" as jd on jd."JournalCode" = vd."JournalCode"
    inner join public."OfficeDetail" as od on od."OfficeId" = vd."OfficeId"
    inner join public."CurrencyDetails" as cd on cd."CurrencyId" = vd."CurrencyId"
    left join "ProjectSector" as ps on vt."ProjectId" = ps."ProjectId"
    left join "SectorDetails" as sd on ps."SectorId" = sd."SectorId"
    left join public."ProjectDetail" as pd on pd."ProjectId" = vt."ProjectId"
    left join public."ProjectBudgetLineDetail" as pbld on pbld."BudgetLineId" = vt."BudgetLineId"
    left join public."ProjectJobDetail" as pjd on pjd."ProjectJobId" = vt."JobId"
where
    vt."IsDeleted" = false
    AND vt."VoucherNo" = voucherid;

END;

$Body$;
CREATE
OR REPLACE FUNCTION public.get_projectcashflow(
    currency integer,
    projectid bigint [],
    startdate text,
    enddate text,
    donorid bigint
) RETURNS TABLE(
    voucherdate date,
    expenditure double precision,
    income double precision,
    currencyid integer,
    budgetlinedate date
) LANGUAGE 'plpgsql' COST 100 VOLATILE ROWS 1000 AS $Body$ -- Declaration
DECLARE _currencyid INTEGER = currency;

_projectid BIGINT [] = projectid;

_startdate TEXT = startdate;

_enddate TEXT = enddate;

_donorid BIGINT = donorid;

BEGIN CREATE TEMP TABLE temp_projectcashflow(
    voucherdate DATE,
    expenditure DOUBLE PRECISION,
    income DOUBLE PRECISION,
    currencyid INTEGER,
    budgetlinedate DATE
);

INSERT INTO
    temp_projectcashflow
SELECT
    tt.VoucherDate,
    tt."Debit",
    tt."Income",
    tt._currencyid,
    tt.budgetlinedate
FROM
    (
        SELECT
            date(vd."VoucherDate") as VoucherDate,
            Sum(
                vt."Debit" * (
                    SELECT
                        "Rate"
                    FROM
                        "ExchangeRateDetail"
                    WHERE
                        "IsDeleted" = false
                        AND "FromCurrency" = vd."CurrencyId"
                        AND "ToCurrency" = _currencyid
                        AND "Date" :: Date <= vd."VoucherDate" :: Date
                    ORDER BY
                        "Date" DESC
                    LIMIT
                        1
                )
            ) as "Debit",
            SUM(
                (
                    SELECT
                        SUM(
                            "InitialBudget" * (
                                SELECT
                                    "Rate"
                                FROM
                                    "ExchangeRateDetail"
                                WHERE
                                    "IsDeleted" = false
                                    AND "FromCurrency" = bl."CurrencyId"
                                    AND "ToCurrency" = _currencyid
                                    AND "Date" :: Date <= vd."VoucherDate" :: Date
                                ORDER BY
                                    "Date" DESC
                                LIMIT
                                    1
                            )
                        )
                    FROM
                        "ProjectBudgetLineDetail"
                    WHERE
                        "IsDeleted" = false
                        AND "CreatedDate" :: Date >= _startdate :: Date
                        AND "CreatedDate" :: Date <= _enddate :: Date
                        AND "ProjectId" = vt."ProjectId"
                    GROUP BY
                        "ProjectId"
                )
            ) as "Income",
            _currencyid,
            date(bl."CreatedDate") AS budgetlinedate
        FROM
            "VoucherDetail" AS vd
            JOIN "VoucherTransactions" AS vt ON vt."VoucherNo" = vd."VoucherNo"
            JOIN "ProjectBudgetLineDetail" AS bl ON vt."BudgetLineId" = bl."BudgetLineId"
        WHERE
            vd."IsDeleted" = false
            AND bl."IsDeleted" = false
            AND vt."BudgetLineId" IN (
                SELECT
                    "BudgetLineId"
                FROM
                    "ProjectBudgetLineDetail"
                WHERE
                    "IsDeleted" = false
                    AND "CreatedDate" :: Date >= CAST(_startdate as Date) :: Date
                    AND "CreatedDate" :: Date <= CAST(_enddate as Date) :: Date
                    AND "ProjectId" IN(
                        (
                            select
                                *
                            from
                                unnest(_projectid)
                        )
                    )
            )
            AND vd."VoucherDate" :: Date >= _startdate :: Date
            AND vd."VoucherDate" :: Date <= _enddate :: Date
        GROUP BY
            date(vd."VoucherDate"),
            date(bl."CreatedDate")
    ) as tt;

RETURN QUERY
SELECT
    *
from
    temp_projectcashflow
ORDER BY
    voucherdate;

DROP TABLE temp_projectcashflow;

END;

$Body$;
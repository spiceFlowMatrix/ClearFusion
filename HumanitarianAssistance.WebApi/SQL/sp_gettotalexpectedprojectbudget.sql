CREATE
OR REPLACE FUNCTION public.get_totalexpectedprojectbudget(
    projectid bigint [],
    currencyid integer,
    comparisiondate text
) RETURNS TABLE(totalexpectedprojectbudget double precision) LANGUAGE 'plpgsql' COST 100 VOLATILE ROWS 1000 AS $Body$ -- Declaration
DECLARE _projectid BIGINT [] = projectid;

_currencyid INT = currencyid;

_totalexpectedbudget DOUBLE PRECISION = 0;

_comparisiondate TEXT = comparisiondate;

BEGIN CREATE TEMP TABLE temp_totalexpectedprojectbudget(
    totalexpectedprojectbudget DOUBLE PRECISION
);

Insert Into
    temp_totalexpectedprojectbudget
Select
    tt.Sum
From
    (
        SELECT
            SUM(
                "ProposalBudget" * (
                    SELECT
                        "Rate"
                    FROM
                        "ExchangeRateDetail"
                    WHERE
                        "IsDeleted" = false
                        AND "FromCurrency" = "CurrencyId"
                        AND "ToCurrency" = _currencyid
                        AND "Date" :: Date <= _comparisiondate :: Date
                    ORDER BY
                        "Date" DESC
                    LIMIT
                        1
                )
            )
        FROM
            "ProjectProposalDetail"
        WHERE
            "ProjectId" IN (
                select
                    *
                from
                    unnest(_projectid)
            )
    ) as tt;

RETURN QUERY
SELECT
    *
from
    temp_totalexpectedprojectbudget;

DROP TABLE temp_totalexpectedprojectbudget;

END;

$Body$;
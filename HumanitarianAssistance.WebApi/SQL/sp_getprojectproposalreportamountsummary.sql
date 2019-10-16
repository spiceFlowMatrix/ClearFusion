CREATE
OR REPLACE FUNCTION public.get_projectproposalreportamountsummary(
    projectname character varying DEFAULT '' :: character varying,
    startdate text DEFAULT '' :: text,
    enddate text DEFAULT '' :: text,
    startdatefilteroption integer DEFAULT 0,
    duedatefilteroption integer DEFAULT 0,
    currencyid integer DEFAULT 0,
    amount double precision DEFAULT 0.0,
    amountfilteroption integer DEFAULT 0,
    iscompleted boolean DEFAULT false,
    islate boolean DEFAULT false
) RETURNS TABLE(
    projectamount double precision,
    projectcurrency integer
) LANGUAGE 'plpgsql' COST 100 VOLATILE ROWS 1000 AS $Body$ BEGIN CREATE TEMP TABLE temp_projectproposalreportamountsummary(
    projectamount DOUBLE PRECISION,
    projectcurrency INTEGER
);

INSERT INTO
    temp_projectproposalreportamountsummary
SELECT
    tt.projectamount,
    tt.currency
FROM
    (
        SELECT
            (
                select
                    "CurrencyId"
                from
                    "CurrencyDetails"
                limit
                    1
            ) AS currency,
            COALESCE(
                ppd."ProposalBudget" * (
                    SELECT
                        "Rate"
                    FROM
                        "ExchangeRateDetail"
                    WHERE
                        "IsDeleted" = false
                        AND "FromCurrency" = ppd."CurrencyId"
                        AND "ToCurrency" = (
                            select
                                "CurrencyId"
                            from
                                "CurrencyDetails"
                            limit
                                1
                        )
                        AND "Date" :: Date <= now() :: Date
                    ORDER BY
                        "Date" DESC
                    LIMIT
                        1
                ), 0
            ) as projectamount
        FROM
            "ProjectDetail" AS pd
            LEFT JOIN "ProjectProposalDetail" AS ppd ON pd."ProjectId" = ppd."ProjectId"
            LEFT JOIN "CurrencyDetails" AS cd ON ppd."CurrencyId" = (
                select
                    "CurrencyId"
                from
                    "CurrencyDetails"
                limit
                    1
            )
            LEFT JOIN "ApproveProjectDetails" AS apd ON apd."ProjectId" = pd."ProjectId"
        WHERE
            (
                projectname = ''
                OR pd."ProjectName" = projectname
            )
            AND (
                startdate = ''
                OR CASE
                    WHEN startdatefilteroption = 1 THEN ppd."ProposalStartDate" :: Date = CAST(startdate AS Date) :: Date
                    WHEN startdatefilteroption = 2 THEN ppd."ProposalStartDate" :: Date > CAST(startdate AS Date) :: Date
                    WHEN startdatefilteroption = 3 THEN ppd."ProposalStartDate" :: Date < CAST(startdate AS Date) :: Date
                    WHEN startdatefilteroption = 4 THEN ppd."ProposalStartDate" :: Date != CAST(startdate AS Date) :: Date
                    ELSE ppd."ProposalStartDate" :: Date >= CAST(startdate AS Date) :: Date
                END
            )
            AND (
                enddate = ''
                OR CASE
                    WHEN duedatefilteroption = 1 THEN ppd."ProposalDueDate" :: Date = CAST(enddate AS DATE) :: DATE
                    WHEN duedatefilteroption = 2 THEN ppd."ProposalDueDate" :: Date > CAST(enddate AS DATE) :: DATE
                    WHEN duedatefilteroption = 3 THEN ppd."ProposalDueDate" :: Date < CAST(enddate AS DATE) :: DATE
                    WHEN duedatefilteroption = 4 THEN ppd."ProposalDueDate" :: Date != CAST(enddate AS DATE) :: DATE
                    ELSE ppd."ProposalDueDate" :: Date >= CAST(enddate AS DATE) :: DATE
                END
            )
            AND (
                amount = 0.0
                OR CASE
                    WHEN amountfilteroption = 1 THEN ppd."ProposalBudget" = amount
                    WHEN amountfilteroption = 2 THEN ppd."ProposalBudget" > amount
                    WHEN amountfilteroption = 3 THEN ppd."ProposalBudget" < amount
                    WHEN amountfilteroption = 4 THEN ppd."ProposalBudget" != amount
                    ELSE ppd."ProposalBudget" >= amount
                END
            )
            AND (
                iscompleted = false
                OR apd."IsApproved" = iscompleted
            )
            AND (
                islate = false
                OR now() :: Date > ppd."ProposalDueDate" :: Date
            )
            AND pd."IsDeleted" = false
        ORDER BY
            pd."CreatedDate" DESC
    ) as tt;

RETURN QUERY
SELECT
    *
from
    temp_projectproposalreportamountsummary;

DROP TABLE temp_projectproposalreportamountsummary;

END;

$Body$;
CREATE
OR REPLACE FUNCTION public.get_project_activityactualstartdate_min_byparentid(parentid bigint) RETURNS timestamp without time zone LANGUAGE 'sql' COST 100 VOLATILE AS $Body$
SELECT
    MIN(pa."ActualStartDate")
FROM
    "ProjectActivityDetail" AS pa
    INNER JOIN "ProjectBudgetLineDetail" AS pbl ON pa."BudgetLineId" = pbl."BudgetLineId"
WHERE
    pa."ParentId" = parentid;

$Body$;
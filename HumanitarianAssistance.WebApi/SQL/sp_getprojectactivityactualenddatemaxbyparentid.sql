CREATE
OR REPLACE FUNCTION public.get_project_activityactualenddate_max_byparentid(parentid bigint) RETURNS timestamp without time zone LANGUAGE 'sql' COST 100 VOLATILE AS $Body$
SELECT
    MAX(pa."ActualStartDate")
FROM
    "ProjectActivityDetail" AS pa
WHERE
    pa."ParentId" = parentid;

$Body$;
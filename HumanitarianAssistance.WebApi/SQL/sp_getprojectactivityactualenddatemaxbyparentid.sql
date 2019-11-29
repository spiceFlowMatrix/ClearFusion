CREATE
OR REPLACE FUNCTION public.get_project_activityactualenddate_max_byparentid(parentid bigint) RETURNS timestamp without time zone LANGUAGE 'sql' COST 100 VOLATILE AS $Body$
SELECT
    MAX(pa."ActualEndDate")
FROM
    "ProjectActivityDetail" AS pa
WHERE
     CASE WHEN pa."ParentId" is not null
        THEN 
        pa."ParentId" = parentid 
        ELSE
		pa."ActivityId" = parentid
		END;

$Body$;
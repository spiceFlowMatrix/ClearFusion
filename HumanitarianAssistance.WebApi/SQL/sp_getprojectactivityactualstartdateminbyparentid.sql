CREATE
OR REPLACE FUNCTION public.get_project_activityactualstartdate_min_byparentid(parentid bigint) RETURNS timestamp without time zone LANGUAGE 'sql' COST 100 VOLATILE AS $Body$
SELECT
    MIN(pa."ActualStartDate")
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
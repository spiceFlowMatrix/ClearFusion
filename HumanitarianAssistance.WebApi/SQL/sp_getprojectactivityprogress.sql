CREATE
OR REPLACE FUNCTION public.get_projectactivityprogress(
    activityid bigint,
    OUT progress double precision
) RETURNS double precision LANGUAGE 'plpgsql' COST 100 VOLATILE AS $Body$ BEGIN
SELECT
    INTO progress AVG(pgrs)
FROM
    (
        select
            (
                COALESCE((pa."Achieved" / NULLIF(pa."Target", 0)), 0) * 100
            ) as pgrs
        from
            "ProjectActivityDetail" pa
        where
            pa."IsDeleted" = false
            and pa."ParentId" = activityid
    ) MyTable;

END;

$Body$;
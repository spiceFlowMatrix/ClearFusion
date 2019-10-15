CREATE
OR REPLACE FUNCTION public.get_Activity_list_Report(activity_id bigint [], project_id bigint) returns TABLE (activityname character varying(200)) LANGUAGE 'plpgsql' COST 100 VOLATILE ROWS 1000 AS $Body$ declare ActivityIds bigint [];

Begin ActivityIds = activity_id;

return Query
select
    row_number() OVER () as SerialNumber,
    pd."ProjectCode",
    pd."ProjectName",
    pod."projectGoal",
    pod."StartDate",
    pod."EndDate",
    (
        select
            cd."CountryName"
        from
            "CountryMultiSelectDetails" as cmsd
            join "CountryDetails" as cd on cmsd."CountryId" = cd."CountryId"
        where
            cmsd."ProjectId" = pacd."ProjectId"
    ) as CountryName,
    Array(
        select
            pd."ProvinceName"
        from
            "ProvinceMultiSelect" as pms
            join "ProvinceDetails" as pd on pms."ProvinceId" = pd."ProvinceId"
        where
            pms."ProjectId" = pacd."ProjectId"
    ) as ProvinceName,
    Array(
        select
            dd."District"
        from
            "DistrictMultiSelect" as dms
            join "DistrictDetail" as dd on dms."DistrictID" = dd."DistrictID"
        where
            dms."ProjectId" = pacd."ProjectId"
    ) as DistrictName,
    pacd."ActivityName",
    pacd."ActualStartDate",
    pacd."ActualEndDate",
    pi."IndicatorName",
    Array(
        select
            "IndicatorQuestion"
        from
            "ProjectMonitoringIndicatorQuestions" as pmiq
            join "ProjectIndicatorQuestions" as piq on pmiq."IndicatorQuestionId" = piq."IndicatorQuestionId"
        where
            pmiq."MonitoringIndicatorId" = pmid."MonitoringIndicatorId"
    ) as ActivityQuestions,
    (
        select
            Sum("Score") / count("Id")
        from
            "ProjectMonitoringIndicatorQuestions"
        where
            "MonitoringIndicatorId" = pmid."MonitoringIndicatorId"
    ) as RatingPerQuestions,
    prd."PostivePoints",
    prd."NegativePoints",
    prd."Recommendations"
from
    "ProjectActivityDetail" as pacd
    join "ProjectDetail" as pd on pacd."ProjectId" = pd."ProjectId"
    left join "ProjectOtherDetail" as pod on pd."ProjectId" = pod."ProjectId"
    left join "ProjectIndicators" as pi on pacd."ProjectId" = pi."ProjectId"
    join "ProjectMonitoringIndicatorDetail" as pmid on pi."ProjectIndicatorId" = pmid."ProjectIndicatorId"
    join "ProjectMonitoringReviewDetail" as prd on pmid."ProjectMonitoringReviewId" = prd."ProjectMonitoringReviewId"
where
    pacd."ActivityId" = any(activity_id)
    and pacd."ParentId" is null
    and pacd."ProjectId" = project_id
    and pacd."IsDeleted" = false;

End;

$Body$;


--DROP FUNCTION get_activity_list_report_pdf(activity_id bigint[],project_id bigint)
CREATE
OR REPLACE FUNCTION public.get_activity_list_report_pdf(activity_id bigint [], project_id bigint) returns TABLE (
    serialnumber bigint,
    projectcode text,
    projectname text,
    projectgoal text,
    startdate timestamp without time zone,
    enddate timestamp without time zone,
    countryname character varying(50),
    provincename character varying [],
    districtname character varying [],
    activityname character varying(200),
    actualstartdate timestamp without time zone,
    actualenddate timestamp without time zone,
    indicatorname text,
    activityquestions text [],
    ratingperquestion bigint,
    postivepoint text,
    negativepoint text,
    recommendations text
) LANGUAGE 'plpgsql' COST 100 VOLATILE ROWS 1000 AS $BODY$ declare ActivityIds bigint [];

Begin ActivityIds = activity_id;

return Query
select
    row_number() OVER () as "SerialNumber",
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
    ) as "CountryName",
    Array(
        select
            pd."ProvinceName"
        from
            "ProvinceMultiSelect" as pms
            join "ProvinceDetails" as pd on pms."ProvinceId" = pd."ProvinceId"
        where
            pms."ProjectId" = pacd."ProjectId"
    ) as "ProvinceName",
    Array(
        select
            dd."District"
        from
            "DistrictMultiSelect" as dms
            join "DistrictDetail" as dd on dms."DistrictID" = dd."DistrictID"
        where
            dms."ProjectId" = pacd."ProjectId"
    ) as "DistrictName",
    pacd."ActivityName",
    pacd."ActualStartDate",
    pacd."ActualEndDate",
    (
        select
            "IndicatorName"
        from
            "ProjectIndicators"
        where
            "ProjectIndicatorId" = pmid."ProjectIndicatorId"
    ) as "IndicatorName",
    Array(
        select
            "IndicatorQuestion"
        from
            "ProjectMonitoringIndicatorQuestions" as pmiq
            left join "ProjectIndicatorQuestions" as piq on pmiq."IndicatorQuestionId" = piq."IndicatorQuestionId"
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
    left join "ProjectDetail" as pd on pacd."ProjectId" = pd."ProjectId"
    left join "ProjectOtherDetail" as pod on pd."ProjectId" = pod."ProjectId"
    left join "ProjectMonitoringReviewDetail" as prd on pacd."ActivityId" = prd."ActivityId"
    left join "ProjectMonitoringIndicatorDetail" as pmid on prd."ProjectMonitoringReviewId" = pmid."ProjectMonitoringReviewId"
where
    pacd."ActivityId" = any(ActivityIds)
    and pacd."ParentId" is null
    and pacd."ProjectId" = project_id
    and pacd."IsDeleted" = false;

End;

$BODY$;
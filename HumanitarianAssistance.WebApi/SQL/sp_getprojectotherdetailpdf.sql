CREATE
OR REPLACE FUNCTION public.get_project_other_detail_pdf(projectid integer) RETURNS TABLE(
    opportunitytype integer,
    opportunity text,
    opportunitydescription text,
    opportunityno text,
    donor text,
    countryname character varying,
    province character varying [],
    district character varying [],
    officename character varying,
    sectorname text,
    programname text,
    startdate timestamp without time zone,
    enddate timestamp without time zone,
    projectgoal text,
    projectobjective text,
    reoireceivedate timestamp without time zone,
    submissiondate timestamp without time zone,
    mainactivities text,
    beneficiarymale integer,
    beneficiaryfemale integer,
    indirectbeneficiarymale integer,
    indirectbeneficiaryfemale integer,
    strengthconsiderationname text,
    genderconsiderationname text,
    genderremarks text,
    securityname text,
    securityconsideration text [],
    securityremarks text,
    projectdescription text,
    projectcode text,
    projectname text
) LANGUAGE 'plpgsql' COST 100 VOLATILE ROWS 1000 AS $Body$ DECLARE Donor text;

Province character varying [];

District character varying [];

SecurityConsideration text [];

Begin RETURN QUERY
SELECT
    pod."OpportunityType",
    pod."opportunity",
    pod."opportunitydescription",
    pod."opportunityNo",
    dd."Name" as Donor,
    cd."CountryName",
    ARRAY(
        Select
            pd."ProvinceName" :: character varying
        from
            public."ProvinceMultiSelect" as pms
            left join public."ProvinceDetails" as pd on pd."ProvinceId" = pms."ProvinceId"
        Where
            pms."ProjectId" = projectid
    ) as Province,
    ARRAY(
        Select
            dd."District" :: character varying
        from
            public."DistrictMultiSelect" as dms
            left join public."DistrictDetail" as dd on dd."DistrictID" = dms."DistrictID"
        Where
            dms."ProjectId" = projectid
    ) as District,
    od."OfficeName",
    sd."SectorName",
    prgdet."ProgramName",
    pod."StartDate",
    pod."EndDate",
    pod."projectGoal",
    pod."projectObjective",
    pod."REOIReceiveDate",
    pod."SubmissionDate",
    pod."mainActivities",
    pod."beneficiaryMale",
    pod."beneficiaryFemale",
    pod."InDirectBeneficiaryMale",
    pod."InDirectBeneficiaryFemale",
    scd."StrengthConsiderationName",
    gcd."GenderConsiderationName",
    pod."GenderRemarks",
    secdet."SecurityName",
    ARRAY(
        Select
            scd."SecurityConsiderationName" :: text
        from
            public."SecurityConsiderationMultiSelect" as scms
            left join public."SecurityConsiderationDetail" as scd on scd."SecurityConsiderationId" = scms."SecurityConsiderationId"
        Where
            scms."ProjectId" = projectid
    ) as SecurityConsideration,
    pod."SecurityRemarks",
    projdet."ProjectDescription",
    projdet."ProjectCode",
    projdet."ProjectName"
from
    public."ProjectOtherDetail" as pod
    inner join public."ProjectDetail" as projdet on projdet."ProjectId" = pod."ProjectId"
    left join public."DonorDetail" as dd on dd."DonorId" = pod."DonorId"
    left join public."CountryMultiSelectDetails" as cmsd on pod."ProjectId" = cmsd."ProjectId"
    left join public."CountryDetails" as cd on cd."CountryId" = cmsd."CountryId"
    left join public."OfficeDetail" as od on pod."OfficeId" = od."OfficeId"
    left join public."ProjectSector" as ps on pod."ProjectId" = ps."ProjectId"
    left join public."SectorDetails" as sd on ps."SectorId" = sd."SectorId"
    left join public."ProjectProgram" as pp on pod."ProjectId" = pp."ProjectId"
    left join public."ProgramDetail" as prgdet on pp."ProgramId" = prgdet."ProgramId"
    left join public."StrengthConsiderationDetail" as scd on scd."StrengthConsiderationId" = pod."StrengthConsiderationId"
    left join public."GenderConsiderationDetail" as gcd on gcd."GenderConsiderationId" = pod."GenderConsiderationId"
    left join public."SecurityDetail" as secdet on secdet."SecurityId" = pod."SecurityId"
WHERE
    pod."ProjectId" = projectid
    AND pod."IsDeleted" = false;

END;

$Body$;
CREATE
OR REPLACE FUNCTION public.get_annual_appraisal_report(office_id integer) returns TABLE (
    serialnumber bigint,
    name text,
    fathername text,
    designation character varying(100),
    department text,
    weakpoint character varying [],
    strongpoint character varying [],
    appraisalstatus text,
    --requiredtraining text,
    employeecomments text,
    --supervisercomment text,
    committeememberone text,
    committeemembertwo text --finalreview text,
) LANGUAGE 'plpgsql' COST 100 VOLATILE ROWS 1000 AS $BODY$ Begin return Query
select
    row_number() OVER () as serialnumber,
    ed."EmployeeName",
    ed."FatherName",
    dd."Designation",
    ead."Department",
    ARRAY(
        Select
            "Point" :: character varying
        from
            "StrongandWeakPoints"
        Where
            "EmployeeAppraisalDetailsId" = ead."EmployeeAppraisalDetailsId"
            and "Status" = 2
    ) as weakpoint,
    ARRAY(
        Select
            "Point" :: character varying
        from
            "StrongandWeakPoints"
        Where
            "EmployeeAppraisalDetailsId" = ead."EmployeeAppraisalDetailsId"
            and "Status" = 1
    ) as strongpoint,
    ee."EvaluationStatus",
    ee."CommentsByEmployee",
    (
        select
            ed."EmployeeName"
        from
            "EmployeeDetail" as ed
            join "EmployeeAppraisalTeamMember" as eat on ed."EmployeeID" = eat."EmployeeId"
        where
            eat."IsDeleted" = false
            and eat."EmployeeAppraisalDetailsId" = ead."EmployeeAppraisalDetailsId"
        order by
            "EmployeeAppraisalTeamMemberId" asc
        limit
            1
    ) as committeememberone,
    (
        select
            ed."EmployeeName"
        from
            "EmployeeDetail" as ed
            join "EmployeeAppraisalTeamMember" as eat on ed."EmployeeID" = eat."EmployeeId"
        where
            eat."IsDeleted" = false
            and eat."EmployeeAppraisalDetailsId" = ead."EmployeeAppraisalDetailsId"
        order by
            "EmployeeAppraisalTeamMemberId" desc
        limit
            1
    ) as committeemembertwo
from
    "EmployeeAppraisalDetails" as ead
    left join "EmployeeDetail" as ed on ead."EmployeeId" = ed."EmployeeID"
    left join "EmployeeProfessionalDetail" as epd on ed."EmployeeID" = epd."EmployeeId"
    left join "DesignationDetail" as dd on epd."DesignationId" = dd."DesignationId"
    left join "EmployeeEvaluation" as ee on ead."EmployeeAppraisalDetailsId" = ee."EmployeeAppraisalDetailsId"
where
    ead."OfficeId" = office_id
    And ead."IsDeleted" = false;

End;
$BODY$;
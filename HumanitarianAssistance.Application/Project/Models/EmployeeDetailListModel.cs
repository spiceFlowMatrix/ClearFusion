﻿using System;

namespace HumanitarianAssistance.Application.Project.Models
{
    public class EmployeeDetailListModel
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeCode { get; set; }
        public string FathersName { get; set; }
        public string Position { get; set; }
        public string Department { get; set; }
        public string Qualification { get; set; }
        public int? DutyStation { get; set; }
        public DateTime? RecruitmentDate { get; set; }
        public DateTime? ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public string CodeEmployeeName { get; set; }
        public string TenureWithCHA { get; set; }
        public string Gender { get; set; }
        public int? OfficeId { get; set; }
        public int? CountryId { get; set; }
        public int? ProvinceId { get; set; }
        public int? DesignationId { get; set; }
        public long? ProjectId { get; set; }
        public long? BudgetLineId { get; set; }
    }
}

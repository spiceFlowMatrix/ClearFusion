using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Project.Models
{
    public class ProjectHiringRequestModel
    {
        public long? HiringRequestId { get; set; }
        public string HiringRequestCode { get; set; }
        public string Description { get; set; }
        public string ProfessionName { get; set; }
        public int? ProfessionId { get; set; }
        public string Position { get; set; }
        public int? TotalVacancies { get; set; }
        public int? FilledVacancies { get; set; }
        public double? BasicPay { get; set; }
        public long? BudgetLineId { get; set; }
        public int OfficeId { get; set; }
        public int? GradeId { get; set; }
        public int? EmployeeID { get; set; }
        public long? ProjectId { get; set; }
        public bool IsCompleted { get; set; }
        public int? CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public string EmployeeName { get; set; }
        public string GradeName { get; set; }
        public string OfficeName { get; set; }
        public string BudgetName { get; set; }

        public string RequestedBy { get; set; }

        public int? pageIndex { get; set; }
        public int? pageSize { get; set; }
        public int? totalCount { get; set; }


    }
}

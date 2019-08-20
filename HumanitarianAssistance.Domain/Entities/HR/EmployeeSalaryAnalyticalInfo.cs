using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using HumanitarianAssistance.Domain.Entities.Project;

namespace HumanitarianAssistance.Domain.Entities.HR
{
    public class EmployeeSalaryAnalyticalInfo : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int EmployeeSalaryAnalyticalInfoId { get; set; }
        public int? AccountCode { get; set; }
        public long ProjectId { get; set; }
        public ProjectDetail ProjectDetail { get; set; }
        public long BudgetlineId { get; set; }
        public ProjectBudgetLineDetail ProjectBudgetLine { get; set; }
        public double SalaryPercentage { get; set; }
        public int EmployeeID { get; set; }
        public EmployeeDetail EmployeeDetail { get; set; }
        public long? HiringRequestId { get; set; }
        public ProjectHiringRequestDetail ProjectHiringRequestDetail { get; set; }
    }
}

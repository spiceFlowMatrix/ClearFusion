using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using HumanitarianAssistance.Domain.Entities.Accounting;
using HumanitarianAssistance.Domain.Entities.Project;

namespace HumanitarianAssistance.Domain.Entities.HR
{
    public class EmployeeSalaryAnalyticalInfo : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int EmployeeSalaryAnalyticalInfoId { get; set; }
        public long? AccountNo { get; set; }
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public ProjectDetail ProjectDetail { get; set; }
        public long BudgetlineId { get; set; }
        [ForeignKey("BudgetlineId")]
        public ProjectBudgetLineDetail ProjectBudgetLine { get; set; }
        public double SalaryPercentage { get; set; }
        public int EmployeeID { get; set; }
        [ForeignKey("EmployeeID")]
        public EmployeeDetail EmployeeDetail { get; set; }
        public long? HiringRequestId { get; set; }
        [ForeignKey("HiringRequestId")]
        public ProjectHiringRequestDetail ProjectHiringRequestDetail { get; set; }
        [ForeignKey("AccountNo")]
        public ChartOfAccountNew ChartOfAccountNew { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using HumanitarianAssistance.Domain.Entities.HR;

namespace HumanitarianAssistance.Domain.Entities.Project
{
    public class ProjectHiringRequestDetail : BaseEntity
    {
        public ProjectHiringRequestDetail()
        {
            IsCompleted = false;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long HiringRequestId { get; set; }
        public string HiringRequestCode { get; set; }
        public string Description { get; set; }
        //public string Profession { get; set; }
        [ForeignKey("ProfessionId")]
        public int? ProfessionId { get; set; }
        public ProfessionDetails ProfessionDetails { get; set; }

        public string Position { get; set; }
        public int? TotalVacancies { get; set; }
        public int? FilledVacancies { get; set; }
        public double? BasicPay { get; set; }
        public int? CurrencyId { get; set; }
        [ForeignKey("CurrencyId")]
        public CurrencyDetails CurrencyDetails { get; set; }

        public long? BudgetLineId { get; set; }
        [ForeignKey("BudgetLineId")]
        public ProjectBudgetLineDetail ProjectBudgetLineDetail { get; set; }

        [ForeignKey("OfficeId")]
        public int? OfficeId { get; set; }
        public OfficeDetail OfficeDetails { get; set; }

        [ForeignKey("GradeId")]
        public int? GradeId { get; set; }
        public JobGrade JobGrade { get; set; }

        public int? EmployeeID { get; set; }
        [ForeignKey("EmployeeID")]
        public EmployeeDetail EmployeeDetail { get; set; }

        public long? ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public ProjectDetail ProjectDetail { get; set; }
        public bool IsCompleted { get; set; }
        public JobHiringDetails JobHiringDetails { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.HR
{
    public class SalaryTaxReportContent : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int SalaryTaxReportContentId { get; set; }
        public string EmployerAuthorizedOfficerName { get; set; }
        public string PositionAuthorizedOfficer { get; set; }
        public int OfficeId { get; set; }
    }
}

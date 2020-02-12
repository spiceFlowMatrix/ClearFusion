using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using HumanitarianAssistance.Domain.Entities.HR;

namespace HumanitarianAssistance.Domain.Entities
{
    public class StrongandWeakPoints : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int StrongPointsId { get; set; }
        public DateTime CurrentAppraisalDate { get; set; }
        public int EmployeeId { get; set; }
        public int? EmployeeAppraisalDetailsId { get; set; }
        [ForeignKey("EmployeeAppraisalDetailsId")]
        public EmployeeAppraisalDetails EmployeeAppraisalDetails { get; set; }
        public string Point { get; set; }
        public int Status { get; set; }                 // 1 for strong and 2 for weak (Managed at backend)


    }
}

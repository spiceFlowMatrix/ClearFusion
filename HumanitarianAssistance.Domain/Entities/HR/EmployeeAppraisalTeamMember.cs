using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.HR
{
    public class EmployeeAppraisalTeamMember: BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int EmployeeAppraisalTeamMemberId { get; set; }
         public int? EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public EmployeeDetail EmployeeDetail { get; set; }

        public int? EmployeeAppraisalDetailsId { get; set; }
        [ForeignKey("EmployeeAppraisalDetailsId")]
        public EmployeeAppraisalDetails EmployeeAppraisalDetails { get; set; }
    }
}

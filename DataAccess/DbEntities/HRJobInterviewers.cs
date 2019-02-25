using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.DbEntities
{
    public class HRJobInterviewers : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public long HRJobInterviewerId { get; set; }
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public EmployeeDetail EmployeeDetail { get; set; }
        public int InterviewDetailsId { get; set; }
        [ForeignKey("InterviewDetailsId")]
        public InterviewDetails InterviewDetails { get; set; }
    }
}

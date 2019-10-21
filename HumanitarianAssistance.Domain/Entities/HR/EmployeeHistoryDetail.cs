using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.HR
{
    public class EmployeeHistoryDetail : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long HistoryID { get; set; }
        public int? EmployeeID { get; set; }
        public EmployeeDetail EmployeeDetail { get; set; }
        public DateTime? HistoryDate { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
    }
}

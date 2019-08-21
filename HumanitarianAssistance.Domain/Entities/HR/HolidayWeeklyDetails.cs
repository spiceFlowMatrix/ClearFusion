using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.HR
{
    public class HolidayWeeklyDetails : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int HolidayWeeklyId { get; set; }
        [StringLength(30)]
        public string Day { get; set; }
        public int OfficeId { get; set; }
        public OfficeDetail OfficeDetails { get; set; }
        public int FinancialYearId { get; set; }
        public FinancialYearDetail FinancialYearDetails { get; set; }
    }
}

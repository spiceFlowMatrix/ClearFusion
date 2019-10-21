using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.HR
{
    public  class HolidayDetails : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long HolidayId { get; set; }
        [StringLength(50)]
        public string HolidayName { get; set; }
        public DateTime Date { get; set; }
        public string Remarks { get; set; }
        public int? FinancialYearId { get; set; }
        public FinancialYearDetail FinancialYearDetail { get; set; }
        public int? OfficeId { get; set; }
        public OfficeDetail OfficeDetails { get; set; }
        public int? HolidayType { get; set; }
    }
}

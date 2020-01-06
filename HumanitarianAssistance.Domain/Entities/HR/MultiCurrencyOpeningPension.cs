using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.HR
{
    public class MultiCurrencyOpeningPension: BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long PensionId { get; set; }
        public int? EmployeeID { get; set; }
        [ForeignKey("EmployeeID")]
        public EmployeeDetail EmployeeDetail { get; set; }
        public DateTime? PensionStartDate { get; set; }
        public int? CurrencyId { get; set; }
        [ForeignKey("CurrencyId")]
        public CurrencyDetails CurrencyDetails { get; set; }
        public double? Amount { get; set; }
	}
}

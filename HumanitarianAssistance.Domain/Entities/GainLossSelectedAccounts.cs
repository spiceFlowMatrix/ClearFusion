using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using HumanitarianAssistance.Domain.Entities.Accounting;
using HumanitarianAssistance.Domain.Entities.HR;

namespace HumanitarianAssistance.Domain.Entities
{
    public class GainLossSelectedAccounts : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int GainLossSelectedAccountId { get; set; }
        public int? EmployeeId { get; set; }
        public string UserId { get; set; }
        public int? CurrencyId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime ComparisionDate { get; set; }
        public long? DebitAccountId { get; set; }
        public long? CreditAccountId { get; set; }
        public long[] SelectedAccounts { get; set; }
        [ForeignKey("EmployeeId")]
        public EmployeeDetail EmployeeDetail { get; set; }
        [ForeignKey("CurrencyId")]
        public CurrencyDetails CurrencyDetails { get; set; }
        [ForeignKey("DebitAccountId")]
        public ChartOfAccountNew DebitAccount { get; set; }
        [ForeignKey("CreditAccountId")]
        public ChartOfAccountNew CreditAccount { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.Accounting
{
    public class AccountHeadType : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int AccountHeadTypeId { get; set; }
        public string AccountHeadTypeName { get; set; }
        public bool IsCreditBalancetype { get; set; }
    }
}

using DataAccess.DbEntities.AccountingNew;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public class PensionDebitAccountMaster : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public int PensionDebitAccountId { get; set; }
        public long ChartOfAccountNewId { get; set; }
        [ForeignKey("ChartOfAccountNewId")]
        public ChartOfAccountNew ChartOfAccountNew { get; set; }
    }
}

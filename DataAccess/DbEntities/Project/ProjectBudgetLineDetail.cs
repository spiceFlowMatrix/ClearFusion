using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities.Project
{
   public class ProjectBudgetLineDetail: BaseEntityWithoutId
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public long  BudgetLineId { get; set; }
        public string BudgetCode { get; set; }
        public string BudgetName { get; set; }
        public long? ProjectJobId { get; set; }
        [ForeignKey("ProjectJobId")]
        public ProjectJobDetail ProjectJobDetail { get; set; }
        public double? InitialBudget  { get; set; }
        public long? ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public ProjectDetail ProjectDetail { get; set; }
        public int? CurrencyId { get; set; }
        [ForeignKey("CurrencyId")]
        public CurrencyDetails CurrencyDetails { get; set; }
        public List<VoucherTransactions> VoucherTransactions { get; set; }

    }
}

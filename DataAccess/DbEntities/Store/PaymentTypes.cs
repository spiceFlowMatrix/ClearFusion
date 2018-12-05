using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities.Store
{
    public class PaymentTypes : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public int PaymentId { get; set; }
        public string Name { get; set; }
        public int AccountId { get; set; }
        [ForeignKey("AccountId")]
        ChartAccountDetail ChartAccountDetail { get; set; }
    }
}

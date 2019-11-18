using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.Project
{
    public class ProjectLogisticSuppliers : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long SupplierId { get; set; }
        public string SupplierName { get; set; }
        public long Quantity { get; set; }
        public double FinalPrice { get; set; }
        public long LogisticRequestsId { get; set; }
        [ForeignKey("LogisticRequestsId")]
        public virtual ProjectLogisticRequests ProjectLogisticRequests { get; set; }
    }
}
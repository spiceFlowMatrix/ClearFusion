using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using HumanitarianAssistance.Domain.Entities.Store;

namespace HumanitarianAssistance.Domain.Entities.Project
{
    public class ProjectLogisticSuppliers : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long SupplierId { get; set; }
        public long StoreSourceCodeId { get; set; }
        public long Quantity { get; set; }
        public double FinalUnitPrice { get; set; }
        public long ItemId { get; set; }
        public long LogisticRequestsId { get; set; }
        [ForeignKey("LogisticRequestsId")]
        public virtual ProjectLogisticRequests ProjectLogisticRequests { get; set; }
        [ForeignKey("StoreSourceCodeId")]
        public virtual StoreSourceCodeDetail StoreSourceCodeDetail { get; set; }
        [ForeignKey("ItemId")]
        public virtual StoreInventoryItem InventoryItems { get; set; }
    }
}
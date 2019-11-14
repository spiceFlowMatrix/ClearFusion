using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HumanitarianAssistance.Domain.Entities.Store;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.Project
{
    public class ProjectLogisticItems : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long LogisticItemId { get; set; }
        public long Quantity { get; set; }
        public double EstimatedCost { get; set; }
        public long ItemId { get; set; }
        public bool PurchaseSubmitted { get; set; }
        public long LogisticRequestsId { get; set; }
        public double? FinalCost { get; set; }
        [ForeignKey("LogisticRequestsId")]
        public virtual ProjectLogisticRequests ProjectLogisticRequests { get; set; }
        [ForeignKey("ItemId")]
        public virtual StoreInventoryItem StoreInventoryItem { get; set; }
    }
}
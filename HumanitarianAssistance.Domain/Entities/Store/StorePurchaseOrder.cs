using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HumanitarianAssistance.Domain.Entities.HR;

namespace HumanitarianAssistance.Domain.Entities.Store
{
    public class StorePurchaseOrder : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string OrderId { get; set; }
        public string Purchase { get; set; }
        public string InventoryItem { get; set; }

        public int IssuedQuantity { get; set; }
        public bool MustReturn { get; set; }
        public bool Returned { get; set; }
        public int IssuedToEmployeeId { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime? ReturnedDate { get; set; }

        [ForeignKey("Purchase")]
        public StoreItemPurchase StoreItemPurchase { get; set; }
        [ForeignKey("InventoryItem")]
        public StoreInventoryItem StoreInventoryItem { get; set; }
        [ForeignKey("IssuedToEmployeeId")]
        public EmployeeDetail EmployeeDetail { get; set; }


        public string IssueVoucherNo { get; set; }

        public string Remarks { get; set; }

        public long Project { get; set; }

        public string IssedToLocation { get; set; }

        public int StatusAtTimeOfIssue { get; set; }
    }
}

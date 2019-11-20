using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanitarianAssistance.Domain.Entities.Store
{
    public class StoreLogger: BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long Id { get; set; }
        public string EventType { get; set; }
        public string LogText { get; set; }
        public long? PurchaseId { get; set; }
        public int? TransportType { get; set; }
        public long? TransportTypeEntityId { get; set; } // vehicle or generator id
        [ForeignKey("PurchaseId")]
        public StoreItemPurchase StoreItemPurchase {get; set;}
    }
}
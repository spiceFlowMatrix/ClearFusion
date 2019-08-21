using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanitarianAssistance.Domain.Entities.Store
{
    public class ItemPurchaseDocument : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int DocumentId { get; set; }
        public string DocumentName { get; set; }
        public byte[] File { get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }
        public string DocumentGuid { get; set; }
        public string Purchase { get; set; }

        [ForeignKey("Purchase")]
        public StoreItemPurchase ItemPurchase { get; set; }
    }
}

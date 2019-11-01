using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.DbEntities.Store
{
    public class ItemPurchaseDocument : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public int DocumentId { get; set; }
        public string DocumentName { get; set; }
        public byte[] File { get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }
        public string DocumentGuid { get; set; }
        public long PurchaseId { get; set; }

        [ForeignKey("Purchase")]
        public StoreItemPurchase ItemPurchase { get; set; }
    }
}

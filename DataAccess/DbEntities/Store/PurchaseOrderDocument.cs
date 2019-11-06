using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.DbEntities.Store
{
    public class PurchaseOrderDocument : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public string DocumentId { get; set; }

        public string DocumentName { get; set; }
        public string DocumentExtension { get; set; }
        public string DocumentFileType { get; set; }
        public string DocumentFileName { get; set; }
    }
}

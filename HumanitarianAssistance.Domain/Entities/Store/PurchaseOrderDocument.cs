using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanitarianAssistance.Domain.Entities.Store
{
    public class PurchaseOrderDocument : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public string DocumentId { get; set; }

        public string DocumentName { get; set; }
        public string DocumentExtension { get; set; }
        public string DocumentFileType { get; set; }
        public string DocumentFileName { get; set; }
    }
}

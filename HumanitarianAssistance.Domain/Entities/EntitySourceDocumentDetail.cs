using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities
{
    public class EntitySourceDocumentDetail : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long EntitySourceDocumentId { get; set; }
        public long EntityId { get; set; }
        public long DocumentFileId { get; set; }
        [ForeignKey("DocumentFileId")]
        public DocumentFileDetail DocumentFileDetail { get; set; }
    }
}

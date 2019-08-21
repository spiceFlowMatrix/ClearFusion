using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities
{
    public class ChatDetail : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long ChatId { get; set; }
        public int ChatSourceEntityId { get; set; }
        public int EntityId { get; set; }
        public string Message { get; set; }
        public long? EntitySourceDocumentId { get; set; }
        [ForeignKey("EntitySourceDocumentId")]
        public EntitySourceDocumentDetail EntitySourceDocumentDetail { get; set; }
    }
}

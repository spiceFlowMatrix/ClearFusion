using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities
{
    public class UserTokenCaches : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int UserTokenCacheId { get; set; }
        public string webUserUniqueId { get; set; }
        public string cacheBits { get; set; }
        public DateTime? LastWrite { get; set; }
    }
}

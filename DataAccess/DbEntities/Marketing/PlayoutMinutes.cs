using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities.Marketing
{
    public class PlayoutMinutes : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long PlayoutMinuteId { get; set; }
        [ForeignKey("PolicyId")]
        public long? PolicyId { get; set; }
        public virtual PolicyDetail PolicyDetails { get; set; }
        public long? TotalMinutes { get; set; }
        public long? DroppedMinutes { get; set; }

    }
}

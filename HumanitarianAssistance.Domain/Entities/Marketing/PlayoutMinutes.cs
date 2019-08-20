using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.Marketing
{
    public class PlayoutMinutes : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long PlayoutMinuteId { get; set; }
        [ForeignKey("ScheduleId")]
        public long? ScheduleId { get; set; }
        public virtual ScheduleDetails ScheduleDetails { get; set; }
        public long? TotalMinutes { get; set; }
        public long? DroppedMinutes { get; set; }

    }
}

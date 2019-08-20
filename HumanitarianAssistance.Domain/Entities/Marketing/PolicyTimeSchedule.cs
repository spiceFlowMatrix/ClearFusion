using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.Marketing
{
    public class PolicyTimeSchedule: BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long Id { get; set; }
        [ForeignKey("PolicyId")]
        public long? PolicyId { get; set; }
        public virtual PolicyDetail PolicyDetails { get; set; }
        public string TimeScheduleCode { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }        
        public bool IsActive { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.Marketing
{
    public class PolicySchedule: BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long PolicyScheduleId { get; set; }
        [ForeignKey("PolicyId")]
        public long? PolicyId { get; set; }
        public PolicyDetail PolicyDetails { get; set; }
        public string ScheduleCode { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? Frequency { get; set; }      
        public int? ByMonth { get; set; }
        public int? ByWeek { get; set; }
        public int? ByDay { get; set; }
        public DateTime StartDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime EndDate { get; set; }
        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public string RepeatDays { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using HumanitarianAssistance.Domain.Entities.Project;

namespace HumanitarianAssistance.Domain.Entities.Marketing
{
    public class ScheduleDetails : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long ScheduleId { get; set; }
        public string ScheduleType { get; set; }
        public string ScheduleName { get; set; }
        public string ScheduleCode { get; set; }
        [ForeignKey("PolicyId")]
        public long? PolicyId { get; set; }
        public virtual PolicyDetail PolicyDetails { get; set; }
        [ForeignKey("ProjectId")]
        public long? ProjectId { get; set; }
        public virtual ProjectDetail ProjectDetail { get; set; }
        [ForeignKey("JobId")]
        public long? JobId { get; set; }
        public virtual JobDetails JobDetails { get; set; }
        [ForeignKey("MediumId")]
        public long? MediumId { get; set; }
        public virtual Medium Mediums { get; set; }
        [ForeignKey("ChannelId")]
        public long? ChannelId { get; set; }
        public virtual Channel Channel { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
        public bool IsActive { get; set; }
    }
}

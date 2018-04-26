using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public partial class AssignActivity : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public long AssignActivityId { get; set; }
        public long ProjectId { get; set; }
        public ProjectDetails ProjectDetails { get; set; }
        public int TaskId { get; set; }
        public TaskMaster TaskMaster { get; set; }
        public int ActivityId { get; set; }
        public ActivityMaster ActivityMaster { get; set; }
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public DateTime? PlannedStartDate { get; set; }
        public DateTime? PlannedEndDate { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        [StringLength(20)]
        public string Status { get; set; }
        [StringLength(30)]
        public string ApprovedStatus { get; set; }
    }
}

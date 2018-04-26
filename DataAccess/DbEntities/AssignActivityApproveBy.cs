using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public partial class AssignActivityApproveBy
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public long AssignActivityApprovedById { get; set; }
        public long AssignActivityId { get; set; }
        public AssignActivity AssignActivity { get; set; }
        public string ApprovedById { get; set; }
        public AppUser ApprovedBy { get; set; }
        [StringLength(20)]
        public string Status { get; set; }
        public DateTime? Date { get; set; }
        public string Description { get; set; }
    }
}

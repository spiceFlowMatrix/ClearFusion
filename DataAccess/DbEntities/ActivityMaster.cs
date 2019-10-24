using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public partial class ActivityMaster : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public int ActivityId { get; set; }
        public int TaskId { get; set; }
        public TaskMaster TaskMaster { get; set; }
        public string ActivityName { get; set; }
        [StringLength(20)]
        public string Priority { get; set; }
        public string Description { get; set; }
        [StringLength(50)]
        public string Status { get; set; }
    }
}

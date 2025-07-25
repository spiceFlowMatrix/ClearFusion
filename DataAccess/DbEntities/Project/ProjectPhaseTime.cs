﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities.Project
{
   public class ProjectPhaseTime : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public long ProjectPhaseTimeId { get; set; }
        public long ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public ProjectDetail ProjectDetail { get; set; }
        public long ProjectPhaseDetailsId { get; set; }
        [ForeignKey("ProjectPhaseDetailsId")]
        public ProjectPhaseDetails ProjectPhaseDetails { get; set; }
        public DateTime? PhaseStartData { get; set; }
        public DateTime? PhaseEndDate { get; set; } 

    }
}

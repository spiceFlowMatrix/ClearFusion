using DataAccess.DbEntities.Marketing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public class JobDetails : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long JobId { get; set; }
        public string JobName { get; set; }
        public string Description { get; set; }
        public string JobCode { get; set; }
        public int? ContractId { get; set; }
        [ForeignKey("JobPhaseId")]
        public long? JobPhaseId { get; set; }
        public JobPhase JobPhases { get; set; }        
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsApproved { get; set; }
    }
}

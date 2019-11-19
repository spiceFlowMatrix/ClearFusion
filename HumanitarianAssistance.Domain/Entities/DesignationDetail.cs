using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using HumanitarianAssistance.Domain.Entities.HR;

namespace HumanitarianAssistance.Domain.Entities
{
    public class DesignationDetail : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int DesignationId { get; set; }
        [StringLength(100)]
        public string Designation { get; set; }
        public string DesignationDari { get; set; }
        public string Description { get; set; }
        public virtual ICollection<TechnicalQuestion> TechnicalQuestion { get; set; }
    }
}

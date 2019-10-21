using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities
{
    public class DonorDetail : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long DonorId { get; set; }
        public string Name { get; set; }
        public string ContactPerson { get; set; }
        public string ContactDesignation { get; set; }
        public string ContactPersonEmail { get; set; }
        public string ContactPersonCell { get; set; }


    }
}

using DataAccess.DbEntities.Project;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities
{
    public class DonorDetail : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public long DonorId { get; set; }
        public string Name { get; set; }
        public string ContactPerson { get; set; }
        public string ContactDesignation { get; set; }
        public string ContactPersonEmail { get; set; }
        public string ContactPersonCell { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.Project
{
    public class ProvinceMultiSelect : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long ProvinceMultiSelectId { get; set; }
        [ForeignKey("ProjectId")]
        public ProjectDetail ProjectDetail { get; set; }
        public long ProjectId { get; set; }

        [ForeignKey("CountryMultiSelectId")]
        public long? CountryMultiSelectId { get; set; }
        public CountryMultiSelectDetails CountryMultiSelectDetails { get; set; }

        [ForeignKey("ProvinceId")]
        public ProvinceDetails ProvinceDetails { get; set; }
        public int ProvinceId { get; set; }

        public long? ProvinceSelectionId { get; set; }

    }
}

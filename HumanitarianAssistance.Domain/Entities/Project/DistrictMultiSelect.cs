using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace HumanitarianAssistance.Domain.Entities.Project
{
    public class DistrictMultiSelect : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public long DistrictMultiSelectId { get; set; }
        [ForeignKey("ProjectId")]
        public ProjectDetail ProjectDetail { get; set; }
        public long ProjectId { get; set; }
        [ForeignKey("DistrictID")]
        public DistrictDetail DistrictDetail { get; set; }
        public long DistrictID { get; set; }

        public long? DistrictSelectionId { get; set; }

        [ForeignKey("ProvinceId")]
        public ProvinceDetails ProvinceDetails { get; set; }
        public int ProvinceId { get; set; }

    }
}

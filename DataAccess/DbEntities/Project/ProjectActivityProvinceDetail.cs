﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DataAccess.DbEntities.Project
{
    public class ProjectActivityProvinceDetail : BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public long ActivityProvinceId { get; set; }

        public long ActivityId { get; set; }
        [ForeignKey("ActivityId")]
        public ProjectActivityDetail ProjectActivityDetail { get; set; }

        public int ProvinceId { get; set; }
        [ForeignKey("ProvinceId")]
        public ProvinceDetails ProvinceDetails { get; set; }

        public long? DistrictID { get; set; }
        [ForeignKey("DistrictID")]
        public DistrictDetail DistrictDetail { get; set; }
    }
}

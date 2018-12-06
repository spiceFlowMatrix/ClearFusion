using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities.Project
{
    public class PurposeofInitiativeCriteria: BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public long ProductServiceId { get; set; }
        public bool? Women { get; set; }
        [ForeignKey("ProjectId")]
        public long ProjectId { get; set; }
        public bool? Children { get; set; }
        public bool? Awareness { get; set; }
        public bool? Education { get; set; }
        public bool? DrugAbuses { get; set; }
        public bool? Right { get; set; }
        public bool? Culture { get; set; }
        public bool? Music { get; set; }
        public bool? Documentaries { get; set; }
        public bool? InvestigativeJournalism { get; set; }
        public bool? HealthAndNutrition { get; set; }
        public bool? News { get; set; }
        public bool? SocioPolitiacalDebate { get; set; }
        public bool? Studies { get; set; }
        public bool? Reports { get; set; }
        public bool? CommunityDevelopment { get; set; }
        public bool? Aggriculture { get; set; }
        public bool? DRR { get; set; }
        public bool? ServiceEducation { get; set; }
        public bool? ServiceHealthAndNutrition { get; set; }
        public bool? RadioProduction { get; set; }
        public bool? TVProgram { get; set; }
        public bool? PrintedMedia { get; set; }
        public bool? RoundTable { get; set; }
        public bool? Others { get; set; }
        public bool? OtherActivity { get; set; }
        public bool? TargetBenificaiaryWomen { get; set; }
        public bool? TargetBenificiaryMen { get; set; }
        public bool? TargetBenificiaryAgeGroup { get; set; }
        public bool? TargetBenificiaryaOccupation { get; set; }
    }
}

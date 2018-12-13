﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Project
{
  public  class PurposeofInitiativeCriteriaModel
    {
        public long? ProductServiceId { get; set; }
        public bool? Women { get; set; }        
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
        public string OtherActivity { get; set; }
        public bool? TargetBenificaiaryWomen { get; set; }
        public bool? TargetBenificiaryMen { get; set; }
        public bool? TargetBenificiaryAgeGroup { get; set; }
        public bool? TargetBenificiaryaOccupation { get; set; }
        public bool? Service { get; set; }
        public bool? Product { get; set; }
    }
}

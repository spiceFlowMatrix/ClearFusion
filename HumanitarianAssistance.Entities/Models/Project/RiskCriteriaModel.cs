using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Project
{
   public class RiskCriteriaModel
    {
        public long? RiskCriteriaDetailId { get; set; }
        public long? ProjectId { get; set; }
        public bool? Security { get; set; }
        public bool? Staff { get; set; }
        public bool? ProjectAssets { get; set; }
        public bool? Suppliers { get; set; }
        public bool? Beneficiaries { get; set; }
        public bool? OverallOrganization { get; set; }
        public bool? DeliveryFaiLure { get; set; }
        public bool? PrematureSeizure { get; set; }
        public bool? GovernmentConfiscation { get; set; }
        public bool? DesctructionByTerroristActivity { get; set; }
        public bool? Reputation { get; set; }
        public bool? Religious { get; set; }
        public bool? Sectarian { get; set; }
        public bool? Ethinc { get; set; }
        public bool? Social { get; set; }
        public bool? Traditional { get; set; }
        public bool? FocusDivertingrisk { get; set; }
        public bool? Financiallosses { get; set; }
        public bool? Opportunityloss { get; set; }
        public string ProjectSelection { get; set; }
        public bool? Probablydelaysinfunding { get; set; }
        public bool? OtherOrganizationalHarms { get; set; }
        public string OrganizationalDescription { get; set; }
        public List<long?> ProjectSelectionId { get; set; }
    }
}

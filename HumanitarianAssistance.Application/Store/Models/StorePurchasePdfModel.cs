using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Store.Models
{


    public class StorePurchasePdfModel 
    {
        public StorePurchasePdfModel()
        {
            StorePurchaseList= new List<StorePurchasePdf>();
        }

        public string LogoPath {get; set;}
        public List<StorePurchasePdf> StorePurchaseList { get; set; }
    }

    public class StorePurchasePdf
    {
        public long PurchaseId { get; set; }
        public string ItemName { get; set; }
        public string PurchasedBy {get; set;}
        public string ProjectName {get; set;}
        public double OriginalCost {get; set;}
        public double DepriciatedCost {get; set;}
        
    }
}
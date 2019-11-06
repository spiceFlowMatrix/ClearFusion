using System;
using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Store.Models
{


    public class StorePurchasePdfModel 
    {
        public StorePurchasePdfModel()
        {
            StorePurchaseList= new List<StorePurchasePdf>();
            StorePurchasePdfFlags = new StorePurchasePdfFlags();
        }

        public string LogoPath {get; set;}
        public List<StorePurchasePdf> StorePurchaseList { get; set; }
        public StorePurchasePdfFlags StorePurchasePdfFlags {get; set;}
    }

    public class StorePurchasePdf
    {
        public long PurchaseId { get; set; }
        public string ItemCode { get; set; }
        public string ItemCodeDescription { get; set; }
        public string MasterInventoryCode { get; set; }
        public string Description { get; set; }
        public string OfficeCode { get; set; }
        public string BudgetLineName { get; set; }
        public long PurchaseOrderNo { get; set; }
        public string InvoiceDate {get; set;}
        public string AssetTypeName {get; set;}
        public string ItemName { get; set; }
        public long? ProjectId { get; set; }
        public string ProjectName { get; set; }
        public double OriginalCost { get; set; }
        public double DepreciatedCost { get; set; }
        public int PurchasedQuantity { get; set; }
        public string PurchaseDateDisplay {get; set;} // to be used to diplay
        public DateTime PurchaseDate {get; set;} // to be used for further calculation
        public string Unit { get; set; }
        public string CurrencyName { get; set; }
        public string ReceiptDate { get; set; }
        public double DepreciationRate { get; set; }
        public string ReceivedFromEmployee { get; set; }
        public string ReceivedFromLocationName { get; set; }
        public bool ApplyDepreciation { get; set; }
        public double UnitCost {get; set;}
        public int CurrencyId {get; set;}
        public string Status { get; set; }  
    }
     public class StorePurchasePdfFlags
    {
        public bool Id { get; set; }
        public bool Item { get; set; }
        public bool PurchasedBy { get; set; }
        public bool Project { get; set; }
        public bool OriginalCost { get; set; }
        public bool DepreciatedCost { get; set; }
        public bool PurchaseDate { get; set; }
        public bool Currency { get; set; }
        public bool PurchasedQuantity {get; set;}
        public bool ItemCode {get; set;}
        public bool ProjectId { get; set; }
        public bool ItemCodeDescription { get; set; }
        public bool Description { get; set; }
        public bool BudgetLineName { get; set; }
        public bool DepreciationRate { get; set; }
        public bool MasterInventoryCode { get; set; }
        public bool OfficeCode {get; set;}
        public bool ReceiptDate { get; set; }
        public bool CurrencyName { get; set; }
        public bool InvoiceDate { get; set; }
        public bool ReceivedFromLocationName { get; set; }
        public bool Status { get; set; }
    }
}
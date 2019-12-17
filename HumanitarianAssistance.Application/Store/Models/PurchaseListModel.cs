using System;
using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Store.Models
{
    public class PurchaseListModel
    {
        public PurchaseListModel()
        {
            ProcurementList= new List<ProcurementListModel>();
        }

        public long PurchaseId { get; set; }
        public long ItemId { get; set; }
        public string ItemCode { get; set; }
        public string ItemCodeDescription { get; set; }
        public string MasterInventoryCode { get; set; }
        public string Description { get; set; }
        public string OfficeCode { get; set; }
        public long? BudgetLineId { get; set; }
        public string BudgetLineName { get; set; }
        public long PurchaseOrderNo { get; set; }
        public string InvoiceDate {get; set;}
        public int? AssetTypeId {get; set;}
        public string AssetTypeName {get; set;}
        public int CurrencyId { get; set; }
        public string ItemName { get; set; }
        public string EmployeeName { get; set; }
        public long? ProjectId { get; set; }
        public string ProjectName { get; set; }
        public double OriginalCost { get; set; }
        public double DepreciatedCost { get; set; }
        public int PurchasedQuantity { get; set; }
        public DateTime PurchaseDate {get; set;}
        public string ChasisNo { get; set; }
        public string EngineSerialNo { get; set; }
        public string RegistrationNo { get; set; }
        public string IdentificationNo { get; set; }
        public string ModelType { get; set; }
        public string Unit { get; set; }
        public string CurrencyName { get; set; }
        public string ReceiptDate { get; set; }
        public double DepreciationRate { get; set; }
        public string ReceivedFromEmployee { get; set; }
        public string ReceivedFromLocationName { get; set; }
        public string Status { get; set; }
        public string Remarks { get; set; }
        public int recordsCount { get; set; }
        public string MakerCountry { get; set; }
        public bool ApplyDepreciation { get; set; }
        public int Quantity {get; set;}
        public double UnitCost {get; set;}
        public long? LogisticRequestId { get; set; }
        public List<ProcurementListModel> ProcurementList { get; set; }
    }
}
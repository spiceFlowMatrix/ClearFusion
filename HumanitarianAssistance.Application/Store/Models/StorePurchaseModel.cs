using System;
using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Store.Models
{
    public class StorePurchaseModel
    {
        public StorePurchaseModel()
        {
            PurchasedVehicleList = new List<PurchasedVehicleModel>();
            PurchasedGeneratorList= new List<PurchasedGeneratorModel>();
            StoreDocumentList = new List<StoreDocumentModel>();
        }
        public long? InventoryTypeId {get; set;}
        public long? InventoryId { get; set; }
        public long? ItemGroupId { get; set; }
        public long? ItemId { get; set; }
        public long PurchaseId { get; set; }
        public string SerialNo { get; set; }                    // Barcode Value
        public long InventoryItem { get; set; }               // Item Id
        public DateTime PurchaseDate { get; set; }              // Date Of Purchase
        public DateTime DeliveryDate { get; set; }              // The date that the item arrived at it's desired location or a service took place.		
        public int Currency { get; set; }                       // Currency ID
        public int UnitType { get; set; }
        public double UnitCost { get; set; }
        public int Quantity { get; set; }
        public bool ApplyDepreciation { get; set; }
        public double DepreciationRate { get; set; }
        public string ImageFileName { get; set; }               // Image String
        public string InvoiceFileName { get; set; }             // Invoice String
        public int PurchasedById { get; set; }                  // Employee ID
        public long? PurchaseOrderNo {get; set;}
        public int? AssetTypeId { get; set; } // 1. Cash , 2. In Kind
        public string InvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public int? Status { get; set; }
        public int? ReceiptTypeId { get; set; }
        public long? ReceivedFromLocation { get; set; }
        public long? ProjectId { get; set; }
        public long? BudgetLineId { get; set; }
        public int? PaymentTypeId { get; set; }
        public int? OfficeId { get; set; }
        public int? JournalCode { get; set; }
        public int? TimezoneOffset { get; set; }
        public string PurchaseName { get; set; }
        public long? TransportItemId { get; set; }
        public int? TransportItemTypeCategory { get; set; }
        public int? ItemGroupTransportCategory { get; set; }
        public string InventoryMasterName { get; set; }
        public string ItemGroupName { get; set; }
        public string ItemName { get; set; }
        public string ProjectName { get; set; }
        public string BudgetLineName { get; set; }
        public string ReceivedFromLocationName { get; set; }
        public string ReceivedFromEmployeeName { get; set; }
        public List<PurchasedVehicleModel> PurchasedVehicleList { get; set; }
        public List<PurchasedGeneratorModel> PurchasedGeneratorList { get; set; }
        public List<StoreDocumentModel> StoreDocumentList {get; set;}
    }
}
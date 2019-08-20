using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;

namespace HumanitarianAssistance.Application.Store.Commands.Common
{
    public class VerifyPurchaseCommand : BaseModel, IRequest<ApiResponse>
    {
        public string PurchaseId { get; set; }
        public string SerialNo { get; set; }                    // Barcode Value
        public string InventoryItem { get; set; }               // Item Id
        public DateTime PurchaseDate { get; set; }              // Date Of Purchase
        public DateTime DeliveryDate { get; set; }              // The date that the item arrived at it's desired location or a service took place.		
        public int Currency { get; set; }                       // Currency ID
        public int UnitType { get; set; }
        public long UnitCost { get; set; }
        public int Quantity { get; set; }
        public bool ApplyDepreciation { get; set; }
        public double DepreciationRate { get; set; }
        public string ImageFileName { get; set; }               // Image String
        public string InvoiceFileName { get; set; }             // Invoice String
        public int PurchasedById { get; set; }                  // Employee ID


        //Newly Added Fields
        public long? VoucherId { get; set; }
        public DateTime VoucherDate { get; set; }
        public int? AssetTypeId { get; set; } // 1. Cash , 2. In Kind
        public string InvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public int? Status { get; set; }
        public int? ReceiptTypeId { get; set; }
        public string ReceivedFromLocation { get; set; }
        public long? ProjectId { get; set; }
        public long? BudgetLineId { get; set; }
        public int? PaymentTypeId { get; set; }
        public bool? IsPurchaseVerified { get; set; }
        public long? VerifiedPurchaseVoucher { get; set; }
        public int? OfficeId { get; set; }
        public int? JournalCode { get; set; }
        public string VerifiedPurchaseVoucherReferenceNo { get; set; }
        public int? TimezoneOffset { get; set; }
    }
}

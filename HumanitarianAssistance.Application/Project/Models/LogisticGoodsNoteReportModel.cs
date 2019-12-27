using System;
using System.Collections.Generic;

namespace HumanitarianAssistance.Application.Project.Models
{
    public class LogisticGoodsNoteReportModel
    {

        public LogisticGoodsNoteReportModel()
        {
            GoodsNoteAmountModel = new List<GoodsNoteAmountModel>();
        }
        public string ProjectCode { get; set; }
        public long Number { get; set; }
        public string JobCode { get; set; }
        public string PurchaseDate { get; set; }
        public string BudgetLine { get; set; }
        public string HeaderLogo { get; set; }
        public string ChaDesignLogo { get; set; }
        public string ApplySheetText { get; set; }
        public string InstructionText { get; set; }
        public int TotalQuantity { get; set; }
        public double TotalUnitPrice { get; set; }
        public double TotalEstimatedAmount { get; set; }
        public string OfficeName { get; set; }
        public List<GoodsNoteAmountModel> GoodsNoteAmountModel { get; set; }

    }
    public class GoodsNoteAmountModel
    {
        public string ItemDescription { get; set; }
        public int Quantity { get; set; }
        public double UnitPrice { get; set; }
        public int ToBePurchased { get; set; }
        public double EstimatedAmount { get; set; }

    }
}
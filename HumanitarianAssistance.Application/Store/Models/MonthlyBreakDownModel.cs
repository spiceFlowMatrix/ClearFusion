using System;
using System.Collections.Generic;
using HumanitarianAssistance.Domain.Entities.Store;

namespace HumanitarianAssistance.Application.Store.Models
{
    public class MonthlyBreakDownModel
    {
        public long VehicleId { get; set; }
        public long GeneratorId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string PlateNo { get; set; }
        public double Voltage { get; set; }
        public int EmployeeId { get; set; }
        public double StandardFuelConsumptionRate { get; set; }
        public double StandardMobilOilConsumptionRate { get; set; }
        public double StartingMileage { get; set; }
        public double IncurredMileage { get; set; }
        public double StartingUsage { get; set; }
        public double IncurredUsage { get; set; }
        public int ModelYear { get; set; }
        public int OfficeId { get; set; }
        public double OriginalCost { get; set; }
        public string EmployeeName { get; set; }
        public string PurchaseName { get; set; }
        public long PurchaseId { get; set; }
        public string OfficeName { get; set; }
        public StoreItemPurchase StoreItemPurchase {get; set;}
        public List<VehicleItemDetail> VehicleItemDetail { get; set; }
        public List<VehicleMileageDetail> VehicleMileageDetail { get; set; }
        public List<GeneratorItemDetail> GeneratorItemDetail { get; set; }
        public List<GeneratorUsageHourDetail> GeneratorUsageHourDetail { get; set; }
    }

    public class TotalMobilOilAndMonth 
    {
        public double TotalMobilOil { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
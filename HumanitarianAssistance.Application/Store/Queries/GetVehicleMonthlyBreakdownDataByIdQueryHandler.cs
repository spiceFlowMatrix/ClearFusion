using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Store.Models;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetVehicleMonthlyBreakdownDataByIdQueryHandler : IRequestHandler<GetVehicleMonthlyBreakdownDataByIdQuery, MonthlyBreakdownDataModel>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetVehicleMonthlyBreakdownDataByIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<MonthlyBreakdownDataModel> Handle(GetVehicleMonthlyBreakdownDataByIdQuery request, CancellationToken cancellationToken)
        {
            MonthlyBreakdownDataModel model = new MonthlyBreakdownDataModel();

            try
            {

                var vehicleData = await _dbContext.PurchasedVehicleDetail
                                          .Include(x => x.EmployeeDetail)
                                          .Include(x => x.StoreItemPurchase)
                                          .ThenInclude(x => x.StoreInventoryItem)
                                          .ThenInclude(x => x.StoreItemGroup)
                                          .Include(x => x.VehicleMileageDetail)
                                          .Include(x => x.VehicleItemDetail)
                                          .ThenInclude(x => x.StoreItemPurchase)
                                          .ThenInclude(x => x.StoreInventoryItem)
                                          .ThenInclude(x => x.StoreItemGroup)
                                          .Where(x => x.IsDeleted == false && x.Id == request.VehicleId && x.CreatedDate.Value.Year == request.SelectedYear).ToListAsync();


                MonthlyBreakDownModel vehicle = vehicleData
                                                 .Select(x => new MonthlyBreakDownModel
                                                 {
                                                     CreatedDate = x.CreatedDate.Value,
                                                     VehicleId = x.Id,
                                                     PlateNo = x.PlateNo,
                                                     EmployeeId = x.EmployeeId,
                                                     StandardFuelConsumptionRate = x.FuelConsumptionRate,
                                                     StandardMobilOilConsumptionRate = x.MobilOilConsumptionRate,
                                                     StartingMileage = x.StartingMileage,
                                                     IncurredMileage = x.IncurredMileage,
                                                     ModelYear = x.ModelYear,
                                                     OfficeId = x.OfficeId,
                                                     OriginalCost = x.StoreItemPurchase.UnitCost,
                                                     EmployeeName = x.EmployeeDetail.EmployeeName,
                                                     PurchaseName = $"{x.StoreItemPurchase.PurchaseName} - {x.StoreItemPurchase.PurchaseDate.ToShortDateString()}-{x.StoreItemPurchase.PurchaseId}",
                                                     PurchaseId = x.StoreItemPurchase.PurchaseId,
                                                     OfficeName = x.OfficeId != 0 ? _dbContext.OfficeDetail.FirstOrDefault(y => y.IsDeleted == false && y.OfficeId == x.OfficeId).OfficeName : null,
                                                     VehicleItemDetail = x.VehicleItemDetail.Where(y => y.IsDeleted == false && y.CreatedDate.Value.Date.Year == request.SelectedYear).ToList(),
                                                     VehicleMileageDetail = x.VehicleMileageDetail.Where(y => y.IsDeleted == false && y.CreatedDate.Value.Date.Year >= (request.SelectedYear)).ToList() // selectedYear -1 =
                                                 }).FirstOrDefault();

                if (vehicle != null)
                {
                    model.IncurredMileage = vehicle.IncurredMileage;
                    model.StandardFuelConsumptionRate = vehicle.StandardFuelConsumptionRate;
                    model.StandardMobilOilConsumptionRate = vehicle.StandardMobilOilConsumptionRate;
                    model.StartingMileage = vehicle.StartingMileage;
                    model.StartingCost = vehicle.OriginalCost;

                    Array UsageTypeValues = Enum.GetValues(typeof(UsageType));

                    foreach (UsageType val in UsageTypeValues)
                    {
                        // Skip Current Usage
                        if (val != UsageType.CurrentUsage)
                        {
                            UsageAnalysisBreakDown Usagedata;
                            CostAnalysisBreakDown Costdata;
                            SwitchCaseStatement(vehicle, request.SelectedYear, (int)val, out Usagedata, out Costdata);
                            model.UsageAnalysisBreakDownList.Add(Usagedata);
                        }

                    }

                    Array costAnalysisValues = Enum.GetValues(typeof(CostAnalysis));

                    foreach (CostAnalysis val in costAnalysisValues)
                    {
                        UsageAnalysisBreakDown Usagedata;
                        CostAnalysisBreakDown Costdata;
                        SwitchCaseStatement(vehicle, request.SelectedYear, (int)val, out Usagedata, out Costdata);
                        model.CostAnalysisBreakDownList.Add(Costdata);
                    }

                    CostAnalysisBreakDown totalCost = new CostAnalysisBreakDown();

                    totalCost.Header = "Total Cost";
                    totalCost.January = model.CostAnalysisBreakDownList.Sum(x => x.January) + (vehicle.CreatedDate.Month <= (int)Month.January ? vehicle.OriginalCost : 0);
                    totalCost.February = model.CostAnalysisBreakDownList.Sum(x => x.February) + (vehicle.CreatedDate.Month <= (int)Month.February ? vehicle.OriginalCost : 0);
                    totalCost.March = model.CostAnalysisBreakDownList.Sum(x => x.March) + (vehicle.CreatedDate.Month <= (int)Month.March ? vehicle.OriginalCost : 0);
                    totalCost.April = model.CostAnalysisBreakDownList.Sum(x => x.April) + (vehicle.CreatedDate.Month <= (int)Month.April ? vehicle.OriginalCost : 0);
                    totalCost.May = model.CostAnalysisBreakDownList.Sum(x => x.May) + (vehicle.CreatedDate.Month <= (int)Month.May ? vehicle.OriginalCost : 0);
                    totalCost.June = model.CostAnalysisBreakDownList.Sum(x => x.June) + (vehicle.CreatedDate.Month <= (int)Month.June ? vehicle.OriginalCost : 0);
                    totalCost.July = model.CostAnalysisBreakDownList.Sum(x => x.July) + (vehicle.CreatedDate.Month <= (int)Month.July ? vehicle.OriginalCost : 0);
                    totalCost.August = model.CostAnalysisBreakDownList.Sum(x => x.August) + (vehicle.CreatedDate.Month <= (int)Month.August ? vehicle.OriginalCost : 0);
                    totalCost.September = model.CostAnalysisBreakDownList.Sum(x => x.September) + (vehicle.CreatedDate.Month <= (int)Month.September ? vehicle.OriginalCost : 0);
                    totalCost.October = model.CostAnalysisBreakDownList.Sum(x => x.October) + (vehicle.CreatedDate.Month <= (int)Month.October ? vehicle.OriginalCost : 0);
                    totalCost.November = model.CostAnalysisBreakDownList.Sum(x => x.November) + (vehicle.CreatedDate.Month <= (int)Month.November ? vehicle.OriginalCost : 0);
                    totalCost.December = model.CostAnalysisBreakDownList.Sum(x => x.December) + (vehicle.CreatedDate.Month <= (int)Month.December ? vehicle.OriginalCost : 0);

                    model.CostAnalysisBreakDownList.Add(totalCost);
                }
                else
                {
                    model.IncurredMileage = 0;
                    model.StandardFuelConsumptionRate = 0;
                    model.StandardMobilOilConsumptionRate = 0;
                    model.StartingMileage = 0;
                    model.StartingCost = 0;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return model;
        }


        private double GetMonthlyVehicleDetail(MonthlyBreakDownModel vehicle, int selectedYear, int month, int usageType)
        {
            double monthData = 0;

            try
            {
                if (vehicle.CreatedDate.Year <= selectedYear)
                {
                    if (usageType == (int)UsageType.CurrentMileage)
                    {
                        // if mileage is present for the month
                        if (vehicle.VehicleMileageDetail.Any(x => x.MileageMonth.Month == month && x.CreatedDate.Value.Year == selectedYear))
                        {
                            // get sum of all mileages of all previous months if any then add mileage of current month and if vehicle is purchase in current month then add starting mileage and incurred mileage
                            monthData = vehicle.VehicleMileageDetail
                                                            .Where(x => x.CreatedDate.Value.Year == selectedYear && x.MileageMonth.Month < month)
                                                            .Select(x => x.Mileage).DefaultIfEmpty(0).Sum() + vehicle.VehicleMileageDetail.First(x => x.MileageMonth.Month == month
                                                                                                                 && x.CreatedDate.Value.Year == selectedYear).Mileage +
                                                                                                                 ((vehicle.CreatedDate.Date.Month == month && vehicle.CreatedDate.Year <= selectedYear) ? (vehicle.StartingMileage + vehicle.IncurredMileage) : 0);
                        }
                        else // if mileage is not present for the month then sum previous months mileages
                        {
                            // get sum of all mileages of all previous months if any
                            monthData = vehicle.VehicleMileageDetail
                                                            .Where(x => x.CreatedDate.Value.Year == selectedYear && x.MileageMonth.Month < month)
                                                            .Select(x => x.Mileage).DefaultIfEmpty(0).Sum() +
                                                            ((vehicle.CreatedDate.Date.Month <= month && vehicle.CreatedDate.Year == selectedYear) ? (vehicle.StartingMileage + vehicle.IncurredMileage) : 0);
                        }
                    }
                    else if (usageType == (int)CostAnalysis.FuelTotalCost)
                    {
                        // if Fuel is present for the month
                        if (vehicle.VehicleItemDetail.Any(x => x.CreatedDate.Value.Month == month && x.CreatedDate.Value.Year == selectedYear &&
                        x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Vehicle &&
                           x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.Fuel))
                        {
                            // get sum of all fuel of all previous months if any
                            monthData = vehicle.VehicleItemDetail
                                               .Where(x => x.CreatedDate.Value.Year == selectedYear && x.CreatedDate.Value.Month < month &&
                                               x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Vehicle &&
                                               x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.Fuel)
                                               .Select(x => x.StoreItemPurchase.Quantity * x.StoreItemPurchase.UnitCost)
                                               .DefaultIfEmpty(0).Sum() +
                                        vehicle.VehicleItemDetail.Where(x => x.CreatedDate.Value.Month == month
                                                                        && x.CreatedDate.Value.Year == selectedYear &&
                                                                        x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Vehicle &&
                                                                        x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.Fuel)
                                                                 .Select(y => y.StoreItemPurchase.UnitCost * y.StoreItemPurchase.Quantity)
                                                                 .First();
                        }
                        else // if fuel is not present for the month then sum previous months mileages
                        {
                            // get sum of all fuel purchase of all previous months if any
                            monthData = vehicle.VehicleItemDetail
                                               .Where(x => x.CreatedDate.Value.Year == selectedYear && x.CreatedDate.Value.Month < month
                                                     && x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Vehicle &&
                                                     x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.Fuel)
                                               .Select(x => x.StoreItemPurchase.Quantity * x.StoreItemPurchase.UnitCost).DefaultIfEmpty(0)
                                               .Sum();
                        }
                    }
                    else if (usageType == (int)CostAnalysis.MobilOilTotalCost)
                    {
                        // if mobil oil is present for the month
                        if (vehicle.VehicleItemDetail.Any(x => x.CreatedDate.Value.Month == month && x.CreatedDate.Value.Year == selectedYear &&
                            x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Vehicle &&
                                                     x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.MobilOil))
                        {
                            // get sum of all mobil oil of all previous months if any
                            monthData = vehicle.VehicleItemDetail
                                               .Where(x => x.CreatedDate.Value.Year == selectedYear && x.CreatedDate.Value.Month < month &&
                                               x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Vehicle &&
                                               x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.MobilOil)
                                               .Select(x => x.StoreItemPurchase.Quantity * x.StoreItemPurchase.UnitCost)
                                               .DefaultIfEmpty(0).Sum() +
                                               vehicle.VehicleItemDetail.Where(x => x.CreatedDate.Value.Month == month
                                                                        && x.CreatedDate.Value.Year == selectedYear &&
                                                                        x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Vehicle &&
                                                                        x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.MobilOil)
                                                                 .Select(y => y.StoreItemPurchase.UnitCost * y.StoreItemPurchase.Quantity)
                                                                 .First();
                        }
                        else // if mobil oil is not present for the month then sum previous months
                        {
                            // get sum of all mobil oil purchase of all previous months if any
                            monthData = vehicle.VehicleItemDetail
                                               .Where(x => x.CreatedDate.Value.Year == selectedYear && x.CreatedDate.Value.Month < month &&
                                               x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Vehicle &&
                                                                        x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.MobilOil)
                                               .Select(x => x.StoreItemPurchase.Quantity * x.StoreItemPurchase.UnitCost).DefaultIfEmpty(0)
                                               .Sum();
                        }
                    }
                    else if (usageType == (int)UsageType.TotalFuelUsage)
                    {
                        // if fuel is present for the month
                        if (vehicle.VehicleItemDetail.Any(x => x.CreatedDate.Value.Month == month && x.CreatedDate.Value.Year == selectedYear &&
                            x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Vehicle &&
                                                                        x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.Fuel))
                        {
                            // get sum of all fuel of all previous months if any
                            monthData = vehicle.VehicleItemDetail
                                               .Where(x => x.CreatedDate.Value.Year == selectedYear && x.CreatedDate.Value.Month < month
                                                      && x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Vehicle &&
                                                                        x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.Fuel)
                                               .Select(x => x.StoreItemPurchase.Quantity)
                                               .DefaultIfEmpty(0).Sum() +
                                        vehicle.VehicleItemDetail.Where(x => x.CreatedDate.Value.Month == month
                                                                        && x.CreatedDate.Value.Year == selectedYear &&
                                                                        x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Vehicle &&
                                                                        x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.Fuel)
                                                                 .Select(y => y.StoreItemPurchase.Quantity)
                                                                 .First();
                        }
                        else // if fuel is not present for the month then sum previous months
                        {
                            // get sum of all fuel purchase of all previous months if any
                            monthData = vehicle.VehicleItemDetail
                                               .Where(x => x.CreatedDate.Value.Year == selectedYear && x.CreatedDate.Value.Month < month
                                                    && x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Vehicle &&
                                                                        x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.Fuel)
                                               .Select(x => x.StoreItemPurchase.Quantity).DefaultIfEmpty(0)
                                               .Sum();
                        }
                    }
                    else if (usageType == (int)UsageType.TotalMobilOilUsage)
                    {
                        // if mobil oil is present for the month
                        if (vehicle.VehicleItemDetail.Any(x => x.CreatedDate.Value.Month == month && x.CreatedDate.Value.Year == selectedYear &&
                        x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Vehicle &&
                                                                        x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.MobilOil))
                        {
                            // get sum of all mobil oil of all previous months if any
                            monthData = vehicle.VehicleItemDetail
                                               .Where(x => x.CreatedDate.Value.Year == selectedYear && x.CreatedDate.Value.Month < month
                                                      && x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Vehicle &&
                                                                        x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.MobilOil)
                                               .Select(x => x.StoreItemPurchase.Quantity)
                                               .DefaultIfEmpty(0).Sum() +
                                        vehicle.VehicleItemDetail.Where(x => x.CreatedDate.Value.Month == month
                                                                        && x.CreatedDate.Value.Year == selectedYear &&
                                                                        x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Vehicle &&
                                                                        x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.MobilOil)
                                                                 .Select(y => y.StoreItemPurchase.Quantity)
                                                                 .First();
                        }
                        else // if mobil oil is not present for the month then sum previous months
                        {
                            // get sum of all mobil oil purchase of all previous months if any
                            monthData = vehicle.VehicleItemDetail
                                               .Where(x => x.CreatedDate.Value.Year == selectedYear && x.CreatedDate.Value.Month < month
                                                     && x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Vehicle &&
                                                                        x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.MobilOil)
                                               .Select(x => x.StoreItemPurchase.Quantity).DefaultIfEmpty(0)
                                               .Sum();
                        }
                    }
                    else if (usageType == (int)UsageType.ActualFuelConsumptionRate)
                    {
                        // if fuel is present for the month
                        if (vehicle.VehicleItemDetail.Any(x => x.CreatedDate.Value.Month == month && x.CreatedDate.Value.Year == selectedYear &&
                            x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Vehicle &&
                                                                        x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.Fuel))
                        {

                            var mileageTravelledInaMonth = vehicle.VehicleMileageDetail.FirstOrDefault(x => x.MileageMonth.Month == month && x.MileageMonth.Year == selectedYear);
                            // get sum of all fuel purchase in a month
                            double? purchaseFuelInMonth = vehicle.VehicleItemDetail.Where(x => x.CreatedDate.Value.Month == month
                                                                                    && x.CreatedDate.Value.Year == selectedYear &&
                                                                                        x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Vehicle &&
                                                                        x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.Fuel)
                                                                                    .Select(y => y.StoreItemPurchase.Quantity)
                                                                                    .FirstOrDefault();

                            if (mileageTravelledInaMonth != null && purchaseFuelInMonth != null)
                            {
                                monthData = Math.Round((purchaseFuelInMonth.Value / mileageTravelledInaMonth.Mileage) * 100, 4);
                            }
                        }
                    }
                    else if (usageType == (int)UsageType.ActualMobilConsumptionRate)
                    {
                        // if fuel is present for the month
                        if (vehicle.VehicleItemDetail.Any(x => x.CreatedDate.Value.Month == month && x.CreatedDate.Value.Year == selectedYear &&
                            x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Vehicle &&
                                                                        x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.MobilOil))
                        {

                            var mileageTravelledInaMonth = vehicle.VehicleMileageDetail.FirstOrDefault(x => x.MileageMonth.Month == month && x.MileageMonth.Year == selectedYear);
                            // get sum of all fuel purchase in a month
                            double? purchaseFuelInMonth = vehicle.VehicleItemDetail.Where(x => x.CreatedDate.Value.Month == month
                                                                                    && x.CreatedDate.Value.Year == selectedYear &&
                                                                                        x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Vehicle &&
                                                                        x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.MobilOil)
                                                                                    .Select(y => y.StoreItemPurchase.Quantity)
                                                                                    .FirstOrDefault();

                            if (purchaseFuelInMonth != null && mileageTravelledInaMonth != null && mileageTravelledInaMonth.Mileage != 0)
                            {
                                monthData = Math.Round((purchaseFuelInMonth.Value / mileageTravelledInaMonth.Mileage) * 100, 4);
                            }
                        }
                    }
                    else if (usageType == (int)UsageType.FuelConsumptionDifference)
                    {
                        // if fuel is present for the month
                        if (vehicle.VehicleItemDetail.Any(x => x.CreatedDate.Value.Month == month && x.CreatedDate.Value.Year == selectedYear &&
                            x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Vehicle &&
                                                                        x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.Fuel))
                        {
                            double actualRate = 0;
                            var mileageTravelledInaMonth = vehicle.VehicleMileageDetail.FirstOrDefault(x => x.MileageMonth.Month == month && x.MileageMonth.Year == selectedYear);
                            // get sum of all fuel purchase in a month
                            double? purchaseFuelInMonth = vehicle.VehicleItemDetail.Where(x => x.CreatedDate.Value.Month == month
                                                                                    && x.CreatedDate.Value.Year == selectedYear &&
                                                                                        x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Vehicle &&
                                                                        x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.Fuel)
                                                                                    .Select(y => y.StoreItemPurchase.Quantity)
                                                                                    .FirstOrDefault();

                            if (purchaseFuelInMonth != null && mileageTravelledInaMonth != null && mileageTravelledInaMonth.Mileage != 0)
                            {
                                actualRate = (purchaseFuelInMonth.Value / mileageTravelledInaMonth.Mileage) * 100;
                                monthData = Math.Round(vehicle.StandardFuelConsumptionRate - actualRate, 4);
                            }
                        }
                    }
                    else if (usageType == (int)UsageType.MobilOilConsumptionDifference)
                    {
                        // if fuel is present for the month
                        if (vehicle.VehicleItemDetail.Any(x => x.CreatedDate.Value.Month == month && x.CreatedDate.Value.Year == selectedYear &&
                            x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Vehicle &&
                                                                        x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.MobilOil))
                        {
                            double actualMobilOilRate = 0;

                            var mileageTravelledInaMonth = vehicle.VehicleMileageDetail.FirstOrDefault(x => x.MileageMonth.Month == month && x.MileageMonth.Year == selectedYear);
                            // get sum of all fuel purchase in a month
                            double? purchaseFuelInMonth = vehicle.VehicleItemDetail.Where(x => x.CreatedDate.Value.Month == month
                                                                                    && x.CreatedDate.Value.Year == selectedYear &&
                                                                                        x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Vehicle &&
                                                                        x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.MobilOil)
                                                                                    .Select(y => y.StoreItemPurchase.Quantity)
                                                                                    .First();

                            if (purchaseFuelInMonth != null && mileageTravelledInaMonth != null && mileageTravelledInaMonth.Mileage > 0)
                            {
                                actualMobilOilRate = (purchaseFuelInMonth.Value / mileageTravelledInaMonth.Mileage) * 100;
                                monthData = Math.Round(vehicle.StandardMobilOilConsumptionRate - actualMobilOilRate, 4);
                            }
                        }
                    }
                    else if (usageType == (int)CostAnalysis.SparePartsTotalCost)
                    {
                        // if vehicle spare parts is present for the month
                        if (vehicle.VehicleItemDetail.Any(x => x.CreatedDate.Value.Month == month && x.CreatedDate.Value.Year == selectedYear &&
                           x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Vehicle &&
                                                                        x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.SpareParts))
                        {
                            // get sum of all vehicle spare parts of all previous months if any
                            monthData = vehicle.VehicleItemDetail
                                               .Where(x => x.CreatedDate.Value.Year == selectedYear && x.CreatedDate.Value.Month < month
                                                      && x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Vehicle &&
                                                                        x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.SpareParts)
                                               .Select(x => x.StoreItemPurchase.Quantity * x.StoreItemPurchase.UnitCost)
                                               .DefaultIfEmpty(0).Sum() +
                                        vehicle.VehicleItemDetail.Where(x => x.CreatedDate.Value.Month == month
                                                                        && x.CreatedDate.Value.Year == selectedYear &&
                                                                        x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Vehicle &&
                                                                        x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.SpareParts)
                                                                 .Select(y => y.StoreItemPurchase.UnitCost * y.StoreItemPurchase.Quantity)
                                                                 .First();
                        }
                        else // if vehicle spare parts is not present for the month then sum previous months mileages
                        {
                            // get sum of all vehicle spare parts purchase of all previous months if any
                            monthData = vehicle.VehicleItemDetail
                                               .Where(x => x.CreatedDate.Value.Year == selectedYear && x.CreatedDate.Value.Month < month
                                                     && x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Vehicle &&
                                                                        x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.SpareParts)
                                               .Select(x => x.StoreItemPurchase.Quantity * x.StoreItemPurchase.UnitCost).DefaultIfEmpty(0)
                                               .Sum();
                        }
                    }
                    else if (usageType == (int)CostAnalysis.ServiceAndMaintenanceTotalCost)
                    {
                        // if vehicle maintenance service is present for the month
                        if (vehicle.VehicleItemDetail.Any(x => x.CreatedDate.Value.Month == month && x.CreatedDate.Value.Year == selectedYear &&
                           x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Vehicle &&
                                                                        x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.MaintenanceService))
                        {
                            // get sum of all vehicle maintenance service of all previous months if any
                            monthData = vehicle.VehicleItemDetail
                                               .Where(x => x.CreatedDate.Value.Year == selectedYear && x.CreatedDate.Value.Month < month
                                                      && x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Vehicle &&
                                                                        x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.MaintenanceService)
                                               .Select(x => x.StoreItemPurchase.Quantity * x.StoreItemPurchase.UnitCost)
                                               .DefaultIfEmpty(0).Sum() +
                                        vehicle.VehicleItemDetail.Where(x => x.CreatedDate.Value.Month == month
                                                                        && x.CreatedDate.Value.Year == selectedYear &&
                                                                        x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Vehicle &&
                                                                        x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.MaintenanceService)
                                                                 .Select(y => y.StoreItemPurchase.UnitCost * y.StoreItemPurchase.Quantity)
                                                                 .First();
                        }
                        else // if vehicle maintenance service is not present for the month then sum previous months mileages
                        {
                            // get sum of all vehicle maintenance service purchase of all previous months if any
                            monthData = vehicle.VehicleItemDetail
                                               .Where(x => x.CreatedDate.Value.Year == selectedYear && x.CreatedDate.Value.Month < month
                                                     && x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Vehicle &&
                                                                        x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.MaintenanceService)
                                               .Select(x => x.StoreItemPurchase.Quantity * x.StoreItemPurchase.UnitCost).DefaultIfEmpty(0)
                                               .Sum();
                        }

                    }
                    else if (usageType == (int)UsageType.MobilOilChangeRotation)
                    {
                        // if mobil oil is present for the month
                        if (vehicle.VehicleItemDetail.Any(x => x.CreatedDate.Value.Month == month && x.CreatedDate.Value.Year == selectedYear &&
                            x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Vehicle &&
                                                                        x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.MobilOil))
                        {
                            // get sum of all mobil oil of all previous months if any
                            monthData = vehicle.VehicleItemDetail.Where(x => x.CreatedDate.Value.Month == month
                                                                        && x.CreatedDate.Value.Year == selectedYear &&
                                                                        x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Vehicle &&
                                                                        x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.MobilOil).Count();
                        }
                    }
                    else if (usageType == (int)UsageType.RemainingKmForMobilOilChange)
                    {
                        // double mobilOilPerKm = Math.Round(vehicle.StandardMobilOilConsumptionRate / 100, 4);

                        if (vehicle.VehicleItemDetail.Any(x => x.CreatedDate.Value.Month == month && x.CreatedDate.Value.Year == selectedYear &&
                             x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Vehicle &&
                                                                        x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.MobilOil))
                        {
                            double quantity = vehicle.VehicleItemDetail
                                               .Where(x => x.CreatedDate.Value.Year == selectedYear && x.CreatedDate.Value.Month < month
                                                      && x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Vehicle &&
                                                                        x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.MobilOil)
                                               .Select(x => x.StoreItemPurchase.Quantity)
                                               .DefaultIfEmpty(0).Sum();
                            if (quantity != 0)
                            {
                                monthData = (100 / vehicle.StandardMobilOilConsumptionRate) * quantity; // (100Km/stdmobiloilconsumptionrate(per 100 km))* total quantity of purchased mobil oil
                            }
                        }
                        else
                        {
                            TotalMobilOilAndMonth data = new TotalMobilOilAndMonth();
                            data = GetTotalKmTravelledTillMobilOilChange(month, selectedYear, vehicle);

                            double totalDistanceTravelled = vehicle.VehicleMileageDetail
                                                            .Where(x => x.CreatedDate.Value.Year == selectedYear && x.CreatedDate.Value.Year == data.Year && x.MileageMonth.Month > data.Month
                                                            && x.MileageMonth.Month <= month)
                                                            .Select(x => x.Mileage).DefaultIfEmpty(0).Sum();


                            double distanceVehicleCanTravel = Math.Round((100 / vehicle.StandardMobilOilConsumptionRate) * data.TotalMobilOil, 4);

                            monthData = distanceVehicleCanTravel - totalDistanceTravelled;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return monthData;
        }

        private TotalMobilOilAndMonth GetTotalKmTravelledTillMobilOilChange(int month, int year, MonthlyBreakDownModel vehicle)
        {
            TotalMobilOilAndMonth mobilOilAndMonth = new TotalMobilOilAndMonth();
            mobilOilAndMonth.Year = year;
            mobilOilAndMonth.Month = month;
            mobilOilAndMonth.TotalMobilOil = 0;

            var item = vehicle.VehicleItemDetail.FirstOrDefault(x => x.CreatedDate.Value.Month == month && x.CreatedDate.Value.Year == year &&
                             x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Vehicle &&
                                                                        x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.MobilOil);

            if (item == null && month == (int)Month.January && vehicle.CreatedDate.Date.Year < year)
            {
                mobilOilAndMonth = GetTotalKmTravelledTillMobilOilChange((int)Month.December, year - 1, vehicle);
            }
            else if (item == null && vehicle.CreatedDate.Date.Year == year && month != (int)Month.January)
            {
                mobilOilAndMonth = GetTotalKmTravelledTillMobilOilChange(month - 1, year, vehicle);
            }

            if (item != null)
            {
                mobilOilAndMonth.TotalMobilOil = item.StoreItemPurchase.Quantity;
                mobilOilAndMonth.Month = month;
                mobilOilAndMonth.Year = year;
            }

            return mobilOilAndMonth;
        }

        private void SwitchCaseStatement(MonthlyBreakDownModel vehicle, int selectedYear, int usageType, out UsageAnalysisBreakDown usageBreakDown, out CostAnalysisBreakDown costBreakDown)
        {
            usageBreakDown = new UsageAnalysisBreakDown();
            costBreakDown = new CostAnalysisBreakDown();

            for (int j = 1; j <= 12; j++)
            {
                double monthData = GetMonthlyVehicleDetail(vehicle, selectedYear, j, usageType);

                UsageType usage = (UsageType)usageType;
                CostAnalysis cost = (CostAnalysis)usageType;

                usageBreakDown.Header = usage.GetDescription();
                costBreakDown.Header = cost.GetDescription();

                switch (j)
                {
                    case (int)Month.January:
                        usageBreakDown.January = monthData;
                        costBreakDown.January = monthData;
                        break;

                    case (int)Month.February:
                        usageBreakDown.February = monthData;
                        costBreakDown.February = monthData;
                        break;

                    case (int)Month.March:
                        usageBreakDown.March = monthData;
                        costBreakDown.March = monthData;
                        break;

                    case (int)Month.April:
                        usageBreakDown.April = monthData;
                        costBreakDown.April = monthData;
                        break;

                    case (int)Month.May:
                        usageBreakDown.May = monthData;
                        costBreakDown.May = monthData;
                        break;

                    case (int)Month.June:
                        usageBreakDown.June = monthData;
                        costBreakDown.June = monthData;
                        break;

                    case (int)Month.July:
                        usageBreakDown.July = monthData;
                        costBreakDown.July = monthData;
                        break;

                    case (int)Month.August:
                        usageBreakDown.August = monthData;
                        costBreakDown.August = monthData;
                        break;

                    case (int)Month.September:
                        usageBreakDown.September = monthData;
                        costBreakDown.September = monthData;
                        break;

                    case (int)Month.October:
                        usageBreakDown.October = monthData;
                        costBreakDown.October = monthData;
                        break;

                    case (int)Month.November:
                        usageBreakDown.November = monthData;
                        costBreakDown.November = monthData;
                        break;

                    case (int)Month.December:
                        usageBreakDown.December = monthData;
                        costBreakDown.December = monthData;
                        break;
                }
            }
        }
    }
}
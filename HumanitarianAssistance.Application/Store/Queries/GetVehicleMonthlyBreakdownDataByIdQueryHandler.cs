using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Store.Models;
using HumanitarianAssistance.Common.Enums;
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
                
                MonthlyBreakDownModel vehicle = await _dbContext.PurchasedVehicleDetail
                                           .Include(x => x.EmployeeDetail)
                                           .Include(x => x.StoreItemPurchase)
                                           .Include(x => x.VehicleMileageDetail)
                                           .Include(x=> x.VehicleItemDetail)
                                           .ThenInclude(x=> x.StoreItemPurchase)
                                           .ThenInclude(x=> x.StoreInventoryItem)
                                           .Where(x => x.IsDeleted == false && x.Id == request.VehicleId)
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
                                                     VehicleMileageDetail = x.VehicleMileageDetail.Where(y => y.IsDeleted == false && y.CreatedDate.Value.Date.Year == request.SelectedYear).ToList()
                                                 })
                                           .FirstOrDefaultAsync();

                if (vehicle != null)
                {
                    model.IncurredMileage = vehicle.IncurredMileage;
                    model.StandardFuelConsumptionRate = vehicle.StandardFuelConsumptionRate;
                    model.StandardMobilOilConsumptionRate = vehicle.StandardMobilOilConsumptionRate;
                    model.StartingMileage = vehicle.StartingMileage;
                    model.StartingCost = vehicle.OriginalCost;

                    // Array values = Enum.GetValues(typeof(UsageType));

                    // foreach (UsageType val in values)
                    // {
                    //     MonthlyBreakDownData data = new MonthlyBreakDownData();
                    //     data.Header = Enum.GetName(typeof(UsageType), val);

                    //     for (int j = 1; j <= 12; j++)
                    //     {
                    //         double monthData = GetMonthlyVehicleDetail(vehicle, request.SelectedYear, j, (int)val);

                    //         switch(j)
                    //         {
                    //             case (int)Month.January:
                    //             data.January= monthData;
                    //             break;

                    //             case (int)Month.February:
                    //             data.February= monthData;
                    //             break;

                    //             case (int)Month.March:
                    //             data.March= monthData;
                    //             break;

                    //             case (int)Month.April:
                    //             data.April= monthData;
                    //             break;

                    //             case (int)Month.May:
                    //             data.May= monthData;
                    //             break;

                    //             case (int)Month.June:
                    //             data.June= monthData;
                    //             break;

                    //             case (int)Month.July:
                    //             data.July= monthData;
                    //             break;

                    //             case (int)Month.August:
                    //             data.January= monthData;
                    //             break;

                    //             case (int)Month.September:
                    //             data.September= monthData;
                    //             break;

                    //             case (int)Month.October:
                    //             data.October= monthData;
                    //             break;

                    //             case (int)Month.November:
                    //             data.November= monthData;
                    //             break;

                    //             case (int)Month.December:
                    //             data.December= monthData;
                    //             break;
                    //         }
                    //     }

                    //     model.MonthlyBreakDownList.Add(data);
                    // }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return model;
        }


        public double GetMonthlyVehicleDetail(MonthlyBreakDownModel vehicle, int selectedYear, int month, int usageType)
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
                            // get sum of all mileages of all previous months if any
                            monthData = vehicle.VehicleMileageDetail
                                                            .Where(x => x.CreatedDate.Value.Year == selectedYear && x.MileageMonth.Month < month)
                                                            .Select(x => x.Mileage).DefaultIfEmpty(0).Sum() + vehicle.VehicleMileageDetail.First(x => x.MileageMonth.Month == month
                                                                                                                 && x.CreatedDate.Value.Year == selectedYear).Mileage;
                        }
                        else // if mileage is not present for the month then sum previous months mileages
                        {
                            // get sum of all mileages of all previous months if any
                            monthData = vehicle.VehicleMileageDetail
                                                            .Where(x => x.CreatedDate.Value.Year == selectedYear && x.MileageMonth.Month < month)
                                                            .Select(x => x.Mileage).DefaultIfEmpty(0).Sum();
                        }
                    }
                    else if (usageType == (int)UsageType.FuelTotalCost)
                    {
                        // if Fuel is present for the month
                        if (vehicle.VehicleItemDetail.Any(x => x.CreatedDate.Value.Month == month && x.CreatedDate.Value.Year == selectedYear &&
                           x.StoreItemPurchase.StoreInventoryItem.ItemId == (int)TransportItem.VehicleFuel))
                        {
                            // get sum of all fuel of all previous months if any
                            monthData = vehicle.VehicleItemDetail
                                               .Where(x => x.CreatedDate.Value.Year == selectedYear && x.CreatedDate.Value.Month < month
                                                      && x.StoreItemPurchase.StoreInventoryItem.ItemId == (int)TransportItem.VehicleFuel)
                                               .Select(x => x.StoreItemPurchase.Quantity * x.StoreItemPurchase.UnitCost)
                                               .DefaultIfEmpty(0).Sum() +
                                        vehicle.VehicleItemDetail.Where(x => x.CreatedDate.Value.Month == month
                                                                        && x.CreatedDate.Value.Year == selectedYear &&
                                                                        x.StoreItemPurchase.StoreInventoryItem.ItemId == (int)TransportItem.VehicleFuel)
                                                                 .Select(y => y.StoreItemPurchase.UnitCost * y.StoreItemPurchase.Quantity)
                                                                 .First();
                        }
                        else // if fuel is not present for the month then sum previous months mileages
                        {
                            // get sum of all fuel purchase of all previous months if any
                            monthData = vehicle.VehicleItemDetail
                                               .Where(x => x.CreatedDate.Value.Year == selectedYear && x.CreatedDate.Value.Month < month
                                                     && x.StoreItemPurchase.StoreInventoryItem.ItemId == (int)TransportItem.VehicleFuel)
                                               .Select(x => x.StoreItemPurchase.Quantity * x.StoreItemPurchase.UnitCost).DefaultIfEmpty(0)
                                               .Sum();
                        }
                    }
                    else if (usageType == (int)UsageType.MobilOilTotalCost)
                    {
                        // if mobil oil is present for the month
                        if (vehicle.VehicleItemDetail.Any(x => x.CreatedDate.Value.Month == month && x.CreatedDate.Value.Year == selectedYear &&
                            x.StoreItemPurchase.StoreInventoryItem.ItemId == (int)TransportItem.VehicleMobilOil))
                        {
                            // get sum of all mobil oil of all previous months if any
                            monthData = vehicle.VehicleItemDetail
                                               .Where(x => x.CreatedDate.Value.Year == selectedYear && x.CreatedDate.Value.Month < month
                                                      && x.StoreItemPurchase.StoreInventoryItem.ItemId == (int)TransportItem.VehicleMobilOil)
                                               .Select(x => x.StoreItemPurchase.Quantity * x.StoreItemPurchase.UnitCost)
                                               .DefaultIfEmpty(0).Sum() +
                                        vehicle.VehicleItemDetail.Where(x => x.CreatedDate.Value.Month == month
                                                                        && x.CreatedDate.Value.Year == selectedYear &&
                                                                        x.StoreItemPurchase.StoreInventoryItem.ItemId == (int)TransportItem.VehicleMobilOil)
                                                                 .Select(y => y.StoreItemPurchase.UnitCost * y.StoreItemPurchase.Quantity)
                                                                 .First();
                        }
                        else // if mobil oil is not present for the month then sum previous months
                        {
                            // get sum of all mobil oil purchase of all previous months if any
                            monthData = vehicle.VehicleItemDetail
                                               .Where(x => x.CreatedDate.Value.Year == selectedYear && x.CreatedDate.Value.Month < month
                                                     && x.StoreItemPurchase.StoreInventoryItem.ItemId == (int)TransportItem.VehicleMobilOil)
                                               .Select(x => x.StoreItemPurchase.Quantity * x.StoreItemPurchase.UnitCost).DefaultIfEmpty(0)
                                               .Sum();
                        }
                    }
                    else if (usageType == (int)UsageType.TotalFuelUsage)
                    {
                        // if fuel is present for the month
                        if (vehicle.VehicleItemDetail.Any(x => x.CreatedDate.Value.Month == month && x.CreatedDate.Value.Year == selectedYear &&
                            x.StoreItemPurchase.StoreInventoryItem.ItemId == (int)TransportItem.VehicleFuel))
                        {
                            // get sum of all fuel of all previous months if any
                            monthData = vehicle.VehicleItemDetail
                                               .Where(x => x.CreatedDate.Value.Year == selectedYear && x.CreatedDate.Value.Month < month
                                                      && x.StoreItemPurchase.StoreInventoryItem.ItemId == (int)TransportItem.VehicleFuel)
                                               .Select(x => x.StoreItemPurchase.Quantity)
                                               .DefaultIfEmpty(0).Sum() +
                                        vehicle.VehicleItemDetail.Where(x => x.CreatedDate.Value.Month == month
                                                                        && x.CreatedDate.Value.Year == selectedYear &&
                                                                        x.StoreItemPurchase.StoreInventoryItem.ItemId == (int)TransportItem.VehicleFuel)
                                                                 .Select(y => y.StoreItemPurchase.Quantity)
                                                                 .First();
                        }
                        else // if fuel is not present for the month then sum previous months
                        {
                            // get sum of all fuel purchase of all previous months if any
                            monthData = vehicle.VehicleItemDetail
                                               .Where(x => x.CreatedDate.Value.Year == selectedYear && x.CreatedDate.Value.Month < month
                                                     && x.StoreItemPurchase.StoreInventoryItem.ItemId == (int)TransportItem.VehicleFuel)
                                               .Select(x => x.StoreItemPurchase.Quantity).DefaultIfEmpty(0)
                                               .Sum();
                        }
                    }
                    else if (usageType == (int)UsageType.TotalMobilOilUsage)
                    {
                        // if mobil oil is present for the month
                        if (vehicle.VehicleItemDetail.Any(x => x.CreatedDate.Value.Month == month && x.CreatedDate.Value.Year == selectedYear &&
                            x.StoreItemPurchase.StoreInventoryItem.ItemId == (int)TransportItem.VehicleMobilOil))
                        {
                            // get sum of all mobil oil of all previous months if any
                            monthData = vehicle.VehicleItemDetail
                                               .Where(x => x.CreatedDate.Value.Year == selectedYear && x.CreatedDate.Value.Month < month
                                                      && x.StoreItemPurchase.StoreInventoryItem.ItemId == (int)TransportItem.VehicleMobilOil)
                                               .Select(x => x.StoreItemPurchase.Quantity)
                                               .DefaultIfEmpty(0).Sum() +
                                        vehicle.VehicleItemDetail.Where(x => x.CreatedDate.Value.Month == month
                                                                        && x.CreatedDate.Value.Year == selectedYear &&
                                                                        x.StoreItemPurchase.StoreInventoryItem.ItemId == (int)TransportItem.VehicleMobilOil)
                                                                 .Select(y => y.StoreItemPurchase.Quantity)
                                                                 .First();
                        }
                        else // if mobil oil is not present for the month then sum previous months
                        {
                            // get sum of all mobil oil purchase of all previous months if any
                            monthData = vehicle.VehicleItemDetail
                                               .Where(x => x.CreatedDate.Value.Year == selectedYear && x.CreatedDate.Value.Month < month
                                                     && x.StoreItemPurchase.StoreInventoryItem.ItemId == (int)TransportItem.VehicleMobilOil)
                                               .Select(x => x.StoreItemPurchase.Quantity).DefaultIfEmpty(0)
                                               .Sum();
                        }
                    }
                    else if (usageType == (int)UsageType.ActualFuelConsumptionRate)
                    {
                        // if fuel is present for the month
                        if (vehicle.VehicleItemDetail.Any(x => x.CreatedDate.Value.Month == month && x.CreatedDate.Value.Year == selectedYear &&
                            x.StoreItemPurchase.StoreInventoryItem.ItemId == (int)TransportItem.VehicleFuel))
                        {

                            double mileageTravelledInaMonth = vehicle.VehicleMileageDetail.First(x => x.MileageMonth.Month == month && x.MileageMonth.Year == selectedYear).Mileage;
                            // get sum of all fuel purchase in a month
                            double purchaseFuelInMonth = vehicle.VehicleItemDetail.Where(x => x.CreatedDate.Value.Month == month
                                                                                    && x.CreatedDate.Value.Year == selectedYear &&
                                                                                        x.StoreItemPurchase.StoreInventoryItem.ItemId == (int)TransportItem.VehicleMobilOil)
                                                                                    .Select(y => y.StoreItemPurchase.Quantity)
                                                                                    .First();

                            if (purchaseFuelInMonth > 0)
                            {
                                monthData = (purchaseFuelInMonth / mileageTravelledInaMonth) * 100;
                            }
                        }
                    }
                    else if (usageType == (int)UsageType.ActualMobilConsumptionRate)
                    {
                        // if fuel is present for the month
                        if (vehicle.VehicleItemDetail.Any(x => x.CreatedDate.Value.Month == month && x.CreatedDate.Value.Year == selectedYear &&
                            x.StoreItemPurchase.StoreInventoryItem.ItemId == (int)TransportItem.VehicleMobilOil))
                        {

                            double mileageTravelledInaMonth = vehicle.VehicleMileageDetail.First(x => x.MileageMonth.Month == month && x.MileageMonth.Year == selectedYear).Mileage;
                            // get sum of all fuel purchase in a month
                            double purchaseFuelInMonth = vehicle.VehicleItemDetail.Where(x => x.CreatedDate.Value.Month == month
                                                                                    && x.CreatedDate.Value.Year == selectedYear &&
                                                                                        x.StoreItemPurchase.StoreInventoryItem.ItemId == (int)TransportItem.VehicleMobilOil)
                                                                                    .Select(y => y.StoreItemPurchase.Quantity)
                                                                                    .First();

                            if (purchaseFuelInMonth > 0)
                            {
                                monthData = (purchaseFuelInMonth / mileageTravelledInaMonth) * 100;
                            }
                        }
                    }
                    else if (usageType == (int)UsageType.ActualMobilConsumptionRate)
                    {
                        // if fuel is present for the month
                        if (vehicle.VehicleItemDetail.Any(x => x.CreatedDate.Value.Month == month && x.CreatedDate.Value.Year == selectedYear &&
                            x.StoreItemPurchase.StoreInventoryItem.ItemId == (int)TransportItem.VehicleMobilOil))
                        {

                            double mileageTravelledInaMonth = vehicle.VehicleMileageDetail.First(x => x.MileageMonth.Month == month && x.MileageMonth.Year == selectedYear).Mileage;
                            // get sum of all fuel purchase in a month
                            double purchaseFuelInMonth = vehicle.VehicleItemDetail.Where(x => x.CreatedDate.Value.Month == month
                                                                                    && x.CreatedDate.Value.Year == selectedYear &&
                                                                                        x.StoreItemPurchase.StoreInventoryItem.ItemId == (int)TransportItem.VehicleMobilOil)
                                                                                    .Select(y => y.StoreItemPurchase.Quantity)
                                                                                    .First();

                            if (purchaseFuelInMonth > 0)
                            {
                                monthData = (purchaseFuelInMonth / mileageTravelledInaMonth) * 100;
                            }
                        }
                    }
                    else if (usageType == (int)UsageType.FuelConsumptionDifference)
                    {
                        // if fuel is present for the month
                        if (vehicle.VehicleItemDetail.Any(x => x.CreatedDate.Value.Month == month && x.CreatedDate.Value.Year == selectedYear &&
                            x.StoreItemPurchase.StoreInventoryItem.ItemId == (int)TransportItem.VehicleMobilOil))
                        {
                            double actualRate = 0;
                            double mileageTravelledInaMonth = vehicle.VehicleMileageDetail.First(x => x.MileageMonth.Month == month && x.MileageMonth.Year == selectedYear).Mileage;
                            // get sum of all fuel purchase in a month
                            double purchaseFuelInMonth = vehicle.VehicleItemDetail.Where(x => x.CreatedDate.Value.Month == month
                                                                                    && x.CreatedDate.Value.Year == selectedYear &&
                                                                                        x.StoreItemPurchase.StoreInventoryItem.ItemId == (int)TransportItem.VehicleMobilOil)
                                                                                    .Select(y => y.StoreItemPurchase.Quantity)
                                                                                    .First();

                            if (purchaseFuelInMonth > 0)
                            {
                                actualRate = (purchaseFuelInMonth / mileageTravelledInaMonth) * 100;
                                monthData = vehicle.StandardFuelConsumptionRate - actualRate;
                            }
                        }
                    }
                    else if (usageType == (int)UsageType.MobilOilConsumptionDifference)
                    {
                        // if fuel is present for the month
                        if (vehicle.VehicleItemDetail.Any(x => x.CreatedDate.Value.Month == month && x.CreatedDate.Value.Year == selectedYear &&
                            x.StoreItemPurchase.StoreInventoryItem.ItemId == (int)TransportItem.VehicleMobilOil))
                        {
                            double actualMobilOilRate = 0;

                            double mileageTravelledInaMonth = vehicle.VehicleMileageDetail.First(x => x.MileageMonth.Month == month && x.MileageMonth.Year == selectedYear).Mileage;
                            // get sum of all fuel purchase in a month
                            double purchaseFuelInMonth = vehicle.VehicleItemDetail.Where(x => x.CreatedDate.Value.Month == month
                                                                                    && x.CreatedDate.Value.Year == selectedYear &&
                                                                                        x.StoreItemPurchase.StoreInventoryItem.ItemId == (int)TransportItem.VehicleMobilOil)
                                                                                    .Select(y => y.StoreItemPurchase.Quantity)
                                                                                    .First();

                            if (purchaseFuelInMonth > 0)
                            {
                                actualMobilOilRate = (purchaseFuelInMonth / mileageTravelledInaMonth) * 100;
                                monthData = vehicle.StandardMobilOilConsumptionRate - actualMobilOilRate;
                            }
                        }
                    }
                    else if (usageType == (int)UsageType.SparePartsTotalCost)
                    {
                        // if vehicle spare parts is present for the month
                        if (vehicle.VehicleItemDetail.Any(x => x.CreatedDate.Value.Month == month && x.CreatedDate.Value.Year == selectedYear &&
                           x.StoreItemPurchase.StoreInventoryItem.ItemId == (int)TransportItem.VehicleSpareParts))
                        {
                            // get sum of all vehicle spare parts of all previous months if any
                            monthData = vehicle.VehicleItemDetail
                                               .Where(x => x.CreatedDate.Value.Year == selectedYear && x.CreatedDate.Value.Month < month
                                                      && x.StoreItemPurchase.StoreInventoryItem.ItemId == (int)TransportItem.VehicleSpareParts)
                                               .Select(x => x.StoreItemPurchase.Quantity * x.StoreItemPurchase.UnitCost)
                                               .DefaultIfEmpty(0).Sum() +
                                        vehicle.VehicleItemDetail.Where(x => x.CreatedDate.Value.Month == month
                                                                        && x.CreatedDate.Value.Year == selectedYear &&
                                                                        x.StoreItemPurchase.StoreInventoryItem.ItemId == (int)TransportItem.VehicleSpareParts)
                                                                 .Select(y => y.StoreItemPurchase.UnitCost * y.StoreItemPurchase.Quantity)
                                                                 .First();
                        }
                        else // if vehicle spare parts is not present for the month then sum previous months mileages
                        {
                            // get sum of all vehicle spare parts purchase of all previous months if any
                            monthData = vehicle.VehicleItemDetail
                                               .Where(x => x.CreatedDate.Value.Year == selectedYear && x.CreatedDate.Value.Month < month
                                                     && x.StoreItemPurchase.StoreInventoryItem.ItemId == (int)TransportItem.VehicleSpareParts)
                                               .Select(x => x.StoreItemPurchase.Quantity * x.StoreItemPurchase.UnitCost).DefaultIfEmpty(0)
                                               .Sum();
                        }
                    }
                    else if (usageType == (int)UsageType.ServiceAndMaintenanceTotalCost)
                    {
                        // if vehicle maintenance service is present for the month
                        if (vehicle.VehicleItemDetail.Any(x => x.CreatedDate.Value.Month == month && x.CreatedDate.Value.Year == selectedYear &&
                           x.StoreItemPurchase.StoreInventoryItem.ItemId == (int)TransportItem.VehicleMaintenanceService))
                        {
                            // get sum of all vehicle maintenance service of all previous months if any
                            monthData = vehicle.VehicleItemDetail
                                               .Where(x => x.CreatedDate.Value.Year == selectedYear && x.CreatedDate.Value.Month < month
                                                      && x.StoreItemPurchase.StoreInventoryItem.ItemId == (int)TransportItem.VehicleMaintenanceService)
                                               .Select(x => x.StoreItemPurchase.Quantity * x.StoreItemPurchase.UnitCost)
                                               .DefaultIfEmpty(0).Sum() +
                                        vehicle.VehicleItemDetail.Where(x => x.CreatedDate.Value.Month == month
                                                                        && x.CreatedDate.Value.Year == selectedYear &&
                                                                        x.StoreItemPurchase.StoreInventoryItem.ItemId == (int)TransportItem.VehicleMaintenanceService)
                                                                 .Select(y => y.StoreItemPurchase.UnitCost * y.StoreItemPurchase.Quantity)
                                                                 .First();
                        }
                        else // if vehicle maintenance service is not present for the month then sum previous months mileages
                        {
                            // get sum of all vehicle maintenance service purchase of all previous months if any
                            monthData = vehicle.VehicleItemDetail
                                               .Where(x => x.CreatedDate.Value.Year == selectedYear && x.CreatedDate.Value.Month < month
                                                     && x.StoreItemPurchase.StoreInventoryItem.ItemId == (int)TransportItem.VehicleMaintenanceService)
                                               .Select(x => x.StoreItemPurchase.Quantity * x.StoreItemPurchase.UnitCost).DefaultIfEmpty(0)
                                               .Sum();
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
    }
}
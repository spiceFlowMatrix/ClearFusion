using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Store.Models;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Common.Helpers;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetGeneratorMonthlyBreakdownDataByIdQueryHandler : IRequestHandler<GetGeneratorMonthlyBreakdownDataByIdQuery, MonthlyBreakdownDataModel>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetGeneratorMonthlyBreakdownDataByIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<MonthlyBreakdownDataModel> Handle(GetGeneratorMonthlyBreakdownDataByIdQuery request, CancellationToken cancellationToken)
        {
            MonthlyBreakdownDataModel model = new MonthlyBreakdownDataModel();

            try
            {

                var generatorData = await _dbContext.PurchasedGeneratorDetail
                                           .Include(x => x.StoreItemPurchase)
                                           .Include(x => x.GeneratorUsageHourList)
                                           .Include(x => x.GeneratorItemDetail)
                                           .ThenInclude(x => x.StoreItemPurchase)
                                           .ThenInclude(x => x.StoreInventoryItem)
                                           .ThenInclude(x => x.StoreItemGroup)
                                           .Where(x => x.IsDeleted == false && x.Id == request.GeneratorId && x.CreatedDate.Value.Year == request.SelectedYear)
                                           .ToListAsync();

                var generator = generatorData.Select(x => new MonthlyBreakDownModel
                {
                    CreatedDate = x.CreatedDate.Value,
                    GeneratorId = x.Id,
                    Voltage = x.Voltage,
                    StandardFuelConsumptionRate = x.FuelConsumptionRate,
                    StandardMobilOilConsumptionRate = x.MobilOilConsumptionRate,
                    StartingUsage = x.StartingUsage,
                    IncurredUsage = x.IncurredUsage,
                    ModelYear = x.ModelYear,
                    OfficeId = x.OfficeId,
                    OriginalCost = x.StoreItemPurchase.UnitCost,
                    PurchaseName = $"{x.StoreItemPurchase.PurchaseName} - {x.StoreItemPurchase.PurchaseDate.ToShortDateString()}-{x.StoreItemPurchase.PurchaseId}",
                    PurchaseId = x.StoreItemPurchase.PurchaseId,
                    OfficeName = x.OfficeId != 0 ? _dbContext.OfficeDetail.FirstOrDefault(y => y.IsDeleted == false && y.OfficeId == x.OfficeId).OfficeName : null,
                    GeneratorItemDetail = x.GeneratorItemDetail.Where(y => y.IsDeleted == false && y.CreatedDate.Value.Date.Year == request.SelectedYear).ToList(),
                    GeneratorUsageHourDetail = x.GeneratorUsageHourList.Where(y => y.IsDeleted == false && y.CreatedDate.Value.Date.Year >= (request.SelectedYear - 1)).ToList()
                }).FirstOrDefault();

                if (generator != null)
                {
                    model.IncurredUsage = generator.IncurredUsage;
                    model.StandardFuelConsumptionRate = generator.StandardFuelConsumptionRate;
                    model.StandardMobilOilConsumptionRate = generator.StandardMobilOilConsumptionRate;
                    model.StartingUsage = generator.StartingUsage;
                    model.StartingCost = generator.OriginalCost;

                    Array UsageTypeValues = Enum.GetValues(typeof(UsageType));

                    foreach (UsageType val in UsageTypeValues)
                    {
                        // Skip Current mileage for generator
                        if (val != UsageType.CurrentMileage)
                        {
                            UsageAnalysisBreakDown Usagedata;
                            CostAnalysisBreakDown Costdata;
                            SwitchCaseStatement(generator, request.SelectedYear, (int)val, out Usagedata, out Costdata);
                            model.UsageAnalysisBreakDownList.Add(Usagedata);
                        }
                    }

                    Array costAnalysisValues = Enum.GetValues(typeof(CostAnalysis));

                    foreach (CostAnalysis val in costAnalysisValues)
                    {
                        UsageAnalysisBreakDown Usagedata;
                        CostAnalysisBreakDown Costdata;
                        SwitchCaseStatement(generator, request.SelectedYear, (int)val, out Usagedata, out Costdata);
                        model.CostAnalysisBreakDownList.Add(Costdata);
                    }

                    //calculate total cost
                    CostAnalysisBreakDown totalCost = new CostAnalysisBreakDown();

                    totalCost.Header = "Total Cost";
                    totalCost.January = model.CostAnalysisBreakDownList.Sum(x => x.January) + (generator.CreatedDate.Month <= (int)Month.January ? generator.OriginalCost : 0);
                    totalCost.February = model.CostAnalysisBreakDownList.Sum(x => x.February) + (generator.CreatedDate.Month <= (int)Month.February ? generator.OriginalCost : 0);
                    totalCost.March = model.CostAnalysisBreakDownList.Sum(x => x.March) + (generator.CreatedDate.Month <= (int)Month.March ? generator.OriginalCost : 0);
                    totalCost.April = model.CostAnalysisBreakDownList.Sum(x => x.April) + (generator.CreatedDate.Month <= (int)Month.April ? generator.OriginalCost : 0);
                    totalCost.May = model.CostAnalysisBreakDownList.Sum(x => x.May) + (generator.CreatedDate.Month <= (int)Month.May ? generator.OriginalCost : 0);
                    totalCost.June = model.CostAnalysisBreakDownList.Sum(x => x.June) + (generator.CreatedDate.Month <= (int)Month.June ? generator.OriginalCost : 0);
                    totalCost.July = model.CostAnalysisBreakDownList.Sum(x => x.July) + (generator.CreatedDate.Month <= (int)Month.July ? generator.OriginalCost : 0);
                    totalCost.August = model.CostAnalysisBreakDownList.Sum(x => x.August) + (generator.CreatedDate.Month <= (int)Month.August ? generator.OriginalCost : 0);
                    totalCost.September = model.CostAnalysisBreakDownList.Sum(x => x.September) + (generator.CreatedDate.Month <= (int)Month.September ? generator.OriginalCost : 0);
                    totalCost.October = model.CostAnalysisBreakDownList.Sum(x => x.October) + (generator.CreatedDate.Month <= (int)Month.October ? generator.OriginalCost : 0);
                    totalCost.November = model.CostAnalysisBreakDownList.Sum(x => x.November) + (generator.CreatedDate.Month <= (int)Month.November ? generator.OriginalCost : 0);
                    totalCost.December = model.CostAnalysisBreakDownList.Sum(x => x.December) + (generator.CreatedDate.Month <= (int)Month.December ? generator.OriginalCost : 0);

                    model.CostAnalysisBreakDownList.Add(totalCost);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return model;
        }

        private double GetMonthlyGeneratorDetail(MonthlyBreakDownModel generator, int selectedYear, int month, int usageType)
        {
            double monthData = 0;

            try
            {
                if (generator.CreatedDate.Year <= selectedYear)
                {
                    if (usageType == (int)UsageType.CurrentUsage)
                    {
                        // if Generator Hour is present for the month
                        if (generator.GeneratorUsageHourDetail.Any(x => x.Month.Month == month && x.CreatedDate.Value.Year == selectedYear))
                        {
                            // get sum of all Generator Hour of all previous months if any
                            monthData = generator.GeneratorUsageHourDetail
                                                            .Where(x => x.CreatedDate.Value.Year == selectedYear && x.Month.Month < month)
                                                            .Select(x => x.Hours).DefaultIfEmpty(0).Sum() + generator.GeneratorUsageHourDetail.First(x => x.Month.Month == month
                                                                                                                 && x.CreatedDate.Value.Year == selectedYear).Hours;
                        }
                        else // if Generator Hour is not present for the month then sum previous months Generator Hours
                        {
                            // get sum of all Generator Hour of all previous months if any
                            monthData = generator.GeneratorUsageHourDetail
                                                            .Where(x => x.CreatedDate.Value.Year == selectedYear && x.Month.Month < month)
                                                            .Select(x => x.Hours).DefaultIfEmpty(0).Sum();
                        }
                    }
                    else if (usageType == (int)CostAnalysis.FuelTotalCost)
                    {
                        // if Fuel is present for the month
                        if (generator.GeneratorItemDetail.Any(x => x.CreatedDate.Value.Month == month && x.CreatedDate.Value.Year == selectedYear &&
                           x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Generator &&
                                               x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.Fuel))
                        {
                            // get sum of all fuel of all previous months if any
                            monthData = generator.GeneratorItemDetail
                                               .Where(x => x.CreatedDate.Value.Year == selectedYear && x.CreatedDate.Value.Month < month
                                                      && x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Generator &&
                                               x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.Fuel)
                                               .Select(x => x.StoreItemPurchase.Quantity * x.StoreItemPurchase.UnitCost)
                                               .DefaultIfEmpty(0).Sum() +
                                        generator.GeneratorItemDetail.Where(x => x.CreatedDate.Value.Month == month
                                                                        && x.CreatedDate.Value.Year == selectedYear &&
                                                                        x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Generator &&
                                               x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.Fuel)
                                                                 .Select(y => y.StoreItemPurchase.UnitCost * y.StoreItemPurchase.Quantity)
                                                                 .First();
                        }
                        else // if fuel is not present for the month then sum previous months mileages
                        {
                            // get sum of all fuel purchase of all previous months if any
                            monthData = generator.GeneratorItemDetail
                                               .Where(x => x.CreatedDate.Value.Year == selectedYear && x.CreatedDate.Value.Month < month
                                                     && x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Generator &&
                                               x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.Fuel)
                                               .Select(x => x.StoreItemPurchase.Quantity * x.StoreItemPurchase.UnitCost).DefaultIfEmpty(0)
                                               .Sum();
                        }
                    }
                    else if (usageType == (int)CostAnalysis.MobilOilTotalCost)
                    {
                        // if mobil oil is present for the month
                        if (generator.GeneratorItemDetail.Any(x => x.CreatedDate.Value.Month == month && x.CreatedDate.Value.Year == selectedYear &&
                            x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Generator &&
                                               x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.MobilOil))
                        {
                            // get sum of all mobil oil of all previous months if any
                            monthData = generator.GeneratorItemDetail
                                               .Where(x => x.CreatedDate.Value.Year == selectedYear && x.CreatedDate.Value.Month < month
                                                      && x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Generator &&
                                               x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.MobilOil)
                                               .Select(x => x.StoreItemPurchase.Quantity * x.StoreItemPurchase.UnitCost)
                                               .DefaultIfEmpty(0).Sum() +
                                        generator.GeneratorItemDetail.Where(x => x.CreatedDate.Value.Month == month
                                                                        && x.CreatedDate.Value.Year == selectedYear &&
                                                                        x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Generator &&
                                               x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.MobilOil)
                                                                 .Select(y => y.StoreItemPurchase.UnitCost * y.StoreItemPurchase.Quantity)
                                                                 .First();
                        }
                        else // if mobil oil is not present for the month then sum previous months
                        {
                            // get sum of all mobil oil purchase of all previous months if any
                            monthData = generator.GeneratorItemDetail
                                               .Where(x => x.CreatedDate.Value.Year == selectedYear && x.CreatedDate.Value.Month < month
                                                     && x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Generator &&
                                               x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.MobilOil)
                                               .Select(x => x.StoreItemPurchase.Quantity * x.StoreItemPurchase.UnitCost).DefaultIfEmpty(0)
                                               .Sum();
                        }
                    }
                    else if (usageType == (int)UsageType.TotalFuelUsage)
                    {
                        // if fuel is present for the month
                        if (generator.GeneratorItemDetail.Any(x => x.CreatedDate.Value.Month == month && x.CreatedDate.Value.Year == selectedYear &&
                            x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Generator &&
                                               x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.Fuel))
                        {
                            // get sum of all fuel of all previous months if any
                            monthData = generator.GeneratorItemDetail
                                               .Where(x => x.CreatedDate.Value.Year == selectedYear && x.CreatedDate.Value.Month < month
                                                      && x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Generator &&
                                               x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.Fuel)
                                               .Select(x => x.StoreItemPurchase.Quantity)
                                               .DefaultIfEmpty(0).Sum() +
                                        generator.GeneratorItemDetail.Where(x => x.CreatedDate.Value.Month == month
                                                                        && x.CreatedDate.Value.Year == selectedYear &&
                                                                        x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Generator &&
                                               x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.Fuel)
                                                                 .Select(y => y.StoreItemPurchase.Quantity)
                                                                 .First();
                        }
                        else // if fuel is not present for the month then sum previous months
                        {
                            // get sum of all fuel purchase of all previous months if any
                            monthData = generator.GeneratorItemDetail
                                               .Where(x => x.CreatedDate.Value.Year == selectedYear && x.CreatedDate.Value.Month < month
                                                     && x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Generator &&
                                               x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.Fuel)
                                               .Select(x => x.StoreItemPurchase.Quantity).DefaultIfEmpty(0)
                                               .Sum();
                        }
                    }
                    else if (usageType == (int)UsageType.TotalMobilOilUsage)
                    {
                        // if mobil oil is present for the month
                        if (generator.GeneratorItemDetail.Any(x => x.CreatedDate.Value.Month == month && x.CreatedDate.Value.Year == selectedYear &&
                            x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Generator &&
                                               x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.MobilOil))
                        {
                            // get sum of all mobil oil of all previous months if any
                            monthData = generator.GeneratorItemDetail
                                               .Where(x => x.CreatedDate.Value.Year == selectedYear && x.CreatedDate.Value.Month < month
                                                      && x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Generator &&
                                               x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.MobilOil)
                                               .Select(x => x.StoreItemPurchase.Quantity)
                                               .DefaultIfEmpty(0).Sum() +
                                        generator.GeneratorItemDetail.Where(x => x.CreatedDate.Value.Month == month
                                                                        && x.CreatedDate.Value.Year == selectedYear &&
                                                                        x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Generator &&
                                               x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.MobilOil)
                                                                 .Select(y => y.StoreItemPurchase.Quantity)
                                                                 .First();
                        }
                        else // if mobil oil is not present for the month then sum previous months
                        {
                            // get sum of all mobil oil purchase of all previous months if any
                            monthData = generator.GeneratorItemDetail
                                               .Where(x => x.CreatedDate.Value.Year == selectedYear && x.CreatedDate.Value.Month < month
                                                     && x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Generator &&
                                               x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.MobilOil)
                                               .Select(x => x.StoreItemPurchase.Quantity).DefaultIfEmpty(0)
                                               .Sum();
                        }
                    }
                    else if (usageType == (int)UsageType.ActualFuelConsumptionRate)
                    {
                        // if fuel is present for the month
                        if (generator.GeneratorItemDetail.Any(x => x.CreatedDate.Value.Month == month && x.CreatedDate.Value.Year == selectedYear &&
                            x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Generator &&
                                               x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.Fuel))
                        {

                            var usageInaMonth = generator.GeneratorUsageHourDetail.FirstOrDefault(x => x.Month.Month == month && x.Month.Year == selectedYear);
                            // get sum of all fuel purchase in a month
                            double? purchaseFuelInMonth = generator.GeneratorItemDetail.Where(x => x.CreatedDate.Value.Month == month
                                                                                    && x.CreatedDate.Value.Year == selectedYear &&
                                                                                        x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Generator &&
                                               x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.Fuel)
                                                                                    .Select(y => y.StoreItemPurchase.Quantity)
                                                                                    .FirstOrDefault();

                            if (usageInaMonth != null && purchaseFuelInMonth != null)
                            {
                                monthData = Math.Round((purchaseFuelInMonth.Value / usageInaMonth.Hours) * 100, 4);
                            }
                        }
                    }
                    else if (usageType == (int)UsageType.ActualMobilConsumptionRate)
                    {
                        // if fuel is present for the month
                        if (generator.GeneratorItemDetail.Any(x => x.CreatedDate.Value.Month == month && x.CreatedDate.Value.Year == selectedYear &&
                            x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Generator &&
                                               x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.MobilOil))
                        {

                            var usageInaMonth = generator.GeneratorUsageHourDetail.FirstOrDefault(x => x.Month.Month == month && x.Month.Year == selectedYear);
                            // get sum of all fuel purchase in a month
                            double? purchaseFuelInMonth = generator.GeneratorItemDetail.Where(x => x.CreatedDate.Value.Month == month
                                                                                    && x.CreatedDate.Value.Year == selectedYear &&
                                                                                        x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Generator &&
                                               x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.MobilOil)
                                                                                    .Select(y => y.StoreItemPurchase.Quantity)
                                                                                    .FirstOrDefault();

                            if (purchaseFuelInMonth != null && usageInaMonth != null && usageInaMonth.Hours != 0)
                            {
                                monthData = Math.Round((purchaseFuelInMonth.Value / usageInaMonth.Hours) * 100);
                            }
                        }
                    }
                    else if (usageType == (int)UsageType.FuelConsumptionDifference)
                    {
                        // if fuel is present for the month
                        if (generator.GeneratorItemDetail.Any(x => x.CreatedDate.Value.Month == month && x.CreatedDate.Value.Year == selectedYear &&
                            x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Generator &&
                                               x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.Fuel))
                        {
                            double actualRate = 0;
                            var usageInaMonth = generator.GeneratorUsageHourDetail.FirstOrDefault(x => x.Month.Month == month && x.Month.Year == selectedYear);
                            // get sum of all fuel purchase in a month
                            double? purchaseFuelInMonth = generator.GeneratorItemDetail.Where(x => x.CreatedDate.Value.Month == month
                                                                                    && x.CreatedDate.Value.Year == selectedYear &&
                                                                                        x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Generator &&
                                               x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.Fuel)
                                                                                    .Select(y => y.StoreItemPurchase.Quantity)
                                                                                    .FirstOrDefault();

                            if (purchaseFuelInMonth != null && usageInaMonth != null && usageInaMonth.Hours != 0)
                            {
                                actualRate = (purchaseFuelInMonth.Value / usageInaMonth.Hours) * 100;
                                monthData = Math.Round(generator.StandardFuelConsumptionRate - actualRate, 4);
                            }
                        }
                    }
                    else if (usageType == (int)UsageType.MobilOilConsumptionDifference)
                    {
                        // if fuel is present for the month
                        if (generator.GeneratorItemDetail.Any(x => x.CreatedDate.Value.Month == month && x.CreatedDate.Value.Year == selectedYear &&
                            x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Generator &&
                                               x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.MobilOil))
                        {
                            double actualMobilOilRate = 0;

                            var usageInaMonth = generator.GeneratorUsageHourDetail.FirstOrDefault(x => x.Month.Month == month && x.Month.Year == selectedYear);
                            // get sum of all fuel purchase in a month
                            double? purchaseFuelInMonth = generator.GeneratorItemDetail.Where(x => x.CreatedDate.Value.Month == month
                                                                                    && x.CreatedDate.Value.Year == selectedYear &&
                                                                                        x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Generator &&
                                               x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.MobilOil)
                                                                                    .Select(y => y.StoreItemPurchase.Quantity)
                                                                                    .First();

                            if (purchaseFuelInMonth != null && usageInaMonth != null && usageInaMonth.Hours > 0)
                            {
                                actualMobilOilRate = (purchaseFuelInMonth.Value / usageInaMonth.Hours) * 100;
                                monthData = Math.Round(generator.StandardMobilOilConsumptionRate - actualMobilOilRate, 4);
                            }
                        }
                    }
                    else if (usageType == (int)CostAnalysis.SparePartsTotalCost)
                    {
                        // if generator spare parts is present for the month
                        if (generator.GeneratorItemDetail.Any(x => x.CreatedDate.Value.Month == month && x.CreatedDate.Value.Year == selectedYear &&
                           x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Generator &&
                                               x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.SpareParts))
                        {
                            // get sum of all generator spare parts of all previous months if any
                            monthData = generator.GeneratorItemDetail
                                               .Where(x => x.CreatedDate.Value.Year == selectedYear && x.CreatedDate.Value.Month < month
                                                      && x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Generator &&
                                               x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.SpareParts)
                                               .Select(x => x.StoreItemPurchase.Quantity * x.StoreItemPurchase.UnitCost)
                                               .DefaultIfEmpty(0).Sum() +
                                        generator.GeneratorItemDetail.Where(x => x.CreatedDate.Value.Month == month
                                                                        && x.CreatedDate.Value.Year == selectedYear &&
                                                                        x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Generator &&
                                               x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.SpareParts)
                                                                 .Select(y => y.StoreItemPurchase.UnitCost * y.StoreItemPurchase.Quantity)
                                                                 .First();
                        }
                        else // if generator spare parts is not present for the month then sum previous months mileages
                        {
                            // get sum of all generator spare parts purchase of all previous months if any
                            monthData = generator.GeneratorItemDetail
                                               .Where(x => x.CreatedDate.Value.Year == selectedYear && x.CreatedDate.Value.Month < month
                                                     && x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Generator &&
                                               x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.SpareParts)
                                               .Select(x => x.StoreItemPurchase.Quantity * x.StoreItemPurchase.UnitCost).DefaultIfEmpty(0)
                                               .Sum();
                        }
                    }
                    else if (usageType == (int)CostAnalysis.ServiceAndMaintenanceTotalCost)
                    {
                        // if generator maintenance service is present for the month
                        if (generator.GeneratorItemDetail.Any(x => x.CreatedDate.Value.Month == month && x.CreatedDate.Value.Year == selectedYear &&
                           x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Generator &&
                                               x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.MaintenanceService))
                        {
                            // get sum of all generator maintenance service of all previous months if any
                            monthData = generator.GeneratorItemDetail
                                               .Where(x => x.CreatedDate.Value.Year == selectedYear && x.CreatedDate.Value.Month < month
                                                      && x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Generator &&
                                               x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.MaintenanceService)
                                               .Select(x => x.StoreItemPurchase.Quantity * x.StoreItemPurchase.UnitCost)
                                               .DefaultIfEmpty(0).Sum() +
                                        generator.GeneratorItemDetail.Where(x => x.CreatedDate.Value.Month == month
                                                                        && x.CreatedDate.Value.Year == selectedYear &&
                                                                        x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Generator &&
                                               x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.MaintenanceService)
                                                                 .Select(y => y.StoreItemPurchase.UnitCost * y.StoreItemPurchase.Quantity)
                                                                 .First();
                        }
                        else // if generator maintenance service is not present for the month then sum previous months mileages
                        {
                            // get sum of all generator maintenance service purchase of all previous months if any
                            monthData = generator.GeneratorItemDetail
                                               .Where(x => x.CreatedDate.Value.Year == selectedYear && x.CreatedDate.Value.Month < month
                                                     && x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Generator &&
                                               x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.MaintenanceService)
                                               .Select(x => x.StoreItemPurchase.Quantity * x.StoreItemPurchase.UnitCost).DefaultIfEmpty(0)
                                               .Sum();
                        }

                    }
                    else if (usageType == (int)UsageType.MobilOilChangeRotation)
                    {
                        // if mobil oil is present for the month
                        if (generator.GeneratorItemDetail.Any(x => x.CreatedDate.Value.Month == month && x.CreatedDate.Value.Year == selectedYear &&
                            x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Generator &&
                                               x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.MobilOil))
                        {
                            // get sum of all mobil oil of all previous months if any
                            monthData = generator.GeneratorItemDetail.Where(x => x.CreatedDate.Value.Month == month
                                                                        && x.CreatedDate.Value.Year == selectedYear &&
                                                                        x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Generator &&
                                               x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.MobilOil).Count();
                        }
                    }
                    else if (usageType == (int)UsageType.RemainingKmForMobilOilChange)
                    {
                        if (generator.GeneratorItemDetail.Any(x => x.CreatedDate.Value.Month == month && x.CreatedDate.Value.Year == selectedYear &&
                             x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Generator &&
                                               x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.MobilOil))
                        {
                            double quantity = generator.GeneratorItemDetail
                                               .Where(x => x.CreatedDate.Value.Year == selectedYear && x.CreatedDate.Value.Month < month
                                                      && x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Generator &&
                                               x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.MobilOil)
                                               .Select(x => x.StoreItemPurchase.Quantity)
                                               .DefaultIfEmpty(0).Sum();
                            if (quantity != 0)
                            {
                                monthData = (100 / generator.StandardMobilOilConsumptionRate) * quantity; // (100Km/stdmobiloilconsumptionrate(per hour))* total quantity of purchased mobil oil
                            }
                        }
                        else
                        {
                            TotalMobilOilAndMonth data = new TotalMobilOilAndMonth();
                            data = GetTotalUsageTillMobilOilChange(month, selectedYear, generator);

                            double totalHoursUsed = generator.GeneratorUsageHourDetail
                                                            .Where(x => x.CreatedDate.Value.Year == selectedYear && x.CreatedDate.Value.Year == data.Year && x.Month.Month > data.Month
                                                            && x.Month.Month <= month)
                                                            .Select(x => x.Hours).DefaultIfEmpty(0).Sum();


                            double totalHoursGeneratorCanBeUsed = Math.Round(generator.StandardMobilOilConsumptionRate * data.TotalMobilOil, 4);

                            monthData = totalHoursGeneratorCanBeUsed - totalHoursUsed;

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

        private TotalMobilOilAndMonth GetTotalUsageTillMobilOilChange(int month, int year, MonthlyBreakDownModel generator)
        {
            TotalMobilOilAndMonth mobilOilAndMonth = new TotalMobilOilAndMonth();
            mobilOilAndMonth.Year = year;
            mobilOilAndMonth.Month = month;
            mobilOilAndMonth.TotalMobilOil = 0;

            var item = generator.GeneratorItemDetail.FirstOrDefault(x => x.CreatedDate.Value.Month == month && x.CreatedDate.Value.Year == year &&
                             x.StoreItemPurchase.StoreInventoryItem.StoreItemGroup.ItemTypeCategory == (int)TransportItemCategory.Generator &&
                                               x.StoreItemPurchase.StoreInventoryItem.ItemTypeCategory == (int)TransportItemCategory.MobilOil);

            if (item == null && month == (int)Month.January && generator.CreatedDate.Date.Year < year)
            {
                mobilOilAndMonth = GetTotalUsageTillMobilOilChange((int)Month.December, year - 1, generator);
            }
            else if (item == null && generator.CreatedDate.Date.Year == year && month != (int)Month.January)
            {
                mobilOilAndMonth = GetTotalUsageTillMobilOilChange(month - 1, year, generator);
            }

            if (item != null)
            {
                mobilOilAndMonth.TotalMobilOil = item.StoreItemPurchase.Quantity;
                mobilOilAndMonth.Month = month;
                mobilOilAndMonth.Year = year;
            }

            return mobilOilAndMonth;
        }

        private void SwitchCaseStatement(MonthlyBreakDownModel generator, int selectedYear, int usageType, out UsageAnalysisBreakDown usageBreakDown, out CostAnalysisBreakDown costBreakDown)
        {
            usageBreakDown = new UsageAnalysisBreakDown();
            costBreakDown = new CostAnalysisBreakDown();

            for (int j = 1; j <= 12; j++)
            {
                double monthData = GetMonthlyGeneratorDetail(generator, selectedYear, j, usageType);

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
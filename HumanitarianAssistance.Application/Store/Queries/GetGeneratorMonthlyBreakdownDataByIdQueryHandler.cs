using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Store.Models;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetGeneratorMonthlyBreakdownDataByIdQueryHandler: IRequestHandler<GetGeneratorMonthlyBreakdownDataByIdQuery, MonthlyBreakdownDataModel>
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
                
                MonthlyBreakDownModel generator = await _dbContext.PurchasedGeneratorDetail
                                           //.Include(x => x.EmployeeDetail)
                                           .Include(x => x.StoreItemPurchase)
                                           .Include(x => x.GeneratorUsageHourList)
                                           .Include(x=> x.GeneratorItemDetail)
                                           .ThenInclude(x=> x.StoreItemPurchase)
                                           .ThenInclude(x=> x.StoreInventoryItem)
                                           .Where(x => x.IsDeleted == false && x.Id == request.GeneratorId)
                                                 .Select(x => new MonthlyBreakDownModel
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
                                                     GeneratorUsageHourDetail = x.GeneratorUsageHourList.Where(y => y.IsDeleted == false && y.CreatedDate.Value.Date.Year == request.SelectedYear).ToList()
                                                 })
                                           .FirstOrDefaultAsync();

                if (generator != null)
                {
                    model.IncurredUsage = generator.IncurredUsage;
                    model.StandardFuelConsumptionRate = generator.StandardFuelConsumptionRate;
                    model.StandardMobilOilConsumptionRate = generator.StandardMobilOilConsumptionRate;
                    model.StartingUsage = generator.StartingUsage;
                    model.StartingCost = generator.OriginalCost;

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
    }
}
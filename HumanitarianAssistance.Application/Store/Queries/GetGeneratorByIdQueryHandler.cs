using System;
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
    public class GetGeneratorByIdQueryHandler : IRequestHandler<GetGeneratorByIdQuery, GeneratorModel>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetGeneratorByIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<GeneratorModel> Handle(GetGeneratorByIdQuery request, CancellationToken cancellationToken)
        {
            GeneratorModel model = new GeneratorModel();

            try
            {
                model = await _dbContext.PurchasedGeneratorDetail
                                        .Include(x => x.StoreItemPurchase)
                                        .ThenInclude(x => x.EmployeeDetail)
                                        .Where(x => x.IsDeleted == false &&
                                         x.Id == request.GeneratorId)
                                         .Select(x => new GeneratorModel
                                         {
                                             GeneratorId = x.Id,
                                             Voltage = x.Voltage,
                                             StartingUsage = x.StartingUsage,
                                             IncurredUsage = x.IncurredUsage,
                                             StandardFuelConsumptionRate = x.FuelConsumptionRate,
                                             StandardMobilOilConsumptionRate = x.MobilOilConsumptionRate,
                                             TotalFuelUsage= x.GeneratorItemDetail.Where(y => y.IsDeleted == false &&
                                                                    y.GeneratorPurchaseId == request.GeneratorId && y.StoreItemPurchase.StoreInventoryItem.ItemId == (int)TransportItem.GeneratorFuel)
                                                                               .Select(z => z.StoreItemPurchase.Quantity).DefaultIfEmpty(0).Sum(),
                                            TotalMobilOilUsage= x.GeneratorItemDetail.Where(y => y.IsDeleted == false &&
                                                                     y.GeneratorPurchaseId == request.GeneratorId && y.StoreItemPurchase.StoreInventoryItem.ItemId == (int)TransportItem.GeneratorMobilOil)
                                                                               .Select(z => z.StoreItemPurchase.Quantity).DefaultIfEmpty(0).Sum(),
                                            FuelTotalCost = x.GeneratorItemDetail.Where(y => y.IsDeleted == false &&
                                                           y.GeneratorPurchaseId == request.GeneratorId && y.StoreItemPurchase.StoreInventoryItem.ItemId == (int)TransportItem.GeneratorFuel)
                                                                               .Select(z => z.StoreItemPurchase.Quantity * z.StoreItemPurchase.UnitCost).DefaultIfEmpty(0).Sum(),
                                            MobilOilTotalCost = x.GeneratorItemDetail.Where(y => y.IsDeleted == false &&
                                                           y.GeneratorPurchaseId == request.GeneratorId && y.StoreItemPurchase.StoreInventoryItem.ItemId == (int)TransportItem.GeneratorMobilOil)
                                                                               .Select(z => z.StoreItemPurchase.Quantity * z.StoreItemPurchase.UnitCost).DefaultIfEmpty(0).Sum(),
                                            SparePartsTotalCost = x.GeneratorItemDetail.Where(y => y.IsDeleted == false &&
                                                           y.GeneratorPurchaseId == request.GeneratorId && y.StoreItemPurchase.StoreInventoryItem.ItemId == (int)TransportItem.GeneratorSpareParts)
                                                                               .Select(z => z.StoreItemPurchase.Quantity * z.StoreItemPurchase.UnitCost).DefaultIfEmpty(0).Sum() ,
                                            ServicesAndMaintenanceTotalCost = x.GeneratorItemDetail.Where(y => y.IsDeleted == false &&
                                                           y.GeneratorPurchaseId == request.GeneratorId && y.StoreItemPurchase.StoreInventoryItem.ItemId == (int)TransportItem.GeneratorMaintenanceService)
                                                                               .Select(z => z.StoreItemPurchase.Quantity * z.StoreItemPurchase.UnitCost).DefaultIfEmpty(0).Sum(),

                                            CurrentUsage= x.StartingUsage+ x.IncurredUsage+ x.GeneratorUsageHourList.Where(z=> z.IsDeleted == false).Select(z=> z.Hours).DefaultIfEmpty(0).Sum(),
                                            GeneratorStartingCost= x.StoreItemPurchase.UnitCost,
                                             OfficeId = x.OfficeId,
                                             OfficeName = x.OfficeId != 0 ? _dbContext.OfficeDetail.FirstOrDefault(y => x.IsDeleted == false && y.OfficeId == x.OfficeId).OfficeName : null,
                                             PurchaseName = $"{x.StoreItemPurchase.PurchaseName} - {x.StoreItemPurchase.PurchaseDate.ToShortDateString()}-{x.StoreItemPurchase.PurchaseId}",
                                             ModelYear = x.ModelYear,
                                             PurchaseId = x.PurchaseId,
                                             PurchasedBy = x.StoreItemPurchase.EmployeeDetail.EmployeeName,
                                         })
                                         .FirstOrDefaultAsync();

                    model.ActualFuelConsumptionRate= model.CurrentUsage !=0 ? Math.Round((model.TotalFuelUsage/model.CurrentUsage)*100, 4) :0;
                    model.ActualMobilOilConsumptionRate= model.CurrentUsage !=0 ? Math.Round((model.TotalMobilOilUsage/model.CurrentUsage)*100, 4): 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return model;
        }
    }
}
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
    public class GetGeneratorListQueryHandler: IRequestHandler<GetGeneratorListQuery, GeneratorTrackerListModel>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetGeneratorListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<GeneratorTrackerListModel> Handle(GetGeneratorListQuery request, CancellationToken cancellationToken)
        {
            GeneratorTrackerListModel generator = new GeneratorTrackerListModel();

            try
            {

                var purchasedGeneratorsQuery = _dbContext.PurchasedGeneratorDetail
                                                  .Include(x=> x.StoreItemPurchase)
                                                  .Where(x=> x.IsDeleted == false && x.StoreItemPurchase.IsDeleted == false)
                                                  .OrderByDescending(x=> x.Id)
                                                  .Select(x=> new GeneratorTrackerModel 
                                                  {
                                                      GeneratorId = x.Id,
                                                      Voltage= x.Voltage,
                                                      FuelConsumptionRate = x.FuelConsumptionRate,
                                                      StartingUsage= x.StartingUsage,
                                                      IncurredUsage= x.IncurredUsage,
                                                      TotalUsage= x.StartingUsage + x.IncurredUsage,
                                                      MobilOilConsumptionRate= x.MobilOilConsumptionRate,
                                                      ModelYear= x.ModelYear,
                                                      PurchaseId= x.PurchaseId,
                                                      TotalCost = x.StoreItemPurchase.UnitCost,
                                                      OriginalCost = x.StoreItemPurchase.UnitCost,
                                                      OfficeId = x.OfficeId
                                                  })
                                                  .AsQueryable();

                if(request.Voltage != null)
                {
                    purchasedGeneratorsQuery=  purchasedGeneratorsQuery.Where(x=> x.Voltage == request.Voltage);
                }

                if(request.TotalCost!= null)
                {
                   purchasedGeneratorsQuery= purchasedGeneratorsQuery.Where(x=> x.TotalCost == request.TotalCost); 
                }

                if(request.OfficeId != null)
                {
                   purchasedGeneratorsQuery= purchasedGeneratorsQuery.Where(x=> x.OfficeId == request.OfficeId); 
                }

                if(request.ModelYear != null)
                {
                   purchasedGeneratorsQuery= purchasedGeneratorsQuery.Where(x=> x.ModelYear == request.ModelYear); 
                }

                generator.TotalRecords= await purchasedGeneratorsQuery.CountAsync();

                generator.GeneratorTrackerList = await purchasedGeneratorsQuery.Skip(request.pageSize.Value * request.pageIndex.Value)
                                                    .Take(request.pageSize.Value)
                                                    .ToListAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return generator;
        }
    }
}
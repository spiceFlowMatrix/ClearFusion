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
                                             FuelConsumptionRate = x.FuelConsumptionRate,
                                             MobilOilConsumptionRate = x.MobilOilConsumptionRate,
                                             OfficeId = x.OfficeId,
                                             OfficeName = x.OfficeId != 0 ? _dbContext.OfficeDetail.FirstOrDefault(y => x.IsDeleted == false && y.OfficeId == x.OfficeId).OfficeName : null,
                                             PurchaseName = $"{x.StoreItemPurchase.PurchaseName} - {x.StoreItemPurchase.PurchaseDate.ToShortDateString()}-{x.StoreItemPurchase.PurchaseId}",
                                             ModelYear = x.ModelYear,
                                             PurchaseId = x.PurchaseId,
                                             PurchasedBy = x.StoreItemPurchase.EmployeeDetail.EmployeeName,
                                         })
                                         .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return model;
        }
    }
}
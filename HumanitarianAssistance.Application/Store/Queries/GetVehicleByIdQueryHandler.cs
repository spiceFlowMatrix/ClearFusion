using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Store.Models;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Store;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetVehicleByIdQueryHandler : IRequestHandler<GetVehicleByIdQuery, VehicleModel>
    {

        private HumanitarianAssistanceDbContext _dbContext;

        public GetVehicleByIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<VehicleModel> Handle(GetVehicleByIdQuery request, CancellationToken cancellationToken)
        {
            VehicleModel vehicle = new VehicleModel();

            try
            {
                vehicle = await _dbContext.PurchasedVehicleDetail
                                          .Include(x=> x.EmployeeDetail)
                                          .Include(x=> x.StoreItemPurchase)
                                          .Where(x => x.IsDeleted == false && x.Id == request.VehicleId)
                                        .Select(x => new VehicleModel
                                        {
                                            VehicleId = x.Id,
                                            PlateNo = x.PlateNo,
                                            EmployeeId = x.EmployeeId,
                                            FuelConsumptionRate = x.FuelConsumptionRate,
                                            StartingMileage = x.StartingMileage,
                                            IncurredMileage = x.IncurredMileage,
                                            MobilOilConsumptionRate = x.MobilOilConsumptionRate,
                                            ModelYear = x.ModelYear,
                                            OfficeId= x.OfficeId,
                                            EmployeeName= x.EmployeeDetail.EmployeeName,
                                            PurchaseName= $"{x.StoreItemPurchase.PurchaseName} - {x.StoreItemPurchase.PurchaseDate.ToShortDateString()}-{x.StoreItemPurchase.PurchaseId}",
                                            PurchaseId= x.StoreItemPurchase.PurchaseId,
                                            OfficeName = x.OfficeId != 0 ? _dbContext.OfficeDetail.FirstOrDefault(y=> y.IsDeleted== false && y.OfficeId == x.OfficeId).OfficeName : null
                                        }).FirstOrDefaultAsync();

                if (vehicle == null)
                {
                    throw new Exception(StaticResource.RecordNotFound);
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
            return vehicle;
        }
    }
}
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
    public class GetVehicleMonthlyBreakdownDataByIdQueryHandler: IRequestHandler<GetVehicleMonthlyBreakdownDataByIdQuery, MonthlyBreakdownDataModel>
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
               var vehicle = await _dbContext.PurchasedVehicleDetail
                                          .Include(x => x.EmployeeDetail)
                                          .Include(x => x.StoreItemPurchase)
                                          .Include(x => x.VehicleMileageDetail)
                                          .Include(x => x.VehicleItemDetail)
                                          .ThenInclude(x => x.StoreItemPurchase)
                                          .ThenInclude(x => x.StoreInventoryItem)
                                          .Where(x => x.IsDeleted == false && x.Id == request.VehicleId)
                                                .Select(x => new
                                          {
                                              CreatedDate= x.CreatedDate,
                                              VehicleId = x.Id,
                                              PlateNo = x.PlateNo,
                                              EmployeeId = x.EmployeeId,
                                              StandardFuelConsumptionRate = x.FuelConsumptionRate,
                                              StandardMobilOilConsumptionRate = x.MobilOilConsumptionRate,
                                              StartingMileage = x.StartingMileage,
                                              IncurredMileage = x.IncurredMileage,
                                              ModelYear = x.ModelYear,
                                              OfficeId = x.OfficeId,
                                              EmployeeName = x.EmployeeDetail.EmployeeName,
                                              PurchaseName = $"{x.StoreItemPurchase.PurchaseName} - {x.StoreItemPurchase.PurchaseDate.ToShortDateString()}-{x.StoreItemPurchase.PurchaseId}",
                                              PurchaseId = x.StoreItemPurchase.PurchaseId,
                                              OfficeName = x.OfficeId != 0 ? _dbContext.OfficeDetail.FirstOrDefault(y => y.IsDeleted == false && y.OfficeId == x.OfficeId).OfficeName : null,
                                              VehicleItemDetail = x.VehicleItemDetail.Where(y => y.IsDeleted == false && y.CreatedDate.Value.Date.Year == request.SelectedYear).ToList(),
                                              VehicleMileageDetail= x.VehicleMileageDetail.Where(y=> y.IsDeleted == false && y.CreatedDate.Value.Date.Year == request.SelectedYear).ToList()
                                          })
                                          .FirstOrDefaultAsync();

                    if(vehicle != null)
                    {
                        MonthlyBreakDown CurrentMileage= new MonthlyBreakDown();

                        // if(vehicle.)

                    }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return model;
        }
    }
}
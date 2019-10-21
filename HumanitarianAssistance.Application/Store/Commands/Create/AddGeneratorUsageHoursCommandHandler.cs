using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Store.Commands.Create
{
    public class AddGeneratorUsageHoursCommandHandler: IRequestHandler<AddGeneratorUsageHoursCommand, bool>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public AddGeneratorUsageHoursCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(AddGeneratorUsageHoursCommand request, CancellationToken cancellationToken)
        {
            bool success= false;

            try
            {
                if(request != null) 
                {

                //     VehicleMileageDetail mileage = await _dbContext.VehicleMileageDetail.FirstOrDefaultAsync(x=> x.IsDeleted == false &&
                //                                                                         x.VehicleId == request.GeneratorId &&
                //                                                                         x.MileageMonth.Month == request.Month.Month);

                //     if(mileage == null)
                //     {
                //          mileage= new VehicleMileageDetail
                //         {
                //             IsDeleted= false,
                //             CreatedDate= DateTime.UtcNow,
                //             CreatedById= request.CreatedById,
                //             VehicleId= request.VehicleId,
                //             MileageMonth = request.Month,
                //             Mileage= request.Mileage
                //         };

                //         await _dbContext.VehicleMileageDetail.AddAsync(mileage);
                //         await _dbContext.SaveChangesAsync();
                //     }
                //     else 
                //     {
                //         mileage.ModifiedDate = DateTime.UtcNow;
                //         mileage.ModifiedById = request.CreatedById;
                //         mileage.VehicleId= request.VehicleId;
                //         mileage.MileageMonth = request.Month;
                //         mileage.Mileage= request.Mileage;

                //         _dbContext.VehicleMileageDetail.Update(mileage);
                //         await _dbContext.SaveChangesAsync();
                //     }

                //    success= true;
                }
                else
                {
                    throw new Exception(StaticResource.RequestValuesInAppropriate);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return success;
        }
    }
}
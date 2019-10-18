using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Domain.Entities.Store;
using HumanitarianAssistance.Persistence;
using MediatR;

namespace HumanitarianAssistance.Application.Store.Commands.Create
{
    public class AddVehicleMileageCommandHandler: IRequestHandler<AddVehicleMileageCommand, bool>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public AddVehicleMileageCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(AddVehicleMileageCommand request, CancellationToken cancellationToken)
        {
            bool success= false;

            try
            {
                if(request != null) 
                {
                    VehicleMileageDetail mileage= new VehicleMileageDetail
                    {
                        IsDeleted= false,
                        CreatedDate= DateTime.UtcNow,
                        VehicleId= request.VehicleId,
                        MileageMonth = request.Month,
                        Mileage= request.Mileage
                    };

                   await _dbContext.VehicleMileageDetail.AddAsync(mileage);
                   await _dbContext.SaveChangesAsync();

                   success= true;
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
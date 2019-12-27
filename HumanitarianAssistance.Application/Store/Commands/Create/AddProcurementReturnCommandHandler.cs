using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Domain.Entities.Store;
using HumanitarianAssistance.Persistence;
using MediatR;

namespace HumanitarianAssistance.Application.Store.Commands.Create
{
    public class AddProcurementReturnCommandHandler: IRequestHandler<AddProcurementReturnCommand, object>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public AddProcurementReturnCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(AddProcurementReturnCommand request, CancellationToken cancellationToken)
        {
            bool success = false;

            try
            {
                if(request != null)
                {
                    ReturnProcurementDetail detail= new ReturnProcurementDetail
                    {
                        IsDeleted = false,
                        PurchaseId= request.PurchaseId,
                        ProcurementId= request.ProcurementId,
                        ReturnedDate= request.ReturnedDate,
                        ReturnedQuantity = request.ReturnedQuantity,
                        CreatedDate = DateTime.UtcNow,
                        CreatedById = request.CreatedById
                    };

                    await _dbContext.ReturnProcurementDetail.AddAsync(detail);
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
using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Store.Commands.Delete
{
    public class DeleteReturnProcurementCommandHandler: IRequestHandler<DeleteReturnProcurementCommand, object>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public DeleteReturnProcurementCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(DeleteReturnProcurementCommand request, CancellationToken cancellationToken)
        {
            bool success = false;

            try
            {
                if(request.Id != 0)
                {
                    var item = await _dbContext.ReturnProcurementDetail.FirstOrDefaultAsync(x=> x.IsDeleted == false && x.Id == request.Id);

                    if(item != null)
                    {
                        item.IsDeleted = true;
                        item.ModifiedById = request.ModifiedById;
                        item.ModifiedDate = DateTime.UtcNow; 

                        _dbContext.ReturnProcurementDetail.Update(item);
                        await _dbContext.SaveChangesAsync();
                        success = true;
                    }
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
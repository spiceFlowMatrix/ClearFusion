using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class RejectAdvanceRequestCommandHandler: IRequestHandler<RejectAdvanceRequestCommand, object>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public RejectAdvanceRequestCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(RejectAdvanceRequestCommand request, CancellationToken cancellationToken)
        {
            bool success = false;

            try
            {
                Advances advance = await _dbContext.Advances.FirstOrDefaultAsync(x=> x.IsDeleted == false && x.AdvancesId == request.AdvanceId);

                if(advance == null)
                {
                    throw new Exception(StaticResource.RecordNotFound);
                }

                advance.IsApproved = false;
                advance.ModifiedDate = DateTime.UtcNow;
                advance.ModifiedById = request.ModifiedById;
                _dbContext.Advances.Update(advance);
                await _dbContext.SaveChangesAsync();

                success = true;
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return success;
        }
    }
}
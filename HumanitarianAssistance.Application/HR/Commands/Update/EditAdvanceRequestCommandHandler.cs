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
    public class EditAdvanceRequestCommandHandler: IRequestHandler<EditAdvanceRequestCommand, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public EditAdvanceRequestCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(EditAdvanceRequestCommand request, CancellationToken cancellationToken)
        {
            bool success = false;

            try
            {
                Advances advance = await _dbContext.Advances.FirstOrDefaultAsync(x=> x.IsDeleted == false && x.AdvancesId == request.AdvanceId);

                if(advance == null)
                {
                    throw new Exception(StaticResource.RecordNotFound);
                }

                advance.RequestAmount = request.RequestAmount;
                advance.AdvanceDate = request.AdvanceDate;
                advance.ApprovedBy= request.ApprovedBy;
                advance.NumberOfInstallments = request.NumberOfInstallments;
                advance.ModeOfReturn= request.ModeOfReturn;
                advance.Description = request.Description;
                advance.ModifiedById = request.ModifiedById;
                advance.ModifiedDate = request.ModifiedDate;

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
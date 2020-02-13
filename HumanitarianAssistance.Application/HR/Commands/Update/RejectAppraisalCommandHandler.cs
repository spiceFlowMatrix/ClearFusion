

using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using HumanitarianAssistance.Domain.Entities.HR;


namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class RejectAppraisalCommandHandler : IRequestHandler<RejectAppraisalCommand, bool>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public RejectAppraisalCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(RejectAppraisalCommand request, CancellationToken cancellationToken)
        {
            bool success;

            try
            {
                EmployeeAppraisalDetails ifExistEmpRecord = await _dbContext.EmployeeAppraisalDetails.Where(x => x.IsDeleted == false && x.EmployeeAppraisalDetailsId == request.EmployeeAppraisalDetailsId ).FirstOrDefaultAsync();
                if (ifExistEmpRecord != null)
                {
                    ifExistEmpRecord.AppraisalStatus = false;
                    // ifExistEmpRecord.IsDeleted = true;
                    ifExistEmpRecord.ModifiedById = request.ModifiedById;
                    ifExistEmpRecord.ModifiedDate = request.ModifiedDate;
                    await _dbContext.SaveChangesAsync();
                }

                success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return success;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Common.Enums;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class TerminateEmployeeByEmployeeIdCommandHandler : IRequestHandler<TerminateEmployeeByEmployeeIdCommand, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public TerminateEmployeeByEmployeeIdCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(TerminateEmployeeByEmployeeIdCommand request, CancellationToken cancellationToken)
        {
            bool success = false;

            try
            {
                var empDetail = await _dbContext.EmployeeDetail
                                .Where(x=>x.IsDeleted == false && x.EmployeeID == request.EmployeeId)
                                .Include(x=>x.EmployeeProfessionalDetail)
                                .FirstOrDefaultAsync();
                
                if(empDetail != null) 
                {
                    empDetail.EmployeeTypeId = (int)EmployeeTypeStatus.Terminated;
                    empDetail.EmployeeProfessionalDetail.EmployeeTypeId = (int)EmployeeTypeStatus.Terminated;
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
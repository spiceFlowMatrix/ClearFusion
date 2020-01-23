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

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class RevokeEmployeePayrollCommandHandler: IRequestHandler<RevokeEmployeePayrollCommand, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public RevokeEmployeePayrollCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(RevokeEmployeePayrollCommand request, CancellationToken cancellationToken)
        {
            bool success = false;

            try
            {
                EmployeeMonthlyAttendance attendance = await _dbContext.EmployeeMonthlyAttendance
                                                                       .FirstOrDefaultAsync(x=> x.IsDeleted == false &&
                                                                        x.EmployeeId == request.EmployeeId && x.Month == request.Month &&
                                                                        x.Year == DateTime.UtcNow.Year);

                if(attendance == null)
                {
                    throw new Exception(StaticResource.RecordNotFound);
                }

                attendance.IsApproved = false;

                List<AccumulatedSalaryHeadDetail> list = await _dbContext.AccumulatedSalaryHeadDetail.Where(x=> x.IsDeleted == false &&
                                                                x.EmployeeId == request.EmployeeId && x.Month == request.Month &&
                                                                x.Year == DateTime.UtcNow.Year).ToListAsync();

                if(list.Any())
                {
                    list.ForEach(x=> x.IsDeleted = true);
                }

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
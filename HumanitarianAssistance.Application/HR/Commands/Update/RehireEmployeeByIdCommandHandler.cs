using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class RehireEmployeeByIdCommandHandler : IRequestHandler<RehireEmployeeByIdCommand, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public RehireEmployeeByIdCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(RehireEmployeeByIdCommand request, CancellationToken cancellationToken)
        {
            bool success = false;

            try
            {
                var resignationDetail = await _dbContext.EmployeeResignationDetail
                                        .Include(x=>x.EmployeeDetail)
                                        .FirstOrDefaultAsync(x=>x.IsDeleted == false && x.EmployeeID == request.EmployeeId);
                
                if (resignationDetail == null)
                {
                    var empDetail =  await _dbContext.EmployeeDetail.FirstOrDefaultAsync(x=>x.IsDeleted == false && x.EmployeeID == request.EmployeeId);
                    if (empDetail != null) 
                    {
                        empDetail.IsResigned = false;
                        empDetail.ResignationStatus = (int)ResignationStatus.Rehired;
                    }
                }
                else 
                {
                    resignationDetail.IsDeleted = true;
                    resignationDetail.EmployeeDetail.IsResigned = false;
                    resignationDetail.EmployeeDetail.ResignationStatus = (int)ResignationStatus.Rehired;
                    
                    var resignationQuestionDetail = await _dbContext.EmployeeResignationQuestionDetail.Where(x=>x.IsDeleted == false && x.ResignationId == resignationDetail.EmployeeResignationId)
                                                .ToListAsync();
                    if(resignationQuestionDetail.Any()) 
                    {
                        resignationQuestionDetail.ForEach(x => x.IsDeleted = true);
                    }
                }

                await _dbContext.SaveChangesAsync();
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
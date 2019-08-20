using HumanitarianAssistance.Application.Accounting.Models;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetPrimarySalaryHeadsQueryHandler : IRequestHandler<GetPrimarySalaryHeadsQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public GetPrimarySalaryHeadsQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetPrimarySalaryHeadsQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                List<PayrollHeadModel> PayrollHeadModelList = await _dbContext.EmployeePayrollAccountHead
                                                                                       .Where(x => x.IsDeleted == false && x.EmployeeId == request.EmployeeId)
                                                                                       .Select(x => new PayrollHeadModel
                                                                                       {
                                                                                           AccountNo = x.AccountNo,
                                                                                           PayrollHeadId = x.PayrollHeadId,
                                                                                           PayrollHeadName = x.PayrollHeadName,
                                                                                           PayrollHeadTypeId = x.PayrollHeadTypeId,
                                                                                           TransactionTypeId = x.TransactionTypeId
                                                                                       }).ToListAsync();

                response.data.PayrollHeadModelList = PayrollHeadModelList;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}

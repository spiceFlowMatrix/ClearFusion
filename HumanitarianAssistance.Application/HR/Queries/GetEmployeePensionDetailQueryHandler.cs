using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeePensionDetailQueryHandler: IRequestHandler<GetEmployeePensionDetailQuery, ApiResponse>
    {
         private readonly HumanitarianAssistanceDbContext _dbContext;
        public GetEmployeePensionDetailQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(GetEmployeePensionDetailQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                List<EmployeePensionDetailModel> openingPensionDetail = await _dbContext.MultiCurrencyOpeningPension
                                                    .Include(x => x.CurrencyDetails)
                                                    .Where(x => x.IsDeleted == false && x.EmployeeID == request.EmployeeId)
                                                    .Select(x => new EmployeePensionDetailModel
                                                    {
                                                        Date = x.PensionStartDate,
                                                        CurrencyName = x.CurrencyDetails.CurrencyName,
                                                        Amount = x.Amount
                                                    })
                                                    .ToListAsync();
                response.ResponseData = openingPensionDetail;
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
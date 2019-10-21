using HumanitarianAssistance.Application.Configuration.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeeAdvanceHistoryDetailQueryHandler : IRequestHandler<GetEmployeeAdvanceHistoryDetailQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetEmployeeAdvanceHistoryDetailQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetEmployeeAdvanceHistoryDetailQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var advances = await _dbContext.EmployeePaymentTypes.Where(x => x.IsDeleted == false && x.AdvanceId == request.AdvanceID).ToListAsync();

                if (advances.Any())
                {

                    List<AdvancesHistoryModel> advancesHistory = advances.Select(x => new AdvancesHistoryModel
                    {
                        AdvanceId = x.AdvanceId,
                        BalanceAmount = x.AdvanceAmount.Value,
                        InstallmentPaid = x.AdvanceRecoveryAmount.Value,
                        PaymentDate = x.PaymentDate.Value.Date
                    }).ToList();

                    response.data.AdvanceHistory = advancesHistory;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
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

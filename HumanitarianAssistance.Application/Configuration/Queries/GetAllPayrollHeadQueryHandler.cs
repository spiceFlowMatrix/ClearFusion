using HumanitarianAssistance.Application.Configuration.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Configuration.Queries
{
    public class GetAllPayrollHeadQueryHandler : IRequestHandler<GetAllPayrollHeadQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetAllPayrollHeadQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllPayrollHeadQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                ICollection<PayrollAccountHead> PayrollAccountHeadList = await _dbContext.PayrollAccountHead.Where(x => x.IsDeleted == false).ToListAsync();

                response.data.PayrollAccountHead = PayrollAccountHeadList;
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

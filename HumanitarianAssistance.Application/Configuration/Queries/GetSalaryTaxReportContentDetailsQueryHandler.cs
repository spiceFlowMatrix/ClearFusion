using AutoMapper;
using HumanitarianAssistance.Application.Configuration.Models;
using HumanitarianAssistance.Application.Infrastructure;
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

namespace HumanitarianAssistance.Application.Configuration.Queries
{
    public class GetSalaryTaxReportContentDetailsQueryHandler : IRequestHandler<GetSalaryTaxReportContentDetailsQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;

        public GetSalaryTaxReportContentDetailsQueryHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(GetSalaryTaxReportContentDetailsQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var salaryTaxContentdata = await _dbContext.SalaryTaxReportContent.FirstOrDefaultAsync(x => x.OfficeId == request.OfficeId && x.IsDeleted == false);

                if (salaryTaxContentdata != null)
                {
                    response.data.SalaryTaxReportContentDetails = salaryTaxContentdata;
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

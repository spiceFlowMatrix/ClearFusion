using AutoMapper;
using HumanitarianAssistance.Application.Accounting.Models;
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
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Configuration.Queries
{
    public class GetAllPensionRateQueryHandler : IRequestHandler<GetAllPensionRateQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public GetAllPensionRateQueryHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(GetAllPensionRateQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                response.data.EmployeePensionRateList = await _dbContext.EmployeePensionRate.Include(x => x.FinancialYearDetail)
                                                                                            .Where(x => x.IsDeleted == false)
                                                                                            .Select(x => new EmployeePensionRateModel
                                                                                            {
                                                                                                FinancialYearId = x.FinancialYearId,
                                                                                                FinancialYearName = x.FinancialYearDetail.FinancialYearName,
                                                                                                PensionRate = x.PensionRate,
                                                                                                IsDefault = x.IsDefault
                                                                                            }).ToListAsync();

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
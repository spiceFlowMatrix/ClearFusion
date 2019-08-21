using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeeHealthInfoQueryHandler : IRequestHandler<GetEmployeeHealthInfoQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetEmployeeHealthInfoQueryHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(GetEmployeeHealthInfoQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var employeeRecord = await _dbContext.EmployeeHealthInfo.FirstOrDefaultAsync(x => x.EmployeeId == request.EmployeeId && x.IsDeleted == false);

                EmployeeHealthInformationModel obj = _mapper.Map<EmployeeHealthInformationModel>(employeeRecord);

                if (employeeRecord != null)
                {
                    response.data.EmployeeHealthInfo = obj;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "No Record Found";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;

        }
    }
}

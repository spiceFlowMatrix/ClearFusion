using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetAllEmployeeHealthDetailByIdQueryHandler : IRequestHandler<GetAllEmployeeHealthDetailByIdQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public GetAllEmployeeHealthDetailByIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllEmployeeHealthDetailByIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {


                var emphealthList = await _dbContext.EmployeeHealthInfo.Where(x => x.EmployeeId == request.EmployeeId).

                Select(x => new EmployeeHealthInformationModel
                {
                    HealthInfoId = x.EmployeeHealthInfoId,
                    EmployeeId = x.EmployeeId.Value,
                    BloodGroup = x.BloodGroup,
                    MedicalHistory = x.HistoryOfPastIllness,
                }).ToListAsync();
                response.data.EmployeeHealthInfoList = emphealthList;
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
using System;
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
    public class GetAllEmployeeEducationsQueryHandler : IRequestHandler<GetAllEmployeeEducationsQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetAllEmployeeEducationsQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllEmployeeEducationsQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var employeeHistoryRecord = await _dbContext.EmployeeEducations.Where(x => x.IsDeleted == false && x.EmployeeID == request.EmployeeId).ToListAsync();

                if (employeeHistoryRecord.Count > 0)
                {
                    response.data.EmployeeEducationsList = employeeHistoryRecord.Select(x => new EmployeeEducationsModel
                    {
                        EmployeeEducationsId = x.EmployeeEducationsId,
                        EmployeeID = Convert.ToInt32(x.EmployeeID),
                        Degree = x.Degree,
                        EducationFrom = Convert.ToDateTime(x.EducationFrom),
                        EducationTo = Convert.ToDateTime(x.EducationTo),
                        FieldOfStudy = x.FieldOfStudy,
                        Institute = x.Institute
                    }).ToList();
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
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
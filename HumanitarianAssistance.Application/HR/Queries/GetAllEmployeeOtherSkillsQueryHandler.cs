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
    public class GetAllEmployeeOtherSkillsQueryHandler : IRequestHandler<GetAllEmployeeOtherSkillsQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetAllEmployeeOtherSkillsQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllEmployeeOtherSkillsQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var employeeHistoryRecord = await _dbContext.EmployeeOtherSkills.Where(x => x.IsDeleted == false && x.EmployeeID == request.EmployeeId).ToListAsync();

                if (employeeHistoryRecord.Count > 0)
                {
                    response.data.EmployeeOtherSkillsList = employeeHistoryRecord.Select(x => new EmployeeOtherSkillsModel
                    {
                        EmployeeOtherSkillsId = x.EmployeeOtherSkillsId,
                        AbilityLevel = x.AbilityLevel,
                        EmployeeID = x.EmployeeID,
                        Experience = x.Experience,
                        Remarks = x.Remarks,
                        TypeOfSkill = x.TypeOfSkill
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

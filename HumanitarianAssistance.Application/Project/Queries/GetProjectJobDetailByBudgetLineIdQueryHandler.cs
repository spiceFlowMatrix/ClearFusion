using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetProjectJobDetailByBudgetLineIdQueryHandler : IRequestHandler<GetProjectJobDetailByBudgetLineIdQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetProjectJobDetailByBudgetLineIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetProjectJobDetailByBudgetLineIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {

                ProjectBudgetLineDetail projectBudgetLineDetail = await _dbContext.ProjectBudgetLineDetail
                                                                                           .Include(x => x.ProjectJobDetail)
                                                                                           .FirstOrDefaultAsync(x => x.IsDeleted == false &&
                                                                                                                     x.BudgetLineId == request.BudgetLineId);

                ProjectJobDetailModel model = new ProjectJobDetailModel();

                if (projectBudgetLineDetail.ProjectJobDetail != null)
                {

                    model.ProjectId = projectBudgetLineDetail.ProjectJobDetail.ProjectId;
                    model.ProjectJobCode = projectBudgetLineDetail.ProjectJobDetail.ProjectJobCode;
                    model.ProjectJobName = projectBudgetLineDetail.ProjectJobDetail.ProjectJobName;
                    model.ProjectJobId = projectBudgetLineDetail.ProjectJobDetail.ProjectJobId;
                }

                response.data.ProjectJobModel = model;
                response.StatusCode = 200;
                response.Message = "Success";

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

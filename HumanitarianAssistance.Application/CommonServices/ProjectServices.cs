using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.CommonModels;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.CommonServices
{
    public class ProjectServices : IProjectServices
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public ProjectServices(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        

        public List<ProjectBudgetLineDetailModel> GetBudgetLineByProjectId(List<ProjectBudgetLineDetailModel> data, long projectId)
        {

            List<ProjectBudgetLineDetailModel> newobj = new List<ProjectBudgetLineDetailModel>();
            var selectedProjectData = data.Where(x => x.ProjectId == projectId).Select(x => new ProjectBudgetLineDetailModel
            {
                ProjectId = x.ProjectId,
                ProjectJobId = x.ProjectJobId,
                ProjectJobCode = x.ProjectJobCode,
                ProjectJobName = x.ProjectJobName,
                BudgetLineId = x.BudgetLineId,
                BudgetCode = x.BudgetCode,
                BudgetName = x.BudgetName,
                InitialBudget = x.InitialBudget,
                CurrencyId = x.CurrencyId,
                CurrencyName = x.CurrencyName,
                CreatedDate = x.CreatedDate,
            }).ToList();

            return selectedProjectData;

        }

        public async Task<string> GetProjectBudgetLineCode(ProjectBudgetLineDetail model)
        {
            ProjectDetail projectDetail = await _dbContext.ProjectDetail
                                                                   .FirstOrDefaultAsync(x => x.ProjectId == model.ProjectId &&
                                                                                             x.IsDeleted == false);
            long projectjobCount = await _dbContext.ProjectBudgetLineDetail
                                                            .LongCountAsync(x => x.ProjectId == model.ProjectId &&
                                                                                 x.IsDeleted == false);
            long id = projectjobCount + 1;

            return ProjectUtility.GenerateProjectBudgetLineCode(projectDetail.ProjectCode,id);
        }

        public async Task<string> GetProjectJobCode(ProjectJobDetail model)
        {
            ProjectDetail projectDetail = await _dbContext.ProjectDetail.FirstOrDefaultAsync(x => x.ProjectId == model.ProjectId &&
                                                                                                           x.IsDeleted == false);
            long projectjobCount = await _dbContext.ProjectJobDetail.LongCountAsync(x => x.ProjectId == model.ProjectId 
                                                                                                );

            return ProjectUtility.GenerateProjectJobCode(projectDetail.ProjectCode, projectjobCount++);
        }

    }
}
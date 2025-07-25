using System.Collections.Generic;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.CommonModels;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Domain.Entities.Project;

namespace HumanitarianAssistance.Application.CommonServicesInterface
{
    public interface IProjectServices
    {
        List<ProjectBudgetLineDetailModel> GetBudgetLineByProjectId(List<ProjectBudgetLineDetailModel> data, long projectId);
        Task<string> GetProjectBudgetLineCode(ProjectBudgetLineDetail model);
        Task<string> GetProjectJobCode(ProjectJobDetail model);
    }
}
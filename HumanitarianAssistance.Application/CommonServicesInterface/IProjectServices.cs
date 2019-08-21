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
        Task<ApiResponse> AddEditProjectSector(ProjectSectorModel model, string UserId);
        Task<ApiResponse> AddEditProjectProgram(ProjectProgramModel model, string UserId);
        List<ProjectBudgetLineDetailModel> GetBudgetLineByProjectId(List<ProjectBudgetLineDetailModel> data, long projectId);
        Task<string> GetProjectBudgetLineCode(ProjectBudgetLineDetail model);
        Task<string> GetProjectJobCode(ProjectJobDetail model);
    }
}
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

        public async Task<ApiResponse> AddEditProjectSector(ProjectSectorModel model, string UserId)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var existRecord = await _dbContext.ProjectSector.FirstOrDefaultAsync(x => x.IsDeleted == false && x.ProjectId == model.ProjectId);

                if (existRecord == null)
                {
                    ProjectSector obj = new ProjectSector();
                    obj.ProjectId = model.ProjectId;
                    obj.SectorId = model.SectorId;
                    obj.IsDeleted = false;
                    obj.CreatedById = UserId;
                    obj.CreatedDate = DateTime.UtcNow;
                    await _dbContext.ProjectSector.AddAsync(obj);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    if (existRecord != null)
                    {
                        existRecord.ProjectId = model.ProjectId;
                        existRecord.SectorId = model.SectorId;
                        existRecord.IsDeleted = false;
                        existRecord.ModifiedById = UserId;
                        existRecord.ModifiedDate = DateTime.UtcNow;
                        _dbContext.ProjectSector.Update(existRecord);
                        await _dbContext.SaveChangesAsync();
                    }
                }
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        public async Task<ApiResponse> AddEditProjectProgram(ProjectProgramModel model, string UserId)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                if (model != null)
                {
                    var existRecord = await _dbContext.ProjectProgram.FirstOrDefaultAsync(x => x.IsDeleted == false && x.ProjectId == model.ProjectId);
                    if (existRecord == null)
                    {
                        ProjectProgram obj = new ProjectProgram();
                        obj.ProjectId = model.ProjectId;
                        obj.ProgramId = model.ProgramId;
                        obj.IsDeleted = false;
                        obj.CreatedById = UserId;
                        obj.CreatedDate = DateTime.UtcNow;
                        await _dbContext.ProjectProgram.AddAsync(obj);
                        await _dbContext.SaveChangesAsync();
                    }
                    else
                    {
                        if (existRecord != null)
                        {
                            existRecord.ProjectId = model.ProjectId;
                            existRecord.ProgramId = model.ProgramId;
                            existRecord.IsDeleted = false;
                            existRecord.ModifiedById = UserId;
                            existRecord.ModifiedDate = DateTime.UtcNow;
                            await _dbContext.SaveChangesAsync();
                        }
                    }
                    response.StatusCode = 200;
                    response.Message = "Success";

                }
                else
                {
                    throw new Exception("Project Program Not Selected");
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
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

            return ProjectUtility.GenerateProjectBudgetLineCode(projectDetail.ProjectCode, projectjobCount++);
        }

        public async Task<string> GetProjectJobCode(ProjectJobDetail model)
        {
            ProjectDetail projectDetail = await _dbContext.ProjectDetail.FirstOrDefaultAsync(x => x.ProjectId == model.ProjectId &&
                                                                                                           x.IsDeleted == false);
            long projectjobCount = await _dbContext.ProjectJobDetail.LongCountAsync(x => x.ProjectId == model.ProjectId &&
                                                                                                  x.IsDeleted == false);

            return ProjectUtility.GenerateProjectJobCode(projectDetail.ProjectCode, projectjobCount++);
        }

    }
}
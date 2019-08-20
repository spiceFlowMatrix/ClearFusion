using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.CommonModels;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class ExcelImportOfBudgetLineQueryHandler : IRequestHandler<ExcelImportOfBudgetLineQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IProjectServices _iProjectServices;
        public ExcelImportOfBudgetLineQueryHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper, IProjectServices iProjectServices)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _iProjectServices = iProjectServices;
        }

        public async Task<ApiResponse> Handle(ExcelImportOfBudgetLineQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                if (request.ProjectId != 0)
                {
                    using (ExcelPackage package = new ExcelPackage(request.File))
                    {
                        ExcelWorksheet workSheet = package.Workbook.Worksheets[1];
                        int totalRows = workSheet.Dimension.Rows;

                        List<ProjectBudgetLineDetailModel> DataList = new List<ProjectBudgetLineDetailModel>();

                        for (int i = 2; i <= totalRows; i++)
                        {
                            DataList.Add(new ProjectBudgetLineDetailModel
                            {
                                ProjectId = Convert.ToInt64(workSheet.Cells[i, 1].Value == null ? null : workSheet.Cells[i, 1].Value.ToString()),
                                //ProjectJobId = Convert.ToInt64(workSheet.Cells[i, 2].Value == null ? null : workSheet.Cells[i, 2].Value.ToString()),
                                ProjectJobCode = workSheet.Cells[i, 2].Value == null ? null : workSheet.Cells[i, 2].Value.ToString(),
                                ProjectJobName = workSheet.Cells[i, 3].Value == null ? null : workSheet.Cells[i, 3].Value.ToString(),
                                //BudgetLineId = Convert.ToInt64(workSheet.Cells[i, 5].Value == null ? null : workSheet.Cells[i, 5].Value.ToString()),
                                BudgetCode = workSheet.Cells[i, 4].Value == null ? null : workSheet.Cells[i, 4].Value.ToString(),
                                BudgetName = workSheet.Cells[i, 5].Value == null ? null : workSheet.Cells[i, 5].Value.ToString(),
                                InitialBudget = Convert.ToInt64(workSheet.Cells[i, 6].Value == null ? null : workSheet.Cells[i, 6].Value.ToString()),
                                CurrencyId = Convert.ToInt32(workSheet.Cells[i, 7].Value == null ? null : workSheet.Cells[i, 7].Value.ToString()),
                                CurrencyName = workSheet.Cells[i, 8].Value == null ? null : workSheet.Cells[i, 8].Value.ToString(),
                                //CreatedDate = Convert.ToDateTime(workSheet.Cells[i, 11].Value == null ? null : workSheet.Cells[i, 11].Value.ToString()),
                            });
                            //Console.WriteLine("code", code);
                        }

                        //Note: GetBudgetLine List by project Id 
                        List<ProjectBudgetLineDetailModel> projectListdata = _iProjectServices.GetBudgetLineByProjectId(DataList, request.ProjectId);

                        if (projectListdata.Count > 0)
                        {
                            foreach (var item in projectListdata)
                            {
                                ProjectBudgetLineDetail budgetLineDetailExist = new ProjectBudgetLineDetail();


                                if (!string.IsNullOrEmpty(item.BudgetCode) && !string.IsNullOrEmpty(item.ProjectJobCode))
                                {

                                    //Note : check fro existing Record budget and job
                                    budgetLineDetailExist = await _dbContext
                                                                      .ProjectBudgetLineDetail
                                                                      .Include(x => x.ProjectJobDetail)
                                                                      .FirstOrDefaultAsync(x =>
                                                                                           x.ProjectId == item.ProjectId &&
                                                                                           x.BudgetCode == item.BudgetCode &&
                                                                                           x.ProjectJobDetail.ProjectJobCode == item.ProjectJobCode &&
                                                                                           x.IsDeleted == false);


                                    if (budgetLineDetailExist == null)
                                    {
                                        ProjectBudgetLineDetail obj = _mapper.Map<ProjectBudgetLineDetailModel, ProjectBudgetLineDetail>(item);

                                        // CASE 1 :: Note: check for creating a new budgetLine having existing Project Job
                                        if (item.BudgetCode == null && item.ProjectJobCode != null)
                                        {

                                            ProjectBudgetLineDetail ifExistbudgetDetail = await IfexistBudgetLine(item.BudgetName);

                                            if (ifExistbudgetDetail == null)
                                            {
                                                obj.CreatedDate = DateTime.UtcNow;
                                                obj.IsDeleted = false;
                                                obj.CreatedById = request.UserId;

                                                await _dbContext.ProjectBudgetLineDetail.AddAsync(obj);
                                                await _dbContext.SaveChangesAsync();

                                                if (obj.BudgetLineId != 0)
                                                {
                                                    obj.BudgetCode = await GetProjectBudgetLineCode(obj);

                                                    _dbContext.ProjectBudgetLineDetail.Update(obj);
                                                    await _dbContext.SaveChangesAsync();
                                                }
                                            }
                                            // note : update  budgetline with new
                                            else
                                            {
                                                obj.ModifiedDate = DateTime.UtcNow;
                                                obj.IsDeleted = false;
                                                obj.ModifiedById = request.UserId;
                                                _dbContext.ProjectBudgetLineDetail.Update(obj);
                                                await _dbContext.SaveChangesAsync();
                                            }

                                        }
                                        // CASE 2 ::  Note: create a new project job first then add new budgetLine 
                                        else if (item.BudgetCode == null && item.ProjectJobCode == null)
                                        {
                                            // Note: call method to check job exsit or not
                                            var ifJobExist = await IfExistProjectJob(item.ProjectJobName);
                                            //call add ProjectJob when job is new 
                                            if (ifJobExist == null)
                                            {
                                                var projectJobObj = await AddProjectJob(item.ProjectId.Value, item.ProjectJobName, request.UserId);

                                                //Note : check for budget exists

                                                var ifBudgetExist = await IfexistBudgetLine(item.BudgetName);

                                                if (ifBudgetExist == null)
                                                {
                                                    ProjectBudgetLineDetail budgetLineObj = await AddEditProjectBudgetLine(item, projectJobObj.ProjectJobId, request.UserId);
                                                }
                                                else
                                                {
                                                    //Note : if budgetLine exist already then update the newly created job with previous one
                                                    ifBudgetExist.ProjectJobId = projectJobObj.ProjectJobId;
                                                    _dbContext.ProjectBudgetLineDetail.Update(ifBudgetExist);
                                                    await _dbContext.SaveChangesAsync();
                                                }
                                                //Note: add the newly created project job with new budget line
                                            }

                                            else
                                            {
                                                //Note : if project job is already exist and we created a new budgetLine then update budgetLine
                                                ProjectBudgetLineDetail budgetLineObj = await AddEditProjectBudgetLine(item, ifJobExist.ProjectJobId, request.UserId);
                                            }
                                        }
                                        // Case 3::
                                        else if (item.BudgetCode != null && item.ProjectJobCode == null)
                                        {
                                            //check for is the project job name is already exist 
                                            var ifJobDetailExist = await IfExistProjectJob(item.ProjectJobName);
                                            if (ifJobDetailExist == null)
                                            {
                                                //add new job here
                                                var projectJobObj = await AddProjectJob(item.ProjectId.Value, item.ProjectJobName, request.UserId);

                                                //Note: add the newly created project job with new budget line
                                                ProjectBudgetLineDetail budgetLineObj = await AddEditProjectBudgetLine(item, projectJobObj.ProjectJobId, request.UserId);
                                            }
                                            else
                                            {
                                                //Note : if project job exist and budgetLine already exist then do nothing else update the 
                                                var ifBudgetExist = await IfexistBudgetLine(item.BudgetName);

                                                // Note: if new budget line then update the newly created project job with new budget line
                                                if (ifBudgetExist == null)
                                                {
                                                    ProjectBudgetLineDetail budgetLineObj = await AddEditProjectBudgetLine(item, ifJobDetailExist.ProjectJobId, request.UserId);
                                                }
                                            }

                                        }
                                        //CASE 4::
                                        else if (item.BudgetCode != null && item.ProjectJobCode != null)
                                        {
                                            //Note: check string format for budget code and project code
                                            bool ifBudgetCodeFormtCorrect = CheckBudgetCodeFormat(item.BudgetCode);

                                            // Note: check for project job code status
                                            bool ifProjectCodeFormatCorrect = CheckProjectCodeFormat(item.ProjectJobCode);

                                            if (ifBudgetCodeFormtCorrect && ifProjectCodeFormatCorrect)
                                            {
                                                //var ifJobExist = await IfExistProjectJob(item.ProjectJobName);
                                                var ifjobExist = await _dbContext.ProjectJobDetail
                                                                                .FirstOrDefaultAsync(x => x.ProjectJobCode == item.ProjectJobCode &&
                                                                                                                    x.IsDeleted == false);
                                                if (ifjobExist == null)
                                                {
                                                    var projectJobObj = await AddProjectJob(item.ProjectId.Value, item.ProjectJobName, request.UserId);

                                                    var ifExistBudgetCode = await _dbContext.ProjectBudgetLineDetail
                                                                                                     .FirstOrDefaultAsync(x => x.BudgetCode == item.BudgetCode &&
                                                                                                                                x.IsDeleted == false);
                                                    if (ifExistBudgetCode == null)
                                                    {
                                                        ProjectBudgetLineDetail budgetLineObj = await AddEditProjectBudgetLine(item, projectJobObj.ProjectJobId, request.UserId);
                                                    }
                                                    //update the project budget
                                                    else
                                                    {
                                                        ifExistBudgetCode.ProjectJobId = projectJobObj.ProjectJobId;
                                                        _dbContext.ProjectBudgetLineDetail.Update(ifExistBudgetCode);
                                                        await _dbContext.SaveChangesAsync();
                                                    }
                                                }
                                                else
                                                {
                                                    var ifExistBudgetCode = await _dbContext.ProjectBudgetLineDetail
                                                                                                     .FirstOrDefaultAsync(x => x.BudgetCode == item.BudgetCode &&
                                                                                                                                x.IsDeleted == false);
                                                    if (ifExistBudgetCode == null)
                                                    {
                                                        ProjectBudgetLineDetail budgetLineObj = await AddEditProjectBudgetLine(item, ifjobExist.ProjectJobId, request.UserId);
                                                    }
                                                    //update the project budget
                                                    else
                                                    {
                                                        ifExistBudgetCode.ProjectJobId = ifjobExist.ProjectJobId;
                                                        ifExistBudgetCode.ModifiedDate = DateTime.UtcNow;
                                                        ifExistBudgetCode.ModifiedById = request.UserId;
                                                        _dbContext.ProjectBudgetLineDetail.Update(ifExistBudgetCode);
                                                        await _dbContext.SaveChangesAsync();

                                                    }

                                                }
                                            }

                                            else
                                            {
                                                throw new Exception("Please provide correct format");
                                            }
                                        }

                                        //  response.data.TransactionBudgetModelList = budgetLineDetailExist;

                                    }
                                }
                                //Note : if budget code and job code are empty check for new budget line on the bases of name
                                else
                                {
                                    if (!string.IsNullOrEmpty(item.BudgetName) && !string.IsNullOrEmpty(item.ProjectJobName))
                                    {
                                        ProjectBudgetLineDetail obj = _mapper.Map<ProjectBudgetLineDetailModel, ProjectBudgetLineDetail>(item);
                                        // Note: call method to check job exsit or not
                                        var ifJobExist = await IfExistProjectJob(item.ProjectJobName);
                                        var ifBudgetExist = await IfexistBudgetLine(item.BudgetName);

                                        //call add ProjectJob when job is new 
                                        if (ifJobExist == null)
                                        {
                                            var projectJobObj = await AddProjectJob(item.ProjectId.Value, item.ProjectJobName, request.UserId);

                                            if (ifBudgetExist == null)
                                            {

                                                ProjectBudgetLineDetail budgetLineObj = await AddEditProjectBudgetLine(item, projectJobObj.ProjectJobId, request.UserId);

                                            }
                                            else
                                            {
                                                //Note : if budgetLine exist already then update the newly created job with previous one
                                                ifBudgetExist.ProjectJobId = projectJobObj.ProjectJobId;
                                                _dbContext.ProjectBudgetLineDetail.Update(ifBudgetExist);
                                                await _dbContext.SaveChangesAsync();

                                            }
                                        }
                                        else
                                        {
                                            if (ifBudgetExist == null)
                                            {
                                                ProjectBudgetLineDetail budgetLineObj = await AddEditProjectBudgetLine(item, ifJobExist.ProjectJobId, request.UserId);
                                            }
                                            else
                                            {
                                                //Note : if budgetLine exist already then update the newly created job with previous one
                                                ifBudgetExist.ProjectJobId = ifJobExist.ProjectJobId;
                                                _dbContext.ProjectBudgetLineDetail.Update(ifBudgetExist);
                                                await _dbContext.SaveChangesAsync();
                                            }
                                        }
                                    }

                                }

                                response.StatusCode = StaticResource.successStatusCode;
                                response.Message = "Success";
                            }
                        }

                        else
                        {
                            response.StatusCode = StaticResource.notFoundCode;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex;
            }
            return response;


        }

        public async Task<ProjectBudgetLineDetail> IfexistBudgetLine(string item)
        {
            ProjectBudgetLineDetail ifExistbudgetDetail = await _dbContext.ProjectBudgetLineDetail
                                                                           .FirstOrDefaultAsync(x =>
                                                                            x.BudgetName == item &&
                                                                            x.IsDeleted == false);
            return ifExistbudgetDetail;
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

        public async Task<ProjectJobDetail> IfExistProjectJob(string item)
        {
            ProjectJobDetail ifJobExist = await _dbContext.ProjectJobDetail
                                                                  .FirstOrDefaultAsync(x =>
                                                                                       x.ProjectJobName == item &&
                                                                                       x.IsDeleted == false);
            return ifJobExist;

        }

        public async Task<ProjectJobDetail> AddProjectJob(long projectId, string projectJobName, string userId)
        {
            ProjectJobDetail projectJobObj = new ProjectJobDetail()
            {
                ProjectId = projectId,
                ProjectJobName = projectJobName,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false,
                CreatedById = userId
            };

            await _dbContext.ProjectJobDetail.AddAsync(projectJobObj);
            await _dbContext.SaveChangesAsync();

            if (projectJobObj.ProjectJobId != 0)
            {
                // update project job code
                projectJobObj.ProjectJobCode = await GetProjectJobCode(projectJobObj);

                _dbContext.ProjectJobDetail.Update(projectJobObj);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Project Job can not be created");
            }

            return projectJobObj;
        }

        public async Task<ProjectBudgetLineDetail> AddEditProjectBudgetLine(ProjectBudgetLineDetailModel item, long projectJobId, string userId)
        {

            ProjectBudgetLineDetail budgetLineObj = new ProjectBudgetLineDetail()
            {
                ProjectJobId = projectJobId,
                ProjectId = item.ProjectId,
                InitialBudget = item.InitialBudget,
                CurrencyId = item.CurrencyId,
                BudgetName = item.BudgetName,
                CreatedDate = DateTime.UtcNow,
                IsDeleted = false,
                CreatedById = userId,
                BudgetCode = item.BudgetCode
            };
            await _dbContext.ProjectBudgetLineDetail.AddAsync(budgetLineObj);
            await _dbContext.SaveChangesAsync();


            if (budgetLineObj.BudgetLineId != 0)
            {
                budgetLineObj.BudgetCode = await GetProjectBudgetLineCode(budgetLineObj);
                //Note : update using repository not working thats why update using entity. 
                _dbContext.ProjectBudgetLineDetail.Update(budgetLineObj);
                await _dbContext.SaveChangesAsync();


            }

            else
            {
                throw new Exception("Budget line can not be created");
            }
            return budgetLineObj;
        }

        public bool CheckBudgetCodeFormat(string budgetCode)
        {
            bool isFormatCorrect = false;
            if (!string.IsNullOrEmpty(budgetCode))
            {
                string budgetFirstIndex = budgetCode.Substring(0, 1);
                if (budgetFirstIndex == "P")
                {
                    isFormatCorrect = budgetCode.Split('-')[1].Contains('B');
                }
            }
            return isFormatCorrect;
        }

        public bool CheckProjectCodeFormat(string jobCode)
        {
            bool isProjectCodeCorrect = false;

            if (!string.IsNullOrEmpty(jobCode))
            {
                string jobCodeFirstIndex = jobCode.Substring(0, 1);

                if (jobCodeFirstIndex == "P")
                {
                    isProjectCodeCorrect = jobCode.Split('-')[1].Contains('J');
                }
            }
            return isProjectCodeCorrect;
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
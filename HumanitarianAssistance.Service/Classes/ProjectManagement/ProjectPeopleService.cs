using AutoMapper;
using DataAccess;
using DataAccess.DbEntities;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces.ProjectManagement;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HumanitarianAssistance.Common.Helpers;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.ViewModels.Models.Project;
using DataAccess.DbEntities.Project;

namespace HumanitarianAssistance.Service.Classes.ProjectManagement
{

    public class ProjectPeopleService : IProjectPeopleService
    {
        IUnitOfWork _uow;
        IMapper _mapper;
        UserManager<AppUser> _userManager;

        public ProjectPeopleService(
            IUnitOfWork uow,
            IMapper mapper,
            UserManager<AppUser> userManager
            )
        {
            _uow = uow;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<APIResponse> GetOpportunityControlList(long projectId)
        {
            APIResponse response = new APIResponse();

            try
            {
                var opportunityList = await _uow.GetDbContext().ProjectOpportunityControl
                                                               .Where(x => x.IsDeleted == false && x.ProjectId == projectId)
                                                               .Select(x => new OpportunityControlViewModel
                                                               {
                                                                   Id = x.Id,
                                                                   ProjectId = x.ProjectId,
                                                                   RoleId = x.RoleId,
                                                                   UserId = x.UserID,
                                                                   DateAdded = x.CreatedDate ?? DateTime.UtcNow
                                                               })
                                                               .ToListAsync();

                response.data.OpportunityControlList = opportunityList;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<APIResponse> AddOpportunityControl(OpportunityControlAddModel model, string userId)
        {
            APIResponse response = new APIResponse();

            try
            {
                ProjectOpportunityControl obj = new ProjectOpportunityControl
                {
                    ProjectId = model.ProjectId,
                    UserID = model.UserId,
                    RoleId = model.UserId,
                    CreatedDate = DateTime.UtcNow,
                    CreatedById = userId,
                };

                await _uow.ProjectOpportunityControlRepository.AddAsyn(obj);

                response.CommonId.LongId = obj.Id;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<APIResponse> EditOpportunityControl(OpportunityControlEditModel model, string userId)
        {
            APIResponse response = new APIResponse();

            try
            {
                var opportunityDetail = await _uow.GetDbContext().ProjectOpportunityControl
                                                                   .FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == model.Id);

                if (opportunityDetail == null)
                {
                    throw new Exception(StaticResource.OpportunityControlNotfound);
                }

                ProjectOpportunityControl obj = new ProjectOpportunityControl
                {
                    UserID = model.UserId,
                    RoleId = model.UserId,
                    ModifiedDate = DateTime.UtcNow,
                    ModifiedById = userId,
                };

                await _uow.ProjectOpportunityControlRepository.UpdateAsyn(obj);

                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }

            return response;
        }




        public async Task<APIResponse> GetLogisticsControlList(long projectId)
        {
            APIResponse response = new APIResponse();

            try
            {
                var logisticsList = await _uow.GetDbContext().ProjectLogisticsControl
                                                               .Where(x => x.IsDeleted == false && x.ProjectId == projectId)
                                                               .Select(x => new LogisticsControlViewModel
                                                               {
                                                                   Id = x.Id,
                                                                   ProjectId = x.ProjectId,
                                                                   RoleId = x.RoleId,
                                                                   UserId = x.UserID,
                                                                   DateAdded = x.CreatedDate ?? DateTime.UtcNow
                                                               })
                                                               .ToListAsync();

                response.data.LogisticsControlList = logisticsList;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<APIResponse> AddLogisticsControl(LogisticsControlAddModel model, string userId)
        {
            APIResponse response = new APIResponse();

            try
            {
                ProjectLogisticsControl obj = new ProjectLogisticsControl
                {
                    ProjectId = model.ProjectId,
                    UserID = model.UserId,
                    RoleId = model.UserId,
                    CreatedDate = DateTime.UtcNow,
                    CreatedById = userId,
                };

                await _uow.ProjectLogisticsControlRepository.AddAsyn(obj);

                response.CommonId.LongId = obj.Id;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<APIResponse> EditLogisticsControl(LogisticsControlEditModel model, string userId)
        {
            APIResponse response = new APIResponse();

            try
            {
                var opportunityDetail = await _uow.GetDbContext().ProjectLogisticsControl
                                                                   .FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == model.Id);

                if (opportunityDetail == null)
                {
                    throw new Exception(StaticResource.OpportunityControlNotfound);
                }

                ProjectLogisticsControl obj = new ProjectLogisticsControl
                {
                    UserID = model.UserId,
                    RoleId = model.UserId,
                    ModifiedDate = DateTime.UtcNow,
                    ModifiedById = userId,
                };

                await _uow.ProjectLogisticsControlRepository.UpdateAsyn(obj);

                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }

            return response;
        }




        public async Task<APIResponse> GetActivitiesControlList(long projectId)
        {
            APIResponse response = new APIResponse();

            try
            {
                var activitiesList = await _uow.GetDbContext().ProjectActivitiesControl
                                                               .Where(x => x.IsDeleted == false && x.ProjectId == projectId)
                                                               .Select(x => new ActivitiesControlViewModel
                                                               {
                                                                   Id = x.Id,
                                                                   ProjectId = x.ProjectId,
                                                                   RoleId = x.RoleId,
                                                                   UserId = x.UserID,
                                                                   DateAdded = x.CreatedDate ?? DateTime.UtcNow
                                                               })
                                                               .ToListAsync();

                response.data.ActivitiesControlList = activitiesList;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<APIResponse> AddActivitiesControl(ActivitiesControlAddModel model, string userId)
        {
            APIResponse response = new APIResponse();

            try
            {
                ProjectActivitiesControl obj = new ProjectActivitiesControl
                {
                    ProjectId = model.ProjectId,
                    UserID = model.UserId,
                    RoleId = model.UserId,
                    CreatedDate = DateTime.UtcNow,
                    CreatedById = userId,
                };

                await _uow.ProjectActivitiesControlRepository.AddAsyn(obj);

                response.CommonId.LongId = obj.Id;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<APIResponse> EditActivitiesControl(ActivitiesControlEditModel model, string userId)
        {
            APIResponse response = new APIResponse();

            try
            {
                var opportunityDetail = await _uow.GetDbContext().ProjectActivitiesControl
                                                                   .FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == model.Id);

                if (opportunityDetail == null)
                {
                    throw new Exception(StaticResource.OpportunityControlNotfound);
                }

                ProjectActivitiesControl obj = new ProjectActivitiesControl
                {
                    UserID = model.UserId,
                    RoleId = model.UserId,
                    ModifiedDate = DateTime.UtcNow,
                    ModifiedById = userId,
                };

                await _uow.ProjectActivitiesControlRepository.UpdateAsyn(obj);

                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }

            return response;
        }



        public async Task<APIResponse> GetHiringControlList(long projectId)
        {
            APIResponse response = new APIResponse();

            try
            {
                var hiringList = await _uow.GetDbContext().ProjectHiringControl
                                                               .Where(x => x.IsDeleted == false && x.ProjectId == projectId)
                                                               .Select(x => new HiringControlViewModel
                                                               {
                                                                   Id = x.Id,
                                                                   ProjectId = x.ProjectId,
                                                                   RoleId = x.RoleId,
                                                                   UserId = x.UserID,
                                                                   DateAdded = x.CreatedDate ?? DateTime.UtcNow
                                                               })
                                                               .ToListAsync();

                response.data.HiringControlList = hiringList;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<APIResponse> AddHiringControl(HiringControlAddModel model, string userId)
        {
            APIResponse response = new APIResponse();

            try
            {
                ProjectHiringControl obj = new ProjectHiringControl
                {
                    ProjectId = model.ProjectId,
                    UserID = model.UserId,
                    RoleId = model.UserId,
                    CreatedDate = DateTime.UtcNow,
                    CreatedById = userId,
                };

                await _uow.ProjectHiringControlRepository.AddAsyn(obj);

                response.CommonId.LongId = obj.Id;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<APIResponse> EditHiringControl(HiringControlEditModel model, string userId)
        {
            APIResponse response = new APIResponse();

            try
            {
                var opportunityDetail = await _uow.GetDbContext().ProjectHiringControl
                                                                 .FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == model.Id);

                if (opportunityDetail == null)
                {
                    throw new Exception(StaticResource.OpportunityControlNotfound);
                }

                ProjectHiringControl obj = new ProjectHiringControl
                {
                    UserID = model.UserId,
                    RoleId = model.UserId,
                    ModifiedDate = DateTime.UtcNow,
                    ModifiedById = userId,
                };

                await _uow.ProjectHiringControlRepository.UpdateAsyn(obj);

                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
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

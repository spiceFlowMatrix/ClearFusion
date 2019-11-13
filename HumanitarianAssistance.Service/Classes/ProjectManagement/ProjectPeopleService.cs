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

        public async Task<bool> ValidateOpportunityControl(long? opportunityControlId, long projectId, int userId, int roleId)
        {
            bool validateOpportunity = await _uow.ProjectOpportunityControlRepository
                                                           .AnyAsync(x => x.IsDeleted == false &&
                                                                          x.ProjectId == projectId &&
                                                                          (opportunityControlId != null ? x.Id != opportunityControlId : true) &&
                                                                          x.UserID == userId &&
                                                                          x.RoleId == roleId);
            if (validateOpportunity)
            {
                throw new Exception(StaticResource.sameRoleAlreadyExistForTheUser);
            }

            return true;
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
                    IsDeleted = false
                };

                // validation
                await ValidateOpportunityControl(null, model.ProjectId, model.UserId, model.RoleId);

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
                var opportunityDetail = await _uow.ProjectOpportunityControlRepository
                                                  .FindAsync(x => x.IsDeleted == false && x.Id == model.Id);

                if (opportunityDetail == null)
                {
                    throw new Exception(StaticResource.OpportunityControlNotfound);
                }

                // validation
                await ValidateOpportunityControl(opportunityDetail.Id, model.ProjectId, model.UserId, model.RoleId);


                opportunityDetail.UserID = model.UserId;
                opportunityDetail.RoleId = model.RoleId;

                opportunityDetail.ModifiedDate = DateTime.UtcNow;
                opportunityDetail.ModifiedById = userId;
                opportunityDetail.IsDeleted = false;

                await _uow.ProjectOpportunityControlRepository.UpdateAsyn(opportunityDetail);

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

        public async Task<APIResponse> DeleteOpportunityControl(long opportunityControlId, string userId)
        {
            APIResponse response = new APIResponse();

            try
            {
                var opportunityDetail = await _uow.ProjectOpportunityControlRepository
                                                  .FindAsync(x => x.IsDeleted == false && x.Id == opportunityControlId);

                if (opportunityDetail == null)
                {
                    throw new Exception(StaticResource.OpportunityControlNotfound);
                }

                opportunityDetail.ModifiedDate = DateTime.UtcNow;
                opportunityDetail.ModifiedById = userId;
                opportunityDetail.IsDeleted = true;

                await _uow.ProjectOpportunityControlRepository.UpdateAsyn(opportunityDetail);

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

        public async Task<bool> ValidateLogisticsControl(long projectId, int userId, int roleId)
        {
            bool validateLogistics = await _uow.ProjectLogisticsControlRepository
                                                           .AnyAsync(x => x.IsDeleted == false &&
                                                                          x.ProjectId == projectId &&
                                                                          x.UserID == userId &&
                                                                          x.RoleId == roleId);
            if (validateLogistics)
            {
                throw new Exception(StaticResource.sameRoleAlreadyExistForTheUser);
            }

            return true;
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

                // validation
                await ValidateLogisticsControl(model.ProjectId, model.UserId, model.RoleId);

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
                var logisticsDetail = await _uow.ProjectLogisticsControlRepository
                                                .FindAsync(x => x.IsDeleted == false && x.Id == model.Id);

                if (logisticsDetail == null)
                {
                    throw new Exception(StaticResource.LogisticsControlNotfound);
                }


                logisticsDetail.UserID = model.UserId;
                logisticsDetail.RoleId = model.RoleId;

                logisticsDetail.ModifiedDate = DateTime.UtcNow;
                logisticsDetail.ModifiedById = userId;
                logisticsDetail.IsDeleted = false;

                await _uow.ProjectLogisticsControlRepository.UpdateAsyn(logisticsDetail);

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

        public async Task<APIResponse> DeleteLogisticsControl(long logisticsControlId, string userId)
        {
            APIResponse response = new APIResponse();

            try
            {
                var logisticsDetail = await _uow.ProjectLogisticsControlRepository
                                                .FindAsync(x => x.IsDeleted == false && x.Id == logisticsControlId);

                if (logisticsDetail == null)
                {
                    throw new Exception(StaticResource.LogisticsControlNotfound);
                }


                logisticsDetail.ModifiedDate = DateTime.UtcNow;
                logisticsDetail.ModifiedById = userId;
                logisticsDetail.IsDeleted = true;

                await _uow.ProjectLogisticsControlRepository.UpdateAsyn(logisticsDetail);

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

        public async Task<bool> ValidateActivitiesControl(long projectId, int userId, int roleId)
        {
            bool validateActivities = await _uow.ProjectActivitiesControlRepository
                                                           .AnyAsync(x => x.IsDeleted == false &&
                                                                          x.ProjectId == projectId &&
                                                                          x.UserID == userId &&
                                                                          x.RoleId == roleId);
            if (validateActivities)
            {
                throw new Exception(StaticResource.sameRoleAlreadyExistForTheUser);
            }

            return true;
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

                // validation
                await ValidateActivitiesControl(model.ProjectId, model.UserId, model.RoleId);

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
                var activitiesDetail = await _uow.ProjectActivitiesControlRepository
                                                 .FindAsync(x => x.IsDeleted == false && x.Id == model.Id);

                if (activitiesDetail == null)
                {
                    throw new Exception(StaticResource.ActivitiesControlNotfound);
                }


                activitiesDetail.UserID = model.UserId;
                activitiesDetail.RoleId = model.RoleId;

                activitiesDetail.ModifiedDate = DateTime.UtcNow;
                activitiesDetail.ModifiedById = userId;
                activitiesDetail.IsDeleted = false;


                await _uow.ProjectActivitiesControlRepository.UpdateAsyn(activitiesDetail);

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

        public async Task<APIResponse> DeleteActivitiesControl(long activitiesControlId, string userId)
        {
            APIResponse response = new APIResponse();

            try
            {
                var activitiesDetail = await _uow.ProjectActivitiesControlRepository
                                                 .FindAsync(x => x.IsDeleted == false && x.Id == activitiesControlId);

                if (activitiesDetail == null)
                {
                    throw new Exception(StaticResource.ActivitiesControlNotfound);
                }


                activitiesDetail.ModifiedDate = DateTime.UtcNow;
                activitiesDetail.ModifiedById = userId;
                activitiesDetail.IsDeleted = true;

                await _uow.ProjectActivitiesControlRepository.UpdateAsyn(activitiesDetail);

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

        public async Task<APIResponse> GetActivitiesControlPermission(long projectId, string userId)
        {
            APIResponse response = new APIResponse();

            try
            {
                var userDetail = await _uow.UserDetailsRepository
                                           .FindAsync(x => x.AspNetUserId == userId && x.IsDeleted == false);

                if (userDetail == null) {
                    throw new Exception(StaticResource.UserNotExist);
                }

                ICollection<ProjectActivitiesControl> permissions = await _uow.ProjectActivitiesControlRepository
                                                                                .FindAllAsync(x => x.ProjectId == projectId && 
                                                                                                   x.UserID == userDetail.UserID &&
                                                                                                   x.IsDeleted == false);

                List<ProjectActivityPermissionModel> permissionList = permissions.Select(x => new ProjectActivityPermissionModel {
                                                                                                Id = x.Id,
                                                                                                ProjectId = x.ProjectId,
                                                                                                RoleId = x.RoleId,
                                                                                                UserId = x.UserID,
                                                                                                DateAdded = x.CreatedDate
                                                                                        }).ToList();


                response.data.ProjectActivityPermissionList = permissionList;
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

        public async Task<bool> ValidateHiringControl(long projectId, int userId, int roleId)
        {
            bool validateHiring = await _uow.ProjectHiringControlRepository
                                                           .AnyAsync(x => x.IsDeleted == false &&
                                                                          x.ProjectId == projectId &&
                                                                          x.UserID == userId &&
                                                                          x.RoleId == roleId);
            if (validateHiring)
            {
                throw new Exception(StaticResource.sameRoleAlreadyExistForTheUser);
            }

            return true;
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

                // validation
                await ValidateHiringControl(model.ProjectId, model.UserId, model.RoleId);

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
                var hiringDetail = await _uow.ProjectHiringControlRepository
                                             .FindAsync(x => x.IsDeleted == false && x.Id == model.Id);

                if (hiringDetail == null)
                {
                    throw new Exception(StaticResource.HiringControlNotfound);
                }


                hiringDetail.UserID = model.UserId;
                hiringDetail.RoleId = model.RoleId;

                hiringDetail.ModifiedDate = DateTime.UtcNow;
                hiringDetail.ModifiedById = userId;
                hiringDetail.IsDeleted = false;

                await _uow.ProjectHiringControlRepository.UpdateAsyn(hiringDetail);

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

        public async Task<APIResponse> DeleteHiringControl(long hiringControlId, string userId)
        {
            APIResponse response = new APIResponse();

            try
            {
                var hiringDetail = await _uow.GetDbContext().ProjectHiringControl
                                                                   .FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == hiringControlId);

                if (hiringDetail == null)
                {
                    throw new Exception(StaticResource.HiringControlNotfound);
                }


                hiringDetail.ModifiedDate = DateTime.UtcNow;
                hiringDetail.ModifiedById = userId;
                hiringDetail.IsDeleted = true;

                await _uow.ProjectHiringControlRepository.UpdateAsyn(hiringDetail);

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

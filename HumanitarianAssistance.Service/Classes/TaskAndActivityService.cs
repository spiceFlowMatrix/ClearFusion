using AutoMapper;
using DataAccess;
using DataAccess.DbEntities;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces;
using HumanitarianAssistance.ViewModels.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.Classes
{
    public class TaskAndActivityService : ITaskAndActivity
    {
        IUnitOfWork _uow;
        IMapper _mapper;
        UserManager<AppUser> _userManager;
        public TaskAndActivityService(IUnitOfWork uow, IMapper mapper, UserManager<AppUser> userManager)
        {
            this._uow = uow;
            this._mapper = mapper;
            this._userManager = userManager;
        }
        public async Task<APIResponse> AddTask(TaskMasterModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                TaskMaster obj = _mapper.Map<TaskMaster>(model);
                await _uow.TaskMasterRepository.AddAsyn(obj);
                await _uow.SaveAsync();
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

        public async Task<APIResponse> EditTask(TaskMasterModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                var taskinfo = await _uow.TaskMasterRepository.FindAsync(x => x.TaskId == model.TaskId);
                if (taskinfo != null)
                {
                    taskinfo.TaskName = model.TaskName;
                    taskinfo.Priority = model.Priority;
                    taskinfo.Description = model.Description;
                    taskinfo.Status = model.Status;
                    taskinfo.ModifiedById = model.ModifiedById;
                    taskinfo.ModifiedDate = model.ModifiedDate;
                    await _uow.TaskMasterRepository.UpdateAsyn(taskinfo);
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

        public async Task<APIResponse> GetAllTask(int projectid)
        {
            APIResponse response = new APIResponse();
            try
            {
                var tasklist = await Task.Run(() =>
                    _uow.GetDbContext().TaskMaster.Include(x=> x.ActivityMaster).Where(t=> t.IsDeleted == false && t.ProjectId == projectid).ToListAsync()
                );

                var projecttasklist = tasklist.Select(x => new TaskMasterModel
                {
                    TaskId = x.TaskId,
                    TaskName = x.TaskName,
                    Priority = x.Priority,
                    Description = x.Description,
                    Status = x.Status,

                    ActivityMasterList = x.ActivityMaster.Select(a => new ActivityMasterModel
                    {
                        ActivityId = a.ActivityId,
                        ActivityName = a.ActivityName,
                        Priority = a.Priority,
                        Description = a.Description,
                        Status = a.Status
                    }).ToList(),
                }).ToList();
                response.data.TaskMasterList = projecttasklist;
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

        public async Task<APIResponse> AddActivityDetail(ActivityMasterModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                ActivityMaster obj = _mapper.Map<ActivityMaster>(model);
                await _uow.ActivityMasterRepository.AddAsyn(obj);
                await _uow.SaveAsync();
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

        public async Task<APIResponse> EditActivityDetail(ActivityMasterModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                var activityinfo = await _uow.ActivityMasterRepository.FindAsync(x => x.ActivityId == model.ActivityId);
                if (activityinfo != null)
                {
                    activityinfo.TaskId = model.TaskId;
                    activityinfo.ActivityName = model.ActivityName;
                    activityinfo.Priority = model.Priority;
                    activityinfo.Description = model.Description;
                    activityinfo.ModifiedById = model.ModifiedById;
                    activityinfo.ModifiedDate = model.ModifiedDate;
                    await _uow.ActivityMasterRepository.UpdateAsyn(activityinfo);
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

        public async Task<APIResponse> GetAllActivity()
        {
            APIResponse response = new APIResponse();
            try
            {
                var list = await Task.Run(() =>
                    _uow.GetDbContext().ActivityMaster.Include(x=> x.TaskMaster).Where(a=> a.IsDeleted == false).ToListAsync()
                );

                var activitylist = list.Select(x => new ActivityMasterModel
                {
                    ActivityId = x.ActivityId,
                    TaskId = x.TaskId,
                    TaskName = x.TaskMaster.TaskName,
                    ActivityName = x.ActivityName,
                    Priority = x.Priority,
                    Description = x.Description
                }).ToList();
                response.data.ActivityMasterList = activitylist;
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

        public async Task<APIResponse> AddAssignActivityDetail(AssignActivityModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                AssignActivity obj = _mapper.Map<AssignActivity>(model);
                await _uow.AssignActivityRepository.AddAsyn(obj);
                await _uow.SaveAsync();
                var assignactivityid = obj.AssignActivityId;

                foreach (var i in model.UserForApprovelist)
                {
                    AssignActivityApproveByModel activityapprove = new AssignActivityApproveByModel();
                    activityapprove.AssignActivityId = assignactivityid;
                    activityapprove.ApprovedById = i.ApprovedById;
                    activityapprove.Date = DateTime.UtcNow;
                    
                    AssignActivityApproveBy obj1 = _mapper.Map<AssignActivityApproveBy>(activityapprove);
                    await _uow.AssignActivityApproveByRepository.AddAsyn(obj1);
                    await _uow.SaveAsync();
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

        public async Task<APIResponse> GetAllAssignActivityDetailByCondition(long? ProjectId, int? TaskId, int? ActivityId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var list = await Task.Run(() =>
                    _uow.GetDbContext().AssignActivity.Include(a=> a.ActivityMaster).Include(u=> u.User).Where(x=> x.IsDeleted == false).ToListAsync()
                );

                if (ProjectId != null && ProjectId != 0)
                {
                    list = list.Where(x => x.ProjectId == ProjectId).ToList();
                }
                else if (TaskId != null && TaskId != 0)
                {
                    list = list.Where(x => x.TaskId == TaskId).ToList();
                }
                else if (ActivityId != null && ActivityId != 0)
                {
                    list = list.Where(x => x.ActivityId == ActivityId).ToList();
                }

                var assignactivitylist = list.Select(x => new AssignActivityModel
                {
                    AssignActivityId = x.AssignActivityId,
                    ProjectId = x.ProjectId,
                    TaskId = x.TaskId,
                    ActivityId = x.ActivityId,
                    ActivityName = x.ActivityMaster.ActivityName,
                    UserId = x.UserId,
                    UserName = x.User.UserName,
                    PlannedStartDate = x.PlannedStartDate,
                    PlannedEndDate = x.PlannedEndDate,
                    ActualStartDate = x.ActualStartDate,
                    ActualEndDate = x.ActualEndDate,
                    Status = x.Status,
                    ApprovedStatus = x.ApprovedStatus,
                }).ToList();
                response.data.AssignActivityList = assignactivitylist;
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
    }
}

﻿using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.ViewModels.Models.Marketing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.interfaces.Marketing
{
    public interface IPolicyService
    {
        Task<APIResponse> AddEditPolicy(PolicyModel model, string UserId);
        Task<APIResponse> GetAllPolicyList();
        Task<APIResponse> DeletePolicy(int model, string UserId);
        Task<APIResponse> FilterPolicyList(PolicyFilterModel model, string userId);
        Task<APIResponse> GetPolicyPaginatedList(PolicyPaginationModel model, string UserId);
        Task<APIResponse> GetPolicyById(int model, string UserId);
        Task<APIResponse> AddEditPolicySchedules(ScheduleDetailsModel model, string UserId);
        Task<APIResponse> GetScheduleByDate(string model, string UserId);
        Task<APIResponse> AddEditPolicyTimeSchedule(PolicyTimeScheduleModel model, string UserId);
        Task<APIResponse> GetAllSchedule(string UserId);
        Task<APIResponse> GetPolicyTimeScheduleList(int id,string UserId);
        Task<APIResponse> DeletePolicyTimeSchedule(int id, string UserId);
        Task<APIResponse> GetPolicyTimeScheduleById(int id, string UserId);
        Task<APIResponse> AddPolicyRepeatDays(PolicyTimeModel model, string UserId);
        Task<APIResponse> GetDayScheduleByPolicyId(int id, string UserId);
        Task<APIResponse> AddEditPolicyOrderSchedule(PolicyOrderScheduleModel model, string UserId);
    }
}

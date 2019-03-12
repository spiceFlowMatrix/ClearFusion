using AutoMapper;
using DataAccess;
using DataAccess.DbEntities;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces.Marketing;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using HumanitarianAssistance.ViewModels.Models.Marketing;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Service.Classes.Marketing
{
    public class SchedulerService : ISchedulerService
    {
        IUnitOfWork _uow;
        IMapper _mapper;
        UserManager<AppUser> _userManager;
        public SchedulerService(IUnitOfWork uow, IMapper mapper, UserManager<AppUser> userManager)
        {
            this._uow = uow;
            this._mapper = mapper;
            this._userManager = userManager;
        }
        public async Task<APIResponse> GetAllPolicyScheduleList()
        {
            APIResponse response = new APIResponse();
            try
            {
                var policyScheduleList = await (from j in _uow.GetDbContext().PolicyTimeSchedules                                                   
                                                    join mc in _uow.GetDbContext().PolicyDetails on j.PolicyId equals mc.PolicyId
                                                where !j.IsDeleted.Value && !mc.IsDeleted.Value
                                                //&& !jp.IsDeleted.Value && !me.IsDeleted.Value
                                                //&& !mc.IsDeleted.Value
                                                select (new ScheduleTimeModel
                                                {
                                                   PolicyId = mc.PolicyId,
                                                   Name = mc.PolicyName,
                                                   Id = j.Id,
                                                   StartTime = j.StartTime,
                                                   EndTime = j.EndTime
                                                })).ToListAsync();
                response.data.scheduleTimeList = policyScheduleList;
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

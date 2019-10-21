using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Marketing.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Marketing;
using HumanitarianAssistance.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Marketing.Commands.Common
{
    public class AddEditPolicySchedulesCommandHandler : IRequestHandler<AddEditPolicySchedulesCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddEditPolicySchedulesCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(AddEditPolicySchedulesCommand request, CancellationToken cancellationToken)
        {
            PolicyScheduleModel mdl = new PolicyScheduleModel();
            mdl.ByDay = request.ByDay;
            mdl.ByMonth = request.ByMonth;
            mdl.ByWeek = request.ByWeek;
            mdl.Description = request.Description;
            mdl.EndTime = TimeSpan.Parse(request.EndTime);
            mdl.StartTime = TimeSpan.Parse(request.StartTime);
            mdl.Title = request.Title;
            mdl.RepeatDays = string.Join(",", request.RepeatDays);
            mdl.Frequency = request.Frequency;
            mdl.PolicyId = request.PolicyId;
            mdl.PolicyScheduleId = mdl.PolicyScheduleId;
            mdl.StartDate = DateTime.Parse(request.StartDate);
            mdl.EndDate = DateTime.Parse(request.EndDate);
            long LatestScheduleId = 0;
            var scheduleCode = string.Empty;
            ApiResponse response = new ApiResponse();
            try
            {
                if (request.PolicyScheduleId == 0)
                {
                    var schedule = _dbContext.PolicySchedules.Where(x => x.Title == request.Title && x.IsDeleted == false).FirstOrDefault();
                    if (schedule == null)
                    {
                        var policyDetail = _dbContext.PolicySchedules.OrderByDescending(x => x.PolicyScheduleId)
                                                                                       .FirstOrDefault();
                        if (policyDetail == null)
                        {
                            LatestScheduleId = 1;
                            scheduleCode = LatestScheduleId.ToString().GetPolicyCode();
                        }
                        else
                        {
                            LatestScheduleId = Convert.ToInt32(policyDetail.PolicyId) + 1;
                            scheduleCode = LatestScheduleId.ToString().GetPolicyCode();
                        }
                        PolicySchedule obj = new PolicySchedule();
                        _mapper.Map<PolicyScheduleModel, PolicySchedule>(mdl);
                        obj.CreatedById = request.CreatedById;
                        obj.ScheduleCode = scheduleCode;
                        obj.CreatedDate = DateTime.Now;
                        obj.IsDeleted = false;
                        obj.isActive = true;
                        obj.Description = mdl.Description;
                        obj.ByDay = mdl.ByDay;
                        obj.ByMonth = mdl.ByMonth;
                        obj.ByWeek = mdl.ByWeek;
                        obj.EndDate = mdl.EndDate;
                        obj.EndTime = mdl.EndTime;
                        obj.Frequency = mdl.Frequency;
                        obj.PolicyId = mdl.PolicyId;
                        obj.StartDate = mdl.StartDate;
                        obj.StartTime = mdl.StartTime;
                        obj.Title = mdl.Title;
                        await _dbContext.PolicySchedules.AddAsync(obj);
                        await _dbContext.SaveChangesAsync();
                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = "Schedule created successfully.";
                    }
                    else
                    {
                        response.StatusCode = StaticResource.failStatusCode;
                        response.Message = "Schedule already exists. Please try again with other Title.";
                    }
                }
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

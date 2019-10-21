using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Marketing.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Marketing.Queries
{
    public class GetScheduleByDateQueryHandler : IRequestHandler<GetScheduleByDateQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetScheduleByDateQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(GetScheduleByDateQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                int count = await _dbContext.PolicySchedules.Where(x => x.StartDate == DateTime.Parse(request.date)).CountAsync(x => x.IsDeleted == false);
                var policyScheduleList = await (from j in _dbContext.PolicySchedules
                                                    //join jp in _uow.GetDbContext().LanguageDetail on j.LanguageId equals jp.LanguageId
                                                    //join me in _uow.GetDbContext().Mediums on j.MediumId equals me.MediumId
                                                    //join mc in _uow.GetDbContext().MediaCategories on j.MediaCategoryId equals mc.MediaCategoryId
                                                where !j.IsDeleted && j.StartDate == DateTime.Parse(request.date)
                                                //&& !jp.IsDeleted && !me.IsDeleted
                                                //&& !mc.IsDeleted
                                                select (new PolicyScheduleModel
                                                {
                                                    PolicyId = j.PolicyId,
                                                    Title = j.Title,
                                                    ScheduleCode = j.ScheduleCode,
                                                    Description = j.Description,
                                                    ByDay = j.ByDay,
                                                    ByMonth = j.ByMonth,
                                                    ByWeek = j.ByWeek,
                                                    EndDate = j.EndDate,
                                                    EndTime = j.EndTime,
                                                    Frequency = j.Frequency,
                                                    isActive = j.isActive,
                                                    PolicyScheduleId = j.PolicyScheduleId,
                                                    RepeatDays = j.RepeatDays,
                                                    StartDate = j.StartDate,
                                                    StartTime = j.StartTime
                                                })).ToListAsync();
                response.data.TotalCount = count;
                response.StatusCode = 200;
                response.Message = "Success";
                response.data.policySchedulesByDateList = policyScheduleList;
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

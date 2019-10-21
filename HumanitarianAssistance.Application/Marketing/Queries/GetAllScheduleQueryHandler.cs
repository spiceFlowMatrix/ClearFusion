using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Marketing.Models;
using HumanitarianAssistance.Common.Helpers;

namespace HumanitarianAssistance.Application.Marketing.Queries
{
    public class GetAllScheduleQueryHandler : IRequestHandler<GetAllScheduleQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetAllScheduleQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(GetAllScheduleQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                int count = await _dbContext.PolicySchedules.CountAsync(x => x.IsDeleted == false);
                var policyScheduleList = await (from j in _dbContext.PolicySchedules
                                                    //join jp in _uow.GetDbContext().LanguageDetail on j.LanguageId equals jp.LanguageId
                                                    //join me in _uow.GetDbContext().Mediums on j.MediumId equals me.MediumId
                                                    //join mc in _uow.GetDbContext().MediaCategories on j.MediaCategoryId equals mc.MediaCategoryId
                                                where !j.IsDeleted
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
                                                    //LanguageId = jp.LanguageId,
                                                    //LanguageName = jp.LanguageName,
                                                    //MediumId = me.MediumId,
                                                    //MediumName = me.MediumName,
                                                    //MediaCategoryId = mc.MediaCategoryId,
                                                    //MediaCategoryName = mc.CategoryName
                                                }))
                                          //.Take(10).Skip(0).OrderByDescending(x => x.)
                                          .ToListAsync();

                response.data.policySchedulesByDateList = policyScheduleList;
                response.data.TotalCount = count;
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

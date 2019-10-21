using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Accounting.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetAllUserNotificationsQueryHandler : IRequestHandler<GetAllUserNotificationsQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public GetAllUserNotificationsQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllUserNotificationsQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var lst = await _dbContext.LoggerDetails.Where(x => x.CreatedById == request.UserId).ToListAsync();

                List<LoggerModel> loggerList = new List<LoggerModel>();

                foreach (var item in lst)
                {
                    LoggerModel obj = new LoggerModel();
                    obj.userId = item.CreatedById;
                    obj.createdDate = DateTime.Now;
                    obj.notificationId = item.NotificationId;
                    obj.loggerDetailsId = item.LoggerDetailsId;
                    obj.isRead = item.IsRead;
                    obj.userName = item.UserName;
                    obj.loggedDetail = item.LoggedDetail;
                    loggerList.Add(obj);
                }

                response.data.LoggerDetailsModelList = loggerList.OrderByDescending(x => x.createdDate).ToList();
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
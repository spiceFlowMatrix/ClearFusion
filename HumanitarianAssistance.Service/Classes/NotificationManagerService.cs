using DataAccess;
using DataAccess.DbEntities;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.Classes
{
    public class NotificationManagerService : INotificationManager
    {
        IUnitOfWork _uow;
        UserManager<AppUser> _userManager;

        public NotificationManagerService(IUnitOfWork uow, UserManager<AppUser> userManager)
        {
            _uow = uow;
            _userManager = userManager;
        }

        public async Task<APIResponse> SetNotificationIsReadFlag(int loggerDetailsId)
        {
            APIResponse response = new APIResponse();

            if (loggerDetailsId != 0)
            {
                var notificationData = await _uow.LoggerDetailsRepository.FindAsync(x => x.LoggerDetailsId == loggerDetailsId);

                if (notificationData != null)
                {
                    notificationData.IsRead = true;

                    await _uow.LoggerDetailsRepository.UpdateAsyn(notificationData);
                    await _uow.SaveAsync();


                    response.data.notificationIsReadCount = _uow.LoggerDetailsRepository.FindAll(x => x.IsRead == false).Count;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "No Result Found";
                }
            }
            else
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = "Incorrect Id";
            }
            return response;
        }

        public async Task<APIResponse> GetNotificationIsReadCount()
        {
            APIResponse response = new APIResponse();

            var item = await _uow.LoggerDetailsRepository.FindAllAsync(x => x.IsRead == false);

            response.data.notificationIsReadCount = item.Count;
            response.StatusCode = StaticResource.successStatusCode;
            response.Message = "Success";

            return response;
        }


    }
}

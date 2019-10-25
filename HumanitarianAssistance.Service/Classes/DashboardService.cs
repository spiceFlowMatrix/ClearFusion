using AutoMapper;
using DataAccess;
using DataAccess.DbEntities;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces;
using Microsoft.AspNetCore.Identity;
using System;

namespace HumanitarianAssistance.Service.Classes
{
    public class DashboardService : IDashboardService
    {
        IUnitOfWork _uow;
        IMapper _mapper;
        UserManager<AppUser> _userManager;
        public DashboardService(IUnitOfWork uow, IMapper mapper, UserManager<AppUser> userManager)
        {
            this._uow = uow;
            this._mapper = mapper;
            this._userManager = userManager;
        }

        public APIResponse GetTrainingLink()
        {
            APIResponse response = new APIResponse();
            try
            {
                var trainingLink = GetEnvironmentVariable("DOCS_URL");
                if (trainingLink == null)
                {
                    trainingLink = SetEnvironmentVariable("DOCS_URL", StaticResource.TrainingDocUrl);
                }
                response.data.TrainingLink = trainingLink;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }


        public string GetEnvironmentVariable(string key)
        {
            return Environment.GetEnvironmentVariable(key);
        }

        public string SetEnvironmentVariable(string key, string value)
        {
            Environment.SetEnvironmentVariable(key, value);
            return Environment.GetEnvironmentVariable(key);
        }
    }
}

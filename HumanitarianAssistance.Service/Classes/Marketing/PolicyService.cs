using AutoMapper;
using DataAccess;
using DataAccess.DbEntities;
using DataAccess.DbEntities.Marketing;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces.Marketing;
using HumanitarianAssistance.ViewModels.Models.Marketing;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.Classes.Marketing
{
    public class PolicyService: IPolicyService
    {
        IUnitOfWork _uow;
        IMapper _mapper;
        UserManager<AppUser> _userManager;
        public PolicyService(IUnitOfWork uow, IMapper mapper, UserManager<AppUser> userManager)
        {
            this._uow = uow;
            this._mapper = mapper;
            this._userManager = userManager;
        }
        public async Task<APIResponse> AddEditPolicy(PolicyModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                if (model.PolicyId == 0 || model.PolicyId == null)
                {
                    PolicyDetail obj = _mapper.Map<PolicyModel, PolicyDetail>(model);
                    obj.CreatedById = UserId;
                    obj.MediumId = model.MediumId;
                    obj.MediaCategoryId = model.MediaCategoryId;
                    obj.PolicyName = model.PolicyName;
                    obj.CreatedDate = DateTime.Now;
                    obj.IsDeleted = false;
                    obj.Description = model.Description;
                    await _uow.PolicyRepository.AddAsyn(obj);
                    await _uow.SaveAsync();
                    //response.data.activityById = obj;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    //ActivityType obj = await _uow.ActivityTypeRepository.FindAsync(x => x.ActivityTypeId == model.ActivityTypeId);
                    //obj.ModifiedById = UserId;
                    //obj.ModifiedDate = DateTime.Now;
                    //_mapper.Map(model, obj);
                    //await _uow.ActivityTypeRepository.UpdateAsyn(obj);
                    //await _uow.SaveAsync();
                    //response.data.activityById = obj;
                    //response.StatusCode = StaticResource.successStatusCode;
                    //response.Message = "Success";
                }
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

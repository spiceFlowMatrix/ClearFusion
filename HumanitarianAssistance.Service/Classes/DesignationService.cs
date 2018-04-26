using AutoMapper;
using DataAccess;
using DataAccess.DbEntities;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces;
using HumanitarianAssistance.ViewModels.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.Classes
{
    public class DesignationService : IDesignation
    {
        IUnitOfWork _uow;
        IMapper _mapper;
        UserManager<AppUser> _userManager;
        public DesignationService(IUnitOfWork uow, IMapper mapper, UserManager<AppUser> userManager)
        {
            this._uow = uow;
            this._mapper = mapper;
            this._userManager = userManager;
        }

        public async Task<APIResponse> AddDesignation(DesignationModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                DesignationDetail obj = _mapper.Map<DesignationDetail>(model);
                await _uow.DesignationDetailRepository.AddAsyn(obj);
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

        public async Task<APIResponse> EditDesignation(DesignationModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                var designationinfo = await _uow.DesignationDetailRepository.FindAsync(x => x.DesignationId == model.DesignationId && x.IsDeleted == false);
                if (designationinfo != null)
                {
                    designationinfo.Designation = model.Designation;
                    designationinfo.ModifiedById = model.ModifiedById;
                    designationinfo.ModifiedDate = model.ModifiedDate;
                    await _uow.DesignationDetailRepository.UpdateAsyn(designationinfo);
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Sucess";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> GetAllDesignation()
        {
            APIResponse response = new APIResponse();
            try
            {
                var designationlist = (from c in await _uow.DesignationDetailRepository.GetAllAsyn()
                                      select new DesignationModel
                                      {
                                          DesignationId = c.DesignationId,
                                          Designation = c.Designation
                                      }).ToList();
                response.data.DesignationList = designationlist;
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

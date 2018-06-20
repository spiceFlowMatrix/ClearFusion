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
    public class OfficeDetailsService : IOfficeDetails
    {
        IUnitOfWork _uow;
        IMapper _mapper;
        UserManager<AppUser> _userManager;
        public OfficeDetailsService(IUnitOfWork uow, IMapper mapper, UserManager<AppUser> userManager)
        {
            this._uow = uow;
            this._mapper = mapper;
            this._userManager = userManager;
        }

        public async Task<APIResponse> AddOfficeDetails(OfficeDetailModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                var existoffice = await _uow.OfficeDetailRepository.FindAsync(o => o.OfficeCode == model.OfficeCode || o.OfficeKey == model.OfficeKey);
                if (existoffice == null)
                {
                    OfficeDetail obj = _mapper.Map<OfficeDetail>(model);
                    //obj.CreatedById = model.CreatedById;
                    //obj.CreatedDate = DateTime.UtcNow;
                    //obj.IsDeleted = false;
                    await _uow.OfficeDetailRepository.AddAsyn(obj);
                    await _uow.SaveAsync();
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.MandateNameAlreadyExistCode;
                    response.Message = StaticResource.MandateNameAlreadyExist;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> EditOfficeDetails(OfficeDetailModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                var officeInfo = await _uow.OfficeDetailRepository.FindAsync(c => c.OfficeId == model.OfficeId);
				officeInfo.OfficeCode = model.OfficeCode;
				officeInfo.OfficeName = model.OfficeName;
                officeInfo.SupervisorName = model.SupervisorName;
                officeInfo.PhoneNo = model.PhoneNo;
                officeInfo.FaxNo = model.FaxNo;
				officeInfo.ModifiedById = model.ModifiedById;
				officeInfo.ModifiedDate = model.ModifiedDate;
				await _uow.OfficeDetailRepository.UpdateAsyn(officeInfo, officeInfo.OfficeId, officeInfo.OfficeCode);
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

        public async Task<APIResponse> DeleteOfficeDetails(OfficeDetailsModelDelete model)
        {
            APIResponse response = new APIResponse();
            try
            {
                var officeInfo = await _uow.OfficeDetailRepository.FindAsync(c => c.OfficeId == model.OfficeId);
                officeInfo.IsDeleted = true;
                officeInfo.ModifiedById = model.ModifiedById;
                officeInfo.ModifiedDate = model.ModifiedDate;
                await _uow.OfficeDetailRepository.UpdateAsyn(officeInfo, officeInfo.OfficeId, officeInfo.OfficeCode);
                //await _uow.OfficeDetailRepository.DeleteAsyn(officeInfo);
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

        public async Task<APIResponse> GetAllOfficeDetails()
        {
            APIResponse response = new APIResponse();
            try
            {
                var officelist = (from o in await _uow.OfficeDetailRepository.GetAllAsyn()
                                  where o.IsDeleted == false
                                    select new OfficeDetailModel
                                    {  
                                        OfficeId = o.OfficeId,
                                        OfficeCode = o.OfficeCode,
                                        OfficeName = o.OfficeName,
                                        SupervisorName = o.SupervisorName,
                                        PhoneNo = o.PhoneNo,
                                        FaxNo = o.FaxNo,
                                        OfficeKey = o.OfficeKey,
                                        CreatedById = o.CreatedById,
                                        CreatedDate = o.CreatedDate,
                                        ModifiedById = o.ModifiedById,
                                        ModifiedDate = o.ModifiedDate
                                    }).ToList();
                response.data.OfficeDetailsList = officelist;
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

        public async Task<APIResponse> GetOfficeDetailsByOfficeCode(string OfficeCode)
        {
            APIResponse response = new APIResponse();
            try
            {
                var officelist = (from o in await _uow.OfficeDetailRepository.GetAllAsyn()
                                  where o.OfficeCode == OfficeCode
                                  select new OfficeDetailModel
                                  {
                                      OfficeCode = o.OfficeCode,
                                      OfficeName = o.OfficeName,
                                      SupervisorName = o.SupervisorName,
                                      PhoneNo = o.PhoneNo,
                                      FaxNo = o.FaxNo,
                                      OfficeKey = o.OfficeKey,
                                      CreatedById = o.CreatedById,
                                      CreatedDate = o.CreatedDate,
                                      ModifiedById = o.ModifiedById,
                                      ModifiedDate = o.ModifiedDate
                                  }).ToList();
                response.data.OfficeDetailsList = officelist;
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

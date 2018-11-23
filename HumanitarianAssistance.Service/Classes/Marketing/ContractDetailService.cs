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
using System.Linq;

namespace HumanitarianAssistance.Service.Classes.Marketing
{
    public class ContractDetailService : IContractDetailsService
    {
        IUnitOfWork _uow;
        IMapper _mapper;
        UserManager<AppUser> _userManager;
        public ContractDetailService(IUnitOfWork uow, IMapper mapper, UserManager<AppUser> userManager)
        {
            this._uow = uow;
            this._mapper = mapper;
            this._userManager = userManager;
        }

        public async Task<APIResponse> GetContractDetailsById(int contractId, string userId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var clientDetails = (from ur in _uow.GetDbContext().ClientDetails
                                     join at in _uow.GetDbContext().Categories on ur.CategoryId equals at.CategoryId
                                     into jou
                                     from dev_skill in jou.DefaultIfEmpty()
                                     from at in _uow.GetDbContext().Categories
                                     where !ur.IsDeleted.Value && !at.IsDeleted.Value && ur.ClientId == contractId
                                     select (new ClientDetailModel
                                     {
                                         CategoryId = ur.CategoryId,
                                         CategoryName = dev_skill.CategoryName ?? String.Empty,
                                         ClientBackground = ur.ClientBackground,
                                         ClientCode = ur.ClientCode,
                                         ClientId = ur.ClientId,
                                         ClientName = ur.ClientName,
                                         Email = ur.Email,
                                         FocalPoint = ur.FocalPoint,
                                         History = ur.History,
                                         Phone = ur.Phone,
                                         PhysicialAddress = ur.PhysicialAddress,
                                         Position = ur.Position
                                     })).FirstOrDefault();

                response.data.clientDetailsById = clientDetails;
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

        /// <summary>
        /// Get All Contracts List
        /// </summary>
        /// <returns></returns>
        public async Task<APIResponse> GetAllContractDetails()
        {
            APIResponse response = new APIResponse();
            try
            {
                var list = await _uow.ContractDetailsRepository.FindAllAsync(x => !x.IsDeleted.Value);
                response.data.ContractDetails = list;
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

        /// <summary>
        /// Add New Contract
        /// </summary>
        /// <param name="model"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<APIResponse> AddContractDetails(ContractDetailsModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                ContractDetails obj = _mapper.Map<ContractDetailsModel, ContractDetails>(model);
                obj.ClientName = model.ClientName;
                obj.ContractCode = model.ContractCode;
                obj.UnitRate = model.UnitRate;
                obj.QualityId = model.QualityId;
                obj.NatureId = model.NatureId;
                obj.MediumId = model.MediumId;
                obj.MediaCategoryId = model.MediaCategoryId;
                obj.LanguageId = model.LanguageId;
                obj.IsCompleted = false;
                obj.StartDate = model.StartDate;
                obj.EndDate = model.EndDate;
                obj.CurrencyId = model.CurrencyId;
                obj.ActivityTypeId = model.ActivityTypeId;
                obj.IsDeleted = false;
                obj.CreatedById = UserId;
                obj.CreatedDate = DateTime.Now;
                await _uow.ContractDetailsRepository.AddAsyn(obj);
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

        /// <summary>
        /// Edit selected Contract Details
        /// </summary>
        /// <param name="model"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<APIResponse> EditContractDetails(ContractDetailsModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var existRecord = await _uow.ContractDetailsRepository.FindAsync(x => x.IsDeleted == false && x.ContractId == model.ContractId);
                if (existRecord != null)
                {
                    _mapper.Map(model, existRecord);
                    existRecord.IsDeleted = false;
                    existRecord.ModifiedById = UserId;
                    existRecord.ModifiedDate = DateTime.Now;

                    await _uow.ContractDetailsRepository.UpdateAsyn(existRecord);

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Record not found";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Delete Selected Contract
        /// </summary>
        /// <param name="jobid"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<APIResponse> DeleteContractDetail(ContractDetails model)
        {
            APIResponse response = new APIResponse();
            try
            {
                var contractInfo = await _uow.ContractDetailsRepository.FindAsync(c => c.ContractId == model.ContractId);
                contractInfo.IsDeleted = true;
                contractInfo.ModifiedById = model.ModifiedById;
                contractInfo.ModifiedDate = model.ModifiedDate;
                await _uow.ContractDetailsRepository.UpdateAsyn(contractInfo, contractInfo.ContractId);
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

        /// <summary>
        /// Add or Edit Contract Detail
        /// </summary>
        /// <param name="model"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<APIResponse> AddEditContractDetail(ContractDetailsModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                if (model.ContractId == 0)
                {
                    ContractDetails obj = _mapper.Map<ContractDetailsModel, ContractDetails>(model);
                    obj.ContractId = model.ContractId;
                    obj.ActivityTypeId = model.ActivityTypeId;
                    obj.ClientName = model.ClientName;
                    obj.ContractCode = model.ContractCode;
                    obj.IsDeleted = false;
                    obj.CreatedById = UserId;
                    obj.CreatedDate = DateTime.Now;
                    obj.CurrencyId = model.CurrencyId;
                    obj.StartDate = model.StartDate;
                    obj.EndDate = model.EndDate;
                    obj.IsCompleted = false;
                    obj.LanguageId = model.LanguageId;
                    obj.MediaCategoryId = model.MediaCategoryId;
                    obj.MediumId = model.MediumId;
                    obj.NatureId = model.NatureId;
                    obj.QualityId = model.QualityId;
                    obj.TimeCategoryId = model.TimeCategoryId;
                    await _uow.ContractDetailsRepository.AddAsyn(obj);
                    await _uow.SaveAsync();
                }
                else
                {
                    var existRecord = await _uow.ContractDetailsRepository.FindAsync(x => x.IsDeleted == false && x.ContractId == model.ContractId);
                    if (existRecord != null)
                    {
                        _mapper.Map(model, existRecord);
                        existRecord.IsCompleted = true;
                        existRecord.IsDeleted = false;
                        existRecord.ModifiedById = UserId;
                        existRecord.ModifiedDate = DateTime.Now;
                        await _uow.ContractDetailsRepository.UpdateAsyn(existRecord);
                    }
                }
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

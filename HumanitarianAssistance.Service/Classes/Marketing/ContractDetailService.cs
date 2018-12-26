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

        public async Task<APIResponse> GetContractByClientId(int model, string userId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var contracts = (from j in _uow.GetDbContext().ContractDetails
                                 join jp in _uow.GetDbContext().ClientDetails on j.ClientId equals jp.ClientId
                                 where !j.IsDeleted.Value && !jp.IsDeleted.Value && j.ClientId == model
                                 select (new ContractByClient
                                 {
                                     ClientId = jp.ClientId,
                                     ClientName = jp.ClientName,
                                     ContractId = j.ContractId
                                 })).ToList();
                response.data.ContractByClientList = contracts;
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

        public async Task<APIResponse> GetContractDetailsById(int contractId, string userId)
        {
            APIResponse response = new APIResponse();
            ContractDetailsModel contractDetails = new ContractDetailsModel();
            try
            {
                var existRecords = await _uow.ContractDetailsRepository.FindAsync(x => x.IsDeleted == false && x.ContractId == contractId);
                contractDetails.ActivityTypeId = existRecords.ActivityTypeId;
                contractDetails.ClientName = existRecords.ClientName;
                contractDetails.ContractCode = existRecords.ContractCode;
                contractDetails.ContractId = existRecords.ContractId;
                contractDetails.CurrencyId = existRecords.CurrencyId;
                contractDetails.EndDate = existRecords.EndDate;
                contractDetails.LanguageId = existRecords.LanguageId;
                contractDetails.MediaCategoryId = existRecords.MediaCategoryId;
                contractDetails.MediumId = existRecords.MediumId;
                contractDetails.UnitRateId = existRecords.UnitRateId;
                contractDetails.ClientId = existRecords.ClientId;
                contractDetails.NatureId = existRecords.NatureId;
                contractDetails.QualityId = existRecords.QualityId;
                contractDetails.StartDate = existRecords.StartDate;
                contractDetails.TimeCategoryId = existRecords.TimeCategoryId;
                contractDetails.UnitRate = existRecords.UnitRate;
                contractDetails.IsApproved = existRecords.IsApproved;
                contractDetails.IsDeclined = existRecords.IsDeclined;
                //var clientDetails = (from ur in _uow.GetDbContext().ClientDetails
                //                     join at in _uow.GetDbContext().Categories on ur.CategoryId equals at.CategoryId
                //                     into jou
                //                     from dev_skill in jou.DefaultIfEmpty()
                //                     from at in _uow.GetDbContext().Categories
                //                     where !ur.IsDeleted.Value && !at.IsDeleted.Value && ur.ClientId == contractId
                //                     select (new ClientDetailModel
                //                     {
                //                         CategoryId = ur.CategoryId,
                //                         CategoryName = dev_skill.CategoryName ?? String.Empty,
                //                         ClientBackground = ur.ClientBackground,
                //                         ClientCode = ur.ClientCode,
                //                         ClientId = ur.ClientId,
                //                         ClientName = ur.ClientName,
                //                         Email = ur.Email,
                //                         FocalPoint = ur.FocalPoint,
                //                         History = ur.History,
                //                         Phone = ur.Phone,
                //                         PhysicialAddress = ur.PhysicialAddress,
                //                         Position = ur.Position
                //                     })).FirstOrDefault();

                response.data.contractDetailsModel = contractDetails;
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

        public async Task<APIResponse> GetAllContractDetailsByClientId()
        {
            APIResponse response = new APIResponse();
            
            List<ContractByClient> modelList = new List<ContractByClient>();

            try
            {
                var list = await _uow.ContractDetailsRepository.FindAllAsync(x => !x.IsDeleted.Value && x.IsApproved == true);
                foreach (var item in list)
                {
                    ContractByClient model = new ContractByClient();
                    model.ContractByClients = item.ClientName + "" + "-" + item.ContractId;
                    model.ContractId = item.ContractId;
                    model.ClientName = item.ClientName;
                    model.ClientId = item.ClientId;
                    model.UnitRate = item.UnitRate;
                    model.CurrencyId = item.CurrencyId;
                    modelList.Add(model);
                }
               
                response.data.ContractByClientList = modelList;
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
                obj.ClientId = model.ClientId;
                obj.UnitRateId = model.UnitRateId;
                obj.ContractCode = model.ContractCode;
                obj.UnitRate = model.UnitRate;
                obj.QualityId = model.QualityId;
                obj.NatureId = model.NatureId;
                obj.MediumId = model.MediumId;
                obj.MediaCategoryId = model.MediaCategoryId;
                obj.LanguageId = model.LanguageId;
                obj.IsCompleted = false;
                obj.StartDate = Convert.ToDateTime(model.StartDate);
                obj.EndDate = Convert.ToDateTime(model.EndDate);
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
        public async Task<APIResponse> DeleteContractDetail(int model, string userId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var contractInfo = await _uow.ContractDetailsRepository.FindAsync(c => c.ContractId == model);
                contractInfo.IsDeleted = true;
                contractInfo.ModifiedById = userId;
                contractInfo.ModifiedDate = DateTime.UtcNow;
                await _uow.ContractDetailsRepository.UpdateAsyn(contractInfo, contractInfo.ContractId);
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Contract Deleted Successfully";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        public string getContractCode(string ContractId)
        {
            string code = string.Empty;
            if (ContractId.Length == 1)
                return code = "0000" + ContractId;
            else if (ContractId.Length == 2)
                return code = "000" + ContractId;
            else if (ContractId.Length == 3)
                return code = "00" + ContractId;
            else if (ContractId.Length == 4)
                return code = "0" + ContractId;
            else
                return code = "" + ContractId;
        }

        /// <summary>
        /// Add or Edit Contract Detail
        /// </summary>
        /// <param name="model"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<APIResponse> AddEditContractDetail(ContractDetailsModel model, string UserId)
        {
            ContractDetails contractDetails = new ContractDetails();
            long LatestContractId = 0;
            var contractcode = string.Empty;
            var client = _uow.GetDbContext().ClientDetails.Where(x => x.ClientId == model.ClientId && x.IsDeleted == false).FirstOrDefault();
            model.ClientName = client.ClientName;
            APIResponse response = new APIResponse();
            ContractDetailsModel conDetails = new ContractDetailsModel();
            try
            {
                if (model.ContractId == 0)
                {
                    var ContractDetail = _uow.GetDbContext().ContractDetails
                                                               .OrderByDescending(x => x.ContractId)
                                                               .FirstOrDefault();
                    if (ContractDetail == null)
                    {
                        LatestContractId = 1;
                        contractcode = getContractCode(LatestContractId.ToString());
                    }
                    else
                    {
                        LatestContractId = Convert.ToInt32(ContractDetail.ContractId) + 1;
                        contractcode = getContractCode(LatestContractId.ToString());
                    }
                    ContractDetails obj = _mapper.Map<ContractDetailsModel, ContractDetails>(model);
                    obj.ContractId = model.ContractId;
                    obj.ActivityTypeId = model.ActivityTypeId;
                    obj.ClientName = model.ClientName;
                    obj.ContractCode = contractcode;
                    obj.IsDeleted = false;
                    obj.CreatedById = UserId;
                    obj.UnitRate = model.UnitRate;
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
                    obj.IsApproved = model.IsApproved;
                    if (model.Type == "Approve")
                    {
                        obj.IsApproved = true;
                    }
                    if (model.Type == "Rejected")
                    {
                        obj.IsDeclined = true;
                    }
                    await _uow.ContractDetailsRepository.AddAsyn(obj);
                    await _uow.SaveAsync();
                    conDetails.ActivityTypeId = obj.ActivityTypeId;
                    conDetails.ClientName = obj.ClientName;
                    conDetails.ContractCode = obj.ContractCode;
                    conDetails.ContractId = obj.ContractId;
                    conDetails.CurrencyId = obj.CurrencyId;
                    conDetails.EndDate = obj.EndDate;
                    conDetails.LanguageId = obj.LanguageId;
                    conDetails.MediaCategoryId = obj.MediaCategoryId;
                    conDetails.MediumId = obj.MediumId;
                    conDetails.NatureId = obj.NatureId;
                    conDetails.QualityId = obj.QualityId;
                    conDetails.StartDate = obj.StartDate;
                    conDetails.TimeCategoryId = obj.TimeCategoryId;
                    conDetails.UnitRate = obj.UnitRate;
                    conDetails.IsApproved = obj.IsApproved;
                    if (obj.IsDeclined == true)
                    {
                        conDetails.IsDeclined = obj.IsDeclined;
                    }
                    if (obj.IsApproved == true)
                    {
                        conDetails.IsApproved = obj.IsApproved;
                    }
                    response.data.contractDetailsModel = conDetails;
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
                        response.data.contractDetailsModel = model;
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

        public async Task<APIResponse> ApproveContract(ApproveContractModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            var existRecord = await _uow.ContractDetailsRepository.FindAsync(x => x.IsDeleted == false && x.ContractId == model.ContractId);
            if (existRecord != null)
            {
                try
                {
                    if (model.Type == "Approve")
                    {
                        existRecord.IsApproved = true;
                    }
                    if (model.Type == "Decline")
                    {
                        existRecord.IsDeclined = true;
                    }
                    existRecord.ModifiedDate = DateTime.UtcNow;
                    existRecord.ModifiedById = "aaaaaa";
                    _uow.GetDbContext().ContractDetails.Update(existRecord);
                    _uow.GetDbContext().SaveChanges();
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                catch (Exception ex)
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.SomethingWrong + ex.Message;
                }
            }
            return response;
        }

        public async Task<APIResponse> FilterContractList(FilterContractModel model, string UserId)
        {
            //object filteredList;
            APIResponse response = new APIResponse();
            try
            {
                var contractList = _uow.GetDbContext().ContractDetails.Where(x => x.IsDeleted == false).ToList();
                List<ContractDetails> filteredList = new List<ContractDetails>();
                if (model != null)
                {
                    if (model.ActivityTypeId != 0 && model.ActivityTypeId != null)
                    {
                        contractList = contractList.Where(x => x.ActivityTypeId == model.ActivityTypeId).ToList();
                    }
                    if (!string.IsNullOrEmpty(model.ClientName))
                    {
                        contractList = contractList.Where(x => x.ClientName == model.ClientName).ToList();
                    }
                    if (model.ContractId != 0 && model.ContractId != null)
                    {
                        contractList = contractList.Where(x => x.ContractId == model.ContractId).ToList();
                    }
                    if (model.CurrencyId != 0 && model.CurrencyId != null)
                    {
                        contractList = contractList.Where(x => x.CurrencyId == model.CurrencyId).ToList();
                    }
                    if (model.IsApproved == true)
                    {
                        contractList = contractList.Where(x => x.IsApproved == Convert.ToBoolean(model.IsApproved)).ToList();
                    }
                    if (model.YesOrNo == "No")
                    {
                        contractList = contractList.Where(x => x.IsDeclined == true).ToList();
                    }
                    if (!string.IsNullOrEmpty(model.FilterType))
                    {
                        if (model.FilterType == "Equals")
                        {
                            if (model.UnitRate != 0 && model.UnitRate != null)
                            {
                                contractList = contractList.Where(x => x.UnitRate == model.UnitRate).ToList();
                            }
                        }
                        if (model.FilterType == "Greater Than")
                        {
                            if (model.UnitRate != 0 && model.UnitRate != null)
                            {
                                contractList = contractList.Where(x => x.UnitRate > model.UnitRate).ToList();
                            }
                        }
                        if (model.FilterType == "Less Than")
                        {
                            if (model.UnitRate != 0 && model.UnitRate != null)
                            {
                                contractList = contractList.Where(x => x.UnitRate < model.UnitRate).ToList();
                            }
                        }
                    }
                    if (string.IsNullOrEmpty(model.FilterType))
                    {
                        if (model.UnitRate != 0 && model.UnitRate != null)
                        {
                            contractList = contractList.Where(x => x.UnitRate == model.UnitRate).ToList();
                        }

                    }
                    response.data.ContractDetails = contractList;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "No Entries Found.Try Different Filters";
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

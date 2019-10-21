﻿using AutoMapper;
using DataAccess;
using DataAccess.DbEntities;
using DataAccess.DbEntities.Marketing;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces;
using HumanitarianAssistance.ViewModels.Models.Marketing;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Service.Classes
{
    public class ClientDetailsService : IClientDetails
    {
        IUnitOfWork _uow;
        IMapper _mapper;
        UserManager<AppUser> _userManager;
        public ClientDetailsService(IUnitOfWork uow, IMapper mapper, UserManager<AppUser> userManager)
        {
            this._uow = uow;
            this._mapper = mapper;
            this._userManager = userManager;
        }

        #region Client

        public async Task<APIResponse> GetClientsPaginatedList(ClientPaginationModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var list =  _uow.GetDbContext().ClientDetails.Where(x=>x.IsDeleted==false).Skip((model.pageSize * model.pageIndex)).Take(model.pageSize).ToList();

                //var JobList = (from j in _uow.GetDbContext().JobDetails
                //               join jp in _uow.GetDbContext().JobPriceDetails on j.JobId equals jp.JobId
                //               where !j.IsDeleted.Value && !jp.IsDeleted.Value
                //               select (new ClientDetailModel
                //               {
                //               })).Skip((model.pageSize * model.pageIndex)).Take(model.pageSize).ToList();
                //updated list order By AS 07/06/2019
                response.data.ClientDetails = list.OrderByDescending(x=>x.ClientId).ToList();
                response.StatusCode = 200;
                response.Message = "Success";
                response.data.TotalCount = await _uow.GetDbContext().ClientDetails.CountAsync(x => x.IsDeleted == false);
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Get Client Details By Id
        /// </summary>
        /// <param name="ClientId"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<APIResponse> GetClientDetailsById(int ClientId, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                //add await by AS 07/06/2019
                var clientDetails = await (from ur in _uow.GetDbContext().ClientDetails
                                           join at in _uow.GetDbContext().Categories on ur.CategoryId equals at.CategoryId
                                           into jou
                                           from dev_skill in jou.DefaultIfEmpty()
                                           from a in _uow.GetDbContext().ClientDetails
                                           where !ur.IsDeleted.Value && !a.IsDeleted.Value && ur.ClientId == ClientId
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
                                           })).FirstOrDefaultAsync();

                response.data.clientDetailsById = clientDetails;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Add/Edit client
        /// </summary>
        /// <param name="model"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<APIResponse> AddEditClientDetails(ClientDetailModel model, string UserId)
        {
            long LatestClientId = 0;
            var clientcode = string.Empty;
            DbContext db = _uow.GetDbContext();
            APIResponse response = new APIResponse();
            long ClientId = 0;
            ClientDetails clientDetails = new ClientDetails();
            ClientDetailModel mod = new ClientDetailModel();

            try
            {
                if (model.ClientId == 0)
                {
                    var ClientDetail = _uow.GetDbContext().ClientDetails
                                                           .OrderByDescending(x => x.ClientId)
                                                           .FirstOrDefault();
                    if (ClientDetail == null)
                    {
                        LatestClientId = 1;
                        clientcode = getClientCode(LatestClientId.ToString());
                    }
                    else
                    {
                        LatestClientId = ClientDetail.ClientId + 1;
                        clientcode = getClientCode(LatestClientId.ToString());
                    }
                    ClientDetails ob = new ClientDetails();
                    //ClientDetails obj = _mapper.Map<ClientDetailModel, ClientDetails>(model);
                    ob.ClientName = model.ClientName;
                    ob.CategoryId = model.CategoryId;
                    ob.ClientBackground = model.ClientBackground;
                    ob.Email = model.Email;
                    ob.FocalPoint = model.FocalPoint;
                    ob.History = model.History;
                    ob.ClientCode = clientcode;
                    ob.Phone = model.Phone;
                    ob.PhysicialAddress = model.PhysicialAddress;
                    ob.Position = model.Position;
                    ob.IsDeleted = false;
                    ob.CreatedById = UserId;
                    ob.CreatedDate = DateTime.Now;
                    await _uow.GetDbContext().ClientDetails.AddAsync(ob);
                    await _uow.SaveAsync();

                    ClientId = ob.ClientId;
                    mod.ClientId = ClientId;
                    mod.ClientName = ob.ClientName;
                    mod.CategoryId = ob.CategoryId;
                    mod.ClientBackground = ob.ClientBackground;
                    mod.Email = ob.Email;
                    mod.FocalPoint = ob.FocalPoint;
                    mod.History = ob.History;
                    mod.ClientCode = ob.ClientCode;
                    mod.type = "Add";
                    mod.Phone = ob.Phone;
                    mod.PhysicialAddress = ob.PhysicialAddress;
                    mod.Position = ob.Position;
                    mod.Count = await _uow.GetDbContext().ClientDetails.CountAsync(x => x.IsDeleted == false);
                    response.data.clientDetailsById = mod;
                    response.Message = "Client added successfully";
                    response.StatusCode = StaticResource.successStatusCode;
                }
                else
                {
                    var existRecords = await _uow.ClientDetailsRepository.FindAsync(x => x.IsDeleted == false && x.ClientId == model.ClientId);
                    if (existRecords != null)
                    {
                        _mapper.Map(model, existRecords);
                        existRecords.IsDeleted = false;
                        existRecords.ModifiedById = UserId;
                        existRecords.ModifiedDate = DateTime.Now;
                        await _uow.ClientDetailsRepository.UpdateAsyn(existRecords);
                        model.type = "Edit";
                        response.data.clientDetailsById = model;
                        response.Message = "Client updated successfully";
                        response.StatusCode = StaticResource.successStatusCode;
                    }
                    LatestClientId = Convert.ToInt32(model.ClientId);
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
        /// Client Code
        /// </summary>
        /// <param name="ClientId"></param>
        /// <returns></returns>
        public string getClientCode(string ClientId)
        {
            string code = string.Empty;
            if (ClientId.Length == 1)
                return code = "0000" + ClientId;
            else if (ClientId.Length == 2)
                return code = "000" + ClientId;
            else if (ClientId.Length == 3)
                return code = "00" + ClientId;
            else if (ClientId.Length == 4)
                return code = "0" + ClientId;
            else
                return code = "" + ClientId;
        }

        /// <summary>
        /// edit selected client details
        /// </summary>
        /// <param name="model"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<APIResponse> EditClientDetails(ClientDetailModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var existRecords = await _uow.ClientDetailsRepository.FindAsync(x => x.IsDeleted == false && x.ClientId == model.ClientId);
                if (existRecords != null)
                {
                    _mapper.Map(model, existRecords);
                    existRecords.IsDeleted = false;
                    existRecords.ModifiedById = UserId;
                    existRecords.ModifiedDate = DateTime.Now;
                    await _uow.ClientDetailsRepository.UpdateAsyn(existRecords);
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
        /// get clients list
        /// </summary>
        /// <returns></returns>
        public async Task<APIResponse> GetAllClient()
        {
            APIResponse response = new APIResponse();
            try
            {
                var list = await _uow.ClientDetailsRepository.FindAllAsync(x => !x.IsDeleted.Value);
                //updated list order By AS 07/06/2019 
                response.data.ClientDetails = list.OrderByDescending(x => x.ClientId).ToList();
                response.StatusCode = 200;
                response.data.jobListTotalCount = await _uow.GetDbContext().ClientDetails.CountAsync(x => x.IsDeleted == false);
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
        /// delete selected client
        /// </summary>
        /// <param name="model"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<APIResponse> DeleteClientDetails(int model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var ClientInfo = await _uow.ClientDetailsRepository.FindAsync(c => c.ClientId == model);
                ClientInfo.IsDeleted = true;
                ClientInfo.ModifiedById = UserId;
                ClientInfo.ModifiedDate = DateTime.UtcNow;
                await _uow.ClientDetailsRepository.UpdateAsyn(ClientInfo, ClientInfo.ClientId);
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Client Deleted Successfully";
                response.data.jobListTotalCount = await _uow.GetDbContext().JobDetails.CountAsync(x => x.IsDeleted == false);
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> FilterClientList(FilterClientModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var contractList = await _uow.GetDbContext().ClientDetails.Where(x => x.IsDeleted == false).ToListAsync();

                List<ClientDetails> filteredList = new List<ClientDetails>();

                if (model != null)
                {
                    if (model.ClientId != 0 && model.ClientId != null)
                    {
                        contractList = contractList.Where(x => x.ClientId == model.ClientId).ToList();
                    }

                    if (!string.IsNullOrEmpty(model.ClientName))
                    {
                        contractList = contractList.Where(x => x.ClientName == model.ClientName).ToList();
                    }
                    if (model.CategoryId != 0 && model.CategoryId != null)
                    {
                        contractList = contractList.Where(x => x.CategoryId == model.CategoryId).ToList();
                    }
                    if (!string.IsNullOrEmpty(model.Email))
                    {
                        contractList = contractList.Where(x => x.Email == model.Email).ToList();
                    }
                    if (!string.IsNullOrEmpty(model.Position))
                    {
                        contractList = contractList.Where(x => x.Position == model.Position).ToList();
                    }
                    response.data.ClientDetails = contractList;
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

        #endregion

        #region Category

        /// <summary>
        /// get category list
        /// </summary>
        /// <returns></returns>
        public async Task<APIResponse> GetAllCategory()
        {
            APIResponse response = new APIResponse();
            try
            {
                var list = await _uow.CategoryRepository.FindAllAsync(x => !x.IsDeleted.Value);
                response.data.Categories = list;
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
        /// add a new category
        /// </summary>
        /// <param name="model"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<APIResponse> AddCategory(CategoryModel model, string UserId)
        {

            APIResponse response = new APIResponse();
            try
            {
                Category obj = _mapper.Map<CategoryModel, Category>(model);
                obj.CategoryName = model.CategoryName;
                obj.IsDeleted = false;
                obj.CreatedById = UserId;
                obj.CreatedDate = DateTime.Now;
                await _uow.CategoryRepository.AddAsyn(obj);
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
        /// edit selected category
        /// </summary>
        /// <param name="model"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<APIResponse> EditCategory(CategoryModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var existRecord = await _uow.CategoryRepository.FindAsync(x => x.IsDeleted == false && x.CategoryId == model.CategoryId);
                if (existRecord != null)
                {
                    _mapper.Map(model, existRecord);
                    existRecord.IsDeleted = false;
                    existRecord.ModifiedById = UserId;
                    existRecord.ModifiedDate = DateTime.Now;
                    await _uow.CategoryRepository.UpdateAsyn(existRecord);
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

        #endregion



    }
}


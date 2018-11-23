using AutoMapper;
using DataAccess;
using DataAccess.DbEntities;
using DataAccess.DbEntities.Marketing;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Entities;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces.Marketing;
using HumanitarianAssistance.ViewModels.Models.Marketing;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.Classes.Marketing
{
    public class JobDetailsService : IJobDetailsService
    {
        IUnitOfWork _uow;
        IMapper _mapper;
        UserManager<AppUser> _userManager;
        ApplicationDbContext _context;

        public JobDetailsService(IUnitOfWork uow, IMapper mapper, UserManager<AppUser> userManager, ApplicationDbContext context)
        {
            this._context = context;
            this._uow = uow;
            this._mapper = mapper;
            this._userManager = userManager;

        }

        #region Job Details

        /// <summary>
        ///  Get All Jobs List
        /// </summary>
        /// <returns></returns>

        public async Task<APIResponse> GetAllJobDetails()
        {
            APIResponse response = new APIResponse();
            try
            {


                var JobList = (from j in _uow.GetDbContext().JobDetails
                              join jp in _uow.GetDbContext().JobPriceDetails on j.JobId equals jp.JobId
                              where !j.IsDeleted.Value && !jp.IsDeleted.Value
                              select (new JobDetailsModel
                              {
                                  JobId = j.JobId,
                                  JobCode = j.JobCode,
                                  JobName = j.JobName,
                                  Description = j.Description,
                                  JobPhaseId = j.JobPhaseId,
                                  StartDate = j.StartDate,
                                  EndDate = j.EndDate,
                                  IsActive = j.IsActive,
                                  IsApproved = j.IsApproved,
                                  UnitRate = jp.UnitRate,
                                  Units = jp.Units,
                                  FinalRate = jp.FinalRate,
                                  FinalPrice = jp.FinalPrice,
                                  TotalPrice = jp.TotalPrice,
                                  IsInvoiceApproved = jp.IsInvoiceApproved
                              })).ToList();
                


              

                //var list = await _uow.JobDetailsRepository.FindAllAsync(x => !x.IsDeleted.Value);
                response.data.JobDetailsModel = JobList;
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
        /// Add New Job
        /// </summary>
        /// <param name="model"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<APIResponse> AddJobDetails(JobDetailsModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                JobDetails obj = _mapper.Map<JobDetailsModel, JobDetails>(model);
                obj.JobName = model.JobName;
                obj.ContractId = model.ContractId;
                obj.Description = model.Description;
                obj.EndDate = model.EndDate;
                obj.IsActive = true;
                obj.IsApproved = true;
                obj.JobCode = model.JobCode;
                obj.JobPhaseId = model.JobPhaseId;
                obj.StartDate = model.StartDate;
                obj.IsDeleted = false;
                obj.CreatedById = UserId;
                obj.CreatedDate = DateTime.Now;
                await _uow.JobDetailsRepository.AddAsyn(obj);
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
        /// Edit selected Job Details
        /// </summary>
        /// <param name="model"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<APIResponse> EditJobDetails(JobDetailsModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var existRecord = await _uow.JobDetailsRepository.FindAsync(x => x.IsDeleted == false && x.JobId == model.JobId);
                if (existRecord != null)
                {
                    _mapper.Map(model, existRecord);
                    existRecord.IsDeleted = false;
                    existRecord.ModifiedById = UserId;
                    existRecord.ModifiedDate = DateTime.Now;

                    await _uow.JobDetailsRepository.UpdateAsyn(existRecord);

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
        /// Delete Selected Job
        /// </summary>
        /// <param name="documentid"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<APIResponse> DeleteJobDetail(JobDetails model)
        {
            APIResponse response = new APIResponse();
            try
            {
                var jobInfo = await _uow.JobDetailsRepository.FindAsync(c => c.JobId == model.JobId && c.IsDeleted == false);
                jobInfo.IsDeleted = true;
                jobInfo.ModifiedById = model.ModifiedById;
                jobInfo.ModifiedDate = model.ModifiedDate;
                await _uow.JobDetailsRepository.UpdateAsyn(jobInfo, jobInfo.JobId);
                var jobPriceInfo = await _uow.JobPriceDetailsRepository.FindAsync(c => c.JobId == model.JobId);
                jobPriceInfo.IsDeleted = true;
                jobPriceInfo.ModifiedById = model.ModifiedById;
                jobPriceInfo.ModifiedDate = model.ModifiedDate;
                await _uow.JobPriceDetailsRepository.UpdateAsyn(jobPriceInfo, jobPriceInfo.JobId);
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
        /// Add or Edit Job Detail
        /// </summary>
        /// <param name="model"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<APIResponse> AddEditJobDetail(JobDetailsModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                if (model.JobId == 0)
                {
                    JobDetails obj = _mapper.Map<JobDetailsModel, JobDetails>(model);
                    obj.ContractId = model.ContractId;
                    obj.Description = model.Description;
                    obj.EndDate = model.EndDate;
                    obj.IsActive = true;
                    obj.IsApproved = true;
                    obj.JobCode = model.JobCode;
                    obj.IsDeleted = false;
                    obj.CreatedById = UserId;
                    obj.CreatedDate = DateTime.Now;
                    obj.JobPhaseId = model.JobPhaseId;
                    obj.JobName = model.JobName;
                    obj.IsDeleted = false;
                    await _uow.JobDetailsRepository.AddAsyn(obj);
                    await _uow.SaveAsync();

                    JobPriceDetailsModel obj1 = new JobPriceDetailsModel();
                    obj1.JobId = obj.JobId;
                    obj1.Discount = model.Discount;
                    obj1.DiscountPercent = model.DiscountPercent;
                    obj1.FinalPrice = model.FinalPrice;
                    obj1.FinalRate = model.FinalRate;
                    obj1.TotalPrice = model.TotalPrice;
                    obj1.UnitRate = model.UnitRate;
                    obj1.Units = model.Units;

                    JobPriceDetails priceDetails = _mapper.Map<JobPriceDetailsModel, JobPriceDetails>(obj1);
                    priceDetails.JobId = obj1.JobId;
                    priceDetails.Discount = obj1.Discount;
                    priceDetails.DiscountPercent = obj1.DiscountPercent;
                    priceDetails.FinalPrice = obj1.FinalPrice;
                    priceDetails.FinalRate = obj1.FinalRate;
                    priceDetails.TotalPrice = obj1.TotalPrice;
                    priceDetails.UnitRate = obj1.UnitRate;
                    priceDetails.Units = obj1.Units;
                    priceDetails.CreatedById = UserId;
                    priceDetails.CreatedDate = DateTime.UtcNow;
                    priceDetails.IsDeleted = false;
                    await _uow.JobPriceDetailsRepository.AddAsyn(priceDetails);
                    await _uow.SaveAsync();
                }
                else
                {
                    var existRecord = await _uow.JobDetailsRepository.FindAsync(x => x.IsDeleted == false && x.JobId == model.JobId);
                    if (existRecord != null)
                    {
                        _mapper.Map(model, existRecord);
                        existRecord.IsDeleted = false;
                        existRecord.ModifiedById = UserId;
                        existRecord.ModifiedDate = DateTime.Now;
                        await _uow.JobDetailsRepository.UpdateAsyn(existRecord);

                        var existRecords = await _uow.JobPriceDetailsRepository.FindAsync(x => x.IsDeleted == false && x.JobId == model.JobId);
                        JobPriceDetailsModel obj2 = new JobPriceDetailsModel();
                        obj2.JobId = model.JobId;
                        obj2.Discount = model.Discount;
                        obj2.DiscountPercent = model.DiscountPercent;
                        obj2.FinalPrice = model.FinalPrice;
                        obj2.FinalRate = model.FinalRate;
                        obj2.TotalPrice = model.TotalPrice;
                        obj2.UnitRate = model.UnitRate;
                        obj2.Units = model.Units;
                        _mapper.Map(obj2, existRecords);

                        existRecords.IsDeleted = false;
                        existRecords.ModifiedById = UserId;
                        existRecords.ModifiedDate = DateTime.Now;
                        await _uow.JobPriceDetailsRepository.UpdateAsyn(existRecords);
                    }
                }
                response.data.JobDetail = model;
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

        #endregion

    }
}

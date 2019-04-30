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
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using SelectPdf;

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

        public async Task<APIResponse> ApproveJob(int model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var jobInfo = await _uow.JobDetailsRepository.FindAsync(c => c.JobId == model && c.IsDeleted == false);
                jobInfo.IsApproved = true;
                jobInfo.ModifiedById = UserId;
                jobInfo.ModifiedDate = DateTime.UtcNow;
                await _uow.JobDetailsRepository.UpdateAsyn(jobInfo, jobInfo.JobId);
                var jobPriceInfo = await _uow.JobPriceDetailsRepository.FindAsync(c => c.JobId == model);
                jobPriceInfo.IsInvoiceApproved = true;
                jobPriceInfo.ModifiedById = UserId;
                jobPriceInfo.ModifiedDate = DateTime.UtcNow;
                await _uow.JobPriceDetailsRepository.UpdateAsyn(jobPriceInfo, jobPriceInfo.JobId);
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Job Approved";
                response.data.jobListTotalCount = await _uow.GetDbContext().JobDetails.CountAsync(x => x.IsDeleted == false);
                response.data.JobDetails = _uow.GetDbContext().JobDetails.Where(x => x.IsDeleted == false).ToList();
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> AcceptAgreement(int model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var jobInfo = await _uow.JobDetailsRepository.FindAsync(c => c.JobId == model && c.IsDeleted == false);
                if (jobInfo.IsApproved == true)
                {
                    jobInfo.IsAgreementApproved = true;
                    jobInfo.ModifiedById = UserId;
                    jobInfo.ModifiedDate = DateTime.UtcNow;
                    await _uow.JobDetailsRepository.UpdateAsyn(jobInfo, jobInfo.JobId);
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Agreement Accepted";
                    response.data.JobDetailModel = jobInfo;
                    response.data.jobListTotalCount = await _uow.GetDbContext().JobDetails.CountAsync(x => x.IsDeleted == false);
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Job is not approved.";
                    response.data.JobDetailModel = jobInfo;
                    response.data.jobListTotalCount = await _uow.GetDbContext().JobDetails.CountAsync(x => x.IsDeleted == false);

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
        ///  Get All Jobs List
        /// </summary>
        /// <returns></returns>

        public async Task<APIResponse> GetAllJobDetails()
        {
            APIResponse response = new APIResponse();
            try
            {
                int count = await _uow.GetDbContext().JobDetails.CountAsync(x => x.IsDeleted == false);
                var JobList = await (from j in _uow.GetDbContext().JobDetails
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
                                         CreatedDate = j.CreatedDate,
                                         IsInvoiceApproved = jp.IsInvoiceApproved
                                     })).Take(10).Skip(0).OrderByDescending(x => x.CreatedDate).ToListAsync();






                //var list = await _uow.JobDetailsRepository.FindAllAsync(x => !x.IsDeleted.Value);
                response.data.JobDetailsModel = JobList;
                response.data.jobListTotalCount = count;
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
        public async Task<APIResponse> DeleteJobDetail(int model, string userId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var jobInfo = await _uow.JobDetailsRepository.FindAsync(c => c.JobId == model && c.IsDeleted == false);
                jobInfo.IsDeleted = true;
                jobInfo.ModifiedById = userId;
                jobInfo.ModifiedDate = DateTime.UtcNow;
                await _uow.JobDetailsRepository.UpdateAsyn(jobInfo, jobInfo.JobId);
                var jobPriceInfo = await _uow.JobPriceDetailsRepository.FindAsync(c => c.JobId == model);
                jobPriceInfo.IsDeleted = true;
                jobPriceInfo.ModifiedById = userId;
                jobPriceInfo.ModifiedDate = DateTime.UtcNow;
                await _uow.JobPriceDetailsRepository.UpdateAsyn(jobPriceInfo, jobPriceInfo.JobId);
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Job Deleted Successfully";
                response.data.jobListTotalCount = await _uow.GetDbContext().JobDetails.CountAsync(x => x.IsDeleted == false);
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        public string getJobCode(string JobId)
        {
            string code = string.Empty;
            if (JobId.Length == 1)
                return code = "0000" + JobId;
            else if (JobId.Length == 2)
                return code = "000" + JobId;
            else if (JobId.Length == 3)
                return code = "00" + JobId;
            else if (JobId.Length == 4)
                return code = "0" + JobId;
            else
                return code = "" + JobId;
        }


        public async Task<APIResponse> GetJobDetailsById(int model, string UserId)
        {
            APIResponse response = new APIResponse(); try
            {


                var JobList = (from j in _uow.GetDbContext().JobDetails
                               join jp in _uow.GetDbContext().JobPriceDetails on j.JobId equals jp.JobId
                               join cd in _uow.GetDbContext().ContractDetails on j.ContractId equals cd.ContractId
                               join cur in _uow.GetDbContext().CurrencyDetails on cd.CurrencyId equals cur.CurrencyId
                               where !j.IsDeleted.Value && !jp.IsDeleted.Value && j.JobId == model
                               select (new JobPriceModel
                               {
                                   JobId = j.JobId,
                                   JobName = j.JobName,
                                   JobCode = j.JobCode,
                                   UnitRate = jp.UnitRate,
                                   FinalRate = jp.FinalRate,
                                   FinalPrice = jp.FinalPrice,
                                   TotalPrice = jp.TotalPrice,
                                   Discount = jp.Discount,
                                   DiscountPercent = jp.DiscountPercent,
                                   EndDate = j.EndDate,
                                   StartDate = cd.StartDate,
                                   ContractId = j.ContractId,
                                   Minutes = jp.Minutes,
                                   IsApproved = j.IsApproved,
                                   CreatedBy = j.CreatedBy.ToString(),
                                   IsAgreementApproved = j.IsAgreementApproved,
                                   ClientId = cd.ClientId,
                                   ClientName = cd.ClientName,
                                   JobPriceId = jp.JobPriceId,
                                   CurrencyCode = cur.CurrencyCode

                               })).FirstOrDefault();
                response.data.JobPriceDetail = JobList;
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

        /// Add or Edit Job Detail
        /// </summary>
        /// <param name="model"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<APIResponse> AddEditJobDetail(JobDetailsModel model, string UserId)
        {
            long LatestJobId = 0;
            var jobcode = string.Empty;
            APIResponse response = new APIResponse();
            try
            {
                if (model.JobId == 0)
                {
                    var jobList = _uow.GetDbContext().JobDetails.Where(x => x.JobName == model.JobName && x.IsDeleted == false).FirstOrDefault();
                    if (jobList == null)
                    {
                        var jobDetail = _uow.GetDbContext().JobDetails
                                                                                       .OrderByDescending(x => x.ContractId)
                                                                                       .FirstOrDefault();
                        if (jobDetail == null)
                        {
                            LatestJobId = 1;
                            jobcode = getJobCode(LatestJobId.ToString());
                        }
                        else
                        {
                            LatestJobId = Convert.ToInt32(jobDetail.ContractId) + 1;
                            jobcode = getJobCode(LatestJobId.ToString());
                        }
                        JobDetails obj = _mapper.Map<JobDetailsModel, JobDetails>(model);
                        obj.ContractId = model.ContractId;
                        obj.Description = model.Description;
                        obj.EndDate = model.EndDate;
                        obj.IsActive = true;
                        obj.IsApproved = model.IsApproved;
                        obj.JobCode = model.JobCode;
                        obj.IsDeleted = false;
                        obj.CreatedById = UserId;
                        obj.CreatedDate = DateTime.Now;
                        obj.JobPhaseId = model.JobPhaseId;
                        obj.JobName = model.JobName;
                        obj.JobCode = jobcode;
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
                        obj1.Minutes = model.Minutes;

                        JobPriceDetails priceDetails = _mapper.Map<JobPriceDetailsModel, JobPriceDetails>(obj1);
                        priceDetails.JobId = obj1.JobId;
                        priceDetails.Minutes = obj1.Minutes;
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

                        JobPriceModel details = new JobPriceModel();
                        details.ContractId = model.ContractId;
                        details.Discount = model.Discount;
                        details.DiscountPercent = model.DiscountPercent;
                        details.EndDate = model.EndDate;
                        details.FinalPrice = model.FinalPrice;
                        details.FinalRate = model.FinalRate;
                        details.JobCode = obj.JobCode;
                        details.JobName = model.JobName;
                        details.TotalPrice = model.TotalPrice;
                        details.UnitRate = model.UnitRate;
                        details.JobId = obj.JobId;
                        details.JobPriceId = obj1.JobPriceId;
                        details.Minutes = obj1.Minutes;
                        details.IsApproved = obj.IsApproved;
                        int count = _uow.GetDbContext().JobDetails.Where(x => x.IsDeleted == false).ToList().Count();
                        response.data.JobPriceDetail = details;
                        response.data.jobListTotalCount = count;
                        response.Message = "Job Created Successfully";
                        response.StatusCode = StaticResource.successStatusCode;
                    }
                    else
                    {
                        response.StatusCode = StaticResource.failStatusCode;
                        response.Message = "Job Name already exists. Please try again with other job name.";
                    }

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
                        obj2.Minutes = model.Minutes;
                        obj2.JobPriceId = existRecords.JobPriceId;
                        _mapper.Map(obj2, existRecords);

                        existRecords.IsDeleted = false;
                        existRecords.ModifiedById = UserId;
                        existRecords.ModifiedDate = DateTime.Now;
                        await _uow.JobPriceDetailsRepository.UpdateAsyn(existRecords);
                        response.Message = "Job Updated Successfully";
                        response.StatusCode = StaticResource.successStatusCode;
                    }
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> FilterJobList(JobFilterModel model, string userId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var JobList1 = (from j in _uow.GetDbContext().JobDetails
                                join jp in _uow.GetDbContext().JobPriceDetails on j.JobId equals jp.JobId
                                where !j.IsDeleted.Value && !jp.IsDeleted.Value
                                select (new JobDetailsModel
                                {
                                    JobId = j.JobId,
                                    JobCode = j.JobCode,
                                    JobName = j.JobName,
                                    EndDate = j.EndDate,
                                    IsActive = j.IsActive,
                                    IsApproved = j.IsApproved,
                                    UnitRate = jp.UnitRate,
                                    Units = jp.Units,
                                    FinalRate = jp.FinalRate,
                                    FinalPrice = jp.FinalPrice,
                                    TotalPrice = jp.TotalPrice,
                                    IsInvoiceApproved = jp.IsInvoiceApproved,
                                    ContractId = j.ContractId,
                                    Discount = jp.Discount,
                                    DiscountPercent = jp.DiscountPercent,
                                    Minutes = jp.Minutes
                                })).ToList();

                if (model != null)
                {
                    if (!string.IsNullOrEmpty(model.JobName))
                    {
                        JobList1 = JobList1.Where(x => x.JobName == model.JobName).ToList();
                    }
                    if (model.JobId != 0)
                    {
                        JobList1 = JobList1.Where(x => x.JobId == model.JobId).ToList();
                    }
                    if (model.IsApproved == true)
                    {
                        JobList1 = JobList1.Where(x => x.IsApproved == Convert.ToBoolean(model.IsApproved)).ToList();
                    }
                    if (model.YesOrNo == "No")
                    {
                        JobList1 = JobList1.Where(x => x.IsApproved == false).ToList();
                    }
                    if (model.ContractId != 0)
                    {
                        JobList1 = JobList1.Where(x => x.ContractId == model.ContractId).ToList();
                    }
                    if (!string.IsNullOrEmpty(model.FilterType))
                    {
                        if (model.FilterType == "Equals")
                        {
                            if (model.UnitRate != 0)
                            {
                                JobList1 = JobList1.Where(x => x.UnitRate == model.UnitRate).ToList();
                            }
                            if (model.TotalPrice != 0)
                            {
                                JobList1 = JobList1.Where(x => x.TotalPrice == model.TotalPrice).ToList();
                            }
                        }
                        if (model.FilterType == "Greater Than")
                        {
                            if (model.UnitRate != 0)
                            {
                                JobList1 = JobList1.Where(x => x.UnitRate > model.UnitRate).ToList();
                            }
                            if (model.TotalPrice != 0)
                            {
                                JobList1 = JobList1.Where(x => x.TotalPrice > model.TotalPrice).ToList();
                            }
                        }
                        if (model.FilterType == "Less Than")
                        {
                            if (model.UnitRate != 0)
                            {
                                JobList1 = JobList1.Where(x => x.UnitRate < model.UnitRate).ToList();
                            }
                            if (model.TotalPrice != 0)
                            {
                                JobList1 = JobList1.Where(x => x.TotalPrice < model.TotalPrice).ToList();
                            }
                        }
                    }
                    if (string.IsNullOrEmpty(model.FilterType))
                    {
                        if (model.UnitRate != 0)
                        {
                            JobList1 = JobList1.Where(x => x.UnitRate == model.UnitRate).ToList();
                        }
                        if (model.TotalPrice != 0)
                        {
                            JobList1 = JobList1.Where(x => x.TotalPrice == model.TotalPrice).ToList();
                        }

                    }
                    response.data.JobPriceDetailList = JobList1;
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

        public async Task<APIResponse> FilterJobsList(FilterJobModel model, string userId)
        {
            string finalPriceValue = null;
            string jobIdValue = null;
            string jobNameValue = null;
            string ApprovedValue = null;

            if (!string.IsNullOrEmpty(model.Value))
            {
                finalPriceValue = model.FinalPrice ? model.Value.ToLower().Trim() : null;
                jobIdValue = model.JobId ? model.Value.ToLower().Trim() : null;
                jobNameValue = model.JobName ? model.Value.ToLower().Trim() : null;
                ApprovedValue = model.Approved ? model.Value.ToLower().Trim() : null;
            }

            APIResponse response = new APIResponse();
            try
            {
                var voucherList = await _uow.GetDbContext().JobPriceDetails
                                    .Where(v => v.IsDeleted == false &&
                                          (!string.IsNullOrEmpty(model.Value) ?
                                             (
                                                   v.JobDetails.JobId.ToString().Trim().ToLower().Contains(jobIdValue) ||
                                                   v.FinalPrice.ToString().Trim().ToLower().Contains(finalPriceValue) ||
                                                   v.JobDetails.JobName.Trim().ToLower().Contains(jobNameValue) ||
                                                   v.JobDetails.IsApproved.ToString().Trim().ToLower().Contains(ApprovedValue)
                                              ) : true
                                           )
                                     )
                                    .OrderByDescending(x => x.JobDetails.CreatedDate)
                                    .Select(x => new JobDetailsModel
                                    {
                                        JobId = x.JobId,
                                        JobCode = x.JobDetails.JobCode,
                                        JobName = x.JobDetails.JobName,
                                        EndDate = x.JobDetails.EndDate,
                                        IsActive = x.JobDetails.IsActive,
                                        IsApproved = x.JobDetails.IsApproved,
                                        UnitRate = x.UnitRate,
                                        Units = x.Units,
                                        FinalRate = x.FinalRate,
                                        FinalPrice = x.FinalPrice,
                                        TotalPrice = x.TotalPrice,
                                        IsInvoiceApproved = x.IsInvoiceApproved,
                                        ContractId = x.JobDetails.ContractId,
                                        Discount = x.Discount,
                                        DiscountPercent = x.DiscountPercent,
                                        Minutes = x.Minutes
                                    })
                                    .AsNoTracking()
                                    .ToListAsync();
                response.data.jobListTotalCount = voucherList.Count();
                response.data.JobPriceDetailList = voucherList;
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
        public async Task<APIResponse> GetJobsPaginatedList(JobPaginationModel model, string UserId)
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
                               })).Skip((model.pageSize * model.pageIndex)).Take(model.pageSize).OrderByDescending(x => x.CreatedDate).ToList();
                response.data.TotalCount = await _uow.GetDbContext().JobDetails.CountAsync(x => x.IsDeleted == false);
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

        [BindProperty]
        public string TxtHtmlCode { get; set; }

        [BindProperty]
        public string DdlPageSize { get; set; }

        [BindProperty]
        public string DdlPageOrientation { get; set; }
        public List<SelectListItem> PageOrientations { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "Portrait", Text = "Portrait" },
            new SelectListItem { Value = "Landscape", Text = "Landscape" },
        };

        [BindProperty]
        public string TxtWidth { get; set; }

        [BindProperty]
        public string TxtHeight { get; set; }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get(string html)
        {

            return new string[] { "value1", "value2" };
        }
        public async Task<APIResponse> CreatePDF(int JobId)
        {
            byte[] pdf = null;
            APIResponse response = new APIResponse();
            try
            {
                JobPriceModel JobDetails = (from j in _uow.GetDbContext().JobDetails
                                            join jp in _uow.GetDbContext().JobPriceDetails on j.JobId equals jp.JobId
                                            join cd in _uow.GetDbContext().ContractDetails on j.ContractId equals cd.ContractId
                                            join cur in _uow.GetDbContext().CurrencyDetails on cd.CurrencyId equals cur.CurrencyId
                                            where !j.IsDeleted.Value && !jp.IsDeleted.Value && j.JobId == JobId
                                            select (new JobPriceModel
                                            {
                                                JobName = j.JobName,
                                                JobCode = j.JobCode,
                                                UnitRate = jp.UnitRate,
                                                EndDate = j.EndDate,
                                                StartDate = cd.StartDate,
                                                IsApproved = j.IsApproved,
                                                ClientName = cd.ClientName
                                            })).FirstOrDefault();

                //var imagepath = Path.Combine(_hostingEnvironment.WebRootPath, "agreement-logo.png");
                //< img width = '100' height = '100' src = " + imagepath + @" >
                DdlPageSize = "A4";
                PdfPageSize pageSize = (PdfPageSize)Enum.Parse(typeof(PdfPageSize),
                    DdlPageSize, true);
                DdlPageOrientation = "Portrait";
                PdfPageOrientation pdfOrientation =
                    (PdfPageOrientation)Enum.Parse(typeof(PdfPageOrientation),
                    DdlPageOrientation, true);

                int webPageWidth = 1024;

                webPageWidth = Convert.ToInt32(TxtWidth);


                int webPageHeight = 0;

                webPageHeight = Convert.ToInt32(TxtHeight);


                // instantiate a html to pdf converter object
                HtmlToPdf converter = new HtmlToPdf();

                // set converter options
                converter.Options.PdfPageSize = pageSize;
                converter.Options.PdfPageOrientation = pdfOrientation;
                converter.Options.WebPageWidth = webPageWidth;
                converter.Options.WebPageHeight = webPageHeight;
                TxtHtmlCode = @"<div style='padding-left:40px !important' id='jobReportPdf' align='center'>
                        <div class='container-fluid'>
                        <div class='col-md-12'>
                            <table align = 'center' width='700px;' style='margin:0 auto; vertical-align: middle; font-family: Arial, Helvetica, sans-serif;'>
                               <tbody>
                                  <tr>
                                      <td colspan = '2'>
                                         <table width='100%' cellpadding='0' cellspacing='0'>
                                            <tbody>
                                               <tr>
                                                  <td width = '85%' style='font-size:16px; font-weight:700; vertical-align:top; text-align: center;'>NAWA RADIO<br>
                                                      <p style = 'margin:5px 0;'> Marketing Department</p>
                                                      Broadcasting Agreement Paper
                                                  </td>
                                                  <td width = '15%' style= 'text-align:right;'>  
                                                    
                                                  </td>
                                              </tr>
                                          </tbody>
                                       </table>
                                    </td>
                                   <td></td>
                               </tr>
                               <tr>
                                  <td colspan= '2'>
                                      <table width= '100%' style= 'border: 1px solid; padding: 5px;'>
                                          <tbody>
                                              <tr>
                                                 <td colspan= '2' style= 'font-size:14px; text-align:center;'>
                                                    Add: Khushhal Khan Meena in front of Dawat University<br>
                                                    Kabul, Afghanistan<br>
                                                    Email: Marketing @sabacent.org<br>
                                                    Phone # 0703141414<br>
                                                 </td>
                                             </tr>
                                         </tbody>
                                     </table>
                                  </td>
                               </tr>
                               <tr>
                                  <td colspan = '2'>
                                     <table width= '100%' cellpadding= '0' cellspacing= '0'>
                                        <tbody>
                                           <tr>
                                              <td colspan= '2' style= 'font-size:13px;' ><b> Contractor:</b><br>
                                                 <b>Subject of Contract:</b> Broadcasting of Spots<br>
                                                 This contract is between NAWA RADIO 103.1FM as vender and (
                                                 <b>" + JobDetails.ClientName + @"</b> ) as a client.<br>
                                                 The contract is based on: <b>" + JobDetails.StartDate.ToShortDateString() + @"</b> up
                                                 to <b>" + JobDetails.EndDate.ToShortDateString() + @"</b><br>
                                              </td>
                                          </tr>
                                          <tr>
                                             <td colspan = '2' style='font-size:15px;font-weight: 700; color:#000;'>
                                                <p style = 'padding:10px  0;' > Both parties' responsibilities are as follow:</p>
                                            </td>
                                          </tr>
                                          <tr>
                                              <td colspan = '2' style='font-size:13px; color:#000;padding-left:20px;'>
                                                  <p>1. Broadcasting of(Spots) in NAWA Radio.</p>
                                                  <p>2. Radio airtimes should be in Flat time.</p>
                                                  <p>3. Programs status : ( Active )</p>
                                                  <p>4. The broadcasting will be provided by</p>
                                                  <p>The broadcasting cost of one month will be (
                                                  <b>" + JobDetails.UnitRate + @"</b> )</p>
                                              </td>
                                          </tr>
                                          <tr>
                                             <td colspan = '2' style='font-size:15px;font-weight: 700; color:#000;'>
                                                 <p style = 'padding:10px  0;'> Both parties' responsibilities are as follow:</p>
                                             </td>
                                          </tr>
                                          <tr>
                                             <td colspan = '2' style='font-size:13px; color:#000;padding-left:20px;'>
                                                <p style = 'font-weight:700;' > 1 - Customer:</p>
                                                <p>- Payment of amount.before the starting of broadcasting.<br>
                                                   - If client once approve the program format it will be his/her
                                                   responsibility even if any mistakes were there.<br>
                                                   - Provision of the schedule.<br>
                                                   - The programs should not be against National benefits and Radio
                                                   Nawa policies.
                                                </p>
                                                <p style = 'font-weight:700;' > 2 - NAWA RADIO</p>
                                                <p>  - Broadcasting of (Spots) Audio programs in Flat times as per
                                                     the approved schedule.<br>
                                                </p>
                                            </td>
                                         </tr>
                                         <tr>
                                           <td colspan = '2' style= 'font-size:15px;padding-top:10px;'>
                                             <b> Note:</b>  NAWA RADIO has no legal responsibility for the subjects and
                                             contents of the programs and advertisements.
                                           </td>
                                         </tr>
                                      </tbody>
                                   </table>
                                 </td>
                               </tr>
                               <tr>
                                 <td colspan = '2'><hr></td>
                               </tr>
                               <tr>
                                 <td colspan= '2'>
                                    <p style= 'font-size:13px;' > Both parties are agreed to terms and conditions in the contract stated above.</p>
                                 </td>
                               </tr>
                               <tr>
                                  <td style = 'font-size: 15px;padding-top:10px;'>
                                     <p><b> NAWA RADIO</b></p>
                                     <p><b>Representative Name:</b></p>
                                     <p><b>Signature:</b></p>
                                     <p><b>Date:</b>" + DateTime.UtcNow.ToShortDateString() + @"</p>
                                  </td>
                                  <td>
                                     <p><b>Customer's Name: </b>" + JobDetails.ClientName + @"</p>
                                     <p><b>Signature:</b></p>
                                  </td>
                               </tr>
                            </tbody>
                        </table>
                     </div>
                     </div>
                 </div>";

                PdfDocument doc = converter.ConvertHtmlString(TxtHtmlCode);
                pdf = doc.Save();
                Console.WriteLine(doc);
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
                response.data.pdf = pdf;
                doc.Close();

            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;

        }

        public async Task<APIResponse> GenerateInvoice(int jobId, string userId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var scheduleList = await _uow.ScheduleDetailsRepository.FindAllAsync(x => x.JobId == jobId);
                var playout = _uow.PlayoutMinutesRepository.FindAll(x => scheduleList.Any(s => s.ScheduleId == x.ScheduleId)).ToList();
                APIResponse response1 = await GetJobDetailsById(jobId, userId);
                InvoiceModel invoiceDetails = new InvoiceModel();

                if (scheduleList.Count > 0)
                {
                    if (playout.Count > 0)
                    {
                        var TotalMinutes = (from n in playout
                                            group n by 1 into og
                                            select new
                                            {
                                                TotalTransacted = og.Sum(item => item.TotalMinutes),
                                            }).FirstOrDefault().TotalTransacted;

                        //var currency = _uow.CurrencyDetailsRepository.GetAll().AsQueryable().Where(x => x.CurrencyCode == response1.data.JobPriceDetail.CurrencyCode).FirstOrDefault();
                        //response.data.JobPriceDetail = response1.data.JobPriceDetail;


                        invoiceDetails.TotalRunningMinutes = TotalMinutes;
                    }
                    else
                    {
                        var minutes = from n in scheduleList
                                      select new
                                      {
                                          TotalTransacted = ((n.EndTime - n.StartTime).TotalHours) * 60,
                                      };
                        var TotalMinutes = (from n in minutes
                                            group n by 1 into og
                                            select new
                                            {
                                                TotalTransacted = og.Sum(item => item.TotalTransacted),
                                            }).FirstOrDefault().TotalTransacted;
                        invoiceDetails.TotalRunningMinutes = Convert.ToInt32(TotalMinutes);
                    }

                    invoiceDetails.JobId = jobId;
                    //response1.data.JobPriceDetail.CurrencyCode;
                    invoiceDetails.EndDate = response1.data.JobPriceDetail.EndDate;
                    invoiceDetails.JobRate = response1.data.JobPriceDetail.TotalPrice;
                    invoiceDetails.CurrencyCode = response1.data.JobPriceDetail.CurrencyCode;
                    invoiceDetails.TotalMinutes = response1.data.JobPriceDetail.Minutes;
                    invoiceDetails.JobName = response1.data.JobPriceDetail.JobName;
                    invoiceDetails.ClientName = response1.data.JobPriceDetail.ClientName;
                    invoiceDetails.FinalPrice = (invoiceDetails.JobRate / invoiceDetails.TotalMinutes) * invoiceDetails.TotalRunningMinutes;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.data.invoiceDetails = invoiceDetails;
                    response.Message = "Success";
                }

                else
                {
                    response.Message = "Schedule does not exist";
                    response.StatusCode = StaticResource.notFoundCode;
                }

            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> ApproveInvoice(int jobId, string userId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var invoiceDetails = _uow.InvoiceApprovalRepository.GetAll().AsQueryable().Where(x => x.JobId == jobId).FirstOrDefault();
                if (invoiceDetails == null)
                {
                    InvoiceApproval obj = new InvoiceApproval();
                    obj.IsDeleted = false;
                    obj.JobId = jobId;
                    obj.IsInvoiceApproved = true;
                    obj.CreatedById = userId;
                    obj.CreatedDate = DateTime.Now;
                    await _uow.InvoiceApprovalRepository.AddAsyn(obj);
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Invoice Approved successfully";
                }
                else
                {
                    var existRecord = await _uow.InvoiceApprovalRepository.FindAsync(x => x.IsDeleted == false && x.JobId == jobId);
                    if (existRecord != null)
                    {
                        existRecord.IsInvoiceApproved = true;
                        existRecord.IsDeleted = false;
                        existRecord.ModifiedById = userId;
                        existRecord.ModifiedDate = DateTime.Now;
                        await _uow.InvoiceApprovalRepository.UpdateAsyn(existRecord);
                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = "Invoice Approved successfully";
                    }
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


using AutoMapper;
using DataAccess;
using DataAccess.DbEntities;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Service.APIResponses;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using HumanitarianAssistance.Service.interfaces;
using HumanitarianAssistance.ViewModels.Models.Project;
using DataAccess.DbEntities.Project;
using HumanitarianAssistance.Common.Enums;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Entities;
using System.Linq;
using System.Data.Common;
using Microsoft.EntityFrameworkCore.Storage;
using HumanitarianAssistance.ViewModels.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using DataAccess.DbEntities.ErrorLog;

namespace HumanitarianAssistance.Service.Classes
{

    public class ProjectService : IProject
    {
        IUnitOfWork _uow;
        IMapper _mapper;
        UserManager<AppUser> _userManager;
        private IHostingEnvironment _hostingEnvironment;
        public ProjectService(IUnitOfWork uow, IMapper mapper, UserManager<AppUser> userManager, IHostingEnvironment hostingEnvironment)
        {
            this._uow = uow;
            this._mapper = mapper;
            this._userManager = userManager;
            _hostingEnvironment = hostingEnvironment;
        }

        #region Donor Details
        public async Task<APIResponse> GetAllDonorFilterList(DonorFilterModel donorFilterModel)
        {
            APIResponse response = new APIResponse();
            try
            {

                int totalCount = await _uow.GetDbContext().DonorDetail.Where(x => x.IsDeleted == false).AsNoTracking().CountAsync();

                var list = await _uow.GetDbContext().DonorDetail.Where(x => !x.IsDeleted.Value)
                    .OrderByDescending(x => x.DonorId)
                    .Skip(donorFilterModel.pageSize.Value * donorFilterModel.pageIndex.Value)
                    .Take(donorFilterModel.pageSize.Value)
                    .ToListAsync();

                response.data.DonorDetail = list;
                response.data.TotalCount = totalCount;

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

        public async Task<APIResponse> GetAllDonorList()
        {
            APIResponse response = new APIResponse();
            try
            {
                var list = await _uow.GetDbContext().DonorDetail.Where(x => !x.IsDeleted.Value)
                    .OrderByDescending(x => x.DonorId).ToListAsync();
                response.data.DonorDetail = list;
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



        public async Task<APIResponse> AddEditDonorDetails(DonorModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                if (model.DonorId == 0)
                {
                    DonorDetail obj = _mapper.Map<DonorModel, DonorDetail>(model);
                    obj.Name = model.Name;
                    obj.ContactPerson = model.ContactPerson;
                    obj.ContactDesignation = model.ContactDesignation;
                    obj.ContactPersonEmail = model.ContactPersonEmail;
                    obj.ContactPersonCell = model.ContactPersonCell;
                    obj.IsDeleted = false;
                    obj.CreatedById = UserId;
                    obj.CreatedDate = DateTime.Now;
                    await _uow.DonorDetailRepository.AddAsyn(obj);
                    await _uow.SaveAsync();
                    //model.DonorId = obj.DonorId;
                    //var DonarDetail = await _uow.GetDbContext().DonorDetail
                    //  .FirstOrDefaultAsync(x => !x.IsDeleted.Value && x.DonorId == obj.DonorId);
                    response.data.DonorDetailById = obj;
                    response.data.TotalCount = await _uow.GetDbContext().DonorDetail.Where(x => x.IsDeleted == false).AsNoTracking().CountAsync();

                }
                else
                {
                    var existRecord = await _uow.DonorDetailRepository.FindAsync(x => x.IsDeleted == false && x.DonorId == model.DonorId);
                    if (existRecord != null)
                    {
                        existRecord.Name = model.Name;
                        existRecord.ContactPerson = model.ContactPerson;
                        existRecord.ContactDesignation = model.ContactDesignation;
                        existRecord.ContactPersonEmail = model.ContactPersonEmail;
                        existRecord.ContactPersonCell = model.ContactPersonCell;
                        existRecord.ModifiedById = UserId;
                        existRecord.ModifiedDate = DateTime.Now;
                        _uow.GetDbContext().SaveChanges();
                        response.StatusCode = StaticResource.successStatusCode;
                        response.data.DonorDetailById = existRecord;
                        response.Message = "Success";
                    }
                }
                response.StatusCode = StaticResource.successStatusCode;
                response.CommonId.Id = Convert.ToInt32(model.DonorId);
                response.Message = "Success";
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }
        public async Task<APIResponse> EditDonorDetails(DonorModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var existRecord = await _uow.DonorDetailRepository.FindAsync(x => x.IsDeleted == false && x.DonorId == model.DonorId);
                if (existRecord != null)
                {
                    existRecord.Name = model.Name;
                    existRecord.ContactPerson = model.ContactPerson;
                    existRecord.ContactDesignation = model.ContactDesignation;
                    existRecord.ContactPersonEmail = model.ContactPersonEmail;
                    existRecord.ContactPersonCell = model.ContactPersonCell;
                    existRecord.ModifiedById = UserId;
                    existRecord.ModifiedDate = DateTime.Now;
                    _uow.GetDbContext().SaveChanges();
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Record not found";
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

        public async Task<APIResponse> DeleteDonorDetails(long DonarId, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var DonorInfo = await _uow.DonorDetailRepository.FindAsync(c => c.DonorId == DonarId);
                DonorInfo.IsDeleted = true;
                DonorInfo.ModifiedById = UserId;
                DonorInfo.ModifiedDate = DateTime.Now;
                await _uow.DonorDetailRepository.UpdateAsyn(DonorInfo, DonorInfo.DonorId);
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
                response.data.TotalCount = await _uow.GetDbContext().DonorDetail.Where(x => x.IsDeleted == false).AsNoTracking().CountAsync();

            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }
        public async Task<APIResponse> GetDonarListById(long DonarId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var DonarDetail = await _uow.GetDbContext().DonorDetail
                       .FirstOrDefaultAsync(x => !x.IsDeleted.Value && x.DonorId == DonarId);
                response.data.DonorDetailById = DonarDetail;
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
        #endregion

        #region Sector Details
        /// <summary>
        /// Get All Sector Details
        /// </summary>
        /// <returns></returns>
        public async Task<APIResponse> GetAllSectorList()
        {
            APIResponse response = new APIResponse();
            try
            {
                var list = await _uow.SectorDetailsRepository.FindAllAsync(x => !x.IsDeleted.Value);
                response.data.sectorDetails = list;
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
        /// Add New Sector Details 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<APIResponse> AddSectorDetails(SectorModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            long LatestCodeId = 0;
            var code = string.Empty;
            try
            {

                var data = _uow.GetDbContext().SectorDetails.FirstOrDefault(x => x.IsDeleted == false && x.SectorName.Trim().ToLower() == model.SectorName.Trim().ToLower());

                if (data == null)
                {
                    SectorDetails obj = new SectorDetails();
                    var sectorDetail = _uow.GetDbContext().SectorDetails
                                                               .OrderByDescending(x => x.SectorId)
                                                               .FirstOrDefault(x => x.IsDeleted == false);
                    if (sectorDetail == null)
                    {
                        LatestCodeId = 1;
                        code = genrateCode(LatestCodeId.ToString());
                    }
                    else
                    {
                        LatestCodeId = sectorDetail.SectorId + 1;
                        code = genrateCode(LatestCodeId.ToString());
                    }
                    obj.SectorName = model.SectorName;
                    obj.IsDeleted = false;
                    obj.SectorCode = code;
                    obj.CreatedById = UserId;
                    obj.CreatedDate = DateTime.Now;
                    await _uow.SectorDetailsRepository.AddAsyn(obj);
                    await _uow.SaveAsync();
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.NameAlreadyExist;
                    response.Message = StaticResource.ListNameAlreadyExist;
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
        /// Edit Sector Details 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<APIResponse> EditSectorDetails(SectorModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var existRecord = await _uow.SectorDetailsRepository.FindAsync(x => x.IsDeleted == false && x.SectorId == model.SectorId);
                if (existRecord != null)
                {
                    _mapper.Map(model, existRecord);
                    existRecord.IsDeleted = false;
                    existRecord.ModifiedById = UserId;
                    existRecord.ModifiedDate = DateTime.Now;

                    await _uow.SectorDetailsRepository.UpdateAsyn(existRecord);

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
        /// delete Sector Details 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<APIResponse> DeleteSectorDetails(SectorDetails model)
        {
            APIResponse response = new APIResponse();
            try
            {
                var SectorInfo = await _uow.SectorDetailsRepository.FindAsync(c => c.SectorId == model.SectorId);
                SectorInfo.IsDeleted = true;
                SectorInfo.ModifiedById = model.ModifiedById;
                SectorInfo.ModifiedDate = model.ModifiedDate;
                await _uow.SectorDetailsRepository.UpdateAsyn(SectorInfo, SectorInfo.SectorId);
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

        #region Program Details
        /// <summary>
        /// Get All Program Details
        /// </summary>
        /// <returns></returns>
        public async Task<APIResponse> GetAllProgramList()
        {
            APIResponse response = new APIResponse();
            try
            {
                var list = await _uow.ProgramDetailRepository.FindAllAsync(x => !x.IsDeleted.Value);
                response.data.programDetails = list;
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
        /// Add New Program Details 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<APIResponse> AddProgramDetails(ProgramModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            long LatestCodeId = 0;
            var code = string.Empty;
            try
            {
                ProgramDetail obj = _mapper.Map<ProgramModel, ProgramDetail>(model);


                var data = _uow.GetDbContext().ProgramDetail.FirstOrDefault(x => x.IsDeleted == false && x.ProgramName.Trim().ToLower() == model.ProgramName.Trim().ToLower()); //  Contains(model.ProgramName);                               


                if (data == null)
                {
                    var ProgramDetail = _uow.GetDbContext().ProgramDetail
                                                              .OrderByDescending(x => x.ProgramId)
                                                              .FirstOrDefault(x => x.IsDeleted == false);
                    if (ProgramDetail == null)
                    {
                        LatestCodeId = 1;
                        code = genrateCode(LatestCodeId.ToString());
                    }
                    else
                    {
                        LatestCodeId = ProgramDetail.ProgramId + 1;
                        code = genrateCode(LatestCodeId.ToString());
                    }
                    obj.ProgramName = model.ProgramName;
                    obj.IsDeleted = false;
                    obj.ProgramCode = code;
                    obj.CreatedById = UserId;
                    obj.CreatedDate = DateTime.Now;
                    await _uow.ProgramDetailRepository.AddAsyn(obj);
                    await _uow.SaveAsync();
                    response.data.ProgramDetail = obj;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.NameAlreadyExist;
                    response.Message = StaticResource.ListNameAlreadyExist;
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
        /// Edit Program Details 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<APIResponse> EditProgramDetails(ProgramModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var existRecord = await _uow.ProgramDetailRepository.FindAsync(x => x.IsDeleted == false && x.ProgramId == model.ProgramId);
                if (existRecord != null)
                {
                    _mapper.Map(model, existRecord);
                    existRecord.IsDeleted = false;
                    existRecord.ModifiedById = UserId;
                    existRecord.ModifiedDate = DateTime.Now;

                    await _uow.ProgramDetailRepository.UpdateAsyn(existRecord);

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
        /// delete Program Details 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<APIResponse> DeleteProgramDetails(ProgramDetail model)
        {
            APIResponse response = new APIResponse();
            try
            {
                var ProgramInfo = await _uow.ProgramDetailRepository.FindAsync(c => c.ProgramId == model.ProgramId);
                ProgramInfo.IsDeleted = true;
                ProgramInfo.ModifiedById = model.ModifiedById;
                ProgramInfo.ModifiedDate = model.ModifiedDate;
                await _uow.ProgramDetailRepository.UpdateAsyn(ProgramInfo, ProgramInfo.ProgramId);
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

        #region Area Details
        /// <summary>
        /// Get All Area Details
        /// </summary>
        /// <returns></returns>
        public async Task<APIResponse> GetAllAreaList()
        {
            APIResponse response = new APIResponse();
            try
            {
                var list = await _uow.AreaDetailRepository.FindAllAsync(x => !x.IsDeleted.Value);
                response.data.AreaDetail = list;
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
        /// Add New Program Details 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<APIResponse> AddAreaDetails(AreaModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            long LatestCodeId = 0;
            var code = string.Empty;
            try
            {

                var data = _uow.GetDbContext().AreaDetail.FirstOrDefault(x => x.IsDeleted == false && x.AreaName.Trim().ToLower() == model.AreaName.Trim().ToLower());

                if (data == null)
                {
                    AreaDetail obj = _mapper.Map<AreaModel, AreaDetail>(model);
                    var AreaDetail = _uow.GetDbContext().AreaDetail
                                                               .OrderByDescending(x => x.AreaId)
                                                               .FirstOrDefault(x => x.IsDeleted == false);
                    if (AreaDetail == null)
                    {
                        LatestCodeId = 1;
                        code = genrateCode(LatestCodeId.ToString());
                    }
                    else
                    {
                        LatestCodeId = AreaDetail.AreaId + 1;
                        code = genrateCode(LatestCodeId.ToString());
                    }
                    obj.AreaCode = code;
                    obj.AreaName = model.AreaName;
                    obj.IsDeleted = false;
                    obj.CreatedById = UserId;
                    obj.CreatedDate = DateTime.Now;
                    await _uow.AreaDetailRepository.AddAsyn(obj);
                    await _uow.SaveAsync();
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.NameAlreadyExist;
                    response.Message = StaticResource.ListNameAlreadyExist;
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
        /// Edit Program Details 
        /// </summary>
        /// <param name="model"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<APIResponse> EditAreaDetails(AreaModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var existRecord = await _uow.AreaDetailRepository.FindAsync(x => x.IsDeleted == false && x.AreaId == model.AreaId);
                if (existRecord != null)
                {
                    _mapper.Map(model, existRecord);
                    existRecord.IsDeleted = false;
                    existRecord.ModifiedById = UserId;
                    existRecord.ModifiedDate = DateTime.Now;

                    await _uow.AreaDetailRepository.UpdateAsyn(existRecord);

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
        /// delete Program Details 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<APIResponse> DeleteAreaDetails(AreaDetail model)
        {
            APIResponse response = new APIResponse();
            try
            {
                var AreaInfo = await _uow.AreaDetailRepository.FindAsync(c => c.AreaId == model.AreaId);
                AreaInfo.IsDeleted = true;
                AreaInfo.ModifiedById = model.ModifiedById;
                AreaInfo.ModifiedDate = model.ModifiedDate;
                await _uow.AreaDetailRepository.UpdateAsyn(AreaInfo, AreaInfo.AreaId);
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

        #region Consideration 

        /// <summary>
        /// Get All GenderConsiderationList
        /// </summary>
        /// <returns></returns>
        public async Task<APIResponse> GenderConsiderationList()
        {
            APIResponse response = new APIResponse();
            try
            {
                var list = await _uow.GenderConsiderationRepository.FindAllAsync(x => !x.IsDeleted.Value);
                response.data.GenderConsiderationDetail = list;
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
        /// Get All StrengthConsiderationDetailList
        /// </summary>
        /// <returns></returns>
        public async Task<APIResponse> StrengthConsiderationDetailList()
        {
            APIResponse response = new APIResponse();
            try
            {
                var list = await _uow.StrengthConsiderationRepository.FindAllAsync(x => !x.IsDeleted.Value);
                response.data.StrengthConsiderationDetail = list;
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
        /// Get All StrengthConsiderationDetailList
        /// </summary>
        /// <returns></returns>
        public async Task<APIResponse> SecurityDetailList()
        {
            APIResponse response = new APIResponse();
            try
            {
                var list = await _uow.SecurityDetailRepository.FindAllAsync(x => !x.IsDeleted.Value);
                response.data.SecurityDetail = list;
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
        /// Get All SecurityConsiderationDetailList
        /// </summary>
        /// <returns></returns>
        public async Task<APIResponse> SecurityConsiderationDetailList()
        {
            APIResponse response = new APIResponse();
            try
            {
                var list = await _uow.SecurityConsiderationDetailRepository.FindAllAsync(x => !x.IsDeleted.Value);
                response.data.SecurityConsiderationDetail = list;
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


        #endregion

        #region Add/Edit/Delete Project Info 

        #region Add/Edit ProjectDetail

        public APIResponse AddEditProjectDetail(ProjectDetailNewModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            ProjectPhaseTime _ProjectPhase = new ProjectPhaseTime();
            ProjectDetail _ProjectDetail = new ProjectDetail();
            DbContext db = _uow.GetDbContext();
            long LatestprojectId = 0;
            var projectcode = string.Empty;
            using (IDbContextTransaction tran = db.Database.BeginTransaction())
            {
                try
                {
                    if (model.ProjectId == 0)
                    {
                        var ProjectDetail = _uow.GetDbContext().ProjectDetail
                                                           .OrderByDescending(x => x.ProjectId)
                                                           .FirstOrDefault(x => x.IsDeleted == false);
                        if (ProjectDetail == null)
                        {
                            LatestprojectId = 1;
                            projectcode = genrateCode(LatestprojectId.ToString());
                        }
                        else
                        {
                            LatestprojectId = ProjectDetail.ProjectId + 1;
                            projectcode = genrateCode(LatestprojectId.ToString());
                        }

                        ProjectDetail obj = _mapper.Map<ProjectDetailNewModel, ProjectDetail>(model);
                        obj.ProjectCode = projectcode; // ProjectDetail != null ? getProjectCode(ProjectDetail.ProjectId.ToString()) : getProjectCode("1");

                        obj.ProjectName = model.ProjectName;
                        obj.ProjectDescription = model.ProjectDescription;
                        obj.StartDate = DateTime.Now;
                        obj.EndDate = model.EndDate;
                        obj.IsProposalComplate = model.IsProposalComplate;
                        obj.ReviewerId = model.ReviewerId;
                        obj.DirectorId = model.DirectorId;
                        obj.ProjectPhaseDetailsId = Convert.ToInt64(ProjectPhaseType.DataEntryPhase);
                        obj.IsDeleted = false;
                        obj.IsActive = true;
                        obj.CreatedById = UserId;
                        obj.CreatedDate = DateTime.Now;
                        _uow.ProjectDetailNewRepository.Add(obj);
                        _ProjectPhase.ProjectId = LatestprojectId = obj.ProjectId;
                        _ProjectPhase.ProjectPhaseDetailsId = Convert.ToInt64(ProjectPhaseType.DataEntryPhase);
                        _ProjectPhase.PhaseStartData = DateTime.Now;
                        _ProjectPhase.IsDeleted = false;
                        _ProjectPhase.CreatedById = UserId;
                        _ProjectPhase.CreatedDate = DateTime.Now;
                        _uow.ProjectPhaseTimeRepository.Add(_ProjectPhase);

                        response.data.ProjectDetail = obj; //dont remove this

                    }
                    else
                    {
                        var existProjectRecord = _uow.ProjectDetailNewRepository.Find(x => x.IsDeleted == false && x.ProjectId == model.ProjectId);
                        var exstingProjectTimePhase = _uow.ProjectPhaseTimeRepository.FindAll(y => y.IsDeleted == false && y.ProjectId == model.ProjectId);
                        if (existProjectRecord != null)
                        {
                            existProjectRecord.ProjectName = model.ProjectName;
                            existProjectRecord.ProjectDescription = model.ProjectDescription;
                            existProjectRecord.IsProposalComplate = model.IsProposalComplate;
                            existProjectRecord.ReviewerId = model.ReviewerId;
                            existProjectRecord.DirectorId = model.DirectorId;
                            existProjectRecord.IsDeleted = false;
                            existProjectRecord.ModifiedById = UserId;
                            existProjectRecord.ModifiedDate = DateTime.Now;
                            _uow.GetDbContext().SaveChanges();
                            //    _uow.ProjectDetailNewRepository.UpdateAsyn(existProjectRecord);
                            if (exstingProjectTimePhase != null)
                            {
                                _ProjectPhase.ProjectPhaseDetailsId = Convert.ToInt64(ProjectPhaseType.DataEntryPhase);
                                _ProjectPhase.PhaseStartData = DateTime.Now;
                                _ProjectPhase.IsDeleted = false;
                                _ProjectPhase.ModifiedById = UserId;
                                _ProjectPhase.ModifiedDate = DateTime.Now;
                                // await _uow.ProjectPhaseTimeRepository.UpdateAsyn(_ProjectPhase);
                                _uow.GetDbContext().SaveChanges();

                            }
                            //_uow.GetDbContext().SaveChanges();
                            LatestprojectId = model.ProjectId;
                        }
                    }
                    response.StatusCode = StaticResource.successStatusCode;
                    response.CommonId.Id = Convert.ToInt32(LatestprojectId);

                    response.Message = "Success";
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.SomethingWrong + ex.Message;
                }
            }
            return response;
        }
        //crete code
        public string genrateCode(string id)
        {
            string code = string.Empty;
            if (id.Length == 1)
                return code = "0000" + id;
            else if (id.Length == 2)
                return code = "000" + id;
            else if (id.Length == 3)
                return code = "00" + id;
            else if (id.Length == 4)
                return code = "0" + id;
            else
                return code = id;
        }

        public async Task<APIResponse> DeleteProjectDetail(long ProjectId, string UserId)
        {
            APIResponse response = new APIResponse();
            DbContext db = _uow.GetDbContext();
            using (IDbContextTransaction tran = db.Database.BeginTransaction())
            {
                try
                {

                    ProjectPhaseTime _ProjectPhase = new ProjectPhaseTime();
                    var ProjectInfo = await _uow.ProjectDetailNewRepository.FindAsync(c => c.ProjectId == ProjectId);
                    ProjectInfo.IsDeleted = true;
                    ProjectInfo.ModifiedById = UserId;
                    ProjectInfo.ModifiedDate = DateTime.Now;
                    await _uow.ProjectDetailNewRepository.UpdateAsyn(ProjectInfo, ProjectInfo.ProjectId);
                    _ProjectPhase.IsDeleted = true;
                    _ProjectPhase.ModifiedById = UserId;
                    _ProjectPhase.ModifiedDate = DateTime.Now;
                    await _uow.ProjectPhaseTimeRepository.UpdateAsyn(_ProjectPhase);
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                    tran.Commit();
                }

                catch (Exception ex)
                {
                    tran.Rollback();
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.SomethingWrong + ex.Message;
                }
            }
            return response;
        }

        public async Task<APIResponse> GetAllProjectFilterList(ProjectFilterModel projectFilterModel)
        {
            APIResponse response = new APIResponse();
            try
            {
                int totalCount = await _uow.GetDbContext().ProjectDetail.Where(x => x.IsDeleted == false).AsNoTracking().CountAsync();


                var ProjectList = await _uow.GetDbContext().ProjectDetail
                                          .Where(x => !x.IsDeleted.Value)
                                          .OrderByDescending(x => x.ProjectId)
                                          .Skip(projectFilterModel.pageSize.Value * projectFilterModel.pageIndex.Value)
                                          .Take(projectFilterModel.pageSize.Value)
                                          .Select(x => new ProjectDetailNewModel
                                          {
                                              ProjectId = x.ProjectId,
                                              ProjectCode = x.ProjectCode,
                                              ProjectName = x.ProjectName,
                                              ProjectDescription = x.ProjectDescription,
                                              IsWin = _uow.GetDbContext().WinProjectDetails.Where(y => y.ProjectId == x.ProjectId).Select(y => y.IsWin).FirstOrDefault(),
                                              IsCriteriaEvaluationSubmit = x.IsCriteriaEvaluationSubmit,
                                              ProjectPhase = x.ProjectPhaseDetailsId == x.ProjectPhaseDetails.ProjectPhaseDetailsId ? x.ProjectPhaseDetails.ProjectPhase.ToString() : "",
                                              //? "Data Entry"
                                              // : x.ProjectPhaseDetailsId == (long)ProjectPhaseType.DataEntryPhase
                                              //   ? ""
                                              // : "",

                                              TotalDaysinHours = x.EndDate == null ? (Convert.ToString(Math.Round(DateTime.Now.Subtract(x.StartDate.Value).TotalHours, 0) + ":" + DateTime.Now.Subtract(x.StartDate.Value).Minutes)) : (Convert.ToString(Math.Round(x.EndDate.Value.Subtract(x.StartDate.Value).TotalHours, 0) + ":" + x.EndDate.Value.Subtract(x.StartDate.Value).Minutes))
                                          }).ToListAsync();
                response.data.ProjectDetailModel = ProjectList;
                response.data.TotalCount = totalCount;

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


        public async Task<APIResponse> GetAllProjectList()
        {
            APIResponse response = new APIResponse();
            try
            {
                var ProjectList = await _uow.GetDbContext().ProjectDetail
                                          .Where(x => !x.IsDeleted.Value)
                                          .OrderByDescending(x => x.ProjectId).Select(x => new ProjectDetailNewModel
                                          {
                                              ProjectId = x.ProjectId,
                                              ProjectCode = x.ProjectCode,
                                              ProjectName = x.ProjectName,
                                              ProjectDescription = x.ProjectDescription,
                                              IsWin = _uow.GetDbContext().WinProjectDetails.Where(y => y.ProjectId == x.ProjectId).Select(y => y.IsWin).FirstOrDefault(),
                                              IsCriteriaEvaluationSubmit = x.IsCriteriaEvaluationSubmit,
                                              ProjectPhase = x.ProjectPhaseDetailsId == x.ProjectPhaseDetails.ProjectPhaseDetailsId ? x.ProjectPhaseDetails.ProjectPhase.ToString() : "",
                                              //? "Data Entry"
                                              // : x.ProjectPhaseDetailsId == (long)ProjectPhaseType.DataEntryPhase
                                              //   ? ""
                                              // : "",

                                              TotalDaysinHours = x.EndDate == null ? (Convert.ToString(Math.Round(DateTime.Now.Subtract(x.StartDate.Value).TotalHours, 0) + ":" + DateTime.Now.Subtract(x.StartDate.Value).Minutes)) : (Convert.ToString(Math.Round(x.EndDate.Value.Subtract(x.StartDate.Value).TotalHours, 0) + ":" + x.EndDate.Value.Subtract(x.StartDate.Value).Minutes))
                                          }).ToListAsync();
                response.data.ProjectDetailModel = ProjectList;
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

        public APIResponse GetProjectListById(long ProjectId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var ProjectDetail = (from obj in _uow.GetDbContext().ProjectDetail
                                     join win in _uow.GetDbContext().WinProjectDetails on obj.ProjectId equals win.ProjectId into p
                                     from c in p.DefaultIfEmpty()
                                     join approve in _uow.GetDbContext().ApproveProjectDetails on obj.ProjectId equals approve.ProjectId into z
                                     from approve in z.DefaultIfEmpty()
                                     join Proposal in _uow.GetDbContext().ProjectProposalDetail on obj.ProjectId equals Proposal.ProjectId into pr
                                     from Proposal in pr.DefaultIfEmpty()
                                     join phase in _uow.GetDbContext().ProjectPhaseDetails on obj.ProjectPhaseDetailsId equals phase.ProjectPhaseDetailsId
                                     select new ProjectDetailNewModel
                                     {
                                         ProjectId = obj.ProjectId,
                                         ProjectCode = obj.ProjectCode,
                                         DirectorId = obj.DirectorId,
                                         ReviewerId = obj.ReviewerId,
                                         ProjectName = obj.ProjectName,
                                         ProjectDescription = obj.ProjectDescription,
                                         ProjectPhaseDetailsId = phase.ProjectPhaseDetailsId,
                                         IsWin = c.IsWin,
                                         IsApproved = approve.IsApproved,
                                         IsProposalSubmit = Proposal.IsProposalAccept,
                                         IsCriteriaEvaluationSubmit = obj.IsCriteriaEvaluationSubmit,
                                         IsDelete = obj.IsDeleted
                                         //IsProposalComplate = obj.IsProposalComplate,
                                     }).FirstOrDefault(x => x.ProjectId == ProjectId && x.IsDelete == false);


                response.data.ProjectDetailModel1 = ProjectDetail;
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



        #endregion

        #region Project AssignToEmployee
        public async Task<APIResponse> AddEditProjectAssignToEmployee(ProjectAssignToModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                if (model.ProjectAssignToId > 0)
                {
                    ProjectAssignTo obj = _mapper.Map<ProjectAssignToModel, ProjectAssignTo>(model);
                    obj.ProjectId = model.ProjectId;
                    obj.EmployeeId = model.EmployeeId;
                    obj.IsDeleted = false;
                    obj.CreatedById = UserId;
                    obj.CreatedDate = DateTime.Now;
                    await _uow.ProjectAssignToRepository.AddAsyn(obj);
                    await _uow.SaveAsync();

                }
                else
                {
                    var existRecord = await _uow.ProjectAssignToRepository.FindAsync(x => x.IsDeleted == false && x.ProjectAssignToId == model.ProjectAssignToId && x.ProjectId == model.ProjectId);
                    if (existRecord != null)
                    {
                        _mapper.Map(model, existRecord);
                        existRecord.IsDeleted = false;
                        existRecord.ModifiedById = UserId;
                        existRecord.ModifiedDate = DateTime.Now;
                        await _uow.ProjectAssignToRepository.UpdateAsyn(existRecord);
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

        public async Task<APIResponse> DeleteProjectAssignToEmployee(ProjectAssignTo model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var ProjectAssignToInfo = await _uow.ProjectAssignToRepository.FindAsync(c => c.ProjectAssignToId == model.ProjectAssignToId && c.ProjectId == model.ProjectId);
                ProjectAssignToInfo.IsDeleted = true;
                ProjectAssignToInfo.ModifiedById = model.ModifiedById;
                ProjectAssignToInfo.ModifiedDate = model.ModifiedDate;
                await _uow.ProjectAssignToRepository.UpdateAsyn(ProjectAssignToInfo, ProjectAssignToInfo.ProjectAssignToId);
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

        #region Project Program
        public async Task<APIResponse> AddEditProjectProgram(ProjectProgramModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var existRecord = await _uow.ProjectProgramRepository.FindAsync(x => x.IsDeleted == false && x.ProjectId == model.ProjectId);
                if (existRecord == null)
                {
                    ProjectProgram obj = new ProjectProgram();
                    obj.ProjectId = model.ProjectId;
                    obj.ProgramId = model.ProgramId;
                    obj.IsDeleted = false;
                    obj.CreatedById = UserId;
                    obj.CreatedDate = DateTime.Now;
                    await _uow.ProjectProgramRepository.AddAsyn(obj);
                    await _uow.SaveAsync();
                }
                else
                {
                    if (existRecord != null)
                    {
                        existRecord.ProjectId = model.ProjectId;
                        existRecord.ProgramId = model.ProgramId;
                        existRecord.IsDeleted = false;
                        existRecord.ModifiedById = UserId;
                        existRecord.ModifiedDate = DateTime.Now;
                        _uow.GetDbContext().SaveChanges();
                    }
                }
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

        public async Task<APIResponse> DeleteProjectProgram(ProjectProgram model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var ProjectProgramInfo = await _uow.ProjectProgramRepository.FindAsync(c => c.ProjectProgramId == model.ProjectProgramId && c.ProjectId == model.ProjectId);
                ProjectProgramInfo.IsDeleted = true;
                ProjectProgramInfo.ModifiedById = model.ModifiedById;
                ProjectProgramInfo.ModifiedDate = model.ModifiedDate;
                await _uow.ProjectProgramRepository.UpdateAsyn(ProjectProgramInfo, ProjectProgramInfo.ProjectProgramId);
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
        public async Task<APIResponse> getProjectProgramById(long ProjectId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var Projectprogram = await _uow.GetDbContext().ProjectProgram
                       .FirstOrDefaultAsync(x => !x.IsDeleted.Value && x.ProjectId == ProjectId);
                response.data.projectProgram = Projectprogram;
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

        #endregion
        #region Project Sector
        public async Task<APIResponse> AddEditProjectSector(ProjectSectorModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var existRecord = await _uow.ProjectSectorRepository.FindAsync(x => x.IsDeleted == false && x.ProjectId == model.ProjectId);
                if (existRecord == null)
                {
                    ProjectSector obj = new ProjectSector();
                    obj.ProjectId = model.ProjectId;
                    obj.SectorId = model.SectorId;
                    obj.IsDeleted = false;
                    obj.CreatedById = UserId;
                    obj.CreatedDate = DateTime.Now;
                    await _uow.ProjectSectorRepository.AddAsyn(obj);
                    await _uow.SaveAsync();
                }
                else
                {
                    if (existRecord != null)
                    {
                        // _mapper.Map(model, existRecord);
                        existRecord.ProjectId = model.ProjectId;
                        existRecord.SectorId = model.SectorId;
                        existRecord.IsDeleted = false;
                        existRecord.ModifiedById = UserId;
                        existRecord.ModifiedDate = DateTime.Now;
                        await _uow.ProjectSectorRepository.UpdateAsyn(existRecord);
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
        public async Task<APIResponse> getProjectSectorById(long ProjectId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var Projectsector = await _uow.GetDbContext().ProjectSector
                       .FirstOrDefaultAsync(x => !x.IsDeleted.Value && x.ProjectId == ProjectId);
                response.data.projectSector = Projectsector;
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
        public async Task<APIResponse> DeleteProjectSector(ProjectSector model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var ProjectSectorInfo = await _uow.ProjectSectorRepository.FindAsync(c => c.ProjectSectorId == model.ProjectSectorId && c.ProjectId == model.ProjectId);
                ProjectSectorInfo.IsDeleted = true;
                ProjectSectorInfo.ModifiedById = model.ModifiedById;
                ProjectSectorInfo.ModifiedDate = model.ModifiedDate;
                await _uow.ProjectSectorRepository.UpdateAsyn(ProjectSectorInfo, ProjectSectorInfo.ProjectId);
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
        #region Project Area
        public async Task<APIResponse> AddEditProjectArea(ProjectAreaModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var existRecord = await _uow.ProjectAreaRepository.FindAsync(x => x.IsDeleted == false && x.ProjectId == model.ProjectId);
                if (existRecord == null)
                {
                    ProjectArea obj = new ProjectArea();
                    obj.ProjectId = model.ProjectId;
                    obj.AreaId = model.AreaId;
                    obj.IsDeleted = false;
                    obj.CreatedById = UserId;
                    obj.CreatedDate = DateTime.Now;
                    await _uow.ProjectAreaRepository.AddAsyn(obj);
                    await _uow.SaveAsync();
                }
                else
                {
                    // _mapper.Map(model, existRecord);
                    existRecord.ProjectId = model.ProjectId;
                    existRecord.AreaId = model.AreaId;
                    existRecord.IsDeleted = false;
                    existRecord.ModifiedById = UserId;
                    existRecord.ModifiedDate = DateTime.Now;
                    _uow.GetDbContext().SaveChanges();

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
        public async Task<APIResponse> getProjectAreaById(long ProjectId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var Projectarea = await _uow.GetDbContext().ProjectArea
                       .FirstOrDefaultAsync(x => !x.IsDeleted.Value && x.ProjectId == ProjectId);
                response.data.projectArea = Projectarea;
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
        public async Task<APIResponse> DeleteProjectArea(ProjectArea model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var ProjectSectorInfo = await _uow.ProjectAreaRepository.FindAsync(x => x.IsDeleted == false && x.ProjectAreaId == model.ProjectAreaId && x.ProjectId == model.ProjectId);
                if (ProjectSectorInfo != null)
                {
                    ProjectSectorInfo.IsDeleted = true;
                    ProjectSectorInfo.ModifiedById = model.ModifiedById;
                    ProjectSectorInfo.ModifiedDate = model.ModifiedDate;
                    _uow.GetDbContext().SaveChanges();
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
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
        #endregion

        #region Project Communication
        public async Task<APIResponse> GetChatByProjectId(long ProjectId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var ProjectCommunicationModel = _uow.GetDbContext().ProjectCommunication.Where(x => !x.IsDeleted.Value && x.ProjectId == ProjectId).Select(y => new ProjectCommunicationModel()
                {
                    ProjectDescription = y.ProjectDescription,
                    CreatedByName = _uow.GetDbContext().UserDetails.Where(z => z.AspNetUserId == y.CreatedById).Select(p => p.FirstName + " " + p.LastName).FirstOrDefault(),
                    RoleId = _uow.GetDbContext().UserRoles.Where(z => z.UserId == y.CreatedById).Select(z => z.RoleId).FirstOrDefault(),
                    CreatedDate = y.CreatedDate.Value.ToString("dd MMMM yyyy h:mm tt"),
                    CreatedById = y.CreatedById,
                    PCId = y.PCId,
                }).ToList();
                var resp = (from obj in ProjectCommunicationModel
                            join role in _uow.GetDbContext().Roles on obj.RoleId equals role.Id
                            select new ProjectCommunicationModel
                            {
                                ProjectDescription = obj.ProjectDescription,
                                CreatedByName = obj.CreatedByName,
                                UserRole = role.Name == "HR Manager" ? "HR" : role.Name == "SuperAdmin" ? "SA" : role.Name == "Accounting Manager" ? "AM" : role.Name == "Administrator" ? "AD" : role.Name == "Project Manager" ? "PM" : "",
                                CreatedDate = obj.CreatedDate,
                                PCId = obj.PCId
                            }).ToList();

                response.data.ProjectCommunicationModel = resp;
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

        public async Task<APIResponse> AddProjectChat(ProjectCommunicationModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            ProjectCommunication _chat = new ProjectCommunication();
            try
            {
                if (model != null)
                {
                    _chat.ProjectDescription = model.ProjectDescription;
                    _chat.IsDeleted = false;
                    _chat.ProjectId = model.ProjectId;
                    _chat.CreatedById = UserId;
                    _chat.CreatedDate = DateTime.Now;
                    await _uow.ProjectCommunicationRepository.AddAsyn(_chat);
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                    //await GetChatByProjectId(model.ProjectId);
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


        #region Master Page
        /// <summary>
        /// Get All Area Details
        /// </summary>
        /// <returns></returns>
        public APIResponse GetAllProvinceDetails()
        {
            APIResponse response = new APIResponse();
            try
            {
                var provincelist = _uow.GetDbContext().ProvinceDetails.Where(x => x.IsDeleted == false).Select(x => new ProvinceDetailsModel
                {
                    ProvinceId = x.ProvinceId,
                    ProvinceName = x.ProvinceName
                }).OrderBy(x => x.ProvinceName).ToList();

                //(from p in _uow.GetDbContext().ProvinceDetails()
                //                    where p.IsDeleted == false
                //                    select new ProvinceDetailsModel
                //                    {
                //                        ProvinceId = p.ProvinceId,
                //                        ProvinceName = p.ProvinceName
                //                    }).OrderBy(x => x.ProvinceName).ToList();
                response.data.ProvinceDetailsList = provincelist;
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

        public APIResponse GetProvinceMultiSelectByProjectId(long ProjectId)
        {
            APIResponse response = new APIResponse();
            try
            {

                List<int> SelectedProvinceList = _uow.GetDbContext().ProvinceMultiSelect.Where(x => x.ProjectId == ProjectId && x.IsDeleted == false).Select(x => x.ProvinceId).ToList();

                //details.ProjectSelectionId = selectedProjects != null ? selectedProjects : null;

                response.data.ProvinceMultiSelectById = SelectedProvinceList;
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

        public APIResponse AddEditProvinceMultiSelectDetail(ProvinceMultiSelectModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {

                if (model.ProvinceId != null)
                {

                    //bool securityPresent = _uow.GetDbContext().ProvinceMultiSelect.Any(x => x.ProjectId == model.ProjectId && x.IsDeleted == false);
                    var provinceExist = _uow.GetDbContext().ProvinceMultiSelect.Where(x => x.ProjectId == model.ProjectId && x.IsDeleted == false).ToList();

                    var noExistProvinceId = provinceExist.Where(x => !model.ProvinceId.Contains(x.ProvinceId)).Select(x => x.ProvinceId).ToList();

                    if (provinceExist.Any())
                    {
                        var districtExist = _uow.GetDbContext().DistrictMultiSelect.Where(x => noExistProvinceId.Contains(x.ProvinceId) && x.IsDeleted == false).ToList();
                        if (districtExist.Any())
                        {
                            _uow.GetDbContext().DistrictMultiSelect.RemoveRange(districtExist);
                            _uow.GetDbContext().SaveChanges();
                        }
                    }

                    _uow.GetDbContext().ProvinceMultiSelect.RemoveRange(provinceExist);
                    _uow.GetDbContext().SaveChanges();


                    List<ProvinceMultiSelect> provinceList = new List<ProvinceMultiSelect>();

                    foreach (var item in model.ProvinceId)
                    {
                        ProvinceMultiSelect _data = new ProvinceMultiSelect();

                        _data.ProvinceId = item;
                        _data.ProjectId = model.ProjectId;
                        _data.IsDeleted = false;
                        _data.CreatedById = UserId;
                        _data.CreatedDate = DateTime.Now;

                        provinceList.Add(_data);
                    }

                    //Add
                    _uow.GetDbContext().ProvinceMultiSelect.AddRange(provinceList);
                    _uow.GetDbContext().SaveChanges();
                }



                //response.CommonId.Id = Convert.ToInt32(_detail.SecurityConsiderationId);
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

        public APIResponse GetDistrictMultiSelectByProjectId(long ProjectId)
        {
            APIResponse response = new APIResponse();
            try
            {

                List<long> SelectedProvinceList = _uow.GetDbContext().DistrictMultiSelect.Where(x => x.ProjectId == ProjectId && x.IsDeleted == false).Select(x => x.DistrictID).ToList();

                //details.ProjectSelectionId = selectedProjects != null ? selectedProjects : null;

                response.data.DistrictMultiSelectById = SelectedProvinceList;
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
        public APIResponse AddEditDistrictMultiSelectDetail(DistrictMultiSelectModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {


                if (model.DistrictID != null)
                {
                    // bool districtPresent = _uow.GetDbContext().DistrictMultiSelect.Any(x => x.ProjectId == model.ProjectId && x.IsDeleted == false);
                    var districtExist = _uow.GetDbContext().DistrictMultiSelect.Where(x => x.ProjectId == model.ProjectId && x.IsDeleted == false);

                    if (districtExist.Any())
                    {
                        _uow.GetDbContext().DistrictMultiSelect.RemoveRange(districtExist);
                        _uow.GetDbContext().SaveChanges();
                    }

                    List<DistrictMultiSelect> districtList = new List<DistrictMultiSelect>();

                    var selectedDistricts = _uow.GetDbContext().DistrictDetail.Where(x => model.DistrictID.Contains(x.DistrictID)).ToList();

                    foreach (var item in selectedDistricts)
                    {

                        DistrictMultiSelect _data = new DistrictMultiSelect();

                        _data.DistrictID = item.DistrictID;
                        _data.ProjectId = model.ProjectId;
                        _data.IsDeleted = false;
                        _data.CreatedById = UserId;
                        _data.ProvinceId = item.ProvinceID.Value;
                        _data.CreatedDate = DateTime.Now;

                        //_data.ProvinceId=model.ProvinceId

                        districtList.Add(_data);
                    }

                    //Add
                    _uow.GetDbContext().DistrictMultiSelect.AddRange(districtList);
                    _uow.GetDbContext().SaveChanges();
                }



                //response.CommonId.Id = Convert.ToInt32(_detail.SecurityConsiderationId);
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

        public APIResponse GetAllStrengthConsiderationDetails()
        {
            APIResponse response = new APIResponse();
            try
            {
                var StrengthConsiderationDetail = _uow.GetDbContext().StrengthConsiderationDetail.Where(x => x.IsDeleted == false).Select(x => new StrengthConsiderationDetail
                {
                    StrengthConsiderationId = x.StrengthConsiderationId,
                    StrengthConsiderationName = x.StrengthConsiderationName
                }).OrderBy(x => x.StrengthConsiderationName).ToList();


                //(from p in await _uow.StrengthConsiderationRepository.GetAllAsyn()
                //                                   where p.IsDeleted == false
                //                                   select new StrengthConsiderationDetail
                //                                   {
                //                                       StrengthConsiderationId = p.StrengthConsiderationId,
                //                                       StrengthConsiderationName = p.StrengthConsiderationName
                //                                   }).OrderBy(x => x.StrengthConsiderationName).ToList();
                response.data.StrengthConsiderationDetail = StrengthConsiderationDetail;
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
        public APIResponse GetAllGenderConsiderationDetails()
        {
            APIResponse response = new APIResponse();
            try
            {
                var GenderConsiderationDetail = _uow.GetDbContext().GenderConsiderationDetail.Where(x => x.IsDeleted == false).Select(x => new GenderConsiderationDetail
                {
                    GenderConsiderationId = x.GenderConsiderationId,
                    GenderConsiderationName = x.GenderConsiderationName
                }).OrderBy(x => x.GenderConsiderationName).ToList();


                //(from p in await _uow.GenderConsiderationRepository.GetAllAsyn()
                //                                 where p.IsDeleted == false
                //                                 select new GenderConsiderationDetail
                //                                 {
                //                                     GenderConsiderationId = p.GenderConsiderationId,
                //                                     GenderConsiderationName = p.GenderConsiderationName
                //                                 }).OrderBy(x => x.GenderConsiderationName).ToList();
                response.data.GenderConsiderationDetail = GenderConsiderationDetail;
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

        public APIResponse GetAllSecurityDetails()
        {
            APIResponse response = new APIResponse();
            try
            {
                var SecurityDetail = _uow.GetDbContext().SecurityDetail.Where(x => x.IsDeleted == false).Select(x => new SecurityDetail
                {
                    SecurityId = x.SecurityId,
                    SecurityName = x.SecurityName
                }).OrderBy(x => x.SecurityName).ToList();




                //(from p in await _uow.SecurityDetailRepository.GetAllAsyn()
                //                      where p.IsDeleted == false
                //                      select new SecurityDetail
                //                      {
                //                          SecurityId = p.SecurityId,
                //                          SecurityName = p.SecurityName
                //                      }).OrderBy(x => x.SecurityName).ToList();
                response.data.SecurityDetail = SecurityDetail;
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

        public APIResponse GetAllSecurityConsiderationDetails()
        {
            APIResponse response = new APIResponse();
            try
            {
                var SecurityConsiderationDetail =
                     _uow.GetDbContext().SecurityConsiderationDetail.Where(x => x.IsDeleted == false).Select(x => new SecurityConsiderationDetail
                     {
                         SecurityConsiderationId = x.SecurityConsiderationId,
                         SecurityConsiderationName = x.SecurityConsiderationName
                     }).OrderBy(x => x.SecurityConsiderationName).ToList();


                //(from p in await _uow.SecurityConsiderationDetailRepository.GetAllAsyn()
                //                                   where p.IsDeleted == false
                //                                   select new SecurityConsiderationDetail
                //                                   {
                //                                       SecurityConsiderationId = p.SecurityConsiderationId,
                //                                       SecurityConsiderationName = p.SecurityConsiderationName
                //                                   }).OrderBy(x => x.SecurityConsiderationName).ToList();
                response.data.SecurityConsiderationDetail = SecurityConsiderationDetail;
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
        public APIResponse GetAllDistrictvalueByProvinceId(int[] ProvinceId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var DistrictDetailList = _uow.GetDbContext().DistrictDetail.Where(x => !x.IsDeleted.Value).ToList();
                var Newlist = DistrictDetailList.Where(x => ProvinceId.Any(y => x.ProvinceID == y)).ToList();
                response.data.Districtlist = Newlist;
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

        public APIResponse AddEditProjectotherDetail(ProjectOtherDetail model, string UserId)
        {
            APIResponse response = new APIResponse();
            DbContext db = _uow.GetDbContext();
            long LatestProjectOtherDetailId = 0;

            using (IDbContextTransaction tran = db.Database.BeginTransaction())
            {
                try
                {
                    var Projectdetail = _uow.ProjectOtherDetailRepository.Find(x => x.IsDeleted == false && x.ProjectId == model.ProjectId);
                    if (Projectdetail == null)
                    {
                        ProjectOtherDetail obj = new ProjectOtherDetail();
                        obj.opportunityNo = model.opportunityNo;
                        obj.opportunity = model.opportunity;
                        obj.opportunitydescription = model.opportunitydescription;
                        obj.ProvinceId = model.ProvinceId;
                        obj.ProjectId = model.ProjectId;
                        obj.DistrictID = model.DistrictID;
                        obj.OfficeId = model.OfficeId;
                        obj.StartDate = model.StartDate;
                        obj.EndDate = model.EndDate;
                        obj.CurrencyId = model.CurrencyId;
                        obj.budget = model.budget;
                        obj.beneficiaryMale = model.beneficiaryMale;
                        obj.beneficiaryFemale = model.beneficiaryFemale;
                        obj.projectGoal = model.projectGoal;
                        obj.projectObjective = model.projectObjective;
                        obj.mainActivities = model.mainActivities;
                        obj.DonorId = model.DonorId;
                        obj.SubmissionDate = model.SubmissionDate;
                        obj.REOIReceiveDate = model.REOIReceiveDate;
                        obj.StrengthConsiderationId = model.StrengthConsiderationId;
                        obj.GenderConsiderationId = model.GenderConsiderationId;
                        obj.GenderRemarks = model.GenderRemarks;
                        obj.SecurityId = model.SecurityId;
                        obj.SecurityConsiderationId = model.SecurityConsiderationId;
                        obj.SecurityRemarks = model.SecurityRemarks;
                        obj.IsDeleted = false;
                        obj.CreatedById = UserId;
                        obj.CreatedDate = DateTime.Now;
                        _uow.ProjectOtherDetailRepository.Add(obj);
                        LatestProjectOtherDetailId = obj.ProjectOtherDetailId;
                    }
                    else
                    {
                        var existProjectRecord = _uow.ProjectOtherDetailRepository.Find(x => x.IsDeleted == false && x.ProjectOtherDetailId == model.ProjectOtherDetailId && x.ProjectId == model.ProjectId);
                        if (existProjectRecord != null)
                        {
                            existProjectRecord.opportunityNo = model.opportunityNo;
                            existProjectRecord.ProjectId = model.ProjectId;
                            existProjectRecord.opportunity = model.opportunity;
                            existProjectRecord.opportunitydescription = model.opportunitydescription;
                            existProjectRecord.ProvinceId = model.ProvinceId;
                            existProjectRecord.DistrictID = model.DistrictID;
                            existProjectRecord.OfficeId = model.OfficeId;
                            existProjectRecord.StartDate = model.StartDate;
                            existProjectRecord.EndDate = model.EndDate;
                            existProjectRecord.CurrencyId = model.CurrencyId;
                            existProjectRecord.budget = model.budget;
                            existProjectRecord.beneficiaryMale = model.beneficiaryMale;
                            existProjectRecord.beneficiaryFemale = model.beneficiaryFemale;
                            existProjectRecord.projectGoal = model.projectGoal;
                            existProjectRecord.projectObjective = model.projectObjective;
                            existProjectRecord.mainActivities = model.mainActivities;
                            existProjectRecord.DonorId = model.DonorId;
                            existProjectRecord.SubmissionDate = model.SubmissionDate;
                            existProjectRecord.REOIReceiveDate = model.REOIReceiveDate;
                            existProjectRecord.StrengthConsiderationId = model.StrengthConsiderationId;
                            existProjectRecord.GenderConsiderationId = model.GenderConsiderationId;
                            existProjectRecord.GenderRemarks = model.GenderRemarks;
                            existProjectRecord.SecurityId = model.SecurityId;
                            existProjectRecord.SecurityConsiderationId = model.SecurityConsiderationId;
                            existProjectRecord.SecurityRemarks = model.SecurityRemarks;
                            existProjectRecord.IsDeleted = false;
                            existProjectRecord.ModifiedById = UserId;
                            existProjectRecord.ModifiedDate = DateTime.Now;
                            _uow.GetDbContext().SaveChanges();
                            LatestProjectOtherDetailId = model.ProjectOtherDetailId;
                        }
                    }
                    response.StatusCode = StaticResource.successStatusCode;
                    response.CommonId.Id = Convert.ToInt32(LatestProjectOtherDetailId);

                    response.Message = "Success";
                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.SomethingWrong + ex.Message;
                }
            }

            return response;
        }


        public APIResponse GetOtherProjectListById(long ProjectId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var OtherProjectDetail = _uow.GetDbContext().ProjectOtherDetail
                                         .FirstOrDefault(x => !x.IsDeleted.Value && x.ProjectId == ProjectId);

                response.data.OtherProjectDetailById = OtherProjectDetail;
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
        //secutiryconsideration

        public APIResponse GetSecurityConsiMultiSelectByProjectId(long ProjectId)
        {
            APIResponse response = new APIResponse();
            try
            {

                List<long> SelectedSecurityList = _uow.GetDbContext().SecurityConsiderationMultiSelect.Where(x => x.ProjectId == ProjectId && x.IsDeleted == false).Select(x => x.SecurityConsiderationId).ToList();

                //details.ProjectSelectionId = selectedProjects != null ? selectedProjects : null;

                response.data.SecurityConsiderationMultiSelectById = SelectedSecurityList;
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

        public APIResponse AddEditSecurityConsidMultiDetail(SecurityConsiderationMultiSelectModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {


                if (model.SecurityConsiderationId != null)
                {

                    bool securityPresent = _uow.GetDbContext().SecurityConsiderationMultiSelect.Any(x => x.ProjectId == model.ProjectId && x.IsDeleted == false);

                    if (securityPresent)
                    {
                        var securityExist = _uow.GetDbContext().SecurityConsiderationMultiSelect.Where(x => x.ProjectId == model.ProjectId && x.IsDeleted == false);


                        _uow.GetDbContext().SecurityConsiderationMultiSelect.RemoveRange(securityExist);
                        _uow.GetDbContext().SaveChanges();
                    }

                    List<SecurityConsiderationMultiSelect> securityList = new List<SecurityConsiderationMultiSelect>();

                    foreach (var item in model.SecurityConsiderationId)
                    {
                        SecurityConsiderationMultiSelect _data = new SecurityConsiderationMultiSelect();

                        _data.SecurityConsiderationId = item.Value;
                        _data.ProjectId = model.ProjectId;
                        _data.IsDeleted = false;
                        _data.CreatedById = UserId;
                        _data.CreatedDate = DateTime.Now;

                        securityList.Add(_data);
                    }

                    //Add
                    _uow.GetDbContext().SecurityConsiderationMultiSelect.AddRange(securityList);
                    _uow.GetDbContext().SaveChanges();
                }



                //response.CommonId.Id = Convert.ToInt32(_detail.SecurityConsiderationId);
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

        #region Project ApprovalDetail
        public async Task<APIResponse> AddApprovalDetail(ApproveProjectDetailModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                ApproveProjectDetails obj = new ApproveProjectDetails();

                obj = _uow.GetDbContext().ApproveProjectDetails.Where(x => x.ProjectId == model.ProjectId && x.IsDeleted == false).FirstOrDefault();
                if (obj == null)
                {
                    obj = new ApproveProjectDetails();
                    obj.ProjectId = model.ProjectId;
                    obj.CommentText = model.CommentText;
                    obj.FileName = model.FileName;
                    obj.FilePath = model.FilePath;
                    obj.IsApproved = model.IsApproved;
                    obj.UploadedFile = string.IsNullOrEmpty(model.UploadedFile) ? null : Convert.FromBase64String(model.UploadedFile);
                    obj.IsDeleted = false;
                    obj.CreatedById = UserId;
                    obj.CreatedDate = DateTime.Now;
                    await _uow.ApproveProjectDetailsRepository.AddAsyn(obj);
                    await _uow.SaveAsync();
                    if (model.IsApproved == false)
                    {
                        var details = _uow.GetDbContext().ProjectProposalDetail.Where(x => x.ProjectId == model.ProjectId && x.IsDeleted == false).FirstOrDefault();
                        if (details != null)
                        {
                            details.IsProposalAccept = model.IsApproved;
                            details.ModifiedById = UserId;
                            details.IsDeleted = false;
                            details.ModifiedDate = DateTime.Now;
                            _uow.GetDbContext().SaveChanges();
                        }
                    }


                }
                else
                {
                    obj.ProjectId = model.ProjectId;
                    obj.CommentText = model.CommentText;
                    obj.FileName = model.FileName;
                    obj.FilePath = model.FilePath;
                    obj.IsApproved = model.IsApproved;
                    obj.UploadedFile = string.IsNullOrEmpty(model.UploadedFile) ? null : Convert.FromBase64String(model.UploadedFile);
                    obj.IsDeleted = false;
                    obj.CreatedById = UserId;
                    obj.CreatedDate = DateTime.Now;
                    await _uow.SaveAsync();
                    if (model.IsApproved == false)
                    {
                        var details = _uow.GetDbContext().ProjectProposalDetail.Where(x => x.ProjectId == model.ProjectId && x.IsDeleted == false).FirstOrDefault();
                        //var Approveprojectdetail = _uow.GetDbContext().ApproveProjectDetails.Where(x => x.ProjectId == model.ProjectId && x.IsDeleted == false).FirstOrDefault();
                        if (details != null)
                        {
                            details.IsProposalAccept = model.IsApproved;
                            details.ModifiedById = UserId;
                            details.IsDeleted = false;
                            details.ModifiedDate = DateTime.Now;
                            _uow.GetDbContext().SaveChanges();
                        }
                        //if (Approveprojectdetail != null)
                        //{
                        //    Approveprojectdetail.IsApproved = null;
                        //    Approveprojectdetail.ModifiedById = UserId;
                        //    Approveprojectdetail.IsDeleted = false;
                        //    Approveprojectdetail.ModifiedDate = DateTime.Now;
                        //    _uow.GetDbContext().SaveChanges();
                        //}
                    }
                }
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
                response.CommonId.IsApproved = model.IsApproved;
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }
        #endregion

        #region proposals Details

        public async Task<APIResponse> WinApprovalDetail(WinApprovalProjectModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                WinProjectDetails obj = new WinProjectDetails();
                obj.ProjectId = model.ProjectId;
                obj.CommentText = model.CommentText;
                obj.FileName = model.FileName;
                obj.FilePath = model.FilePath;
                obj.IsWin = model.IsWin;
                obj.UploadedFile = string.IsNullOrEmpty(model.UploadedFile) ? null : Convert.FromBase64String(model.UploadedFile);
                obj.IsDeleted = false;
                obj.CreatedById = UserId;
                obj.CreatedDate = DateTime.Now;
                await _uow.WinProjectDetailsRepository.AddAsyn(obj);
                await _uow.SaveAsync();
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
                response.CommonId.IsApproved = model.IsWin;

            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }
        public APIResponse AddEditProjectproposals(long Projectid, string userid, string logginUserEmailId)
        {
            ProjectProposalDetail model = new ProjectProposalDetail();
            APIResponse response = new APIResponse();
            //try
            //{
                var pathFile = Path.Combine(Directory.GetCurrentDirectory(), "GoogleCredentials/" + "credentials.json");
                var GoogleCredentialsFile = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
                GoogleCredential result = new GoogleCredential();

                using (StreamReader file = File.OpenText(GoogleCredentialsFile))
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    JObject o2 = (JObject)JToken.ReadFrom(reader);

                    result = o2["GoogleCredential"].ToObject<GoogleCredential>();
                }
                var EmailID = string.Empty;
                string FolderName;
                FolderName = _uow.GetDbContext().ProjectDetail.Where(x => x.ProjectId == Projectid && !x.IsDeleted.Value).Select(x => x.ProjectCode).FirstOrDefault();
                string ProjectProposalfilename;
                ProjectProposalfilename = _uow.GetDbContext().ProjectDetail.Where(x => x.ProjectId == Projectid && !x.IsDeleted.Value).Select(x => x.ProjectName + "-" + x.ProjectCode + "-" + "Proposal").FirstOrDefault();
                var proposaldata = _uow.GetDbContext().ProjectProposalDetail.Where(x => x.ProjectId == Projectid && x.IsDeleted == false).FirstOrDefault();
                if (proposaldata != null)
                {
                    if (proposaldata.UserId != null)
                    {
                    EmailID = _uow.GetDbContext().UserDetails.Where(z => z.UserID == proposaldata.UserId).Select(p => p.Username).FirstOrDefault();
                    if (proposaldata != null && EmailID != null)
                    {
                        response.data.ProjectProposalModel = ProposalDoc.userCredential(ProjectProposalfilename, pathFile, result, EmailID, FolderName, logginUserEmailId);
                    }
                  }
                }
                else
                {
                    response.data.ProjectProposalModel = ProposalDoc.userCredential(ProjectProposalfilename, pathFile, result, null, FolderName, logginUserEmailId);
                }
                if (proposaldata == null)
                {
                    model.FolderName = response.data.ProjectProposalModel.FolderName;
                    model.FolderId = response.data.ProjectProposalModel.FolderId;
                    model.ProposalFileName = response.data.ProjectProposalModel.ProposalFileName;
                    model.ProposalFileId = response.data.ProjectProposalModel.ProposalFileId;
                    model.ProposalWebLink = response.data.ProjectProposalModel.ProposalWebLink;
                    model.ProjectId = Projectid;
                    model.IsDeleted = false;
                    model.CreatedById = userid;
                    model.CreatedDate = DateTime.Now;
                    _uow.ProjectProposalDetailRepository.Add(model);
                    _uow.Save();
                }
                else
                {
                    proposaldata.FolderName = response.data.ProjectProposalModel.FolderName;
                    proposaldata.FolderId = response.data.ProjectProposalModel.FolderId;
                    proposaldata.ProposalFileName = response.data.ProjectProposalModel.ProposalFileName;
                    proposaldata.ProposalFileId = response.data.ProjectProposalModel.ProposalFileId;
                    proposaldata.ProposalWebLink = response.data.ProjectProposalModel.ProposalWebLink;
                    proposaldata.ProjectId = Projectid;
                    proposaldata.CreatedDate = DateTime.Now;
                    proposaldata.IsDeleted = false;
                    proposaldata.CreatedById = userid;
                    _uow.GetDbContext().SaveChanges();
                }
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            //}
            //catch (Exception ex)
            //{
            //    response.StatusCode = StaticResource.failStatusCode;
            //    response.Message = StaticResource.SomethingWrong + ex.Message;
            //}
            return response;
        }
        public APIResponse GetProjectproposalsById(long Projectid)
        {
            APIResponse response = new APIResponse();
            ProjectProposalModel obj = new ProjectProposalModel();
            try
            {
                var detail = _uow.GetDbContext().ProjectProposalDetail.FirstOrDefault(x => x.ProjectId == Projectid && x.IsDeleted == false);
                var Projectdetail = _uow.GetDbContext().ApproveProjectDetails.FirstOrDefault(x => x.ProjectId == Projectid && x.IsDeleted == false);
                if (detail != null)
                {

                    obj.ProjectProposaldetailId = detail.ProjectProposaldetailId;
                    obj.FolderName = detail.FolderName;
                    obj.FolderId = detail.FolderId;
                    obj.ProposalFileName = detail.ProposalFileName;
                    obj.ProjectId = detail.ProjectId;
                    obj.ProposalFileId = detail.ProposalFileId;
                    obj.EDIFileName = detail.EDIFileName;
                    obj.EdiFileId = detail.EdiFileId;
                    obj.BudgetFileName = detail.BudgetFileName;
                    obj.BudgetFileId = detail.BudgetFileId;
                    obj.ConceptFileName = detail.ConceptFileName;
                    obj.ConceptFileId = detail.ConceptFileId;
                    obj.PresentationFileName = detail.PresentationFileName;
                    obj.ProposalWebLink = detail.ProposalWebLink;
                    obj.EDIFileWebLink = detail.EDIFileWebLink;
                    obj.BudgetFileWebLink = detail.BudgetFileWebLink;
                    obj.ConceptFileWebLink = detail.ConceptFileWebLink;
                    obj.PresentationFileWebLink = detail.PresentationFileWebLink;
                    obj.ProposalExtType = detail.ProposalExtType;
                    obj.EDIFileExtType = detail.EDIFileExtType;
                    obj.BudgetFileExtType = detail.BudgetFileExtType;
                    obj.ConceptFileExtType = detail.ConceptFileExtType;
                    obj.PresentationExtType = detail.PresentationExtType;
                    obj.ProposalStartDate = detail.CreatedDate;
                    obj.ProposalBudget = detail.ProposalBudget;
                    obj.ProposalDueDate = detail.ProposalDueDate;
                    obj.ProjectAssignTo = detail.ProjectAssignTo;
                    obj.IsProposalAccept = detail.IsProposalAccept;
                    obj.CurrencyId = detail.CurrencyId;
                    obj.UserId = detail.UserId;
                    obj.IsApproved = Projectdetail?.IsApproved;
                    response.data.ProjectProposalModel = obj;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.NoDataFound;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }
        public APIResponse UploadOtherProposalFile(IFormFile file, string UserId, string Projectid, string fullPath, string fileNames, string logginUserEmailId)
        {
            APIResponse response = new APIResponse();
            try
            {
                //string fileName = string.Empty;
                long ProjectId = long.Parse(Projectid);
                ProjectProposalDetail model = new ProjectProposalDetail();
                string _detail = _uow.GetDbContext().ProjectProposalDetail.Where(x => x.ProjectId == ProjectId && !x.IsDeleted.Value).Select(x => x.FolderName).FirstOrDefault();
                var pathFile = Path.Combine(Directory.GetCurrentDirectory(), "GoogleCredentials/" + "credentials.json");
                //
                var GoogleCredentialsFile = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
                GoogleCredential result = new GoogleCredential();

                using (StreamReader files = File.OpenText(GoogleCredentialsFile))
                using (JsonTextReader reader = new JsonTextReader(files))
                {
                    JObject o2 = (JObject)JToken.ReadFrom(reader);

                    result = o2["GoogleCredential"].ToObject<GoogleCredential>();
                }
                var EmailID = string.Empty;
                var proposaldata = _uow.GetDbContext().ProjectProposalDetail.Where(x => x.ProjectId == ProjectId && x.IsDeleted == false).FirstOrDefault();
                if (proposaldata != null)
                {
                    if (proposaldata.UserId != null)
                    {
                        EmailID = _uow.GetDbContext().UserDetails.Where(z => z.UserID == proposaldata.UserId).Select(p => p.Username).FirstOrDefault();
                        if (proposaldata != null && EmailID != null)
                        {
                            response.data.ProjectProposalModel = ProposalDoc.uploadOtherProposaldoc(_detail, file, fileNames, pathFile, fullPath, result, EmailID, logginUserEmailId);
                        }
                    }
                    else
                    {
                        response.data.ProjectProposalModel = ProposalDoc.uploadOtherProposaldoc(_detail, file, fileNames, pathFile, fullPath, result, null, logginUserEmailId);
                    }
                }
                else
                {
                    response.data.ProjectProposalModel = ProposalDoc.uploadOtherProposaldoc(_detail, file, fileNames, pathFile, fullPath, result, null, logginUserEmailId);
                }
                var proposaldetails = _uow.GetDbContext().ProjectProposalDetail.Where(x => x.ProjectId == ProjectId && x.IsDeleted == false).FirstOrDefault();
                if (proposaldetails == null)
                {
                    proposaldetails.EdiFileId = response.data.ProjectProposalModel.EdiFileId;
                    proposaldetails.EDIFileName = response.data.ProjectProposalModel.EDIFileName;
                    proposaldetails.EDIFileWebLink = response.data.ProjectProposalModel.EDIFileWebLink;
                    proposaldetails.BudgetFileId = response.data.ProjectProposalModel.BudgetFileId;
                    proposaldetails.BudgetFileName = response.data.ProjectProposalModel.BudgetFileName;
                    proposaldetails.BudgetFileWebLink = response.data.ProjectProposalModel.BudgetFileWebLink;
                    proposaldetails.ConceptFileId = response.data.ProjectProposalModel.ConceptFileId;
                    proposaldetails.ConceptFileName = response.data.ProjectProposalModel.ConceptFileName;
                    proposaldetails.ConceptFileWebLink = response.data.ProjectProposalModel.ConceptFileWebLink;
                    proposaldetails.PresentationFileId = response.data.ProjectProposalModel.PresentationFileId;
                    proposaldetails.PresentationFileName = response.data.ProjectProposalModel.PresentationFileName;
                    proposaldetails.PresentationFileWebLink = response.data.ProjectProposalModel.PresentationFileWebLink;
                    proposaldetails.ProjectId = ProjectId;
                    proposaldetails.IsDeleted = false;
                    proposaldetails.CreatedById = UserId;
                    // proposaldetails.CreatedDate = DateTime.Now;
                    _uow.ProjectProposalDetailRepository.Add(model);
                }
                else
                {
                    if (response.data.ProjectProposalModel.FileType == "EOI")
                    {
                        proposaldetails.EdiFileId = response.data.ProjectProposalModel.EdiFileId;
                        proposaldetails.EDIFileName = response.data.ProjectProposalModel.EDIFileName;
                        proposaldetails.EDIFileWebLink = response.data.ProjectProposalModel.EDIFileWebLink;
                        proposaldetails.EDIFileExtType = response.data.ProjectProposalModel.EDIFileExtType;
                    }
                    else if (response.data.ProjectProposalModel.FileType == "BUDGET")
                    {
                        proposaldetails.BudgetFileId = response.data.ProjectProposalModel.BudgetFileId;
                        proposaldetails.BudgetFileName = response.data.ProjectProposalModel.BudgetFileName;
                        proposaldetails.BudgetFileWebLink = response.data.ProjectProposalModel.BudgetFileWebLink;
                        proposaldetails.BudgetFileExtType = response.data.ProjectProposalModel.BudgetFileExtType;
                    }
                    else if (response.data.ProjectProposalModel.FileType == "CONCEPT")
                    {
                        proposaldetails.ConceptFileId = response.data.ProjectProposalModel.ConceptFileId;
                        proposaldetails.ConceptFileName = response.data.ProjectProposalModel.ConceptFileName;
                        proposaldetails.ConceptFileWebLink = response.data.ProjectProposalModel.ConceptFileWebLink;
                        proposaldetails.ConceptFileExtType = response.data.ProjectProposalModel.ConceptFileExtType;
                    }
                    else if (response.data.ProjectProposalModel.FileType == "PRESENTATION")
                    {
                        proposaldetails.PresentationFileId = response.data.ProjectProposalModel.PresentationFileId;
                        proposaldetails.PresentationFileName = response.data.ProjectProposalModel.PresentationFileName;
                        proposaldetails.PresentationFileWebLink = response.data.ProjectProposalModel.PresentationFileWebLink;
                        proposaldetails.PresentationExtType = response.data.ProjectProposalModel.PresentationExtType;
                    }
                    proposaldetails.ProjectId = ProjectId;
                    proposaldetails.IsDeleted = false;
                    proposaldetails.ModifiedById = UserId;
                    proposaldetails.ModifiedDate = DateTime.Now;
                    _uow.GetDbContext().SaveChanges();
                }
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
                //return Json("Upload Successful.");
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }

        public APIResponse AddEditProjectProposalDetail(ProposalDocModel model, string UserId, string logginUserEmailId)
        {
            APIResponse response = new APIResponse();
            ProjectProposalDetail details = new ProjectProposalDetail();
            try
            {
                var pathFile = Path.Combine(Directory.GetCurrentDirectory(), "GoogleCredentials/" + "credentials.json");
                details = _uow.GetDbContext().ProjectProposalDetail.Where(x => x.ProjectId == model.ProjectId && x.IsDeleted == false).FirstOrDefault();
                var GoogleCredentialsFile = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
                GoogleCredential Credential = new GoogleCredential();

                using (StreamReader files = File.OpenText(GoogleCredentialsFile))
                using (JsonTextReader reader = new JsonTextReader(files))
                {
                    JObject o2 = (JObject)JToken.ReadFrom(reader);

                    Credential = o2["GoogleCredential"].ToObject<GoogleCredential>();
                }

                if (details == null)
                {
                    details = new ProjectProposalDetail();
                    details.ProposalStartDate = model.ProposalStartDate;
                    details.ProposalBudget = model.ProposalBudget;
                    details.ProposalDueDate = model.ProposalDueDate;
                    details.ProjectAssignTo = model.UserId;
                    details.IsProposalAccept = model.IsProposalAccept;
                    details.ProjectId = model.ProjectId.Value;
                    details.CurrencyId = model.CurrencyId;
                    details.UserId = model.UserId;
                    details.IsDeleted = false;
                    details.CreatedById = UserId;
                    //details.CreatedDate = DateTime.Now;
                    _uow.ProjectProposalDetailRepository.Add(details);

                    if (model.ProjectAssignTo != null)
                    {
                        var proposaldetails = _uow.GetDbContext().ProjectProposalDetail.Where(x => x.ProjectId == model.ProjectId && x.IsDeleted == false).FirstOrDefault();
                        var EmailID = _uow.GetDbContext().UserDetails.Where(z => z.UserID == model.UserId).Select(p => p.Username).FirstOrDefault();
                        if (proposaldetails != null && EmailID != null)
                        {
                            if (proposaldetails.FolderId != null)
                            {
                                ProposalDoc.FilePermission(proposaldetails.FolderName, proposaldetails.FolderId, EmailID, pathFile, Credential, logginUserEmailId);
                            }

                            if (proposaldetails.EdiFileId != null)
                            {
                                ProposalDoc.FilePermission(proposaldetails.FolderName, proposaldetails.EdiFileId, EmailID, pathFile, Credential, logginUserEmailId);
                            }
                            if (proposaldetails.BudgetFileId != null)
                            {
                                ProposalDoc.FilePermission(proposaldetails.FolderName, proposaldetails.BudgetFileId, EmailID, pathFile, Credential, logginUserEmailId);
                            }
                            if (proposaldetails.ConceptFileId != null)
                            {
                                ProposalDoc.FilePermission(proposaldetails.FolderName, proposaldetails.ConceptFileId, EmailID, pathFile, Credential, logginUserEmailId);
                            }
                            if (proposaldetails.PresentationFileId != null)
                            {
                                ProposalDoc.FilePermission(proposaldetails.FolderName, proposaldetails.PresentationFileId, EmailID, pathFile, Credential, logginUserEmailId);
                            }
                        }
                    }
                }
                else
                {
                    details.ProposalStartDate = model.ProposalStartDate;
                    details.ProposalBudget = model.ProposalBudget;
                    details.ProposalDueDate = model.ProposalDueDate;
                    details.ProjectAssignTo = model.UserId;
                    details.IsProposalAccept = model.IsProposalAccept;
                    details.ProjectId = model.ProjectId.Value;
                    details.CurrencyId = model.CurrencyId;
                    details.UserId = model.UserId;
                    details.ModifiedById = UserId;
                    details.ModifiedDate = DateTime.Now;
                    _uow.GetDbContext().SaveChanges();
                    if (details.ProjectAssignTo != null)
                    {
                        var proposaldetails = _uow.GetDbContext().ProjectProposalDetail.Where(x => x.ProjectId == model.ProjectId && x.IsDeleted == false).FirstOrDefault();
                        var EmailID = _uow.GetDbContext().UserDetails.Where(z => z.UserID == details.UserId).Select(p => p.Username).FirstOrDefault();
                        if (proposaldetails != null && EmailID != null)
                        {
                            if (proposaldetails.FolderId != null)
                            {
                                ProposalDoc.FilePermission(proposaldetails.FolderName, proposaldetails.FolderId, EmailID, pathFile, Credential, logginUserEmailId);
                            }
                            if (proposaldetails.ProposalFileId != null)
                            {
                                ProposalDoc.FilePermission(proposaldetails.FolderName, proposaldetails.ProposalFileId, EmailID, pathFile, Credential, logginUserEmailId);
                            }
                            if (proposaldetails.EdiFileId != null)
                            {
                                ProposalDoc.FilePermission(proposaldetails.FolderName, proposaldetails.EdiFileId, EmailID, pathFile, Credential, logginUserEmailId);
                            }
                            if (proposaldetails.BudgetFileId != null)
                            {
                                ProposalDoc.FilePermission(proposaldetails.FolderName, proposaldetails.BudgetFileId, EmailID, pathFile, Credential, logginUserEmailId);
                            }
                            if (proposaldetails.ConceptFileId != null)
                            {
                                ProposalDoc.FilePermission(proposaldetails.FolderName, proposaldetails.ConceptFileId, EmailID, pathFile, Credential, logginUserEmailId);
                            }
                            if (proposaldetails.PresentationFileId != null)
                            {
                                ProposalDoc.FilePermission(proposaldetails.FolderName, proposaldetails.PresentationFileId, EmailID, pathFile, Credential, logginUserEmailId);
                            }
                        }
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
        #endregion

        #region Criteria
        public APIResponse AddEditDonorCriteria(DonorCriteriaModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            DonorCriteriaDetails _detail = new DonorCriteriaDetails();
            try
            {
                _detail = _uow.GetDbContext().DonorCriteriaDetail.Where(x => x.ProjectId == model.ProjectId && x.IsDeleted == false).FirstOrDefault();
                if (_detail == null)
                {
                    _detail = new DonorCriteriaDetails();
                    _detail.MethodOfFunding = model.MethodOfFunding;
                    _detail.PastFundingExperience = model.PastFundingExperience;
                    _detail.ProposalAccepted = model.ProposalAccepted;
                    _detail.ProposalExperience = model.ProposalExperience;
                    _detail.Professional = model.Professional;
                    _detail.FundsOnTime = model.FundsOnTime;
                    _detail.EffectiveCommunication = model.EffectiveCommunication;
                    _detail.Dispute = model.Dispute;
                    _detail.OtherDeliverable = model.OtherDeliverable;
                    _detail.OtherDeliverableType = model.OtherDeliverableType;
                    _detail.PastWorkingExperience = model.PastWorkingExperience;
                    _detail.CriticismPerformance = model.CriticismPerformance;
                    _detail.TimeManagement = model.TimeManagement;
                    _detail.MoneyAllocation = model.MoneyAllocation;
                    _detail.Accountability = model.Accountability;
                    _detail.DeliverableQuality = model.DeliverableQuality;
                    _detail.DonorFinancingHistory = model.DonorFinancingHistory;
                    _detail.ReligiousStanding = model.ReligiousStanding;
                    _detail.PoliticalStanding = model.PoliticalStanding;
                    _detail.ProjectId = model.ProjectId.Value;
                    _detail.IsDeleted = false;
                    _detail.CreatedById = UserId;
                    _detail.CreatedDate = DateTime.Now;
                    _uow.DonorCriteriaDetailsRepository.Add(_detail);
                }
                else
                {
                    _detail.MethodOfFunding = model.MethodOfFunding;
                    _detail.PastFundingExperience = model.PastFundingExperience;
                    _detail.ProposalAccepted = model.ProposalAccepted;
                    _detail.ProposalExperience = model.ProposalExperience;
                    _detail.Professional = model.Professional;
                    _detail.FundsOnTime = model.FundsOnTime;
                    _detail.EffectiveCommunication = model.EffectiveCommunication;
                    _detail.Dispute = model.Dispute;
                    _detail.OtherDeliverable = model.OtherDeliverable;
                    _detail.OtherDeliverableType = model.OtherDeliverableType;
                    _detail.PastWorkingExperience = model.PastWorkingExperience;
                    _detail.CriticismPerformance = model.CriticismPerformance;
                    _detail.TimeManagement = model.TimeManagement;
                    _detail.MoneyAllocation = model.MoneyAllocation;
                    _detail.Accountability = model.Accountability;
                    _detail.DeliverableQuality = model.DeliverableQuality;
                    _detail.DonorFinancingHistory = model.DonorFinancingHistory;
                    _detail.ReligiousStanding = model.ReligiousStanding;
                    _detail.PoliticalStanding = model.PoliticalStanding;
                    _detail.IsDeleted = false;
                    _detail.ModifiedById = UserId;
                    _detail.ModifiedDate = DateTime.Now;
                    _uow.GetDbContext().SaveChanges();
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

        public APIResponse AddEditPurposeofInitiativeCriteria(PurposeofInitiativeCriteriaModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            PurposeofInitiativeCriteria _detail = new PurposeofInitiativeCriteria();
            try
            {
                _detail = _uow.GetDbContext().PurposeofInitiativeCriteria.Where(x => x.ProjectId == model.ProjectId && x.IsDeleted == false).FirstOrDefault();
                if (_detail == null)
                {
                    _detail = new PurposeofInitiativeCriteria();
                    _detail.Women = model.Women;
                    _detail.Children = model.Children;
                    _detail.Awareness = model.Awareness;
                    _detail.Education = model.Education;
                    _detail.DrugAbuses = model.DrugAbuses;
                    _detail.Right = model.Right;
                    _detail.Culture = model.Culture;
                    _detail.Music = model.Music;
                    _detail.Documentaries = model.Documentaries;
                    _detail.InvestigativeJournalism = model.InvestigativeJournalism;
                    _detail.HealthAndNutrition = model.HealthAndNutrition;
                    _detail.News = model.News;
                    _detail.SocioPolitiacalDebate = model.SocioPolitiacalDebate;
                    _detail.Studies = model.Studies;
                    _detail.Reports = model.Reports;
                    _detail.CommunityDevelopment = model.CommunityDevelopment;
                    _detail.Aggriculture = model.Aggriculture;
                    _detail.DRR = model.DRR;
                    _detail.ServiceEducation = model.ServiceEducation;
                    _detail.ServiceHealthAndNutrition = model.ServiceHealthAndNutrition;
                    _detail.RadioProduction = model.RadioProduction;
                    _detail.TVProgram = model.TVProgram;
                    _detail.PrintedMedia = model.PrintedMedia;
                    _detail.RoundTable = model.RoundTable;
                    _detail.Others = model.Others;
                    _detail.OtherActivity = model.OtherActivity;
                    _detail.TargetBenificaiaryWomen = model.TargetBenificaiaryWomen;
                    _detail.TargetBenificiaryMen = model.TargetBenificiaryMen;
                    _detail.TargetBenificiaryAgeGroup = model.TargetBenificiaryAgeGroup;
                    _detail.TargetBenificiaryaOccupation = model.TargetBenificiaryaOccupation;
                    _detail.ProjectId = model.ProjectId;
                    _detail.IsDeleted = false;
                    _detail.CreatedById = UserId;
                    _detail.CreatedDate = DateTime.Now;
                    _detail.Product = model.Product;
                    _detail.Service = model.Service;
                    _uow.PurposeofInitiativeCriteriaRepository.Add(_detail);
                }
                else
                {
                    _detail.Women = model.Women;
                    _detail.Children = model.Children;
                    _detail.Awareness = model.Awareness;
                    _detail.Education = model.Education;
                    _detail.DrugAbuses = model.DrugAbuses;
                    _detail.Right = model.Right;
                    _detail.Culture = model.Culture;
                    _detail.Music = model.Music;
                    _detail.Documentaries = model.Documentaries;
                    _detail.InvestigativeJournalism = model.InvestigativeJournalism;
                    _detail.HealthAndNutrition = model.HealthAndNutrition;
                    _detail.News = model.News;
                    _detail.SocioPolitiacalDebate = model.SocioPolitiacalDebate;
                    _detail.Studies = model.Studies;
                    _detail.Reports = model.Reports;
                    _detail.CommunityDevelopment = model.CommunityDevelopment;
                    _detail.Aggriculture = model.Aggriculture;
                    _detail.DRR = model.DRR;
                    _detail.ServiceEducation = model.ServiceEducation;
                    _detail.ServiceHealthAndNutrition = model.ServiceHealthAndNutrition;
                    _detail.RadioProduction = model.RadioProduction;
                    _detail.TVProgram = model.TVProgram;
                    _detail.PrintedMedia = model.PrintedMedia;
                    _detail.RoundTable = model.RoundTable;
                    _detail.Others = model.Others;
                    _detail.OtherActivity = model.OtherActivity;
                    _detail.TargetBenificaiaryWomen = model.TargetBenificaiaryWomen;
                    _detail.TargetBenificiaryMen = model.TargetBenificiaryMen;
                    _detail.TargetBenificiaryAgeGroup = model.TargetBenificiaryAgeGroup;
                    _detail.TargetBenificiaryaOccupation = model.TargetBenificiaryaOccupation;
                    _detail.IsDeleted = false;
                    _detail.ModifiedById = UserId;
                    _detail.ModifiedDate = DateTime.Now;
                    _detail.Product = model.Product;
                    _detail.Service = model.Service;
                    _uow.GetDbContext().SaveChanges();
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
        public APIResponse AddEditEligibilityCriteriaDetail(EligibilityCriteriaDetailModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            EligibilityCriteriaDetail _detail = new EligibilityCriteriaDetail();
            try
            {
                _detail = _uow.GetDbContext().EligibilityCriteriaDetail.Where(x => x.ProjectId == model.ProjectId && x.IsDeleted == false).FirstOrDefault();
                if (_detail == null)
                {
                    _detail = new EligibilityCriteriaDetail();
                    _detail.DonorCriteriaMet = model.DonorCriteriaMet;
                    _detail.EligibilityDealine = model.EligibilityDealine;
                    _detail.CoPartnership = model.CoPartnership;
                    _detail.ProjectId = model.ProjectId;
                    _detail.IsDeleted = false;
                    _detail.CreatedById = UserId;
                    _detail.CreatedDate = DateTime.Now;
                    _uow.EligibilityCriteriaDetailRepository.Add(_detail);
                }
                else
                {
                    _detail.DonorCriteriaMet = model.DonorCriteriaMet;
                    _detail.EligibilityDealine = model.EligibilityDealine;
                    _detail.CoPartnership = model.CoPartnership;
                    _detail.ProjectId = model.ProjectId;
                    _detail.IsDeleted = false;
                    _detail.ModifiedById = UserId;
                    _detail.ModifiedDate = DateTime.Now;
                    _uow.GetDbContext().SaveChanges();
                }
                response.data.eligibilityCriteriaDetail = _detail;
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

        public APIResponse AddEditFeasibilityCriteria(FeasibilityCriteriaModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            FeasibilityCriteriaDetail _detail = new FeasibilityCriteriaDetail();
            try
            {
                _detail = _uow.GetDbContext().FeasibilityCriteriaDetail.Where(x => x.ProjectId == model.ProjectId && x.IsDeleted == false).FirstOrDefault();
                if (_detail == null)
                {
                    _detail = new FeasibilityCriteriaDetail();
                    _detail.CapacityAvailableForProject = model.CapacityAvailableForProject;
                    _detail.TrainedStaff = model.TrainedStaff;
                    _detail.ByEquipment = model.ByEquipment;
                    _detail.ExpandScope = model.ExpandScope;
                    _detail.GeoGraphicalPresence = model.GeoGraphicalPresence;
                    _detail.ThirdPartyContract = model.ThirdPartyContract;
                    _detail.CostOfCompensationMonth = model.CostOfCompensationMonth;
                    _detail.CostOfCompensationMoney = model.CostOfCompensationMoney;
                    _detail.AnyInKindComponent = model.AnyInKindComponent;
                    _detail.UseableByOrganisation = model.UseableByOrganisation;
                    _detail.FeasibleExpertDeploy = model.FeasibleExpertDeploy;
                    _detail.EnoughTimeForProject = model.EnoughTimeForProject;
                    _detail.ProjectAllowedBylaw = model.ProjectAllowedBylaw;
                    _detail.ProjectByLeadership = model.ProjectByLeadership;
                    _detail.IsProjectPractical = model.IsProjectPractical;
                    _detail.PresenceCoverageInProject = model.PresenceCoverageInProject;
                    _detail.ProjectInLineWithOrgFocus = model.ProjectInLineWithOrgFocus;
                    _detail.EnoughTimeToPrepareProposal = model.EnoughTimeToPrepareProposal;
                    _detail.ProjectRealCost = model.ProjectRealCost;
                    _detail.IsCostGreaterthenBudget = model.IsCostGreaterthenBudget;
                    _detail.PerCostGreaterthenBudget = model.PerCostGreaterthenBudget;
                    _detail.IsFinancialContribution = model.IsFinancialContribution;
                    _detail.IsSecurity = model.IsSecurity;
                    _detail.IsGeographical = model.IsGeographical;
                    _detail.IsSeasonal = model.IsSeasonal;
                    _detail.ProjectId = model.ProjectId.Value;
                    _detail.IsDeleted = false;
                    _detail.CreatedById = UserId;
                    _detail.CreatedDate = DateTime.Now;
                    _uow.FeasibilityCriteriaDetailRepository.Add(_detail);
                }
                else
                {
                    _detail.CapacityAvailableForProject = model.CapacityAvailableForProject;
                    _detail.TrainedStaff = model.TrainedStaff;
                    _detail.ByEquipment = model.ByEquipment;
                    _detail.ExpandScope = model.ExpandScope;
                    _detail.GeoGraphicalPresence = model.GeoGraphicalPresence;
                    _detail.ThirdPartyContract = model.ThirdPartyContract;
                    _detail.CostOfCompensationMonth = model.CostOfCompensationMonth;
                    _detail.CostOfCompensationMoney = model.CostOfCompensationMoney;
                    _detail.AnyInKindComponent = model.AnyInKindComponent;
                    _detail.UseableByOrganisation = model.UseableByOrganisation;
                    _detail.FeasibleExpertDeploy = model.FeasibleExpertDeploy;
                    _detail.EnoughTimeForProject = model.EnoughTimeForProject;
                    _detail.ProjectAllowedBylaw = model.ProjectAllowedBylaw;
                    _detail.ProjectByLeadership = model.ProjectByLeadership;
                    _detail.IsProjectPractical = model.IsProjectPractical;
                    _detail.PresenceCoverageInProject = model.PresenceCoverageInProject;
                    _detail.ProjectInLineWithOrgFocus = model.ProjectInLineWithOrgFocus;
                    _detail.EnoughTimeToPrepareProposal = model.EnoughTimeToPrepareProposal;
                    _detail.ProjectRealCost = model.ProjectRealCost;
                    _detail.IsCostGreaterthenBudget = model.IsCostGreaterthenBudget;
                    _detail.PerCostGreaterthenBudget = model.PerCostGreaterthenBudget;
                    _detail.IsFinancialContribution = model.IsFinancialContribution;
                    _detail.IsSecurity = model.IsSecurity;
                    _detail.IsGeographical = model.IsGeographical;
                    _detail.IsSeasonal = model.IsSeasonal;
                    _detail.ProjectId = model.ProjectId.Value;
                    _detail.IsDeleted = false;
                    _detail.ModifiedById = UserId;
                    _detail.ModifiedDate = DateTime.Now;
                    _uow.GetDbContext().SaveChanges();
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

        public APIResponse GetAllCriteriaEvaluationDetalByProjectId(long ProjectId)
        {
            APIResponse response = new APIResponse();

            try
            {
                var details =
                    (from obj in _uow.GetDbContext().ProjectDetail
                     join donor in _uow.GetDbContext().DonorCriteriaDetail on obj.ProjectId equals donor.ProjectId into a
                     from donor in a.DefaultIfEmpty()
                     join purpose in _uow.GetDbContext().PurposeofInitiativeCriteria on obj.ProjectId equals purpose.ProjectId into d
                     from purpose in d.DefaultIfEmpty()
                     join eligibility in _uow.GetDbContext().EligibilityCriteriaDetail on obj.ProjectId equals eligibility.ProjectId into e
                     from eligibility in e.DefaultIfEmpty()
                     join feasibility in _uow.GetDbContext().FeasibilityCriteriaDetail on obj.ProjectId equals feasibility.ProjectId into g
                     from feasibility in g.DefaultIfEmpty()
                     join Priority in _uow.GetDbContext().PriorityCriteriaDetail on obj.ProjectId equals Priority.ProjectId into pr
                     from Priority in pr.DefaultIfEmpty()
                     join financial in _uow.GetDbContext().FinancialCriteriaDetail on obj.ProjectId equals financial.ProjectId into fi
                     from financial in fi.DefaultIfEmpty()
                     join risk in _uow.GetDbContext().RiskCriteriaDetail on obj.ProjectId equals risk.ProjectId into ri
                     from risk in ri.DefaultIfEmpty()
                         //join selected in _uow.GetDbContext().FinancialProjectDetail on obj.ProjectId equals selected.ProjectId into sp
                         //from selected in sp.DefaultIfEmpty()
                     select new CriteriaEveluationModel
                     {
                         ProjectId = obj.ProjectId,
                         IsCriteriaEvaluationSubmit = obj.IsCriteriaEvaluationSubmit,
                         DonorCEId = donor != null ? donor.DonorCEId : 0,
                         MethodOfFunding = donor.MethodOfFunding,
                         PastFundingExperience = donor.PastFundingExperience,
                         ProposalAccepted = donor.ProposalAccepted,
                         ProposalExperience = donor.ProposalExperience,
                         Professional = donor.Professional,
                         FundsOnTime = donor.FundsOnTime,
                         EffectiveCommunication = donor.EffectiveCommunication,
                         Dispute = donor.Dispute,
                         OtherDeliverable = donor.OtherDeliverable,
                         OtherDeliverableType = donor.OtherDeliverableType,
                         PastWorkingExperience = donor.PastWorkingExperience,
                         CriticismPerformance = donor.CriticismPerformance,
                         TimeManagement = donor.TimeManagement,
                         MoneyAllocation = donor.MoneyAllocation,
                         Accountability = donor.Accountability,
                         DeliverableQuality = donor.DeliverableQuality,
                         DonorFinancingHistory = donor.DonorFinancingHistory,
                         ReligiousStanding = donor.ReligiousStanding,
                         PoliticalStanding = donor.PoliticalStanding,
                         ProductServiceId = purpose != null ? purpose.ProductServiceId : 0,
                         Women = purpose.Women,
                         Children = purpose.Children,
                         Awareness = purpose.Awareness,
                         Education = purpose.Education,
                         DrugAbuses = purpose.DrugAbuses,
                         Right = purpose.Right,
                         Culture = purpose.Culture,
                         Music = purpose.Music,
                         Documentaries = purpose.Documentaries,
                         InvestigativeJournalism = purpose.InvestigativeJournalism,
                         HealthAndNutrition = purpose.HealthAndNutrition,
                         News = purpose.News,
                         SocioPolitiacalDebate = purpose.SocioPolitiacalDebate,
                         Studies = purpose.Studies,
                         Reports = purpose.Reports,
                         CommunityDevelopment = purpose.CommunityDevelopment,
                         Aggriculture = purpose.Aggriculture,
                         DRR = purpose.DRR,
                         ServiceEducation = purpose.ServiceEducation,
                         ServiceHealthAndNutrition = purpose.ServiceHealthAndNutrition,
                         RadioProduction = purpose.RadioProduction,
                         TVProgram = purpose.TVProgram,
                         PrintedMedia = purpose.PrintedMedia,
                         RoundTable = purpose.RoundTable,
                         Others = purpose.Others,
                         OtherActivity = purpose.OtherActivity,
                         TargetBenificaiaryWomen = purpose.TargetBenificaiaryWomen,
                         TargetBenificiaryMen = purpose.TargetBenificiaryMen,
                         TargetBenificiaryAgeGroup = purpose.TargetBenificiaryAgeGroup,
                         TargetBenificiaryaOccupation = purpose.TargetBenificiaryaOccupation,
                         Product = purpose.Product,
                         Service = purpose.Service,
                         DonorCriteriaMet = eligibility.DonorCriteriaMet,
                         EligibilityDealine = eligibility.EligibilityDealine,
                         CoPartnership = eligibility.CoPartnership,
                         CapacityAvailableForProject = feasibility.CapacityAvailableForProject,
                         TrainedStaff = feasibility.TrainedStaff,
                         ByEquipment = feasibility.ByEquipment,
                         ExpandScope = feasibility.ExpandScope,
                         GeoGraphicalPresence = feasibility.GeoGraphicalPresence,
                         ThirdPartyContract = feasibility.ThirdPartyContract,
                         CostOfCompensationMonth = feasibility.CostOfCompensationMonth,
                         CostOfCompensationMoney = feasibility.CostOfCompensationMoney,
                         AnyInKindComponent = feasibility.AnyInKindComponent,
                         UseableByOrganisation = feasibility.UseableByOrganisation,
                         FeasibleExpertDeploy = feasibility.FeasibleExpertDeploy,
                         EnoughTimeForProject = feasibility.EnoughTimeForProject,
                         ProjectAllowedBylaw = feasibility.ProjectAllowedBylaw,
                         ProjectByLeadership = feasibility.ProjectByLeadership,
                         IsProjectPractical = feasibility.IsProjectPractical,
                         PresenceCoverageInProject = feasibility.PresenceCoverageInProject,
                         ProjectInLineWithOrgFocus = feasibility.ProjectInLineWithOrgFocus,
                         EnoughTimeToPrepareProposal = feasibility.EnoughTimeToPrepareProposal,
                         ProjectRealCost = feasibility.ProjectRealCost,
                         IsCostGreaterthenBudget = feasibility.IsCostGreaterthenBudget,
                         PerCostGreaterthenBudget = feasibility.PerCostGreaterthenBudget,
                         IsFinancialContribution = feasibility.IsFinancialContribution,
                         IsSecurity = feasibility.IsSecurity,
                         IsGeographical = feasibility.IsGeographical,
                         IsSeasonal = feasibility.IsSeasonal,
                         IncreaseEligibility = Priority.IncreaseEligibility,
                         IncreaseReputation = Priority.IncreaseReputation,
                         ImproveDonorRelationship = Priority.ImproveDonorRelationship,
                         GoodCause = Priority.GoodCause,
                         ImprovePerformancecapacity = Priority.ImprovePerformancecapacity,
                         SkillImprove = Priority.SkillImprove,
                         FillingFundingGap = Priority.FillingFundingGap,
                         NewSoftware = Priority.NewSoftware,
                         NewEquipment = Priority.NewEquipment,
                         CoverageAreaExpansion = Priority.CoverageAreaExpansion,
                         NewTraining = Priority.NewTraining,
                         ExpansionGoal = Priority.ExpansionGoal,
                         Total = financial.Total,
                         ProjectActivities = financial.ProjectActivities,
                         Operational = financial.Operational,
                         Overhead_Admin = financial.Overhead_Admin,
                         Lump_Sum = financial.Lump_Sum,
                         Security = risk.Security,
                         Staff = risk.Staff,
                         ProjectAssets = risk.ProjectAssets,
                         Suppliers = risk.Suppliers,
                         Beneficiaries = risk.Beneficiaries,
                         OverallOrganization = risk.OverallOrganization,
                         DeliveryFaiLure = risk.DeliveryFaiLure,
                         PrematureSeizure = risk.PrematureSeizure,
                         GovernmentConfiscation = risk.GovernmentConfiscation,
                         DesctructionByTerroristActivity = risk.DesctructionByTerroristActivity,
                         Reputation = risk.Reputation,
                         Religious = risk.Religious,
                         Sectarian = risk.Sectarian,
                         Ethinc = risk.Ethinc,
                         Social = risk.Social,
                         Traditional = risk.Traditional,
                         FocusDivertingrisk = risk.FocusDivertingrisk,
                         Financiallosses = risk.Financiallosses,
                         Opportunityloss = risk.Opportunityloss,

                         //ProjectSelectionId = selected.ProjectSelectionId,

                         Probablydelaysinfunding = risk.Probablydelaysinfunding,
                         OtherOrganizationalHarms = risk.OtherOrganizationalHarms,
                         OrganizationalDescription = risk.OrganizationalDescription
                     }).FirstOrDefault(x => x.ProjectId == ProjectId);


                List<long?> selectedProjects = _uow.GetDbContext().FinancialProjectDetail.Where(x => x.ProjectId == ProjectId && x.IsDeleted == false).Select(x => x.ProjectSelectionId).ToList();

                details.ProjectSelectionId = selectedProjects != null ? selectedProjects : null;

                response.data.CriteriaEveluationModel = details;
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

        public APIResponse AddEditPriorityCriteria(PriorityCriteriaModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            PriorityCriteriaDetail _detail = new PriorityCriteriaDetail();
            try
            {
                _detail = _uow.GetDbContext().PriorityCriteriaDetail.Where(x => x.ProjectId == model.ProjectId && x.IsDeleted == false).FirstOrDefault();
                if (_detail == null)
                {
                    _detail = new PriorityCriteriaDetail();
                    _detail.IncreaseEligibility = model.IncreaseEligibility;
                    _detail.IncreaseReputation = model.IncreaseReputation;
                    _detail.ImproveDonorRelationship = model.ImproveDonorRelationship;
                    _detail.GoodCause = model.GoodCause;
                    _detail.ImprovePerformancecapacity = model.ImprovePerformancecapacity;
                    _detail.SkillImprove = model.SkillImprove;
                    _detail.FillingFundingGap = model.FillingFundingGap;
                    _detail.NewSoftware = model.NewSoftware;
                    _detail.NewEquipment = model.NewEquipment;
                    _detail.CoverageAreaExpansion = model.CoverageAreaExpansion;
                    _detail.NewTraining = model.NewTraining;
                    _detail.ExpansionGoal = model.ExpansionGoal;
                    _detail.ProjectId = model.ProjectId;
                    _detail.IsDeleted = false;
                    _detail.CreatedById = UserId;
                    _detail.CreatedDate = DateTime.Now;
                    _uow.PriorityCriteriaDetailRepository.Add(_detail);
                }
                else
                {
                    _detail.IncreaseEligibility = model.IncreaseEligibility;
                    _detail.IncreaseReputation = model.IncreaseReputation;
                    _detail.ImproveDonorRelationship = model.ImproveDonorRelationship;
                    _detail.GoodCause = model.GoodCause;
                    _detail.ImprovePerformancecapacity = model.ImprovePerformancecapacity;
                    _detail.SkillImprove = model.SkillImprove;
                    _detail.FillingFundingGap = model.FillingFundingGap;
                    _detail.NewSoftware = model.NewSoftware;
                    _detail.NewEquipment = model.NewEquipment;
                    _detail.CoverageAreaExpansion = model.CoverageAreaExpansion;
                    _detail.NewTraining = model.NewTraining;
                    _detail.ExpansionGoal = model.ExpansionGoal;
                    _detail.ProjectId = model.ProjectId;
                    _detail.IsDeleted = false;
                    _detail.ModifiedById = UserId;
                    _detail.ModifiedDate = DateTime.Now;
                    _uow.GetDbContext().SaveChanges();
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
        public APIResponse AddEditFinancialCriteria(FinancialCriteriaModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            FinancialCriteriaDetail _detail = new FinancialCriteriaDetail();
            try
            {
                _detail = _uow.GetDbContext().FinancialCriteriaDetail.Where(x => x.ProjectId == model.ProjectId && x.IsDeleted == false).FirstOrDefault();
                if (_detail == null)
                {
                    _detail = new FinancialCriteriaDetail();
                    _detail.ProjectActivities = model.ProjectActivities;
                    _detail.Operational = model.Operational;
                    _detail.Overhead_Admin = model.Overhead_Admin;
                    _detail.Lump_Sum = model.Lump_Sum;
                    _detail.ProjectId = model.ProjectId.Value;
                    _detail.IsDeleted = false;
                    _detail.CreatedById = UserId;
                    _detail.CreatedDate = DateTime.Now;
                    _uow.FinancialCriteriaDetailRepository.Add(_detail);
                }
                else
                {
                    _detail.ProjectActivities = model.ProjectActivities;
                    _detail.Operational = model.Operational;
                    _detail.Overhead_Admin = model.Overhead_Admin;
                    _detail.Lump_Sum = model.Lump_Sum;
                    _detail.ProjectId = model.ProjectId.Value;
                    _detail.IsDeleted = false;
                    _detail.ModifiedById = UserId;
                    _detail.ModifiedDate = DateTime.Now;
                    _uow.GetDbContext().SaveChanges();
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
        public APIResponse AddEditRiskCriteria(RiskCriteriaModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            RiskCriteriaDetail _detail = new RiskCriteriaDetail();
            try
            {
                _detail = _uow.GetDbContext().RiskCriteriaDetail.FirstOrDefault(x => x.ProjectId == model.ProjectId && x.IsDeleted == false);
                if (_detail == null)
                {
                    _detail = new RiskCriteriaDetail();
                    _detail.Security = model.Security;
                    _detail.Staff = model.Staff;
                    _detail.ProjectAssets = model.ProjectAssets;
                    _detail.Suppliers = model.Suppliers;
                    _detail.Beneficiaries = model.Beneficiaries;
                    _detail.OverallOrganization = model.OverallOrganization;
                    _detail.DeliveryFaiLure = model.DeliveryFaiLure;
                    _detail.PrematureSeizure = model.PrematureSeizure;
                    _detail.GovernmentConfiscation = model.GovernmentConfiscation;
                    _detail.DesctructionByTerroristActivity = model.DesctructionByTerroristActivity;
                    _detail.Reputation = model.Reputation;
                    _detail.Religious = model.Religious;
                    _detail.Sectarian = model.Sectarian;
                    _detail.Ethinc = model.Ethinc;
                    _detail.Social = model.Social;
                    _detail.Traditional = model.Traditional;
                    _detail.FocusDivertingrisk = model.FocusDivertingrisk;
                    _detail.Financiallosses = model.Financiallosses;
                    _detail.Opportunityloss = model.Opportunityloss;


                    _detail.Probablydelaysinfunding = model.Probablydelaysinfunding;
                    _detail.OtherOrganizationalHarms = model.OtherOrganizationalHarms;
                    _detail.OrganizationalDescription = model.OrganizationalDescription;
                    _detail.ProjectId = model.ProjectId.Value;
                    _detail.IsDeleted = false;
                    _detail.CreatedById = UserId;
                    _detail.CreatedDate = DateTime.Now;


                    _uow.RiskCriteriaDetailRepository.Add(_detail);


                }
                else
                {
                    _detail.Security = model.Security;
                    _detail.Staff = model.Staff;
                    _detail.ProjectAssets = model.ProjectAssets;
                    _detail.Suppliers = model.Suppliers;
                    _detail.Beneficiaries = model.Beneficiaries;
                    _detail.OverallOrganization = model.OverallOrganization;
                    _detail.DeliveryFaiLure = model.DeliveryFaiLure;
                    _detail.PrematureSeizure = model.PrematureSeizure;
                    _detail.GovernmentConfiscation = model.GovernmentConfiscation;
                    _detail.DesctructionByTerroristActivity = model.DesctructionByTerroristActivity;
                    _detail.Reputation = model.Reputation;
                    _detail.Religious = model.Religious;
                    _detail.Sectarian = model.Sectarian;
                    _detail.Ethinc = model.Ethinc;
                    _detail.Social = model.Social;
                    _detail.Traditional = model.Traditional;
                    _detail.FocusDivertingrisk = model.FocusDivertingrisk;
                    _detail.Financiallosses = model.Financiallosses;
                    _detail.Opportunityloss = model.Opportunityloss;


                    _detail.Probablydelaysinfunding = model.Probablydelaysinfunding;
                    _detail.OtherOrganizationalHarms = model.OtherOrganizationalHarms;
                    _detail.OrganizationalDescription = model.OrganizationalDescription;
                    _detail.ProjectId = model.ProjectId.Value;
                    _detail.IsDeleted = false;
                    _detail.ModifiedById = UserId;
                    _detail.ModifiedDate = DateTime.Now;


                    _uow.GetDbContext().SaveChanges();
                }



                if (model.ProjectSelectionId != null)
                {
                    //check
                    bool projectPresent = _uow.GetDbContext().FinancialProjectDetail.Any(x => x.ProjectId == model.ProjectId && x.IsDeleted == false);


                    //if exist then remove
                    if (projectPresent)
                    {
                        var projectExist = _uow.GetDbContext().FinancialProjectDetail.Where(x => x.ProjectId == model.ProjectId && x.IsDeleted == false);

                        // if exist then remove it
                        _uow.GetDbContext().FinancialProjectDetail.RemoveRange(projectExist);
                        _uow.GetDbContext().SaveChanges();
                    }

                    List<FinancialProjectDetail> projectList = new List<FinancialProjectDetail>();

                    foreach (var item in model.ProjectSelectionId)
                    {
                        FinancialProjectDetail _data = new FinancialProjectDetail();

                        _data.ProjectSelectionId = item;
                        _data.ProjectId = model.ProjectId.Value;
                        _data.IsDeleted = false;
                        _data.CreatedById = UserId;
                        _data.CreatedDate = DateTime.Now;

                        projectList.Add(_data);
                    }

                    //Add
                    _uow.GetDbContext().FinancialProjectDetail.AddRange(projectList);
                    _uow.GetDbContext().SaveChanges();
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
        public APIResponse AddEditTargetBeneficiary(TargetBeneficiaryDetail model, string UserId)
        {
            APIResponse response = new APIResponse();
            TargetBeneficiaryDetail _detail = new TargetBeneficiaryDetail();
            try
            {
                _detail = _uow.GetDbContext().TargetBeneficiaryDetail.Where(x => x.ProjectId == model.ProjectId && x.TargetId == model.TargetId && x.IsDeleted == false).FirstOrDefault();
                if (_detail == null)
                {
                    _detail = new TargetBeneficiaryDetail();
                    _detail.TargetType = model.TargetType;
                    _detail.TargetName = model.TargetName;
                    _detail.ProjectId = model.ProjectId;
                    _detail.IsDeleted = false;
                    _detail.CreatedById = UserId;
                    _detail.CreatedDate = DateTime.Now;
                    _uow.TargetBeneficiaryDetailRepository.Add(_detail);
                }
                else
                {
                    _detail.TargetType = model.TargetType;
                    _detail.TargetName = model.TargetName;
                    _detail.ProjectId = model.ProjectId;
                    _detail.IsDeleted = false;
                    _detail.ModifiedById = UserId;
                    _detail.ModifiedDate = DateTime.Now;
                    _uow.GetDbContext().SaveChanges();
                }
                response.CommonId.LongId = _detail.TargetId;
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

        public APIResponse AddEditFinancialProjectDetail(FinancialProjectDetailModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            FinancialProjectDetail _detail = new FinancialProjectDetail();
            try
            {
                _detail = _uow.GetDbContext().FinancialProjectDetail.Where(x => x.ProjectId == model.ProjectId && x.FinancialProjectDetailId == model.FinancialProjectDetailId && x.IsDeleted == false).FirstOrDefault();
                if (_detail == null)
                {
                    _detail = new FinancialProjectDetail();
                    _detail.ProjectId = model.ProjectId;
                    _detail.ProjectSelectionId = model.ProjectSelectionId;
                    _detail.ProjectName = model.ProjectName;
                    _detail.IsDeleted = false;
                    _detail.CreatedById = UserId;
                    _detail.CreatedDate = DateTime.Now;
                    _uow.FinancialProjectDetailRepository.Add(_detail);
                }
                else
                {
                    _detail.ProjectId = model.ProjectId;
                    _detail.ProjectSelectionId = model.ProjectSelectionId;
                    _detail.ProjectName = model.ProjectName;
                    _detail.IsDeleted = false;
                    _detail.CreatedById = UserId;
                    _detail.CreatedDate = DateTime.Now;
                    _uow.FinancialProjectDetailRepository.Add(_detail);
                    _uow.GetDbContext().SaveChanges();
                }
                response.CommonId.Id = Convert.ToInt32(_detail.ProjectSelectionId);
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

        #region priorityOtherDetail
        public async Task<APIResponse> GetAllPriorityDetailList()
        {
            APIResponse response = new APIResponse();
            try
            {
                var list = await _uow.GetDbContext().PriorityOtherDetail.Where(x => x.IsDeleted == false)
                   .OrderByDescending(x => x.PriorityOtherDetailId).ToListAsync();

                response.data.PriorityOtherDetail = list;
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


        public async Task<APIResponse> GetAllPriorityDetailByProjectId(long projectId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var list = await _uow.GetDbContext().PriorityOtherDetail.Where(x => x.IsDeleted == false && x.ProjectId == projectId)
                   .OrderByDescending(x => x.PriorityOtherDetailId).ToListAsync();

                response.data.PriorityOtherDetail = list;
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

        public async Task<APIResponse> AddPriorityOtherDetail(CEPriorityDetailModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                PriorityOtherDetail _detail = new PriorityOtherDetail();

                _detail.PriorityOtherDetailId = model.PriorityOtherDetailId.Value;
                _detail.Name = model.Name;
                _detail.ProjectId = model.ProjectId;
                _detail.IsDeleted = false;
                _detail.CreatedById = UserId;
                _detail.CreatedDate = DateTime.Now;

                await _uow.PriorityOtherDetailRepository.AddAsyn(_detail);

                response.CommonId.LongId = _detail.PriorityOtherDetailId;
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

        public async Task<APIResponse> EditPriorityOtherDetail(CEPriorityDetailModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                PriorityOtherDetail _detail = await _uow.GetDbContext().PriorityOtherDetail.FirstOrDefaultAsync(x => x.PriorityOtherDetailId == model.PriorityOtherDetailId && x.IsDeleted == false);
                if (_detail != null)
                {
                    _detail.Name = model.Name;
                    _detail.IsDeleted = false;
                    _detail.CreatedById = UserId;
                    _detail.CreatedDate = DateTime.Now;

                    await _uow.PriorityOtherDetailRepository.UpdateAsyn(_detail);
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

        public async Task<APIResponse> DeletePriorityOtherDetails(long priorityOtherDetailId, string userId)
        {
            APIResponse response = new APIResponse();
            try
            {
                PriorityOtherDetail priorityInfo = await _uow.PriorityOtherDetailRepository.FindAsync(c => c.PriorityOtherDetailId == priorityOtherDetailId);

                priorityInfo.IsDeleted = true;
                priorityInfo.ModifiedById = userId;
                priorityInfo.ModifiedDate = DateTime.Now;

                await _uow.PriorityOtherDetailRepository.UpdateAsyn(priorityInfo);
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

        #region FeasibilityExpertOtherDetail
        public async Task<APIResponse> GetAllFeasibilityExpertList()
        {
            APIResponse response = new APIResponse();
            try
            {
                var list = await _uow.GetDbContext().CEFeasibilityExpertOtherDetail.Where(x => x.IsDeleted == false)
                   .OrderByDescending(x => x.ExpertOtherDetailId).ToListAsync();

                response.data.FeasibilityExpertOtherDetail = list;
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


        public async Task<APIResponse> GetAllExpertDetailByProjectId(long projectId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var list = await _uow.GetDbContext().CEFeasibilityExpertOtherDetail.Where(x => x.IsDeleted == false && x.ProjectId == projectId)
                   .OrderByDescending(x => x.ExpertOtherDetailId).ToListAsync();

                response.data.FeasibilityExpertOtherDetail = list;
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

        public async Task<APIResponse> AddFeasibilityExpertDetail(CEFeasibilityExpertOtherModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                CEFeasibilityExpertOtherDetail _detail = new CEFeasibilityExpertOtherDetail();

                _detail.ExpertOtherDetailId = model.ExpertOtherDetailId.Value;
                _detail.Name = model.Name;
                _detail.ProjectId = model.ProjectId;
                _detail.IsDeleted = false;
                _detail.CreatedById = UserId;
                _detail.CreatedDate = DateTime.Now;

                await _uow.CEFeasibilityExpertOtherDetail.AddAsyn(_detail);

                response.CommonId.LongId = _detail.ExpertOtherDetailId;
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

        public async Task<APIResponse> EditFeasibilityExpertDetail(CEFeasibilityExpertOtherModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                CEFeasibilityExpertOtherDetail _detail = await _uow.GetDbContext().CEFeasibilityExpertOtherDetail.FirstOrDefaultAsync(x => x.ExpertOtherDetailId == model.ExpertOtherDetailId && x.IsDeleted == false);
                if (_detail != null)
                {
                    _detail.Name = model.Name;
                    _detail.IsDeleted = false;
                    _detail.CreatedById = UserId;
                    _detail.CreatedDate = DateTime.Now;

                    await _uow.CEFeasibilityExpertOtherDetail.UpdateAsyn(_detail);
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

        public async Task<APIResponse> DeleteFeasibilityExperrtDetails(long expertOtherDetailId, string userId)
        {
            APIResponse response = new APIResponse();
            try
            {
                CEFeasibilityExpertOtherDetail expertInfo = await _uow.CEFeasibilityExpertOtherDetail.FindAsync(c => c.ExpertOtherDetailId == expertOtherDetailId);

                expertInfo.IsDeleted = true;
                expertInfo.ModifiedById = userId;
                expertInfo.ModifiedDate = DateTime.Now;

                await _uow.CEFeasibilityExpertOtherDetail.UpdateAsyn(expertInfo);
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

        #region agegroup 

        public async Task<APIResponse> GetAllAgeGroupDetailList()
        {
            APIResponse response = new APIResponse();
            try
            {
                var list = await _uow.GetDbContext().CEAgeGroupDetail.Where(x => x.IsDeleted == false)
                   .OrderByDescending(x => x.AgeGroupOtherDetailId).ToListAsync();

                response.data.CEAgeGroupDetail = list;
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


        public async Task<APIResponse> GetAllAgeGroupByProjectId(long projectId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var list = await _uow.GetDbContext().CEAgeGroupDetail.Where(x => x.IsDeleted == false && x.ProjectId == projectId)
                   .OrderByDescending(x => x.AgeGroupOtherDetailId).ToListAsync();

                response.data.CEAgeGroupDetail = list;
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

        public async Task<APIResponse> AddAgeGroupDetail(CEAgeGroupDetailModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                CEAgeGroupDetail _detail = new CEAgeGroupDetail();

                _detail.AgeGroupOtherDetailId = model.AgeGroupOtherDetailId.Value;
                _detail.Name = model.Name;
                _detail.ProjectId = model.ProjectId;
                _detail.IsDeleted = false;
                _detail.CreatedById = UserId;
                _detail.CreatedDate = DateTime.Now;

                await _uow.CEAgeGroupDetail.AddAsyn(_detail);

                response.CommonId.LongId = _detail.AgeGroupOtherDetailId;
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

        public async Task<APIResponse> EditAgeGroupDetail(CEAgeGroupDetailModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                CEAgeGroupDetail _detail = await _uow.GetDbContext().CEAgeGroupDetail.FirstOrDefaultAsync(x => x.AgeGroupOtherDetailId == model.AgeGroupOtherDetailId && x.IsDeleted == false);
                if (_detail != null)
                {
                    _detail.Name = model.Name;
                    _detail.IsDeleted = false;
                    _detail.CreatedById = UserId;
                    _detail.CreatedDate = DateTime.Now;

                    await _uow.CEAgeGroupDetail.UpdateAsyn(_detail);
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

        public async Task<APIResponse> DeleteAgeGroupDetails(long ageGroupOtherDetailId, string userId)
        {
            APIResponse response = new APIResponse();
            try
            {
                CEAgeGroupDetail expertInfo = await _uow.CEAgeGroupDetail.FindAsync(c => c.AgeGroupOtherDetailId == ageGroupOtherDetailId);

                expertInfo.IsDeleted = true;
                expertInfo.ModifiedById = userId;
                expertInfo.ModifiedDate = DateTime.Now;

                await _uow.CEAgeGroupDetail.UpdateAsyn(expertInfo);
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



        #region CEOccupationDtail

        public async Task<APIResponse> GetAllOccuopationList()
        {
            APIResponse response = new APIResponse();
            try
            {
                var list = await _uow.GetDbContext().CEOccupationDetail.Where(x => x.IsDeleted == false)
                   .OrderByDescending(x => x.OccupationOtherDetailId).ToListAsync();

                response.data.CEOccupationDetail = list;
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


        public async Task<APIResponse> GetAllOccupatiopnByProjectId(long projectId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var list = await _uow.GetDbContext().CEOccupationDetail.Where(x => x.IsDeleted == false && x.ProjectId == projectId)
                   .OrderByDescending(x => x.OccupationOtherDetailId).ToListAsync();

                response.data.CEOccupationDetail = list;
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

        public async Task<APIResponse> AddOccupationOtherDetail(CEOccupationDetailModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                CEOccupationDetail _detail = new CEOccupationDetail();

                _detail.OccupationOtherDetailId = model.OccupationOtherDetailId.Value;
                _detail.Name = model.Name;
                _detail.ProjectId = model.ProjectId;
                _detail.IsDeleted = false;
                _detail.CreatedById = UserId;
                _detail.CreatedDate = DateTime.Now;

                await _uow.CEOccupationDetail.AddAsyn(_detail);

                response.CommonId.LongId = _detail.OccupationOtherDetailId;
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

        public async Task<APIResponse> EditOccupationOtherDetail(CEOccupationDetailModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                CEOccupationDetail _detail = await _uow.GetDbContext().CEOccupationDetail.FirstOrDefaultAsync(x => x.OccupationOtherDetailId == model.OccupationOtherDetailId && x.IsDeleted == false);
                if (_detail != null)
                {
                    _detail.Name = model.Name;
                    _detail.IsDeleted = false;
                    _detail.CreatedById = UserId;
                    _detail.CreatedDate = DateTime.Now;

                    await _uow.CEOccupationDetail.UpdateAsyn(_detail);
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

        public async Task<APIResponse> DeleteOccupationDetails(long occupationOtherDetailId, string userId)
        {
            APIResponse response = new APIResponse();
            try
            {
                CEOccupationDetail expertInfo = await _uow.CEOccupationDetail.FindAsync(c => c.OccupationOtherDetailId == occupationOtherDetailId);

                expertInfo.IsDeleted = true;
                expertInfo.ModifiedById = userId;
                expertInfo.ModifiedDate = DateTime.Now;

                await _uow.CEOccupationDetail.UpdateAsyn(expertInfo);
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


        #region assumprtionDetail
        public async Task<APIResponse> GetAllAssumptionList()
        {
            APIResponse response = new APIResponse();
            try
            {
                var list = await _uow.GetDbContext().CEAssumptionDetail.Where(x => x.IsDeleted == false)
                   .OrderByDescending(x => x.AssumptionDetailId).ToListAsync();

                response.data.CEAssumptionDetail = list;
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


        public async Task<APIResponse> GetAllAssumptionByProjectId(long projectId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var list = await _uow.GetDbContext().CEAssumptionDetail.Where(x => x.IsDeleted == false && x.ProjectId == projectId)
                   .OrderByDescending(x => x.AssumptionDetailId).ToListAsync();

                response.data.CEAssumptionDetail = list;
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

        public async Task<APIResponse> AddAssumptionDetail(CEAssumptionDetailModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                CEAssumptionDetail _detail = new CEAssumptionDetail();

                _detail.AssumptionDetailId = model.AssumptionDetailId.Value;
                _detail.Name = model.Name;
                _detail.ProjectId = model.ProjectId;
                _detail.IsDeleted = false;
                _detail.CreatedById = UserId;
                _detail.CreatedDate = DateTime.Now;

                await _uow.CEAssumptionDetail.AddAsyn(_detail);

                response.CommonId.LongId = _detail.AssumptionDetailId;
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

        public async Task<APIResponse> EditAssumptionDetail(CEAssumptionDetailModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                CEAssumptionDetail _detail = await _uow.GetDbContext().CEAssumptionDetail.FirstOrDefaultAsync(x => x.AssumptionDetailId == model.AssumptionDetailId && x.IsDeleted == false);
                if (_detail != null)
                {
                    _detail.Name = model.Name;
                    _detail.IsDeleted = false;
                    _detail.CreatedById = UserId;
                    _detail.CreatedDate = DateTime.Now;

                    await _uow.CEAssumptionDetail.UpdateAsyn(_detail);
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

        public async Task<APIResponse> DeleteAssumptionDetails(long assumptionDetailId, string userId)
        {
            APIResponse response = new APIResponse();
            try
            {
                CEAssumptionDetail expertInfo = await _uow.CEAssumptionDetail.FindAsync(c => c.AssumptionDetailId == assumptionDetailId);

                expertInfo.IsDeleted = true;
                expertInfo.ModifiedById = userId;
                expertInfo.ModifiedDate = DateTime.Now;

                await _uow.CEAssumptionDetail.UpdateAsyn(expertInfo);
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



        #region donorEligibilityCriteria
        public async Task<APIResponse> GetAllDonorEligibilityDetailList()
        {
            APIResponse response = new APIResponse();
            try
            {
                var list = await _uow.GetDbContext().DonorEligibilityCriteria.Where(x => x.IsDeleted == false)
                   .OrderByDescending(x => x.DonorEligibilityDetailId).ToListAsync();

                response.data.DonorEligibilityCriteria = list;
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


        public async Task<APIResponse> GetAllDonorEligibilityDetailByProjectId(long projectId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var list = await _uow.GetDbContext().DonorEligibilityCriteria.Where(x => x.IsDeleted == false && x.ProjectId == projectId)
                   .OrderByDescending(x => x.DonorEligibilityDetailId).ToListAsync();

                response.data.DonorEligibilityCriteria = list;
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

        public async Task<APIResponse> AddDonorEligibilityOtherDetail(DonorEligibilityCriteriaModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                DonorEligibilityCriteria _detail = new DonorEligibilityCriteria();

                _detail.DonorEligibilityDetailId = model.DonorEligibilityDetailId.Value;
                _detail.Name = model.Name;
                _detail.ProjectId = model.ProjectId;
                _detail.IsDeleted = false;
                _detail.CreatedById = UserId;
                _detail.CreatedDate = DateTime.Now;

                await _uow.DonorEligibilityCriteriaRepository.AddAsyn(_detail);

                response.CommonId.LongId = _detail.DonorEligibilityDetailId;
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

        public async Task<APIResponse> EditDonorEligibilityOtherDetail(DonorEligibilityCriteriaModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                DonorEligibilityCriteria _detail = await _uow.GetDbContext().DonorEligibilityCriteria.FirstOrDefaultAsync(x => x.DonorEligibilityDetailId == model.DonorEligibilityDetailId && x.IsDeleted == false);
                if (_detail != null)
                {
                    _detail.Name = model.Name;
                    _detail.IsDeleted = false;
                    _detail.CreatedById = UserId;
                    _detail.CreatedDate = DateTime.Now;

                    await _uow.DonorEligibilityCriteriaRepository.UpdateAsyn(_detail);
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

        public async Task<APIResponse> DeleteDOnorEligibilityCriteriaOtherDetails(long donorEligibilityDetailId, string userId)
        {
            APIResponse response = new APIResponse();
            try
            {
                DonorEligibilityCriteria donorEligibilityInfo = await _uow.DonorEligibilityCriteriaRepository.FindAsync(c => c.DonorEligibilityDetailId == donorEligibilityDetailId);

                donorEligibilityInfo.IsDeleted = true;
                donorEligibilityInfo.ModifiedById = userId;
                donorEligibilityInfo.ModifiedDate = DateTime.Now;

                await _uow.DonorEligibilityCriteriaRepository.UpdateAsyn(donorEligibilityInfo);
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



        #region add/edit IscriteriaEvalutaionSubmit

        public async Task<APIResponse> AddEditCriteriaEvalutionSubmitDetail(ProjectDetailNewModel model)
        {
            APIResponse response = new APIResponse();

            DbContext db = _uow.GetDbContext();
            try
            {
                var ProjectDetail = _uow.GetDbContext().ProjectDetail
                                                   .Where(x => x.ProjectId == model.ProjectId)
                                                   .FirstOrDefault(x => x.IsDeleted == false);
                if (ProjectDetail != null)
                {
                    ProjectDetail.IsCriteriaEvaluationSubmit = model.IsCriteriaEvaluationSubmit;
                    ProjectDetail.ModifiedDate = DateTime.Now;
                    await _uow.ProjectDetailNewRepository.UpdateAsyn(ProjectDetail, ProjectDetail.ProjectId);
                    response.StatusCode = StaticResource.successStatusCode;
                    response.CommonId.IsApproved = ProjectDetail.IsCriteriaEvaluationSubmit.Value;
                    response.Message = "Success";
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

        #region ErrorLog
        public void SaveErrorlog(int status, string message, string userName, string userId)
        {
            APIResponse response = new APIResponse();
            Errorlog obj = new Errorlog();
            obj.Status = status;
            obj.UserName = userName;
            obj.IsActive = true;
            obj.stackTrace = message;
            obj.IsDeleted = false;
            obj.CreatedById = userId;
            obj.CreatedDate = DateTime.Now;
            _uow.ErrorlogRepository.AddAsyn(obj);
            _uow.SaveAsync();




        }
        #endregion
        #endregion
    }
}

using AutoMapper;
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

namespace HumanitarianAssistance.Service.Classes
{

    public class ProjectService : IProject
    {
        IUnitOfWork _uow;
        IMapper _mapper;
        UserManager<AppUser> _userManager;
        public ProjectService(IUnitOfWork uow, IMapper mapper, UserManager<AppUser> userManager)
        {
            this._uow = uow;
            this._mapper = mapper;
            this._userManager = userManager;
        }

        #region Donor Details
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
                    model.DonorId = obj.DonorId;
                    var DonarDetail = await _uow.GetDbContext().DonorDetail
                      .FirstOrDefaultAsync(x => !x.IsDeleted.Value && x.DonorId == obj.DonorId);
                    response.data.DonorDetailById = DonarDetail;

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
        //public async Task<APIResponse> EditDonorDetails(DonorModel model, string UserId)
        //{
        //    APIResponse response = new APIResponse();
        //    try
        //    {
        //        var existRecord = await _uow.DonorDetailRepository.FindAsync(x => x.IsDeleted == false && x.DonorId == model.DonorId);
        //        if (existRecord != null)
        //        {
        //            existRecord.Name = model.Name;
        //            existRecord.ContactPerson = model.ContactPerson;
        //            existRecord.ContactDesignation = model.ContactDesignation;
        //            existRecord.ContactPersonEmail = model.ContactPersonEmail;
        //            existRecord.ContactPersonCell = model.ContactPersonCell;
        //            existRecord.ModifiedById = UserId;
        //            existRecord.ModifiedDate = DateTime.Now;
        //            _uow.GetDbContext().SaveChanges();
        //            response.StatusCode = StaticResource.successStatusCode;
        //            response.Message = "Success";
        //        }
        //        else
        //        {
        //            response.StatusCode = StaticResource.failStatusCode;
        //            response.Message = "Record not found";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        response.StatusCode = StaticResource.failStatusCode;
        //        response.Message = StaticResource.SomethingWrong + ex.Message;
        //    }
        //    return response;
        //}
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
        public async Task<APIResponse> AddProgramDetails(ProgramModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            long LatestCodeId = 0;
            var code = string.Empty;
            try
            {
                ProgramDetail obj = _mapper.Map<ProgramModel, ProgramDetail>(model);


                var data =  _uow.GetDbContext().ProgramDetail.FirstOrDefault(x => x.IsDeleted == false && x.ProgramName.Trim().ToLower() == model.ProgramName.Trim().ToLower()); //  Contains(model.ProgramName);                               


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
                        obj.IsProposalComplate = false;
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
                    }
                    else
                    {
                        var existProjectRecord = _uow.ProjectDetailNewRepository.Find(x => x.IsDeleted == false && x.ProjectId == model.ProjectId);
                        var exstingProjectTimePhase = _uow.ProjectPhaseTimeRepository.FindAll(y => y.IsDeleted == false && y.ProjectId == model.ProjectId);
                        if (existProjectRecord != null)
                        {
                            existProjectRecord.ProjectName = model.ProjectName;
                            existProjectRecord.ProjectDescription = model.ProjectDescription;
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

        public async Task<APIResponse> GetAllProjectList()
        {
            APIResponse response = new APIResponse();
            try
            {
                var ProjectList = await _uow.GetDbContext().ProjectDetail
                                          .Include(x => x.ProjectPhaseDetails)
                                          .Where(x => !x.IsDeleted.Value)
                                          .OrderByDescending(x => x.ProjectId).Select(x => new ProjectDetailNewModel
                                          {
                                              ProjectId = x.ProjectId,
                                              ProjectCode = x.ProjectCode,
                                              ProjectName = x.ProjectName,
                                              ProjectDescription = x.ProjectDescription,
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
                //var ProjectDetail = await _uow.GetDbContext().ProjectDetail
                //                          .Include(x => x.ProjectPhaseDetails)
                //                          .FirstOrDefaultAsync(x => !x.IsDeleted.Value && x.ProjectId == ProjectId);

                var ProjectDetail = (from obj in _uow.GetDbContext().ProjectDetail
                                     join win in _uow.GetDbContext().WinProjectDetails on obj.ProjectId equals win.ProjectId into p
                                     from c in p.DefaultIfEmpty()
                                     join approve in _uow.GetDbContext().ApproveProjectDetails on obj.ProjectId equals approve.ProjectId into z
                                     from y in z.DefaultIfEmpty()
                                     join phase in _uow.GetDbContext().ProjectPhaseDetails on obj.ProjectPhaseDetailsId equals phase.ProjectPhaseDetailsId 
                                     select new ProjectDetailNewModel
                                     {
                                         ProjectId = obj.ProjectId,
                                         ProjectCode = obj.ProjectCode,
                                         ProjectName = obj.ProjectName,
                                         ProjectDescription = obj.ProjectDescription,
                                         ProjectPhaseDetailsId = phase.ProjectPhaseDetailsId,
                                         IsWin = c==null? false: c.IsWin,
                                         IsApproved = y==null? false : y.IsApproved
                                     }).FirstOrDefault(x => x.ProjectId == ProjectId);


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
                if (model.ProjectProgramId == 0)
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
                    var existRecord = await _uow.ProjectProgramRepository.FindAsync(x => x.IsDeleted == false && x.ProjectId == model.ProjectId);
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
                if (model.ProjectSectorId == 0)
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
                    var existRecord = await _uow.ProjectSectorRepository.FindAsync(x => x.IsDeleted == false && x.ProjectSectorId == model.ProjectSectorId && x.ProjectId == model.ProjectId);
                    if (existRecord != null)
                    {
                        _mapper.Map(model, existRecord);
                        existRecord.IsDeleted = false;
                        existRecord.ModifiedById = UserId;
                        existRecord.ModifiedDate = DateTime.Now;
                        await _uow.ProjectSectorRepository.UpdateAsyn(existRecord);
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
        public async Task<APIResponse> getProjectSectorById(long ProjectId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var Projectsector = await _uow.GetDbContext().ProjectSector
                       .FirstOrDefaultAsync(x => !x.IsDeleted.Value && x.ProjectId == ProjectId);
                response.data.projectSector = Projectsector;
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
                if (model.ProjectAreaId == 0)
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
                    var existRecord = await _uow.ProjectAreaRepository.FindAsync(x => x.IsDeleted == false && x.ProjectAreaId == model.ProjectAreaId && x.ProjectId == model.ProjectId);
                    if (existRecord != null)
                    {
                        // _mapper.Map(model, existRecord);
                        existRecord.ProjectId = model.ProjectId;
                        existRecord.AreaId = model.AreaId;
                        existRecord.IsDeleted = false;
                        existRecord.ModifiedById = UserId;
                        existRecord.ModifiedDate = DateTime.Now;
                        _uow.GetDbContext().SaveChanges();
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
        public async Task<APIResponse> getProjectAreaById(long ProjectId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var Projectarea = await _uow.GetDbContext().ProjectArea
                       .FirstOrDefaultAsync(x => !x.IsDeleted.Value && x.ProjectId == ProjectId);
                response.data.projectArea = Projectarea;
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
                var ProjectCommunicationModel = //(from obj in _uow.GetDbContext().ProjectCommunication
                                                //join Att in _uow.GetDbContext().ProjectCommunicationAttachment on obj.ProjectId equals Att.ProjectId 
                                                 _uow.GetDbContext().ProjectCommunication.Where(x => !x.IsDeleted.Value && x.ProjectId == ProjectId).Select(y => new ProjectCommunicationModel()
                                                 {
                                                     ProjectDescription = y.ProjectDescription,
                                                     CreatedByName = _uow.GetDbContext().UserDetails.Where(z => z.AspNetUserId == y.CreatedById).Select(p => p.FirstName + " " + p.LastName).FirstOrDefault(),
                                                     RoleId = _uow.GetDbContext().UserRoles.Where(z => z.UserId == y.CreatedById).Select(z => z.RoleId).FirstOrDefault(),
                                                     //UserRole = _uow.GetDbContext().Roles.Where(a=>a.Id== RoleId).Select(z=>z.Name).FirstOrDefault(),
                                                     CreatedDate = y.CreatedDate.Value.ToString("dd MMMM yyyy h:mm tt"),
                                                     CreatedById = y.CreatedById,
                                                     PCId = y.PCId,
                                                     // PCAId = y.PCAId
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


        #region MyRegion
        /// <summary>
        /// Get All Area Details
        /// </summary>
        /// <returns></returns>
        public async Task<APIResponse> GetAllProvinceDetails()
        {
            APIResponse response = new APIResponse();
            try
            {
                var provincelist = (from p in await _uow.ProvinceDetailsRepository.GetAllAsyn()
                                    where p.IsDeleted == false
                                    select new ProvinceDetailsModel
                                    {
                                        ProvinceId = p.ProvinceId,
                                        ProvinceName = p.ProvinceName
                                    }).OrderBy(x => x.ProvinceName).ToList();
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
        public async Task<APIResponse> GetAllStrengthConsiderationDetails()
        {
            APIResponse response = new APIResponse();
            try
            {
                var StrengthConsiderationDetail = (from p in await _uow.StrengthConsiderationRepository.GetAllAsyn()
                                    where p.IsDeleted == false
                                    select new StrengthConsiderationDetail
                                    {
                                        StrengthConsiderationId = p.StrengthConsiderationId,
                                        StrengthConsiderationName = p.StrengthConsiderationName
                                    }).OrderBy(x => x.StrengthConsiderationName).ToList();
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
        public async Task<APIResponse> GetAllGenderConsiderationDetails()
        {
            APIResponse response = new APIResponse();
            try
            {
                var GenderConsiderationDetail = (from p in await _uow.GenderConsiderationRepository.GetAllAsyn()
                                                   where p.IsDeleted == false
                                                   select new GenderConsiderationDetail
                                                   {
                                                       GenderConsiderationId = p.GenderConsiderationId,
                                                       GenderConsiderationName = p.GenderConsiderationName
                                                   }).OrderBy(x => x.GenderConsiderationName).ToList();
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

        public async Task<APIResponse> GetAllSecurityDetails()
        {
            APIResponse response = new APIResponse();
            try
            {
                var SecurityDetail = (from p in await _uow.SecurityDetailRepository.GetAllAsyn()
                                                 where p.IsDeleted == false
                                                 select new SecurityDetail
                                                 {
                                                     SecurityId = p.SecurityId,
                                                     SecurityName = p.SecurityName
                                                 }).OrderBy(x => x.SecurityName).ToList();
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

        public async Task<APIResponse> GetAllSecurityConsiderationDetails()
        {
            APIResponse response = new APIResponse();
            try
            {
                var SecurityConsiderationDetail = (from p in await _uow.SecurityConsiderationDetailRepository.GetAllAsyn()
                                      where p.IsDeleted == false
                                      select new SecurityConsiderationDetail
                                      {
                                          SecurityConsiderationId = p.SecurityConsiderationId,
                                          SecurityConsiderationName = p.SecurityConsiderationName
                                      }).OrderBy(x => x.SecurityConsiderationName).ToList();
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
        public async Task<APIResponse> GetAllDistrictvalueByProvinceId(int[] ProvinceId)
        {
            APIResponse response = new APIResponse();
            try
            {              
                var DistrictDetailList =  _uow.GetDbContext().DistrictDetail.Where(x => !x.IsDeleted.Value).ToList();
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
                    if (model.ProjectOtherDetailId == 0)
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
                        var existProjectRecord = _uow.ProjectOtherDetailRepository.Find(x => x.IsDeleted == false && x.ProjectOtherDetailId == model.ProjectOtherDetailId);
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


        #endregion

        #region
        public async Task<APIResponse> AddApprovalDetail(ApproveProjectDetailModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                ApproveProjectDetails obj = _mapper.Map<ApproveProjectDetailModel, ApproveProjectDetails>(model);
                obj.ProjectId = model.ProjectId;
                obj.CommentText = model.CommentText;
                obj.FileName = model.FileName;
                obj.FilePath = model.FilePath;
                obj.IsApproved = model.IsApproved;
                obj.IsDeleted = false;
                obj.CreatedById = UserId;
                obj.CreatedDate = DateTime.Now;
                await _uow.ApproveProjectDetailsRepository.AddAsyn(obj);
                await _uow.SaveAsync();
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

        #region

        public async Task<APIResponse> WinApprovalDetail(WinApprovalProjectModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                WinProjectDetails obj = _mapper.Map<WinApprovalProjectModel, WinProjectDetails>(model);
                obj.ProjectId = model.ProjectId;
                obj.CommentText = model.CommentText;
                obj.FileName = model.FileName;
                obj.FilePath = model.FilePath;
                obj.IsWin = model.IsWin;
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
       public APIResponse AddEditProjectproposals(long Projectid,string userid)
        {
            ProjectProposalDetail model = new ProjectProposalDetail();
            APIResponse response = new APIResponse();
            try
            {
                var pathFile = Path.Combine(Directory.GetCurrentDirectory(), "GoogleCredentials/"+ "credentials.json");
                string _detail;
                _detail = _uow.GetDbContext().ProjectDetail.Where(x => x.ProjectId == Projectid && !x.IsDeleted.Value).Select(x => x.ProjectCode).FirstOrDefault();
                response.data.ProjectProposalModel = ProposalDoc.userCredential(_detail, pathFile);
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
        public APIResponse GetProjectproposalsById(long Projectid)
        {
            APIResponse response = new APIResponse();
            try
            {
                var pathFile = Path.Combine(Directory.GetCurrentDirectory(), "GoogleCredentials/" + "credentials.json");
                string _detail;
                _detail = _uow.GetDbContext().ProjectDetail.Where(x => x.ProjectId == Projectid && !x.IsDeleted.Value).Select(x => x.ProjectCode).FirstOrDefault();
                response.data.ProjectProposalModel = ProposalDoc.GetProjectProposal(_detail, pathFile);
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
        public APIResponse UploadEDIProposalFile(IFormFile file, string UserId,string Projectid,string fullPath)
        {
            APIResponse response = new APIResponse();
            try
            {
                string fileName = string.Empty;
                long ProjectId = long.Parse(Projectid);
                string _detail= _uow.GetDbContext().ProjectDetail.Where(x => x.ProjectId == ProjectId && !x.IsDeleted.Value).Select(x => x.ProjectCode).FirstOrDefault();
                var pathFile = Path.Combine(Directory.GetCurrentDirectory(), "GoogleCredentials/" + "credentials.json");
                if (file.Length > 0)
                {
                     fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"').Split('_')[0];
                    fileName = "EDI" + fileName;                   
                }
                response.data.ProjectProposalModel = ProposalDoc.uploadEDIdoc(_detail, file, fileName, pathFile, fullPath);
                //return Json("Upload Successful.");
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

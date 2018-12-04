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
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Service.Classes.Marketing
{
    public class MasterPageService : IMasterPageService
    {
        IUnitOfWork _uow;
        IMapper _mapper;
        UserManager<AppUser> _userManager;
        public MasterPageService(IUnitOfWork uow, IMapper mapper, UserManager<AppUser> userManager)
        {
            this._uow = uow;
            this._mapper = mapper;
            this._userManager = userManager;
        }
        #region Quality

        public async Task<APIResponse> GetQualityById(int model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                Quality obj = await _uow.QualityRepository.FindAsync(x => x.QualityId == model && x.IsDeleted == false);
                response.data.qualityById = obj;
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
        /// get Quality List
        /// </summary>
        /// <returns></returns>
        public async Task<APIResponse> GetAllQuality()
        {
            APIResponse response = new APIResponse();
            try
            {
                ICollection<Quality> Quality = await _uow.QualityRepository.FindAllAsync(x => x.IsDeleted == false);
                response.data.Qualities = Quality;
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
        /// Delete Selected Quality
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<APIResponse> DeleteQuality(int model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var qualityDetails = await _uow.QualityRepository.FindAsync(x => x.IsDeleted == false && x.QualityId == model);
                if (qualityDetails != null)
                {
                    qualityDetails.ModifiedById = UserId;
                    qualityDetails.ModifiedDate = DateTime.UtcNow;
                    qualityDetails.IsDeleted = true;
                    await _uow.QualityRepository.UpdateAsyn(qualityDetails);

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Edit Selected Quality
        /// </summary>
        /// <param name="model"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<APIResponse> EditQuality(QualityModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                Quality obj = await _uow.QualityRepository.FindAsync(x => x.QualityId == model.QualityId);
                obj.ModifiedById = UserId;
                obj.ModifiedDate = DateTime.Now;
                _mapper.Map(model, obj);
                await _uow.QualityRepository.UpdateAsyn(obj);
                await _uow.SaveAsync();
                response.data.qualityById = obj;
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
        /// Add New Quality
        /// </summary>
        /// <param name="model"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<APIResponse> AddQuality(QualityModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                Quality obj = _mapper.Map<QualityModel, Quality>(model);
                obj.CreatedById = UserId;
                obj.CreatedDate = DateTime.Now;
                obj.IsDeleted = false;
                obj.QualityName = model.QualityName;
                await _uow.QualityRepository.AddAsyn(obj);
                await _uow.SaveAsync();
                response.data.qualityById = obj;
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
        #endregion

        #region Medium

        public async Task<APIResponse> GetMediumById(int model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                Medium obj = await _uow.MediumRepository.FindAsync(x => x.MediumId == model && x.IsDeleted == false);
                response.data.mediumById = obj;
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
        /// get Medium List
        /// </summary>
        /// <returns></returns>
        public async Task<APIResponse> GetAllMedium()
        {
            APIResponse response = new APIResponse();
            try
            {
                ICollection<Medium> Mediums = await _uow.MediumRepository.FindAllAsync(x => x.IsDeleted == false);
                response.data.Mediums = Mediums;
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
        /// Delete Selected Medium
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<APIResponse> DeleteMedium(int model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var mediumDetails = await _uow.MediumRepository.FindAsync(x => x.IsDeleted == false && x.MediumId == model);
                if (mediumDetails != null)
                {
                    mediumDetails.ModifiedById = UserId;
                    mediumDetails.ModifiedDate = DateTime.UtcNow;
                    mediumDetails.IsDeleted = true;
                    await _uow.MediumRepository.UpdateAsyn(mediumDetails);

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Edit Selected Medium
        /// </summary>
        /// <param name="model"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<APIResponse> EditMedium(MediumModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                Medium obj = await _uow.MediumRepository.FindAsync(x => x.MediumId == model.MediumId);
                obj.ModifiedById = UserId;
                obj.ModifiedDate = DateTime.Now;
                _mapper.Map(model, obj);
                await _uow.MediumRepository.UpdateAsyn(obj);
                await _uow.SaveAsync();
                response.data.mediumById = obj;
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
        /// Add New Medium
        /// </summary>
        /// <param name="model"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<APIResponse> AddMedium(MediumModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                Medium obj = _mapper.Map<MediumModel, Medium>(model);
                obj.CreatedById = UserId;
                obj.CreatedDate = DateTime.Now;
                obj.IsDeleted = false;
                obj.MediumName = model.MediumName;
                await _uow.MediumRepository.AddAsyn(obj);
                await _uow.SaveAsync();
                response.data.mediumById = obj;
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
        #endregion

        #region Nature

        public async Task<APIResponse> GetNatureById(int model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                Nature obj = await _uow.NatureRepository.FindAsync(x => x.NatureId == model && x.IsDeleted == false);
                response.data.natureById = obj;
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
        /// get Nature List
        /// </summary>
        /// <returns></returns>
        public async Task<APIResponse> GetAllNature()
        {
            APIResponse response = new APIResponse();
            try
            {
                ICollection<Nature> Natures = await _uow.NatureRepository.FindAllAsync(x => x.IsDeleted == false);
                response.data.Natures = Natures;
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
        /// Delete Selected Nature
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<APIResponse> DeleteNature(int model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var natureDetails = await _uow.NatureRepository.FindAsync(x => x.IsDeleted == false && x.NatureId == model);
                if (natureDetails != null)
                {
                    natureDetails.ModifiedById = UserId;
                    natureDetails.ModifiedDate = DateTime.UtcNow;
                    natureDetails.IsDeleted = true;
                    await _uow.NatureRepository.UpdateAsyn(natureDetails);

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Edit Selected Nature
        /// </summary>
        /// <param name="model"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<APIResponse> EditNature(NatureModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                Nature obj = await _uow.NatureRepository.FindAsync(x => x.NatureId == model.NatureId);
                obj.ModifiedById = UserId;
                obj.ModifiedDate = DateTime.Now;
                _mapper.Map(model, obj);
                await _uow.NatureRepository.UpdateAsyn(obj);
                await _uow.SaveAsync();
                response.data.natureById = obj;
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
        /// Add New Nature
        /// </summary>
        /// <param name="model"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<APIResponse> AddNature(NatureModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                Nature obj = _mapper.Map<NatureModel, Nature>(model);
                obj.CreatedById = UserId;
                obj.CreatedDate = DateTime.Now;
                obj.IsDeleted = false;
                obj.NatureName = model.NatureName;
                await _uow.NatureRepository.AddAsyn(obj);
                await _uow.SaveAsync();
                response.data.natureById = obj;
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
        #endregion        

        #region Phase

        public async Task<APIResponse> GetPhaseById(int model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                JobPhase obj = await _uow.JobPhaseRepository.FindAsync(x => x.JobPhaseId == model && x.IsDeleted == false);
                response.data.phaseById = obj;
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
        /// get Phase List
        /// </summary>
        /// <returns></returns>
        public async Task<APIResponse> GetAllPhase()
        {
            APIResponse response = new APIResponse();
            try
            {
                ICollection<JobPhase> Phases = await _uow.JobPhaseRepository.FindAllAsync(x => x.IsDeleted == false);
                response.data.JobPhases = Phases;
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
        /// Delete Selected Phase
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<APIResponse> DeletePhase(int model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var phaseDetails = await _uow.JobPhaseRepository.FindAsync(x => x.IsDeleted == false && x.JobPhaseId == model);
                if (phaseDetails != null)
                {
                    phaseDetails.ModifiedById = UserId;
                    phaseDetails.ModifiedDate = DateTime.UtcNow;
                    phaseDetails.IsDeleted = true;
                    await _uow.JobPhaseRepository.UpdateAsyn(phaseDetails);

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Edit Selected Phase
        /// </summary>
        /// <param name="model"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<APIResponse> EditPhase(JobPhaseModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                JobPhase obj = await _uow.JobPhaseRepository.FindAsync(x => x.JobPhaseId == model.JobPhaseId);
                obj.ModifiedById = UserId;
                obj.ModifiedDate = DateTime.Now;
                _mapper.Map(model, obj);
                await _uow.JobPhaseRepository.UpdateAsyn(obj);
                await _uow.SaveAsync();
                response.data.phaseById = obj;
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
        /// Add New Phase
        /// </summary>
        /// <param name="model"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<APIResponse> AddPhase(JobPhaseModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                JobPhase obj = _mapper.Map<JobPhaseModel, JobPhase>(model);
                obj.CreatedById = UserId;
                obj.CreatedDate = DateTime.Now;
                obj.IsDeleted = false;
                obj.Phase = model.Phase;
                await _uow.JobPhaseRepository.AddAsyn(obj);
                await _uow.SaveAsync();
                response.data.phaseById = obj;
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
        #endregion

        #region Activity Type

        public async Task<APIResponse> GetActivityById(int model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                ActivityType obj = await _uow.ActivityTypeRepository.FindAsync(x => x.ActivityTypeId == model && x.IsDeleted == false);
                response.data.activityById = obj;
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
        /// get Activity Type List
        /// </summary>
        /// <returns></returns>
        public async Task<APIResponse> GetAllActivityType()
        {
            APIResponse response = new APIResponse();
            try
            {
                ICollection<ActivityType> activityTypes = await _uow.ActivityTypeRepository.FindAllAsync(x => x.IsDeleted == false);
                response.data.ActivityTypes = activityTypes;
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
        /// Delete Selected Activity Type
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<APIResponse> DeleteActivityType(int model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var activityType = await _uow.ActivityTypeRepository.FindAsync(x => x.IsDeleted == false && x.ActivityTypeId == model);
                if (activityType != null)
                {
                    activityType.ModifiedById = UserId;
                    activityType.ModifiedDate = DateTime.UtcNow;
                    activityType.IsDeleted = true;
                    await _uow.ActivityTypeRepository.UpdateAsyn(activityType);

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Edit Selected activity Type
        /// </summary>
        /// <param name="model"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<APIResponse> EditActivityType(ActivityTypeModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                ActivityType obj = await _uow.ActivityTypeRepository.FindAsync(x => x.ActivityTypeId == model.ActivityTypeId);
                obj.ModifiedById = UserId;
                obj.ModifiedDate = DateTime.Now;
                _mapper.Map(model, obj);
                await _uow.ActivityTypeRepository.UpdateAsyn(obj);
                await _uow.SaveAsync();
                response.data.activityById = obj;
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
        /// Add New Activity Type
        /// </summary>
        /// <param name="model"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<APIResponse> AddActivityType(ActivityTypeModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                ActivityType obj = _mapper.Map<ActivityTypeModel, ActivityType>(model);
                obj.CreatedById = UserId;
                obj.CreatedDate = DateTime.Now;
                obj.IsDeleted = false;
                obj.ActivityName = model.ActivityName;
                await _uow.ActivityTypeRepository.AddAsyn(obj);
                await _uow.SaveAsync();
                response.data.activityById = obj;
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
        #endregion

        #region MediaCategory
        /// <summary>
        /// get Media Category List
        /// </summary>
        /// <returns></returns>
        public async Task<APIResponse> GetAllMediaCategory()
        {
            APIResponse response = new APIResponse();
            try
            {
                ICollection<MediaCategory> mediaCategories = await _uow.MediaCategoryRepository.FindAllAsync(x => x.IsDeleted == false);
                response.data.MediaCategories = mediaCategories;
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
        /// Delete Selected Media Category
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<APIResponse> DeleteMediaCategory(int model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var mediaCategory = await _uow.MediaCategoryRepository.FindAsync(x => x.IsDeleted == false && x.MediaCategoryId == model);
                if (mediaCategory != null)
                {
                    mediaCategory.ModifiedById = UserId;
                    mediaCategory.ModifiedDate = DateTime.UtcNow;
                    mediaCategory.IsDeleted = true;
                    await _uow.MediaCategoryRepository.UpdateAsyn(mediaCategory);

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<APIResponse> GetMediaCategoryById(int model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                MediaCategory obj = await _uow.MediaCategoryRepository.FindAsync(x => x.MediaCategoryId == model && x.IsDeleted == false);
                response.data.mediaCategoryById = obj;
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
        /// Edit Selected Media Category
        /// </summary>
        /// <param name="model"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<APIResponse> EditMediaCategory(MediaCategoryModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                MediaCategory obj = await _uow.MediaCategoryRepository.FindAsync(x => x.MediaCategoryId == model.MediaCategoryId);
                obj.ModifiedById = UserId;
                obj.ModifiedDate = DateTime.Now;
                _mapper.Map(model, obj);
                await _uow.MediaCategoryRepository.UpdateAsyn(obj);
                await _uow.SaveAsync();
                response.data.mediaCategoryById = obj;
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
        /// Add New Media Category
        /// </summary>
        /// <param name="model"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<APIResponse> AddMediaCategory(MediaCategoryModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                MediaCategory obj = _mapper.Map<MediaCategoryModel, MediaCategory>(model);
                obj.CreatedById = UserId;
                obj.CreatedDate = DateTime.Now;
                obj.IsDeleted = false;
                obj.CategoryName = model.CategoryName;
                await _uow.MediaCategoryRepository.AddAsyn(obj);
                await _uow.SaveAsync();
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
                response.data.mediaCategoryById = obj;
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }
        #endregion

        #region Master Page

        /// <summary>
        /// Get master page matrix values
        /// </summary>
        /// <returns></returns>
        public async Task<APIResponse> GetMasterPagesValues()
        {
            APIResponse response = new APIResponse();
            try
            {
                ICollection<TimeCategory> timeCategories = await _uow.TimeCategoryRepository.FindAllAsync(x => x.IsDeleted == false);
                ICollection<MediaCategory> mediaCategories = await _uow.MediaCategoryRepository.FindAllAsync(x => x.IsDeleted == false);
                ICollection<Medium> Mediums = await _uow.MediumRepository.FindAllAsync(x => x.IsDeleted == false);
                ICollection<Language> languages = await _uow.LanguageRepository.FindAllAsync(x => x.IsDeleted == false);
                ICollection<Nature> jobNature = await _uow.NatureRepository.FindAllAsync(x => x.IsDeleted == false);
                ICollection<CurrencyDetails> currency = await _uow.CurrencyDetailsRepository.FindAllAsync(x => x.IsDeleted == false);
                ICollection<Quality> quality = await _uow.QualityRepository.FindAllAsync(x => x.IsDeleted == false);
                ICollection<ActivityType> activityType = await _uow.ActivityTypeRepository.FindAllAsync(x => x.IsDeleted == false);
                response.data.Qualities = quality;
                response.data.Mediums = Mediums;
                response.data.Currencies = currency;
                response.data.Languages = languages;
                response.data.Natures = jobNature;
                response.data.ActivityTypes = activityType;
                response.data.MediaCategories = mediaCategories;
                response.data.TimeCategories = timeCategories;
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

        #endregion

        #region TimeCategory

        public async Task<APIResponse> GetTimeCategoryById(int model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                TimeCategory obj = await _uow.TimeCategoryRepository.FindAsync(x => x.TimeCategoryId == model && x.IsDeleted == false);
                response.data.timeCatergoryById = obj;
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
        /// get Time Category List
        /// </summary>
        /// <returns></returns>
        public async Task<APIResponse> GetAllTimeCategory()
        {
            APIResponse response = new APIResponse();
            try
            {
                ICollection<TimeCategory> timeCategories = await _uow.TimeCategoryRepository.FindAllAsync(x => x.IsDeleted == false);
                response.data.TimeCategories = timeCategories;
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
        /// Delete Selected Time Category
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<APIResponse> DeleteTimeCategory(int model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var timeCategory = await _uow.TimeCategoryRepository.FindAsync(x => x.IsDeleted == false && x.TimeCategoryId == model);
                if (timeCategory != null)
                {
                    timeCategory.ModifiedById = UserId;
                    timeCategory.ModifiedDate = DateTime.UtcNow;
                    timeCategory.IsDeleted = true;
                    await _uow.TimeCategoryRepository.UpdateAsyn(timeCategory);

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Edit Selected Time Category
        /// </summary>
        /// <param name="model"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<APIResponse> EditTimeCategory(TimeCategoryModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                TimeCategory obj = await _uow.TimeCategoryRepository.FindAsync(x => x.TimeCategoryId == model.TimeCategoryId);
                obj.ModifiedById = UserId;
                obj.ModifiedDate = DateTime.Now;
                _mapper.Map(model, obj);
                await _uow.TimeCategoryRepository.UpdateAsyn(obj);
                await _uow.SaveAsync();
                response.data.timeCatergoryById = obj;
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
        /// Add New Time Category
        /// </summary>
        /// <param name="model"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<APIResponse> AddTimeCategory(TimeCategoryModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                TimeCategory obj = _mapper.Map<TimeCategoryModel, TimeCategory>(model);
                obj.CreatedById = UserId;
                obj.CreatedDate = DateTime.Now;
                obj.IsDeleted = false;
                obj.TimeCategoryName = model.TimeCategoryName;
                await _uow.TimeCategoryRepository.AddAsyn(obj);
                await _uow.SaveAsync();
                response.data.timeCatergoryById = obj;
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

        #endregion

        #region Unit Rate

        /// <summary>
        /// Get Unit Rate By Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<APIResponse> GetUnitRateById(int id, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var unitRateDetails = (from ur in _uow.GetDbContext().UnitRates
                                       join at in _uow.GetDbContext().ActivityTypes on ur.ActivityTypeId equals at.ActivityTypeId
                                       where !ur.IsDeleted.Value && !at.IsDeleted.Value && ur.UnitRateId == id
                                       select (new UnitRateDetailsModel
                                       {
                                           UnitRateId = ur.UnitRateId,
                                           ActivityTypeId = at.ActivityTypeId,
                                           ActivityName = at.ActivityName,
                                           UnitRates = ur.UnitRates,
                                           CurrencyId = ur.CurrencyId,
                                           MediumId = ur.MediumId,
                                           NatureId = ur.NatureId,
                                           QualityId = ur.QualityId,
                                           TimeCategoryId = ur.TimeCategoryId
                                       })).FirstOrDefault();

                response.data.rateDetailsById = unitRateDetails;
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

        public async Task<APIResponse> DeleteUnitRate(int UnitRateId, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var mediumDetails = await _uow.UnitRateRepository.FindAsync(x => x.IsDeleted == false && x.UnitRateId == UnitRateId);
                if (mediumDetails != null)
                {
                    mediumDetails.ModifiedById = UserId;
                    mediumDetails.ModifiedDate = DateTime.UtcNow;
                    mediumDetails.IsDeleted = true;
                    await _uow.UnitRateRepository.UpdateAsyn(mediumDetails);

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }
            return response;
        }

        /// <summary>
        /// Get Unit Rate List
        /// </summary>
        /// <returns></returns>
        public async Task<APIResponse> GetAllUnitRateList()
        {
            APIResponse response = new APIResponse();
            try
            {
                var unitRateList = (from ur in _uow.GetDbContext().UnitRates
                                    join at in _uow.GetDbContext().ActivityTypes on ur.ActivityTypeId equals at.ActivityTypeId
                                    where !ur.IsDeleted.Value && !at.IsDeleted.Value
                                    select (new UnitRateDetailsModel
                                    {
                                        UnitRateId = ur.UnitRateId,
                                        ActivityTypeId = at.ActivityTypeId,
                                        ActivityName = at.ActivityName,
                                        UnitRates = ur.UnitRates,
                                        CurrencyId = ur.CurrencyId,
                                        MediumId = ur.MediumId,
                                        NatureId = ur.NatureId,
                                        QualityId = ur.QualityId,
                                        TimeCategoryId = ur.TimeCategoryId
                                    })).ToList();


                response.data.UnitRateDetails = unitRateList;
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
        /// Add/Edit unit rate
        /// </summary>
        /// <param name="model"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<APIResponse> AddEditUnitRate(UnitRateModel model, string UserId)
        {
            DbContext db = _uow.GetDbContext();
            APIResponse response = new APIResponse();
            try
            {
                if (model.UnitRateId == 0 || model.UnitRateId == null)
                {
                    UnitRate obj = _mapper.Map<UnitRateModel, UnitRate>(model);
                    obj.CreatedById = UserId;
                    obj.CreatedDate = DateTime.Now;
                    obj.IsDeleted = false;
                    obj.ActivityTypeId = model.ActivityTypeId;
                    obj.CurrencyId = model.CurrencyId;
                    obj.UnitRates = model.UnitRate;
                    obj.MediumId = model.MediumId;
                    obj.NatureId = model.NatureId;
                    obj.QualityId = model.QualityId;
                    obj.TimeCategoryId = model.TimeCategoryId;
                    await _uow.UnitRateRepository.AddAsyn(obj);
                    await _uow.SaveAsync();
                    model.UnitRateId = obj.UnitRateId;
                    response.data.unitRateDetails = model;
                }
                else
                {
                    var obj = await _uow.UnitRateRepository.FindAsync(x => x.UnitRateId == model.UnitRateId);
                    obj.ModifiedById = UserId;
                    obj.ModifiedDate = DateTime.Now;
                    obj.ActivityTypeId = model.ActivityTypeId;
                    obj.CurrencyId = model.CurrencyId;
                    obj.MediumId = model.MediumId;
                    obj.NatureId = model.NatureId;
                    obj.QualityId = model.QualityId;
                    obj.TimeCategoryId = model.TimeCategoryId;
                    obj.UnitRates = model.UnitRate;
                    await _uow.UnitRateRepository.UpdateAsyn(obj);
                    response.data.unitRateDetailsById = obj;
                }

                var activityDetails = await _uow.ActivityTypeRepository.FindAsync(x => x.ActivityTypeId == model.ActivityTypeId);
                model.ActivityName = activityDetails.ActivityName;


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
        /// Edit Unit Rate
        /// </summary>
        /// <param name="model"></param>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<APIResponse> EditUnitRate(UnitRateModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                UnitRate obj = await _uow.UnitRateRepository.FindAsync(x => x.UnitRateId == model.UnitRateId);
                obj.ModifiedById = UserId;
                obj.ModifiedDate = DateTime.Now;
                _mapper.Map(model, obj);
                await _uow.UnitRateRepository.UpdateAsyn(obj);
                await _uow.SaveAsync();
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

        #endregion

        public async Task<APIResponse> GetUnitRateByActivityTypeId(UnitRateModel model, string UserId)
        {
            APIResponse response = new APIResponse();
            try
            {
                var activity = await _uow.ActivityTypeRepository.FindAllAsync(x => x.IsDeleted == false);
                var activityDetails = activity.Where(x => x.ActivityTypeId == model.ActivityTypeId).FirstOrDefault();
                string activityName = activityDetails.ActivityName;
                UnitRate unitRateById = new UnitRate();
                if (activityName == "Broadcasting")
                {
                    unitRateById = _uow.GetDbContext().UnitRates.Where(x => x.ActivityTypeId == model.ActivityTypeId && x.TimeCategoryId == model.TimeCategoryId && x.MediumId == model.MediumId && x.CurrencyId == model.CurrencyId && x.IsDeleted == false).FirstOrDefault();
                    //await _uow.UnitRateRepository.FindAsync(x => x.TimeCategoryId == model.TimeCategoryId && x.MediumId == model.MediumId && x.CurrencyId == model.CurrencyId && x.IsDeleted == false);
                }
                if (activityName == "Production")
                {
                    unitRateById = _uow.GetDbContext().UnitRates.Where(x => x.ActivityTypeId == model.ActivityTypeId && x.MediumId == model.MediumId && x.NatureId == model.NatureId && x.QualityId == model.QualityId && x.CurrencyId == model.CurrencyId && x.IsDeleted == false).FirstOrDefault();
                    //await _uow.UnitRateRepository.FindAsync(x=>x.MediumId == model.MediumId && x.NatureId == model.NatureId && x.QualityId == model.QualityId && x.CurrencyId == model.CurrencyId && x.IsDeleted == false);
                }

                response.data.UnitRateByActivityId = unitRateById;
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

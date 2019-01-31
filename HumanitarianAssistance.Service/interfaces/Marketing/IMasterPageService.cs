using DataAccess.DbEntities.Marketing;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.ViewModels.Models.Marketing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.interfaces.Marketing
{
    public interface IMasterPageService
    {
        #region Quality
        Task<APIResponse> GetQualityById(int model, string UserId);
        Task<APIResponse> GetAllQuality();
        Task<APIResponse> DeleteQuality(int model, string UserId);
        Task<APIResponse> AddEditQuality(QualityModel model, string UserId);
        #endregion

        #region Medium
        Task<APIResponse> GetAllMedium();
        Task<APIResponse> DeleteMedium(int model, string UserId);
        Task<APIResponse> AddEditMedium(MediumModel model, string UserId);
        Task<APIResponse> GetMediumById(int model, string UserId);
        #endregion

        #region Nature
        Task<APIResponse> GetAllNature();
        Task<APIResponse> GetNatureById(int model, string UserId);
        Task<APIResponse> DeleteNature(int model, string UserId);
        Task<APIResponse> AddEditNature(NatureModel model, string UserId);
        #endregion

        #region Phase
        Task<APIResponse> GetPhaseById(int model, string UserId);
        Task<APIResponse> GetAllPhase();
        Task<APIResponse> DeletePhase(int model, string UserId);
        Task<APIResponse> AddEditPhase(JobPhaseModel model, string UserId);
        #endregion

        #region Activity Type
        Task<APIResponse> GetActivityById(int model, string UserId);
        Task<APIResponse> GetAllActivityType();
        Task<APIResponse> DeleteActivityType(int model, string UserId);
        Task<APIResponse> AddEditActivityType(ActivityTypeModel model, string UserId);
        #endregion

        #region Producer
        Task<APIResponse> GetProducerById(int model, string UserId);
        Task<APIResponse> GetAllProducers();
        Task<APIResponse> DeleteProducer(int model, string UserId);
        Task<APIResponse> AddEditProducer(ProducerModel model, string UserId);
        #endregion

        #region Media Category
        Task<APIResponse> GetAllMediaCategory();
        Task<APIResponse> DeleteMediaCategory(int model, string UserId);
        Task<APIResponse> GetMediaCategoryById(int model, string UserId);
        Task<APIResponse> AddEditMediaCategory(MediaCategoryModel model, string UserId);
        #endregion

        #region Time Category
        Task<APIResponse> GetAllTimeCategory();
        Task<APIResponse> GetMasterPagesValues();
        Task<APIResponse> DeleteTimeCategory(int model, string UserId);
        Task<APIResponse> AddEditTimeCategory(TimeCategoryModel model, string UserId);
        Task<APIResponse> GetTimeCategoryById(int model, string UserId);
        #endregion

        #region Unit Rate
        Task<APIResponse> AddEditUnitRate(UnitRateModel model, string UserId);
        Task<APIResponse> EditUnitRate(UnitRateModel model, string UserId);
        Task<APIResponse> GetAllUnitRateList();
        Task<APIResponse> GetUnitRateById(int id, string UserId);
        Task<APIResponse> GetUnitRateByActivityTypeId(UnitRateModel model, string UserId);
        Task<APIResponse> DeleteUnitRate(int id, string UserId);
        Task<APIResponse> GetUnitRatePaginatedList(UnitRatePaginationModel model, string UserId);
        #endregion
    }
}

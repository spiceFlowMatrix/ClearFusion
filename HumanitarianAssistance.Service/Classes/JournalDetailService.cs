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
    public class JournalDetailService : IJournalDetail
    {
        IUnitOfWork _uow;
        IMapper _mapper;
        UserManager<AppUser> _userManager;
        public JournalDetailService(IUnitOfWork uow, IMapper mapper, UserManager<AppUser> userManager)
        {
            this._uow = uow;
            this._mapper = mapper;
            this._userManager = userManager;
        }
        public async Task<APIResponse> AddJournalDetail(JournalDetailModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                var existjournal = await _uow.JournalDetailRepository.FindAsync(o => o.JournalName == model.JournalName);
                if (existjournal == null)
                {
                    JournalDetail obj = _mapper.Map<JournalDetail>(model);
                    obj.CreatedById = model.CreatedById;
                    obj.CreatedDate = DateTime.UtcNow;
                    obj.IsDeleted = false;
                    await _uow.JournalDetailRepository.AddAsyn(obj);
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

        public async Task<APIResponse> EditJournalDetail(JournalDetailModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                var journalInfo = await _uow.JournalDetailRepository.FindAsync(c => c.JournalCode == model.JournalCode);
                journalInfo.JournalName = model.JournalName;
                journalInfo.JournalType = model.JournalType;
                journalInfo.ModifiedById = model.ModifiedById;
                journalInfo.ModifiedDate = model.ModifiedDate;
                await _uow.JournalDetailRepository.UpdateAsyn(journalInfo, journalInfo.JournalCode);
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

        public async Task<APIResponse> DeleteJournalDetail(JournalDetailModelDelete model)
        {
            APIResponse response = new APIResponse();
            try
            {
                var journalInfo = await _uow.JournalDetailRepository.FindAsync(c => c.JournalCode == model.JournalCode);
                journalInfo.IsDeleted = true;
                journalInfo.ModifiedById = model.ModifiedById;
                journalInfo.ModifiedDate = model.ModifiedDate;
                await _uow.JournalDetailRepository.UpdateAsyn(journalInfo, journalInfo.JournalCode);
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

        public async Task<APIResponse> GetAllJournalDetail()
        {
            APIResponse response = new APIResponse();
            try
            {
                var journallist = (from o in await _uow.JournalDetailRepository.GetAllAsyn()
                                  where o.IsDeleted == false orderby o.JournalCode ascending
                                  select new JournalDetailModel
                                  {
                                      JournalCode = o.JournalCode,
                                      JournalName = o.JournalName,
                                      JournalType = o.JournalType,
                                      CreatedById = o.CreatedById,
                                      CreatedDate = o.CreatedDate,
                                      ModifiedById = o.ModifiedById,
                                      ModifiedDate = o.ModifiedDate
                                  }).ToList();
                response.data.JournalDetailList = journallist;
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

        public async Task<APIResponse> GetJournalDetailByCode(int JournalCode)
        {
            APIResponse response = new APIResponse();
            try
            {
                var journallist = (from o in await _uow.JournalDetailRepository.GetAllAsyn()
                                   where o.JournalCode == JournalCode
                                   select new JournalDetailModel
                                   {
                                       JournalCode = o.JournalCode,
                                       JournalName = o.JournalName,
                                       JournalType = o.JournalType,
                                       CreatedById = o.CreatedById,
                                       CreatedDate = o.CreatedDate,
                                       ModifiedById = o.ModifiedById,
                                       ModifiedDate = o.ModifiedDate
                                   }).ToList();
                response.data.JournalDetailList = journallist;
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

        public async Task<APIResponse> GetJournalDetailByName(string JournalName)
        {
            APIResponse response = new APIResponse();
            try
            {
                var journallist = (from o in await _uow.JournalDetailRepository.GetAllAsyn()
                                   where o.JournalName == JournalName
                                   select new JournalDetailModel
                                   {
                                       JournalCode = o.JournalCode,
                                       JournalName = o.JournalName,
                                       JournalType = o.JournalType,
                                       CreatedById = o.CreatedById,
                                       CreatedDate = o.CreatedDate,
                                       ModifiedById = o.ModifiedById,
                                       ModifiedDate = o.ModifiedDate
                                   }).ToList();
                response.data.JournalDetailList = journallist;
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

using AutoMapper;
using DataAccess;
using DataAccess.DbEntities;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Service.APIResponses;
using HumanitarianAssistance.Service.interfaces;
using HumanitarianAssistance.ViewModels.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Service.Classes
{
    public class EmailSettingService : IEmailSetting
    {
        IUnitOfWork _uow;
        IMapper _mapper;
        UserManager<AppUser> _userManager;
        public EmailSettingService(IUnitOfWork uow, IMapper mapper, UserManager<AppUser> userManager)
        {
            this._uow = uow;
            this._mapper = mapper;
            this._userManager = userManager;
        }

        public async Task<APIResponse> GetAllEmailSettingDetail()
        {            
            APIResponse response = new APIResponse();
            try
            {
                var emaillist = await Task.Run(() =>
                  _uow.GetDbContext().EmailSettingDetail.Include(e => e.EmailTypes).Where(a => a.IsDeleted == false).ToList()
                    );
                var emailsettinglist = emaillist.Select(e => new EmailSettingModel
                {
                    EmailId = e.EmailId,
                    SenderEmail = e.SenderEmail,
                    EmailTypeName = e.EmailTypes.EmailTypeName,
                    EmailTypeId = e.EmailTypes.EmailTypeId,
                    SenderPassword = e.SenderPassword,
                    SmtpPort = e.SmtpPort,
                    SmtpServer = e.SmtpServer,
                    EnableSSL = e.EnableSSL
                }).ToList();
                response.data.EmailSettingList = emailsettinglist;
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

        public async Task<APIResponse> AddEmailSettingDetail(EmailSettingModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                EmailSettingDetail obj = _mapper.Map<EmailSettingDetail>(model);
                obj.CreatedById = model.CreatedById;
                obj.CreatedDate = DateTime.UtcNow;
                obj.IsDeleted = false;
                await _uow.EmailSettingDetailRepository.AddAsyn(obj);
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

        public async Task<APIResponse> EditEmailSettingDetail(EmailSettingModel model)
        {
            APIResponse response = new APIResponse();
            try
            {
                var emailsettingInfo = await _uow.EmailSettingDetailRepository.FindAsync(e => e.EmailId == model.EmailId);
                if (emailsettingInfo != null)
                {

                    emailsettingInfo.SenderEmail = model.SenderEmail;
                    emailsettingInfo.EmailTypeId = model.EmailTypeId;
                    emailsettingInfo.SenderPassword = model.SenderPassword;
                    emailsettingInfo.SmtpPort = model.SmtpPort;
                    emailsettingInfo.SmtpServer = model.SmtpServer;
                    emailsettingInfo.EnableSSL = model.EnableSSL;
                    emailsettingInfo.ModifiedById = model.ModifiedById;
                    emailsettingInfo.ModifiedDate = model.ModifiedDate;
                    await _uow.EmailSettingDetailRepository.UpdateAsyn(emailsettingInfo);
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

        public async Task<APIResponse> GetAllEmailType()
        {
            APIResponse response = new APIResponse();
            try
            {
			    var emailtypelist = (from e in await _uow.EmailTypeRepository.GetAllAsyn()
                                        where e.IsDeleted == false
                                        select new EmailTypeModel
                                        {
                                            EmailTypeId = e.EmailTypeId,
                                            EmailTypeName = e.EmailTypeName
                                        }).ToList();
                response.data.EmailTypeList = emailtypelist;
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

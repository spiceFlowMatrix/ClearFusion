using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Configuration.Commands.Update
{
    public class EditEmailSettingCommandHandler: IRequestHandler<EditEmailSettingCommand, ApiResponse>
    {

        private readonly HumanitarianAssistanceDbContext _dbContext;
        public EditEmailSettingCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(EditEmailSettingCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                EmailSettingDetail emailsettingInfo = await _dbContext.EmailSettingDetail.FirstOrDefaultAsync(e => e.EmailId == request.EmailId);
                
                if (emailsettingInfo != null)
                {
                    emailsettingInfo.SenderEmail = request.SenderEmail;
                    emailsettingInfo.EmailTypeId = request.EmailTypeId;
                    emailsettingInfo.SenderPassword = request.SenderPassword;
                    emailsettingInfo.SmtpPort = request.SmtpPort;
                    emailsettingInfo.SmtpServer = request.SmtpServer;
                    emailsettingInfo.EnableSSL = request.EnableSSL;
                    emailsettingInfo.ModifiedById = request.ModifiedById;
                    emailsettingInfo.ModifiedDate = request.ModifiedDate;
                    
                    _dbContext.EmailSettingDetail.Update(emailsettingInfo);
                    await _dbContext.SaveChangesAsync();

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
    }
}
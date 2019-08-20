using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Configuration.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Configuration.Queries
{
    public class GetAllEmailSettingQueryHandler: IRequestHandler<GetAllEmailSettingQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public GetAllEmailSettingQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllEmailSettingQuery request, CancellationToken cancellationToken)
        {            
            ApiResponse response = new ApiResponse();

            try
            {
               var emaillist =  await _dbContext.EmailSettingDetail.Include(e => e.EmailTypes).Where(a => a.IsDeleted == false).ToListAsync();
                
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
    }
}
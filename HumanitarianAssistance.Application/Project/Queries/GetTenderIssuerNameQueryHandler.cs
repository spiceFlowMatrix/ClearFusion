using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Queries {
    public class GetTenderIssuerNameQueryHandler : IRequestHandler<GetTenderIssuerNameQuery, ApiResponse> {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public GetTenderIssuerNameQueryHandler (HumanitarianAssistanceDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle (GetTenderIssuerNameQuery request, CancellationToken cancellationToken) {
            ApiResponse response = new ApiResponse ();
            try 
            { 
                var _logisticsReq = await _dbContext.ProjectLogisticRequests.FirstOrDefaultAsync(x=>x.IsDeleted==false && x.LogisticRequestsId == request.RequestId);
                
                if(_logisticsReq == null) 
                {
                    throw new Exception("Request not found!");
                }

                var user = await _dbContext.UserDetails.FirstOrDefaultAsync(x=>x.IsDeleted == false && x.AspNetUserId == _logisticsReq.ModifiedById);
                response.ResponseData = user.FirstName + ' ' + user.LastName;
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
    }
}
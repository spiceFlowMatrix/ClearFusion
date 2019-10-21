using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Marketing.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Marketing.Queries
{
    public class GetClientDetailsByIdQueryHandler : IRequestHandler<GetClientDetailsByIdQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetClientDetailsByIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetClientDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                //add await by AS 07/06/2019
                var clientDetails = await (from ur in _dbContext.ClientDetails
                                          join at in _dbContext.Categories on ur.CategoryId equals at.CategoryId
                                          into jou
                                          from dev_skill in jou.DefaultIfEmpty()
                                          from a in _dbContext.ClientDetails
                                          where !ur.IsDeleted && !a.IsDeleted && ur.ClientId == request.ClientId
                                          select (new ClientDetailModel
                                          {
                                              CategoryId = ur.CategoryId,
                                              CategoryName = dev_skill.CategoryName ?? String.Empty,
                                              ClientBackground = ur.ClientBackground,
                                              ClientCode = ur.ClientCode,
                                              ClientId = ur.ClientId,
                                              ClientName = ur.ClientName,
                                              Email = ur.Email,
                                              FocalPoint = ur.FocalPoint,
                                              History = ur.History,
                                              Phone = ur.Phone,
                                              PhysicialAddress = ur.PhysicialAddress,
                                              Position = ur.Position
                                          })).FirstOrDefaultAsync();

                response.data.clientDetailsById = clientDetails;
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
    }
}

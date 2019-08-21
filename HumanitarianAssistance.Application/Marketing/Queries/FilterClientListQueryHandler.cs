using HumanitarianAssistance.Application.Infrastructure;
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
    public class FilterClientListQueryHandler : IRequestHandler<FilterClientListQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public FilterClientListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(FilterClientListQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var contractList = await _dbContext.ClientDetails.Where(x => x.IsDeleted == false).ToListAsync();
                if (request != null)
                {
                    if (request.ClientId != 0 && request.ClientId != null)
                    {
                        contractList = contractList.Where(x => x.ClientId == request.ClientId).ToList();
                    }

                    if (!string.IsNullOrEmpty(request.ClientName))
                    {
                        contractList = contractList.Where(x => x.ClientName == request.ClientName).ToList();
                    }
                    if (request.CategoryId != 0 && request.CategoryId != null)
                    {
                        contractList = contractList.Where(x => x.CategoryId == request.CategoryId).ToList();
                    }
                    if (!string.IsNullOrEmpty(request.Email))
                    {
                        contractList = contractList.Where(x => x.Email == request.Email).ToList();
                    }
                    if (!string.IsNullOrEmpty(request.Position))
                    {
                        contractList = contractList.Where(x => x.Position == request.Position).ToList();
                    }
                    response.data.ClientDetails = contractList;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "No Entries Found.Try Different Filters";
                }
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

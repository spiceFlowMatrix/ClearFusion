using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Store.Models;
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

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetItemSpecificationsMasterQueryHandler : IRequestHandler<GetItemSpecificationsMasterQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        public GetItemSpecificationsMasterQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetItemSpecificationsMasterQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var list = await _dbContext.ItemSpecificationMaster.Where(x => x.IsDeleted == false && x.ItemTypeId == request.ItemTypeId && x.OfficeId == request.OfficeId).ToListAsync();
                response.data.ItemSpecificationMasterList = list.Select(x => new ItemSpecificationMasterModel
                {
                    ItemSpecificationMasterId = x.ItemSpecificationMasterId,
                    ItemSpecificationField = x.ItemSpecificationField,
                    OfficeId = x.OfficeId,
                    ItemTypeId = x.ItemTypeId
                }).ToList();
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

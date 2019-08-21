using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Store.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetAllItemSpecificationsDetailsQueryHandler : IRequestHandler<GetAllItemSpecificationsDetailsQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetAllItemSpecificationsDetailsQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllItemSpecificationsDetailsQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                bool flag = await _dbContext.ItemSpecificationDetails.AnyAsync(x => x.ItemId == request.ItemId && x.IsDeleted == false);
                var masterList = await _dbContext.ItemSpecificationMaster.Where(x => x.IsDeleted == false && x.ItemTypeId == request.ItemTypeId && x.OfficeId == request.OfficeId).ToListAsync();
                if (flag == true)
                {

                    var list = await _dbContext.ItemSpecificationDetails.Include(x => x.ItemSpecificationMaster).Where(x => x.ItemId == request.ItemId && x.ItemSpecificationMaster.ItemTypeId == request.ItemTypeId && x.IsDeleted == false).ToListAsync();
                    response.data.ItemSpecificationDetailList = list.Select(x => new ItemSpecificationDetailModel
                    {
                        ItemSpecificationMasterId = x.ItemSpecificationMasterId,
                        ItemId = x.ItemId,
                        ItemSpecificationValue = x.ItemSpecificationValue,
                        ItemSpecificationField = x.ItemSpecificationMaster.ItemSpecificationField
                    }).ToList();

                    foreach (var item in masterList)
                    {
                        var recordExist = list.Where(x => x.ItemSpecificationMasterId == item.ItemSpecificationMasterId).FirstOrDefault();
                        if (recordExist == null)
                        {
                            ItemSpecificationDetailModel obj = new ItemSpecificationDetailModel();
                            obj.ItemSpecificationMasterId = item.ItemSpecificationMasterId;
                            obj.ItemId = request.ItemId;
                            obj.ItemSpecificationValue = null;
                            obj.ItemSpecificationField = item.ItemSpecificationField;
                            response.data.ItemSpecificationDetailList.Add(obj);
                        }
                    }
                }
                else
                {
                    response.data.ItemSpecificationDetailList = masterList.Select(x => new ItemSpecificationDetailModel
                    {
                        ItemSpecificationMasterId = x.ItemSpecificationMasterId,
                        ItemId = request.ItemId,
                        ItemSpecificationValue = null,
                        ItemSpecificationField = x.ItemSpecificationField
                    }).ToList();
                }
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

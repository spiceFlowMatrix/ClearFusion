using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Store;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Store.Commands.Update
{
    public class EditItemSpecificationsDetailsCommandHandler : IRequestHandler<EditItemSpecificationsDetailsCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public EditItemSpecificationsDetailsCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(EditItemSpecificationsDetailsCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                if (request != null)
                {
                    var existRecord = await _dbContext.ItemSpecificationDetails.Where(x => x.IsDeleted == false && x.ItemId == request.ItemId && x.ItemSpecificationMasterId == request.ItemSpecificationMasterId).ToListAsync();
                    if (existRecord.Count > 0)
                    {
                        _dbContext.ItemSpecificationDetails.RemoveRange(existRecord);
                    }
                    ItemSpecificationDetails obj = _mapper.Map<ItemSpecificationDetails>(request);
                    //obj = obj.Select(x =>
                    //{
                    //    x.CreatedById = UserId;
                    //    x.CreatedDate = DateTime.Now;
                    //    x.IsDeleted = false;
                    //    return x;
                    //}).ToList();

                    obj.CreatedById = request.CreatedById;
                    obj.CreatedDate = DateTime.Now;
                    obj.IsDeleted = false;

                    await _dbContext.ItemSpecificationDetails.AddRangeAsync(obj);
                    await _dbContext.SaveChangesAsync();
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Model is invalid";
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

using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Store.Commands.Delete
{
    public class DeleteInventoryItemsTypeCommandHandler : IRequestHandler<DeleteInventoryItemsTypeCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public DeleteInventoryItemsTypeCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(DeleteInventoryItemsTypeCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var deleteItem = await _dbContext.InventoryItemType.FirstOrDefaultAsync(x => x.ItemType == request.ItemType);
                if (deleteItem != null)
                {
                    deleteItem.IsDeleted = true;
                    deleteItem.ModifiedById = request.ModifiedById;
                    deleteItem.ModifiedDate = request.ModifiedDate;
                    await _dbContext.SaveChangesAsync();                    

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = StaticResource.SomethingWrong;
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
                return response;
            }

            return response;
        }
    }
}

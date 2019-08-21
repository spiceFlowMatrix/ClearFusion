using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Store;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Store.Commands.Update
{
    public class EditInventoryItemsCommandHandler : IRequestHandler<EditInventoryItemsCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public EditInventoryItemsCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(EditInventoryItemsCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var editItem = await _dbContext.InventoryItems.FirstOrDefaultAsync(x => x.ItemId == request.ItemId);
                if (editItem != null)
                {
                    editItem.ModifiedDate = request.ModifiedDate;
                    editItem.ModifiedById = request.ModifiedById;
                    editItem.Description = request.Description;
                    editItem.ItemCode = request.ItemCode;
                    editItem.ItemGroupId = request.ItemGroupId;
                    editItem.ItemName = request.ItemName;
                    editItem.ItemType = request.ItemType;
                    editItem.IsDeleted = false;
                    editItem.ItemInventory = request.ItemInventory;

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

using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Store;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Store.Commands.Update
{
    public class EditStoreItemGroupCommandHandler : IRequestHandler<EditStoreItemGroupCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public EditStoreItemGroupCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(EditStoreItemGroupCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                if (request != null)
                {
                    StoreItemGroup storeItemGroup = await _dbContext.StoreItemGroups.FirstOrDefaultAsync(x => x.IsDeleted == false && x.ItemGroupId == request.ItemGroupId);

                    storeItemGroup.ModifiedById = request.ModifiedById;
                    storeItemGroup.ModifiedDate = request.ModifiedDate;
                    storeItemGroup.IsDeleted = false;
                    storeItemGroup.Description = request.Description;
                    storeItemGroup.InventoryId = request.InventoryId;
                    storeItemGroup.ItemGroupCode = request.ItemGroupCode;
                    storeItemGroup.ItemGroupName = request.ItemGroupName;

                    await _dbContext.SaveChangesAsync();
                }

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

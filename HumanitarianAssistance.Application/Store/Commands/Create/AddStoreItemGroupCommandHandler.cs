using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Store;
using HumanitarianAssistance.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Store.Commands.Create
{
    public class AddStoreItemGroupCommandHandler : IRequestHandler<AddStoreItemGroupCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddStoreItemGroupCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(AddStoreItemGroupCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                if (request != null)
                {
                    StoreItemGroup storeItemGroup = new StoreItemGroup();

                    storeItemGroup.CreatedById = request.CreatedById;
                    storeItemGroup.CreatedDate = request.CreatedDate;
                    storeItemGroup.IsDeleted = false;
                    storeItemGroup.Description = request.Description;
                    storeItemGroup.InventoryId = request.InventoryId;
                    storeItemGroup.ItemGroupCode = request.ItemGroupCode;
                    storeItemGroup.ItemGroupName = request.ItemGroupName;

                    await _dbContext.StoreItemGroups.AddAsync(storeItemGroup);
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

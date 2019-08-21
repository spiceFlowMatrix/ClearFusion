using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Domain.Entities.Store;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Store.Commands.Create
{
    public class AddInventoryCommandHandler : IRequestHandler<AddInventoryCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddInventoryCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(AddInventoryCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                if (request != null)
                {
                    if (request.InventoryCreditAccount != request.InventoryDebitAccount)
                    {
                        var inventoryAccount =
                           _dbContext.ChartOfAccountNew.Where(x => x.ChartOfAccountNewId == request.InventoryDebitAccount).ToList();

                        bool inventoryCode = await _dbContext.StoreInventories.AnyAsync(x => x.InventoryCode == request.InventoryCode);

                        if (!inventoryCode)
                        {
                            StoreInventory inventory = _mapper.Map<StoreInventory>(request);
                            inventory.IsDeleted = false;
                            inventory.CreatedDate = DateTime.UtcNow;

                            await _dbContext.StoreInventories.AddAsync(inventory);
                            await _dbContext.SaveChangesAsync();

                            response.StatusCode = StaticResource.successStatusCode;
                            response.Message = "Success";
                        }
                        else
                        {
                            response.StatusCode = StaticResource.failStatusCode;
                            response.Message = StaticResource.InventoryCodeAlreadyExists;
                        }
                    }
                    else
                    {
                        response.StatusCode = StaticResource.failStatusCode;
                        response.Message = StaticResource.AccountCantAddToSameAccount;

                    }
                }
            }
            catch (Exception e)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + e.Message;
                return response;
            }

            return response;
        }
    }
}

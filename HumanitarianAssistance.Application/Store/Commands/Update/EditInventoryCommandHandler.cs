using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Store.Commands.Update
{
    public class EditInventoryCommandHandler : IRequestHandler<EditInventoryCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public EditInventoryCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(EditInventoryCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var edInv = await _dbContext.StoreInventories.FirstOrDefaultAsync(c => c.InventoryId == request.InventoryId);
                var inventoryAccount =
                     _dbContext.ChartOfAccountNew.Where(x =>
                                        x.ChartOfAccountNewId == request.InventoryDebitAccount).ToList();
                if (edInv != null)
                {

                    bool inventoryCode = await _dbContext.StoreInventories.AnyAsync(x => x.InventoryCode == request.InventoryCode && x.InventoryId != request.InventoryId);

                    if (!inventoryCode)
                    {
                        _mapper.Map(request, edInv);

                        edInv.IsDeleted = false;

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

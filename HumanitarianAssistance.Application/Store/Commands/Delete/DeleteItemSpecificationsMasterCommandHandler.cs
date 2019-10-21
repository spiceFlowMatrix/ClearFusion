using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Store;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Store.Commands.Delete
{
    public class DeleteItemSpecificationsMasterCommandHandler : IRequestHandler<DeleteItemSpecificationsMasterCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
      
        public DeleteItemSpecificationsMasterCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle(DeleteItemSpecificationsMasterCommand command, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                if(command.Id != 0)
                {
                    ItemSpecificationMaster model= await _dbContext.ItemSpecificationMaster.FirstOrDefaultAsync(x=> x.IsDeleted== false  && x.ItemSpecificationMasterId == command.Id);
                    
                    if(model == null)
                    {
                        throw new Exception(StaticResource.RecordNotFound);
                    }

                    model.IsDeleted = true;
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
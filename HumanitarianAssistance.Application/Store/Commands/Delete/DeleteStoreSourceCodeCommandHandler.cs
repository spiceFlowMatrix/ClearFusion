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

namespace HumanitarianAssistance.Application.Store.Commands.Delete
{
    public class DeleteStoreSourceCodeCommandHandler : IRequestHandler<DeleteStoreSourceCodeCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public DeleteStoreSourceCodeCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(DeleteStoreSourceCodeCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                //Returning the storeSourceCodeDetail record based on storeSourceCodeId received
                StoreSourceCodeDetail storeSourceCodeDetail = _dbContext.StoreSourceCodeDetail.FirstOrDefault(x => x.SourceCodeId == request.storeSourceCodeId && x.IsDeleted == false);

                //Deleting record
                storeSourceCodeDetail.IsDeleted = true;
                storeSourceCodeDetail.ModifiedDate = request.ModifiedDate;
                storeSourceCodeDetail.ModifiedById =request.ModifiedById;

                //updating to db
                await _dbContext.SaveChangesAsync();

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

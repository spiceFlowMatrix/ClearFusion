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
    public class AddStoreSourceCodeCommandHandler : IRequestHandler<AddStoreSourceCodeCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddStoreSourceCodeCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(AddStoreSourceCodeCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                StoreSourceCodeDetail storeSourceCodeDetail = _dbContext.StoreSourceCodeDetail.FirstOrDefault(x => x.IsDeleted == false && x.Code == request.Code);

                if (storeSourceCodeDetail != null)
                {
                    throw new Exception("Code already present. Please try again!!");
                }

                StoreSourceCodeDetail obj = _mapper.Map<StoreSourceCodeDetail>(request);

                obj.IsDeleted = false;
                obj.CreatedDate = request.CreatedDate;
                obj.CreatedById = request.CreatedById;

                await _dbContext.StoreSourceCodeDetail.AddAsync(obj);
                await _dbContext.SaveChangesAsync();

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

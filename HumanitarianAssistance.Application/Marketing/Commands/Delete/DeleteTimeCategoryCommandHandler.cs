using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Marketing;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Marketing.Commands.Delete
{
        public class DeleteTimeCategoryCommandHandler : IRequestHandler<DeleteTimeCategoryCommand, ApiResponse>
    {
            private HumanitarianAssistanceDbContext _dbContext;
            private IMapper _mapper;
            public DeleteTimeCategoryCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }
            public async Task<ApiResponse> Handle(DeleteTimeCategoryCommand request, CancellationToken cancellationToken)
            {
                ApiResponse response = new ApiResponse(); 
                try
            {
                var timeCategory = await _dbContext.TimeCategories.FirstOrDefaultAsync(x => x.IsDeleted == false && x.TimeCategoryId == request.TimeCategoryId);
                if (timeCategory != null)
                {
                    timeCategory.ModifiedById = request.ModifiedById;
                    timeCategory.ModifiedDate = request.ModifiedDate;
                    timeCategory.IsDeleted = true;
                    await _dbContext.SaveChangesAsync();
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Time Category deleted successfully";
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
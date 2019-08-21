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
        public class DeleteMediaCategoryCommandHandler : IRequestHandler<DeleteMediaCategoryCommand, ApiResponse>
    {
            private HumanitarianAssistanceDbContext _dbContext;
            private IMapper _mapper;
            public DeleteMediaCategoryCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }
            public async Task<ApiResponse> Handle(DeleteMediaCategoryCommand request, CancellationToken cancellationToken)
            {
                ApiResponse response = new ApiResponse();
                try
            {
                var mediaCategory = await _dbContext.MediaCategories.FirstOrDefaultAsync(x => x.IsDeleted == false && x.MediaCategoryId == request.MediaCategoryId);
                if (mediaCategory != null)
                {
                    mediaCategory.ModifiedById = request.ModifiedById;
                    mediaCategory.ModifiedDate = request.ModifiedDate;
                    mediaCategory.IsDeleted = true;
                    await _dbContext.SaveChangesAsync();
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Media Category deleted successfully";
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
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

namespace HumanitarianAssistance.Application.Marketing.Commands.Common
{
        public class AddEditMediaCategoryCommandHandler : IRequestHandler<AddEditMediaCategoryCommand, ApiResponse>
    {
            private HumanitarianAssistanceDbContext _dbContext;
            private IMapper _mapper;
            public AddEditMediaCategoryCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }
            public async Task<ApiResponse> Handle(AddEditMediaCategoryCommand request, CancellationToken cancellationToken)
            {
                ApiResponse response = new ApiResponse();
                try
            {
                if (request.MediaCategoryId == 0 || request.MediaCategoryId == null)
                {
                    MediaCategory obj = new MediaCategory();                    
                    obj.CreatedById = request.CreatedById;
                    obj.CreatedDate = request.CreatedDate;
                    obj.IsDeleted = false;
                    obj.CategoryName = request.CategoryName;
                    _mapper.Map(request, obj);
                    await _dbContext.MediaCategories.AddAsync(obj);
                    await _dbContext.SaveChangesAsync();
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Media Category Added successfully";
                    response.data.mediaCategoryById = obj;
                }
                else
                {
                    MediaCategory obj = await _dbContext.MediaCategories.FirstOrDefaultAsync(x => x.MediaCategoryId == request.MediaCategoryId);
                    obj.ModifiedById = request.ModifiedById;
                    obj.ModifiedDate = request.ModifiedDate;
                    _mapper.Map(request, obj);
                    await _dbContext.SaveChangesAsync();
                    response.data.mediaCategoryById = obj;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Media Category updated successfully";
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
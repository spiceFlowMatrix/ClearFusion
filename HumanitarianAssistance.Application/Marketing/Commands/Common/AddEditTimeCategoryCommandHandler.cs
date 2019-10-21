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
    public class AddEditTimeCategoryCommandHandler : IRequestHandler<AddEditTimeCategoryCommand, ApiResponse>
    {
            private HumanitarianAssistanceDbContext _dbContext;
            private IMapper _mapper;
            public AddEditTimeCategoryCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }
            public async Task<ApiResponse> Handle(AddEditTimeCategoryCommand request, CancellationToken cancellationToken)

        {
                ApiResponse response = new ApiResponse(); 
                try
            {
                if (request.TimeCategoryId == 0 || request.TimeCategoryId == null)
                {
                    TimeCategory obj = new TimeCategory();
                    obj.CreatedById = request.CreatedById;
                    obj.CreatedDate = request.CreatedDate;
                    obj.IsDeleted = false;
                    obj.TimeCategoryName = request.TimeCategoryName;
                    _mapper.Map(request, obj);
                    await _dbContext.TimeCategories.AddAsync(obj);
                    await _dbContext.SaveChangesAsync();
                    response.data.timeCatergoryById = obj;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Time Category added successfully";
                }
                else
                {
                    TimeCategory obj = await _dbContext.TimeCategories.FirstOrDefaultAsync(x => x.TimeCategoryId == request.TimeCategoryId);
                    obj.ModifiedById = request.ModifiedById;
                    obj.ModifiedDate = request.ModifiedDate;
                    _mapper.Map(request, obj);
                    await _dbContext.SaveChangesAsync();
                    response.data.timeCatergoryById = obj;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Time category updated successfully";
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
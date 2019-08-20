using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Marketing.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Marketing;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Marketing.Commands.Common
{
        public class AddEditActivityTypeCommandHandler : IRequestHandler<AddEditActivityTypeCommand, ApiResponse>
        {
            private HumanitarianAssistanceDbContext _dbContext;
            private IMapper _mapper;
            public AddEditActivityTypeCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }
            public async Task<ApiResponse> Handle(AddEditActivityTypeCommand request, CancellationToken cancellationToken)
            {
                ApiResponse response = new ApiResponse();
               try
            {
                if (request.ActivityTypeId == 0 || request.ActivityTypeId == null)
                {
                    ActivityType obj = new ActivityType();                    
                    obj.CreatedById = request.CreatedById;
                    obj.CreatedDate = request.CreatedDate;
                    obj.IsDeleted = false;
                    obj.ActivityName = request.ActivityName;
                    _mapper.Map(request, obj);
                    await _dbContext.ActivityTypes.AddAsync(obj);
                    await _dbContext.SaveChangesAsync();
                    response.data.activityById = obj;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    ActivityType obj = await _dbContext.ActivityTypes.FirstOrDefaultAsync(x => x.ActivityTypeId == request.ActivityTypeId);
                    obj.ModifiedById = request.ModifiedById;
                    obj.ModifiedDate = request.ModifiedDate;
                    _mapper.Map(request, obj);
                    await _dbContext.SaveChangesAsync();
                    response.data.activityById = obj;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
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
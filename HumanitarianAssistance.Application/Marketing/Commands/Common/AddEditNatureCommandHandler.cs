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
    public class AddEditNatureCommandHandler : IRequestHandler<AddEditNatureCommand, ApiResponse>
    {
            private HumanitarianAssistanceDbContext _dbContext;
            private IMapper _mapper;
            public AddEditNatureCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }
            public async Task<ApiResponse> Handle(AddEditNatureCommand request, CancellationToken cancellationToken)
            {
                ApiResponse response = new ApiResponse();
                try
            {
                if (request.NatureId == 0 || request.NatureId == null)
                {
                    Nature obj = new Nature();                    
                    obj.CreatedById = request.CreatedById;
                    obj.CreatedDate = request.CreatedDate;
                    obj.IsDeleted = false;
                    obj.NatureName = request.NatureName;
                    _mapper.Map(request,obj);
                    await _dbContext.Natures.AddAsync(obj);
                    await _dbContext.SaveChangesAsync();
                    response.data.natureById = obj;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Nature added successfully";
                }
                else
                {
                    Nature obj = await _dbContext.Natures.FirstOrDefaultAsync(x => x.NatureId == request.NatureId);
                    obj.ModifiedById = request.ModifiedById;
                    obj.ModifiedDate = request.ModifiedDate;
                    _mapper.Map(request, obj);
                    await _dbContext.SaveChangesAsync();
                    response.data.natureById = obj;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Nature updated successfully";
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
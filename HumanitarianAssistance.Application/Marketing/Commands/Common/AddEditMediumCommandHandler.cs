using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.Marketing;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Marketing.Commands.Common
{
    public class AddEditMediumCommandHandler : IRequestHandler<AddEditMediumCommand, ApiResponse>
    {
            private HumanitarianAssistanceDbContext _dbContext;
            private IMapper _mapper;
            public AddEditMediumCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }
            public async Task<ApiResponse> Handle(AddEditMediumCommand request, CancellationToken cancellationToken)
            {
                ApiResponse response = new ApiResponse();
                try
            {
                if (request.MediumId == 0 || request.MediumId == null)
                {
                    Medium obj = new Medium();                    
                    obj.CreatedById = request.CreatedById;
                    obj.CreatedDate = request.CreatedDate;
                    obj.IsDeleted = false;
                    obj.MediumName = request.MediumName;
                    _mapper.Map(request,obj);
                    await _dbContext.Mediums.AddAsync(obj);
                    await _dbContext.SaveChangesAsync();
                    response.data.mediumById = obj;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Medium added successfully";
                }
                else
                {
                    Medium obj = await _dbContext.Mediums.FirstOrDefaultAsync(x => x.MediumId == request.MediumId);
                    obj.ModifiedById = request.ModifiedById;
                    obj.ModifiedDate = request.ModifiedDate;
                    _mapper.Map(request, obj);
                    await _dbContext.SaveChangesAsync();
                    response.data.mediumById = obj;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Medium updated successfully";
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
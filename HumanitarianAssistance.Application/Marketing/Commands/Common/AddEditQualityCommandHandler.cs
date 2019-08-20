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
        public class AddEditQualityCommandHandler : IRequestHandler<AddEditQualityCommand, ApiResponse>
        {
            private HumanitarianAssistanceDbContext _dbContext;
            private IMapper _mapper;
            public AddEditQualityCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }
            public async Task<ApiResponse> Handle(AddEditQualityCommand request, CancellationToken cancellationToken)
            {
                ApiResponse response = new ApiResponse();
                try
            {
                if (request.QualityId == 0 || request.QualityId == null)
                {
                    Quality obj = new Quality();
                    obj.CreatedById = request.CreatedById;
                    obj.CreatedDate = request.CreatedDate;
                    obj.IsDeleted = false;
                    obj.QualityName = request.QualityName;
                    _mapper.Map(request, obj);
                    await _dbContext.Qualities.AddAsync(obj);
                    await _dbContext.SaveChangesAsync();
                    response.data.qualityById = obj;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Quality added successfully";
                }
                else
                {
                    Quality obj = await _dbContext.Qualities.FirstOrDefaultAsync(x => x.QualityId == request.QualityId);
                    obj.ModifiedById = request.ModifiedById;
                    obj.ModifiedDate = request.ModifiedDate;
                    _mapper.Map(request, obj);
                    await _dbContext.SaveChangesAsync();
                    response.data.qualityById = obj;
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Quality updated successfully";
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
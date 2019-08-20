using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Commands.Create
{
    public class AddAreaDetailCommandHandler: IRequestHandler<AddAreaDetailCommand, ApiResponse>
    {

        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;
        public AddAreaDetailCommandHandler( HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext= dbContext;
            _mapper= mapper;
        }

        public async Task<ApiResponse> Handle(AddAreaDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            long LatestCodeId = 0;
            var code = string.Empty;
            try
            {

                var data = await _dbContext.AreaDetail.FirstOrDefaultAsync(x => x.IsDeleted == false && x.AreaName.Trim().ToLower() == request.AreaName.Trim().ToLower());

                if (data == null)
                {
                    AreaDetail obj = _mapper.Map<AddAreaDetailCommand, AreaDetail>(request);
                    var AreaDetail = await _dbContext.AreaDetail
                                                     .OrderByDescending(x => x.AreaId)
                                                     .FirstOrDefaultAsync(x => x.IsDeleted == false);
                    if (AreaDetail == null)
                    {
                        LatestCodeId = 1;
                        code = ProjectUtility.GenerateCode(LatestCodeId);
                    }
                    else
                    {
                        LatestCodeId = AreaDetail.AreaId + 1;
                        code = ProjectUtility.GenerateCode(LatestCodeId);
                    }
                    obj.AreaCode = code;
                    obj.AreaName = request.AreaName;
                    obj.IsDeleted = false;
                    obj.CreatedById = request.CreatedById;
                    obj.CreatedDate = DateTime.UtcNow;
                    await _dbContext.AreaDetail.AddAsync(obj);
                    await _dbContext.SaveChangesAsync();
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.NameAlreadyExist;
                    response.Message = StaticResource.ListNameAlreadyExist;
                }
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
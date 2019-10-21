using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Commands.Delete
{
    public class DeleteAreaDetailCommandHandler: IRequestHandler<DeleteAreaDetailCommand, ApiResponse>
    {

        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;

        public DeleteAreaDetailCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext= dbContext;
            _mapper= mapper;
        }

        public async Task<ApiResponse> Handle(DeleteAreaDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var AreaInfo = await _dbContext.AreaDetail.FirstOrDefaultAsync(c => c.AreaId == request.AreaId);

                if(AreaInfo != null)
                {
                    AreaInfo.IsDeleted = true;
                    AreaInfo.ModifiedById = request.ModifiedById;
                    AreaInfo.ModifiedDate = request.ModifiedDate;
                    _dbContext.AreaDetail.Update(AreaInfo);
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
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
using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Project.Commands.Delete
{
        public class DeleteLogisticsControlCommandHandler : IRequestHandler<DeleteLogisticsControlCommand, ApiResponse>
        {
            private HumanitarianAssistanceDbContext _dbContext;
            private IMapper _mapper;
            public DeleteLogisticsControlCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }
            public async Task<ApiResponse> Handle(DeleteLogisticsControlCommand request, CancellationToken cancellationToken)
            {
                ApiResponse response = new ApiResponse();
            try
            {
                var logisticsDetail = await _dbContext.ProjectLogisticsControl
                                                .FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == request.id);

                if (logisticsDetail == null)
                {
                    throw new Exception(StaticResource.LogisticsControlNotfound);
                }


                logisticsDetail.ModifiedDate = request.ModifiedDate;
                logisticsDetail.ModifiedById = request.ModifiedById;
                logisticsDetail.IsDeleted = true;

                await _dbContext.SaveChangesAsync();

                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
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

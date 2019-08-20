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


namespace HumanitarianAssistance.Application.Project.Commands.Update
{
    public class EditLogisticsControlCommandHandler : IRequestHandler<EditLogisticsControlCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public EditLogisticsControlCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(EditLogisticsControlCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var logisticsDetail = await _dbContext.ProjectLogisticsControl
                                                .FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == request.Id);

                if (logisticsDetail == null)
                {
                    throw new Exception(StaticResource.LogisticsControlNotfound);
                }


                logisticsDetail.UserID = request.UserId;
                logisticsDetail.RoleId = request.RoleId;

                logisticsDetail.ModifiedDate = request.ModifiedDate;
                logisticsDetail.ModifiedById = request.ModifiedById;
                logisticsDetail.IsDeleted = false;

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

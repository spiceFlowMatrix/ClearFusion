using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Configuration.Commands.Delete
{
    public class DeleteOfficeDetailCommandHandler : IRequestHandler<DeleteOfficeDetailCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public DeleteOfficeDetailCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(DeleteOfficeDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var officeInfo = await _dbContext.OfficeDetail.FirstOrDefaultAsync(c => c.OfficeId == request.OfficeId);

                if (officeInfo != null)
                {
                    officeInfo.IsDeleted = true;
                    officeInfo.ModifiedById = request.ModifiedById;
                    officeInfo.ModifiedDate = request.ModifiedDate;
                    _dbContext.OfficeDetail.Update(officeInfo);
                }

                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
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
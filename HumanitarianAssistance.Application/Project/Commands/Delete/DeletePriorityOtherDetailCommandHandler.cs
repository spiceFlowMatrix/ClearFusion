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

namespace HumanitarianAssistance.Application.Project.Commands.Delete
{
    public class DeletePriorityOtherDetailCommandHandler : IRequestHandler<DeletePriorityOtherDetailCommand, ApiResponse>
    {

        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public DeletePriorityOtherDetailCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(DeletePriorityOtherDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                PriorityOtherDetail priorityInfo = await _dbContext.PriorityOtherDetail.FirstOrDefaultAsync(c => c.PriorityOtherDetailId == request.PriorityOtherDetailId &&
                                                                                                                 c.IsDeleted == false);

                priorityInfo.IsDeleted = true;
                priorityInfo.ModifiedById = request.ModifiedById;
                priorityInfo.ModifiedDate = DateTime.UtcNow;

                await _dbContext.SaveChangesAsync();
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

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
    public class DeleteAgeGroupDetailsCommandHandler : IRequestHandler<DeleteAgeGroupDetailsCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public DeleteAgeGroupDetailsCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(DeleteAgeGroupDetailsCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                CEAgeGroupDetail expertInfo = await _dbContext.CEAgeGroupDetail.FirstOrDefaultAsync(c => c.AgeGroupOtherDetailId == request.AgeGroupOtherDetailId &&
                                                                                                         c.IsDeleted == false);
                if (expertInfo != null)
                {
                    expertInfo.IsDeleted = true;
                    expertInfo.ModifiedById = request.ModifiedById;
                    expertInfo.ModifiedDate = DateTime.UtcNow;
                    await _dbContext.SaveChangesAsync();
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

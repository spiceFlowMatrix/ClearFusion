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
    public class DeleteHiringControlCommandHandler : IRequestHandler<DeleteHiringControlCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public DeleteHiringControlCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(DeleteHiringControlCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var hiringDetail = await _dbContext.ProjectHiringControl
                                                                   .FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == request.id);

                if (hiringDetail == null)
                {
                    throw new Exception(StaticResource.HiringControlNotfound);
                }


                hiringDetail.ModifiedDate = request.ModifiedDate;
                hiringDetail.ModifiedById = request.ModifiedById;
                hiringDetail.IsDeleted = true;

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

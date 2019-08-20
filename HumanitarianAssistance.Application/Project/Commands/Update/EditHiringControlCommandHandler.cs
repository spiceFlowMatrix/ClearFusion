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
        public class EditHiringControlCommandHandler : IRequestHandler<EditHiringControlCommand, ApiResponse>
        {
            private HumanitarianAssistanceDbContext _dbContext;
            private IMapper _mapper;
            public EditHiringControlCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
            {
                _dbContext = dbContext;
                _mapper = mapper;
            }
            public async Task<ApiResponse> Handle(EditHiringControlCommand request, CancellationToken cancellationToken)
            {
                ApiResponse response = new ApiResponse();
            try
            {
                var hiringDetail = await _dbContext.ProjectHiringControl
                                             .FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == request.Id);
                if (hiringDetail == null)
                {
                    throw new Exception(StaticResource.HiringControlNotfound);
                }
                hiringDetail.UserID = request.UserId;
                hiringDetail.RoleId = request.RoleId;

                hiringDetail.ModifiedDate = request.ModifiedDate;
                hiringDetail.ModifiedById = request.ModifiedById;
                hiringDetail.IsDeleted = false;

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

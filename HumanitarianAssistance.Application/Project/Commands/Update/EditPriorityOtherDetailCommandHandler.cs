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
    public class EditPriorityOtherDetailCommandHandler : IRequestHandler<EditPriorityOtherDetailCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public EditPriorityOtherDetailCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(EditPriorityOtherDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                PriorityOtherDetail _detail = await _dbContext.PriorityOtherDetail.FirstOrDefaultAsync(x => x.PriorityOtherDetailId == request.PriorityOtherDetailId &&
                                                                                                            x.IsDeleted == false);
                if (_detail != null)
                {
                    _detail.Name = request.Name;
                    _detail.IsDeleted = false;
                    _detail.ModifiedById = request.ModifiedById;
                    _detail.ModifiedDate = DateTime.UtcNow;
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

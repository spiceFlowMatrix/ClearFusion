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

namespace HumanitarianAssistance.Application.Project.Commands.Common
{
    public class AddEditTargetBeneficiaryCommandHandler : IRequestHandler<AddEditTargetBeneficiaryCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddEditTargetBeneficiaryCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(AddEditTargetBeneficiaryCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            TargetBeneficiaryDetail _detail = new TargetBeneficiaryDetail();
            try
            {
                _detail = await _dbContext.TargetBeneficiaryDetail.FirstOrDefaultAsync(x => x.ProjectId == request.ProjectId &&
                                                                                                    x.TargetId == request.TargetId &&
                                                                                                    x.IsDeleted == false);
                if (_detail == null)
                {
                    _detail = new TargetBeneficiaryDetail
                    {
                        TargetType = request.TargetType,
                        TargetName = request.TargetName,
                        ProjectId = request.ProjectId,
                        IsDeleted = false,
                        CreatedById = request.CreatedById,
                        CreatedDate = DateTime.UtcNow
                    };
                    await  _dbContext.TargetBeneficiaryDetail.AddAsync(_detail);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    _detail.TargetType = request.TargetType;
                    _detail.TargetName = request.TargetName;
                    _detail.ProjectId = request.ProjectId;
                    _detail.IsDeleted = false;
                    _detail.ModifiedById = request.ModifiedById;
                    _detail.ModifiedDate = DateTime.UtcNow;
                    await _dbContext.SaveChangesAsync();

                }
                response.CommonId.LongId = _detail.TargetId;
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

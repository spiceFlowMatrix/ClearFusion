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
    public class AddEditCriteriaEvaluationSubmitCommandHandler : IRequestHandler<AddEditCriteriaEvaluationSubmitCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddEditCriteriaEvaluationSubmitCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(AddEditCriteriaEvaluationSubmitCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            // DbContext db = _uow.GetDbContext();
            try
            {
                ProjectDetail projectDetail = await _dbContext.ProjectDetail
                                                   .FirstOrDefaultAsync(x => x.ProjectId == request.ProjectId &&
                                                                        x.IsDeleted == false);

                if (projectDetail != null)
                {
                    projectDetail.IsCriteriaEvaluationSubmit = request.IsCriteriaEvaluationSubmit;
                    projectDetail.ModifiedDate = DateTime.UtcNow;
                    await _dbContext.SaveChangesAsync();
                    response.StatusCode = StaticResource.successStatusCode;
                    response.CommonId.IsApproved = projectDetail.IsCriteriaEvaluationSubmit.Value;
                    response.Message = "Success";
                }
                else
                {
                    throw new Exception("Project not found");
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

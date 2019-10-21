using AutoMapper;
using HumanitarianAssistance.Application.Configuration.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Configuration.Commands.Common
{

    public class RejectEmployeeEvaluationRequestCommandHandler : IRequestHandler<RejectEmployeeEvaluationRequestCommand, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public RejectEmployeeEvaluationRequestCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(RejectEmployeeEvaluationRequestCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                List<EmployeeAppraisalDetailsModel> lst = new List<EmployeeAppraisalDetailsModel>();
                var emplst = await _dbContext.EmployeeEvaluation.FirstOrDefaultAsync(x => x.EmployeeEvaluationId == request.EmployeeEvaluationId);
                emplst.EvaluationStatus = "reject";
                emplst.ModifiedById = request.ModifiedById;
                emplst.ModifiedDate = request.ModifiedDate;
                await _dbContext.SaveChangesAsync();
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Queries {
    public class GetTechnicalQuestionsByDesignationIdQueryHandler : IRequestHandler<GetTechnicalQuestionsByDesignationIdQuery, ApiResponse> {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public GetTechnicalQuestionsByDesignationIdQueryHandler (HumanitarianAssistanceDbContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle (GetTechnicalQuestionsByDesignationIdQuery request, CancellationToken cancellationToken) {
            ApiResponse response = new ApiResponse ();
            try {
                var technicalQuestionsList = await _dbContext.TechnicalQuestion.Where (x => x.DesignationId == request.DesignationId && x.IsDeleted == false).Select (c => new TechnicalQuestionModel {
                        QuestionId = c.TechnicalQuestionId,
                        Question = c.Question
                }).ToListAsync ();
                response.data.TechnicalQuestionsList = technicalQuestionsList;
                response.StatusCode = 200;
                response.Message = "Success";
            } catch (Exception ex) {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }
    }
}
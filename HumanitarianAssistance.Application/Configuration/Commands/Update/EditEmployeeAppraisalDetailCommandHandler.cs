using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Configuration.Commands.Update
{
    public class EditEmployeeAppraisalDetailCommandHandler: IRequestHandler<EditEmployeeAppraisalDetailCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;
        public EditEmployeeAppraisalDetailCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper= mapper;
        }
        public async Task<ApiResponse> Handle(EditEmployeeAppraisalDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var emp = await _dbContext.EmployeeAppraisalDetails.FirstOrDefaultAsync(x => x.EmployeeAppraisalDetailsId== request.EmployeeAppraisalDetailsId);
                emp.Position = request.Position;
                emp.Department = request.Department;
                emp.DutyStation = request.DutyStation;
                emp.AppraisalPeriod = request.AppraisalPeriod;
                emp.TotalScore = request.TotalScore;
                foreach (var item in request.EmployeeAppraisalQuestionList)
                {
                    var question = await _dbContext.EmployeeAppraisalQuestions.FirstOrDefaultAsync(x => x.EmployeeAppraisalQuestionsId== item.EmployeeAppraisalQuestionsId);
                    question.Score = item.Score;
                    question.Remarks = item.Remarks;
                   _dbContext.EmployeeAppraisalQuestions.Update(question);
                   await _dbContext.SaveChangesAsync();
                }
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
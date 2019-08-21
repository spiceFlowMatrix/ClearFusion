using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Configuration.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Configuration.Queries
{
    public class GetEmployeeAppraisalByIdQueryHandler : IRequestHandler<GetEmployeeAppraisalByIdQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public GetEmployeeAppraisalByIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        public async Task<ApiResponse> Handle(GetEmployeeAppraisalByIdQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                var empAppraisalDetails = await _dbContext.EmployeeAppraisalDetails.Where(x => x.EmployeeId == request.EmployeeId && x.CurrentAppraisalDate.Date.Day == request.CurrentAppraisalDate.Date.Day && x.CurrentAppraisalDate.Date.Month == request.CurrentAppraisalDate.Date.Month && x.CurrentAppraisalDate.Date.Year == request.CurrentAppraisalDate.Date.Year && x.IsDeleted == false && x.AppraisalStatus == true).OrderByDescending(x => x.CurrentAppraisalDate).FirstOrDefaultAsync();
                if (empAppraisalDetails != null)
                {
                    EmployeeAppraisalDetailsModel model = new EmployeeAppraisalDetailsModel();
                    var quesLst = await _dbContext.EmployeeAppraisalQuestions.Include(x => x.AppraisalGeneralQuestions).Where(x => x.EmployeeId == empAppraisalDetails.EmployeeId && x.CurrentAppraisalDate.Date.Day == request.CurrentAppraisalDate.Date.Day && x.CurrentAppraisalDate.Date.Month == request.CurrentAppraisalDate.Date.Month && x.CurrentAppraisalDate.Date.Year == request.CurrentAppraisalDate.Date.Year).ToListAsync();
                    model.EmployeeAppraisalDetailsId = empAppraisalDetails.EmployeeAppraisalDetailsId;
                    model.EmployeeId = empAppraisalDetails.EmployeeId;
                    model.EmployeeCode = empAppraisalDetails.EmployeeCode;
                    model.EmployeeName = empAppraisalDetails.EmployeeName;
                    model.FatherName = empAppraisalDetails.FatherName;
                    model.Position = empAppraisalDetails.Position;
                    model.Department = empAppraisalDetails.Department;
                    model.Qualification = empAppraisalDetails.Qualification;
                    model.DutyStation = empAppraisalDetails.DutyStation;
                    model.RecruitmentDate = empAppraisalDetails.RecruitmentDate;
                    model.AppraisalPeriod = empAppraisalDetails.AppraisalPeriod;
                    model.CurrentAppraisalDate = empAppraisalDetails.CurrentAppraisalDate;
                    model.OfficeId = empAppraisalDetails.OfficeId;
                    model.TotalScore = empAppraisalDetails.TotalScore;
                    model.AppraisalStatus = empAppraisalDetails.AppraisalStatus;
                    foreach (var element in quesLst)
                    {
                        EmployeeAppraisalQuestionModel questions = new EmployeeAppraisalQuestionModel();
                        questions.QuestionEnglish = element.AppraisalGeneralQuestions.Question;
                        questions.QuestionDari = element.AppraisalGeneralQuestions.DariQuestion;
                        questions.SequenceNo = element.AppraisalGeneralQuestions.SequenceNo.Value;
                        questions.AppraisalGeneralQuestionsId = element.AppraisalGeneralQuestionsId;
                        questions.Score = element.Score;
                        questions.Remarks = element.Remarks;
                        model.EmployeeAppraisalQuestionList.Add(questions);
                    }
                    response.data.EmployeeAppraisalDetailsModel = model;
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
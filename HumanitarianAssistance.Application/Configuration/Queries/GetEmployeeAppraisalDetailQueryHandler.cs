using System;
using System.Collections.Generic;
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
    public class GetEmployeeAppraisalDetailQueryHandler : IRequestHandler<GetEmployeeAppraisalDetailQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public GetEmployeeAppraisalDetailQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetEmployeeAppraisalDetailQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            
            try
            {
                List<EmployeeAppraisalDetailsModel> lst = new List<EmployeeAppraisalDetailsModel>();

                var emplst = await _dbContext.EmployeeAppraisalDetails.Where(x => x.OfficeId == request.OfficeId && x.AppraisalStatus == false && x.IsDeleted == false).ToListAsync();
                
                foreach (var item in emplst)
                {
                    EmployeeAppraisalDetailsModel model = new EmployeeAppraisalDetailsModel();
                    var quesLst = await _dbContext.EmployeeAppraisalQuestions.Include(x => x.AppraisalGeneralQuestions).Where(x => x.EmployeeId == item.EmployeeId && x.CurrentAppraisalDate == item.CurrentAppraisalDate).ToListAsync();
                    model.EmployeeAppraisalDetailsId = item.EmployeeAppraisalDetailsId;
                    model.EmployeeId = item.EmployeeId;
                    model.EmployeeCode = item.EmployeeCode;
                    model.EmployeeName = item.EmployeeName;
                    model.FatherName = item.FatherName;
                    model.Position = item.Position;
                    model.Department = item.Department;
                    model.Qualification = item.Qualification;
                    model.DutyStation = item.DutyStation;
                    model.RecruitmentDate = item.RecruitmentDate;
                    model.AppraisalPeriod = item.AppraisalPeriod;
                    model.CurrentAppraisalDate = item.CurrentAppraisalDate;
                    model.OfficeId = item.OfficeId;
                    model.TotalScore = item.TotalScore;
                    model.AppraisalStatus = item.AppraisalStatus;
                    foreach (var element in quesLst)
                    {
                        EmployeeAppraisalQuestionModel questions = new EmployeeAppraisalQuestionModel();
                        questions.QuestionEnglish = element.AppraisalGeneralQuestions.Question;
                        questions.QuestionDari = element.AppraisalGeneralQuestions.DariQuestion;
                        questions.SequenceNo = element.AppraisalGeneralQuestions.SequenceNo.Value;
                        questions.AppraisalGeneralQuestionsId = element.AppraisalGeneralQuestionsId;
                        questions.Score = element.Score;
                        questions.Remarks = element.Remarks;
                        questions.EmployeeAppraisalQuestionsId = element.EmployeeAppraisalQuestionsId;
                        model.EmployeeAppraisalQuestionList.Add(questions);
                    }

                    lst.Add(model);
                }
                response.data.EmployeeAppraisalDetailsModelLst = lst;
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
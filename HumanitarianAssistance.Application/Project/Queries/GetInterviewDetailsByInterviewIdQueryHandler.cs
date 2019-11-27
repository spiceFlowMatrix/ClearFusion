using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Queries {
    public class GetInterviewDetailsByInterviewIdQueryHandler : IRequestHandler<GetInterviewDetailsByInterviewIdQuery, ApiResponse> {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetInterviewDetailsByInterviewIdQueryHandler (HumanitarianAssistanceDbContext dbContext) {
            _dbContext = dbContext;
        }
        public async Task<ApiResponse> Handle (GetInterviewDetailsByInterviewIdQuery request, CancellationToken cancellationToken) {
            ApiResponse response = new ApiResponse ();
            try {

                var interviewDetails = await _dbContext.ProjectInterviewDetails
                    .Include (y => y.RatingBasedCriteriaList)
                    .Include (x => x.InterviewTechnicalQuestionList)
                    .Include (x => x.InterviewLanguagesList)
                    .Include (x => x.InterviewTrainingsList)
                    .Include (y => y.HRJobInterviewersList)
                    .FirstOrDefaultAsync (x => x.IsDeleted == false && x.InterviewId == request.InterviewId);

                List<InterviewerDetailsModel> iobj = new List<InterviewerDetailsModel> ();
                foreach (var item in interviewDetails.HRJobInterviewersList) {
                    var employeeDetails = _dbContext.EmployeeDetail.Where (e => e.EmployeeID == item.EmployeeId)
                        .Select (y => new InterviewerDetailsModel {
                            EmployeeId = y.EmployeeID,
                                EmployeeCode = y.EmployeeCode,
                                EmployeeName = y.EmployeeName,
                        }).FirstOrDefault ();
                    // InterviewerDetailsModel details = new InterviewerDetailsModel () {
                    //     EmployeeId = employeeDetails.EmployeeId,
                    //     EmployeeCode = employeeDetails.EmployeeCode,
                    //     EmployeeName = employeeDetails.EmployeeName,
                    // };
                    iobj.Add (employeeDetails);
                }
                if (interviewDetails != null) {
                    InterviewDetailsModel obj = new InterviewDetailsModel () {

                    Description = interviewDetails.Description,
                    NoticePeriod = interviewDetails.NoticePeriod,
                    AvailableDate = interviewDetails.AvailableDate,
                    WrittenTestMarks = interviewDetails.WrittenTestMarks,
                    CurrentBase = interviewDetails.CurrentBase,
                    CurrentOther = interviewDetails.CurrentOther,
                    ExpectationBase = interviewDetails.ExpectationBase,
                    ExpectationOther = interviewDetails.ExpectationOther,
                    Status = interviewDetails.Status,
                    InterviewQuestionOne = interviewDetails.InterviewQuestionOne,
                    InterviewQuestionTwo = interviewDetails.InterviewQuestionTwo,
                    InterviewQuestionThree = interviewDetails.InterviewQuestionThree,
                    CurrentTransport = interviewDetails.CurrentTransport,
                    CurrentMeal = interviewDetails.CurrentMeal,
                    ExpectationTransport = interviewDetails.ExpectationTransport,
                    ExpectationMeal = interviewDetails.ExpectationMeal,
                    ProfessionalCriteriaMark = interviewDetails.ProfessionalCriteriaMarks,
                    MarksObtain = interviewDetails.MarksObtained,
                    TotalMarksObtain = interviewDetails.TotalMarksObtain,
                    RatingBasedCriteriaList = interviewDetails.RatingBasedCriteriaList.Where (x => x.InterviewId == request.InterviewId && x.IsDeleted == false)
                    .Select (y => new InterviewQuestionDetailsModel {
                    QuestionId = y.QuestionId,
                    Score = y.Score
                    }).ToList (),
                    TechnicalQuestionList = interviewDetails.InterviewTechnicalQuestionList.Where (x => x.InterviewId == request.InterviewId && x.IsDeleted == false)
                    .Select (y => new InterviewQuestionDetailsModel {
                    QuestionId = y.QuestionId,
                    Score = y.Score
                    }).ToList (),
                    LanguageList = interviewDetails.InterviewLanguagesList.Where (x => x.InterviewId == request.InterviewId && x.IsDeleted == false)
                    .Select (y => new LanguageDetailsModel {
                    LanguageName = y.LanguageName,
                    LanguageReading = ((RatingAction) y.Reading).ToString (),
                    LanguageWriting = ((RatingAction) y.Writing).ToString (),
                    LanguageListining = ((RatingAction) y.Listening).ToString (),
                    LanguageSpeaking = ((RatingAction) y.Speaking).ToString ()
                    }).ToList (),
                    TraningList = interviewDetails.InterviewTrainingsList.Where (x => x.InterviewId == request.InterviewId && x.IsDeleted == false)
                    .Select (y => new TraningDetailsModel {
                    TraningType = y.NewTraininigType,
                    TraningName = y.TrainingName,
                    TraningCountryAndCity = y.StudyingCountry,
                    TraningStartDate = y.StartDate.ToString("dd/MM/yyyy"),
                    TraningEndDate = y.EndDate.ToString("dd/MM/yyyy")
                    }).ToList (),
                    InterviewerList = iobj
                    };
                    response.data.InterviewDetails = obj;
                }
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
            } catch (Exception ex) {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
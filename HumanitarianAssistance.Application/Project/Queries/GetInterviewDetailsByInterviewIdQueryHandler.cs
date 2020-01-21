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

                var interviewDetails = await (from pid in _dbContext.ProjectInterviewDetails
                    .Where (x => x.InterviewId == request.InterviewId && x.IsDeleted == false) select new InterviewDetailsModel {
                        Description = pid.Description,
                            NoticePeriod = pid.NoticePeriod,
                            AvailableDate = pid.AvailableDate,
                            WrittenTestMarks = pid.WrittenTestMarks,
                            CurrentBase = pid.CurrentBase,
                            CurrentOther = pid.CurrentOther,
                            ExpectationBase = pid.ExpectationBase,
                            ExpectationOther = pid.ExpectationOther,
                            Status = pid.Status,
                            InterviewQuestionOne = pid.InterviewQuestionOne,
                            InterviewQuestionTwo = pid.InterviewQuestionTwo,
                            InterviewQuestionThree = pid.InterviewQuestionThree,
                            CurrentTransport = pid.CurrentTransport,
                            CurrentMeal = pid.CurrentMeal,
                            ExpectationTransport = pid.ExpectationTransport,
                            ExpectationMeal = pid.ExpectationMeal,
                            ProfessionalCriteriaMark = pid.ProfessionalCriteriaMarks,
                            MarksObtain = pid.MarksObtained,
                            TotalMarksObtain = pid.TotalMarksObtain,
                            RatingBasedCriteriaList = (from rb in _dbContext.RatingBasedCriteria.Where (x => x.InterviewId == pid.InterviewId) select new InterviewQuestionDetailsModel {
                                QuestionId = rb.QuestionId,
                                    Score = rb.Score
                            }).ToList (),
                            TechnicalQuestionList = (from it in _dbContext.InterviewTechnicalQuestion.Where (x => x.InterviewId == pid.InterviewId) select new InterviewQuestionDetailsModel {
                                QuestionId = it.QuestionId,
                                    Score = it.Score
                            }).ToList (),
                            LanguageList = (from ill in _dbContext.InterviewLanguages.Where (x => x.InterviewId == pid.InterviewId) select new LanguageDetailsModel {
                                LanguageName = ill.LanguageName,
                                    LanguageReading = ((RatingAction) ill.Reading).ToString (),
                                    LanguageWriting = ((RatingAction) ill.Writing).ToString (),
                                    LanguageListining = ((RatingAction) ill.Listening).ToString (),
                                    LanguageSpeaking = ((RatingAction) ill.Speaking).ToString ()
                            }).ToList (),
                            TraningList = (from it in _dbContext.InterviewTrainings.Where (x => x.InterviewId == pid.InterviewId) select new TraningDetailsModel {
                                TraningType = it.NewTraininigType,
                                    TraningName = it.TrainingName,
                                    TraningCountryAndCity = it.StudyingCountry,
                                    TraningStartDate = it.StartDate.ToShortDateString(),
                                    TraningEndDate = it.EndDate.ToShortDateString(),
                            }).ToList (),
                            InterviewerList = (from hji in _dbContext.HRJobInterviewers.Where (x => x.InterviewId == pid.InterviewId) 
                            join ed in _dbContext.EmployeeDetail on hji.EmployeeId equals ed.EmployeeID into edl from ed in edl.DefaultIfEmpty () 
                            select new InterviewerDetailsModel {
                                EmployeeId = ed.EmployeeID,
                                    EmployeeCode = ed.EmployeeCode,
                                    EmployeeName = ed.EmployeeName
                            }).ToList (),
                    }).FirstOrDefaultAsync ();
                response.data.InterviewDetails = interviewDetails;
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
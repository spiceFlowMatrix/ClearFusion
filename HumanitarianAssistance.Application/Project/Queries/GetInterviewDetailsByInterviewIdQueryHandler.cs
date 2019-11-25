using System;
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

        // await _dbContext.ProjectIndicatorQuestions
        //                                                                        .Where(x => x.IsDeleted == false && x.ProjectIndicatorId == request.indicatorId)
        //                                                                        .Select(x => new IndicatorQuestions
        //                                                                        {
        //                                                                            QuestionId = x.IndicatorQuestionId,
        //                                                                            QuestionText = x.IndicatorQuestion
        //                                                                        })
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

                //                 var interviewDetails = await (from pid in _dbContext.ProjectInterviewDetails
                //                     .Where (x => x.InterviewId == request.InterviewId && x.IsDeleted == false) 
                //                     join rbc in _dbContext.RatingBasedCriteria 
                //                     on pid.InterviewId equals rbc.InterviewId into pidr from rbc in pidr.DefaultIfEmpty ()

                // //join o in _dbContext.OfficeDetail on cd.OfficeId equals o.OfficeId into od from o in od.DefaultIfEmpty ()

                //                     select new InterviewDetailsModel {
                //                         Description = pid.Description,
                //                             NoticePeriod = pid.NoticePeriod,
                //                             AvailableDate = pid.AvailableDate,
                //                             WrittenTestMarks = pid.WrittenTestMarks,
                //                             CurrentBase = pid.CurrentBase,
                //                             CurrentOther = pid.CurrentOther,
                //                             ExpectationBase = pid.ExpectationBase,
                //                             ExpectationOther = pid.ExpectationOther,
                //                             Status = pid.Status,
                //                             InterviewQuestionOne = pid.InterviewQuestionOne,
                //                             InterviewQuestionTwo = pid.InterviewQuestionTwo,
                //                             InterviewQuestionThree = pid.InterviewQuestionThree,
                //                             CurrentTransport = pid.CurrentTransport,
                //                             CurrentMeal = pid.CurrentMeal,
                //                             ExpectationTransport = pid.ExpectationTransport,
                //                             ExpectationMeal = pid.ExpectationMeal,
                //                             ProfessionalCriteriaMark = pid.ProfessionalCriteriaMarks,
                //                             MarksObtain = pid.MarksObtained,
                //                             TotalMarksObtain = pid.TotalMarksObtain,
                //                             RatingBasedCriteriaList = pid. .Where(z => z.IsDeleted == false).Select(c => new TechnicalQuestionModel
                //                                            {
                //                                                QuestionId = c.TechnicalQuestionId,
                //                                                Question = c.Question

                //                                            })

                //                     }).FirstOrDefaultAsync ();
                response.ResponseData = interviewDetails;
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
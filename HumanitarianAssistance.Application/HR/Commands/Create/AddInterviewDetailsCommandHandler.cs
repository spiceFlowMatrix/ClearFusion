using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class AddInterviewDetailsCommandHandler : IRequestHandler<AddInterviewDetailsCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;

        public AddInterviewDetailsCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(AddInterviewDetailsCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                if (request != null)
                {
                    InterviewDetails obj = _mapper.Map<InterviewDetails>(request);
                    obj.InterviewStatus = null; //Approve - Reject Flag
                    obj.CreatedById = request.CreatedById;
                    obj.CreatedDate = request.CreatedDate ;
                    obj.IsDeleted = false;
                    await _dbContext.InterviewDetails.AddAsync(obj);

                    //Rating Based Criteria
                    foreach (var item in request.RatingBasedCriteriaList)
                    {
                        RatingBasedCriteria ratingobj = new RatingBasedCriteria();
                        ratingobj.InterviewDetailsId = obj.InterviewDetailsId;
                        ratingobj.CriteriaQuestion = item.CriteriaQuestion;
                        ratingobj.Rating = item.Rating;
                        ratingobj.CreatedById = request.CreatedById;
                        ratingobj.CreatedDate = request.CreatedDate;
                        ratingobj.IsDeleted = false;
                        await _dbContext.RatingBasedCriteria.AddAsync(ratingobj);
                    }

                    foreach (var item in request.InterviewLanguageModelList)
                    {
                        InterviewLanguages IL = new InterviewLanguages();
                        IL.InterviewDetailsId = obj.InterviewDetailsId;
                        IL.LanguageName = item.LanguageName;
                        IL.LanguageId = item.LanguageId;
                        IL.Reading = item.Reading;
                        IL.Writing = item.Writing;
                        IL.Listening = item.Listening;
                        IL.Speaking = item.Speaking;
                        IL.CreatedById = request.CreatedById;
                        IL.CreatedDate = request.CreatedDate;
                        IL.IsDeleted = false;
                        await _dbContext.InterviewLanguages.AddAsync(IL);
                    }

                    foreach (var item in request.InterviewTechQuesModelList)
                    {
                        InterviewTechnicalQuestion itq = new InterviewTechnicalQuestion();
                        itq.InterviewDetailsId = obj.InterviewDetailsId;
                        itq.Question = item.Question;
                        itq.Answer = item.Answer;
                        itq.CreatedById = request.CreatedById;
                        itq.CreatedDate = request.CreatedDate;
                        itq.IsDeleted = false;
                        await _dbContext.InterviewTechnicalQuestion.AddAsync(itq);
                    }

                    foreach (var item in request.InterviewTrainingModelList)
                    {
                        InterviewTrainings it = new InterviewTrainings();
                        it.InterviewDetailsId = obj.InterviewDetailsId;
                        it.TraininigType = item.TraininigType;
                        it.TrainingName = item.TrainingName;
                        it.StudyingCountry = item.StudyingCountry;
                        it.StartDate = item.StartDate;
                        it.EndDate = item.EndDate;
                        it.CreatedById = request.CreatedById;
                        it.CreatedDate = request.CreatedDate;
                        it.IsDeleted = false;
                        await _dbContext.InterviewTrainings.AddAsync(it);
                    }

                    foreach (var employeeId in request.Interviewers)
                    {
                        HRJobInterviewers hRJobInterviewers = new HRJobInterviewers();

                        hRJobInterviewers.CreatedDate = request.CreatedDate;
                        hRJobInterviewers.CreatedById = request.CreatedById;
                        hRJobInterviewers.EmployeeId = employeeId.Interviewer;
                        hRJobInterviewers.InterviewDetailsId = obj.InterviewDetailsId;
                        hRJobInterviewers.IsDeleted = false;
                        await _dbContext.HRJobInterviewers.AddAsync(hRJobInterviewers);
                    }

                    await _dbContext.SaveChangesAsync();
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
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

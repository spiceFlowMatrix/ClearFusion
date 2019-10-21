using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Configuration.Commands.Create
{
    public class AddEmployeeAppraisalCommandHandler : IRequestHandler<AddEmployeeAppraisalCommand, ApiResponse>
    {

        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;

        public AddEmployeeAppraisalCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper= mapper;
        }

        public async Task<ApiResponse> Handle(AddEmployeeAppraisalCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            
            try
            {
                //var empAppraisalDetails = await _dbContext.EmployeeAppraisalDetails.Where(x => x.EmployeeId == request.EmployeeId && x.CurrentAppraisalDate.Date.Day == request.CurrentAppraisalDate.Date.Day && x.IsDeleted == false).OrderByDescending(x => x.CurrentAppraisalDate).FirstOrDefaultAsync();
               var empAppraisalDetails = await _dbContext.EmployeeAppraisalDetails.Where(x => x.EmployeeId == request.EmployeeId && x.AppraisalStatus == false && x.IsDeleted == false).OrderByDescending(x => x.CurrentAppraisalDate).FirstOrDefaultAsync();
                if (empAppraisalDetails == null)
                {
                    EmployeeAppraisalDetails obj = _mapper.Map<EmployeeAppraisalDetails>(request);
                    obj.AppraisalStatus = false;
                    obj.CreatedById = request.CreatedById;
                    obj.CreatedDate = DateTime.Now;
                    obj.TotalScore = request.TotalScore;
                    obj.IsDeleted = false;
                    await _dbContext.EmployeeAppraisalDetails.AddAsync(obj);
                    await _dbContext.SaveChangesAsync();

                    List<EmployeeAppraisalQuestions> lst = new List<EmployeeAppraisalQuestions>();

                    foreach (var item in request.EmployeeAppraisalQuestionList)
                    {
                        EmployeeAppraisalQuestions eaq = new EmployeeAppraisalQuestions();
                        eaq.AppraisalGeneralQuestionsId = item.AppraisalGeneralQuestionsId;
                        eaq.Score = item.Score;
                        eaq.Remarks = item.Remarks;
                        eaq.EmployeeId = request.EmployeeId;
                        eaq.CurrentAppraisalDate = request.CurrentAppraisalDate;
                        eaq.CreatedDate = DateTime.Now;
                        eaq.CreatedById = request.CreatedById;
                        lst.Add(eaq);
                    }
                    await _dbContext.EmployeeAppraisalQuestions.AddRangeAsync(lst);
                    await _dbContext.SaveChangesAsync();

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
                }
                else
                {
                    response.StatusCode = StaticResource.failStatusCode;
                    response.Message = "Failed";

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
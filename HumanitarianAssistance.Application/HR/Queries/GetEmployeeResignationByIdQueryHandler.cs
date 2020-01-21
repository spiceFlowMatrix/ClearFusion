using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeeResignationByIdQueryHandler : IRequestHandler<GetEmployeeResignationByIdQuery, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public GetEmployeeResignationByIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(GetEmployeeResignationByIdQuery request, CancellationToken cancellationToken)
        {
            Dictionary<string, object> result= new Dictionary<string, object>();
            try
            {
                var resignationDetail = await _dbContext.EmployeeResignationDetail
                .Where(x=>x.IsDeleted == false && x.EmployeeID == request.EmployeeID)
                .Select(y=> new {
                    ResignDate = y.ResignDate,
                    IsIssueUnresolved = y.IsIssueUnresolved,
                    CommentsIssues = y.CommentsIssues,
                    EmployeeResignationId =y.EmployeeResignationId
                }).FirstOrDefaultAsync(); 
                result.Add("ResignationDetail", resignationDetail);
                var resignationQuestionDetail = await _dbContext.EmployeeResignationQuestionDetail
                            .Include(x=>x.ExitInterviewQuestionsMaster)
                            .Where(x=> x.IsDeleted == false && x.ResignationId == resignationDetail.EmployeeResignationId)
                            .Select(x=>new {
                                QuestionType = x.ExitInterviewQuestionsMaster.QuestionType,
                                QuestionId = x.QuestionId,
                                Answer = x.Answer,
                                QuestionText = x.ExitInterviewQuestionsMaster.QuestionText
                            })
                            .ToListAsync();
                result.Add("ResignationQuestionDetail", resignationQuestionDetail);
            }
            catch (Exception ex) 
            {
                throw ex;
            }

            return result;
        }
    }
}
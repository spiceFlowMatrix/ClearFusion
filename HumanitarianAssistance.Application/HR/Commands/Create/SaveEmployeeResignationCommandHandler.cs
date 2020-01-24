using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class SaveEmployeeResignationCommandHandler : IRequestHandler<SaveEmployeeResignationCommand, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;

        public SaveEmployeeResignationCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<object> Handle(SaveEmployeeResignationCommand request, CancellationToken cancellationToken)
        {
            bool success = false;
            long EmployeeResignationId = 0;
            using (IDbContextTransaction tran = _dbContext.Database.BeginTransaction())
            {
                try
                {
                var resignationDetail = await _dbContext.EmployeeResignationDetail.FirstOrDefaultAsync(x=>x.IsDeleted == false && x.EmployeeID == request.EmployeeID); 
                    if(resignationDetail == null) {
                        EmployeeResignationDetail obj = new EmployeeResignationDetail {
                            ResignDate = request.ResignDate,
                            IsIssueUnresolved = request.IsUnresolvedIssue,
                            CommentsIssues = request.CommentsIssues,
                            EmployeeID = request.EmployeeID,
                            CreatedById = request.CreatedById,
                            CreatedDate = request.CreatedDate
                        };
                        await _dbContext.EmployeeResignationDetail.AddAsync(obj);
                        await _dbContext.SaveChangesAsync();
                        EmployeeResignationId = obj.EmployeeResignationId;
                    } else {
                        resignationDetail.ResignDate = request.ResignDate;
                        resignationDetail.IsIssueUnresolved = request.IsUnresolvedIssue;
                        resignationDetail.CommentsIssues = request.CommentsIssues;
                        resignationDetail.ModifiedById = request.CreatedById;
                        resignationDetail.ModifiedDate = request.CreatedDate;
                        await _dbContext.SaveChangesAsync();
                        EmployeeResignationId = resignationDetail.EmployeeResignationId;
                    }

                    foreach(var item in request.QuestionType1) {
                        var questionDetail =  await _dbContext.EmployeeResignationQuestionDetail
                        .FirstOrDefaultAsync(x=> x.IsDeleted == false && x.ResignationId == EmployeeResignationId && x.QuestionId == item.QuestionId);
                        if ( questionDetail == null) {
                            await _dbContext.EmployeeResignationQuestionDetail.AddAsync(new EmployeeResignationQuestionDetail{
                                QuestionId = item.QuestionId,
                                Answer = item.Answer,
                                ResignationId = EmployeeResignationId,
                                CreatedById = request.CreatedById,
                                CreatedDate = request.CreatedDate
                            });
                        } else {
                            questionDetail.Answer = item.Answer;
                            questionDetail.ModifiedById = request.CreatedById;
                            questionDetail.ModifiedDate = request.CreatedDate;
                        }
                    }

                    foreach(var item in request.QuestionType2) {
                        var questionDetail =  await _dbContext.EmployeeResignationQuestionDetail
                        .FirstOrDefaultAsync(x=> x.IsDeleted == false && x.ResignationId == EmployeeResignationId && x.QuestionId == item.QuestionId);
                        if ( questionDetail == null) {
                            await _dbContext.EmployeeResignationQuestionDetail.AddAsync(new EmployeeResignationQuestionDetail{
                                QuestionId = item.QuestionId,
                                Answer = item.Answer,
                                ResignationId = EmployeeResignationId,
                                CreatedById = request.CreatedById,
                                CreatedDate = request.CreatedDate
                            });
                        } else {
                            questionDetail.Answer = item.Answer;
                            questionDetail.ModifiedById = request.CreatedById;
                            questionDetail.ModifiedDate = request.CreatedDate;
                        }
                    }

                    foreach(var item in request.QuestionType3) {
                        var questionDetail =  await _dbContext.EmployeeResignationQuestionDetail
                        .FirstOrDefaultAsync(x=> x.IsDeleted == false && x.ResignationId == EmployeeResignationId && x.QuestionId == item.QuestionId);
                        if ( questionDetail == null) {
                            await _dbContext.EmployeeResignationQuestionDetail.AddAsync(new EmployeeResignationQuestionDetail{
                                QuestionId = item.QuestionId,
                                Answer = item.Answer,
                                ResignationId = EmployeeResignationId,
                                CreatedById = request.CreatedById,
                                CreatedDate = request.CreatedDate
                            });
                        } else {
                            questionDetail.Answer = item.Answer;
                            questionDetail.ModifiedById = request.CreatedById;
                            questionDetail.ModifiedDate = request.CreatedDate;
                        }
                    }

                    foreach(var item in request.QuestionType4) {
                        var questionDetail =  await _dbContext.EmployeeResignationQuestionDetail
                        .FirstOrDefaultAsync(x=> x.IsDeleted == false && x.ResignationId == EmployeeResignationId && x.QuestionId == item.QuestionId);
                        if ( questionDetail == null) {
                            await _dbContext.EmployeeResignationQuestionDetail.AddAsync(new EmployeeResignationQuestionDetail{
                                QuestionId = item.QuestionId,
                                Answer = item.Answer,
                                ResignationId = EmployeeResignationId,
                                CreatedById = request.CreatedById,
                                CreatedDate = request.CreatedDate
                            });
                        } else {
                            questionDetail.Answer = item.Answer;
                            questionDetail.ModifiedById = request.CreatedById;
                            questionDetail.ModifiedDate = request.CreatedDate;
                        }
                    }

                    foreach(var item in request.QuestionType5) {
                        var questionDetail =  await _dbContext.EmployeeResignationQuestionDetail
                        .FirstOrDefaultAsync(x=> x.IsDeleted == false && x.ResignationId == EmployeeResignationId && x.QuestionId == item.QuestionId);
                        if ( questionDetail == null) {
                            await _dbContext.EmployeeResignationQuestionDetail.AddAsync(new EmployeeResignationQuestionDetail{
                                QuestionId = item.QuestionId,
                                Answer = item.Answer,
                                ResignationId = EmployeeResignationId,
                                CreatedById = request.CreatedById,
                                CreatedDate = request.CreatedDate
                            });
                        } else {
                            questionDetail.Answer = item.Answer;
                            questionDetail.ModifiedById = request.CreatedById;
                            questionDetail.ModifiedDate = request.CreatedDate;
                        }
                    }

                    foreach(var item in request.QuestionType6) {
                        var questionDetail =  await _dbContext.EmployeeResignationQuestionDetail
                        .FirstOrDefaultAsync(x=> x.IsDeleted == false && x.ResignationId == EmployeeResignationId && x.QuestionId == item.QuestionId);
                        if ( questionDetail == null) {
                            await _dbContext.EmployeeResignationQuestionDetail.AddAsync(new EmployeeResignationQuestionDetail{
                                QuestionId = item.QuestionId,
                                Answer = item.Answer,
                                ResignationId = EmployeeResignationId,
                                CreatedById = request.CreatedById,
                                CreatedDate = request.CreatedDate
                            });
                        } else {
                            questionDetail.Answer = item.Answer;
                            questionDetail.ModifiedById = request.CreatedById;
                            questionDetail.ModifiedDate = request.CreatedDate;
                        }
                    }

                    await _dbContext.SaveChangesAsync();
                    tran.Commit();
                    success = true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    success = false;
                    throw ex;
                }
            }
            return success;
        }
    }
}
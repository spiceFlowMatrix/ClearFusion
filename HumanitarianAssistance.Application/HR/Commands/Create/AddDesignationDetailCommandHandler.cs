using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class AddDesignationDetailCommandHandler : IRequestHandler<AddDesignationDetailCommand, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;

        public AddDesignationDetailCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<object> Handle(AddDesignationDetailCommand command, CancellationToken cancellationToken)
        {
            bool success = false;

            using (IDbContextTransaction tran = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    DesignationDetail designation = await _dbContext.DesignationDetail.FirstOrDefaultAsync(x=> x.IsDeleted == false && x.Designation == command.DesignationName.Trim());

                    if(designation != null)
                    {
                        throw new Exception(StaticResource.DesignationNameAlreadyExists);
                    }

                    designation= new DesignationDetail();
                    designation.Designation= command.DesignationName;
                    designation.Description= command.Description;
                    designation.CreatedDate = DateTime.UtcNow;
                    designation.CreatedById= command.CreatedById;
                    designation.IsDeleted = false;

                    await _dbContext.DesignationDetail.AddAsync(designation);
                    await _dbContext.SaveChangesAsync();

                    List<TechnicalQuestion> tQuestions= new List<TechnicalQuestion>();

                    foreach(var item in command.Questions)
                    {
                        TechnicalQuestion question = new TechnicalQuestion()
                        {
                            Question = item.Question,
                            DesignationId= designation.DesignationId,
                            CreatedById= command.CreatedById,
                            CreatedDate= DateTime.UtcNow,
                            IsDeleted = false
                        };

                        tQuestions.Add(question);
                    }

                    await _dbContext.TechnicalQuestion.AddRangeAsync(tQuestions);
                    await _dbContext.SaveChangesAsync();

                    success = true;

                    tran.Commit();
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    throw ex;
                }
            }
            return success;
        }
    }
}
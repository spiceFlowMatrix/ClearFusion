using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Persistence;
using MediatR;
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
                    DesignationDetail designation= new DesignationDetail();
                    designation.Designation= command.DesignationName;
                    designation.Description= command.Description;

                    await _dbContext.DesignationDetail.AddAsync(designation);
                    await _dbContext.SaveChangesAsync();

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
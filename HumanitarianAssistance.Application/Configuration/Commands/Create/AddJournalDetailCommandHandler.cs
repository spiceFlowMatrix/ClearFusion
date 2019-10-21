using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Configuration.Commands.Create
{
    public class AddJournalDetailCommandHandler : IRequestHandler<AddJournalDetailCommand, ApiResponse>
    {

        private readonly HumanitarianAssistanceDbContext _dbContext;
        public AddJournalDetailCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(AddJournalDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                if (!(await _dbContext.JournalDetail.AnyAsync(x => x.JournalName == request.JournalName)))
                {
                    JournalDetail obj = new JournalDetail
                    {
                        JournalName = request.JournalName,
                        CreatedById = request.CreatedById,
                        CreatedDate = DateTime.UtcNow,
                        IsDeleted = false
                    };

                    await _dbContext.JournalDetail.AddAsync(obj);
                    await _dbContext.SaveChangesAsync();

                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = StaticResource.SuccessText;
                }
                else
                {
                    response.StatusCode = StaticResource.MandateNameAlreadyExistCode;
                    response.Message = StaticResource.MandateNameAlreadyExist;
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
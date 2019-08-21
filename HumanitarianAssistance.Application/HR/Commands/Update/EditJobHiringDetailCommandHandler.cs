using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class EditJobHiringDetailCommandHandler : IRequestHandler<EditJobHiringDetailCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;

        public EditJobHiringDetailCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(EditJobHiringDetailCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var jobhiringinfo = await _dbContext.JobHiringDetails.FirstOrDefaultAsync(x => x.JobId == request.JobId);

                Boolean jobCode = await _dbContext.JobHiringDetails.AnyAsync(x => x.JobCode == request.JobCode && x.JobId != jobhiringinfo.JobId);

                if (jobhiringinfo != null)
                {
                    if (!jobCode)
                    {
                        jobhiringinfo.JobCode = request.JobCode;
                        jobhiringinfo.JobDescription = request.JobDescription;
                        jobhiringinfo.Unit = request.Unit;
                        jobhiringinfo.OfficeId = request.OfficeId;
                        jobhiringinfo.GradeId = request.GradeId;
                        jobhiringinfo.IsActive = request.IsActive;
                        jobhiringinfo.ModifiedById = request.ModifiedById;
                        jobhiringinfo.ModifiedDate = request.ModifiedDate;

                        await _dbContext.SaveChangesAsync();

                        response.StatusCode = StaticResource.successStatusCode;
                        response.Message = "Success";
                    }
                    else
                    {
                        response.StatusCode = StaticResource.MandateNameAlreadyExistCode;
                        response.Message = StaticResource.JobCodeExist;
                    }
                }
            }
            catch (Exception ex)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }
    }
}

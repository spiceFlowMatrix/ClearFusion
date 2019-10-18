using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class AddJobHiringDetailsCommandHandler : IRequestHandler<AddJobHiringDetailsCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;

        public AddJobHiringDetailsCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ApiResponse> Handle(AddJobHiringDetailsCommand request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                JobHiringDetails jobHiringDetails = await _dbContext.JobHiringDetails.OrderByDescending(x => x.CreatedDate).FirstOrDefaultAsync(x => x.OfficeId == request.office);
                string JobCode;
                if (jobHiringDetails != null && jobHiringDetails.JobCode != null)
                {
                    //getting the latest Job Code and finding the max number from it
                    int count = Convert.ToInt32(jobHiringDetails.JobCode.Substring(2));

                    JobCode = "JC" + String.Format("{0:D4}", ++count);
                }
                else
                {
                    JobCode = "JC" + String.Format("{0:D4}", 1);
                }

                var jobhiringinfo = await _dbContext.JobHiringDetails.FirstOrDefaultAsync(x => x.JobCode == JobCode && x.IsDeleted == false && x.OfficeId == request.office);
                if (jobhiringinfo == null)
                {
                    JobHiringDetails obj = new JobHiringDetails();
                    obj.IsDeleted = false;
                    obj.CreatedById = request.CreatedById;
                    obj.CreatedDate = request.CreatedDate;
                    obj.GradeId = request.jobGrade;
                    obj.JobCode = JobCode;
                    obj.JobDescription = request.description;
                    obj.IsActive = false;
                    obj.OfficeId = request.office;
                    obj.Unit = request.totalVacancies;
                    obj.CurrencyId=request.payCurrency;
                    obj.ProfessionId=request.position;
                    obj.Rate=request.payRate;
                    await _dbContext.JobHiringDetails.AddAsync(obj);
                    await _dbContext.SaveChangesAsync();
                    response.StatusCode = StaticResource.successStatusCode;
                    response.Message = "Success";
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
                response.Message = StaticResource.SomethingWrong + ex.Message;
            }
            return response;
        }
    }
}
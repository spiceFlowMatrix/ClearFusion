using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetAllJobHiringDetailsQueryHandler : IRequestHandler<GetAllJobHiringDetailsQuery, ApiResponse>
    {       
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllJobHiringDetailsQueryHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(GetAllJobHiringDetailsQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var jobhiringdetailslist = await _dbContext.JobHiringDetails.Where(x => x.IsDeleted == false && x.OfficeId == request.OfficeId && x.IsActive == true).ToListAsync();

                List<JobHiringDetailsModel> JobHiringInfo = new List<JobHiringDetailsModel>();

                foreach (var item in jobhiringdetailslist)
                {
                    JobHiringDetailsModel obj = _mapper.Map<JobHiringDetailsModel>(item);
                    JobHiringInfo.Add(obj);
                }

                response.data.JobHiringDetailsList = JobHiringInfo;
                response.Message = "Success";
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

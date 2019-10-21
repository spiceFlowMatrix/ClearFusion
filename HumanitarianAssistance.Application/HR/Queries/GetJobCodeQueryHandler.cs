using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetJobCodeQueryHandler: IRequestHandler<GetJobCodeQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public GetJobCodeQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetJobCodeQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
                JobHiringDetails jobHiringDetails = await _dbContext.JobHiringDetails.OrderByDescending(x => x.CreatedDate).FirstOrDefaultAsync(x => x.OfficeId == request.OfficeId);

                if (jobHiringDetails != null && jobHiringDetails.JobCode != null)
                {
                    //getting the latest Job Code and finding the max number from it
                    int count = Convert.ToInt32(jobHiringDetails.JobCode.Substring(2));

                    response.data.JobCode = "JC" + String.Format("{0:D4}", ++count);
                }
                else
                {
                    response.data.JobCode = "JC" + String.Format("{0:D4}", 1);
                }

                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";

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
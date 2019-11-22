using System.Threading.Tasks;
using System.Linq;
using HumanitarianAssistance.Common.Helpers;
using System;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Persistence;
using MediatR;
using System.Threading;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Common.Enums;

namespace HumanitarianAssistance.Application.Project.Queries
{
        public class GetInterviewDetailsByInterviewIdQueryHandler : IRequestHandler<GetInterviewDetailsByInterviewIdQuery, ApiResponse>
        {
            private HumanitarianAssistanceDbContext _dbContext;

            public GetInterviewDetailsByInterviewIdQueryHandler(HumanitarianAssistanceDbContext dbContext)
            {
                _dbContext = dbContext;
            }
            public async Task<ApiResponse> Handle(GetInterviewDetailsByInterviewIdQuery request, CancellationToken cancellationToken)
            {
                ApiResponse response = new ApiResponse();
                try
            {
                var interviewDetails = await (from pid in _dbContext.ProjectInterviewDetails
                .Where(x=>x.InterviewId==request.InterviewId && x.IsDeleted ==false)
                join rbc in _dbContext.RatingBasedCriteria 
                on pid.JobId equals p.JobId into pd from p in pd.DefaultIfEmpty () 

                                    )

                response.ResponseData = hiringList;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = StaticResource.SuccessText;
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
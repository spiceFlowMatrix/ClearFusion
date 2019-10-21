using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetAllJobGradeQueryHandler : IRequestHandler<GetAllJobGradeQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetAllJobGradeQueryHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(GetAllJobGradeQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var queryResult = await _dbContext.JobGrade.Where(x => x.IsDeleted == false).ToListAsync();

                List<JobGradeModel> jobGradeDetailsList = queryResult.Select(x => new JobGradeModel
                {
                    GradeId = x.GradeId,
                    GradeName = x.GradeName,

                }).ToList();
                response.StatusCode = StaticResource.successStatusCode;
                response.data.JobGradeList = jobGradeDetailsList;
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

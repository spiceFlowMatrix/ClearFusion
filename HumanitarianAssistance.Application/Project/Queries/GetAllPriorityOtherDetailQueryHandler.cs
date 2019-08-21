using AutoMapper;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using HumanitarianAssistance.Common.Helpers;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetAllPriorityOtherDetailQueryHandler : IRequestHandler<GetAllPriorityOtherDetailQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public GetAllPriorityOtherDetailQueryHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ApiResponse> Handle(GetAllPriorityOtherDetailQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();


            try
            {
                var list = await _dbContext.PriorityOtherDetail.Where(x => x.IsDeleted == false
                                                                               ).OrderByDescending(x => x.PriorityOtherDetailId)
                                                                                .ToListAsync();

                response.data.PriorityOtherDetail = list;
                response.StatusCode = StaticResource.successStatusCode;
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
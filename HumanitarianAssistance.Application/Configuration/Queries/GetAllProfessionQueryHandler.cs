using HumanitarianAssistance.Application.Configuration.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.Project;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Configuration.Queries
{
    public class GetAllProfessionQueryHandler : IRequestHandler<GetAllProfessionQuery, ApiResponse>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public GetAllProfessionQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllProfessionQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            try
            {
                var professionlist = await (from c in _dbContext.ProfessionDetails
                                            where c.IsDeleted == false
                                            select new ProfessionModel
                                            {
                                                ProfessionId = c.ProfessionId,
                                                ProfessionName = c.ProfessionName
                                            }).ToListAsync();
                response.data.ProfessionList = professionlist.OrderBy(x => x.ProfessionId).ToList();
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
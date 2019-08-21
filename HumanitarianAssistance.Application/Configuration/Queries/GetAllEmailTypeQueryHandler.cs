using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Persistence;
using MediatR;
using System.Linq;
using HumanitarianAssistance.Common.Helpers;
using System;
using HumanitarianAssistance.Application.Configuration.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace HumanitarianAssistance.Application.Configuration.Queries
{
    public class GetAllEmailTypeQueryHandler : IRequestHandler<GetAllEmailTypeQuery, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public GetAllEmailTypeQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApiResponse> Handle(GetAllEmailTypeQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();

            try
            {
			    var emailtypelist = await (from e in _dbContext.EmailType
                                        where e.IsDeleted == false
                                        select new EmailTypeModel
                                        {
                                            EmailTypeId = e.EmailTypeId,
                                            EmailTypeName = e.EmailTypeName
                                        }).ToListAsync();
                                        
                response.data.EmailTypeList = emailtypelist;
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
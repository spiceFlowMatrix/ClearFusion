using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Queries;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class CheckDefaultUnitTypeQueryHandler : IRequestHandler<CheckDefaultUnitTypeQuery, ApiResponse>
    {
         private readonly HumanitarianAssistanceDbContext _dbContext;
         public CheckDefaultUnitTypeQueryHandler(HumanitarianAssistanceDbContext dbContext)
         {
            _dbContext= dbContext;
         }

        public async Task<ApiResponse> Handle(CheckDefaultUnitTypeQuery request, CancellationToken cancellationToken)
        {
            ApiResponse response = new ApiResponse();
            var unitTypeExist = false;
            try
            {
                var unitType = await _dbContext.PurchaseUnitType.FirstOrDefaultAsync(x=> x.IsDeleted== false && x.IsDefault == true);
                if(unitType== null) {
                    unitTypeExist = false;
                } 
                else
                {
                    unitTypeExist = true;
                }
                response.ResponseData = unitTypeExist;
                response.StatusCode = StaticResource.successStatusCode;
                response.Message = "Success";
            }
            catch(Exception exception)
            {
                response.StatusCode = StaticResource.failStatusCode;
                response.Message = exception.Message;
            }

            return response;
        }
    }
}

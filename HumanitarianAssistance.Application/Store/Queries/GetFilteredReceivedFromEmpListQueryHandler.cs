using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Store.Queries
{
    public class GetFilteredReceivedFromEmpListQueryHandler : IRequestHandler<GetFilteredReceivedFromEmpListQuery, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public GetFilteredReceivedFromEmpListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<object> Handle(GetFilteredReceivedFromEmpListQuery request, CancellationToken cancellationToken)
        {
              Dictionary<string, object> response = new Dictionary<string, object>();

            try
            {

                var list = await _dbContext.EmployeeDetail     
                                 .Where(x => x.IsDeleted == false &&  x.EmployeeTypeId == (int)EmployeeTypeStatus.Active &&  x.EmployeeProfessionalDetail.OfficeId == request.OfficeId && 
                                     (x.EmployeeCode.ToLower().Contains(request.FilterValue.ToLower()) ||
                                     x.EmployeeName.ToLower().Contains(request.FilterValue.ToLower()))).ToListAsync();

                response.Add("EmployeeList", list);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }
    }
}
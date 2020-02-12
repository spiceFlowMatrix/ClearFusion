using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetFilteredEmployeeeListQueryHandler : IRequestHandler<GetFilteredEmployeeeListQuery, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        public GetFilteredEmployeeeListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(GetFilteredEmployeeeListQuery request, CancellationToken cancellationToken)
        {
            Dictionary<string, object> response = new Dictionary<string, object>();
            try
            {
                var employeeList = await _dbContext.EmployeeDetail
                                                    .Include(x => x.EmployeeProfessionalDetail)
                                                    .ThenInclude(x=>x.DesignationDetails)
                                                    .Where(x => x.IsDeleted == false &&
                                                                              x.EmployeeName.ToLower()
                                                                              .Contains(request.EmployeeName)).
                                                                              Select(y => new
                                                                              {
                                                                                  EmployeeId = y.EmployeeID,
                                                                                  EmployeeName = y.EmployeeName,
                                                                                  EmployeeCode = y.EmployeeCode,
                                                                                  Type = y.EmployeeProfessionalDetail.DesignationDetails.Designation
                                                                              })
                                                                        .ToListAsync();
                response.Add("EmployeeList", employeeList);

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return response;
        }
    }
}
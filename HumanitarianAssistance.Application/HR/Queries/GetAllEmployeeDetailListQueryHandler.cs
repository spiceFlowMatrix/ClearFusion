using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetAllEmployeeDetailListQueryHandler : IRequestHandler<GetAllEmployeeDetailListQuery, object>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public GetAllEmployeeDetailListQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(GetAllEmployeeDetailListQuery request, CancellationToken cancellationToken)
        {
            Dictionary<string, object> result= new Dictionary<string, object>();
            var query = _dbContext.EmployeeDetail
                    .Include(e => e.EmployeeProfessionalDetail)
                    .ThenInclude(p => p.professionDetails)
                    .Where(x => x.EmployeeProfessionalDetail.OfficeId == request.OfficeId && x.IsDeleted == false)
                    .Select(x => new {
                        EmployeeTypeId = x.EmployeeTypeId,
                        EmployeeID = x.EmployeeID,
                        EmployeeCode = x.EmployeeCode,
                        FirstName = x.EmployeeName.Trim().Substring(0, ((x.EmployeeName.IndexOf(' ') != -1) ? x.EmployeeName.IndexOf(' ') : x.EmployeeName.Length-1)),
                        LastName= (x.EmployeeName.IndexOf(' ') != -1) ? (x.EmployeeName.Trim().Substring(x.EmployeeName.IndexOf(' '), x.EmployeeName.Length-1)) : "",
                        Profession = x.EmployeeProfessionalDetail.professionDetails.ProfessionName,
                        SexId = x.SexId,
                        SexName = x.SexId == (int)Gender.MALE ? "Male" : x.SexId == (int)Gender.FEMALE ? "Female" : x.SexId == (int)Gender.OTHER ? "Other" : null,
                     });
            if(request.EmploymentStatusFilter != 0)
            {
                query = query.Where(x=> x.EmployeeTypeId == request.EmploymentStatusFilter);
            }

            if(!string.IsNullOrEmpty(request.FirstNameFilter))
            {
                query = query.Where(x=> x.FirstName.Contains(request.FirstNameFilter));
            }

            if(!string.IsNullOrEmpty(request.LastNameFilter))
            {
                query = query.Where(x=> x.LastName.Contains(request.LastNameFilter));
            }

            if(request.GenderFilter != 0)
            {
                query = query.Where(x=> x.SexId == request.GenderFilter);
            }

            if(!string.IsNullOrEmpty(request.EmployeeIdFilter))
            {
                query = query.Where(x=> x.EmployeeCode.Contains(request.EmployeeIdFilter));
            }

            long count = await query.CountAsync();
            var queryResult= await query.Skip(request.PageSize * request.PageIndex).Take(request.PageSize).ToListAsync();
            result.Add("RecordCount", count);
            result.Add("EmployeeList", queryResult);
            
            return result;
        }
    }
}
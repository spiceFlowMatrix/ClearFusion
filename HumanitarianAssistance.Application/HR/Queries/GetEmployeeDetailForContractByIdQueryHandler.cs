using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Enums;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries {
    public class GetEmployeeDetailForContractByIdQueryHandler : IRequestHandler<GetEmployeeDetailForContractByIdQuery, object> {
        private readonly HumanitarianAssistanceDbContext _dbContext;

        public GetEmployeeDetailForContractByIdQueryHandler (HumanitarianAssistanceDbContext dbContext, IMapper mapper) {
            _dbContext = dbContext;
        }

        public async Task<object> Handle (GetEmployeeDetailForContractByIdQuery request, CancellationToken cancellationToken) {
            Dictionary<string, object> response = new Dictionary<string, object> ();

            try {
                var result = await _dbContext.EmployeeDetail
                    .Include (x => x.EmployeeProfessionalDetail)
                    .Include (x => x.CountryDetails)
                    .Include (x => x.ProvinceDetails)
                    .Where (x => x.IsDeleted == false &&
                        x.EmployeeID == request.EmployeeId)
                    .Select (x => new {
                            FirstName = x.EmployeeName.Trim ().Substring (0, ((x.EmployeeName.IndexOf (' ') != -1) ? x.EmployeeName.IndexOf (' ') : x.EmployeeName.Length - 1)),
                            LastName = (x.EmployeeName.IndexOf (' ') != -1) ? (x.EmployeeName.Trim ().Substring (x.EmployeeName.IndexOf (' '), x.EmployeeName.Length - 1)) : "",                           
                            EmployeeCode = x.EmployeeCode,
                            Country = x.CountryId,
                            Province = x.ProvinceId,
                            DutyStation = x.EmployeeProfessionalDetail.DutyStation,
                            Designation = x.EmployeeProfessionalDetail.DesignationId                 
                    }).FirstOrDefaultAsync ();

                if (result == null) {
                    throw new Exception (StaticResource.RecordNotFound);
                }

                response.Add ("EmployeeDetail", result);
            } catch (Exception ex) {
                throw ex;
            }

            return response;
        }
    }
}
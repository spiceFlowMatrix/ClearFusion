using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Configuration.Models;
using HumanitarianAssistance.Application.HR.Models;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetAllEducationDegreeQueryHandler : IRequestHandler<GetAllEducationDegreeQuery, object>
    {

        private readonly HumanitarianAssistanceDbContext _dbContext;

        public GetAllEducationDegreeQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<object> Handle(GetAllEducationDegreeQuery request, CancellationToken cancellationToken)
        {
           EducationDegreeResponseModel response = new EducationDegreeResponseModel();

           try
           {
               var query = _dbContext.EducationDegreeMaster.Where(x=> x.IsDeleted == false).AsQueryable();
               response.TotalCount = await query.CountAsync();
               response.EducationDegreeList = await query.Skip(request.PageIndex * request.PageSize)
                                                         .Take(request.PageSize)
                                                         .Select(x=> new EducationDegreeModel {
                                                             EducationDegreeId= x.Id,
                                                             EducationDegreeName= x.Name
                                                         }).ToListAsync();

           }
           catch(Exception ex)
           {
               throw ex;
           }

           return response;
        }
    }
}
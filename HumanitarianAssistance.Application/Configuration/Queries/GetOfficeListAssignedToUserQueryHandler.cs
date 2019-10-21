using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Configuration.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Configuration.Queries
{
    public class GetOfficeListAssignedToUserQueryHandler : IRequestHandler<GetOfficeListAssignedToUserQuery, List<OfficeDetailModel>>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        
        public GetOfficeListAssignedToUserQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<OfficeDetailModel>> Handle(GetOfficeListAssignedToUserQuery request, CancellationToken cancellationToken)
        {
            List<OfficeDetailModel> model;

            try
            {
                var user= await _dbContext.UserDetails.FirstOrDefaultAsync(x=> x.IsDeleted== false && x.AspNetUserId== request.UserId);

                if(user == null)
                {
                    throw new Exception(StaticResource.RecordNotFound);
                }

                List<int> Offices= await _dbContext.UserDetailOffices.Where(x=> x.IsDeleted== false && x.UserId==user.UserID)
                                                               .Select(x=> x.OfficeId).ToListAsync();

                 model = await _dbContext.OfficeDetail.Where(x => x.IsDeleted == false && Offices.Contains(x.OfficeId))
                                                    .Select(z=> new OfficeDetailModel 
                                                    {
                                                        OfficeId = z.OfficeId,
                                                        OfficeCode = z.OfficeCode,
                                                        OfficeName= z.OfficeName,
                                                    })
                                                    .ToListAsync();
            }
            catch(Exception exception)
            {
                throw exception;
            }

            return model;
        }
    }
}
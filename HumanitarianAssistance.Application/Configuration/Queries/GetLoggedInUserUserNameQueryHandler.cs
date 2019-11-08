using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Configuration.Queries
{
    public class GetLoggedInUserUserNameQueryHandler: IRequestHandler<GetLoggedInUserUserNameQuery, string>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        
        public GetLoggedInUserUserNameQueryHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> Handle(GetLoggedInUserUserNameQuery request, CancellationToken cancellationToken)
        {
            string username= string.Empty;

            try
            {
                var user= await _dbContext.UserDetails.FirstOrDefaultAsync(x=> x.IsDeleted== false && x.AspNetUserId== request.Id);

                if(user != null)
                {
                    username= $"{user.FirstName} {user.LastName}";
                }
            }
            catch(Exception exception)
            {
                throw exception;
            }

            return username;
        }

    }
}
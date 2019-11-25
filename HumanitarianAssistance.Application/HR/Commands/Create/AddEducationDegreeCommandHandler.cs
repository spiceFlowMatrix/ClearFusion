using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class AddEducationDegreeCommandHandler : IRequestHandler<AddEducationDegreeCommand, object>
    {

        private readonly HumanitarianAssistanceDbContext _dbContext;

        public AddEducationDegreeCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<object> Handle(AddEducationDegreeCommand request, CancellationToken cancellationToken)
        {
            bool success = false;

            try
            {
                if(request != null)
                {
                    EducationDegreeMaster degree = await _dbContext.EducationDegreeMaster.FirstOrDefaultAsync(x=> x.IsDeleted == false && x.Name.Trim().ToLower() == request.Name.Trim().ToLower());

                    if(degree != null)
                    {
                        throw new Exception(StaticResource.DegreeNameAlreadyExists);
                    }

                    degree = new EducationDegreeMaster
                    {
                        Name = request.Name,
                        CreatedDate= DateTime.UtcNow,
                        CreatedById= request.CreatedById
                    };

                    await _dbContext.EducationDegreeMaster.AddAsync(degree);
                    await _dbContext.SaveChangesAsync();

                    success= true;
                }
                else
                {
                    throw new Exception(StaticResource.RequestValuesInAppropriate);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
            return success;
        }
    }
}
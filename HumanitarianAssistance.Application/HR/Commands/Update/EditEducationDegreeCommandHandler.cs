using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class EditEducationDegreeCommandHandler : IRequestHandler<EditEducationDegreeCommand, object>
    {

        private readonly HumanitarianAssistanceDbContext _dbContext;
        public EditEducationDegreeCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(EditEducationDegreeCommand request, CancellationToken cancellationToken)
        {
            bool success = false;

            try
            {
                if (request == null)
                {
                    throw new Exception(StaticResource.RequestValuesInAppropriate);
                }

                EducationDegreeMaster degree = await _dbContext.EducationDegreeMaster.FirstOrDefaultAsync(x => x.IsDeleted == false && x.Name.Trim().ToLower() == request.Name.Trim().ToLower());

                if (degree != null)
                {
                    throw new Exception(StaticResource.DegreeNameAlreadyExists);
                }

                degree = await _dbContext.EducationDegreeMaster.FirstOrDefaultAsync(x => x.IsDeleted == false && x.Id == request.Id);

                if (degree == null)
                {
                    throw new Exception(StaticResource.RecordNotFound);
                }

                degree.Name = request.Name;
                degree.ModifiedById = request.ModifiedById;
                degree.ModifiedDate = DateTime.UtcNow;

                _dbContext.EducationDegreeMaster.Update(degree);
                await _dbContext.SaveChangesAsync();
                success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return success;
        }
    }
}
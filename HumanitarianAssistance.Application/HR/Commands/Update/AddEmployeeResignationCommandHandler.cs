using System;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class AddEmployeeResignationCommandHandler : IRequestHandler<AddEmployeeResignationCommand, bool>
    {
        private HumanitarianAssistanceDbContext _dbContext;

        public AddEmployeeResignationCommandHandler(HumanitarianAssistanceDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        public async Task<bool> Handle(AddEmployeeResignationCommand request, CancellationToken cancellationToken)
        {
            bool response = false;
            try
            { 
                var employeeDetail = await _dbContext.EmployeeDetail.FirstOrDefaultAsync(x=>x.IsDeleted== false && x.EmployeeID == request.EmployeeID);
                if(employeeDetail != null) {
                    employeeDetail.IsResigned = true;
                    employeeDetail.ModifiedDate =request.ModifiedDate;
                    employeeDetail.ModifiedById =request.ModifiedById;
                    await _dbContext.SaveChangesAsync();
                    response = true;
                } else {
                    response = false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return response;
        }
    }
}
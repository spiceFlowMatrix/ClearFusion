using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Domain.Entities.HR;
using HumanitarianAssistance.Persistence;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Create
{
    public class AddNewAdvanceRequestCommandHandler: IRequestHandler<AddNewAdvanceRequestCommand, object>
    {
        private HumanitarianAssistanceDbContext _dbContext;
        private IMapper _mapper;
        public AddNewAdvanceRequestCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<object> Handle(AddNewAdvanceRequestCommand request, CancellationToken cancellationToken)
        {
            Dictionary<string, object> response = new Dictionary<string, object>();

            try
            {
                Advances obj = _mapper.Map<Advances>(request);
                obj.ApprovedBy = request.ApprovedByEmployeeId;
                await _dbContext.Advances.AddAsync(obj);
                await _dbContext.SaveChangesAsync();
                response.Add("Success", true);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return response;
        }
    }
}
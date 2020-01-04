using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.Infrastructure;

using HumanitarianAssistance.Persistence;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class AddNewEmployeeCommandHandler : IRequestHandler<AddNewEmployeeCommand, ApiResponse>
    {
        private readonly HumanitarianAssistanceDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IHRService _hrService;
        public AddNewEmployeeCommandHandler(HumanitarianAssistanceDbContext dbContext, IMapper mapper, IHRService hrService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _hrService = hrService;
        }

        public async Task<ApiResponse> Handle(AddNewEmployeeCommand request, CancellationToken cancellationToken)
        {
            return await _hrService.AddNewEmployee(request);
        }
    }
}

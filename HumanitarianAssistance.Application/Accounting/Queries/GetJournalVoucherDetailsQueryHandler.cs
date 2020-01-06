using AutoMapper;
using HumanitarianAssistance.Application.Accounting.Models;
using HumanitarianAssistance.Application.CommonServicesInterface;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using HumanitarianAssistance.Persistence.Extensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetJournalVoucherDetailsQueryHandler : IRequestHandler<GetJournalVoucherDetailsQuery, ApiResponse>
    {
         private readonly IAccountingServices _iAccountingServices;
         private readonly IMapper _mapper;
        public GetJournalVoucherDetailsQueryHandler(IAccountingServices iAccountingServices, IMapper mapper)
        {

            _iAccountingServices= iAccountingServices;
            _mapper= mapper;
        }

        public async Task<ApiResponse> Handle(GetJournalVoucherDetailsQuery request, CancellationToken cancellationToken)
        {
            JournalReportModel model = _mapper.Map<JournalReportModel>(request);
            return await _iAccountingServices.GetJournalReport(model);
        }
    }
}

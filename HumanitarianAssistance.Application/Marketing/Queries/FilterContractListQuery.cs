using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Marketing.Queries
{
    public class FilterContractListQuery : IRequest<ApiResponse>
    {
        public long? ContractId { get; set; }
        public string FilterType { get; set; }
        public string ClientName { get; set; }
        public long? ClientId { get; set; }
        public long? ActivityTypeId { get; set; }
        public long? CurrencyId { get; set; }
        public int? UnitRate { get; set; }
        public bool IsApproved { get; set; }
        public string YesOrNo { get; set; }
    }
}

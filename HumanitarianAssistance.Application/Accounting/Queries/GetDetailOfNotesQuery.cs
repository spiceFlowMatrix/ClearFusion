using System;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Queries
{
    public class GetDetailOfNotesQuery: IRequest<ApiResponse>
    {
        public int CurrencyId { get; set; }
        public DateTime TillDate { get; set; }
    }
}
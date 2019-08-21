using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Marketing.Queries
{
    public class FilterClientListQuery : IRequest<ApiResponse>
    {
        public long? ClientId { get; set; }
        public string ClientName { get; set; }
        public string Email { get; set; }
        public long? CategoryId { get; set; }
        public string Position { get; set; }
    }
}

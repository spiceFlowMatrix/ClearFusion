using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Marketing.Models
{
    public class ContractByClient
    {
        public long? ContractId { get; set; }
        public long? ClientId { get; set; }
        public string ClientName { get; set; }
        public string ContractByClients { get; set; }
        public double UnitRate { get; set; }
        public int? CurrencyId { get; set; }
    }
}

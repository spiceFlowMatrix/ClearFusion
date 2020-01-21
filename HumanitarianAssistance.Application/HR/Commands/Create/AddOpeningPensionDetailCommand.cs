using System;
using System.Collections.Generic;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class AddOpeningPensionDetailCommand : BaseModel, IRequest<bool>
    {
        public int EmployeeID { get; set; }
        public DateTime PensionDate { get; set; }
        public List<OpeningPensionDetail> PensionDetail { get; set; }

    }
    public class OpeningPensionDetail
    {
        public int CurrencyId { get; set; }
        public double Amount { get; set; }
    }
}
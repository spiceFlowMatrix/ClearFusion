using System;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class EditAdvanceRequestCommand: BaseModel, IRequest<object>
    {
        public long AdvanceId { get; set; }
        public DateTime AdvanceDate { get; set; }
        public int ApprovedBy { get; set; }
        public int NumberOfInstallments { get; set; }
        public string ModeOfReturn { get; set; }
        public double RequestAmount { get; set; }
        public string Description { get; set; }
    }
}
using System;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Create
{
    public class AddNewAdvanceRequestCommand: BaseModel, IRequest<object>
    {
        public int EmployeeId { get; set; }
        public DateTime AdvanceDate { get; set; }
        public int ApprovedByEmployeeId { get; set; }
        public int NumberOfInstallments { get; set; }
        public string ModeOfReturn { get; set; }
        public double RequestAmount { get; set; }
        public string Description { get; set; }
    }
}
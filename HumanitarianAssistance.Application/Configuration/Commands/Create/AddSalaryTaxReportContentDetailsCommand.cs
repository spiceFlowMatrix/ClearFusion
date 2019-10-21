using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Configuration.Commands.Create
{
    public class AddSalaryTaxReportContentDetailsCommand : BaseModel, IRequest<ApiResponse>
    {
        public int SalaryTaxReportContentId { get; set; }
        public string EmployerAuthorizedOfficerName { get; set; }
        public string PositionAuthorizedOfficer { get; set; }
        public int OfficeId { get; set; }
    }
}

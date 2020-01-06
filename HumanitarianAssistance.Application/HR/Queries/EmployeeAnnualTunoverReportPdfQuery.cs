using MediatR;

namespace HumanitarianAssistance.Application.HR.Queries {
    public class EmployeeAnnualTunoverReportPdfQuery : IRequest<byte[]> {
        public int OfficeId { get; set;}
    }
}
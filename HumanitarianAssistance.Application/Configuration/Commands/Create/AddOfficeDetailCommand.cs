using System.Collections.Generic;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Configuration.Commands.Create
{
    public class AddOfficeDetailCommand: BaseModel, IRequest<ApiResponse>
    {
        public int OfficeId { get; set; }
        public string OfficeCode { get; set; }
        public string OfficeName { get; set; }
        public string SupervisorName { get; set; }
        public string PhoneNo { get; set; }
        public string FaxNo { get; set; }
        public string OfficeKey { get; set; }
        public List<DepartmentDetails> Department { get; set; }
    }
    public class DepartmentDetails {
        public int? DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }
}
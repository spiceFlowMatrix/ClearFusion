using System.Collections.Generic;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class GetEmployeesPayrollExcelQuery: IRequest<object>
    {
       // public DateTime StartDate { get; set; }
       // public DateTime EndDate { get; set; }
       public List<int?> Month { get; set; }
        public List<int> OfficeId { get; set; }
        public string Project { get; set; }
        public List<int?> SelectedEmployees {get; set;}
        public string EmployeeName { get; set; }
        public string EmployeeCode{ get; set; }
        public int? Sex{ get; set; }
        public int? EmploymentStatus{ get; set; }
    }
}
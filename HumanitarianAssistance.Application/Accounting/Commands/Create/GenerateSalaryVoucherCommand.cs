using System;
using System.Collections.Generic;
using HumanitarianAssistance.Application.Accounting.Models;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Accounting.Commands.Create
{
    public class GenerateSalaryVoucherCommand : BaseModel, IRequest<ApiResponse>
    {
        public List<SalaryHeadModel> EmployeePayrollLists { get; set; }
        public List<PayrollHeadModel> EmployeePayrollListPrimary { get; set; }
        public int OfficeId { get; set; }
        public int EmployeeId { get; set; }
        public int CurrencyId { get; set; }
        public int JournalCode { get; set; }
        public int PresentHours { get; set; }
        public DateTime PayrollMonth { get; set; }
    }
}
using System.IO;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class ExcelImportOfBudgetLineQuery: IRequest<ApiResponse>
    {
        public Stream File { get; set; }
        public long ProjectId { get; set; }
        public string UserId { get; set; }
    }
}
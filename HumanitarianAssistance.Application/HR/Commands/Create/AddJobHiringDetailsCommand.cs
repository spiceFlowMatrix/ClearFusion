using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Create
{
    public class AddJobHiringDetailsCommand : BaseModel, IRequest<ApiResponse>
    {
        public string description { get; set; }
        public int office { get; set; }
        public int position { get; set; }
        public int jobGrade { get; set; }
        public int totalVacancies { get; set; }
        public int payCurrency { get; set; }
        public double payRate { get; set; } 
        public long projectId { get; set; }
    }
}
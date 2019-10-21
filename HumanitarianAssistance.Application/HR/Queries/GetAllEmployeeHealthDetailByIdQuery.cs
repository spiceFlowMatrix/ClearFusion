using HumanitarianAssistance.Application.Infrastructure;
using MediatR;


namespace HumanitarianAssistance.Application.HR.Queries
{
  public  class GetAllEmployeeHealthDetailByIdQuery : BaseModel, IRequest<ApiResponse>
    {
        public int EmployeeId { get; set; }
    }
}

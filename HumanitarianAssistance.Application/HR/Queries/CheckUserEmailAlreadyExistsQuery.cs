using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Queries
{
    public class CheckUserEmailAlreadyExistsQuery: IRequest<bool>
    {
        public string Email { get; set; }
    }
}
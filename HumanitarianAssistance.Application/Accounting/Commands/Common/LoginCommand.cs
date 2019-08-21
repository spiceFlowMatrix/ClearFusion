using MediatR;
using HumanitarianAssistance.Application.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace HumanitarianAssistance.Application.Accounting.Commands.Common
{
    public class LoginCommand : IRequest<ApiResponse>
    {
        public LoginCommand()
        {
            UserName = null;
            Password = null;
        }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
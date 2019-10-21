using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Marketing.Commands.Create
{
    public class AddLanguageCommand: BaseModel,IRequest<ApiResponse>
    {
        public long? LanguageId { get; set; }
        public string LanguageName { get; set; }
    }
}
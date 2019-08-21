using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Marketing.Commands.Update
{
    public class EditLanguageCommand : BaseModel,IRequest<ApiResponse>
    {
        public long? LanguageId { get; set; }
        public string LanguageName { get; set; }
    }
}
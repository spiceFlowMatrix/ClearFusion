using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Marketing.Commands.Delete
{
    public class DeleteLanguageCommand : BaseModel,IRequest<ApiResponse>
    {
        public int LanguageId { get; set; }
        public string LanguageName { get; set; }
    }
}
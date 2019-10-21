using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.HR.Commands.Update
{
    public class EditEmployeeLanguagesCommand : BaseModel, IRequest<ApiResponse>
    {
        public int SpeakLanguageId { get; set; }
        //public int LanguageName { get; set; }
        public int LanguageId { get; set; }
        public int Reading { get; set; }
        public int Writing { get; set; }
        public int Speaking { get; set; }
        public int Listening { get; set; }
        public int EmployeeId { get; set; }
    }
}

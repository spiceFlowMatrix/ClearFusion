using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Marketing.Commands.Common
{
    public class AddEditPolicyCommand : BaseModel, IRequest<ApiResponse>
    {
        public long PolicyId { get; set; }
        public string PolicyName { get; set; }
        public string PolicyCode { get; set; }
        public long? LanguageId { get; set; }
        public string LanguageName { get; set; }
        public long? ProducerId { get; set; }
        public string ProducerName { get; set; }
        public long? MediumId { get; set; }
        public string MediumName { get; set; }
        public long? MediaCategoryId { get; set; }
        public string MediaCategoryName { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public string Description { get; set; }
    }
}

using System;
using System.Collections.Generic;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Queries
{
    public class GetComparativeStatementQuery  : BaseModel, IRequest<ApiResponse>
    {       
        public long requestId { get; set; }
    }

    public class ComparativeStatementModel {
        public string Description { get; set; }
        public string SubmittedBy { get; set; }
        public List<SupplierDetailModel> selectedSupplier { get; set; }
        public List<StatementAttachmentModel> attachments { get; set; }
    }

    public class SupplierDetailModel {
        public long Id { get; set; }
        public string SupplierName { get; set; }
    }

    public class StatementAttachmentModel {
        public string AttachmentName { get; set; }
        public string AttachmentUrl { get; set; }
    }
}
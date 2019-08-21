using HumanitarianAssistance.Application.Infrastructure;
using MediatR;
using System;

namespace HumanitarianAssistance.Application.Project.Commands.Create
{
    public class AddApprovalDetailCommand : BaseModel, IRequest<ApiResponse>
    {
        public long ApproveProjrctId { get; set; }
        public long ProjectId { get; set; }
        public string CommentText { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public bool IsApproved { get; set; }
        public string UploadedFile { get; set; }
        public DateTime? ReviewCompletionDate { get; set; }
    }
}

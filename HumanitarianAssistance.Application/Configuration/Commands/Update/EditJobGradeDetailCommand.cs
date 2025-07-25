﻿using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Configuration.Commands.Update {
    public class EditJobGradeDetailCommand : BaseModel, IRequest<ApiResponse> {
        public int GradeId { get; set; }
        public string GradeName { get; set; }
        public long? ChartOfAccountNewId { get; set; }
    }
}
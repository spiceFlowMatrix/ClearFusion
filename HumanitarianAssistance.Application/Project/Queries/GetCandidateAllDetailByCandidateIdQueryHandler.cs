using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using HumanitarianAssistance.Application.Infrastructure;
using HumanitarianAssistance.Application.Project.Models;
using HumanitarianAssistance.Common.Helpers;
using HumanitarianAssistance.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HumanitarianAssistance.Application.Project.Queries {

    public class GetCandidateAllDetailByCandidateIdQueryHandler : IRequestHandler<GetCandidateAllDetailByCandidateIdQuery, CandidateDetailsModel> {
        public async Task<CandidateDetailsModel> Handle (GetCandidateAllDetailByCandidateIdQuery request, CancellationToken cancellationToken) {
            CandidateDetailsModel model;
            try {
                //model = 
            } catch (Exception exception) {
                throw exception;
            }

            return model;
        }
    }
}
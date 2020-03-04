using System;
using HumanitarianAssistance.Application.Infrastructure;
using MediatR;

namespace HumanitarianAssistance.Application.Project.Commands.Create
{
    public class AddTenderBidCommand: BaseModel, IRequest<ApiResponse>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Owner { get; set; }
        public DateTime OpeningDate { get; set; }
        public DateTime TenderDeliveryDate { get; set; }
        public double QuotedAmount { get; set; }
        public double SecurityAmount { get; set; }
        public bool isResultQualified { get; set; }
        // Administrative Requirement Scoring
        public int Profile_Experience { get; set; }
        public int Securities_BankGuarantee { get; set; }
        public int OfferValidity { get; set; }
        public int OfferDocumentation { get; set; }
        public int TOR_SOWAcceptance { get; set; }
        
        // Technical Requirement Scoring
        public int Company_GoodsSpecification { get; set; }
        public int WorkExperience { get; set; }
        public int Service_Warranty { get; set; }
        public int DeliveryDateScore { get; set; }
        public int Certification_GMP_COPP { get; set; }
        public long LogisticRequestsId { get; set; }
    }
}
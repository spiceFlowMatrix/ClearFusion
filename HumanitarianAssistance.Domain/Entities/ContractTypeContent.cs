using System;
using System.Collections.Generic;
using System.Text;
using HumanitarianAssistance.Domain.Entities.HR;

namespace HumanitarianAssistance.Domain.Entities
{
    public class ContractTypeContent : BaseEntity
    {
        public int ContractTypeContentId { get; set; }
        public int EmployeeContractTypeId { get; set; }
        public EmployeeContractType EmployeeContractType { get; set; }
        public string ContentEnglish { get; set; }
        public string ContentDari { get; set; }
        public int OfficeId { get; set; }
    }
}

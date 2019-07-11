using Athena.Domain.Entities.AdminEntity.Tenant;
using Athena.Domain.Entities.Metadata;
using Athena.Domain.Enums;
using System;

namespace Athena.Domain.Interfaces
{
    public interface IBaseEntity
    {
        string Id { get; set; }
        string CreatedByUserId { get; set; }
        DomainUser CreatedByUser { get; set; }
        DateTimeOffset CreatedOn { get; set; }
    }
}
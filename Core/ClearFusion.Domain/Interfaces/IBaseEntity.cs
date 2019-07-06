using Athena.Domain.Entities.AdminEntity.Tenant;
using Athena.Domain.Entities.Metadata;
using Athena.Domain.Enums;
using System;

namespace Athena.Domain.Interfaces
{
    public interface IBaseEntity
    {
        DomainEntityTypes DomainEntityType { get; set; }
        string Id { get; set; }
        string MetadataChannelId { get; set; }

        DateTimeOffset CreatedDate { get; set; }
        DateTimeOffset ModifiedDate { get; set; }
        DateTimeOffset DeletedDate { get; set; }

        string CreatedByUserId { get; set; }
        string ModifiedByUserId { get; set; }
        string DeletedByUserId { get; set; }

        bool IsDeleted { get; set; }

        AthenaUser CreatedByUser { get; set; }
        AthenaUser ModifiedByUser { get; set; }
        AthenaUser DeletedByUser { get; set; }
        MetadataChannel MetadataChannel { get; set; }
    }
}
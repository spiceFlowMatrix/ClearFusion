namespace ClearFusion.Domain.Interfaces
{
    public interface IEntityHistory : IBaseEntity
    {
        string ModifiedByUserId { get; set; }
        DomainUser ModifiedByUser { get; set; }
        DateTimeOffset ModifiedOn { get; set; }

        bool IsDeleted {get;set;}
        string DeletedByUserId {get;set;}
        DomainUser DeletedByUser {get;set;}
        DateTimeOffset DeletedOn {get;set;}
    }

}
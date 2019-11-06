namespace ClearFusion.Domain.Entities
{
    public class DomainUser
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string NickName { get; set; }
        public string Email { get; set; }
        public string Picture { get; set; }
        public string PhoneNumber { get; set; }
        public bool EmailVerified { get; set; }
        public bool Blocked { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset ModifiedOn { get; set; }
        public DateTimeOffset VerifiedOn { get; set; }
        public DateTimeOffset BlockedOn { get; set; }
    }
}
using System;

namespace HumanitarianAssistance.Domain.Entities
{
    public class Permissions : BaseEntity
    {
        public Permissions()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

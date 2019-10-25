using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataAccess.DbEntities.Marketing
{
   public class ClientDetails: BaseEntityWithoutId
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1, TypeName = "serial")]
        public long ClientId { get; set; }
        public string ClientCode { get; set; }
        public string ClientName { get; set; }
        public string FocalPoint { get; set; }
        public string Position { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PhysicialAddress { get; set; }
        public string History { get; set; }
        public string ClientBackground { get; set; }
        [ForeignKey("CategoryId")]
        public long? CategoryId { get; set; }
        public Category Categories { get; set; }
    }
}


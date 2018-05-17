using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.DbEntities
{
    public class LoggerDetails:BaseEntityWithoutId
    {
		public int LoggerDetailsId { get; set; }
		public int NotificationId { get; set; }
		public string UserId { get; set; }
		public bool IsRead { get; set; }
	}
}

﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.DbEntities
{
    public class LoggerDetails:BaseEntityWithoutId
    {
		public int LoggerDetailsId { get; set; }
		public int NotificationId { get; set; }	
		public bool IsRead { get; set; }
		public string UserName { get; set; }
		public string LoggedDetail { get; set; }		
	}
}

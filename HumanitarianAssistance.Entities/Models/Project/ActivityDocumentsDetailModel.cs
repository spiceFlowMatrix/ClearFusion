﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Project
{
    public class ActivityDocumentsDetailModel
    {
       
        public long ActtivityDocumentId { get; set; }
        public string ActivityDocumentsFilePath { get; set; }
        public string ActivityDocumentsFileName { get; set; }
        public int? StatusId { get; set; }
        public long ActivityId { get; set; }
      

    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.FileManagement.Models
{
    public class FileListModel
    {
        public string FileName { get; set; }
        public string FileSignedURL { get; set; }
        public string FilePath { get; set; }
        public long? DocumentFileId { get; set; }
        public int? DocumentTypeId { get; set; }
        public string Date {get; set;}
        public string StorageDirectoryPath { get; set; }
    }
}

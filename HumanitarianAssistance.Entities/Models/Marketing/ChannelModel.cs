using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.ViewModels.Models.Marketing
{
    public class ChannelModel
    {
        public long ChannelId { get; set; }
        public string ChannelName { get; set; }
        public long? MediumId { get; set; }
    }
}

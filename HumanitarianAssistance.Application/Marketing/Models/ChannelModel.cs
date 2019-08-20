using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Application.Marketing.Models
{
    public class ChannelModel
    {
        public long ChannelId { get; set; }
        public string ChannelName { get; set; }
        public long? MediumId { get; set; }
        public string MediumName { get; set; }
    }
}

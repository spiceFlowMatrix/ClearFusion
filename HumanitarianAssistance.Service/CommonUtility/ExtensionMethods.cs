using System;
using System.Collections.Generic;
using System.Text;

namespace HumanitarianAssistance.Service.CommonUtility
{
    public static class ExtensionMethods
    {
        public static int RoundOff(this decimal i)
        {
            return ((int)Math.Round((i / 10.0m))) * 10;
        }
    }
}

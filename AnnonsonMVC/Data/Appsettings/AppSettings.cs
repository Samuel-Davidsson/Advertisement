using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Appsettings
{
    public class AppSettings
    {
        public string StripeApiKey { get; set; }
        public string MediaFolder { get; set; }
        public string MediaUrl { get; set; }
        public bool IsTest { get; set; }
    }
}

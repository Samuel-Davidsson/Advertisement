using System;
using System.Collections.Generic;

namespace Domain.Entites
{
    public partial class ApplicationSetting
    {
        public int ApplicationSettingId { get; set; }
        public string VersionMajor { get; set; }
        public string VersionMinor { get; set; }
        public string ImageUrlPath { get; set; }
        public bool MaintenanceMode { get; set; }
    }
}

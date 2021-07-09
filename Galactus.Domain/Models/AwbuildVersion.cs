using System;
using System.Collections.Generic;

#nullable disable

namespace Galactus.Domain.Models
{
    // Current version number of the AdventureWorks 2012 sample database.
    public partial class AwbuildVersion
    {
        public byte SystemInformationId { get; set; }
        public string DatabaseVersion { get; set; }
        public DateTime VersionDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}

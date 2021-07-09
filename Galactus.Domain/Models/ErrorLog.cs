using System;
using System.Collections.Generic;

#nullable disable

namespace Galactus.Domain.Models
{
    // Audit table tracking errors in the the AdventureWorks database that are caught by the CATCH block of a TRY...CATCH construct.
    // Data is inserted by stored procedure dbo.uspLogError when it is executed from inside the CATCH block of a TRY...CATCH construct.
    public partial class ErrorLog
    {
        public int ErrorLogId { get; set; }
        public DateTime ErrorTime { get; set; }
        public string UserName { get; set; }
        public int ErrorNumber { get; set; }
        public int? ErrorSeverity { get; set; }
        public int? ErrorState { get; set; }
        public string ErrorProcedure { get; set; }
        public int? ErrorLine { get; set; }
        public string ErrorMessage { get; set; }
    }
}

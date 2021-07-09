using System;
using System.Collections.Generic;

#nullable disable

namespace Galactus.Domain.Models
{
    // Cross-reference table mapping people to their credit card information in the CreditCard table. 
    public partial class PersonCreditCard
    {
        public int BusinessEntityId { get; set; }
        public int CreditCardId { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual Person BusinessEntity { get; set; }
        public virtual CreditCard CreditCard { get; set; }
    }
}

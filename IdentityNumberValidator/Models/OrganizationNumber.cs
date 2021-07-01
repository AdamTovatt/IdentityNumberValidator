using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityNumberValidator.Models
{
    public class OrganizationNumber : IdentityNumber
    {
        public override ValidationResult Validate()
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace IdentityNumberValidator.Models
{
    public class PersonalIdentityNumber : IdentityNumber
    {
        public bool ReallyOldPerson { get; set; }

        public override ValidationResult Validate()
        {
            throw new NotImplementedException();
        }
    }
}

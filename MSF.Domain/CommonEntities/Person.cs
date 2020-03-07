using Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSF.Domain
{
    public abstract class Person: BaseEntity<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EMailAddress { get; set; }

        public string ContactNumber { get; set; }

    }
}

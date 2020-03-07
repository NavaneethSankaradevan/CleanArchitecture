using Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSF.Domain
{
    public class Country: BaseEntity<int>
    {
        public string CountryName { get; set; }
    }
}

using MSF.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSF.Service
{
    public sealed class AccessTokenResponse
    {
        public string AccessToken { get; set; }

        public DateTime Expireation { get; set; }

        public string User { get; set; }

    }
}
